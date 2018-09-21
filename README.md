# LifeLine

## A Dot Net Core based Web Api project.


The project has been designed to roll-out a Free Non-Commercial web api based app. Currently, the web api is being prepared and soon a client apps will also be rolled out for both mobile and web.

The web api is based on:

* DotNet Core v: 2.1.1
* Entity Framework Core Design & Sqlite v: 2.1.3
* Sqlite 3

This app is configured for Pakistani Cell/Mobile phone number, so anyone using it in another country will be required to make necessary changes in DonorEntities.cs. Number that is being accepted right now is in the this format: 923********* without (+) sign. The number is being validated using Regex.

This api has 3 controllers:
* DonorController
* RequestorsController
* RequestsController


The Controllers accepts the following methods:
* DonorController
    1. HTTPGET:     Get(): to get all registered donors on (/api/Donor).
    2. HTTPGET:     Get(int id): to get a donor by ID on (/api/Donor/{id}).
    3. HTTPPOST:    Post(Donor value): to insert a donor record in Database on (/api/Donor) using form.
    4. HTTPDELETE:  Delete(int id): to remove a donor record from Database on (/api/Donor/{id}).
    5. HTTPPUT:     Not configured yet.
    
* RequestorsController:
    1. HTTPGET:     GetRequestor(): to get all registered requestors on (/api/Requestors).
    2. HTTPGET:     GetRequestor(int id): to get a Requestors by ID on (/api/Requestors/{id}).
    3. HTTPPOST:    PostRequestor(Requestors value, Requests requests): to insert a Requestors and a request combined record in Database on (/api/Requestors) using form.
    4. HTTPDELETE:  DeleteRequestor(int id): to remove a Requestor and mapped requests record from Database on (/api/Requestors/{id}). Note: if requestor is removed, all the corresponding requests of this requestor will also be removed.
    5. HTTPPUT:     Not configured yet.

* RequestsController:
    1. HTTPGET:     GetRequests(): to get all registered requests on (/api/Requests).
    2. HTTPGET:     GetRequestsByNumber(string number): to get a Request by cell nunmber on (/api/Requests/{number}).
    3. HTTPDELETE:  DeleteRequests(int id): to remove a request record from Database on (/api/Requests/{id}).
    5. HTTPPUT:     Not configured yet.