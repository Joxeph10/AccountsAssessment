namespace Accounts.Domain.Entities
{
    using System;

    public abstract class AuditEntity
    {
        public int CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedbyID { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
