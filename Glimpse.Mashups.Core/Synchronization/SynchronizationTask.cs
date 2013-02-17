using System;

namespace Glimpse.Mashups.Core.Synchronization
{
    [Serializable]
    public class SynchronizationTask
    {
        private readonly Guid _id;

        public SynchronizationTask(string code, DateTime shouldRunAfter)
        {
            _id = Guid.NewGuid();
            Code = code;
            State = SynchronizationTaskState.Pending;
            ShouldRunAfter = shouldRunAfter;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public String Code { get; private set; }
        public SynchronizationTaskState State { get; private set; }
        public DateTime ShouldRunAfter { get; private set; }
        public DateTime HasRunOn { get; private set; }
        public String Error { get; private set; }

        public void MarkSuccess()
        {
            if (State != SynchronizationTaskState.Pending)
                throw new InvalidOperationException(string.Format("Invalid state transition: {0} => {1}, must be {2} => {1}", State, SynchronizationTaskState.Success, SynchronizationTaskState.Pending));

            State = SynchronizationTaskState.Success;
            HasRunOn = DateTime.Now;
        }

        public void MarkFailed(Exception exc)
        {
            if (State != SynchronizationTaskState.Pending)
                throw new InvalidOperationException(string.Format("Invalid state transition: {0} => {1}, must be {2} => {1}", State, SynchronizationTaskState.Failed, SynchronizationTaskState.Pending));

            State = SynchronizationTaskState.Failed;
            HasRunOn = DateTime.Now;
            Error = exc.Message + Environment.NewLine + exc.StackTrace;
        }
    }
}