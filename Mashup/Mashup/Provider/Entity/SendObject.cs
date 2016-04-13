namespace Mashup.Entity
{
    public class SendObject
    {
        private string identifierKey;
        private string identifierValue;
        private string favouriteLanguage;
        private string format;

        public SendObject(string identifierKey, string identifierValue)
        {
            this.IdentifierKey = identifierKey;
            this.IdentifierValue = identifierValue;
            this.FavouriteLanguage = "FR";
            this.Format = "JSON";
        }

        public SendObject(string identifierKey, string identifierValue, string format) : this(identifierKey, identifierValue)
        {
            this.Format = format;
        }

        public SendObject(string identifierKey, string identifierValue, string format, string favouriteLanguage) : this(identifierKey, identifierValue, format)
        {
            this.FavouriteLanguage = favouriteLanguage;
        }

        public string IdentifierKey
        {
            get
            {
                return identifierKey;
            }

            set
            {
                identifierKey = value;
            }
        }

        public string IdentifierValue
        {
            get
            {
                return identifierValue;
            }

            set
            {
                identifierValue = value;
            }
        }

        public string Format
        {
            get
            {
                return format;
            }

            set
            {
                format = value;
            }
        }

        public string FavouriteLanguage
        {
            get
            {
                return favouriteLanguage;
            }

            set
            {
                favouriteLanguage = value;
            }
        }
    }
}
