using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSaitov.SoftwareDevelop.SoftwareDevelopTests
{
    static class CommonActions
    {
        public static IService Service { get; set; }
        public static IRepository Repository { get; set; }
        static CommonActions()
        {
            //repository = new TextFileDB();
            Repository = new MockRepository();
            Service = new Service(Repository);
        }

        public static IWorker GetFirstWorker(WorkerRole workerRole) => Service.GetWorkers().FirstOrDefault(x => x.GetRole() == workerRole);
    }
}
