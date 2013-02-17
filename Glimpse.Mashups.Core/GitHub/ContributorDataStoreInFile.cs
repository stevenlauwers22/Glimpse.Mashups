using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Glimpse.Mashups.Core.GitHub
{
    public class ContributorDataStoreInFile : IContributorDataStore
    {
        private readonly string _file;

        public ContributorDataStoreInFile(string file)
        {
            if (string.IsNullOrEmpty(file)) 
                throw new ArgumentNullException("file");

            _file = file;
        }

        public IEnumerable<Contributor> GetAll()
        {
            using (var stream = new FileStream(_file, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                if (stream.Length == 0)
                    return new List<Contributor>();

                var serializer = new BinaryFormatter();
                var contributors = (List<Contributor>)serializer.Deserialize(stream);
                return contributors;
            }
        }

        public void SynchronizeWith(IEnumerable<GitHubContributor> gitHubContributors)
        {
            if (gitHubContributors == null)
                return;

            var contributors = new List<Contributor>(GetAll());
            foreach (var gitHubContributor in gitHubContributors)
            {
                var contributor = contributors.SingleOrDefault(c => c.GitHubId == gitHubContributor.Id);
                if (contributor == null)
                {
                    contributor = new Contributor(gitHubContributor);
                    contributors.Add(contributor);
                }
                else
                {
                    contributor.SynchronizeWith(gitHubContributor);
                }
            }

            SaveAll(contributors);
        }

        private void SaveAll(List<Contributor> contributors)
        {
            using (var stream = new FileStream(_file, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, contributors);
            }
        }
    }
}