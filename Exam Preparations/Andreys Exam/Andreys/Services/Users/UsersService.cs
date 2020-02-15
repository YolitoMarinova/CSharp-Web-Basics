using Andreys.Data;
using Andreys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Andreys.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly AndreysDbContext db;

        public UsersService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void CreateUser(string username, string email, string password)
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

        public string GetUserId(string username, string password)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Username == username && u.Password == this.Hash(password));

            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public bool IsEmailExist(string email)
        {
            if (email == null)
            {
                return false;
            }

            return this.db.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameExist(string username)
            => this.db.Users.Any(u => u.Username == username);

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var sha256 = SHA256Managed.Create();
            var hash = new StringBuilder();
            byte[] crypto = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            foreach (var @byte in crypto)
            {
                hash.Append(@byte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
