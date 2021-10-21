//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.PowerFx.Core;
using Microsoft.PowerFx.LanguageServerProtocol.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.PowerFx.Core.Public;

namespace Microsoft.PowerFx.Tests.LanguageServiceProtocol.Tests
{
    [TestClass]
    public class LanguageServerTests
    {
        protected static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        protected static List<string> _sendToClientData;
        protected static TestPowerFxScopeFactory _scopeFactory;
        protected static TestLanguageServer _testServer;

        [ClassInitialize]
        public static void Initialize(TestContext _)
        {
            _sendToClientData = new List<string>();
            _scopeFactory = new TestPowerFxScopeFactory((string documentUri) => RecalcEngineScope.FromUri(new RecalcEngine(), documentUri));
            _testServer = new TestLanguageServer(_sendToClientData.Add, _scopeFactory);
        }

        /// <summary>
        /// We clear the list before each test is run instead of after.
        /// </summary>
        [TestInitialize]
        public void TestInitialize() => _sendToClientData.Clear();

        /// <summary>
        /// When the last test finishes then the list will be cleared again.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup() => _sendToClientData.Clear();

        [TestMethod]
        public void TestLanguageServerCommunication()
        {
            // bad payload
            _testServer.OnDataReceived(JsonSerializer.Serialize(new { }));

            // bad jsonrpc payload
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0"
            }));

            // bad notification payload
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                method = "unknown",
                @params = "unkown"
            }));

            // bad request payload
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "abc",
                method = "unknown",
                @params = "unkown"
            }));

            // verify we have 4 json rpc responeses
            Assert.AreEqual(4, _sendToClientData.Count);
            var errorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", errorResponse.Jsonrpc);
            Assert.AreEqual(null, errorResponse.Id);
            Assert.AreEqual(-32600, errorResponse.Error.Code);

            errorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(_sendToClientData[1], _jsonSerializerOptions);
            Assert.AreEqual("2.0", errorResponse.Jsonrpc);
            Assert.AreEqual(null, errorResponse.Id);
            Assert.AreEqual(-32600, errorResponse.Error.Code);

            errorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(_sendToClientData[2], _jsonSerializerOptions);
            Assert.AreEqual("2.0", errorResponse.Jsonrpc);
            Assert.AreEqual(null, errorResponse.Id);
            Assert.AreEqual(-32601, errorResponse.Error.Code);

            errorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(_sendToClientData[3], _jsonSerializerOptions);
            Assert.AreEqual("2.0", errorResponse.Jsonrpc);
            Assert.AreEqual("abc", errorResponse.Id);
            Assert.AreEqual(-32601, errorResponse.Error.Code);
        }

        [TestMethod]
        public void TestDidChange()
        {
            // test good formula
            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                method = "textDocument/didChange",
                @params = new DidChangeTextDocumentParams()
                {
                    TextDocument = new VersionedTextDocumentIdentifier()
                    {
                        Uri = "powerfx://app?context={\"A\":1,\"B\":[1,2,3]}",
                        Version = 1,
                    },
                    ContentChanges = new TextDocumentContentChangeEvent[]
                    {
                        new TextDocumentContentChangeEvent() { Text = "A+CountRows(B)"}
                    }
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            var notification = JsonSerializer.Deserialize<JsonRpcPublishDiagnosticsNotification>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", notification.Jsonrpc);
            Assert.AreEqual("textDocument/publishDiagnostics", notification.Method);
            Assert.AreEqual("powerfx://app?context={\"A\":1,\"B\":[1,2,3]}", notification.Params.Uri);
            Assert.AreEqual(0, notification.Params.Diagnostics.Length);

            // test bad formula
            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                method = "textDocument/didChange",
                @params = new DidChangeTextDocumentParams()
                {
                    TextDocument = new VersionedTextDocumentIdentifier()
                    {
                        Uri = "powerfx://app",
                        Version = 1,
                    },
                    ContentChanges = new TextDocumentContentChangeEvent[]
                    {
                        new TextDocumentContentChangeEvent() { Text = "AA"}
                    }
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            notification = JsonSerializer.Deserialize<JsonRpcPublishDiagnosticsNotification>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", notification.Jsonrpc);
            Assert.AreEqual("textDocument/publishDiagnostics", notification.Method);
            Assert.AreEqual("powerfx://app", notification.Params.Uri);
            Assert.AreEqual(1, notification.Params.Diagnostics.Length);
            Assert.AreEqual("Name isn't valid. This identifier isn't recognized.", notification.Params.Diagnostics[0].Message);

            // some invalid cases
            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new { }));
            Assert.AreEqual(1, _sendToClientData.Count);
            var errorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", errorResponse.Jsonrpc);
            Assert.AreEqual(null, errorResponse.Id);
            Assert.AreEqual(-32600, errorResponse.Error.Code);

            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                method = "textDocument/didChange"
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            errorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", errorResponse.Jsonrpc);
            Assert.AreEqual(null, errorResponse.Id);
            Assert.AreEqual(-32600, errorResponse.Error.Code);

            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                method = "textDocument/didChange",
                @params = ""
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            errorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", errorResponse.Jsonrpc);
            Assert.AreEqual(null, errorResponse.Id);
            Assert.AreEqual(-32700, errorResponse.Error.Code);
        }

        private void TestPublishDiagnostics(string uri, string method, string formula, Diagnostic[] expectedDiagnostics)
        {
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                method,
                @params = new DidOpenTextDocumentParams()
                {
                    TextDocument = new TextDocumentItem()
                    {
                        Uri = uri,
                        LanguageId = "powerfx",
                        Version = 1,
                        Text = formula
                    }
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            var notification = JsonSerializer.Deserialize<JsonRpcPublishDiagnosticsNotification>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", notification.Jsonrpc);
            Assert.AreEqual("textDocument/publishDiagnostics", notification.Method);
            Assert.AreEqual(uri, notification.Params.Uri);
            Assert.AreEqual(expectedDiagnostics.Length, notification.Params.Diagnostics.Length);

            for (var i = 0; i < expectedDiagnostics.Length; i++)
            {
                var expectedDiagnostic = expectedDiagnostics[i];
                var actualDiagnostic = notification.Params.Diagnostics[i];
                Assert.AreEqual(expectedDiagnostic.Message, actualDiagnostic.Message);
            }
        }

        [DataTestMethod]
        [DataRow("A+CountRows(B)", "{\"A\":1,\"B\":[1,2,3]}")]
        public void TestDidOpenValidFormula(string formula, string context = null)
        {
            var uri = $"powerfx://app{(context != null ? "powerfx://app?context=" + context : "")}";
            TestPublishDiagnostics(uri, "textDocument/didOpen", formula, new Diagnostic[0]);
        }

        [DataTestMethod, Owner("jokellih")]
        [DataRow("AA", null, "Name isn't valid. This identifier isn't recognized.")]
        [DataRow("1+CountRowss", null, "Name isn't valid. This identifier isn't recognized.")]
        [DataRow("CountRows(2)", null, "Invalid argument type (Number). Expecting a Table value instead.", "The function 'CountRows' has some invalid arguments.")]
        public void TestDidOpenErroneousFormula(string formula, string context, params string[] expectedErrors)
        {
            var expectedDiagnostics = expectedErrors.Select(error => new Diagnostic()
            {
                Message = error,
                Severity = DiagnosticSeverity.Error
            }).ToArray();
            TestPublishDiagnostics("powerfx://app", "textDocument/didOpen", formula, expectedDiagnostics);
        }

        [TestMethod, Owner("jokellih")]
        public void TestDidOpenSeverityFormula()
        {
            var formula = "Count([\"test\"])";
            var expectedDiagnostics = new []
            {
                new Diagnostic()
                {
                    Message = "Invalid schema, expected a column of numeric values for 'Value'.",
                    Severity = DiagnosticSeverity.Warning
                },
                new Diagnostic()
                {
                    Message = "The function 'Count' has some invalid arguments.",
                    Severity = DiagnosticSeverity.Error
                },
            };
            TestPublishDiagnostics("powerfx://app", "textDocument/didOpen", formula, expectedDiagnostics);
        }

        [TestMethod]
        public void TestCompletion()
        {
            // test good formula
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "123",
                method = "textDocument/completion",
                @params = new CompletionParams()
                {
                    TextDocument = new TextDocumentIdentifier()
                    {
                        Uri = "powerfx://test?expression=Color.AliceBl&context={}"
                    },
                    Position = new Position()
                    {
                        Line = 0,
                        Character = 13
                    },
                    Context = new CompletionContext()
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            var response = JsonSerializer.Deserialize<JsonRpcCompletionResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", response.Jsonrpc);
            Assert.AreEqual("123", response.Id);
            var foundItems = response.Result.Items.Where(item => item.Label == "AliceBlue");
            Assert.AreEqual(1, Enumerable.Count(foundItems), "AliceBlue should be found from suggestion result");

            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "123",
                method = "textDocument/completion",
                @params = new CompletionParams()
                {
                    TextDocument = new TextDocumentIdentifier()
                    {
                        Uri = "powerfx://test?expression=Color.&context={\"A\":\"ABC\",\"B\":{\"Inner\":123}}"
                    },
                    Position = new Position()
                    {
                        Line = 0,
                        Character = 7
                    },
                    Context = new CompletionContext()
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            response = JsonSerializer.Deserialize<JsonRpcCompletionResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", response.Jsonrpc);
            Assert.AreEqual("123", response.Id);
            foundItems = response.Result.Items.Where(item => item.Label == "AliceBlue");
            Assert.AreEqual(CompletionItemKind.Variable, foundItems.First().Kind);
            Assert.AreEqual(1, Enumerable.Count(foundItems), "AliceBlue should be found from suggestion result");

            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "123",
                method = "textDocument/completion",
                @params = new CompletionParams()
                {
                    TextDocument = new TextDocumentIdentifier()
                    {
                        Uri = "powerfx://test?expression={a:{},b:{},c:{}}."
                    },
                    Position = new Position()
                    {
                        Line = 0,
                        Character = 17
                    },
                    Context = new CompletionContext()
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            response = JsonSerializer.Deserialize<JsonRpcCompletionResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", response.Jsonrpc);
            Assert.AreEqual("123", response.Id);
            foundItems = response.Result.Items.Where(item => item.Label == "a");
            Assert.AreEqual(1, Enumerable.Count(foundItems), "'a' should be found from suggestion result");
            Assert.AreEqual(CompletionItemKind.Variable, foundItems.First().Kind);
            foundItems = response.Result.Items.Where(item => item.Label == "b");
            Assert.AreEqual(1, Enumerable.Count(foundItems), "'b' should be found from suggestion result");
            Assert.AreEqual(CompletionItemKind.Variable, foundItems.First().Kind);
            foundItems = response.Result.Items.Where(item => item.Label == "c");
            Assert.AreEqual(1, Enumerable.Count(foundItems), "'c' should be found from suggestion result");
            Assert.AreEqual(CompletionItemKind.Variable, foundItems.First().Kind);

            // missing 'expression' in documentUri
            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "123",
                method = "textDocument/completion",
                @params = new CompletionParams()
                {
                    TextDocument = new TextDocumentIdentifier()
                    {
                        Uri = "powerfx://test"
                    },
                    Position = new Position()
                    {
                        Line = 0,
                        Character = 1
                    },
                    Context = new CompletionContext()
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            var errorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", errorResponse.Jsonrpc);
            Assert.AreEqual("123", errorResponse.Id);
            Assert.AreEqual(-32602, errorResponse.Error.Code);
        }

        [TestMethod]
        [DataRow("{1}", 1)]
        [DataRow("12{3}45", 3)]
        [DataRow("1234{5}", 5)]
        [DataRow("123\n1{2}3", 2)]
        [DataRow("123\n{5}67", 1)]
        [DataRow("123\n5{6}7", 2)]
        [DataRow("123\n56{7}", 3)]
        [DataRow("123\n567{\n}890", 3)]
        public void TestGetCharPosition(string expression, int expected)
        {
            string pattern = @"\{[0-9|\n]\}";
            var re = new Regex(pattern);
            var matches = re.Matches(expression);
            Assert.AreEqual(1, matches.Count);

            var position = matches[0].Index;
            expression = expression.Substring(0, position) + expression[position + 1] + expression.Substring(position + 3);

            Assert.AreEqual(expected, _testServer.TestGetCharPosition(expression, position));
        }

        [TestMethod]
        public void TestGetPosition()
        {
            Assert.AreEqual(0, _testServer.TestGetPosition("123", 0, 0));
            Assert.AreEqual(1, _testServer.TestGetPosition("123", 0, 1));
            Assert.AreEqual(2, _testServer.TestGetPosition("123", 0, 2));
            Assert.AreEqual(4, _testServer.TestGetPosition("123\n123456\n12345", 1, 0));
            Assert.AreEqual(5, _testServer.TestGetPosition("123\n123456\n12345", 1, 1));
            Assert.AreEqual(11, _testServer.TestGetPosition("123\n123456\n12345", 2, 0));
            Assert.AreEqual(13, _testServer.TestGetPosition("123\n123456\n12345", 2, 2));
            Assert.AreEqual(3, _testServer.TestGetPosition("123", 0, 999));
        }

        [TestMethod]
        public void TestSignatureHelp()
        {
            // test good formula
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "123",
                method = "textDocument/signatureHelp",
                @params = new SignatureHelpParams()
                {
                    TextDocument = new TextDocumentIdentifier()
                    {
                        Uri = "powerfx://test?expression=Power(&context={}"
                    },
                    Position = new Position()
                    {
                        Line = 0,
                        Character = 6
                    },
                    Context = new SignatureHelpContext()
                    {
                        TriggerKind = SignatureHelpTriggerKind.TriggerCharacter,
                        TriggerCharacter = "("
                    }
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            var response = JsonSerializer.Deserialize<JsonRpcSignatureHelpResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", response.Jsonrpc);
            Assert.AreEqual("123", response.Id);
            Assert.AreEqual((uint)0, response.Result.ActiveSignature);
            Assert.AreEqual((uint)0, response.Result.ActiveParameter);
            var foundItems = response.Result.Signatures.Where(item => item.Label.StartsWith("Power"));
            Assert.AreEqual(1, Enumerable.Count(foundItems), "Power should be found from signatures result");
            Assert.AreEqual((uint)0, foundItems.First().ActiveParameter);
            Assert.AreEqual(2, foundItems.First().Parameters.Length);
            Assert.AreEqual("base", foundItems.First().Parameters[0].Label);
            Assert.AreEqual("exponent", foundItems.First().Parameters[1].Label);

            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "123",
                method = "textDocument/signatureHelp",
                @params = new SignatureHelpParams()
                {
                    TextDocument = new TextDocumentIdentifier()
                    {
                        Uri = "powerfx://test?expression=Power(2,&context={}"
                    },
                    Position = new Position()
                    {
                        Line = 0,
                        Character = 8
                    },
                    Context = new SignatureHelpContext()
                    {
                        TriggerKind = SignatureHelpTriggerKind.TriggerCharacter,
                        TriggerCharacter = ","
                    }
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            response = JsonSerializer.Deserialize<JsonRpcSignatureHelpResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", response.Jsonrpc);
            Assert.AreEqual("123", response.Id);
            Assert.AreEqual((uint)0, response.Result.ActiveSignature);
            Assert.AreEqual((uint)1, response.Result.ActiveParameter);
            foundItems = response.Result.Signatures.Where(item => item.Label.StartsWith("Power"));
            Assert.AreEqual(1, Enumerable.Count(foundItems), "Power should be found from signatures result");
            Assert.AreEqual((uint)0, foundItems.First().ActiveParameter);
            Assert.AreEqual(2, foundItems.First().Parameters.Length);
            Assert.AreEqual("base", foundItems.First().Parameters[0].Label);
            Assert.AreEqual("exponent", foundItems.First().Parameters[1].Label);

            // missing 'expression' in documentUri
            _sendToClientData.Clear();
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "123",
                method = "textDocument/signatureHelp",
                @params = new CompletionParams()
                {
                    TextDocument = new TextDocumentIdentifier()
                    {
                        Uri = "powerfx://test"
                    },
                    Position = new Position()
                    {
                        Line = 0,
                        Character = 1
                    },
                    Context = new CompletionContext()
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            var errorResponse = JsonSerializer.Deserialize<JsonRpcErrorResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("2.0", errorResponse.Jsonrpc);
            Assert.AreEqual("123", errorResponse.Id);
            Assert.AreEqual(-32602, errorResponse.Error.Code);
        }

        [TestMethod]
        public void TestPublishTokens()
        {
            // getTokensFlags = 0x0 (none), 0x1 (tokens inside expression), 0x2 (all functions)
            var documentUri = "powerfx://app?context={\"A\":1,\"B\":[1,2,3]}&getTokensFlags=1";
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                method = "textDocument/didOpen",
                @params = new DidOpenTextDocumentParams()
                {
                    TextDocument = new TextDocumentItem()
                    {
                        Uri = documentUri,
                        LanguageId = "powerfx",
                        Version = 1,
                        Text = "A+CountRows(B)"
                    }
                }
            }));
            Assert.AreEqual(2, _sendToClientData.Count);
            var response = JsonSerializer.Deserialize<JsonRpcPublishTokensNotification>(_sendToClientData[1], _jsonSerializerOptions);
            Assert.AreEqual("$/publishTokens", response.Method);
            Assert.AreEqual(documentUri, response.Params.Uri);
            Assert.AreEqual(3, response.Params.Tokens.Count);
            Assert.AreEqual(TokenResultType.Variable, response.Params.Tokens["A"]);
            Assert.AreEqual(TokenResultType.Variable, response.Params.Tokens["B"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["CountRows"]);

            // getTokensFlags = 0x0 (none), 0x1 (tokens inside expression), 0x2 (all functions)
            _sendToClientData.Clear();
            documentUri = "powerfx://app?context={\"A\":1,\"B\":[1,2,3]}&getTokensFlags=2";
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                method = "textDocument/didChange",
                @params = new DidChangeTextDocumentParams()
                {
                    TextDocument = new VersionedTextDocumentIdentifier()
                    {
                        Uri = documentUri,
                        Version = 1,
                    },
                    ContentChanges = new TextDocumentContentChangeEvent[]
                    {
                        new TextDocumentContentChangeEvent() { Text = "A+CountRows(B)"}
                    }
                }
            }));
            Assert.AreEqual(2, _sendToClientData.Count);
            response = JsonSerializer.Deserialize<JsonRpcPublishTokensNotification>(_sendToClientData[1], _jsonSerializerOptions);
            Assert.AreEqual("$/publishTokens", response.Method);
            Assert.AreEqual(documentUri, response.Params.Uri);
            Assert.AreEqual(0, Enumerable.Count(response.Params.Tokens.Where(it => it.Value != TokenResultType.Function)));
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["Abs"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["Clock.AmPm"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["CountRows"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["VarP"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["Year"]);

            // getTokensFlags = 0x0 (none), 0x1 (tokens inside expression), 0x2 (all functions)
            _sendToClientData.Clear();
            documentUri = "powerfx://app?context={\"A\":1,\"B\":[1,2,3]}&getTokensFlags=3";
            _testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                method = "textDocument/didChange",
                @params = new DidChangeTextDocumentParams()
                {
                    TextDocument = new VersionedTextDocumentIdentifier()
                    {
                        Uri = documentUri,
                        Version = 1,
                    },
                    ContentChanges = new TextDocumentContentChangeEvent[]
                    {
                        new TextDocumentContentChangeEvent() { Text = "A+CountRows(B)"}
                    }
                }
            }));
            Assert.AreEqual(2, _sendToClientData.Count);
            response = JsonSerializer.Deserialize<JsonRpcPublishTokensNotification>(_sendToClientData[1], _jsonSerializerOptions);
            Assert.AreEqual("$/publishTokens", response.Method);
            Assert.AreEqual(documentUri, response.Params.Uri);
            Assert.AreEqual(TokenResultType.Variable, response.Params.Tokens["A"]);
            Assert.AreEqual(TokenResultType.Variable, response.Params.Tokens["B"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["Abs"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["Clock.AmPm"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["CountRows"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["VarP"]);
            Assert.AreEqual(TokenResultType.Function, response.Params.Tokens["Year"]);
        }

        [TestMethod]
        public void TestInitialFixup()
        {
            var scopeFactory = new TestPowerFxScopeFactory((string documentUri) => new MockSqlEngine());
            var testServer = new TestLanguageServer(_sendToClientData.Add, scopeFactory);
            var documentUri = "powerfx://app?context={\"A\":1,\"B\":[1,2,3]}";
            testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "123",
                method = "$/initialFixup",
                @params = new InitialFixupParams()
                {
                    TextDocument = new TextDocumentItem()
                    {
                        Uri = documentUri,
                        LanguageId = "powerfx",
                        Version = 1,
                        Text = "new_price * new_quantity"
                    }
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            var response = JsonSerializer.Deserialize<JsonRpcInitialFixupResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("123", response.Id);
            Assert.AreEqual(documentUri, response.Result.Uri);
            Assert.AreEqual("Price * Quantity", response.Result.Text);

            // no change
            _sendToClientData.Clear();
            testServer.OnDataReceived(JsonSerializer.Serialize(new
            {
                jsonrpc = "2.0",
                id = "123",
                method = "$/initialFixup",
                @params = new InitialFixupParams()
                {
                    TextDocument = new TextDocumentItem()
                    {
                        Uri = documentUri,
                        LanguageId = "powerfx",
                        Version = 1,
                        Text = "Price * Quantity"
                    }
                }
            }));
            Assert.AreEqual(1, _sendToClientData.Count);
            response = JsonSerializer.Deserialize<JsonRpcInitialFixupResponse>(_sendToClientData[0], _jsonSerializerOptions);
            Assert.AreEqual("123", response.Id);
            Assert.AreEqual(documentUri, response.Result.Uri);
            Assert.AreEqual("Price * Quantity", response.Result.Text);
        }
    }
}
