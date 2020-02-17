using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string GetUserId(string username, string password)
        {
            var hashedPassowrd = this.Hash(password);
            var user = this.db.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassowrd);

            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public bool IsEmailExist(string email)
            => this.db.Users.Any(u => u.Email == email);

        public bool IsUsernameExist(string username)
            => this.db.Users.Any(u => u.Username == username);

        public void RegisterUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = this.Hash(password)
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var sha256 = SHA256.Create();
            var result = new StringBuilder();
            byte[] crypt = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            foreach (var @byte in crypt)
            {
                result.Append(@byte.ToString("x2"));
            }

            return result.ToString();
        }
    }
}
