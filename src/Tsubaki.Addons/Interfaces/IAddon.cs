// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Interfaces
{
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides basic functions for the add-on.
    /// </summary>
    [InheritedExport]
    [Guid("7515B7B4-76C5-40EB-9EA8-65E1AC6E7FCD")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public interface IAddon
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IAddon"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        bool Enabled { get; set; }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        bool? Execute(string[] args, out object callback);
    }
}