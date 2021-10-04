This package is produced as a nuget here: https://msazure.visualstudio.com/DefaultCollection/OneAgile/_packaging?_a=package&feed=PowerApps-Studio-Official&package=Microsoft.PowerPlatform.Canvas.TransportAttributes&protocolType=NuGet&version=1.0.0

If you update anything in Transport.Runtime.Attributes, bump the version in the .csproj, and then you'll need to run this Release against your CI build after checking into master https://msazure.visualstudio.com/OneAgile/_releaseProgress?_a=release-environment-logs&releaseId=43142&environmentId=118853

After that, update the <PackageReference> tags to Microsoft.PowerPlatform.Canvas.TransportAttributes to your new version in this repo and in PowerFx if there are any. 