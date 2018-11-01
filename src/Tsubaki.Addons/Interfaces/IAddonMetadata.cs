// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Interfaces
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides the metadata for the developer to identify add-on.
    /// </summary>
    [Guid("CAFEFF06-0CFC-4605-A057-96365F0DADB3")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IAddonMetadata
    {
        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <value>The domains.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string[] Domains { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string Id { get; }
    }
}