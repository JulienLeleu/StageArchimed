//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup
{
    using Cache;
    using IO;
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
            DatabaseManager web = new DatabaseManager();
            SendDBObject search = new SendDBObject("Music", "Stromae", "Racine Carré", null, "FR");
            Console.WriteLine(web.Search(search));

            Routine r = new Routine();
            r.process();
            Console.ReadKey();
        }
    }
}