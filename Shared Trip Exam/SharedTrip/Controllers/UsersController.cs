using SharedTrip.Services.Users;
using SharedTrip.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;

namespace SharedTrip.Controllers
{
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
            var userId = this.usersService.GetUserId(model.Username, model.Password);

            if (userId == null)
            {
                return this.Error("Invalid login data!");
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

            if (model.Username.Length < 5 || model.Username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 symbols!");
            }

            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 symbols!");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Error("Password and Confirm Password should be the same!");
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return this.Error("Invalid Email!");
            }

            if (this.usersService.IsUsernameExist(model.Username))
            {
                return this.Error("Username is already used!");
            }

            if (this.usersService.IsEmailExist(model.Email))
            {
                return this.Error("Email is already used!");
            }

            this.usersService.RegisterUser(model.Username, model.Email, model.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }
    }
}
