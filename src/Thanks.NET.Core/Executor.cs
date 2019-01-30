using System;
using System.Linq;
using System.Threading.Tasks;
using ThanksNET.Core.Github;
using ThanksNET.Core.IO;
using ThanksNET.Core.Nuget;

namespace ThanksNET.Core
{
	public class Executor
	{
		#region ASCII

		private const string TITLE_ASCII = @" _____ _                 _          _   _  _____ _____
|_   _| |               | |        | \ | ||  ___|_   _|
  | | | |__   __ _ _ __ | | _____  |  \| || |__   | |
  | | | '_ \ / _` | '_ \| |/ / __| | . ` ||  __|  | |
  | | | | | | (_| | | | |   <\__ \_| |\  || |___  | |
  \_/ |_| |_|\__,_|_| |_|_|\_\___(_)_| \_/\____/  \_/";

		#endregion ASCII

		private NugetWrapper Nuget { get; }
		private GithubWrapper Github { get; }

		public Executor(string githubToken)
		{
			Github = new GithubWrapper(githubToken);
			Nuget = new NugetWrapper();
		}

		public async Task RunAsync(string solutionDirectory)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(TITLE_ASCII);

			var packages = PackageCrawler.Crawl(solutionDirectory);

			var packagesWithMetadata = await Nuget.GetAllAsync(packages);
			var packagesWithGithub = packagesWithMetadata.Where(p => p.Metadata?.HasGithub ?? false);
			var packagesWithGithubFound = packagesWithMetadata.Where(p => p.Metadata?.FoundGithub ?? false);
			var packagesWithProjectUrl = packagesWithMetadata.Where(p => p.Metadata?.ProjectUrl != null && p.Metadata?.HasGithub == false && p.Metadata?.FoundGithub == false);
			var packagesWithNoMetadata = packagesWithMetadata.Where(p => p.Metadata == null);

			Console.WriteLine("-----------------------------------------------------");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("These libraries has been stared on Github:" + Environment.NewLine);
			foreach (var package in packagesWithGithub)
			{
				if (await Github.StarRepo(package.Metadata.GithubUrl))
				{
					Console.Write("* ");
				}
				Console.WriteLine($"{package.Metadata.Title} {package.Version} - {package.Metadata.GithubUrl} - {package.Metadata.ProjectUrl}");
			}

			Console.WriteLine("-----------------------------------------------------");
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("These libraries has been stared on Github(github url found on project website):" + Environment.NewLine);
			foreach (var package in packagesWithGithubFound)
			{
				if (await Github.StarRepo(package.Metadata.GithubUrl))
				{
					Console.Write("* ");
				}
				Console.WriteLine($"{package.Metadata.Title} {package.Version} - {package.Metadata.GithubUrl} - {package.Metadata.ProjectUrl}");
			}

			Console.WriteLine("-----------------------------------------------------");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("We couldn't find github url for these libraries:" + Environment.NewLine);
			foreach (var package in packagesWithProjectUrl)
			{
				Console.WriteLine($"{package.Metadata.Title} {package.Version} - {package.Metadata.ProjectUrl}");
			}

			Console.WriteLine("-----------------------------------------------------");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("We couldn't find any information about these libraries:" + Environment.NewLine);
			foreach (var package in packagesWithNoMetadata)
			{
				Console.WriteLine($"{package.Id} {package.Version}");
			}
		}
	}
}