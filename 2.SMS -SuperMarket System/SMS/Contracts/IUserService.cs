﻿using SMS.Models;

namespace SMS.Contracts
{
    public  interface IUserService
    {

        (bool isValid, string error) Register(RegisterViewModel model);

        string Login(LoginViewModel model);

        string GetUsername(string userId);
    }
}
