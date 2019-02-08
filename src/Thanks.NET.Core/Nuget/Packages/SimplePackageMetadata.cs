using NuGet.Protocol.Core.Types;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThanksNET.Core.Github;

namespace ThanksNET.Core.Nuget.Packages
{
	public class SimplePackageMetadata
	{
		public string Title { get; }
		public string Authors { get; }
		public string Tags { get; }
		public string Summary { get; }
		public Uri IconUrl { get; }
		public Uri ProjectUrl { get; }

		public string GithubUrl { get; private set; }
		public bool HasGithub { get; private set; }
		public bool HasParsedGithub { get; private set; }

		public SimplePackageMetadata(IPackageSearchMetadata packageSearchMetadata)
		{
			Title = packageSearchMetadata.Title;
			Authors = packageSearchMetadata.Authors;
			Tags = packageSearchMetadata.Tags;
			Summary = packageSearchMetadata.Summary;
			IconUrl = packageSearchMetadata.IconUrl;
			ProjectUrl = packageSearchMetadata.ProjectUrl;

			if (ProjectUrl != null)
			{
				var match = Regex.Match(ProjectUrl.ToString(), GithubWrapper.RGX_GITHUB_REPO);
				if (match.Success)
				{
					HasGithub = true;
					GithubUrl = match.Value;
				}
			}
		}

		public async Task TryToFindGithubUrlAsync()
		{
			try
			{
				WebClient client = new WebClient();
				var webPage = await client.DownloadStringTaskAsync(ProjectUrl);
				var match = Regex.Match(webPage, GithubWrapper.RGX_GITHUB_REPO);
				if (match.Success)
				{
					HasParsedGithub = true;
					GithubUrl = match.Value;
				}
			}
			catch
			{
			}
		}
	}
}