using SMS.Models;

namespace SMS.Contracts
{
    public  interface IUserService
    {

        (bool isValid, string error) Register(RegisterViewModel model);

    }
}
