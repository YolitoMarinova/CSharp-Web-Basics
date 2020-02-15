using IRunes.Services.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
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
            if (!this.IsUserLoggedIn())
            {
                return this.View();
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(username, password);

            if (userId != null)
            {
                this.SignIn(userId);
                return this.Redirect("/");
            }

            return this.View();
        }

        public HttpResponse Register()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.View();
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public HttpResponse Register(string username, string password,string confirmPassword, string email)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            if (username.Length < 4 || username.Length > 10)
            {
                return this.View();
            }

            if (password.Length < 6 || password.Length > 20)
            {
                return this.View();
            }

            if (password != confirmPassword)
            {
                return this.View();
            }

            if (usersService.IsUsernameExist(username))
            {
                return this.View();
            }

            if (usersService.IsEmailExist(email))
            {
                return this.View();
            }

            this.usersService.RegisterUser(username, password, email);

            return this.Redirect("Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }
    }
}
