using SharedTrip.Contracts;
using SharedTrip.Data.Common;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using SharedTrip.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace
SharedTrip.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;

        public UserService(IRepository _repo)
        {
            repo = _repo;
        }


        public void RegisterUser(RegisterViewModel model)
        {
            var userExists = GetUserByUsername(model.UserName) != null;
            if (userExists)
            {
                throw new ArgumentException("Registration failed");
            }

            User user = new User()
            {
                Email = model.Email,
                Username = model.UserName
            };

            user.Password = HashPassword(model.Password);

            try
            {
                repo.Add(user);
                repo.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private User GetUserByUsername(string username)
        { return repo.All<User>().FirstOrDefault(x => x.Username == username); }

        private string HashPassword(string password)
        {
            byte[] passwordArr = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(passwordArr));
            }
        }



        public (bool isValid, IEnumerable<ErrorViewModel> errors)
                ValidateModel(RegisterViewModel model)
        {
            bool isValid = true;
            List<ErrorViewModel> errors = new List<ErrorViewModel>();

            if (model.UserName == null || model.UserName.Length < 5
                || model.UserName.Length > 20)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Username is required and must be between 5 and 20 characters."));
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Email is required."));
            }

            if (model.Password == null || model.Password.Length < 6
                || model.Password.Length > 20)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Password is required and must be between 6 and 20 characters."));
            }

            if (model.Password != model.ConfirmPassword)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Password and ConfirmPassword are not th4e same"));
            }

            return (isValid, errors);
        }

        public (string userID, bool isCorrect) IsLoginCorrect(LoginViewModel model)
        {
            bool isCorrect = false;
            string userId = string.Empty;
            var user = GetUserByUsername(model.Username);

            if (user != null)
            {
                isCorrect = user.Password == HashPassword(model.Password);
            }

            if (isCorrect)
            {
                userId = user.Id;
            }

            return (userId, isCorrect);
        }
    }
}