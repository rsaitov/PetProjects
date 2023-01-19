using NUnit.Framework;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using RSaitov.SoftwareDevelop.SoftwareDevelopTests;
using System;

namespace Test_Service
{
    public class ReportSingleWorkerTests
    {
        private IService service = CommonActions.Service;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetReportSingleWorker_Success()
        {
            IWorker sender = CommonActions.GetFirstWorker(WorkerRole.Manager);

            var report = service.GetReportSingleWorker(sender, sender, DateTime.Now.AddDays(-14).Date, DateTime.Now.Date);
            Assert.NotNull(report);
        }

        [Test]
        public void GetReportSingleWorkerSenderNoAccess_Fail()
        {
            IWorker sender = CommonActions.GetFirstWorker(WorkerRole.Employee);
            IWorker worker = CommonActions.GetFirstWorker(WorkerRole.Manager);

            var report = service.GetReportSingleWorker(sender, worker, DateTime.Now.AddDays(-14).Date, DateTime.Now.Date);
            Assert.Null(report);
        }
    }
}
