mvc-concept-bootstrap
=====================
current version: 0.1

Author: Hugo Zapata ( www.hugozap.com)
License: MIT

A bootstrap project for ASPNET MVC applications with OAuth authentication and other utilities to create cool web apps

## Goals
Making it easy and fast to experiment and create fun applications without having to spend too much time setting up
the project, dependencies, etc.

The goal of this project is to have a bootstrap base to create modern web applications using ASP.NET MVC .
The kind of applications this bootstrap will be optimized for are:

* One page applications (where UI is created dynamically and most html is generated in the client)
* OAuth authentication is used (Twitter, Facebook)
* Client and server exchange mostly JSON data ( less razor views )
* KnockoutJS friendly (But could be used with other frameworks too)
* AppHarbor friendly ( ex: Using only one database for data and membership tables )

I prefer simplicity, and i hope to keep this project simple. The main focus will be on allowing
developers to create cool things and don't waste too much time in plumbing

## Only one assembly.
If you want to complicate things then you can create all the projects you want, i prefer just one because.
The less dependencies the better

## Milestones v1.

* Twitter and Facebook authentication with DotNetOAuth
* Membership authentication support 
* Twitter Bootstrap integration 
* Html Helpers for knockout
* Error handling
* Entity Framework Code First basic setup
* Pusher integration andutilities for realtime functionality


## FrontEnd libraries used

* KnockoutJS ( for UI data-binding )
* Underscore.js  ( js utilities )
* JQuery
* moment.js for date handling


  



