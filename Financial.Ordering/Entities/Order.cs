using Financial.Common;
using Financial.Ordering.Enums;
using System;
using System.Collections.Generic;

namespace Financial.Ordering.Entities
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public List<OrderDetail> Details { get; set; }
        public List<OrderState> States { get; set; }

        public Order(int userId)
        {
            UserId = userId;
            Details = new List<OrderDetail>();
            States = new List<OrderState>();
        }
    }
}
