using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Mashup.Provider
{
    // Classe abstraite d'un fournisseur
    internal abstract class AbstractProvider
    {
        // Adresse URL principale de l'API
        protected string url;

        // Clé d'accés pour l'utilisation de l'API (peut être optionnel)
        protected string apiKey;

        // Constructeur à partir de l'URL principale
        public AbstractProvider(string url)
        {
            this.url = url;
            this.apiKey = null;
        }

        // URL principale avec le type d'identifiant et la clé d'API
        public AbstractProvider(string url, string apiKey) : this(url)
        {
            this.apiKey = apiKey;
        }

        // Construit une requête à partir de son url, son api key selon le critère de recherche
        public abstract string requestBuilder(string id, Identifier identifier);

        // Envoie une requete au serveur et retourne la réponse
        protected async Task<string> sendRequest(string id, Identifier identifier)
        {
            var client = new WebClient();
            return await client.DownloadStringTaskAsync(requestBuilder(id, identifier));
        }

        // Retourne les données brutes
        public abstract Task<string> getRawData(string id, Identifier identifier);

        // Retourne les données sous forme d'objet C#
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public async Task<T> getObjectData<T>(string id, Identifier identifier)
        {
            string result = await getRawData(id, identifier);
            return DeserializeJSon<T>(result);
        }

        // Méthode pour déserializer du JSON en objet
        public static T DeserializeJSon<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T newObject = (T)ser.ReadObject(stream);
            stream.Dispose();
            return newObject;
        }
    }
}