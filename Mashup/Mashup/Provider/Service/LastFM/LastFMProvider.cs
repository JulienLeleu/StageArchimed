﻿//-----------------------------------------------------------------------
// <copyright file="LastFMProvider.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.Provider.Service.LastFM
{
    using Entity;
    using Mashup.Provider.Util;

    /// <summary>
    /// Class who implements <see cref="AbstractProvider"/>
    /// </summary>
    internal abstract class LastFMProvider : AbstractProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LastFMProvider"/> class.
        /// </summary>
        /// <param name="url">The main $$(Deezer)$$ url</param>
        public LastFMProvider(Method methodType) : base(Media.Music, methodType, "http://ws.audioscrobbler.com/2.0", "b25b959554ed76058ac220b7b2e0a026")
        {
        }
    }
}