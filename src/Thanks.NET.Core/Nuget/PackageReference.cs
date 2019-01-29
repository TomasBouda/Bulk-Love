namespace ThanksNET.Core.Nuget
{
	public class PackageReference
	{
		public string Id { get; }
		public string Version { get; }

		public PackageReference(string id, string version)
		{
			Id = id;
			Version = version;
		}
	}
}