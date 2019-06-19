using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JwSale.Util.Assemblies
{
    public class AssemblyFinder
    {
        private static IList<Assembly> allAssembly = null;
        public static IList<Assembly> AllAssembly { get { return allAssembly; } }

        static AssemblyFinder()
        {
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.exe", SearchOption.TopDirectoryOnly));

            //allAssembly = new List<Assembly>();

            //foreach (var file in files)
            //{
            //    if (allAssembly.Where(o => o.Location == file).Count() == 0)
            //    {
            //        var assemblyName = AssemblyName.GetAssemblyName(file);
            //        allAssembly.Add(Assembly.Load(assemblyName));
            //    }
            //}


            allAssembly = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var file in files)
            {
                if (allAssembly.Where(o => o.Location == file).Count() == 0)
                {
                    var assemblyName = AssemblyName.GetAssemblyName(file);
                    Assembly.Load(assemblyName);
                }
            }
            allAssembly = AppDomain.CurrentDomain.GetAssemblies();


        }

        public static IEnumerable<Assembly> GetAssemblies(string fiterName)
        {

            return allAssembly.Where(o => o.FullName.Contains(fiterName));
        }


        public static Assembly GetAssembly(string assemblyName)
        {
            return allAssembly.Where(o => o.FullName == assemblyName).FirstOrDefault();
        }
    }
}
