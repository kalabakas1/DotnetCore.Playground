# DotnetCore.Playground
This project should be used to learn the following concepts to some level:

* Domain Driven Design
* DotNet Core 3.0
* DotNet Core ASP.Net
* Blazor
* Documentation of functionality

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