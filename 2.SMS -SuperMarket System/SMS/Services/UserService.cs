using SMS.Contracts;
using SMS.Data.Common;
using SMS.Data.Models;
using SMS.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SMS.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;

        public UserService(IRepository _repo)
        {
            repo = _repo;
        }

        public string Login(LoginViewModel model)
        {
            var user = repo.All<User>()
                 .Where(u => u.Username == model.Username)
                 .Where(u => u.Password == HashPassword(model.Password))
                 .SingleOrDefault();

            return user?.Id;
        }

        public (bool isValid, string error) Register(RegisterViewModel model)
        {
            bool registered = false;
            string error = null;

            var (isValid, validationError) = ValidateRegisterModel(model);

            if (!isValid)
            {
                return (isValid, validationError);
            }

            Cart cart = new Cart();

            User user = new User()
            {
                Email = model.Email,
                Password = HashPassword(model.Password),
                Username = model.Username,
                Cart = cart,
                CardId = cart.Id
            };

            try
            {
                repo.Add(user);
                repo.SaveChanges();
                registered = true;
            }
            catch (Exception)
            {
                error = "Could not save user to database!";

            }

            return (registered, error); 
        }

        private string HashPassword(string password)
        {
            byte[] passwordArr = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(passwordArr));
            }
        }

        private (bool isValid, string error) ValidateRegisterModel(RegisterViewModel model)
        {
            bool isValid = true;
            StringBuilder error = new();

            if (model == null)
            {
                return (false, "Register model is required");
            }

            if (string.IsNullOrWhiteSpace(model.Username) || model.Username.Length < 5 || model.Username.Length > 20)
            {
                isValid = false;
                error.AppendLine("Username must be between 5 and 20 characters.");
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                isValid = false;
                error.AppendLine("Email must be a valid email");
            }
            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < 6 || model.Password.Length > 20)
            {
                isValid = false;
                error.AppendLine("Password must be between 6 and 20 characters");
            }
            if (model.Password != model.ConfirmPassword)
            {

                isValid = false;
                error.AppendLine("Password and ConfirmPaddword must match!");
            }

            return (isValid, error.ToString());
        }
    }
}
