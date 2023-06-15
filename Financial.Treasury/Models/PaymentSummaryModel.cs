namespace Financial.Treasury.Models
{
    public class PaymentSummaryModel
    {
        public int BlockedAmount { get; internal set; }
        public int PaymentAmount { get; internal set; }
        public string PaymentUrl { get; internal set; }
    }
}
