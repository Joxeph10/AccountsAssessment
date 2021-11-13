namespace Accounts.Domain.Entities
{
    using Accounts.Domain.Enumerations;
    using System.ComponentModel.DataAnnotations;

    public class AccountTransaction : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public ETransactionTypes TransactionType { get; set; }
        public string Comment { get; set; }
    }
}
