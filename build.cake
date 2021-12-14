var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var buildVerbosity = Verbosity.Minimal;

var solutionFile = "./QuinaNadal.sln";
var outputDir = Directory("./output");

var assemblyInfo = ParseAssemblyInfo("./QuinaNadal/Properties/SolutionInfo.cs");    
var assemblyVersion = new Version(assemblyInfo.AssemblyVersion);


Task("Restore-NuGet-Packages")    
    .Does(() =>
{
    NuGetRestore(solutionFile);
});

Task("Build")        
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
      MSBuild(solutionFile, settings =>  settings
        .SetConfiguration(configuration)
        .SetVerbosity(buildVerbosity));                
});

Task("CopyToOutput")
    .IsDependentOn("Clean")
    .Does(() => 
    {
        CopyToOutput("QuinaNadal");
        CopyToOutput("QuinaNadalServer");
    });
  
void CopyToOutput(string projectName) {
    var releasePath = Directory("./" + projectName + "/bin")  + Directory(configuration);
    var outputPath = outputDir + Directory(projectName);
    
    Information("Copy files from " + releasePath + " to " + outputPath);
    CopyDirectory(releasePath, outputPath);

    DeleteFiles(outputPath.Path + "/*.pdb");
    DeleteFiles(outputPath.Path + "/*.xml");
    DeleteFiles(outputPath.Path + "/*.vshost.exe");
    DeleteFiles(outputPath.Path + "/*.vshost.exe.config");
    DeleteFiles(outputPath.Path + "/*.vshost.exe.manifest");

    Zip(outputPath, outputDir + File(projectName + ".zip"));
}  

Task("Clean")
    .Does(() =>
{
    EnsureDirectoryExists(outputDir);
    CleanDirectory(outputDir);
});
  
Task("Default")
    .IsDependentOn("Build")
    .IsDependentOn("CopyToOutput");

RunTarget(target);
