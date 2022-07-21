using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.DataProvider;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using System.IO;
using Microsoft.Extensions.Configuration;
using BusinessObject.Models;
using DataAccess.Repository;

namespace DataAccess
{
    public class ProductDAO : BaseDAL
    {
        private static ProductDAO instance = null;

        private static readonly object instancelock = new object();

        private ProductDAO() { }

        public static ProductDAO Instace
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Product> ProductList = new List<Product>();

        public IEnumerable<Product> GetProductList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select ProductID, CategoryID, ProductName, Weight, UnitPrice, UnitslnStock " +
                "From Product";
            var products = new List<Product>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = dataReader.GetInt32(0),
                        CategoryId = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitslnStock = dataReader.GetInt32(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return products;
        }
        public Product GetProductByName(string productName)
        {
            Product product = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select ProductId, CategoryId, ProductName, Weight, UnitPrice, UnitslnStock "
                + "  From Product" +
                " Where ProductName = @ProductName";
            try
            {
                var param = dataProvider.CreateParameter("@ProductName", 4, productName, DbType.String);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    product = new Product
                    {
                        ProductId = dataReader.GetInt32(0),
                        CategoryId = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitslnStock = dataReader.GetInt32(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return product;
        }

        public void AddNew(Product product)
        {
            try
            {
                Product addPro = GetProductByName(product.ProductName);
                if (addPro == null)
                {
                    string SQLInsert = "Insert Product " +
                        "values(@ProductId,@ProductName,@Weight,@UnitPrice,@UnitslnStock)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@ProductId", 100, product.ProductId, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@ProductName", 40, product.ProductName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Weight", 15, product.Weight, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@UnitPrice", 15, product.UnitPrice, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@UnitslnStock", 30, product.UnitslnStock, DbType.String));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("Product exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(Product product)
        {
            Product proUpdate = GetProductByName(product.ProductName);

            try
            {
                if (proUpdate != null)
                {
                    string SQLUpdate = "Update Product" +
                        " set ProductId = @ProductId," +
                            " ProductName = @ProductName" +
                            " Weight = @Weight" +
                            " UnitPrice = @UnitPrice" +
                            " UnitslnStock = @UnitslnStock" +
                        " Where ProductName = @ProductName";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter("@ProductId", 100, product.ProductId, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@ProductName", 40, product.ProductName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@Weight", 15, product.Weight, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@UnitPrice", 15, product.UnitPrice, DbType.String));
                    parameters.Add(dataProvider.CreateParameter("@UnitslnStock", 30, product.UnitslnStock, DbType.String));
                    dataProvider.Insert(SQLUpdate, CommandType.Text, parameters.ToArray());

                }
                else
                {
                    throw new Exception("Product exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Remove(string productName)
        {
            Product proRemove = null;

            try
            {
                proRemove = GetProductByName(productName);

                if (proRemove != null)
                {
                    string SQLDelete = "Delete Product from Product" +
                        " Where ProductName = @ProductName";

                    var param = dataProvider.CreateParameter(SQLDelete, 4, CommandType.Text, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("Product not exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
