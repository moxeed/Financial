namespace Financial.Ordering.Enums
{
    public enum OrderDetailStateEnum : byte
    {
        WaitingForOrder = 1,
        CheckedOut = 2,
        CheckoutError = 3,
        Compeleted = 4
    }
}
