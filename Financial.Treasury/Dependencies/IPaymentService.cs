using Financial.Treasury.Entities;
using Financial.Treasury.Models;
using System.Threading.Tasks;

namespace Financial.Treasury.Dependencies
{
    public interface IPaymentService
    {
        public string SourceName { get; }
        public string GetPaymentLink(Payment payment);
        Task<VerifyResultModel> Verify(Payment payment);
    }
}
