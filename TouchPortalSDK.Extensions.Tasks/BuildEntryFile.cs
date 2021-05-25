using System;
using System.IO;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using TouchPortalSDK.Extensions.Attributes.Reflection;

namespace TouchPortalSDK.Extensions.Tasks
{
    public class BuildEntryFile
#if NET472
                                : Microsoft.Build.Utilities.AppDomainIsolatedTask
#else
                                : Microsoft.Build.Utilities.Task
#endif
    {
        /* Set in targets file:
         * $(PublishDir): bin\Debug\net472\publish\
         * $(MSBuildProjectDirectory): C:\Users\oddbj\source\repos\BuildTargetTest-DeleteMe\BuildTargetTest-DeleteMe
         * $(MSBuildProjectName): BuildTargetTest-DeleteMe
         * $(OutDir): bin\Debug\net472\
         * $(AssemblyName): BuildTargetTest-DeleteMe
         * $(OutputType): Exe or Library
         */
        [Required]
        public string MSBuildProjectDirectory { get; set; }

        [Required]
        public string OutDir { get; set; }

        [Required]
        public string AssemblyName { get; set; }

        private string GetBasePath()
        {
            return Path.Combine(MSBuildProjectDirectory, OutDir);
        }

        private string GetMainAssemblyName(string assemblyName)
        {
            var dllFile = $"{assemblyName}.dll";;
            var exeFile = $"{assemblyName}.exe";

            //.Net 5x (dotnet core can be .dll or .exe):
            if (File.Exists(Path.Combine(GetBasePath(), dllFile)))
                return dllFile;

            //.Net 4x (Managed), .Net 5 has an unmanaged .exe file, check for .dll:
            if (File.Exists(Path.Combine(GetBasePath(), exeFile)))
                return exeFile;

            return null;
        }

        public override bool Execute()
        {
            // Create the new application domain, we need this so MSBuild is not locking the reflected file.
            // This is because of MSBuild node reuse.

            try
            {
                Log.LogMessage(MessageImportance.High, "Task started!");

                var basePath = GetBasePath();
                var fileName = GetMainAssemblyName(AssemblyName);

                if (fileName is null)
                {
                    Log.LogError($"Expected file did not exist: '{Path.Combine(basePath, AssemblyName)}'");
                    return false;
                }

#if NET472
                var mainAssembly = Path.Combine(basePath, fileName);
                var assembly = System.Reflection.Assembly.LoadFrom(mainAssembly);
#else
                using var appContext = new IsolatedAssemblyLoadContext(basePath);
                var assembly = appContext.LoadFrom(fileName);
#endif
                var pluginAnalyzer = new PluginAnalyzer(assembly);
                var generatorIdentifiers = new GeneratorIdentifiers(pluginAnalyzer);
                var entryFileBuilder = new EntryFileBuilder(pluginAnalyzer, generatorIdentifiers);

                var entryFileObject = entryFileBuilder.BuildEntryFile();
                var entryFileContents = JsonConvert.SerializeObject(entryFileObject);

                var entryFilePath = Path.Combine(basePath, "entry.tp");

                File.WriteAllText(entryFilePath, entryFileContents);

                return true;
            }
            catch (Exception e)
            {
                Log.LogError(e.Message);
                return false;
            }
        }
    }
}
