using Financial.ZarinPal.Entities;
using System;

namespace Financial.ZarinPal.Dependencies
{
    public interface IZarinPalRepository
    {
        void SaveTerminal(Terminal terminal);
        void SaveError(ErrorLog error);
        void SaveVerifyResult(VerifyResult verifyResult);
        Terminal GetTerminal(Guid referenceCode);
    }
}
