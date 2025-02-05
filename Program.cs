using System;
using Linqjointables.DAL;

namespace Linqjointables
{
    class Program
    {
        static void Main(string[] args)
        {
            Mergemethods merge = new Mergemethods();

            merge.MergeAndDisplayRecords(); 

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
