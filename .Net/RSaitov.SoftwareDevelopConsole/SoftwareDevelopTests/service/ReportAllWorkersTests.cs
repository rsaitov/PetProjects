using NUnit.Framework;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using RSaitov.SoftwareDevelop.SoftwareDevelopTests;
using System;

namespace Test_Service
{
    public class ReportAllWorkersTests
    {
        private IService service = CommonActions.Service;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetReportAllWorkers_Success()
        {
            IWorker sender = CommonActions.GetFirstWorker(WorkerRole.Manager);
            var report = service.GetReportAllWorkers(sender, DateTime.Now.AddDays(-14).Date, DateTime.Now.Date);
            Assert.NotNull(report);
        }

        [Test]
        public void GetReportOtherWorkerNoAccess_Fail()
        {
            IWorker employee = CommonActions.GetFirstWorker(WorkerRole.Employee);
            IWorker freelancer = CommonActions.GetFirstWorker(WorkerRole.Freelancer);

            var report = service.GetReportSingleWorker(employee, freelancer,
                DateTime.Now.AddDays(-14).Date, DateTime.Now.Date);
            Assert.IsNull(report);
        }
    }
}
