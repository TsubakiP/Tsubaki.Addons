// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
namespace Tsubaki.Addons
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class AddonAttribute : ExportAttribute, IAddonMetadata
    {
        public string[] Keywords { get; }

        public string Name { get; }

        public AddonAttribute(string AddonName, params string[] keywords) : base(typeof(IAddon))
        {
            this.Name = AddonName;
            this.Keywords = keywords;
        }
    }
}