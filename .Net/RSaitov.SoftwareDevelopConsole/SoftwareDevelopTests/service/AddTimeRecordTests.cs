using NUnit.Framework;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using RSaitov.SoftwareDevelop.SoftwareDevelopTests;
using System;
using System.Linq;

namespace Test_Service
{
    public class AddTimeRecordTests
    {
        private IService service = CommonActions.Service;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddTimeRecord_Success()
        {
            var roles = new[] { WorkerRole.Manager, WorkerRole.Employee, WorkerRole.Freelancer };

            foreach (var role in roles)
            {
                IWorker worker = CommonActions.GetFirstWorker(role);
                var rand = new Random();
                var timeRecord = new TimeRecord(
                    DateTime.Now.AddDays(-rand.Next(1, 2)),
                    worker.GetName(),
                    (byte)rand.Next(1, 10),
                    "created from Service test"
                );
                var result = service.AddTimeRecord(worker, timeRecord);
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void AddTimeRecord()
        {
            var roles = new[] { WorkerRole.Manager, WorkerRole.Employee, WorkerRole.Freelancer };
            var results = new bool[] {
                true, true,
                false, false,
                false, false
            };

            var counter = 0;
            foreach (var senderRole in roles)
                foreach (var role in roles.Where(x => x != senderRole))
                {
                    IWorker senderWorker = CommonActions.GetFirstWorker(senderRole);
                    IWorker worker = CommonActions.GetFirstWorker(role);

                    var rand = new Random();
                    var timeRecord = new TimeRecord(
                        DateTime.Now.AddDays(-rand.Next(1, 2)),
                        worker.GetName(),
                        (byte)rand.Next(1, 10),
                        "created from Service test"
                    );
                    var result = service.AddTimeRecord(senderWorker, timeRecord);
                    Assert.AreEqual(result, results[counter++]);
                }
        }
        [Test]
        public void AddTimeRecordOldDate()
        {
            var roles = new[] { WorkerRole.Manager, WorkerRole.Employee, WorkerRole.Freelancer };
            var results = new bool[] { true, true, false };

            var counter = 0;
            foreach (var role in roles)
            {
                IWorker worker = CommonActions.GetFirstWorker(role);

                var rand = new Random();
                var timeRecord = new TimeRecord(
                    DateTime.Now.AddDays(-rand.Next(5, 6)),
                    worker.GetName(),
                    (byte)rand.Next(1, 10),
                    "created from Service test"
                );
                var result = service.AddTimeRecord(worker, timeRecord);
                Assert.AreEqual(result, results[counter++]);
            }
        }
    }
}
