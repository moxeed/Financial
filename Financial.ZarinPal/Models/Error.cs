using System.Collections.Generic;

namespace Financial.ZarinPal.Models
{
    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
        public IDictionary<string, string>[] validations { get; set; }
    }
}

