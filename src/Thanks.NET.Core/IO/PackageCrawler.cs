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
			var packageFiles = Directory.GetFiles(solutionDirectory, "packages.config", SearchOption.AllDirectories);
			var references = new List<PackageReference>();

			foreach (var file in packageFiles)
			{
				var packageRefFile = new PackagesFile(file);
				references.AddRange(packageRefFile.PackageReferences);
			}

			return references.Distinct(new InlineComparer<PackageReference>((t1, t2) => t1.Id == t2.Id && t1.Version == t2.Version, r => r.GetHashCode()));
		}
	}
}