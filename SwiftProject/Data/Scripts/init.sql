CREATE TABLE IF NOT EXISTS mt103 (
    id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,

    transactionReferenceNumber TEXT NOT NULL ,
    bankOperationCode TEXT NOT NULL,

    valueDate TEXT NOT NULL ,
    currencyCode TEXT NOT NULL,
    amount REAL NOT NULL,

    orderingCustomer TEXT NOT NULL,
    accountWithInstitution TEXT NOT NULL,
    beneficiaryCustomer TEXT NOT NULL,
    remittanceInformation TEXT NOT NULL,
    charges TEXT NOT NULL 
);