#DotNet Sample App for OAuth
=====================================
<p>
Welcome to the Intuit Developer's DotNet OAuth Sample App.
</p>
<p>
This sample app is meant to provide a working example of oAuth management.
</p>
<p>

OAuth Management APIs consists of the following:

<ul>
<li>Intuit OAuth Service URLs - URLs to use to access Intuit OAuth Services.</li>
<li>Request token URL - Request token endpoint to retrieve request token and secret</li> 
<li>Access token URL - Access token endpoint to retrieve access token and secret </li>
<li>Authorize URL - To authorize access to the third party app</li>
</ul>
</p>
<p>

This is not a seed project to be taken cart blanche and deployed to your production environment. Certain concerns are not addressed at all in our samples (e.g. security, privacy, scalability). In our sample apps, we strive to strike a balance between clarity, maintainability, and performance where we can. However, clarity is ultimately the most important quality in a sample app.

</p>

## Table of Contents

* [Prerequisites](#prerequisites)
* [First Use Instructions](#first-use-instructions)
* [Running the app](#running-the-app)
* [Project Structure](#project-structure)

## Prerequisites

1. Vsiual Studio 2012 or later
2. .Net framework 4.5
3. Developer.intuit.com account
4. An app on developer.intuit.com and the associated app token, consumer key, and consumer secret.

## Configuration
![Alt text](images/config.JPG "Configurations")

## First Use Instructions:

- Clone the GitHub repo to your workspace

Note: This sample is used for understanding how oauth works

- Configure the app tokens: Go to your app on developer.intuit.com and copy the OAuth Consumer Key and OAuth Consumer Token from the keys tab. Add these values to the app.properties file in our OauthSample folder.

- Build and Run the code using Visual Studio

## Run the App:

Once the sample app code is on your computer, follow the steps below to run the app:

- Run the app

- Connect your app to Quickbooks, by clicking on Connect to QuickBooks button and follow the instructions on the screen.

- After successfully connecting the app to QuickBooks.

## How To Guides

The following How-To guides related to implementation tasks necessary to produce a production-ready Intuit Partner Platform app (e.g. OAuth, OpenId, etc) are available:

* [OAuth How To Guide](https://developer.intuit.com/blog/2015/02/19/oauth-for-intuit-demystified)
