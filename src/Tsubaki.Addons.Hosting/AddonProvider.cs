// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics;
    using System.Linq;
    using Tsubaki.Addons.Hosting.Internal;
    using Tsubaki.Addons.Interfaces;

    /// <summary>
    /// The add-ons provider.
    /// </summary>
    public sealed class AddonProvider
    {
        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
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

        private readonly static object s_locker = new object();
        private static volatile AddonProvider s_instance;

        private const string PATH = "./Addons";

        private readonly List<Lazy<IAddon, IAddonMetadata>> _container;

        private AddonProvider()
        {
            var aggregate = new AggregateCatalog();
            AddonUtils.AddDirectories(aggregate, PATH);
            AddonUtils.AddAssemblies(aggregate);

            this._container = new LazyContainer<IAddon, IAddonMetadata>(aggregate).ToList();

#if DEBUG
            Debug.WriteLine("---");
            foreach (var item in this._container)
            {
                Debug.WriteLine(item.Metadata.Id);
            }
#endif
        }

        /// <summary>
        /// Executes the specified domains.
        /// </summary>
        /// <param name="domains">The domains.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        public ExecutedResult Execute(string[] domains, string[] args, out object callback)
        {
            var r = default(ExecutedResult);
            callback = null;
            switch (this._container.Count)
            {
                case 0:
                    r = ExecutedResult.NoAddon;
                    break;

                case 1:
                    {
                        var m = this._container[0];
                        var diff = Diff.Compare(m.Metadata.Domains, domains);
                        if (diff != 0.0)
                        {
                            var result = m.Value.Execute(args, out callback);
                            r = result.HasValue ? (result.Value ? ExecutedResult.Success : ExecutedResult.Failure) : ExecutedResult.Disabled;
                        }
                        else
                            r = ExecutedResult.NoMatched;
                        break;
                    }

                default:
                    {
                        var top_v = 0.0;
                        var a = default(Lazy<IAddon, IAddonMetadata>);
                        foreach (var m in this._container)
                        {
                            var diff = Diff.Compare(m.Metadata.Domains, domains);
                            // Debug.WriteLine("N: " + m.Metadata.Name + " | " + diff);
                            if (diff >= top_v)
                            {
                                top_v = diff;
                                a = m;
                            }
                        }
                        if (top_v == 0.0)
                            r = ExecutedResult.NoMatched;
                        else
                        {
                            //Found the highest similar object
                            var result = a.Value.Execute(args, out callback);
                            r = result.HasValue ? (result.Value ? ExecutedResult.Success : ExecutedResult.Failure) : ExecutedResult.Disabled;
                        }
                        break;
                    }
            }

            return r;
        }

        /// <summary>
        /// Gets the specified <see cref="IAddon"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <param name="advanceSearch">if set to <c>true</c> [advance search].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">message - name</exception>
        /// <exception cref="Hosting.Internal.AddonNotFoundException"></exception>
        public IAddon Get(string name, bool ignoreCase = false, bool advanceSearch = false)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("message", nameof(name));

            var ic = ignoreCase
                ? StringComparison.CurrentCultureIgnoreCase
                : StringComparison.CurrentCulture;

            bool Advance(Lazy<IAddon, IAddonMetadata> lz)
                => string.Equals(lz.Metadata.Id ?? lz.Value.GetType().Name, name, ic);
            bool Normal(Lazy<IAddon, IAddonMetadata> lz)
                => string.Equals(lz.Metadata.Id, name, ic);

            var comparer = advanceSearch ? (Func<Lazy<IAddon, IAddonMetadata>, bool>)Advance : Normal;

            foreach (var lazy in this._container)
            {
                if (comparer(lazy))
                    return lazy.Value;
            }

            throw new AddonNotFoundException(name);
        }

        /// <summary>
        /// Gets the <see cref="IAddon"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="IAddon"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <param name="advanceSearch">if set to <c>true</c> [advance search].</param>
        /// <returns></returns>
        public IAddon this[string name, bool ignoreCase = false, bool advanceSearch = false]
            => this.Get(name,ignoreCase,advanceSearch);
    }
}