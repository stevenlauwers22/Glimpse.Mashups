using System.Net;

namespace Glimpse.Mashups.Core.GitHub
{
    public class GitHubContributorsJsonProvider : IGitHubContributorsJsonProvider
    {
        public string GetContributorsJson()
        {
            const string url = "https://api.github.com/repos/glimpse/glimpse/contributors";
            using (var webClient = new WebClient())
            {
                webClient.Credentials = CredentialCache.DefaultCredentials;

                var json = webClient.DownloadString(url);
                return json;
            }
        }
    }
}