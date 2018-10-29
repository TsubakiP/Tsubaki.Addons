namespace Tsubaki.Addons
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class AddonAttribute : ExportAttribute, IAddonMetadata
    {
        public AddonAttribute(string AddonName, params string[] keywords) : base(typeof(IAddon))
        {
            this.Name = AddonName;
            this.Keywords = keywords;
        }

        public string Name { get; }
        public string[] Keywords { get; }
    }
    
}
