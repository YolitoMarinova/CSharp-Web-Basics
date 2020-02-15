namespace Andreys.Services.Products
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Andreys.Data;
    using Andreys.Models;
    using Andreys.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void AddProduct(string name, string description, string imageUrl, string category, string gender, decimal price)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Category = (Category)Enum.Parse(typeof(Category), category),
                Gender = (Gender)Enum.Parse(typeof(Gender), gender),
                Price = price
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = this.db.Products.FirstOrDefault(p => p.Id == id);

            this.db.Products.Remove(product);
            db.SaveChanges();
        }

        public DetailsProductViewModel GetDetailsForModel(int id)
        {
            return this.db.Products.Where(p => p.Id == id).Select(p => new DetailsProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Category = p.Category.ToString(),
                Gender = p.Gender.ToString(),
                Price = p.Price
            })
                .FirstOrDefault();
        }

        public IEnumerable<ProductViewModel> GettAllProducts()
        {
            return this.db.Products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price
            })
                .ToList();
        }

        public bool IsProductExist(int id)
            => this.db.Products.Any(p => p.Id == id);
    }
}
