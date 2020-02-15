namespace Andreys.Services.Products
{
    using System.Collections.Generic;

    using Andreys.ViewModels.Products;

    public interface IProductsService
    {
        void AddProduct(string name, string description, string imageUrl, string category, string gender, decimal price);

        IEnumerable<ProductViewModel> GettAllProducts();

        DetailsProductViewModel GetDetailsForModel(int id);

        void DeleteProduct(int id);

        bool IsProductExist(int id);
    }
}
