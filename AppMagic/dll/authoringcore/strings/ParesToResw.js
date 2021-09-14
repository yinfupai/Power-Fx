const fs = require('fs-extra');
const xml2js = require('xml2js');
const minimist = require('minimist');
const parameters = minimist(process.argv.slice(2));
const util = require("util");

const ERROR_RESOURCE_PREFIX = "ErrorResource_";

const RESW_FILE_SUFFIX = ".resw";
const PARES_FILE_SUFFIX = ".pares";

/*
* To add support for a new Pares Error Resource Tag, add to the 
* paresTagToReswSuffix, and isTagMultiValue objects below.
*
* These objects are used to parse/convert data between resw and pares.
*/
const paresTagToReswSuffix = {
    "shortMessage": "_ShortMessage",
    "longMessage": "_LongMessage",
    "howToFixMessage": "_HowToFix",
    "whyToFixMessage": "_WhyToFix",
    "link": "_Link"
}

const isTagMultiValue = {
    "shortMessage": false,
    "longMessage": false,
    "howToFixMessage": true,
    "whyToFixMessage": false,
    "link": true
}

var reswSuffixToParesTag = {}
for (var tag in paresTagToReswSuffix)
    reswSuffixToParesTag[paresTagToReswSuffix[tag]] = tag;

//#region Pares to Resw helper functions

// Get all the resw data elements for a pares error resource element.
function flattenErrorResource(errorResource) {
    var errorName = errorResource.$.name;
    var flattenResult = [];

    for (var propName in errorResource) {
        var reswSuffix = paresTagToReswSuffix[propName];
        if (typeof(reswSuffix) === "undefined")
            continue;

        if (isTagMultiValue[propName]) {
            flattenResult = flattenResult.concat(getMultiValueErrorResourceElement(errorResource, propName, reswSuffix));
        }
        else {
            flattenResult.push(createDataEntry(ERROR_RESOURCE_PREFIX + errorName + reswSuffix, errorResource[propName][0]));
            postProcessElement(errorResource, propName, flattenResult, 0);
        }
    }
    return flattenResult;
}

// Link elements require preprocessing to remove the "url" subelement.
const linkUrlSuffix = "_URL";
function postProcessLinkTag(errorResource, flattenResult, linkIndex) {
    var errorName = errorResource.$.name;
    var linkElement = errorResource["link"][linkIndex];

    // Add the resource for the link url to the resw.
    flattenResult.push(createDataEntityFromScratch(
        ERROR_RESOURCE_PREFIX + errorName + paresTagToReswSuffix["link"] + "_" + (linkIndex + 1) + linkUrlSuffix,
        linkElement["url"][0],
        "{Locked}"
    ));
}

// Helper to direct any elements in need of preprocessing to the correct function.
function postProcessElement(errorResource, propName, flattenResult, propIndex) {
    if (propName == "link")
        postProcessLinkTag(errorResource, flattenResult, propIndex);
}

// Create a resw data element given the contents (value + comment)
function createDataEntry(dataName, contents) {
    return { '$': { 'name': dataName, 'xml:space': 'preserve' }, value: contents.value, comment: contents.comment };
}

function createDataEntityFromScratch(dataName, value, comment) {
    return {
        '$': { 'name': dataName, 'xml:space': 'preserve' },
        'value': [ value  ],
        'comment': [ comment ]
    }
}

function isErrorResource(dataEntry) {
    return "type" in dataEntry.$ && dataEntry.$.type == "errorResource";
}

// Get all the resw data elements for a multivalued error resource tag element.
function getMultiValueErrorResourceElement(errorResource, elementName, elementSuffix) {
    var errorName = errorResource.$.name;

    var result = [];
    for (var i = 0; i < errorResource[elementName].length; i++)
    {
        result.push(createDataEntry(ERROR_RESOURCE_PREFIX + errorName + elementSuffix + "_" + (i + 1), errorResource[elementName][i]));
        postProcessElement(errorResource, elementName, result, i);
    }
    return result;
}

function convertParesToResw(xml) {
    var stringData = xml.root.data;
    var flattenedStringData = [];

    for (var i = 0; i < stringData.length; i++)
    {
        var dataEntry = stringData[i];
        if (isErrorResource(dataEntry)) {
            flattenedStringData = flattenedStringData.concat(flattenErrorResource(dataEntry));
        } else {
            flattenedStringData.push(dataEntry);
        }
    }

    xml.root.data = flattenedStringData;
}

//#endregion

//#region Resw to Pares helper functions



// Helper to get info on the error resource given the resw string name.
// Output is of format:
// {
//    errorName: The name of the error resource,
//    tag: The tag this particular string belongs to.
//    addHandler: The function used to add this data element to the error resource element (this is addTagToErrorResource() by default).
// }
function getErrorResourceInfo(reswName) {
    if (!reswName.startsWith(ERROR_RESOURCE_PREFIX))
        return null;

    if (reswName.endsWith(linkUrlSuffix))
        return getErrorResourceInfoForLinkUrl(reswName);

    for (var suffix in reswSuffixToParesTag) {
        var tag = reswSuffixToParesTag[suffix];
        if (!isTagMultiValue[tag]) {
            if (reswName.endsWith(suffix))
                return { errorName: getErrorName(reswName, suffix), tag: tag };
        } else {
            var multiValueSuffix = getMultiValueSuffix(reswName, suffix);
            if (multiValueSuffix !== null)
                return { errorName: getErrorName(reswName, multiValueSuffix), tag: tag };
        }
    }

    return null;
}

