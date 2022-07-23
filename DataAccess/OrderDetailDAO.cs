using BusinessObject.Models;
using DataAccess.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess {
    public class OrderDetailDAO : BaseDAL {
        private static OrderDetailDAO instance = null;
        private static readonly object instancelock = new object();
        
        private OrderDetailDAO() { }

        public static OrderDetailDAO Instance {
            get {
                lock (instancelock) {
                    if (instance == null) {
                        instance = new OrderDetailDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetailList() {
            /*IDataReader dataReader = null;
            string SQLSelect = "SELECT * FROM OrderDetail";
            var OrderDetails = new List<OrderDetail>();

            try {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read()) {
                    OrderDetails.Add(new OrderDetail(
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

            return OrderDetails;*/
            List<OrderDetail> OrderDetails;

            try {
                using eStoreContext eStoreContext = new eStoreContext();
                OrderDetails = eStoreContext.OrderDetails.ToList();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }

            return OrderDetails;
        }

        public OrderDetail GetOrderDetailByID(int OrderDetailID) {
            OrderDetail OrderDetail = null;
            /*IDataReader dataReader = null;
            string SQLSelect = "SELECT *\n" +
                "FROM OrderDetail\n" +
                "WHERE OrderDetailID = @OrderDetailID";

            try {
                var param = dataProvider.CreateParameter("@OrderDetailID", 4, OrderDetailID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read()) {
                    OrderDetail = new OrderDetail(
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

            return OrderDetail;*/

            try {
                using eStoreContext stock = new eStoreContext();
                OrderDetail = stock.OrderDetails.SingleOrDefault(o => o.OrderId == OrderDetailID);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }

            return OrderDetail;
        }

        public void Add(OrderDetail oDetail) {
            try {
                using eStoreContext stock = new eStoreContext();
                stock.OrderDetails.Add(oDetail);
                stock.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Update(OrderDetail oDetail) { 
            try {
                using eStoreContext stock = new eStoreContext();
                stock.Entry<OrderDetail>(oDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                stock.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int OrderDetailID) { 
            try {
                using eStoreContext stock = new eStoreContext();
                var OrderDetail = stock.OrderDetails.SingleOrDefault(o => o.OrderId== OrderDetailID);
                stock.OrderDetails.Remove(OrderDetail);
                stock.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
