using System.Threading.Tasks;

namespace Mashup.Provider.Service.Deezer
{
    internal class DeezerArtistProvider : DeezerProvider
    {
        public DeezerArtistProvider() : base("https://api.deezer.com/artist")
        {
        }

        public override string requestBuilder(string id, Identifier identifier)
        {
            switch (identifier)
            {
                case Identifier.Id:
                    return string.Format("{0}/{1}", this.url, id);

                default:
                    return null;    // TODO Exception à gérer
            }
        }

        // Exemple d'id : ID = 705
        public override async Task<string> getRawData(string id, Identifier identifier)
        {
            return await sendRequest(id, identifier);
        }
    }
}