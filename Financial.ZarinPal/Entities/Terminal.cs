using Financial.Common;
using Financial.Treasury.Entities;
using Financial.ZarinPal.Models;
using System;

namespace Financial.ZarinPal.Entities
{
    public class Terminal : Entity
    {
        public Guid ReferenceCode { get; set; }
        public string Authority { get; set; }
        public int Code { get; set; }
        public int Amount { get; set; }
        public string Message { get; set; }
        public string FeeType { get; set; }
        public int Fee { get; set; }

        public Terminal(Payment payment, PaymentData data)
        {
            ReferenceCode = payment.ReferenceCode;
            Amount = payment.Amount;
            Authority = data.Authority;
            Code = data.Code;
            Message = data.Message;
            FeeType = data.Fee_type;
            Fee = data.Fee;
        }

        private Terminal()
        {
        }
    }
}
