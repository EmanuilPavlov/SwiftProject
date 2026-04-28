using NLog;
using SwiftProject.Models;
using System.Text.RegularExpressions;

namespace SwiftProject.Parsers
{
    public class MT103Parser : IMT103Parser
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private static readonly Regex TagRegex = new Regex(@"(?<tag>:\d{2}[A-Z]?:)(?<value>.*?)(?=:\d{2}[A-Z]?:|$)", RegexOptions.Singleline | RegexOptions.Compiled);
        
        public MT103 Parse(string mT103)
        {
            try
            {
                if (string.IsNullOrEmpty(mT103))
                    throw new ArgumentException("MT103 message can't be null or empty");

                _logger.Info("Parsing proccess started");

                var fields = new Dictionary<string, string>();
                foreach (Match match in TagRegex.Matches(mT103))
                {
                    string tag = match.Groups["tag"].Value.Trim(':');
                    string value = match.Groups["value"].Value.Trim();
                    fields[tag] = value;

                    _logger.Debug($"Tag - {tag}, value - {value}");
                }

                if (string.IsNullOrEmpty(fields.GetValueOrDefault("32A")))
                {
                    _logger.Error("Invalid data for 32A");
                    throw new FormatException("Invalid MT103 data for field 32A");
                }

                var date = DateTime.ParseExact(fields.GetValueOrDefault("32A").Substring(0, 6), "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                var currency = fields.GetValueOrDefault("32A").Substring(6, 3);
                var amount = Decimal.Parse(fields.GetValueOrDefault("32A").Substring(9));

                return new MT103
                {
                    TransactionReferenceNumber = fields.GetValueOrDefault("20"),
                    BankOperationCode = fields.GetValueOrDefault("23B"),
                    ValueDate = date,
                    CurrencyCode = currency,
                    Amount = amount,
                    OrderingCustomer = fields.GetValueOrDefault("50K"),
                    AccountWithInstitution = fields.GetValueOrDefault("57A"),
                    BeneficiaryCustomer = fields.GetValueOrDefault("59"),
                    RemittanceInformation = fields.GetValueOrDefault("70"),
                    Charges = fields.GetValueOrDefault("71A")
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Parsing proccess failed");
                throw;
            }
        }
    }
}
