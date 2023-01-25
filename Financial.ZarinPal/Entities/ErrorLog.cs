using Financial.Common;
using Financial.Treasury.Entities;
using Financial.ZarinPal.Models;
using Newtonsoft.Json;
using System;

namespace Financial.ZarinPal.Entities
{
    public class ErrorLog : Entity
    {
        public Guid ReferenceCode { get; set; }
        public string Data { get; set; }

        public ErrorLog(Payment payment, Error? error)
        {
            ReferenceCode = payment.ReferenceCode;
            if (error != null)
            {
                Data = JsonConvert.SerializeObject(error);
            }
            Data = "Error Was NULL";
        }

        private ErrorLog()
        {
        }
    }
}
