#if NETSTANDARD
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace TouchPortalSDK.Extensions.Tasks
{
    public class IsolatedAssemblyLoadContext :
        AssemblyLoadContext
    {
        private string _basePath;

        public IsolatedAssemblyLoadContext(string basePath)
        {
            _basePath = basePath;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var assemblyFile = Path.Combine(_basePath, assemblyName.Name + ".dll");
            if (File.Exists(assemblyFile))
                return base.LoadFromAssemblyPath(assemblyFile);

            return null;
        }

        public Assembly LoadFrom(string assemblyFile)
        {
            var path = Path.Combine(_basePath, assemblyFile);
            return base.LoadFromAssemblyPath(path);
        }
        
        public void Unload()
        {
            //TODO: Not supported on netstandard, however, it is supported in .Net 5
            //base.Unload();
        }
    }
}
#endif