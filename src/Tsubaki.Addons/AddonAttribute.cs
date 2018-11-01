// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
namespace Tsubaki.Addons
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Runtime.InteropServices;

    using Tsubaki.Addons.Interfaces;

    /// <summary>
    /// Basic information about tagging add-ons
    /// </summary>
    /// <seealso cref="ExportAttribute"/>
    /// <seealso cref="IAddonMetadata"/>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    [Guid("8A984DF7-08AB-4ACF-A308-758697A1A0BE")]
    public sealed class AddonAttribute : ExportAttribute, IAddonMetadata
    {
        /// <summary>
        /// The name of the add-ons
        /// </summary>
        public string Name = null;

        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <value>The domains.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string[] Domains { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Id => this.Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddonAttribute"/> class.
        /// </summary>
        /// <param name="domains">The domains.</param>
        public AddonAttribute(params string[] domains) : base(typeof(IAddon))
        {
            this.Domains = domains;
        }
    }
}