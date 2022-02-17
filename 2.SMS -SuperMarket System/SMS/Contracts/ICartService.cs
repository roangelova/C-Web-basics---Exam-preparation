using SMS.Models;
using System.Collections;
using System.Collections.Generic;

namespace SMS.Contracts
{
    public  interface ICartService
    {

        IEnumerable<CartViewModel> AddProduct(string productId, string userId);
    }
}
