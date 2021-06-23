using Mono.Options;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text.Json;

namespace TouchPortalSDK.Extensions.Tool
{
    class Program
	{
        static void Main(string[] args)
        {
            var entry = false;
            var package = false; //TODO: find a configuration that works. And why not toggle with a output file path?
            var help = false;
            var outdir = ".";
            var assemblyName = string.Empty;

            //"h|help", "show this message and exit"
            var options = new OptionSet
            {
                { "o|outdir=", "The output directory of the build, ex. $(OutDir) if using variable.", o => outdir = o },
                { "a|assemblyName=", "Assembly name of the plugin, ex. $(AssemblyName) if using variable.", a => assemblyName = a },
                { "e|entry", "Creates the entry.tp file.", (e) => entry = e != null },
                { "p|package", "Creates the .tpp package file.", (p) => package = p != null },
                { "h|help", "Show this message and exit", (h) => help = h != null }
                //TODO: LogLevel, error, warn, verbose...
            };

            options.Parse(args);

            if (string.IsNullOrWhiteSpace(outdir) || string.IsNullOrWhiteSpace(assemblyName) || help)
            {
                ShowHelp(options);
                return;
            }

            if (entry)
            {
                CreateEntryFile(outdir, assemblyName);
            }

            if (package)
            {
                //TODO: Publish path... On publish trigger?
                //PackageTppFile(assemblyName, "", "");
            }
        }

        private static void CreateEntryFile(string outdir, string assemblyName)
        {
            var assemblyPath = GetAssemblyPath(outdir, assemblyName);
            var assemblyFile = Assembly.LoadFrom(assemblyPath);
            var versionString = assemblyFile
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            Console.WriteLine("Test: -------- ");
            Console.WriteLine($"Generating from assembly: '{assemblyFile.Location}', version: '{versionString}'");

            var context = Reflection.PluginTreeBuilder.Build(assemblyFile);
            var entryFileObject = Reflection.EntryFileBuilder.BuildEntryFile(context);
            
            var entryFileContents = JsonSerializer.Serialize(entryFileObject, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            var entryFilePath = Path.Combine(outdir, "entry.tp");

            File.WriteAllText(entryFilePath, entryFileContents);
        }

        private static void PackageTppFile(string pluginId, string publishPath, string outputPath)
        {
            //Find entry.tp before archiving...
            var files = Directory.GetFiles(publishPath, "*");

            if (File.Exists(outputPath))
                File.Delete(outputPath);
            
            using (var archive = ZipFile.Open(outputPath, ZipArchiveMode.Create))
            {
                //The "secret sauce" to get this working in Java:
                var directory = archive.CreateEntry($"{pluginId}/");

                foreach (var file in files)
                {
                    var pluginEntryName = Path.Combine(pluginId, Path.GetFileName(file));
                    archive.CreateEntryFromFile(file, pluginEntryName);
                }
            }
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
