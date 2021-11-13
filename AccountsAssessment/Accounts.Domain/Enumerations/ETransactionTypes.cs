namespace Accounts.Domain.Enumerations
{
    using System.ComponentModel;

    public enum ETransactionTypes
    {
        [Description("Credit")]
        Credit = 1,
        [Description("Debit")]
        Debit = 2
    }
}
