using System.Collections.Generic;
using System.Xml.Linq;

namespace ThanksNET.Core.Nuget
{
	public class PackagesFile
	{
		public string Path { get; }

		public IList<PackageReference> PackageReferences { get; } = new List<PackageReference>();

		public PackagesFile(string path)
		{
			Path = path;
			LoadPackageReferences();
		}

		private void LoadPackageReferences()
		{
			foreach (XElement packageElement in XElement.Load(Path).Elements("package"))
			{
				PackageReferences.Add(new PackageReference(packageElement.Attribute("id").Value, packageElement.Attribute("version").Value));
			}
		}
	}
}