// Link Urls are separated from the parent Link element in preprocessLinkTag(), so we need
// to add correct info to the ErrorResourceInfo to ensure it is added back correctly.
function getErrorResourceInfoForLinkUrl(reswName) {
    var linkTagSuffix = reswName.substring(0, reswName.length - linkUrlSuffix.length);
    var multiValueSuffix = getMultiValueSuffix(linkTagSuffix, paresTagToReswSuffix["link"]);
    return { errorName: getErrorName(linkTagSuffix, multiValueSuffix), tag: "link", addHandler: addLinkUrlTagToErrorResource }
}

// Helper to get the full suffix (e.g. _HowToFix_7) from the resw string name belonging to
// a multi-valued tag's resource.
function getMultiValueSuffix(reswName, defaultSuffix) {
    var pattern = new RegExp(defaultSuffix + "_[0-9]*");
    var match = pattern.exec(reswName);

    if (match === null)
        return null;
    return match[0];
}

// Helper to get the error resource name given the resw string name and the suffix.
function getErrorName(reswName, tagSuffix) {
    return reswName.substring(ERROR_RESOURCE_PREFIX.length, reswName.length - tagSuffix.length);
}

// Creates a root data element of type errorResource, as expected by the .pares format.
function createErrorResourceRoot(errorName) {
    return { '$': { name: errorName, 'xml:space': 'preserve', type: "errorResource" } };
}

// Given a root object for an error resource, adds an element of name tagName with contents tagObject.
function addTagToErrorResource(errorRoot, tagName, tagObject) {
    if (typeof(errorRoot[tagName]) === "undefined")
        errorRoot[tagName] = [];

    errorRoot[tagName].push(tagObject);
}

function addLinkUrlTagToErrorResource(errorRoot, tagName, tagObject) {
    // Urls always come immediately after the link they are attached to,
    // so it will be the last link added.
    errorRoot["link"][errorRoot["link"].length - 1].url = tagObject.value;
}

function convertReswToPares(xml) {
    var errorResourceEntrys = {};

    var stringData = xml.root.data;
    var convertedStringData = [];

    for (var i = 0; i < stringData.length; i++)
    {
        var dataEntry = stringData[i];
        var dataName = dataEntry.$.name;
        var errorResourceTagInfo = getErrorResourceInfo(dataName);

        // If we couldn't get the resource info, it must be a normal string resource.
        if (errorResourceTagInfo === null)
        {
            convertedStringData.push(dataEntry);
            continue;
        }

        // We don't want superfluous entry names (e.g. "ErrorResource_ErrOperandExpected_HowToFix_1") in the .pares file.
        delete dataEntry.$.name;

        if (typeof(errorResourceEntrys[errorResourceTagInfo.errorName]) === "undefined")
            errorResourceEntrys[errorResourceTagInfo.errorName] = createErrorResourceRoot(errorResourceTagInfo.errorName);

        var addHandler = errorResourceTagInfo.addHandler;
        if (typeof(addHandler) == "undefined")
            addHandler = addTagToErrorResource;

        addHandler(errorResourceEntrys[errorResourceTagInfo.errorName], errorResourceTagInfo.tag, dataEntry);
    }

    for (var errorName in errorResourceEntrys)
        convertedStringData.push(errorResourceEntrys[errorName]);

    xml.root.data = convertedStringData;
}

//#endregion

function convertFolderContents(path, fileSuffixToConvert, fileSuffixPostConvert, conversionFunc) {
    fs.readdir(path, {withFileTypes: true}, function(err, files) {
        for (var i = 0; i < files.length; i++)
        {
            var file = files[i];
            if (file.isDirectory())
                convertFolderContents(path + "/" + file.name, fileSuffixToConvert, fileSuffixPostConvert, conversionFunc);
            else if (file.isFile() && file.name.endsWith(fileSuffixToConvert))
                convertFile(path + "/" + file.name, path + "/" + file.name.substring(0, file.name.length - fileSuffixToConvert.length) + fileSuffixPostConvert, conversionFunc);
        }
    });
}

function writeXmlToFile(xml, path) {
    const xmlbuilder = new xml2js.Builder({ renderOpts: { pretty: true, newline: "\r\n" } });
    const fileContents = xmlbuilder.buildObject(xml).replace(/&#xD;/g, "\r");
    fs.writeFileSync(path, fileContents, "utf8");
}

function convertFile(currentPath, destPath, conversionFunc) {
    fs.readFile(currentPath, "utf8", function(err, contents) {
        xml2js.parseString(contents, function(err, result) {
            conversionFunc(result);
            writeXmlToFile(result, destPath);
        });
    });
}

if ('handback' in parameters)
{
    // Convert the localized .resw files to the .pares format.
    convertFolderContents(__dirname, RESW_FILE_SUFFIX, PARES_FILE_SUFFIX, convertReswToPares);
} else {
    // Convert the .pares files into the expected .resw file format.
    convertFolderContents(__dirname, PARES_FILE_SUFFIX, RESW_FILE_SUFFIX, convertParesToResw);
}