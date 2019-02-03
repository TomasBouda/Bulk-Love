using NuGet.Common;
using Serilog.Core;

namespace ThanksNET.Core.Logging
{
	internal class NugetLogger : ILogger
	{
		private Logger _logger;

		public NugetLogger(Logger logger)
		{
			_logger = logger;
		}

		public void LogDebug(string data) => _logger.Debug(data);

		public void LogVerbose(string data) => _logger.Verbose(data);

		public void LogInformation(string data) => _logger.Information(data);

		public void LogMinimal(string data) => _logger.Information($"MINIMAL: {data}");

		public void LogWarning(string data) => _logger.Warning(data);

		public void LogError(string data) => _logger.Error(data);

		public void LogSummary(string data) => _logger.Information($"SUMMARY: {data}");

		public void LogInformationSummary(string data) => _logger.Information($"INFO SUMMARY: {data}");

		public void LogErrorSummary(string data) => _logger.Error($"ERROR SUMMARY: {data}");
	}
}