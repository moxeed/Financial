﻿namespace Financial.ZarinPal.Models
{
    public class VerifyRequest
    {
        public string merchant_id { get; set; }
        public int amount { get; set; }
        public string authority { get; set; }
    }
}