namespace Mashup.Provider
{
    internal interface AbstractFactory<T>
    {
        // Retourne une instance d'un fournisseur en fonction de la catégorie
        T getProvider(Category category);
    }
}