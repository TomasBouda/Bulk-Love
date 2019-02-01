using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanksNET.Core.Github;
using ThanksNET.Core.IO;
using ThanksNET.Core.Nuget.Packages;

namespace ThanksNET.Core
{
	public class PackageHandler
	{
		#region ASCII

		private const string TITLE_ASCII = @" _____ _                 _          _   _  _____ _____
|_   _| |               | |        | \ | ||  ___|_   _|
  | | | |__   __ _ _ __ | | _____  |  \| || |__   | |
  | | | '_ \ / _` | '_ \| |/ / __| | . ` ||  __|  | |
  | | | | | | (_| | | | |   <\__ \_| |\  || |___  | |
  \_/ |_| |_|\__,_|_| |_|_|\_\___(_)_| \_/\____/  \_/";

		#endregion ASCII

		private GithubWrapper Github { get; }

		public GrouppedPackages GrouppedPackages { get; private set; }

		public PackageHandler(string githubToken)
		{
			Github = new GithubWrapper(githubToken);
		}

		public static async Task<GrouppedPackages> GetAllPackages(string solutionDirectory)
		{
			var packages = PackageCrawler.Crawl(solutionDirectory);
			return await GrouppedPackages.FromPackageRefAsync(packages);
		}

		public static async Task<bool> StarPackage(PackageReference package, string githubToken)
		{
			if ((package.Metadata?.HasGithub == false && package.Metadata?.HasParsedGithub == false) || string.IsNullOrEmpty(package.Metadata?.GithubUrl))
				throw new ArgumentException("Package must have github url!", nameof(package));

			var github = new GithubWrapper(githubToken);
			return await github.StarRepo(package.Metadata.GithubUrl);
		}

		public async Task<bool> StarPackage(PackageReference package)
		{
			if ((package.Metadata?.HasGithub == false && package.Metadata?.HasParsedGithub == false) || string.IsNullOrEmpty(package.Metadata?.GithubUrl))
				throw new ArgumentException("Package must have github url!", nameof(package));

			return await Github.StarRepo(package.Metadata.GithubUrl);
		}

		/// <summary>
		/// Finds all packages in given solution direcotry, gets their github url from nuget.org. If there is no github url, then tryes to find it on project website.
		/// </summary>
		/// <param name="solutionDirectory"></param>
		/// <returns></returns>
		public async Task StarAllAsync(string solutionDirectory)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(TITLE_ASCII);
			Console.WriteLine("-----------------------------------------------------");
			Console.Write("Searching packages... ");

			GrouppedPackages = await GetAllPackages(solutionDirectory);

			Console.WriteLine($"{GrouppedPackages.All.Count()} found");

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"These packages({GrouppedPackages.WithGithub.Count()}) have github url:{Environment.NewLine}");
			foreach (var package in GrouppedPackages.WithGithub)
			{
				Console.Write(await StarPackage(package) ? "* " : "x ");

				Console.WriteLine($"{package.Metadata.Title} {package.Version} - {package.Metadata.GithubUrl}");
			}

			Console.WriteLine("-----------------------------------------------------");
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"We found github url for these packages({GrouppedPackages.WithParsedGithub.Count()}) on their project website:{Environment.NewLine}");
			foreach (var package in GrouppedPackages.WithParsedGithub)
			{
				Console.Write(await StarPackage(package) ? "* " : "x ");

				Console.WriteLine($"{package.Metadata.Title} {package.Version} - {package.Metadata.GithubUrl} - {package.Metadata.ProjectUrl}");
			}

			Console.WriteLine("-----------------------------------------------------");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"We couldn't find github url for these packages({GrouppedPackages.WithoutGithub.Count()}):{Environment.NewLine}");
			foreach (var package in GrouppedPackages.WithoutGithub)
			{
				Console.WriteLine($"{package.Metadata.Title} {package.Version} - {package.Metadata.ProjectUrl}");
			}

			Console.WriteLine("-----------------------------------------------------");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"We couldn't find any information about these packages({GrouppedPackages.WithNoMetadata.Count()}):{Environment.NewLine}");
			foreach (var package in GrouppedPackages.WithNoMetadata)
			{
				Console.WriteLine($"{package.Id} {package.Version}");
			}
		}

		private void WriteInfo(IEnumerable<PackageReference> packages, string title, ConsoleColor consoleColor = ConsoleColor.White)
		{
		}
	}
}