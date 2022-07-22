using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    internal interface IProductRepository
    {
        public IEnumerable<Product> GetProductsList();
        public Product GetProductByName(string productName);
        public void AddNewProduct(Product product);
        public void RemoveProduct(String productName);
        public void UpdateProduct(Product product);
    }
}
