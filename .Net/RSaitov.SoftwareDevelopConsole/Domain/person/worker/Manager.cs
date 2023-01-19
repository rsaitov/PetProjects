using RSaitov.SoftwareDevelop.Data;
using System.Collections.Generic;

namespace RSaitov.SoftwareDevelop.Domain
{
    public class Manager : WorkerDTO, IWorker
    {
        public decimal MonthSalary => 200000;
        public decimal MonthBonus => 20000;
        public Manager(string name) : base(name, WorkerRole.Manager)
        {            

        }
        public WorkerRole GetRole() => Role;
        public string GetName() => Name;

        public decimal GetSalary(IEnumerable<TimeRecord> timeRecords)
        {
            var payPerHour = MonthSalary / Settings.WorkingHoursPerMonth;
            var bonusPerDay = Settings.WorkingHoursPerDay * (MonthBonus / Settings.WorkingHoursPerMonth);
            var totalPay = 0m;

            foreach (var timeRecord in timeRecords)
            {
                if (timeRecord.Hours <= Settings.WorkingHoursPerDay)
                {
                    totalPay += timeRecord.Hours * payPerHour;
                }
                else
                {
                    totalPay += Settings.WorkingHoursPerDay * payPerHour + bonusPerDay;
                }
            }

            return totalPay;
        }
    }
}
