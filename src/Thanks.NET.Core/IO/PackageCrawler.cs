using System.Collections.Generic;
using System.IO;
using System.Linq;
using ThanksNET.Core.Extensions;
using ThanksNET.Core.Nuget.Packages;

namespace ThanksNET.Core.IO
{
	public class PackageCrawler
	{
		public static IEnumerable<PackageReference> Crawl(string solutionDirectory)
		{
			var references = new List<PackageReference>();

			// Load packages from package.config
			references.AddRange(CrawlFiles(solutionDirectory, PackagesFileType.Config));
			// Load packages from proj files
			references.AddRange(CrawlFiles(solutionDirectory, PackagesFileType.Proj));

			return references.Distinct(new InlineComparer<PackageReference>((t1, t2) => t1.Id == t2.Id && t1.Version == t2.Version, r => r.GetHashCode()));
		}

		private static IEnumerable<PackageReference> CrawlFiles(string solutionDirectory, PackagesFileType fileType)
		{
			var references = new List<PackageReference>();
			var packageFiles = Directory.GetFiles(solutionDirectory, fileType == PackagesFileType.Config ? "packages.config" : "*.csproj", SearchOption.AllDirectories);

			foreach (var file in packageFiles)
			{
				var packageRefFile = new PackagesFile(file, fileType);
				references.AddRange(packageRefFile.PackageReferences);
			}

			return references;
		}
	}
}