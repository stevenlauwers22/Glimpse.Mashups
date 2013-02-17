using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Glimpse.Mashups.Core.Synchronization
{
    public class SynchronizationTaskDataStoreInFile : ISynchronizationTaskDataStore
    {
        private readonly string _file;

        public SynchronizationTaskDataStoreInFile(string file)
        {
            if (string.IsNullOrEmpty(file)) 
                throw new ArgumentNullException("file");

            _file = file;
        }

        private IEnumerable<SynchronizationTask> GetAll()
        {
            using (var stream = new FileStream(_file, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                if (stream.Length == 0)
                    return new List<SynchronizationTask>();

                var serializer = new BinaryFormatter();
                var synchronizationTasks = (List<SynchronizationTask>)serializer.Deserialize(stream);
                return synchronizationTasks;
            }
        }

        public SynchronizationTaskInfo GetLast(string code)
        {
            var synchronizationTask = GetAll()
                .Where(t => t.ShouldRunAfter <= DateTime.Now)
                .OrderBy(t => t.ShouldRunAfter)
                .LastOrDefault();

            if (synchronizationTask == null)
                return null;

            var synchronizationTaskInfo = new SynchronizationTaskInfo(synchronizationTask.Id, synchronizationTask.Code, synchronizationTask.State);
            return synchronizationTaskInfo;
        }

        public SynchronizationTaskInfo Schedule(string code, DateTime shouldRunAfter)
        {
            var synchronizationTasks = new List<SynchronizationTask>(GetAll());
            var synchronizationTask = new SynchronizationTask(code, shouldRunAfter);
            synchronizationTasks.Add(synchronizationTask);
            SaveAll(synchronizationTasks);

            var synchronizationTaskInfo = new SynchronizationTaskInfo(synchronizationTask.Id, synchronizationTask.Code, synchronizationTask.State);
            return synchronizationTaskInfo;
        }

        public void MarkSuccess(Guid id)
        {
            var synchronizationTasks = new List<SynchronizationTask>(GetAll());
            var synchronizationTask = synchronizationTasks.SingleOrDefault(t => t.Id == id);
            if (synchronizationTask == null)
                return;

            synchronizationTask.MarkSuccess();
            SaveAll(synchronizationTasks);
        }

        public void MarkFailed(Guid id, Exception exception)
        {
            var synchronizationTasks = new List<SynchronizationTask>(GetAll());
            var synchronizationTask = synchronizationTasks.SingleOrDefault(t => t.Id == id);
            if (synchronizationTask == null)
                return;
            
            synchronizationTask.MarkFailed(exception);
            SaveAll(synchronizationTasks);
        }

        private void SaveAll(List<SynchronizationTask> synchronizationTasks)
        {
            using (var stream = new FileStream(_file, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, synchronizationTasks);
            }
        }
    }
}