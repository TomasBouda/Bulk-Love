using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BulkLove.Core.Nuget
{
	public class NugetWrapper
	{
		public async Task<IPackageSearchMetadata> GetAsync(string id, NuGetVersion version)
		{
			Logger logger = new Logger();
			List<Lazy<INuGetResourceProvider>> providers = new List<Lazy<INuGetResourceProvider>>();
			providers.AddRange(Repository.Provider.GetCoreV3());  // Add v3 API support
			PackageSource packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
			SourceRepository sourceRepository = new SourceRepository(packageSource, providers);
			PackageMetadataResource packageMetadataResource = await sourceRepository.GetResourceAsync<PackageMetadataResource>();

			var searchMetadata = await packageMetadataResource.GetMetadataAsync(id, false, false, new Logger(), CancellationToken.None);
			return searchMetadata.FirstOrDefault(p => p.Identity.Version == version);
		}

		public async Task<IPackageSearchMetadata> GetAsync(string id, string version)
		{
			return await GetAsync(id, NuGetVersion.Parse(version));
		}

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

			//public void Log(ILogMessage message)
			//{
			//	throw new NotImplementedException();
			//}

			//public Task LogAsync(ILogMessage message)
			//{
			//	throw new NotImplementedException();
			//}
		}
	}
}