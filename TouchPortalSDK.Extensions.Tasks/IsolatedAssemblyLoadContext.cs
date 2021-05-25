#if NET5_0
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace TouchPortalSDK.Extensions.Tasks
{
    public class IsolatedAssemblyLoadContext :
        AssemblyLoadContext, IDisposable
    {
        private string _basePath;

        public IsolatedAssemblyLoadContext(string basePath)
            : base(true)
        {
            _basePath = basePath;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var assemblyFile = Path.Combine(_basePath, assemblyName.Name + ".dll");
            if (File.Exists(assemblyFile))
                return base.LoadFromAssemblyPath(assemblyFile);

            return base.LoadFromAssemblyName(assemblyName);
        }

        public Assembly LoadFrom(string assemblyFile)
        {
            var path = Path.Combine(_basePath, assemblyFile);
            return base.LoadFromAssemblyPath(path);
        }
        
        public void Dispose()
        {
            base.Unload();
        }
    }
}
#endif