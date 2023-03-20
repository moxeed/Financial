using Financial.Common;

namespace Financial.Ordering.Entities
{
    public class OrderDetail : Entity
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int ReferenceCode { get; set; }
        public string ClientData { get; set; }

        public OrderDetail(int productId, int amount, string initiatorData)
        {
            ProductId = productId;
            Amount = amount;
            ClientData = initiatorData;
        }
    }
}
