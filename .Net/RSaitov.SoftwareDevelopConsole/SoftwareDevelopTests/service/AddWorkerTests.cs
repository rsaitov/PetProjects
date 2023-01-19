using NUnit.Framework;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using RSaitov.SoftwareDevelop.SoftwareDevelopTests;
using System.Linq;

namespace Test_Service
{
    public class AddWorkerTests
    {
        private IService service = CommonActions.Service;
        [SetUp]
        public void Setup()
        {
        }

        private IWorker GetRandomWorkerNotExisted()
        {
            while (true)
            {
                var worker = WorkerGenerator.CreateRandomIWorker();
                var userInDb = service.GetWorker(worker.GetName());
                if (ReferenceEquals(null, userInDb))
                    return worker;
            }
        }

        [Test]
        public void AddNotExistedWorker_Success()
        {
            IWorker worker = GetRandomWorkerNotExisted();
            var result = service.AddWorker(CommonActions.GetFirstWorker(WorkerRole.Manager), worker);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void AddExistedWorker_Fail()
        {
            var workers = service.GetWorkers();
            if (workers.Count() == 0)
                Assert.Pass("No workers in DB");

            var result = service.AddWorker(CommonActions.GetFirstWorker(WorkerRole.Manager), workers.First());
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Сотрудник с таким именем уже существует", result.Message);
        }

        [Test]
        public void AddWorkerSenderNoAccess_Fail()
        {
            IWorker worker = GetRandomWorkerNotExisted();
            var result = service.AddWorker(CommonActions.GetFirstWorker(WorkerRole.Employee), worker);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Нет доступа к созданию сотрудников", result.Message);
        }
    }
}
