using System.Diagnostics.CodeAnalysis;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.Plugins;
using DesperateDevs.Roslyn;
using DesperateDevs.Roslyn.CodeGeneration.Plugins;
using DesperateDevs.Serialization;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;

namespace CorundumGames.CodeGeneration.Plugins.DisposableComponent
{
    [PublicAPI]
    public sealed class DataProvider : IDataProvider, IConfigurable, ICachable
    {
        private static readonly string DisposableName = typeof(IDisposable).FullName!;
        private static readonly string ComponentName = typeof(IComponent).FullName!;

        public string name => "Disposable Component Data Provider";
        public int priority => 0;
        public bool runInDryMode => true;

        public Dictionary<string, string> defaultProperties => _projectPathConfig.defaultProperties;
        public Dictionary<string, object> objectCache
        {
            get;
            set;
        }
        private readonly ProjectPathConfig _projectPathConfig = new();

        public void Configure(Preferences preferences)
        {
            _projectPathConfig.Configure(preferences);
        }

        [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
        public CodeGeneratorData[] GetData()
        {
            return PluginUtil
                .GetCachedProjectParser(objectCache, _projectPathConfig.projectPath)
                .GetTypes()
                .Where(type => type.AllInterfaces.Any(i => i.ToCompilableString() == ComponentName))
                .Where(type => type.AllInterfaces.Any(i => i.SpecialType == SpecialType.System_IDisposable))
                .Select(type => new Data
                {
                    Name = type.Name,
                    Contexts = GetContexts(type),
                })
                .ToArray();
        }

        private string[] GetContexts(INamedTypeSymbol type)
        {
            return type
                .GetAttributes<ContextAttribute>(true)
                .Select(a => a.AttributeClass.Name.Replace("Attribute", ""))
                .ToArray();
        }
    }
}
