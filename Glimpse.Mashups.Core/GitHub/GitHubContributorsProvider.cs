using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Glimpse.Mashups.Core.GitHub
{
    public class GitHubContributorsProvider : IGitHubContributorsProvider
    {
        private readonly IGitHubContributorsJsonProvider _gitHubContributorsJsonProvider;

        public GitHubContributorsProvider(IGitHubContributorsJsonProvider gitHubContributorsJsonProvider)
        {
            if (gitHubContributorsJsonProvider == null)
                throw new ArgumentNullException("gitHubContributorsJsonProvider");

            _gitHubContributorsJsonProvider = gitHubContributorsJsonProvider;
        }

        public IEnumerable<GitHubContributor> GetContributors()
        {
            var json = _gitHubContributorsJsonProvider.GetContributorsJson();
            var contributors = JsonConvert.DeserializeObject<List<GitHubContributor>>(json);
            return contributors;
        }
    }
}