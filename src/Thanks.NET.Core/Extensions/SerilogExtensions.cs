using Serilog;
using Serilog.Configuration;
using System;

namespace ThanksNET.Core.Extensions
{
	public static class SerilogExtensions
	{
		public static LoggerConfiguration WriteToIf(this LoggerConfiguration config, bool condition, Action<LoggerSinkConfiguration> sinkFunction)
		{
			if (condition)
			{
				sinkFunction?.Invoke(config.WriteTo);
			}

			return config;
		}
	}
}