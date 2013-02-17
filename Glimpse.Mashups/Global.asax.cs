using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Glimpse.Mashups.Core.GitHub;
using Glimpse.Mashups.Core.Synchronization;

namespace Glimpse.Mashups
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            RouteTable.Routes.MapRoute(
                 "Default", // Route name
                 "{controller}/{action}/{id}", // URL with parameters
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                 new[] { "Glimpse.Mashups.Controllers" }
            );
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Create github contributors synchronizer
            IContributorDataStore contributorDataStore = new ContributorDataStoreInFile(AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\Contributors.bin");
            IGitHubContributorsJsonProvider gitHubContributorsJsonProvider = new GitHubContributorsJsonProvider();
            IGitHubContributorsProvider gitHubContributorsProvider = new GitHubContributorsProvider(gitHubContributorsJsonProvider);
            ISynchronizer gitHubContributorsSynchronizer = new GitHubContributorsSynchronizer(gitHubContributorsProvider, contributorDataStore);

            // Create stack overflow faq synchronizer
            // TODO

            // Create nuget package synchronizer
            // TODO

            // Register all the synchronizers and run the synchronization agent
            ISynchronizationTaskDataStore synchronizationTaskDataStore = new SynchronizationTaskDataStoreInFile(AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\SynchronizationTasks.bin");
            ISynchronizationAgent synchronizationAgent = new SynchronizationAgent(synchronizationTaskDataStore);
            synchronizationAgent.Register(gitHubContributorsSynchronizer);
            synchronizationAgent.Run();
        }
    }
}