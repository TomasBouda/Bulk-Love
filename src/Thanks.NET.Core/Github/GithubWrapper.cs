using Octokit;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThanksNET.Core.Github
{
	public class GithubWrapper
	{
		internal const string RGX_GITHUB_REPO = @"http[s]?://github.com/(?<owner>(\w|\.|-)+)/(?<repoName>(\w|\.|-)+)";

		private GitHubClient GitHubClient { get; }

		public GithubWrapper(string token)
		{
			GitHubClient = new GitHubClient(new ProductHeaderValue("Thanks.NET"));
			GitHubClient.Credentials = new Credentials(token);
		}

		public async Task<bool> StarRepo(string owner, string repoName)
		{
			return await GitHubClient.Activity.Starring.StarRepo(owner, repoName);
		}

		public async Task<bool> StarRepo(string url)
		{
			var match = Regex.Match(url, RGX_GITHUB_REPO);
			if (match.Success)
			{
				return await GitHubClient.Activity.Starring.StarRepo(match.Groups["owner"].Value, match.Groups["repoName"].Value);
			}
			else
			{
				return false;
			}
		}
	}
}