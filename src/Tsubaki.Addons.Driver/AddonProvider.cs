// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Driver
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics;

    using System.IO;
    using System.Reflection;

    using Tsubaki.Addons.Interfaces;

    [Flags]
    public enum ExecutedResult
    {
        NoAddon,
        Disabled,
        NoMatched,
        Failure,
        Success
    }

    public sealed class AddonProvider
    {
        private readonly static object s_locker = new object();

        private static volatile AddonProvider s_instance;

        [ImportMany]
        private Lazy<IAddon, IAddonMetadata>[] _addons = null;

        private CompositionContainer _container;

        public static AddonProvider Provider
        {
            get
            {
                lock (s_locker)
                {
                    if (s_instance == null)
                    {
                        lock (s_locker)
                        {
                            s_instance = new AddonProvider();
                        }
                    }
                    return s_instance;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public IAddon this[string name, bool ignoreCase = false]
            => this.Get(name, ignoreCase);

        // = new Lazy<IAddon, IAddonMetadata>[0];
        private AddonProvider()
        {
            var catalog = new AggregateCatalog();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Debug.WriteLine("Prepare assembly catalog for composition: " + assembly.FullName);
                catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            }

            var dir = new DirectoryInfo("./Addons/");
            if (!dir.Exists)
            {
                Debug.WriteLine("Create directory");
                dir.Create();
            }
            foreach (var sub in dir.GetDirectories())
            {
                var f = new DirectoryCatalog(sub.FullName, "*.dll");
                Debug.WriteLine("Prepare directory catalog for composition: " + sub.FullName);
                catalog.Catalogs.Add(f);
            }

            this._container = new CompositionContainer(catalog);

            try
            {
                Debug.WriteLine("> Start composition");
                this._container.ComposeParts(this);

                if (this._addons.Length == 0)
                    Debug.WriteLine("> No add-on loaded");
                else
                    foreach (var m in this._addons)
                    {
                        Debug.WriteLine("> Loaded add-on: " + m.Metadata.Name);
                    }
                return;
            }
            catch (TypeLoadException e)
            {
                //diff ver
                Debug.WriteLine("Loaded Error: " + e.ToString());
            }
            catch (ReflectionTypeLoadException e)
            {
                Debug.WriteLine("Loaded Error: " + e.ToString());
                foreach (var ex in e.LoaderExceptions)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            catch (CompositionException e)
            {
                Debug.WriteLine("Composite Error: " + e.ToString());
            }
        }

        public ExecutedResult Execute(string[] keywords, string[] args, out object callback)
        {
            callback = null;
            switch (this._addons.Length)
            {
                case 0:
                    return ExecutedResult.NoAddon;

                case 1:
                    {
                        var m = this._addons[0];
                        var diff = Diff.Compare(m.Metadata.Keywords, keywords);
                        if (diff != 0.0)
                        {
                            var result = m.Value.Execute(args, out callback);
                            return result.HasValue ? (result.Value ? ExecutedResult.Success : ExecutedResult.Failure) : ExecutedResult.Disabled;
                        }
                        return ExecutedResult.NoMatched;
                    }
                default:
                    {
                        var top_v = 0.0;
                        var a = default(Lazy<IAddon, IAddonMetadata>);
                        foreach (var m in this._addons)
                        {
                            var diff = Diff.Compare(m.Metadata.Keywords, keywords);
                           // Debug.WriteLine("N: " + m.Metadata.Name + " | " + diff);
                            if (diff >= top_v)
                            {
                                top_v = diff;
                                a = m;
                            }
                        }
                        if (top_v == 0.0)
                            return ExecutedResult.NoMatched;

                        //Found the highest similar object
                        var result = a.Value.Execute(args, out callback);
                        return result.HasValue ? (result.Value ? ExecutedResult.Success : ExecutedResult.Failure) : ExecutedResult.Disabled;

                    }
            }
            
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ModuleNotFoundException"/>
        public IAddon Get(string name, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("message", nameof(name));

            var ic = ignoreCase
                ? StringComparison.CurrentCultureIgnoreCase
                : StringComparison.CurrentCulture;

            foreach (var lazy in this._addons)
            {
                if (string.Equals(lazy.Metadata.Name, name, ic))
                    return lazy.Value;
            }

            throw new AddonNotFoundException(name);
        }
    }
}