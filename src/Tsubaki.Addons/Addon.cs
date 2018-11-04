// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons
{
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.InteropServices;

    using Tsubaki.Addons.Interfaces;
    using Tsubaki.Addons.Internal;
    using Tsubaki.Configuration;

    /// <summary>
    /// Provides basic features for the add-on.
    /// </summary>
    /// <seealso cref="IAddon"/>
    [Guid("BD47D5EE-83D5-4017-B5F3-1E8549402470")]
    public abstract class Addon : IAddon
    {
        /// <summary>
        /// Gets the domains.
        /// </summary>
        public string[] Domains { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IAddon"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get
            {
                return this._cfg.Enabled;
            }
            set
            {
                this._cfg.Enabled = value;
                this._cfg.Save() ;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Addon"/> class.
        /// </summary>
        /// <exception cref="AddonInitializationException"/>
        protected Addon()
        {
            var t = this.GetType();
            if (t.GetCustomAttribute<AddonAttribute>() is IAddonMetadata metadata)
            {
                this.Name = metadata.Id ?? t.Name;
                if (metadata.Domains is string[] keys && keys.Length > 0)
                {
                    this.Domains = metadata.Domains;
                }
                else
                {
                    throw new AddonInitializationException(t, "The domains is empty so this Addon will not be invoked");
                }
            }
            else
            {
                throw new AddonInitializationException(t, $"The { nameof(AddonAttribute)} not depend on type {t.Name}, Add the attribute can improve this exception");
            }

            this._cfg = Config.Load();

            this.OnInitialize();
        }

        private Config _cfg;


        [EditorBrowsable(EditorBrowsableState.Never)]
        bool? IAddon.Execute(string[] args, out object callback)
        {
            callback = null;
            if (!this.Enabled)
                return null;

            return this.ExecuteImpl(args, ref callback);
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="callback">The callback.</param>
        /// <returns></returns>
        protected abstract bool ExecuteImpl(string[] args, ref object callback);

        /// <summary>
        /// Called when object initialize.
        /// </summary>
        protected virtual void OnInitialize()
        {
        }



        private class Config : SelfDisciplined<Config>
        {
            public bool Enabled { get; set; }
        }
    }

    
}