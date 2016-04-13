//-----------------------------------------------------------------------
// <copyright file="DeezerProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.Deezer
{
    using Mashup.Provider.Util;

    /// <summary>
    /// Class who implements <see cref="AbstractProvider"/>
    /// </summary>
    internal abstract class DeezerProvider : AbstractProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeezerProvider"/> class.
        /// </summary>
        /// <param name="url">The main $$(Deezer)$$ url</param>
        public DeezerProvider(string url) : base(url, null)
        {
        }
    }
}