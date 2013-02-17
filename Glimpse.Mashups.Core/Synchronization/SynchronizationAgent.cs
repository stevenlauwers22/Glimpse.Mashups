using System;
using System.Collections.Generic;

namespace Glimpse.Mashups.Core.Synchronization
{
    public class SynchronizationAgent : ISynchronizationAgent
    {
        private readonly ISynchronizationTaskDataStore _synchronizationTaskDataStore;
        private readonly IList<ISynchronizer> _synchronizers;

        public SynchronizationAgent(ISynchronizationTaskDataStore synchronizationTaskDataStore)
        {
            if (synchronizationTaskDataStore == null) 
                throw new ArgumentNullException("synchronizationTaskDataStore");

            _synchronizationTaskDataStore = synchronizationTaskDataStore;
            _synchronizers = new List<ISynchronizer>();
        }

        public void Register(ISynchronizer synchronizer)
        {
            if (synchronizer == null)
                return;

            _synchronizers.Add(synchronizer);
        }

        public void Run()
        {
            foreach (var synchronizer in _synchronizers)
            {
                var synchronizationTaskInfo =
                    _synchronizationTaskDataStore.GetLast(synchronizer.Code) ??
                    _synchronizationTaskDataStore.Schedule(synchronizer.Code, DateTime.Now);

                if (synchronizationTaskInfo.State != SynchronizationTaskState.Pending)
                    continue;

                try
                {
                    synchronizer.Synchronize();
                    _synchronizationTaskDataStore.MarkSuccess(synchronizationTaskInfo.Id);
                }
                catch (Exception exc)
                {
                    _synchronizationTaskDataStore.MarkFailed(synchronizationTaskInfo.Id, exc);
                }

                _synchronizationTaskDataStore.Schedule(synchronizer.Code, DateTime.Now.AddDays(1));
            }
        }
    }
}