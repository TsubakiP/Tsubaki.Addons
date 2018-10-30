// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Driver
{
    [Addon("Mock", "moq", "fake", "mock")]
    public class Mock : Addon
    {
        protected override bool ExecuteImpl(string[] args, ref object callback)
        {
            callback = "success - mock";

            return true;
        }
    }
}