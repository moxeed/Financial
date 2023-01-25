namespace Financial.ZarinPal.Models
{
    public class PaymentResponse
    {
        public PaymentData? Data { get; set; }
        public Error? Errors { get; set; }
    }

    public class PaymentData
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Authority { get; set; }
        public string Fee_type { get; set; }
        public int Fee { get; set; }
    }
}
