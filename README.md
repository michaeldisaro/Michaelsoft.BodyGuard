# BodyGuard Identity Management
**DISCLAIMER: I build software for myself. This is an early stage work in progress built upon my specific needs. Use it at your own risk. Read the code to understand if it could cause you issues.**  

## What is it
This project contains a .NET Core 3.1 IAM server and its client that provides a direct and simple integration for  manage your application's users with as little configuration as possible.  
  
This is not intended to be another WSO2/Gluu/Keycloak/OpenID identity server, but a modularized solution to the classic ".NET Identity Scaffolding" for projects where the above solutions are definitely an overkill.

**This identity server is for who is really committed to preserve his users' privacy by keeping as less data as possible and encrypting it.**

## How it works

 - You prepare a MongoDB installation ready to accept your users.
 - You install and configure the BodyGuard Server on a machine (I will provide install scripts and configurations for CentOS asap).
 - You import the BodyGuard Client inside your project (I will release a nuget package asap).

Basically you're done: client should provide your application with it's razor pages for registration, login, logout, password recovery, role management, user list, update and delete (in their early stage version).

If you need to integrate the user management into your pages and site navigation, you can import the forms and configure them very easily.

Both of this scenarios are present in the TestWebApp project, I'll provide a very basic wiki to show how easy it's to add this identity server to your project.

## How to model your users
Just model your users as string identifiers, when you need your user's data you'll find some inside the JWT token or you can call the API to get it. 

**NOTE: Be carefull with what you configure to put inside the JWT token, avoid putting sensitive data there!**

## This project needs support, how can you support it?
 - Open issues and ask to solve them.
 - Do some code review.
 - Check for security issues.
 - Contact me to become my sponsor, I'll be happy to have some financial support.

## TODO list
Things to check:  
* Missing APIs.  
* Performance.  
* Check if better security is possible.

Things I'd like to integrate:  
* Cookie consent message and acceptance for each user.  
* Policies messages and acceptance for each user.  
* OAUTH for social networks.