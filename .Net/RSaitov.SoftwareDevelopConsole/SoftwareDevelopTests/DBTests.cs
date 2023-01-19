using NUnit.Framework;
using RSaitov.SoftwareDevelop.Data;
using RSaitov.SoftwareDevelop.Domain;
using RSaitov.SoftwareDevelop.SoftwareDevelopTests;
using System;
using System.Linq;

namespace Test_DB
{
    public class DBTests
    {
        private IRepository repository = CommonActions.Repository;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SelectNotExistedPersons_Success()
        {
            var user = repository.SelectWorker("No user");
            Assert.Null(user);
        }
        [Test]
        public void SelectExistedPerson_Success()
        {
            var users = repository.SelectWorkers();
            if (users.Count() == 0)
                Assert.Pass("No users in DB");
            var firstUser = users.First();
            var user = repository.SelectWorker(firstUser.Name);
            Assert.AreEqual(user.Name, firstUser.Name);
        }
        [Test]
        public void SelectAllPersons_Success()
        {
            var users = repository.SelectWorkers();
            foreach (var user in users)
                Console.WriteLine($"{user.Name}, {user.Role}");
            Assert.NotNull(users);
        }

        [Test]
        public void InsertRandomPerson_Success()
        {
            var worker = WorkerGenerator.CreateRandomWorker();
            var result = repository.InsertWorker(worker);
            Assert.IsTrue(result);
        }

        [Test]
        public void SelectTimeRecords_Success()
        {
            var timeRecordsManager = repository.SelectTimeRecords(WorkerRole.Manager);
            var timeRecordsEmployee = repository.SelectTimeRecords(WorkerRole.Employee);
            var timeRecordsFreelancer = repository.SelectTimeRecords(WorkerRole.Freelancer);

            Assert.NotNull(timeRecordsManager);
            Assert.NotNull(timeRecordsEmployee);
            Assert.NotNull(timeRecordsFreelancer);
        }
        [Test]
        public void InsertRandomTimeRecord_Success()
        {
            var rand = new Random();
            var workers = repository.SelectWorkers();
            var worker = repository.SelectWorkers().Skip(rand.Next(0, workers.Count() - 1)).First();
            var timeRecord = new TimeRecord(
                DateTime.Now.AddDays(-rand.Next(1,3)),
                worker.Name,
                (byte)rand.Next(1, 10),
                "sometext"
            );
            var result = repository.InsertTimeRecord(timeRecord, worker.Role);
            Assert.IsTrue(result);
        }
    }
}