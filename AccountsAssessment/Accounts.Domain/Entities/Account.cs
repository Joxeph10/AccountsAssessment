namespace Accounts.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Account : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid AccountNumber { get; set; }
        public double Balance { get; set; }
        public virtual List<AccountTransaction> AccountTransactions { get; set; }
    }
}
