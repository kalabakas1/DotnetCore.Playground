# DotnetCore.Playground
This project should be used to learn the following concepts to some level:

* Domain Driven Design
* DotNet Core 3.0
* DotNet Core ASP.Net
* Blazor
* Documentation of functionality
* FluentValidation
* MediatR

This is at least the idea and vision. It will not be correct, and it will always be changing 
according to the things i learn through books and articles. 

The following should be the resources I am going to use:

* Domain Driven Design (the blue book)
* [Enterprise Craftmanship](https://enterprisecraftsmanship.com/)
* [Microsoft Docs](https://docs.microsoft.com/)
* Checklist Manifesto

## Tasks to be done
The below list should be the tasks that I need to perform before I consider my self done with this 
small experiment. The domain is pretty simple. It centers a small healthcheck application that can 
be executed resulting in a report defining if the different tests failed or not.

### Domain Driven Design
* Define a small model 
    * Include Aggregates in the model
* Interact with a data store (file system etc) to read from resulting in the creation of the domain models
* Use CQRS to execute domain logic based on DTO's

### DotNet Core 3.0
* Use the new features of the framework to make the code more easy to read and maintain
    * e.g. Operators

### DotNet Core ASP.Net
* Expose the domain logic through the use of API
* Implement layer logic about auth etc.

### Blazor
* Create a very simple blazor client that expose the logic and gets messages regarding the start and end
of the execution of the tests

### Documentation
* Try to find some framework that make sense when it have to deal with the different layers of the exercise
    * Swagger for API
    * XML for the Domain
    * Blazor?
    
## Notes
* Commands to use:
    * prism mock -d http://localhost:5000/swagger/v1/swagger.json
* Missing stuff:
    * Extend the API to return and handle mode entities - remember to use query models not domain models

## Status
So I will try to keep a small series of updates so I know what have happened over the development of 
the project. It should not be long, but long enough so I know the process:

### Status 2019-12-13
So I'm currently home having a cold, so I guess it's okay to write this small update.
The state is currently that I have defined a small domain where you can have a user, on a user there 
should be a list of configurations that each have a list of health-checks attached to it. At
the same time the configuration should associates with a subscription-type that defines 
how many health-checks you are allowed to have.
   
On top of that it can persist to a SqLite database for both the configuration, checks and subscription types.

All this is accessible through a API (currently only containing one POST method - more to come). The
API it self has setup to use FluentValidation that enables me to define validation in a different
class than through attributes directly in the model itself. Very handy btw.  
All of this is readable in a Swashbuckle swagger endpoint, where the FluentValidation rules is 
integrated into as well.

The communication between the API and the domain is handled through the use of commands in the form 
of the MediatR library. I'm currently not sure if it actually is a kind of CQRS, but I guess its
a form of that pattern - more to come.

By using the openapi-generator nodejs tool I were able to create two very simple scripts for 
generating a Client project in the solution Playground.Api.Client. That basically just hooks up
to the swagger.json endpoint and generates working C# dotnet code based directly on the swagger feed.

That way I don't have to maintain the client code too much.

The Idea is that in the future a Blazor Server application will communicate with the API based
on the generated API client code and don't directly depend on the other projects. It needs to 
be hidden behind an interface in the Blazor implementation so it can be switched out later if 
need be.  