using System;

namespace Glimpse.Mashups.Core.GitHub
{
    [Serializable]
    public class Contributor
    {
        private readonly Guid _id;

        private Contributor()
        {
        }

        public Contributor(GitHubContributor gitHubContributor)
            : this()
        {
            if (gitHubContributor == null)
                throw new ArgumentNullException("gitHubContributor");

            _id = Guid.NewGuid();
            GitHubId = gitHubContributor.Id;
            SynchronizeWith(gitHubContributor);
        }

        public Guid Id
        {
            get { return _id; }
        }

        public int GitHubId { get; private set; }
        public string Login { get; private set; }
        public string AvatarUrl { get; private set; }
        public int Contributions { get; private set; }

        public void SynchronizeWith(GitHubContributor gitHubContributor)
        {
            if (gitHubContributor == null)
                throw new ArgumentNullException("gitHubContributor");

            if (gitHubContributor.Id != GitHubId)
                throw new InvalidOperationException("The Id of the GitHubContributor does not match with the GitHubId of the Contributor");

            Login = gitHubContributor.Login;
            AvatarUrl = gitHubContributor.AvatarUrl;
            Contributions = gitHubContributor.Contributions;
        }
    }
}