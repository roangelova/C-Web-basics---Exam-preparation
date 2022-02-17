using Microsoft.EntityFrameworkCore;
using SMS.Contracts;
using SMS.Data.Common;
using SMS.Data.Models;
using SMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository repo;

        public CartService(IRepository _repo)
        {
            repo = _repo;
        }

        public IEnumerable<CartViewModel> AddProduct(string productId, string userId)
        {

            var user = repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(C => C.Products)
                .FirstOrDefault();

            var product = repo.All<Product>()
                .FirstOrDefault(p => p.Id == productId);

            user.Cart.Products.Add(product);

            try
            {
                repo.SaveChanges();
            }
            catch (System.Exception)
            {

            }

            return user.Cart
                .Products.
                Select(p => new CartViewModel()
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price.ToString("F2")
                });
        }

        public void BuyProducts(string userId)
        {
            var user = repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(C => C.Products)
                .FirstOrDefault();

            user.Cart.Products.Clear();

            repo.SaveChanges();
        }

        public IEnumerable<CartViewModel> GetProducts(string userId)
        {
            var user = repo.All<User>()
                  .Where(u => u.Id == userId)
                  .Include(u => u.Cart)
                  .ThenInclude(C => C.Products)
                  .FirstOrDefault();

            return user.Cart
                .Products.
                Select(p => new CartViewModel()
                {
                    ProductName = p.Name,
                    ProductPrice = p.Price.ToString("F2")
                });
        }
    }
}
