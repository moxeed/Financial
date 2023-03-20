using Financial.Common;
using Financial.Ordering.Enums;

namespace Financial.Ordering.Entities
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int ReferenceCode { get; set; }
        public string ClientData { get; set; }
        public OrderStateEnum State { get; set; }

        public Order(int productId, int amount, string initiatorData)
        {
            ProductId = productId;
            Amount = amount;
            ClientData = initiatorData;
        }
    }
}
