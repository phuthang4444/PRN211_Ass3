using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    internal interface IProductRepository
    {
        public IEnumerable<Product> GetProductsList();
        public Product GetProductById(int productID);
        public Product GetProductByName(string productName);
        public Product SearchProduct(int productID, string productName, Decimal UnitPrice, int UnitsInStock);
        public void AddNew(Product product);
        public void Remove(Product product);
    }
}
