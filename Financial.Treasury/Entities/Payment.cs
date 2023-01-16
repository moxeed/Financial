namespace Financial.Treasury.Entities
{
    public class Payment
    {
        public int Amount { get; set; }
        public string Identifier { get; set; }

        public Payment(int amount, string identifier)
        {
            Amount = amount;
            Identifier = identifier;
        }
    }
}
