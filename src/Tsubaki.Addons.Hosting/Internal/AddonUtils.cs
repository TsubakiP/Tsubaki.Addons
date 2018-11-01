// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Hosting.Internal
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;

    internal static class AddonUtils
    {
        public static void AddAssemblies(AggregateCatalog catalog)
        {
#if DEBUG
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            if (asms.Length == 0)
                return;

            foreach (var assembly in asms)
            {
                //Debug.WriteLine("Prepare assembly catalog for composition: " + assembly.FullName);
                catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            }
#endif
        }

        public static void AddDirectories(AggregateCatalog catalog, string directoryPath)
        {
            var dir = new DirectoryInfo(directoryPath);
            if (!dir.Exists)
            {
                // Debug.WriteLine("Create directory");
                dir.Create();
                return;
            }
            var p = dir.GetDirectories();
            if (p.Length == 0)
                return;
            foreach (var sub in p)
            {
                var f = new DirectoryCatalog(sub.FullName, "*.dll");
                // Debug.WriteLine("Prepare directory catalog for composition: " + sub.FullName);
                catalog.Catalogs.Add(f);
            }
        }

        internal static void Swap<T>(ref T t, ref T t2)
        {
            var tmp = t;
            t = t2;
            t2 = tmp;
        }
    }
}