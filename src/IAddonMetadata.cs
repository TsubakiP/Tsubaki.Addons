// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons
{
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IAddonMetadata
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        string[] Keywords { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        string Name { get; }
    }
}