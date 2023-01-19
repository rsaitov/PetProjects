using System;
using System.Collections.Generic;
using System.Linq;

namespace RSaitov.SoftwareDevelop.Data
{
    public class MockRepository : IRepository
    {
        private List<WorkerDTO> _workers;
        private List<TimeRecord> _timeRecords = new List<TimeRecord>();

        public MockRepository()
        {
            _workers = new List<WorkerDTO>() { 
                new WorkerDTO("Alonso", WorkerRole.Manager),
                new WorkerDTO("Prost", WorkerRole.Employee),
                new WorkerDTO("Senna", WorkerRole.Freelancer),
            };
            _timeRecords = new List<TimeRecord>() { 
                //1250 * 6 = 7500
                //1250 * 8 + 1000  = 11000
                //1250 * 7 = 8750
                //1250 * 8 = 10000
                //total 37250

                new TimeRecord(DateTime.Now.AddDays(-1), "Alonso", 6, "lorem ipsum"),
                new TimeRecord(DateTime.Now.AddDays(-2), "Alonso", 14, "lorem ipsum"),
                new TimeRecord(DateTime.Now.AddDays(-3), "Alonso", 7, "lorem ipsum"),
                new TimeRecord(DateTime.Now.AddDays(-4), "Alonso", 8, "lorem ipsum"),

                //750 * 8 = 6000
                //750 * 8 + 4 * 1500 = 12000
                //750 * 8 + 2 * 1500 = 9000
                //750 * 8 + 1500 = 7500
                //total 34500
                new TimeRecord(DateTime.Now.AddDays(-1), "Prost", 8, "lorem ipsum"),
                new TimeRecord(DateTime.Now.AddDays(-2), "Prost", 12, "lorem ipsum"),
                new TimeRecord(DateTime.Now.AddDays(-3), "Prost", 10, "lorem ipsum"),
                new TimeRecord(DateTime.Now.AddDays(-4), "Prost", 9, "lorem ipsum"),

                //1000 * 7 = 7000
                //1000 * 8 = 8000
                //1000 * 9 = 9000
                //1000 * 2 = 2000
                //total 26000
                new TimeRecord(DateTime.Now.AddDays(-1), "Senna", 7, "lorem ipsum"),
                new TimeRecord(DateTime.Now.AddDays(-2), "Senna", 8, "lorem ipsum"),
                new TimeRecord(DateTime.Now.AddDays(-3), "Senna", 9, "lorem ipsum"),
                new TimeRecord(DateTime.Now.AddDays(-4), "Senna", 2, "lorem ipsum"),
            };
        }

        public bool InsertTimeRecord(TimeRecord timeRecord, WorkerRole workerRole)
        {
            _timeRecords.Add(timeRecord);
            return true;
        }

        public bool InsertWorker(WorkerDTO worker)
        {
            _workers.Add(worker);
            return true;
        }

        public IEnumerable<TimeRecord> SelectTimeRecords(WorkerRole workerRole)
        {
            return _timeRecords;            
        }

        public WorkerDTO SelectWorker(string name)
        {
            return _workers.FirstOrDefault(x => string.Equals(x.Name, name, 
                StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<WorkerDTO> SelectWorkers()
        {
            return _workers;
        }
    }
}
