using System.Text;

namespace CreateProjFiles.Facade;

public class CreateFeatureFacade
{
    private readonly string _featureName;
    private readonly string _controllerPath;
    private readonly string _featurePath;
    private readonly string _responsePath;
    private readonly string _mappingPath;

    public CreateFeatureFacade(string featureName)
    {
        _featureName = featureName;
        _controllerPath = "Backend/Presentation/Api/Controllers";
        _featurePath = "Backend/Core/Application/Features";
        _responsePath = "Backend/Core/Application/Shared/Responses";
        _mappingPath = "Backend/Core/Application/Mappings";
    }
    public void CreateFeatureFiles()
    {
        CreateController();

        var commandQueries = new Dictionary<string, string>
        {
            {"Create", "Command"},
            {"Update", "Command"},
            {"Delete", "Command"},
            {"GetById", "Query"},
            {"GetAll", "Query"}
        };
            
        foreach (var commandQuery in commandQueries)
        {
            GenerateCQRSFiles(commandQuery.Key, commandQuery.Value);
        }
    }
    private void CreateController()
    {
        string controllerTemplate = File.ReadAllText("CreateProjFiles/Controller.txt", Encoding.UTF8);
        controllerTemplate = controllerTemplate.Replace("#Template", _featureName);
        string pathToNewController = $"{_controllerPath}/{_featureName}Controller.cs";
        if (!File.Exists(pathToNewController))
            File.WriteAllText(pathToNewController, controllerTemplate);
    }
    private void GenerateCQRSFiles(string type, string queryType = "Command")
    {
        GenerateResponseFiles(type);
        string commandFolderPath = $"{_featurePath}/{_featureName}/{queryType}/{type}";
        Directory.CreateDirectory(commandFolderPath);

        if (!File.Exists($"{commandFolderPath}/{type}{_featureName}{queryType}.cs")) 
            File.WriteAllText($"{commandFolderPath}/{type}{_featureName}{queryType}.cs", File.ReadAllText($"CreateProjFiles/Templates/{queryType}/{type}/Template{queryType}.txt", Encoding.UTF8).Replace("#Template", $"{type}{_featureName}"));
        
        if (!File.Exists($"{commandFolderPath}/{type}{_featureName}{queryType}Handler.cs"))
            File.WriteAllText($"{commandFolderPath}/{type}{_featureName}{queryType}Handler.cs", File.ReadAllText($"CreateProjFiles/Templates/{queryType}/{type}/Template{queryType}Handler.txt", Encoding.UTF8).Replace("#Template", $"{type}{_featureName}"));

        if (!File.Exists($"{commandFolderPath}/{type}{_featureName}{queryType}Validator.cs")) 
            File.WriteAllText($"{commandFolderPath}/{type}{_featureName}{queryType}Validator.cs", File.ReadAllText($"CreateProjFiles/Templates/{queryType}/{type}/Template{queryType}Validator.txt", Encoding.UTF8).Replace("#Template", $"{type}{_featureName}"));

    }
    private void GenerateResponseFiles(string type)
    {
        string responseFolderPath = $"{_responsePath}/{_featureName}";
        var dir = Directory.CreateDirectory(responseFolderPath);
        
        if(File.Exists($"{dir.FullName}/{type}{_featureName}Response.cs")) 
            return;

        File.WriteAllText($"{dir.FullName}/{type}{_featureName}Response.cs", 
        File.ReadAllText("CreateProjFiles/Templates/Response/TemplateResponse.txt", 
        Encoding.UTF8).Replace("#Template", $"{type}{_featureName}"));
    }
    private void CreateMappingFiles()
    {
        string mappingFolderPath = $"{_mappingPath}/{_featureName}";
        Directory.CreateDirectory(mappingFolderPath);
        File.WriteAllText($"{mappingFolderPath}/{_featureName}MappingProfile.cs", 
        File.ReadAllText("CreateProjFiles/Templates/Mapping/TemplateMapping.txt", 
        Encoding.UTF8).Replace("#Template", _featureName));
    }
}