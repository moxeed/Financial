using Financial.Common;
using Financial.Ordering.Enums;
using System.Collections.Generic;

namespace Financial.Ordering.Entities
{
    public class OrderDetail : Entity
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int ReferenceCode { get; set; }
        public string ClientData { get; set; }
        public OrderDetailStateEnum State { get; set; }
        public ICollection<OrderDetailProgress> ProgressItems { get; set; }

        private OrderDetail() 
        {
            ProgressItems = new HashSet<OrderDetailProgress>();
        }

        public OrderDetail(int productId, int amount, string initiatorData) :this()
        {
            ProductId = productId;
            Amount = amount;
            ClientData = initiatorData;
        }

        public void UpdateProgress(ushort progressPercent) 
        {
            var progresItem = new OrderDetailProgress(State, progressPercent, "استعلام وضعیت سفارش");
            ProgressItems.Add(progresItem);
        }
    }
}
