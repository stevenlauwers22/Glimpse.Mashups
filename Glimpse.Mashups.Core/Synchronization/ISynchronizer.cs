namespace Glimpse.Mashups.Core.Synchronization
{
    public interface ISynchronizer
    {
        string Code { get; }
        void Synchronize();
    }
}