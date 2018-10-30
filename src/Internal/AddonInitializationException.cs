// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Internal
{
    using System;

    internal sealed class AddonInitializationException : Exception
    {
        public string Reason { get; }

        public Type Type { get; }

        internal AddonInitializationException(Type type, string reason) : base(reason)
        {
            this.Type = type;
            this.Reason = reason;
        }
    }
}