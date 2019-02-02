using CommandLine;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThanksNET.Core;

namespace ThanksNET.ConsoleApp
{
	internal enum ExitCodes : int
	{
		Success = 0,
		Error = 1,
		MissingArguments = 2
	}

	internal class Program
	{
		private static async Task Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.Enrich.WithExceptionDetails()
				.WriteTo.Console()
				.WriteTo.RollingFile(new JsonFormatter(renderMessage: true), "log.json")
				.CreateLogger();

			if (args.Length == 0)
			{
				await ManualMode();
				Console.ReadKey();
			}
			else
			{
				await ParseArguments(args);
			}
		}

		private static async Task StarAll(string solutionDirectory, string githubToken)
		{
			try
			{
				var handler = new PackageHandler(githubToken);
				await handler.StarAllAsync(solutionDirectory);

				Environment.Exit((int)ExitCodes.Success);
			}
			catch (Exception ex)
			{
				Log.Error(ex, ex.Message);

				Environment.Exit((int)ExitCodes.Error);
			}
		}

		private static async Task ParseArguments(string[] args)
		{
			var res = Parser.Default.ParseArguments<ConsoleOptions>(args);
			await res.MapResult(async o =>
			{
				try
				{
					var handler = new PackageHandler(o.GithubToken);
					await handler.StarAllAsync(o.SolutionDirectory);

					Environment.Exit((int)ExitCodes.Success);
				}
				catch (Exception ex)
				{
					Log.Error(ex, ex.Message);

					Environment.Exit((int)ExitCodes.Error);
				}
			},
			err =>
			{
				Log.Error("Missing arguments! " + string.Join(", ", err.Select(e => e.Tag)));

				Environment.Exit((int)ExitCodes.MissingArguments);
				return Task.FromResult((int)ExitCodes.MissingArguments);
			});
		}

		private static async Task ManualMode()
		{
			Console.Write("Please enter solution directory: ");
			string solutionDirectory = Console.ReadLine();

			Console.Write("Please enter github personal token: ");
			string githubToken = Console.ReadLine();

			try
			{
				var handler = new PackageHandler(githubToken);
				await handler.StarAllAsync(solutionDirectory);
			}
			catch (Exception ex)
			{
				Log.Error(ex, ex.Message);
			}
		}
	}
}