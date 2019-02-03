using CommandLine;

namespace ThanksNET.ConsoleApp
{
	internal class ConsoleOptions
	{
		[Option('s', "solutiondir", Required = true, HelpText = "Sets solution directory from which all packages.config will be loaded.")]
		public string SolutionDirectory { get; set; }

		[Option('t', "githubtoken", Required = true, HelpText = "Sets github personal token. It will be used to authenticate repository \"staring\".")]
		public string GithubToken { get; set; }
	}
}