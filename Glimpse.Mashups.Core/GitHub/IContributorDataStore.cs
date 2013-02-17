using System.Collections.Generic;

namespace Glimpse.Mashups.Core.GitHub
{
    public interface IContributorDataStore
    {
        IEnumerable<Contributor> GetAll();
        void SynchronizeWith(IEnumerable<GitHubContributor> gitHubContributors);
    }
}