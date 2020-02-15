using IRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IRunesDbContext db;

        public UsersService(IRunesDbContext db)
        {
            this.db = db;
        }

        public string GetUserId(string username, string password)
        {
            var user = this.db
                .Users
                .FirstOrDefault(
                 u => u.Username == username &&
                 u.Password == this.Hash(password));

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

        public string GetUsername(string userId)
        {
            return this.db.Users.FirstOrDefault(u => u.Id == userId).Username;
        }

        public void RegisterUser(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = this.Hash(password),
                Email = email
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

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
