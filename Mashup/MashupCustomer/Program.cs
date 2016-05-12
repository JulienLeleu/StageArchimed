//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Archimed">
//     Copyright LookFor.HK. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MashupCustomer
{
    using System;
    using System.Threading.Tasks;
    using Mashup.Provider;
    using Mashup.IO;

    /// <summary>
    /// The Program containing main
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method
        /// </summary>
        /// <param name="args">The arguments</param>
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ProviderManager manager = ProviderManager.GetInstance();

            try
            {
                Task<ResultSetObject> t = manager.GetObjectsDatasFromProviders(new SendObject("Music", "MBID", "9c9f1380-2516-4fc9-a3e6-f9f61941d090", "FR"));
                t.Wait();
                ResultSetObject b = t.Result;
                /*Console.WriteLine(b.GetJson());*/
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ReadKey();
        }
    }
}
