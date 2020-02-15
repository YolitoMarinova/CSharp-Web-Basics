namespace Andreys.App.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;

    using Andreys.Services.Products;
    using Andreys.ViewModels.Products;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.View();
            }

            AllProductsViewModel allProductsViewModel = new AllProductsViewModel();
            allProductsViewModel.Products = this.productsService.GettAllProducts();

            return this.View(allProductsViewModel, "Home");
        }

        [HttpGet("/Home/Index")]
        public HttpResponse Index2()
        {
            return this.Index();
        }
    }
}
