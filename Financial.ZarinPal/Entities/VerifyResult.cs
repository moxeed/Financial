using Financial.Common;
using Financial.ZarinPal.Models;
using System;

namespace Financial.ZarinPal.Entities
{
    public class VerifyResult : Entity
    {
        public Guid ReferenceCode { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string CardHash { get; set; }
        public string CardPan { get; set; }
        public int RefId { get; set; }
        public string FeeType { get; set; }
        public int Fee { get; set; }

        public VerifyResult(Terminal terminal, VerifyData data)
        {
            ReferenceCode = terminal.ReferenceCode;
            Code = data.Code;
            Message = data.Message;
            CardHash = data.Card_hash;
            CardPan = data.Card_pan;
            RefId = data.Ref_id;
            FeeType = data.Fee_type;
            Fee = data.Fee;
        }

        private VerifyResult()
        {
        }
    }
}
