using CreateProjFiles.Facade;

Console.WriteLine("Enter the feature name: ");
string featureName = Console.ReadLine() ?? string.Empty;

CreateFeatureFacade createFeature = new CreateFeatureFacade(featureName);
createFeature.CreateFeatureFiles();