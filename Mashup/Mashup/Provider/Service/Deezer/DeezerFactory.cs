namespace Mashup.Provider.Service.Deezer
{
    internal class DeezerFactory : AbstractFactory<DeezerProvider>
    {
        public DeezerFactory()
        {
        }

        // Renvoie une instance de la catégorie voulue
        public DeezerProvider getProvider(Category category)
        {
            switch (category)
            {
                case Category.Album:
                    return new DeezerAlbumProvider();

                case Category.Artist:
                    return new DeezerArtistProvider();

                default:
                    return null;
            }
        }
    }
}