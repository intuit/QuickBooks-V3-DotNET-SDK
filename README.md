V3-DotNet-SDK
=============

IDG .NET SDK for QuickBooks V3
(Class lib Project written in .Net Framework 4)

**Support:** [![Help](https://img.shields.io/badge/Support-Intuit%20Developer-blue.svg)](https://help.developer.intuit.com/s/) <br/>
**Documentation:** [![User Guide](https://img.shields.io/badge/User%20Guide-SDK%20docs-blue.svg)](https://developer.intuit.com/docs/0100_quickbooks_online/0400_tools/0005_sdks/0010.net_tools) [![Refer SDK class lib docs](https://img.shields.io/badge/Class%20Lib%20Docs-.Net%20SDK-blue.svg)](https://developer-static.intuit.com/SDKDocs/QBV3Doc/IPPDotNetDevKitV3/html/5ca993d2-af77-d050-e246-681e5983b440.htm)<br/>
**License:** [![Apache 2](http://img.shields.io/badge/license-Apache%202-brightgreen.svg)](http://www.apache.org/licenses/LICENSE-2.0) <br/>
**Binaries:** [![Nuget](https://img.shields.io/badge/Nuget-3.2.0-blue.svg)](https://www.nuget.org/packages/IppDotNetSdkForQuickBooksApiV3)<br/>


The QuickBooks Online .Net SDK provides a set of .Net class libraries that make it easier to call QuickBooks Online APIs, and access to QuickBooks Online data. Some of the features included in this SDK are:

* Ability to perform single and batch processing of CRUD operations on all QuickBooks Online entities.
* A common interface to the Request and Response Handler with two implemented classes to handle both synchronous and asynchronous requests.
* Support for both XML and JSON Request and Response format.
* Ability to configure app settings in the configuration file requiring no additional code change.
* Support for Gzip and Deflate compression formats to improve performance of Service calls to QuickBooks Online.
* Retry policy constructors to help apps handle transient errors.
* Logging mechanisms for trace and request/response logging.
* Query Filters that enable you to write Intuit queries to retrieve QuickBooks Online entities whose properties meet a specified criteria.
* Queries for accessing QuickBooks Reports.
* Sparse Update to update writable properties specified in a request and leave the others unchanged.
* Change data that enables you to retrieve a list of entities modified during specified time points.
* .Net CORE is not supported by this SDK.
* Support for Oauth

## Running Tests

Refer steps to generate all the keys required to run tests using OAuth Playground-
  
* Go to [Developer Docs](https://developer.intuit.com/). 
* Create an app on our IDG platform for the QBO v3 apis. 
* You will get a set of Development consumer key, consumer secret and app token.This can be used to get Oauth tokens for sandbox companies. 
* To get Prod app keys to get Oauth tokens for Live companies->Go to your app->Prod tab-> enter all urls and save. Then get the prod keys from Keys tab under Prod tab of the app. 
* Click Test Connect to Oauth->Intuit Anywhere tab->Set time duration in seconds for 15552000sec and get the access token and secret for your app and company by right clicking on the page and doing a view source. 
* You will then set of access token and access token secret and realmid/companyid to make api calls for their QBO company which is valid for 180 days. 
* To 'renew tokens', you can call [Reconnect api](https://developer.intuit.com/docs/0100_quickbooks_online/0100_essentials/0085_develop_quickbooks_apps/0004_authentication_and_authorization/oauth_management_api) after 150 days or do Connect to Quickbooks after 180 days to get new tokens. 


 * NOTE: For sandbox testing, you need to use dev app keys and sandbox base url. 
For live/prod qbo company testing, use prod app keys and prod base url after doing a private publish as mentioned below. 
Go to your app->Prod tab-> enter all urls and save. Then get the prod keys from Keys tab under Prod tab of the app. 
Please refer- 
  * [Blog 1](https://developer.intuit.com/v2/blog/2014/10/20/changes-to-ipp-app-tokens) 
  * [Blog 2](https://developer.intuit.com/blog/2014/10/24/intuit-developer-now-offers-quickbooks-sandboxes) 


* [Unit Tests](https://github.com/IntuitDeveloper/V3-DotNet-SDK/tree/master/IPPDotNetDevKitCSV3/Code) 
  
  * Add all app keys in the [App.config](https://github.com/IntuitDeveloper/V3-DotNet-SDK/blob/master/IPPDotNetDevKitCSV3/Code/App.config) and run tests
    
* [Integration Tests](https://github.com/IntuitDeveloper/V3-DotNet-SDK/tree/master/IPPDotNetDevKitCSV3/Test/Intuit.Ipp.Test)
 
  
  * Add All app keys in the [App.config](https://github.com/IntuitDeveloper/V3-DotNet-SDK/blob/master/IPPDotNetDevKitCSV3/Test/Intuit.Ipp.Test/SDKV3Test/App.config) and run tests


## Contribute:
We greatly encourage contributions! You can add new features, report and fix existing bugs, write docs and
tutorials, or any of the above. Feel free to open issues and/or send pull requests.

The `master` branch of this repository contains the latest stable release of the SDK, while snapshots are published to the `develop` branch. In general, pull requests should be submitted against `develop` by forking this repo into your account, developing and testing your changes, and creating pull requests to request merges. See the [Contributing to a Project](https://guides.github.com/activities/contributing-to-open-source/)
article for more details about how to contribute.

Steps to contribute:

1. Fork this repository into your account on Github
2. Clone *your forked repository* (not our original one) to your hard drive with `git clone https://github.com/YOURUSERNAME/QuickBooks-V3-DotNET-SDK.git`
3. Design and develop your changes
4. Add/update unit tests
5. Create a pull request for review to request merge
6. Obtain approval before your changes can be merged

Note: Before you submit the pull request, make sure to remove the keys and tokens from [App.config](https://github.com/IntuitDeveloper/V3-DotNet-SDK/blob/master/IPPDotNetDevKitCSV3/Code/App.config) and [App.config](https://github.com/IntuitDeveloper/V3-DotNet-SDK/blob/master/IPPDotNetDevKitCSV3/Test/Intuit.Ipp.Test/SDKV3Test/App.config)  that you might have added for testing purpose.

Thank you for your contribution!





