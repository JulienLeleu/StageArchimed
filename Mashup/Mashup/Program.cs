//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup
{
    using Cache;
    using IO;
    using Provider.Service.Deezer;
    using System;

    /// <summary>
    /// The launching program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Method main
        /// </summary>
        private static void Main()
        {
            /*DatabaseManager web = new DatabaseManager();
            SendDBObject search = new SendDBObject("Music", null, null, "825646200115", "FR");
            Console.WriteLine(web.Search(search));*/
            Routine r = new Routine();
            r.process();
            Console.ReadKey();
        }
    }
}