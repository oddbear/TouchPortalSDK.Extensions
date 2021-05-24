using System;
using System.IO;
using System.Reflection;
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

        [Required]
        public string OutputType { get; set; }

        public override bool Execute()
        {
            // Create the new application domain, we need this so MSBuild is not locking the reflected file.
            // This is because of MSBuild node reuse.

            try
            {
                Log.LogMessage(MessageImportance.High, "Task started!");
                
                var extension = OutputType == "Exe" ? ".exe" : ".dll";
                var folder = Path.Combine(MSBuildProjectDirectory, OutDir);
                var fileName = AssemblyName + extension;
                var fileToInspect = Path.Combine(folder, fileName);

                if (!File.Exists(fileToInspect))
                {
                    Log.LogError($"Expected file did not exist: '{fileToInspect}'");
                    return false;
                }

#if NET472
                var assembly = Assembly.LoadFrom(fileToInspect);
#else
                var appContext = new IsolatedAssemblyLoadContext(folder);
                var assembly = appContext.LoadFrom(fileName);
#endif
                var pluginAnalyzer = new PluginAnalyzer(assembly);
                var generatorIdentifiers = new GeneratorIdentifiers(pluginAnalyzer);
                var entryFileBuilder = new EntryFileBuilder(pluginAnalyzer, generatorIdentifiers);

                var entryFileObject = entryFileBuilder.BuildEntryFile();
                var entryFileContents = JsonConvert.SerializeObject(entryFileObject);

                var entryFilePath = Path.Combine(folder, "entry.tp");

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
