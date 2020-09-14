# BodyGuard 

**Work in progress at very early stage.**

**DO NOT USE IT NOW! OR...**  

**USE IT, BUT HELP ME WITH CONTRIBUTIONS!**

A .NETCore 3.1 IAM server and client that provide direct integration for 
user registration, login, password recovery, etc. and other GDPR compliant 
features with as little configuration as possible.

The objective is not making another WSO2/Gluu/Keycloak with all of their flows.

The objectives are:
* Avoiding use of .net identity scaffolding to any developer
* Providing a separate IAM service with everything crypted by default and that helps 
with gdpr features like policies tracking.
* Providing a client that is as simple to use as *"import nuget package and call
user management APIs to register, login, etc."* with little to no configuration.
* Providing a service that gives to the web application only the right amount of data 
only at the right time to build a valid JWT or a valid user session.

If these objectives will be achieved we will never have to loose time again configuring 
a WSO2/Gluu/Keycloak or scaffolding any identity schema for small to medium projects where
OAUTH/SAML/SSOetc. are not required.

We will just model our schema with user IDs, start a container/machine for BodyGuardServer and
add the BodyGuardClient to our application.

Things to improve:
* Cryptography configuration outside of json settings.
* Improve hashing algorithms (Sha1 is there just as a proof of concept).

Things to check:
* Missing APIs.
* Performance.

Things I'd like to integrate:
* Cookie consent message and acceptance for each user.
* Policy messages and acceptance for each user.
* OAUTH for social networks.
