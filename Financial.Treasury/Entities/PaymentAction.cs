using System;

namespace Financial.Treasury.Entities
{
    public class PaymentAction
    {
        public int UserId { get; set; }
        public string? TracingCode { get; set; }
        public DateTime ActionDateTime { get; set; }
        public PaymenActionType AcionType { get; set; }

        public PaymentAction(int userId, string? tracingCode, PaymenActionType acionType)
        {
            UserId = userId;
            AcionType = acionType;
            TracingCode = tracingCode;
            ActionDateTime = DateTime.Now;
        }
    }
}
