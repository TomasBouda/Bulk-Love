using System.Collections.Generic;
using System.IO;
using System.Linq;
using ThanksNET.Core.Nuget;

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

			return references.Distinct();
		}
	}
}