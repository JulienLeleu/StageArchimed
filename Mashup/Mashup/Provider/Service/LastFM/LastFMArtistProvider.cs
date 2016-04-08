using System.Threading.Tasks;

namespace Mashup.Provider.Service.LastFM
{
    internal class LastFMArtistProvider : LastFMProvider
    {
        public LastFMArtistProvider() : base("http://ws.audioscrobbler.com/2.0")
        {
        }

        public override string requestBuilder(string id, Identifier identifier)
        {
            switch (identifier)
            {
                case Identifier.Mbid:
                    return string.Format("{0}?method=artist.getinfo&api_key={1}&mbid={2}&format={3}", this.url, apiKey, id, "json");

                default:
                    return null;    // TODO Exception à gérer
            }
        }

        // Exemple d'id MBID = 9c9f1380-2516-4fc9-a3e6-f9f61941d090
        public override async Task<string> getRawData(string id, Identifier identifier)
        {
            return await sendRequest(id, identifier);
        }
    }
}