using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThanksNET.Core.Logging;
using ThanksNET.Core.Nuget.Packages;

namespace ThanksNET.Core.Nuget
{
	public class NugetWrapper
	{
		private const string PACKAGE_SOURCE_URL = "https://api.nuget.org/v3/index.json";

		private Serilog.Core.Logger _logger;
		private NugetLogger _nugetLogger;
		private PackageMetadataResource _packageMetadataResource;

		private SourceRepository SourceRepository { get; }

		public NugetWrapper()
		{
			List<Lazy<INuGetResourceProvider>> providers = new List<Lazy<INuGetResourceProvider>>();
			providers.AddRange(Repository.Provider.GetCoreV3());  // Add v3 API support
			PackageSource packageSource = new PackageSource(PACKAGE_SOURCE_URL);
			SourceRepository = new SourceRepository(packageSource, providers);

			// Setup SeriLog
			_logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				//.WriteTo.Console()
				.CreateLogger();

			_nugetLogger = new NugetLogger(_logger);
		}

		private async Task<PackageMetadataResource> GetPackageMetadataResource()
		{
			if (_packageMetadataResource == null)
			{
				_packageMetadataResource = await SourceRepository.GetResourceAsync<PackageMetadataResource>();
			}

			return _packageMetadataResource;
		}

		public async Task<IPackageSearchMetadata> GetAsync(string id, NuGetVersion version)
		{
			PackageMetadataResource packageMetadataResource = await GetPackageMetadataResource();
			var searchMetadata = await packageMetadataResource.GetMetadataAsync(id, true, true, _nugetLogger, CancellationToken.None);

			return searchMetadata.FirstOrDefault(p => p.Identity.Version == version); ;
		}

		public async Task<IPackageSearchMetadata> GetAsync(string id, string version)
		{
			return await GetAsync(id, NuGetVersion.Parse(version));
		}

		public async Task<IEnumerable<PackageReference>> GetAllAsync(IEnumerable<PackageReference> packageReferences)
		{
			await Task.WhenAll(packageReferences.Select(p => p.LoadMetadataAsync(this)));

			return packageReferences;
		}
	}
}