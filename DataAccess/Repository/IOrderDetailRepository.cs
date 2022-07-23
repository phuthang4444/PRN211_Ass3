using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.Repository {
    public interface IOrderDetailRepository {
        public IEnumerable<OrderDetail> GetOrderDetails();
        public OrderDetail GetOrderDetailByID(int Id);
        public void AddOrderDetail(OrderDetail oDetail);
        public void UpdateOrderDetail(OrderDetail oDetail);
        public void RemoveOrderDetail(int Id);
    }
}
