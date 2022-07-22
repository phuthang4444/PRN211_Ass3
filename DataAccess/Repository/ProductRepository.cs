using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void AddNewProduct(Product product)
             => ProductDAO.Instace.AddNew(product);

        public Product GetProductByName(string productName)
            => ProductDAO.Instace.GetProductByName(productName);

        public IEnumerable<Product> GetProductsList()
            => ProductDAO.Instace.GetProductList();

        public void RemoveProduct(String productName)
            => ProductDAO.Instace.Remove(productName);

        public void UpdateProduct(Product product)
        => ProductDAO.Instace.Update(product);
    }
}
