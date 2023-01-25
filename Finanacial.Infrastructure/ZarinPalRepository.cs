using Financial.ZarinPal.Dependencies;
using Financial.ZarinPal.Entities;

namespace Finanacial.Infrastructure
{
    internal class ZarinPalRepository : IZarinPalRepository
    {
        public Terminal GetTerminal(Guid referenceCode)
        {
            throw new NotImplementedException();
        }

        public void SaveError(ErrorLog error)
        {
            throw new NotImplementedException();
        }

        public void SaveTerminal(Terminal terminal)
        {
            throw new NotImplementedException();
        }

        public void SaveVerifyResult(VerifyResult verifyResult)
        {
            throw new NotImplementedException();
        }
    }
}
