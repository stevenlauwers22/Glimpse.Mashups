using System;

namespace Glimpse.Mashups.Core.Synchronization
{
    public interface ISynchronizationTaskDataStore
    {
        SynchronizationTaskInfo GetLast(string code);
        SynchronizationTaskInfo Schedule(string code, DateTime shouldRunAfter);
        void MarkSuccess(Guid id);
        void MarkFailed(Guid id, Exception exception);
    }
}