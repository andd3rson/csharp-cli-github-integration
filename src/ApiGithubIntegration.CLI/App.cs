using System.Text.Json;
using cli_api_github_integration.Interfaces;
using cli_api_github_integration.Resources;
using CommandLine;

namespace cli_api_github_integration
{
    public class App
    {
        private readonly IGithubServices _githubServices;

        public App(IGithubServices githubServices)
        {
            _githubServices = githubServices;
        }

        public async Task Run(string[] args)
        {
            await Parser
                .Default
                .ParseArguments<GithubRequestModel>(args)
                .WithParsedAsync(async name =>
                {
                    var userRepositories
                        = await GetReposAsync(name.UserName);
                    
                    // Console.WriteLine(JsonSerializer.Serialize(userRepositories));
                });
            
        }

        private async Task<Response<IEnumerable<GitHubResponse>>> GetReposAsync(string name)
        {
           return await _githubServices.GetRepos(name);
        }
    }
}