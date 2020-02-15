namespace Andreys.Controllers
{
    using SIS.HTTP;
    using SIS.MvcFramework;

    using Andreys.Services.Users;
    using Andreys.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputViewModel model)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(model.Username, model.Password);

            if (userId == null)
            {
                return this.View();
            }

            this.SignIn(userId);

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputViewModel model)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            if (model.Username.Length < 4 || model.Username.Length > 10)
            {
                return this.View();
            }

            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.View();
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.View();
            }

            if (this.usersService.IsUsernameExist(model.Username))
            {
                return this.View();
            }

            if (this.usersService.IsEmailExist(model.Email))
            {
                return this.View();
            }

            this.usersService.CreateUser(model.Username, model.Email, model.Password);

            return this.View("Login");
        }

        [HttpGet("/Logout")]
        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}
