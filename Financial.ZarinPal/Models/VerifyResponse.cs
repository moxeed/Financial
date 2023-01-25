using System.Net.Http.Headers;

namespace Financial.ZarinPal.Models
{
    public class VerifyResponse
    {
        public VerifyData? Data { get; set; }
        public Error? Errors { get; set; }
    }

    public class VerifyData
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Card_hash { get; set; }
        public string Card_pan { get; set; }
        public int Ref_id { get; set; }
        public string Fee_type { get; set; }
        public int Fee { get; set; }
        public bool IsSucceded => Code == 100 || Code == 101;
    }
}
