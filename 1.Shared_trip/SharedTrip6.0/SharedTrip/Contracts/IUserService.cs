﻿using SharedTrip.Models;
using SharedTrip.Models.Users;
using System.Collections.Generic;

namespace SharedTrip.Contracts
{
    public interface IUserService
    {
        (bool isValid, IEnumerable<ErrorViewModel> errors)
            ValidateModel(RegisterViewModel model);

        void RegisterUser(RegisterViewModel model);
        (string userID, bool isCorrect) IsLoginCorrect(LoginViewModel model);
    }
}
