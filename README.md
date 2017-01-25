V3-DotNet-SDK
=============

IDG .NET SDK for QuickBooks V3
(Class lib Project written in .Net Framework 4)

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

## Running Tests
* [Unit Tests](https://github.com/IntuitDeveloper/V3-DotNet-SDK/tree/master/IPPDotNetDevKitCSV3/Code)
  * Add all app keys in the App.config and run tests
      <!--Specify AccessToken Value for QBO-->
        <add key="AccessTokenQBO" value="" />

        <!--Specify accessTokenSecret Value for QBO-->
        <add key="accessTokenSecretQBO" value="" />

        <!--Specify consumerKey Value for QBO-->
        <add key="consumerKeyQBO" value="" />

        <!--Specify consumerSecret Value for QBO-->
        <add key="consumerSecretQBO" value="" />

        <!--Specify realmIdIA Value for QBO-->
        <add key="realmIdIAQBO" value="" />   

        <!--Specify AppToken Value for QBO-->
        <add key="AppTokenQBO" value="" />
    
* [Integration Tests](https://github.com/IntuitDeveloper/V3-DotNet-SDK/tree/master/IPPDotNetDevKitCSV3/Test/Intuit.Ipp.Test)
  * All app keys in the App.config and run tests
    <add key="ConsumerKeyQBO" value="" />
    <add key="ConsumerSecretQBO" value="" />
    <add key="AccessTokenQBO" value="" />
    <add key="AccessTokenSecretQBO" value="" />
    <add key="realmIdIAQBO" value="" />

## Project Overview
* [Refer SDK docs](https://developer.intuit.com/docs/0100_quickbooks_online/0400_tools/0005_accounting)
* [Refer SDK class lib docs](https://github.com/intuit/QuickBooks-V3-DotNET-SDK/tree/master/IPPDotNetDevKitCSV3/Class%20Lib%20Docs)

## Sample Apps

* [CRUD Sample](https://github.com/IntuitDeveloper/SampleApp-CRUD-.Net)
* [Webhooks Sample](https://github.com/IntuitDeveloper/SampleApp-Webhooks-DotNet)
* [OAuth Sample](https://github.com/IntuitDeveloper/oauth-dotnet)
* [OpenId-OAuth Sample](https://github.com/IntuitDeveloper/SampleApp-OpenID-Oauth-Java)
* [MVC3](https://github.com/IntuitDeveloper/QuickbooksV3API-DotNet-Mvc3-Sample)
* [MVC5](https://github.com/IntuitDeveloper/SampleApp-TimeTracking_Invoicing-DotNet)

## Contribute

We greatly encourage contributions! You can add new features, report and fix existing bugs, write docs and
tutorials, or any of the above. Feel free to open issues and/or send pull requests.

The `master` branch of this repository contains the latest stable release of .Net SDK, while snapshots are published to the `develop` branch. In general, pull requests should be submitted against `develop` by forking this repo into your account, developing and testing your changes, and creating pull requests to request merges. See the [Contributing to a Project](https://guides.github.com/activities/contributing-to-open-source/)
article for more details about how to contribute.

Extension projects such as browser plugins, client integration libraries, and apps can be contributed under the `contrib` directory.

Steps to contribute:

1. Fork this repository into your account on Github
2. Clone *your forked repository* (not our original one) to your hard drive with `git clone  https://github.com/intuit/YOURUSERNAME/QuickBooks-V3-DotNET-SDK.git'
3. Design and develop your changes
4. Add/update unit tests
5. Add/update integration tests
6. Add/update documentation on `gh-pages` branch
7. Create a pull request for review to request merge
8. Obtain 2 approval _squirrels_ before your changes can be merged

Thank you for your contribution!
