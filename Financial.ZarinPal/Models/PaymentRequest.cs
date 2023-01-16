namespace Financial.ZarinPal.Models
{
    public class PaymentRequest
    {
        public string merchant_id { get; set; }
        public int amount { get; set; }
        public string callback_url { get; set; }
        public string description { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        public string mobile { get; set; }
        public string email { get; set; }
    }
}


