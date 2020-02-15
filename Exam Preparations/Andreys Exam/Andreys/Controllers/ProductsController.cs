namespace Andreys.Controllers
{
    using System;

    using SIS.HTTP;
    using SIS.MvcFramework;

    using Andreys.Models;
    using Andreys.Services.Products;
    using Andreys.ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(ProductInputModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.View();
            }

            if (model.Description.Length > 10)
            {
                return this.View();
            }

            if (model.Price < 0)
            {
                return this.View();
            }

            if (model.Category == null || !Enum.IsDefined(typeof(Category), model.Category))
            {
                return this.View();
            }

            if (model.Gender == null || !Enum.IsDefined(typeof(Gender), model.Gender))
            {
                return this.View();
            }

            this.productsService.AddProduct(model.Name, model.Description, model.ImageUrl, model.Category, model.Gender, model.Price);

            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.productsService.IsProductExist(id))
            {
                return this.Redirect("/");
            }

            var productModel = this.productsService.GetDetailsForModel(id);

            return this.View(productModel);
        }

        public HttpResponse Delete(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.productsService.IsProductExist(id))
            {
                return this.Redirect("/");
            }

            this.productsService.DeleteProduct(id);

            return this.Redirect("/");
        }
    }
}
