using RSaitov.SoftwareDevelop.Data;
using System.Collections.Generic;
using System.Linq;

namespace RSaitov.SoftwareDevelop.Domain
{
    public class Freelancer : WorkerDTO, IWorker
    {
        public Freelancer(string name) : base(name, WorkerRole.Freelancer)
        {

        }

        public WorkerRole GetRole() => Role;
        public string GetName() => Name;

        public decimal GetSalary(IEnumerable<TimeRecord> timeRecords)
        {
            var hours = timeRecords.Sum(x => x.Hours);
            return hours * Settings.FreelancerSalaryPerHour;
        }
    }
}
