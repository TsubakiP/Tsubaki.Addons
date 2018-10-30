// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Interfaces
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;

#pragma warning disable 1591

    [Guid("CAFEFF06-0CFC-4605-A057-96365F0DADB3")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IAddonMetadata
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        string Name { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        string[] Keywords { get; }
    }

#pragma warning restore 1591
}