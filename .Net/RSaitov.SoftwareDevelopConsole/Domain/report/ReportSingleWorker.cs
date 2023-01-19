using RSaitov.SoftwareDevelop.Data;
using System;
using System.Collections.Generic;

namespace RSaitov.SoftwareDevelop.Domain
{
    public class ReportSingleWorker
    {
        public IWorker Worker { get;  }
        public IEnumerable<TimeRecord> TimeRecords { get;  }
        public decimal Salary { get; }
        public int Hours { get; }
        public DateTime Start { get; }
        public DateTime End { get; }

        public ReportSingleWorker(IWorker worker, IEnumerable<TimeRecord> timeRecords, decimal salary, int hours,
            DateTime start, DateTime end)
        {
            Worker = worker;
            TimeRecords = timeRecords;
            Salary = salary;
            Hours = hours;
            Start = start;
            End = end;
        }
    }
}
