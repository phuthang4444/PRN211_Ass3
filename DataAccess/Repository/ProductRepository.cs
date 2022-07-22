using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void AddNew(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int productID)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(string productName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsList()
        {
            throw new NotImplementedException();
        }

        public void Remove(Product product)
        {
            throw new NotImplementedException();
        }

        public Product SearchProduct(int productID, string productName, decimal UnitPrice, int UnitsInStock)
        {
            throw new NotImplementedException();
        }
    }
}
