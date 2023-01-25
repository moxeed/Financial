namespace Financial.ZarinPal.Models
{
    public class VerifyRequest
    {
        public string Merchant_id { get; set; }
        public int Amount { get; set; }
        public string Authority { get; set; }
    }
}
