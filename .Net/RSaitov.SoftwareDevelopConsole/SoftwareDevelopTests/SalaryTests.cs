using NUnit.Framework;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using RSaitov.SoftwareDevelop.SoftwareDevelopTests;

/*
 * Обязательно свой инстанс БД, чтобы часы работы сотрудников остались неизменными
 */

namespace Test_Salary
{
    public class SalaryTests
    {
        private IRepository repository;
        private IService service;

        [SetUp]
        public void Setup()
        {
            repository = new MockRepository();
            service = new Service(repository);
        }
        
        [Test]
        public void Manager_Success()
        {
            IWorker worker = CommonActions.GetFirstWorker(WorkerRole.Manager);
            var timeRecords = service.GetTimeRecords(worker);
            var salary = worker.GetSalary(timeRecords);
            Assert.AreEqual(37250, salary);
        }
        
        [Test]
        public void Employee_Success()
        {
            IWorker worker = CommonActions.GetFirstWorker(WorkerRole.Employee);
            var timeRecords = service.GetTimeRecords(worker);
            var salary = worker.GetSalary(timeRecords);
            Assert.AreEqual(34500, salary);
        }
        [Test]
        public void Freelancer_Success()
        {
            IWorker worker = CommonActions.GetFirstWorker(WorkerRole.Freelancer);
            var timeRecords = service.GetTimeRecords(worker);
            var salary = worker.GetSalary(timeRecords);
            Assert.AreEqual(26000, salary);
        }
    }
}
