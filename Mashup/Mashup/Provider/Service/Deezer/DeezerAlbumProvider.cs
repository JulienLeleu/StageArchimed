using System.Threading.Tasks;

namespace Mashup.Provider.Service.Deezer
{
    internal class DeezerAlbumProvider : DeezerProvider
    {
        public DeezerAlbumProvider() : base("https://api.deezer.com/album")
        {
        }

        public override string requestBuilder(string id, Identifier identifier)
        {
            switch (identifier)
            {
                case Identifier.Upc:
                    return string.Format("{0}/upc:{1}", this.url, id);

                default:
                    return null;    // TODO Exception à gérer
            }
        }

        // Exemple d'id : UPC = 825646864836
        public override Task<string> getRawData(string id, Identifier identifier)
        {
            return sendRequest(id, identifier);
        }
    }
}