// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Hosting.Internal
{
    using System;

    internal sealed class AddonNotFoundException : Exception
    {
        public string ModuleName { get; }

        internal AddonNotFoundException(string addonName) : base(string.Format("Add-on not found: {0}", addonName))
        {
            this.ModuleName = addonName ?? throw new ArgumentNullException(nameof(addonName));
        }
    }
}