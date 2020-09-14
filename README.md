# BodyGuard 

**Work in progress at very early stage.**

**DO NOT USE IT NOW! OR...**  

**USE IT, BUT HELP ME WITH CONTRIBUTIONS!**

A .NETCore 3.1 server and client that provide direct integration for 
user registration, login, password recovery, etc. and other GDPR compliant 
features with as little configuration as possible.

The objectives are:
* Avoiding use of .net identity scaffolding to any developer
* Providing a separate user management service that runs on another machine/container
with everything crypted by default and helps with gdpr features like policies tracking.
* Providing a client that is as simple to use as *"import nuget package and call
user management APIs to register, login, etc."*
* Providing a service that gives to the web application only the right amount of data 
only at the right time to build a valid JWT to use clientside or to build a valid user 
session data.

If these objectives will be achieved we will never have to loose time again configuring 
a WSO2 for small projects or scaffolding any identity schema. 

We will just model our users like IDs and start a couple of containers/machines:
one with mongodb and one with BodyGuard.

After starting these machines the only thing that remains to do will be adding 
BodyGuardClient to our application.

Things to improve:
* Cryptography configuration outside of json settings

Things to check:
* Client APIs
* Performance

Things I'd like to integrate:
* Cookie consent message and acceptance for each user
* Policy messages and acceptance for each user
* OAUTH for social networks