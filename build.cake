#addin "Cake.Npm"
#addin "Cake.Powershell"

// Target - The task you want to start. Runs the Default task if not specified.
var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

Information($"Running target {target} in configuration {configuration}");

var distDirectory = Directory("./dist/StockManagementWebApi");
var distWebDirectory = Directory("./dist/StockManagementWeb");

var angularFolder = Environment.GetEnvironmentVariable("AngularFolder") ?? "./src/Web";
// Deletes the contents of the Artifacts folder if it contains anything from a previous build.
Task("Clean-Backend")
    .Does(() =>
    {
        CleanDirectory(distDirectory);
    });

Task("Clean-Frontend")
    .Does(() =>
    {
        CleanDirectory(distWebDirectory);
    });

// Run dotnet restore to restore all package references.
Task("Restore-Nuget")
    .IsDependentOn("Clean-Backend")
    .Does(() =>
    {
        DotNetCoreRestore("./src/StockManagement.sln");
    });

// Build using the build configuration specified as an argument.
 Task("Build-Backend")
    .Does(() =>
    {
        DotNetCoreBuild("./src/StockManagement.sln",
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration,
                ArgumentCustomization = args => args.Append("--no-restore"),
            });
    });

Task("Build-Frontend")
    .IsDependentOn("Clean-Frontend")
    .Does(() =>
{
    //Install NPM packages
    var npmInstallSettings = new NpmInstallSettings {
      WorkingDirectory = angularFolder,
      LogLevel = NpmLogLevel.Warn,
      ArgumentCustomization = args => args.Append("--no-save")
    };
    NpmInstall(npmInstallSettings);

    //Build Angular frontend project using Angular cli
    var runSettings = new NpmRunScriptSettings {
      ScriptName = "ng",
      WorkingDirectory = angularFolder,
      LogLevel = NpmLogLevel.Warn
    };
    runSettings.Arguments.Add("build");
    runSettings.Arguments.Add("--prod");
    runSettings.Arguments.Add("--build-optimizer");
    runSettings.Arguments.Add("--progress false");

    NpmRunScript(runSettings);
});
	
Task("UnitTests")
  .IsDependentOn("Build-Backend")
    .Does(() =>
    {
        var projects = GetFiles("./**/*.Unit.Tests.csproj");
        foreach(var project in projects)
        {
            DotNetCoreTest(
                project.FullPath,
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true
                });
        }
    });
	
Task("IntegrationTests")
  .IsDependentOn("Build-Backend")
    .Does(() =>
    {
        var projects = GetFiles("./**/*.Integration.Tests.csproj");
        foreach(var project in projects)
        {
            DotNetCoreTest(
                project.FullPath,
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true
                });
        }
    });

Task("Run-Frontend")
    .IsDependentOn("Build-Frontend")
    .Does(() => {
        StartPowershellScript("http-server ./dist/StockManagementWeb");
    });

// Publish the app to the /dist folder
Task("Publish-Backend")
    .Does(() =>
    {
        DotNetCorePublish(
            "./src/StockManagement.Api/StockManagement.Api.csproj",
            new DotNetCorePublishSettings()
            {
                Configuration = configuration,
                OutputDirectory = distDirectory,
                ArgumentCustomization = args => args.Append("--no-restore"),
            });
    });
	
// Publish the app to the /dist folder
Task("Run-Backend")
    .IsDependentOn("BuildAndTest-Backend")
    .IsDependentOn("Publish-Backend")
    .Does(() =>
    {
        var settings = new DotNetCoreExecuteSettings
        {
            WorkingDirectory = distDirectory
        };

        DotNetCoreExecute(distDirectory.ToString() + "/StockManagement.Api.dll", "", settings);
    });

// A meta-task that runs all the steps to Build and Test the app
Task("BuildAndTest-Backend")
    .IsDependentOn("Clean-Backend")
    .IsDependentOn("Restore-Nuget")
    .IsDependentOn("Build-Backend")
    .IsDependentOn("UnitTests")
    .IsDependentOn("IntegrationTests");

// The default task to run if none is explicitly specified. In this case, we want
// to run everything starting from Clean, all the way up to Publish.
Task("Default")
    .IsDependentOn("Build-Frontend")
    .IsDependentOn("BuildAndTest-Backend")
    .IsDependentOn("Publish-Backend");

// Executes the task specified in the target argument.
RunTarget(target);