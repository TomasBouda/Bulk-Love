using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksNET.Core.Nuget.Packages
{
	public class GrouppedPackages
	{
		public IEnumerable<PackageReference> All { get; }
		public IEnumerable<PackageReference> WithGithub { get; }
		public IEnumerable<PackageReference> WithParsedGithub { get; }
		public IEnumerable<PackageReference> WithoutGithub { get; }
		public IEnumerable<PackageReference> WithNoMetadata { get; }

		public GrouppedPackages(IEnumerable<PackageReference> all, IEnumerable<PackageReference> withGithub,
			IEnumerable<PackageReference> withParsedGithub, IEnumerable<PackageReference> withProjectUrl, IEnumerable<PackageReference> withNoMetadata)
		{
			All = all;
			WithGithub = withGithub;
			WithParsedGithub = withParsedGithub;
			WithoutGithub = withProjectUrl;
			WithNoMetadata = withNoMetadata;
		}

		public static async Task<GrouppedPackages> FromPackageRefAsync(IEnumerable<PackageReference> packageReferences)
		{
			var nuget = new NugetWrapper();
			var packages = await nuget.GetAllAsync(packageReferences);

			var packagesWithGithub = packages.Where(p => p.Metadata?.HasGithub == true);
			var packagesWithGithubFound = packages.Where(p => p.Metadata?.HasParsedGithub == true);
			var packagesWithoutGithub = packages.Where(p => p.Metadata?.HasGithub == false && p.Metadata?.HasParsedGithub == false);
			var packagesWithNoMetadata = packages.Where(p => p.Metadata == null);

			return new GrouppedPackages(packages, packagesWithGithub, packagesWithGithubFound, packagesWithoutGithub, packagesWithNoMetadata);
		}
	}
}