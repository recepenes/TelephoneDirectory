
This application is built using ASP.NET Core and can be easily deployed using Docker and Docker Compose. It also includes a database and message queue for storing and processing data.

## Features

-   Create, read, update, and delete entries in the telephone directory
-   Generate reports based on the directory data
-   Asynchronous processing of report requests using a message queue
-   Deployable as a set of Docker containers

## Prerequisites

-   Docker and Docker Compose

## Running the Application

To start the application, execute the following command in the root directory of the project:

`docker-compose -f docker-compose-prod.yml up --build -d` 

You can then access the following URLs:

-   Contract API Swagger UI: [http://localhost:7276/swagger](http://localhost:7276/swagger)
-   Report API Swagger UI: [http://localhost:7192/swagger](http://localhost:7192/swagger)
-   RabbitMQ GUI: [http://localhost:15672](http://localhost:15672/)

## Credentials

-   RabbitMQ GUI: username: `guest`, password: `guest`
-   Adminer (database GUI): server: `postgres`, username: `pdUser`, password: `pdPassword`, database name: `phonedirectoryDB`

## Development

To run the application in development mode, execute the following command in the root directory of the project:


`docker-compose -f docker-compose-dev.yml up --build` 

This will start the microservices and their dependencies with hot reloading enabled.
