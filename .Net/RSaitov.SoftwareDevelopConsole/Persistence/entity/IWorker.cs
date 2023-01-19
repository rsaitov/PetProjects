using System.Collections.Generic;

namespace RSaitov.SoftwareDevelop.Data
{
    /// <summary>
    /// Интерфейс, описывающий работающего сотрудника
    /// </summary>
    public interface IWorker
    {
        string GetName();
        WorkerRole GetRole();
        decimal GetSalary(IEnumerable<TimeRecord> timeRecords);
    }
}
