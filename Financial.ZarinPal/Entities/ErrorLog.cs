using Financial.Common;
using Financial.Treasury.Entities;
using Financial.ZarinPal.Models;
using System;
using System.Text.Json;

namespace Financial.ZarinPal.Entities
{
    public class ErrorLog : Entity
    {
        public int PaymentId { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

        public ErrorLog(Payment payment, Error? error)
        {
            PaymentId = payment.Id;
            if (error != null)
            {
                Code = error.Code;
                Message = error.Message;
                Data = JsonSerializer.Serialize(error.Validations);
            }
            else
            {
                Code = int.MinValue;
                Message = "Error Was NULL";
                Data = "Error Was NULL";
            }
        }

        private ErrorLog()
        {
        }
    }
}
