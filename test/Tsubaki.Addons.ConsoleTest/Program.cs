// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Addons.ConsoleTest
{
    using System;

    using Tsubaki.Addons.Driver;

    internal class MainClass
    {
        private static void Main(string[] args)
        {
            var result = AddonProvider.Provider.Execute(new[] { "moq" }, new string[1] { "A" }, out var callback);

            if (result == ExecutedResult.Success)
            {
                Console.WriteLine(callback);
            }
            Console.WriteLine("----");

            Console.Write("input T/F: ");
            var c = Console.ReadKey().Key;
            AddonProvider.Provider["Mock"].Enabled = c == ConsoleKey.T;
            Console.ReadKey();
            return;
        }
    }
}