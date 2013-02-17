using System;
using Glimpse.Mashups.Core.Synchronization;

namespace Glimpse.Mashups.Core.GitHub
{
    public class GitHubContributorsSynchronizer : ISynchronizer
    {
        private readonly IGitHubContributorsProvider _gitHubContributorsProvider;
        private readonly IContributorDataStore _contributorDataStore;

        public GitHubContributorsSynchronizer(IGitHubContributorsProvider gitHubContributorsProvider, IContributorDataStore contributorDataStore)
        {
            if (gitHubContributorsProvider == null)
                throw new ArgumentNullException("gitHubContributorsProvider");

            if (contributorDataStore == null)
                throw new ArgumentNullException("contributorDataStore");

            _gitHubContributorsProvider = gitHubContributorsProvider;
            _contributorDataStore = contributorDataStore;
        }

        public string Code 
        { 
            get { return "GitHub"; } 
        }

        public void Synchronize()
        {
            var gitHubContributors = _gitHubContributorsProvider.GetContributors();
            if (gitHubContributors == null)
                return;

            _contributorDataStore.SynchronizeWith(gitHubContributors);
        }
    }
}