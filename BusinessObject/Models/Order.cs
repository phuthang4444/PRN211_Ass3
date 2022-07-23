using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models {
    public partial class Order {
        public Order() {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Order(int orderId, int memberId, DateTime orderDate, DateTime? requiredDate
            , DateTime? shippedDate, decimal? freight) {
            OrderId = orderId;
            MemberId = memberId;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            Freight = freight;
        }

        public Order(int orderId, int memberId, DateTime orderDate, DateTime? requiredDate
            , DateTime? shippedDate, decimal? freight, Member member
            , ICollection<OrderDetail> orderDetails) : this(orderId, memberId, orderDate
                , requiredDate, shippedDate, freight) {
            Member = member;
            OrderDetails = orderDetails;
        }

        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
