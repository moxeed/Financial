namespace Financial.ZarinPal.Models
{
    public class PaymentResponse
    {
        public PaymentData data { get; set; }
        public Error errors { get; set; }
    }

    public class PaymentData
    {
        public int code { get; set; }
        public string message { get; set; }
        public string authority { get; set; }
        public string fee_type { get; set; }
        public int fee { get; set; }
    }
}
