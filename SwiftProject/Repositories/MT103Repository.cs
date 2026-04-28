using Microsoft.Data.Sqlite;
using NLog;
using SwiftProject.Data;
using SwiftProject.Models;

namespace SwiftProject.Repositories
{
    public class MT103Repository(ISqliteConnectionFactory sqliteConnectionFactory) : IMT103Repository
    {
        private readonly ISqliteConnectionFactory _sqliteConnectionFactory = sqliteConnectionFactory;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task InsertAsync(MT103 mT103)
        {
            try
            {
                if (mT103 == null)
                {
                    _logger.Error("MT103 is null");
                    throw new ArgumentNullException(nameof(mT103));
                }

                _logger.Info("Saving proccess started");

                await using var connection = (SqliteConnection)_sqliteConnectionFactory.CreateConnection();
                await connection.OpenAsync();

                await using var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO MT103 (
                    TransactionReferenceNumber,
                    BankOperationCode,
                    ValueDate,
                    CurrencyCode,
                    Amount,
                    OrderingCustomer,
                    AccountWithInstitution,
                    BeneficiaryCustomer,
                    RemittanceInformation,
                    Charges
                )
                VALUES (
                    @transaction, @bankOperation, @valueDate, @currency, @amount,
                    @orderingCustomer, @account, @beneficiaryCustomer, @info, @charge
                );";

                command.Parameters.Add(new SqliteParameter("@transaction", mT103.TransactionReferenceNumber));
                command.Parameters.Add(new SqliteParameter("@bankOperation", mT103.BankOperationCode));
                command.Parameters.Add(new SqliteParameter("@valueDate", mT103.ValueDate?.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqliteParameter("@currency", mT103.CurrencyCode));
                command.Parameters.Add(new SqliteParameter("@amount", mT103.Amount));
                command.Parameters.Add(new SqliteParameter("@orderingCustomer", mT103.OrderingCustomer));
                command.Parameters.Add(new SqliteParameter("@account", mT103.AccountWithInstitution));
                command.Parameters.Add(new SqliteParameter("@beneficiaryCustomer", mT103.BeneficiaryCustomer));
                command.Parameters.Add(new SqliteParameter("@info", mT103.RemittanceInformation));
                command.Parameters.Add(new SqliteParameter("@charge", mT103.Charges));

                await command.ExecuteNonQueryAsync();
                _logger.Info("Succesfully added");
            }
            catch (Exception ex) {
                _logger.Error(ex, "Operation failed");
                throw;
            }
        }
    }
}
