using Financial.Common;
using Financial.Ordering.Enums;

namespace Financial.Ordering.Entities
{
    public class OrderState : Entity
    {
        public OrderStateEnum State { get; }

        public OrderState(OrderStateEnum state)
        {
            State = state;
        }
    }
}
