namespace Glimpse.Mashups.Core.Synchronization
{
    public interface ISynchronizationAgent
    {
        void Register(ISynchronizer synchronizer);
        void Run();
    }
}