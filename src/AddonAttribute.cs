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

#pragma warning disable 1591

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string[] Keywords { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get; }

#pragma warning restore 1591

        /// <summary>
        /// Initializes a new instance of the <see cref="AddonAttribute"/> class.
        /// </summary>
        /// <param name="AddonName">Name of the addon.</param>
        /// <param name="keywords">The keywords.</param>
        public AddonAttribute(string AddonName, params string[] keywords) : base(typeof(IAddon))
        {
            this.Name = AddonName;
            this.Keywords = keywords;
        }
    }
}