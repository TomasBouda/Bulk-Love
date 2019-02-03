using NuGet.Versioning;
using System.Threading.Tasks;

namespace ThanksNET.Core.Nuget.Packages
{
	public class PackageReference
	{
		public string Id { get; }
		public string Version { get; }
		public NuGetVersion NuGetVersion { get; }

		public SimplePackageMetadata Metadata { get; private set; }

		public PackageReference(string id, string version)
		{
			Id = id;
			Version = version;
			NuGetVersion = NuGetVersion.Parse(Version);
		}

		public async Task<bool> LoadMetadataAsync(NugetWrapper nugetWrapper)
		{
			var searchMetadata = await nugetWrapper.GetAsync(Id, Version);
			Metadata = searchMetadata != null ? new SimplePackageMetadata(searchMetadata) : null;
			if (Metadata != null && Metadata.ProjectUrl != null && Metadata.HasGithub == false)
			{
				await Metadata.TryToFindGithubUrlAsync();
			}

			return Metadata != null;
		}

		public override string ToString()
		{
			return $"{Id}.{Version}";
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}
	}
}