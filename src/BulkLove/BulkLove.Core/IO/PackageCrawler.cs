using NuGet;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulkLove.Core.IO
{
	public class PackageCrawler
	{
		public static IEnumerable<PackageReference> Crawl(string solutionDirectory)
		{
			var packageFiles = Directory.GetFiles(solutionDirectory, "packages.config", SearchOption.AllDirectories);
			var references = new List<PackageReference>();

			foreach (var file in packageFiles)
			{
				var packageRefFile = new PackageReferenceFile(file);
				references.AddRange(packageRefFile.GetPackageReferences());
			}

			return references.Distinct();
		}
	}
}