[![SDK Banner](views/SDK.png)][ss1]

V3-DotNet-SDK
=============
<p align="center">
    <img src="./os-project-logo.svg" width="150" alt="Logo"/>
</p>
IDG .NET SDK for QuickBooks V3
(Class lib Project written in .NET Standard 2.0- Supports .Net Full Framework 4.6.1, 4.7.2 and .Net Core 2.1,.Net Core 2.2)

**Support:** [![Help](https://img.shields.io/badge/Support-Intuit%20Developer-blue.svg)](https://help.developer.intuit.com/s/) <br/>
**Documentation:** [![User Guide](https://img.shields.io/badge/User%20Guide-SDK%20docs-blue.svg)](https://developer.intuit.com/app/developer/qbo/docs/develop/sdks-and-samples-collections/net) [![Refer SDK class lib docs](https://img.shields.io/badge/Class%20Lib%20Docs-.Net%20SDK-blue.svg)](https://developer-static.intuit.com/SDKDocs/QBV3Doc/IPPDotNetDevKitV3/html/5ca993d2-af77-d050-e246-681e5983b440.htm)<br/>
**License:** [![Apache 2](http://img.shields.io/badge/license-Apache%202-brightgreen.svg)](http://www.apache.org/licenses/LICENSE-2.0) <br/>
**Binaries:** [![Nuget](https://img.shields.io/badge/Nuget-8.1.0.0-blue.svg)](https://www.nuget.org/packages/IppDotNetSdkForQuickBooksApiV3)<br/>


The QuickBooks Online .NET SDK provides a set of .NET class libraries that make it easier to call QuickBooks Online APIs, and access to QuickBooks Online data. It supports .Net Core 2.1, .Net Core 2.2, .Net Full Framework 4.6.1 and 4.7.2. Some of the features included in this SDK are:

* Ability to perform single and batch processing of CRUD operations on all QuickBooks Online entities.
* A common interface to the Request and Response Handler with two implemented classes to handle both synchronous and asynchronous requests.
* Support for both XML and JSON Request and Response formats.
* Ability to configure app settings in the configuration file requiring no additional code change.
* Support for Gzip and Deflate compression formats to improve performance of Service calls to QuickBooks Online.
* Retry policy constructors to help apps handle transient errors.
* Logging mechanisms for trace and request/response logging.
* Query Filters that enable you to write Intuit queries to retrieve QuickBooks Online entities whose properties meet a specified criteria.
* Queries for accessing QuickBooks Reports.
* Sparse Update to update writable properties specified in a request and leave the others unchanged.
* Change data that enables you to retrieve a list of entities modified during specified time points.
* Support for both OAuth1 and OAuth2

Note: 
->Oauth1 support has been removed from this SDK. Intuit.Ipp.Retry logic now have been moved to Intuit.Ipp.Core. So, if you see Retry not found issues while updating your code, just remove that Using Intuit.Ipp.Retry statement and add Using Intuit.Ipp.Core if not already present.
->If you are doing Migration from Oauth1 to Oauth2 using this SDK then please use 7.5.0 version from Nuget only as that is still using .Net 4.6.1 Full Framework and supports both Oauth1 and Oauth2.


## Running Tests

Refer to the following steps to generate all the keys required to run tests using OAuth Playground-
  
* Go to [Developer Docs](https://developer.intuit.com/). 
* Create an app on our IDG platform for the QuickBooks Online v3 APIs. 
* For OAuth2 apps, you will get a set of Development keys, including client key and client secret. This can be used to get OAuth access token and refresh token for sandbox companies.  

* To get Prod app keys to get OAuth tokens for live companies: Go to your app->Settings tab-> enter all URLs and save. Then get the prod keys from Keys tab under Prod section. 
* To get OAuth access tokens, go to your app's dashboard, Click `Oauth Playground`
    * For OAuth2 apps, select the desired app and make sure the OAuth playground redirect_uri is present under Keys tab and go through the OAuth authorization flow to get access and refresh tokens along with the realmid to make API calls for QuickBooks company. 
      * Access tokens are valid for 1 hour which can be refreshed using refesh token. When you request a fresh access token, always use the refresh token returned in the most recent refreshing access token API response. A refresh token expires 24 hours after you receive it. More info can be found at: [Refreshing the access token](https://developer.intuit.com/docs/00_quickbooks_online/2_build/10_authentication_and_authorization/10_oauth_2.0#/Refreshing_the_access_token)
  
      * To 'renew tokens', you can call [Reconnect api](https://developer.intuit.com/docs/0100_quickbooks_online/0100_essentials/0085_develop_quickbooks_apps/0004_authentication_and_authorization/oauth_management_api) after 150 days or do Connect to Quickbooks after 180 days to get new tokens. 


 * NOTE: For sandbox testing, you need to use dev app keys and sandbox base URL. 
For live/prod QuickBooks company testing, use prod app keys and prod base URL after doing a private publish as mentioned below. 
Go to your app->Prod tab-> enter all URLs and save. Then get the prod keys from Keys tab under Prod tab of the app. 
Please refer- 
  * [Blog 1](https://developer.intuit.com/v2/blog/2014/10/20/changes-to-ipp-app-tokens) 
  * [Blog 2](https://developer.intuit.com/blog/2014/10/24/intuit-developer-now-offers-quickbooks-sandboxes) 


* [Unit Tests](https://github.com/IntuitDeveloper/V3-DotNet-SDK/tree/master/IPPDotNetDevKitCSV3/Code) 
  
  * Add all app keys in the [Appsettings.json] and run tests
    
* [Integration Tests](https://github.com/IntuitDeveloper/V3-DotNet-SDK/tree/master/IPPDotNetDevKitCSV3/Test/Intuit.Ipp.Test)
 
  
  * Add All app keys in the [Appsettings.json](https://github.com/IntuitDeveloper/V3-DotNet-SDK/blob/master/IPPDotNetDevKitCSV3/Test/Intuit.Ipp.Test/SDKV3Test/) and run tests


## Contribute:
We greatly encourage contributions! You can add new features, report and fix existing bugs, write docs and
tutorials, or any of the above. Feel free to open issues and/or send pull requests.

The `master` branch of this repository contains the latest release of the SDK, while snapshots are published to the `develop` branch. In general, pull requests should be submitted against `develop` by forking this repo into your account, developing and testing your changes, and creating pull requests to request merges. See the [Contributing to a Project](https://guides.github.com/activities/contributing-to-open-source/)
article for more details about how to contribute.

Steps to contribute:

1. Fork this repository into your account on Github
2. Clone *your forked repository* (not our original one) to your hard drive with `git clone https://github.com/YOURUSERNAME/QuickBooks-V3-DotNET-SDK.git`
3. Design and develop your changes
4. Add/update unit tests
5. Create a pull request for review to request merge
6. Obtain approval before your changes can be merged

Note: Before you submit the pull request, make sure to remove the keys and tokens from appsettings.json

Thank you for your contribution!

[ss1]: https://help.developer.intuit.com/s/SDKFeedback?cid=1155




