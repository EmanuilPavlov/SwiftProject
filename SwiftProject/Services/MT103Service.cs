using NLog;
using SwiftProject.Parsers;
using SwiftProject.Repositories;

namespace SwiftProject.Services
{
    public class MT103Service(IMT103Repository repository, IMT103Parser parser) : IMT103Service
    {
        private readonly IMT103Parser _parser = parser;
        private readonly IMT103Repository _repository =  repository;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task SaveAsync(IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    _logger.Error("File is null");
                    throw new ArgumentNullException(nameof(file));
                }

                _logger.Info($"MT103 processing started. File: {file.FileName}");

                using var reader = new StreamReader(file.OpenReadStream());
                var content = await reader.ReadToEndAsync();
                _logger.Info("MT103 content read successfully");

                var mT103 = _parser.Parse(content);
                _logger.Info("MT103 parsed successfully");

                await _repository.InsertAsync(mT103);
                _logger.Info("MT103 processing completed successfully");
            }
            catch (Exception ex) {
                _logger.Error(ex, $"MT103 processing failed for file {file.FileName}");
                throw;
            }
        }
    }
}
