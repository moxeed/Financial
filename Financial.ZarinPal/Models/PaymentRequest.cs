namespace Financial.ZarinPal.Models
{
    public class PaymentRequest
    {
        public string Merchant_id { get; set; }
        public int Amount { get; set; }
        public string Callback_url { get; set; }
        public string Description { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class Metadata
    {
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}


