using BusinessObject.Models;
using DataAccess.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess {
    public class OrderDAO : BaseDAL {
        private static OrderDAO instance = null;
        private static readonly object instancelock = new object();
        
        private OrderDAO() { }

        public static OrderDAO Instance {
            get {
                lock (instancelock) {
                    if (instance == null) {
                        instance = new OrderDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Order> GetOrderList() {
            /*IDataReader dataReader = null;
            string SQLSelect = "SELECT * FROM Order";
            var orders = new List<Order>();

            try {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read()) {
                    orders.Add(new Order(
                        dataReader.GetInt32(0),
                        dataReader.GetInt32(1),
                        dataReader.GetDateTime(2),
                        dataReader.GetDateTime(3),
                        dataReader.GetDateTime(4),
                        dataReader.GetDecimal(5)
                    ));
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally {
                dataReader.Close();
                CloseConnection();
            }

            return orders;*/
            List<Order> orders;

            try {
                using eStoreContext eStoreContext = new eStoreContext();
                orders = eStoreContext.Orders.ToList();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }

            return orders;
        }

        public Order GetOrderByID(int orderID) {
            Order order = null;
            /*IDataReader dataReader = null;
            string SQLSelect = "SELECT *\n" +
                "FROM Order\n" +
                "WHERE OrderID = @OrderID";

            try {
                var param = dataProvider.CreateParameter("@OrderID", 4, orderID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read()) {
                    order = new Order(
                        dataReader.GetInt32(0),
                        dataReader.GetInt32(1),
                        dataReader.GetDateTime(2),
                        dataReader.GetDateTime(3),
                        dataReader.GetDateTime(4),
                        dataReader.GetDecimal(5)
                    );
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally {
                dataReader.Close();
                CloseConnection();
            }

            return order;*/

            try {
                using eStoreContext stock = new eStoreContext();
                order = stock.Orders.SingleOrDefault(o => o.OrderId == orderID);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }

            return order;
        }

        public void Add(Order order) {
            try {
                using eStoreContext stock = new eStoreContext();
                stock.Orders.Add(order);
                stock.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order order) { 
            try {
                using eStoreContext stock = new eStoreContext();
                stock.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                stock.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int orderID) { 
            try {
                using eStoreContext stock = new eStoreContext();
                var order = stock.Orders.SingleOrDefault(o => o.OrderId == orderID);
                stock.Orders.Remove(order);
                stock.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
