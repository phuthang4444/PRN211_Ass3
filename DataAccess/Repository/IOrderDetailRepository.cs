using BusinessObject.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.Repository {
    public interface IOrderDetailRepository {
        public IEnumerable<OrderDetail> GetOrderDetails();
        public Order GetOrderDetailByID(int Id);
        public void AddOrderDetail(OrderDetail oDetail);
        public void UpdateOrderDetail(OrderDetail oDetail);
        public void RemoveOrderDetail(int Id);
    }
}
