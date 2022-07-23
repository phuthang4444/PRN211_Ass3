using BusinessObject.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.Repository {
    public interface IOrderRepository {
        public IEnumerable<Order> GetOrders();
        public Order GetOrderByID(int orderID);
        public void AddOrder(Order order);
        public void UpdateOrder(Order order);
        public void RemoveOrder(int orderID);
        public Order SearchOrder(int orderID, DateTime orderDate, DateTime? requireDate
            , DateTime? shippedDate, Decimal freight);
    }
}
