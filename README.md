# SwiftProject – MT103 Parser API (.NET 10)

## 📌 Description

This project is a simple ASP.NET Core Web API that processes SWIFT MT103 messages.
It accepts a file upload, parses the MT103 message manually (without using any external SWIFT libraries), and stores the extracted data into a SQLite database.

---

## ⚙️ Features

- Upload MT103 file via API
- Custom MT103 parser (no external SWIFT libraries)
- Stores parsed data into SQLite database
- Logging using NLog
- OpenAPI documentation with Scalar UI for interactive API testing

---

## 🏗️ Architecture

Controller → Service → Parser → Repository → SQLite

---

## 📥 API Endpoint

POST /api/mt103

Request:
- file (multipart/form-data, IFormFile)

---

## 🗄️ Database

SQLite is used as the database.

Table: MT103

Stores:
- TransactionReferenceNumber
- BankOperationCode
- ValueDate
- CurrencyCode
- Amount
- OrderingCustomer
- AccountWithInstitution
- BeneficiaryCustomer
- RemittanceInformation
- Charges

---

## 🧪 Example Input

{1:F21PRCBBGSFAXXX2082167565}{4:{177:1602161334}{451:0}}{1:F01PRCBBGSFAXXX2082167565}{2:I103COBADEFFXXXXN}{3:{119:STP}}{4:
:20:160216270075956
:23B:CRED
:32A:160217EUR540,00
:33B:EUR540,00
:50K:/BG95RZBB91556261794271
OKO 1000 OOD
TZAR IVAN SHISHMAN ? 11
SOFIA, BULGARIA
:57A:INGDDEFFXXX
:59:/DE83500105172667785918
FRANCA CEVALES
MUNCHENER STR. 35, GERMANY
:70:ACCOMODATION 11-11.02.16  INVOICE
027/2016
:71A:SHA
-}{5:{MAC:00000000}{CHK:6BC2D5BE9937}}

---

## 🚀 How to Run

1. Clone repository
git clone https://github.com/EmanuilPavlov/SwiftProject.git

2. Open project in Visual Studio

3. Run the application (F5 or dotnet run)

4. Open API documentation (Scalar UI):
https://localhost:<port>/scalar

---

## 📝 Notes

- No external SWIFT parsing libraries used
- No authentication required
- SQLite used for simplicity
- Logging implemented with NLog

---
