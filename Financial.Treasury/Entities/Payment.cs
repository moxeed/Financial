using Financial.Treasury.Abstractions;
using System;

namespace Financial.Treasury.Entities
{
    public class Payment : TransactionSource
    {
        public string Source { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime? DoneDateTime { get; set; }
        public int TracingCode { get; set; }

        private Payment() { }
        public Payment(Account account, int amount, string description, string source)
            : base(account, amount, description)
        {
            DeadLine = DateTime.Now;
            Source = source;
        }

        public void Done(int tracingCode)
        {
            TracingCode = tracingCode;
            DoneDateTime = DateTime.Now;
        }
    }
}
