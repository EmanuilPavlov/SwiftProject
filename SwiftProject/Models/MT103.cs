namespace SwiftProject.Models
{
    public class MT103
    {
        // :20: Transaction Reference Number
        public required string TransactionReferenceNumber { get; set; }

        // :23B: Bank Operation Code
        public required string BankOperationCode { get; set; }

        // :32A: Value Date, Currency Code, Amount
        public DateTime? ValueDate { get; set; }
        public required string CurrencyCode { get; set; }
        public decimal? Amount { get; set; }

        // :50k: Customer
        public required string OrderingCustomer { get; set; }

        // :57A: Account with institution 
        public required string AccountWithInstitution { get; set; }

        // :59: Beneficiary Customer
        public required string BeneficiaryCustomer { get; set; }

        // :70: Remittance Information
        public required string RemittanceInformation { get; set; }

        // :71A: Details of Charges 
        public required string Charges { get; set; }
    }
}
