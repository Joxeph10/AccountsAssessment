namespace Accounts.API.Dto.Customer
{
    public class TransactionResponse
    {
        public string AccountNumber { get; set; }
        public string TransactionDate { get; set; }
        public string Description { get; set; }
        public double Income { get; set; }
        public string TransactionType { get; set; }
        public double Balance { get; set; }
    }
}
