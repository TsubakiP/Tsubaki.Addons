// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Hosting.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Diagnostics;
    using System.Reflection;

    internal sealed class LazyContainer<T, TMetadata> : IReadOnlyList<Lazy<T, TMetadata>>, IDisposable
        where T : class
        where TMetadata : class
    {
        private readonly CompositionContainer _container;

        [ImportMany]
        private Lazy<T, TMetadata>[] _lazies = null;

        public int Count => ((IReadOnlyList<Lazy<T, TMetadata>>)this._lazies).Count;

        public Lazy<T, TMetadata> this[int index] => ((IReadOnlyList<Lazy<T, TMetadata>>)this._lazies)[index];

        public LazyContainer(ComposablePartCatalog catalog)
        {
            this._container = new CompositionContainer(catalog);
            try
            {
                this._container.ComposeParts(this);
            }
            catch (TypeLoadException e)
            {
                Debug.WriteLine("> Loaded Error: " + e.ToString());
            }
            catch (ReflectionTypeLoadException e)
            {
                Debug.WriteLine("> Loaded Error: " + e.ToString());
                foreach (var ex in e.LoaderExceptions)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            catch (CompositionException e)
            {
                Debug.WriteLine("> Composite Error: " + e.ToString());
            }
        }

        void IDisposable.Dispose() => this._container.Dispose();

        public IEnumerator<Lazy<T, TMetadata>> GetEnumerator() => ((IReadOnlyList<Lazy<T, TMetadata>>)this._lazies).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<Lazy<T, TMetadata>>)this._lazies).GetEnumerator();
    }
}