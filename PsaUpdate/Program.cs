using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PsaUpdate
{
    class Program
    {
        static void Main(string[] args)
        {

            string result = null;
            Deployer d = new Deployer();

            Console.WriteLine("****************************************************");
            Console.WriteLine("* This script will update your PSA handling mechanism");
            Console.WriteLine("****************************************************");
            Console.WriteLine("Auxiliary information collected from your current PC / Spaceman configuration");
            Console.WriteLine("Operating system: {0}",Deployer.IsX64ArchToString());
            Console.WriteLine("Spaceman shared components root dir: {0} | Is valid: {1}", d.OldFolderName, d.IsValidSharedComponentsDir().ToString());
            Console.WriteLine("****************************************************");
            Console.WriteLine("* Press y to continue or n to break");
            Console.WriteLine("****************************************************");
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.Y:
                    {
                        
                        result = d.Run();    
                    }
                    Console.WriteLine("* Update status: {0}", result.ToUpper());
                    Console.WriteLine("****************************************************");
                    break;
                case ConsoleKey.N:
                    Console.WriteLine("* Update processing cancelled");
                    Console.WriteLine("****************************************************");
                    break;
                default:
                    Console.WriteLine("* Invalid key pressed | Update processing cancelled");
                    Console.WriteLine("****************************************************");
                    break;
            }
            Console.WriteLine("* Hit any key to exit");
            Console.WriteLine("****************************************************");
            Console.ReadKey();

        }

    }
}
