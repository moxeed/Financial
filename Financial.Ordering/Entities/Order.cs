using Financial.Common;
using Financial.Ordering.Enums;

namespace Financial.Ordering.Entities
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public OrderStateEnum State { get; set; }

        public Order(int userId)
        {
            UserId = userId;
            State = OrderStateEnum.Basket;
        }
    }
}
