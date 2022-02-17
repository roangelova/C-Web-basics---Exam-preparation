using SMS.Contracts;
using SMS.Data.Common;
using SMS.Data.Models;
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SMS.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository repo;

        decimal price = 0;

        public ProductService(IRepository _repo)
        {
            repo = _repo;
        }

        public (bool created, string error) Create(CreateViewModel model)
        {
            bool created = false;
            string error = null;

            var (isValid, validationError) = ValidateProduct(model);

            if (!isValid)
            {
                return (isValid, validationError);
            }

            Product product = new Product()
            {
                Price = price,
                Name = model.Name

            };


            try
            {
                repo.Add(product);
                repo.SaveChanges();

                created = true;
            }
            catch (Exception)
            {
                error = "Could not complete the operation";
            }

            return (created, error);
        }

        public IEnumerable<ProductListViewModel> GetProducts()
        {
            return repo.All<Product>()
                .Select(
                p => new ProductListViewModel()
                {
                    ProductName = p.Name,
                    ProductPrice =  p.Price.ToString("F2"),
                    ProductId = p.Id
                })
                .ToList();
        }

        private (bool isValid, string error) ValidateProduct(CreateViewModel model)
        {
            bool isValid = true;
            StringBuilder error = new();

            if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < 4 || model.Name.Length > 20)
            {
                isValid = false;
                error.AppendLine("Name must be between 4 and 20 characters");
            }

            if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < 4 || model.Name.Length > 20)
            {
                isValid = false;
                error.AppendLine("Name must be between 4 and 20 characters");
            }


            if (!decimal.TryParse(model.Price,NumberStyles.Float,CultureInfo.InvariantCulture, out price)
                || price < 0.05m || price > 1000m)
            {
                isValid = false;
                error.AppendLine("Product price must be between 0.05 and 1000");
            }

            return (isValid, error.ToString());

        }
    }
}
