using System.Collections.Generic;

namespace Financial.ZarinPal.Models
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string>[] Validations { get; set; }
    }
}

