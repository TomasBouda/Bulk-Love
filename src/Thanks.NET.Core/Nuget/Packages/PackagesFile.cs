using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ThanksNET.Core.Nuget.Packages
{
	public enum PackagesFileType
	{
		Config,
		Proj
	}

	public class PackagesFile
	{
		public string Path { get; }

		public PackagesFileType Type { get; }

		public IList<PackageReference> PackageReferences { get; } = new List<PackageReference>();

		public PackagesFile(string path, PackagesFileType type)
		{
			Path = path;
			Type = type;
			LoadPackageReferences();
		}

		private void LoadPackageReferences()
		{
			switch (Type)
			{
				case PackagesFileType.Config:
					foreach (XElement packageElement in XElement.Load(Path).Elements("package"))
					{
						PackageReferences.Add(new PackageReference(packageElement.Attribute("id").Value, packageElement.Attribute("version").Value));
					}
					break;

				case PackagesFileType.Proj:
					foreach (XElement packageElement in XElement.Load(Path).XPathSelectElements("//ItemGroup/PackageReference"))
					{
						PackageReferences.Add(new PackageReference(packageElement.Attribute("Include").Value, packageElement.Attribute("Version").Value));
					}
					break;

				default:
					break;
			}
		}
	}
}