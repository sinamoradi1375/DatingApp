using System.Collections.Generic;
using DatingApp.Api.Models;
using Newtonsoft.Json;

namespace DatingApp.Api.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var item in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                item.PasswordHash = passwordHash;
                item.PasswordSalt = passwordSalt;
                item.UserName = item.UserName.ToLower();

                _context.Users.Add(item);
            }

            _context.SaveChanges();
        }

         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {            
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}