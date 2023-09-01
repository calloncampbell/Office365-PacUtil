# Proxy Auto Configuration (PAC) File Utility for Office 365

This utility interacts with the Office 365 IP Address and URL web service, which helps you better identify and differentiate Office 365 network traffic, making it easier for you to evaluate, configure, and stay up to date with changes.

The intention of the tool is to provide a custom `proxy.pac` template file which has been setup for your network.


## Requirements
- [.NET 6]() to run the application.
- [Visual Studio 2022 Community Edition (free)]() if you want to be able to build the solution.


## Getting started

1. Download the release and unzip to a folder on your computer.
1. Open up File Expolorer and navigate to the folder where you extracted the zip file.
1. Open up the `appsettings.json` file and review the configuration and replace the `ClientRequestId` value with a unique GUID for your machine.
1. Update your custom `proxy.pac` file template with the token marker defined in configuration. For example, `/// Office 365 PAC Data ///` would be added to the template file and this is what's replaced.
1. You can check for updates by running the following command:
   ```console
   .\Office365.PacUtil.exe pac-file update-check"
   ````
1. Run the following command to generate the PAC file and update your template file:
   ```console
   .\Office365.PacUtil.exe pac-file generate --file "proxy-template.pac"
   ````
1. The generated output will be located in your USERS TEMP directory. `C:\Users\{username}\AppData\Local\Temp\PacUtil\proxy.pac`

### Configuration

The following is the configuration found in the `appsettings.json` file.

```json
  "Office365IpUrlWebService": {
    "ClientRequestId": "GENERATE_YOUR_UNIQURE_GUID",
    "TemplateFileTokenMarker": "/// Office 365 PAC Data ///",
    "WebServiceRootUrl": "https://endpoints.office.com",
    "VersionPath": "PacUtil\\O365_endpoints_latestversion.json",
    "DataPath": "PacUtil\\O365_endpoints_data.json",
    "Instance": "Worldwide",
    "IncludeIPv6": false
  }
```

- **ClientRequestId** = < guid > - A required GUID that you generate for client association. Generate a unique GUID for each machine that calls the web service. GUID format is xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx, where x represents a hexadecimal number.

- **TemplateFileTokenMarker** - The token marker to insert into your PAC file template which is replaced by the content generated from this tool.

- **VersionPath** - This is the path and filename to store the latest version from the Office 365 IP and Url Web Service. The version value in this file is based on the format of YYYYMMDDNN, where NN is a natural number incremented if there are multiple versions required to be published on a single day, with 00 representing the first update for a given day.

- **DataPath** - This is the path and filename to store the latest data from the Office 365 IP and Url Web Service, which is then used to generate

- **Instance** = < Worldwide | China | USGovDoD | USGovGCCHigh > - The short name of the Office 365 service instance.

- **IncludeIPv6** = < true | false > - Set the value to true to exclude IPv6 addresses from the output if you don't use IPv6 in your network. Default is false.


## References

[Office 365 IP Address and URL web service](https://learn.microsoft.com/en-us/microsoft-365/enterprise/microsoft-365-ip-web-service?view=o365-worldwide)

- For the latest version of the Office 365 URLs and IP address ranges, use https://endpoints.office.com/version/Worldwide?ClientRequestId=b10c5ed1-bad1-445f-b386-b919946339a7

- For the data on the Office 365 URLs and IP address ranges page for firewalls and proxy servers, use https://endpoints.office.com/endpoints/worldwide.
  - https://endpoints.office.com/endpoints/worldwide?clientrequestid=b10c5ed1-bad1-445f-b386-b919946339a7&NoIPv6=$NoIpv6

- To get all the latest changes since July 2018 when the web service was first available, use https://endpoints.office.com/changes/worldwide/0000000000.
