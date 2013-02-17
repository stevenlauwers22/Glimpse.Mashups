using System;

namespace Glimpse.Mashups.Core.Synchronization
{
    public class SynchronizationTaskInfo
    {
        private readonly Guid _id;
        private readonly string _code;
        private readonly SynchronizationTaskState _state;

        public SynchronizationTaskInfo(Guid id, string code, SynchronizationTaskState state)
        {
            _id = id;
            _code = code;
            _state = state;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public String Code
        {
            get { return _code; }
        }

        public SynchronizationTaskState State
        {
            get { return _state; }
        }
    }
}