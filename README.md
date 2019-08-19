# Customer-Manager
This is a customer management application which provides an ability to manage customer details in ASP.NET MVC with C#.Net, Entity Framework and SQL Server.

Core Functionalities
- Ability to find all the customers with the following filters
  - Surname
  - City
  - Country
- Supports pagination
- Ability to create, update and delete customer
- Allows Email address to be unique

Tools used
- Visual studio 2017
- Web API 2
- MVC 5 with razor views
- Knockout 4 JS
- Unity - Dependency injection
- JQuery v3.3
- Bootstrap
- MS Unit test & Moq Framework
- Entity Framework v6.2
- SQL Server 2017
                    
                    
Technical Capabilities
- Utility functionalties (DataHelper.cs)
 - To retrieve the country details and it is resusable
- Generic API Service Repository (ServiceRepository.cs)
  - To make API call
- Generic error page (NotFound.cshtml)
   -ErrorController – Handle the Error
- Unit Testing (CustomerControllerTest.cs)
  -To unit test the customer controller logic.
- Model with essential data annotation (CustomerModel.cs)
   -To perform the model validation
- Security
   -Validate the anti-forgery token to prevent cross site request forgery (CSRF)



### API End-points 

1. Route: **"/api/customer/"** [HttpPost]- To retrieve all customer details
2. Route: **"/api/customer/{id}"** [HttpGet]- To get specific customer details
3. Route: **"/api/customer/"** [HttpPost]- To create a customer.
4. Route: **"/api/customer/{id}"** [HttpPut]- To update a specific customer details.
5. Route: **"/api/customer/{id}"** [HttpDelete]- To delete a specific customer details.

     
 
