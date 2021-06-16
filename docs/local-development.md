# Mistakes Journal - local development
Local development can have actually 2 different variants of deployment.
1. Client is in watch mode, server is run locally and database runs in docker.
2. Client, server and database run in docker.

By default, the solution 2 is enabled using `docker-compose.yml` file. To run such a scenario
just type in `docker-compose -f docker-compose.yml build` and `docker-compose -f docker-compose.yml up` while in root folder. When everything will build
and database and server will run, the application can be accessed via http://localhost:8080.

Now, if you want to manually start every component in an application, you have to run different configurations described
above.

## Client
Go to catalog `/client` and first (if you haven't done it) run command `npm install`. This command installs all required
dependencies listed in a file `package.json`. It may also modify file `package-lock.json`, so don't worry it such thing happens.
The file `package-lock.json` is required for application to build the dependencies tree and by doing fresh install, 
some packages might get updated.

Now, when you have the dependencies installed, just run `npm run serve`. It will start development server for `client` application.
The application will be hosted by node (and some framework magic including Webpack) on url http://localhost:8080.

At this point you have launched hot-reloading (rebuilding when files change) client application. To be fully able to 
communicate with server, you have to launch server separately.

## Server

Go to catalog `/server` and run command `dotnet restore`. It will install dependencies of the solution. Now go to 
`Mistakes.Journal.Api` and with command `DOTNET_ENVIRONMENT=Development dotnet run` you can start application. The 
API application will run on port `5001` on url http://localhost:5001 (because configured automatically, you can also provide env variable `PORT` to
override the value, but client app uses `5001` as proxy for unknown).

## Database

Database will be run only in Docker. To run just a database you can write command `docker-compose -f docker-compose.yml
up db`. It will start database as docker container. You can access it from database browsing software (eg. DBeaver) using
connection string provided in `/server/Mistakes.Journal.Api/appsettings.Development.json`.
