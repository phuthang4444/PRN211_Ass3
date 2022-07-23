using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository {
    public class OrderRepository : IOrderRepository {
        public void AddOrder(Order order)
            => OrderDAO.Instance.Add(order);

        public Order GetOrderByID(int orderID)
            => OrderDAO.Instance.GetOrderByID(orderID);

        public IEnumerable<Order> GetOrders()
            => OrderDAO.Instance.GetOrderList();

        public void RemoveOrder(int orderID)
            => OrderDAO.Instance.Remove(orderID);

        public Order SearchOrder(int orderID, DateTime orderDate, DateTime? requireDate, DateTime? shippedDate, decimal freight) {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
            => OrderDAO.Instance.Update(order);
    }
}
