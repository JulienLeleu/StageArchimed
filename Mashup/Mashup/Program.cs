//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup
{
    using Cache;
    using IO;
    using Provider;
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
            SearchObject search = new SearchObject("Dangerous","David Guetta", null, "FR");
            Console.WriteLine(web.Search(search));
            Console.ReadKey();
        }
    }
}