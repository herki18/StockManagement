StockManagement
###### Initialize tools:
Invoke-WebRequest https://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1

###### Front End
###### Task Commands:
npm install http-server -g is needed for running front end

- **./build.ps1 -Target Run-Frontend** Builds front end application, copies it to dist folder and will run front end application

###### Back End
###### Task Commands:
- **./build.ps1 -Target Run-Backend** - Will compile the files, runs unit and integration tests, copies files to dist folder and runs web api.

- **./build.ps1 -Target BuildAndTest-Backend** - Will compile the files, runs unit and integration tests.

###### Both
- **./build.ps1 -Target Default** - Will compile the files, runs unit and integration tests, copies files to dist folder.
