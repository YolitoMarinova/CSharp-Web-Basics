using IRunes.Services.Users;
using IRunes.ViewModels.Home;
using SIS.HTTP;
using SIS.MvcFramework;

namespace IRunes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.View();
            }

            var viewModel = new HomeLoginViewModel
            {
                Username = this.usersService.GetUsername(this.User)
            };

            return this.View(viewModel, "Home");
        }

        [HttpGet("/Home/Index")]
        public HttpResponse Index2()
        {
            return this.Index();
        }
    }
}
