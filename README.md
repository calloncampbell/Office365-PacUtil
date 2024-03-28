# Proxy Auto Configuration (PAC) File Utility for Office 365

This utility interacts with the Office 365 IP Address and URL web service, which helps you better identify and differentiate Office 365 network traffic, making it easier for you to evaluate, configure, and stay up to date with changes.

The intention of the tool is to provide a custom `proxy.pac` template file which has been setup for your network.


## Requirements

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) to run the application.
- [Visual Studio 2022](https://visualstudio.microsoft.com/) if you want to be able to build the solution.


## Networking requirements
- Outbound internet access to the Office 365 IP Address and URL web service. See reference section for documentation.


## Getting started

1. Download the [release](https://github.com/calloncampbell/Office365-PacUtil/releases) and unzip to a folder on your computer.
1. Open up File Explorer and navigate to the folder where you extracted the zip file contents.
1. Open up the `appsettings.json` file and review the configuration and replace the `ClientRequestId` value with a unique GUID for your machine.
1. Update your custom `proxy.pac` file template with the token marker defined in configuration. For example, `/// Office 365 PAC Data ///` would be added to the template file and this is what's replaced.
1. You can check for updates by running the following command:
   ```console
   .\Office365.PacUtil.exe pac-file update-check
   ````
1. Run the following command to generate the PAC file and update your template file:
   ```console
   .\Office365.PacUtil.exe pac-file generate --file "proxy-template.pac"
   ```
1. Optionally, you can force the file generation even if no changes were detected by running the following command:
   ```console
   .\Office365.PacUtil.exe pac-file generate --file "proxy-template.pac" --force
   ```
1. The generated output will be located in your USERS TEMP directory. `C:\Users\{username}\AppData\Local\Temp\PacUtil\proxy.pac`


### Configuration

The following is the configuration found in the `appsettings.json` file.

```json
  "Office365IpUrlWebService": {
    "ClientRequestId": "GENERATE_YOUR_UNIQURE_GUID",
    "TemplateFileTokenStartMarker": "/// START Office 365 PAC Data ///",
    "TemplateFileTokenEndMarker": "/// END Office 365 PAC Data ///",
    "WebServiceRootUrl": "https://endpoints.office.com",
    "OutputPath": "C:\\Temp",
    "OverrideOutputPathWithUserTempPath": true,
    "VersionPath": "O365_endpoints_latestversion.json",
    "DataPath": "O365_endpoints_data.json",
    "Instance": "Worldwide",
    "IncludeIPv6": false
  }
```

- **ClientRequestId** = < guid > - A required GUID that you generate for client association. Generate a unique GUID for each machine that calls the web service. GUID format is xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx, where x represents a hexadecimal number. You can use https://guidgenerator.com/ to generate one.

- **TemplateFileTokenStartMarker** - The start token marker. Used to determine the starting block of text to replace with the generated contents from this tool.

- **TemplateFileTokenEndMarker** - The end token marker. Used to determine the ending block of text to replace with the generated contents from this tool.

- **WebServiceRootUrl** - This is the Microsoft Office 365 IP Address and URL web service URL.

- **OutputPath** - This is the output path if not using the Users Temp Path.

- **OverrideOutputPathWithUserTempPath** - Enable this if you want to use the `OutputPath` value instead of the Users Temp Path. Default is false.

- **VersionPath** - This is the path and filename to store the latest version from the Office 365 IP and Url Web Service. The version value in this file is based on the format of YYYYMMDDNN, where NN is a natural number incremented if there are multiple versions required to be published on a single day, with 00 representing the first update for a given day.

- **DataPath** - This is the path and filename to store the latest data from the Office 365 IP and Url Web Service, which is then used to generate the final output. 

- **Instance** = < Worldwide | China | USGovDoD | USGovGCCHigh > - The short name of the Office 365 service instance.

- **IncludeIPv6** = < true | false > - Set the value to true to exclude IPv6 addresses from the output if you don't use IPv6 in your network. Default is false.


### Output

- The application uses the Users Temp Path by default, but supports an override to a custom path if needed.
- The application saves generated content and file downloads from the Microsoft Office 365 IP Address and URL web service in a sub-folder called `PacUtil`. 
- The application will create any directories that dont exist.


### Template

Here is a snippet of how you would incorporate this into your proxy template file.

```javascript
function FindProxyForURL(url, host)
{
    // ...
	

    // Office 365
	if (
            (
		        shExpMatch(host, "127.0.0.1")
		    ) &&
		    (

/// START Office 365 PAC Data ///

 /// Generated content goes here

/// END Office 365 PAC Data ///
				
			)
	    )
    {
        return direct;
    }


	// ...
}
```

The following is the output file which replaces the token in the template:

```javascript
function FindProxyForURL(url, host)
{
    // ...
	

    // Office 365
	if (
            (
                // Example of something else from local/network:
		        shExpMatch(host, "127.0.0.1")
		    ) &&
		    (

/// START Office 365 PAC Data ///
// Microsoft 365 URLs and IP address data version: 2024013000
// URL to compare version 2024013000 with the latest: https://endpoints.office.com/changes/Worldwide/2024013000?clientrequestid=635f7b4a-ad6c-4ff2-bf39-756935dff84c

                // Event ID 1 - Exchange
                shExpMatch(host, "outlook.office.com") ||
                shExpMatch(host, "outlook.office365.com") ||

                isInNet(myIpAddress(),"13.107.6.152","255.255.255.254") ||
                isInNet(myIpAddress(),"13.107.18.10","255.255.255.254") ||
                isInNet(myIpAddress(),"13.107.128.0","255.255.252.0") ||
                isInNet(myIpAddress(),"23.103.160.0","255.255.240.0") ||
                isInNet(myIpAddress(),"40.96.0.0","255.248.0.0") ||
                isInNet(myIpAddress(),"40.104.0.0","255.254.0.0") ||
                isInNet(myIpAddress(),"52.96.0.0","255.252.0.0") ||
                isInNet(myIpAddress(),"131.253.33.215","255.255.255.255") ||
                isInNet(myIpAddress(),"132.245.0.0","255.255.0.0") ||
                isInNet(myIpAddress(),"150.171.32.0","255.255.252.0") ||
                isInNet(myIpAddress(),"204.79.197.215","255.255.255.255") ||

                // Event ID 2 - Exchange
                shExpMatch(host, "outlook.office365.com") ||
                shExpMatch(host, "smtp.office365.com") ||

                isInNet(myIpAddress(),"13.107.6.152","255.255.255.254") ||
                isInNet(myIpAddress(),"13.107.18.10","255.255.255.254") ||
                isInNet(myIpAddress(),"13.107.128.0","255.255.252.0") ||
                isInNet(myIpAddress(),"23.103.160.0","255.255.240.0") ||
                isInNet(myIpAddress(),"40.96.0.0","255.248.0.0") ||
                isInNet(myIpAddress(),"40.104.0.0","255.254.0.0") ||
                isInNet(myIpAddress(),"52.96.0.0","255.252.0.0") ||
                isInNet(myIpAddress(),"131.253.33.215","255.255.255.255") ||
                isInNet(myIpAddress(),"132.245.0.0","255.255.0.0") ||
                isInNet(myIpAddress(),"150.171.32.0","255.255.252.0") ||
                isInNet(myIpAddress(),"204.79.197.215","255.255.255.255") ||

                // Event ID 8 - Exchange
                shExpMatch(host, "*.outlook.com") ||
                shExpMatch(host, "autodiscover.*.onmicrosoft.com") ||

                // ...

/// END Office 365 PAC Data ///

			)
	    )
    {
        return direct;
    }


	// ...
}
```


## References

[Office 365 IP Address and URL web service](https://learn.microsoft.com/en-us/microsoft-365/enterprise/microsoft-365-ip-web-service?view=o365-worldwide)

- For the latest version of the Office 365 URLs and IP address ranges, use https://endpoints.office.com/version/Worldwide?ClientRequestId=b10c5ed1-bad1-445f-b386-b919946339a7

- For the data on the Office 365 URLs and IP address ranges page for firewalls and proxy servers, use https://endpoints.office.com/endpoints/worldwide?clientrequestid=b10c5ed1-bad1-445f-b386-b919946339a7&NoIPv6=$NoIpv6

- To get all the latest changes since July 2018 when the web service was first available, use https://endpoints.office.com/changes/worldwide/0000000000?ClientRequestId=b10c5ed1-bad1-445f-b386-b919946339a7.

- To get a listing of all versions, use https://endpoints.office.com/version/Worldwide?AllVersions=true&ClientRequestId=b10c5ed1-bad1-445f-b386-b919946339a7.

- To see all the changes from a specific version, for example version `2023073100` we want to use this url with the version string after `/worldwide/{version}?` https://endpoints.office.com/changes/worldwide/2023073100?ClientRequestId=b10c5ed1-bad1-445f-b386-b919946339a7
