using Mono.Options;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using TouchPortalSDK.Extensions.Attributes.Reflection;

namespace TouchPortalSDK.Extensions.Tool
{
    class Program
	{
        static void Main(string[] args)
        {
            var showHelp = false;
            var outdir = ".";
            var assemblyName = string.Empty;

            //"h|help", "show this message and exit"
            var options = new OptionSet
            {
                { "o|outdir=", "the name of someone to greet.", o => outdir = o },
                { "a|assemblyName=", "the number of times to repeat the greeting.", a => assemblyName = a },
                { "h|help", "show this message and exit", (bool h) => showHelp = h },
            };

            options.Parse(args);

            if (string.IsNullOrWhiteSpace(outdir) || string.IsNullOrWhiteSpace(assemblyName) || showHelp)
            {
                ShowHelp(options);
                return;
            }

            var assemblyPath = GetAssemblyPath(outdir, assemblyName);
            var assemblyFile = Assembly.LoadFrom(assemblyPath);
            var versionString = assemblyFile
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            Console.WriteLine("Test: -------- ");
            Console.WriteLine($"Generating from assembly: '{assemblyFile.Location}', version: '{versionString}'");

            var pluginAnalyzer = new PluginAnalyzer(assemblyFile);
            var generatorIdentifiers = new GeneratorIdentifiers(pluginAnalyzer);
            var entryFileBuilder = new EntryFileBuilder(pluginAnalyzer, generatorIdentifiers);

            var entryFileObject = entryFileBuilder.BuildEntryFile();
            var entryFileContents = JsonSerializer.Serialize(entryFileObject, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            var entryFilePath = Path.Combine(outdir, "entry.tp");

            File.WriteAllText(entryFilePath, entryFileContents);
        }
        
        private static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: dotnet tool run TouchPortalSDK [OPTIONS]+");
            //Missing required parameter xxx.
            Console.WriteLine("Generates the entry.tp file from reflection of your Plugin Assembly.");
            //Console.WriteLine("If no xxx is specified, a xxx is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }

        private static string GetAssemblyPath(string outDir, string assemblyName)
        {
            string PathOrNull(string fileName)
            {
                var filePath = Path.Combine(outDir, fileName);
                return File.Exists(filePath)
                    ? filePath
                    : null;
            }

            var extension = Path.GetExtension(assemblyName).ToLower();

            return extension is ".dll" or ".exe"
                ? PathOrNull(assemblyName)
                : PathOrNull($"{assemblyName}.dll") ?? PathOrNull($"{assemblyName}.exe");
        }
    }
}
