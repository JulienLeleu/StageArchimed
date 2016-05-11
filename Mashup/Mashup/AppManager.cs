using Mashup.Cache;
using Mashup.Provider;

namespace Mashup
{
    public class AppManager
    {
        private DatabaseManager databaseManager;
        private ProviderManager providerManager;

        public AppManager()
        {
            // TODO :   - Créer une classe AppManager (pattern Factory)
            //          - Créer une classe Routine qui itére en boucle toutes les x temps, fais les recherches sur la colonne de la table mashup_media  
            // Déclarer un AppManager (Méthode getProviders, getProviderByName)
            // récupérer toutes les instances depuis AppManager 
            // insérer dans la base toutes les instances si elles n'existent pas (SQL only)
            databaseManager = new DatabaseManager();
            providerManager = ProviderManager.GetInstance();
        }


    }
}
