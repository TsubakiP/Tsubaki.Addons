// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.Hosting.Internal
{
    using System.Collections.Generic;

    internal static class Diff
    {
        public static double Compare(IEnumerable<string> src, IEnumerable<string> cmp)
        {
            if (src is null || cmp is null)
                return 0.0;

            var s = GetSet(src);
            var c = GetSet(cmp);

            var t = s.Count + c.Count;

            s.IntersectWith(c);
            return (double)s.Count / t;
        }

        private static HashSet<string> GetSet(IEnumerable<string> strings)
        {
            var hs = new HashSet<string>();
            foreach (var str in strings)
            {
                hs.Add(str.ToLower());
            }
            return hs;
        }
    }
}