# Schedule Generator
This repository conatins ScheduleGenerator - Blazor web application

## Description
This project is a **.NET 5** implemented Blazor WebAssembly application for creating schedules.

**ScheduleGenerator** enables communication with the **MSSQL database** consisting of sending and receiving data regarding users, schedules and schedule items. Application can be used by users who may create account and login to the application.
## Screenshots
<details>
  <summary>Click to expand!</summary>
  
#### Created schedule
![ListOfTodos](https://i.imgur.com/ojwcUlv.png[/img] "Created schedule")

#### Schedule item details
![Todo details](https://i.imgur.com/YM46pJP.png[/img] "Schedule item details")

#### Adding new item
![Login/Register](https://i.imgur.com/l1oRFKt.png[/img] "Adding new item")

#### Generated PDF
![Login/Register](https://i.imgur.com/BfNf2wH.png[/img] "Generated PDF")

</details>

## Stack
It uses **Entity Framework Core** to communicate with a database, which contains required data tables like:
* Users - where informations about users are stored 
* Schedules - where informations about schedules are stored 
* ScheduleItems -  where informations about schedule items are stored

Other tools used in project:
* **JwtBearer** - for authentication
* **Open API** - for API documentation
* **AutoMapper** - for mapping DTO-s and EntityModels data
* **Fluent Validation** - for data validation

## API Controllers and their operations:

### Users - Controller for users management
<details>
  <summary>Click to expand!</summary>
  
* **[POST] Create new user**  
 ```/api/users```
* **[POST] Authenticate the user**  
 ```/api/users/authenticate```
* **[GET] Get user by id**  
 ```/api/users/{userId}```
</details>

### Schedules - Controller for schedules management
<details>
  <summary>Click to expand!</summary>
  
* **[POST] Create a new schedule**  
 ```/api/schedules```
* **[GET] Get a list of schedules**  
 ```/api/schedules```
* **[GET] Get schedule by id**  
 ```/api/schedules/{scheduleId}```
* **[DELETE] Delete schedule with given id**  
 ```/api/schedules/{scheduleId}```
* **[PUT] Update (full update) schedule**  
 ```/api/schedules/{scheduleId}```
</details>

### ScheduleItems - Controller for schedule items management
<details>
  <summary>Click to expand!</summary>
  
* **[POST] Create a new schedule item**  
 ```/api/schedules/{scheduleId}/ScheduleItems```
* **[GET] Get a list of schedule items from specified schedule**  
 ```/api/schedules/{scheduleId}/ScheduleItems```
* **[GET] Get schedule item from specified schedule**  
 ```/api/schedules/{scheduleId}/ScheduleItems/{scheduleItemId}```
* **[DELETE] Delete the schedule item with given id**  
 ```/api/schedules/{scheduleId}/ScheduleItems/{scheduleItemId}```
* **[PUT] Update (full update) schedule item**  
 ```/api/schedules/{scheduleId}/ScheduleItems/{scheduleItemId}```
* **[PATCH] Update (Partially update) schedule item**  
 ```/api/schedules/{scheduleId}/ScheduleItems/{scheduleItemId}```
</details>

For more documentation data, visit 
```/swagger/ScheduleGeneratorOpenAPISepcification/swagger.json```  
(or ```/index.html``` for documentation UI)

## Installation
Make sure you have the **.NET 5.0 SDK** installed on your machine. Then do:  
>`git clone https://github.com/TomWia9/ScheduleGenerator.git`  
`cd ScheduleGenerator\ScheduleGenerator\Server`  
`dotnet run`

## Configuration
This will need to be perfored before running the application for the first time
1. You have to change ConnectionString in **appsettings.json** for ConnectionString that allow you to connect with database in your computer.
2. Issue the Entity Framework command to update the database  
`dotnet ef database update`
 
## License
[MIT](https://choosealicense.com/licenses/mit/)
