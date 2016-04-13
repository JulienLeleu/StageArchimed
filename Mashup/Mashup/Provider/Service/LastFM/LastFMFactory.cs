namespace Mashup.Provider.Service.LastFM
{
    internal class LastFMFactory : AbstractFactory<LastFMProvider>
    {
        public LastFMFactory()
        {
        }

        public LastFMProvider getProvider(Category category)
        {
            switch (category)
            {
                case Category.Album:
                    return new LastFMAlbumProvider();

                case Category.Artist:
                    return new LastFMArtistProvider();

                default:
                    return null;
            }
        }
    }
}