using System.Threading.Tasks;

namespace Mashup.Provider.Service.LastFM
{
    internal class LastFMAlbumProvider : LastFMProvider
    {
        public LastFMAlbumProvider() : base("http://ws.audioscrobbler.com/2.0")
        {
        }

        public override string requestBuilder(string id, Identifier identifier)
        {
            switch (identifier)
            {
                case Identifier.Mbid:
                    return string.Format("{0}?method=album.getinfo&api_key={1}&mbid={2}&format={3}", this.url, this.apiKey, id, "json");

                default:
                    return null; // TODO Exception à gérer
            }
        }

        // Exemple d'id MBID = fc981ea3-e846-48f8-946d-51e9ef4b7198
        public override async Task<string> getRawData(string id, Identifier identifier)
        {
            return await sendRequest(id, identifier);
        }
    }
}