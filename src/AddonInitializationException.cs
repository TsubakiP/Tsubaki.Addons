

namespace Tsubaki.Addons
{
    using System;

    internal sealed class AddonInitializationException : Exception
    {
        internal AddonInitializationException(Type type, string reason):base(reason)
        {
            this.Type = type;
            this.Reason = reason;
        }

        public Type Type { get; }
        public string Reason { get; }
    }
    
}
