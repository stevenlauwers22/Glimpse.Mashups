using System.Collections.Generic;

namespace Glimpse.Mashups.Core.GitHub
{
    public interface IGitHubContributorsProvider
    {
        IEnumerable<GitHubContributor> GetContributors();
    }
}