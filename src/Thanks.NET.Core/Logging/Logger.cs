using NuGet.Common;
using System;
using System.Threading.Tasks;

namespace ThanksNET.Core.Logging
{
	public class Logger : ILogger
	{
		public void LogDebug(string data) => System.Console.WriteLine($"DEBUG: {data}");

		public void LogVerbose(string data) => System.Console.WriteLine($"VERBOSE: {data}");

		public void LogInformation(string data) => System.Console.WriteLine($"INFORMATION: {data}");

		public void LogMinimal(string data) => System.Console.WriteLine($"MINIMAL: {data}");

		public void LogWarning(string data) => System.Console.WriteLine($"WARNING: {data}");

		public void LogError(string data) => System.Console.WriteLine($"ERROR: {data}");

		public void LogSummary(string data) => System.Console.WriteLine($"SUMMARY: {data}");

		public void LogInformationSummary(string data) => System.Console.WriteLine($"SUMMARY: {data}");

		public void LogErrorSummary(string data) => System.Console.WriteLine($"ERR SUMMARY: {data}");

		public void Log(LogLevel level, string data) => System.Console.WriteLine($"ERR SUMMARY: {data}");

		public Task LogAsync(LogLevel level, string data)
		{
			throw new NotImplementedException();
		}
	}
}