using System.IO;

namespace Glimpse.Mashups.Core.GitHub
{
    public class GitHubContributorsJsonProviderFake : IGitHubContributorsJsonProvider
    {
        public string GetContributorsJson()
        {
            var assembly = GetType().Assembly;
            using (var stream = assembly.GetManifestResourceStream("GitHubContributorsJsonProviderFake.json"))
            {
                if (stream == null)
                    return null;

                using (var streamReader = new StreamReader(stream))
                {
                    var json = streamReader.ReadToEnd();
                    return json;
                }
            }
        }
    }
}