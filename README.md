## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Technical Info](#technical-info)

## General info
This project is a simple Task Manager.
	
## Technologies
Project is created with:
* Image of SQL server - Docker
* .net core - Backend (API)
* Angular - Frontend
	
## Setup
To run this project, firstly you need to run the docker image:
https://hub.docker.com/repository/docker/nivsv/task_manager
use the following command to run the image and create a basic database that works with the project:
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Admin123!" -p 1433:1433 -d nivsv/task_manager:latest
```
SQL Credentials:
Port: 1433
Username (default): sa 
Password: Admin123!

## Technical Info
Backend Dependencies:
* EntityFrameworkCore 6
* NetwonsoftJson

The backend also exposes Swagger (In development mode)
The swagger is auto-generated by the project and also included in the repository.
