using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository {
    public class OrderDetailRepository : IOrderDetailRepository {
        public void AddOrderDetail(OrderDetail oDetail)
            => OrderDetailDAO.Instance.Add(oDetail);

        public OrderDetail GetOrderDetailByID(int Id)
            => OrderDetailDAO.Instance.GetOrderDetailByID(Id);

        public IEnumerable<OrderDetail> GetOrderDetails()
            => OrderDetailDAO.Instance.GetOrderDetailList();

        public void RemoveOrderDetail(int Id)
            => OrderDetailDAO.Instance.Remove(Id);

        public void UpdateOrderDetail(OrderDetail oDetail)
            => OrderDetailDAO.Instance.Update(oDetail);
    }
}
