// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons
{
    using System.ComponentModel;
    using System.ComponentModel.Composition;

    /// <summary>
    /// 提供對模組的基本功能
    /// </summary>
    [InheritedExport]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public interface IAddon
    {
        /// <summary>
        /// 關鍵字
        /// </summary>
        string[] Keywords { get; }

        /// <summary>
        /// 名稱
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 執行
        /// </summary>
        /// <param name="args"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        bool Execute(string[] args, out object callback);
    }
}