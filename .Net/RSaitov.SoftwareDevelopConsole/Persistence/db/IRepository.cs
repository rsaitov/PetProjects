using System.Collections.Generic;

namespace RSaitov.SoftwareDevelop.Data
{
    public interface IRepository
    {
        WorkerDTO SelectWorker(string name);
        IEnumerable<WorkerDTO> SelectWorkers();
        bool InsertWorker(WorkerDTO worker);

        IEnumerable<TimeRecord> SelectTimeRecords(WorkerRole workerRole);
        bool InsertTimeRecord(TimeRecord timeRecord, WorkerRole workerRole);
    }
}
