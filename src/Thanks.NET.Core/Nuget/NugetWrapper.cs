using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThanksNET.Core.Logging;

namespace ThanksNET.Core.Nuget
{
	public class NugetWrapper
	{
		public async Task<IPackageSearchMetadata> GetAsync(string id, NuGetVersion version)
		{
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
	}
}