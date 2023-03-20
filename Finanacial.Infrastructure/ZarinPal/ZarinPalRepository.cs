using Financial.ZarinPal.Dependencies;
using Financial.ZarinPal.Entities;

namespace Finanacial.Infrastructure.ZarinPal
{
    internal class ZarinPalRepository : IZarinPalRepository
    {
        private readonly ApplicationDbContext _context;

        public ZarinPalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Terminal? GetTerminal(int paymentId)
        {
            return _context.Set<Terminal>().FirstOrDefault(t => t.PaymentId == paymentId);
        }

        public void SaveError(ErrorLog error)
        {
            _context.Attach(error);
            _context.SaveChanges();
        }

        public void SaveTerminal(Terminal terminal)
        {
            _context.Attach(terminal);
            _context.SaveChanges();
        }

        public void SaveVerifyResult(VerifyResult verifyResult)
        {
            _context.Attach(verifyResult);
            _context.SaveChanges();
        }
    }
}
