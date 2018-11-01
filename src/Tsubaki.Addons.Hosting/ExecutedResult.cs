// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Hosting
{
    using System;

    /// <summary>
    /// </summary>
    [Flags]
    public enum ExecutedResult
    {
        /// <summary>
        /// The no addon
        /// </summary>
        NoAddon,

        /// <summary>
        /// The disabled
        /// </summary>
        Disabled,

        /// <summary>
        /// The no matched
        /// </summary>
        NoMatched,

        /// <summary>
        /// The failure
        /// </summary>
        Failure,

        /// <summary>
        /// The success
        /// </summary>
        Success
    }
}