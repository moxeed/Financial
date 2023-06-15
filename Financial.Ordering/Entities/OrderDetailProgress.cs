using Financial.Common;
using Financial.Ordering.Enums;

namespace Financial.Ordering.Entities
{
    public class OrderDetailProgress : Entity
    {
        public OrderDetailStateEnum State { get; set; }
        public ushort ProgressPercent { get; set; }
        public string Description { get; set; }

        public OrderDetailProgress(OrderDetailStateEnum state, ushort progressPercent, string description)
        {
            State = state;
            ProgressPercent = progressPercent;
            Description = description;
        }
    }
}
