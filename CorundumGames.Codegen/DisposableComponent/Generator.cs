using CorundumGames.Codegen.Shared;
using DesperateDevs.CodeGeneration;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.DisposableComponent
{
    [PublicAPI]
    public sealed class Generator : ICodeGenerator
    {
        private const string FeatureName = "DisposeDataFeature";
        public string name => "Disposable Component Generator";
        public int priority => 0;
        public bool runInDryMode => true;


        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            var types = data
                .OfType<Data>()
                .ToArray();

            var names = types
                .SelectMany(GenerateSystemNames)
                .ToArray();

            var featureFile = new CodeGenFile[]
            {
                new(
                    Path.Combine("Features", $"{FeatureName}.cs"),
                    new FeatureGeneratorTemplate(FeatureName, names).TransformText(),
                    GetType().FullName
                ),
            };

            if (types.Any())
            {
                return types
                    .SelectMany(GenerateSystems)
                    .Concat(featureFile)
                    .ToArray();
            }
            else
            {
                return featureFile;
            }


        }

        private IEnumerable<CodeGenFile> GenerateSystems(Data data)
        {
            return from contextName in data.Contexts
                let template = new SystemTemplate(data.Name, contextName)
                let fileName = Path.Combine(contextName, "Systems", $"{template.SystemName}.cs")
                select new CodeGenFile(
                    fileName,
                    template.TransformText(),
                    GetType().FullName
                );
        }



        private IEnumerable<string> GenerateSystemNames(Data data)
        {
            var componentName = data.Name.ToComponentName(true);

            return data.Contexts.Select(context => $"DisposeOf{context}{componentName.RemoveComponentSuffix()}System");
        }
    }
}
