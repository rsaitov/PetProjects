using RSaitov.SoftwareDevelop.Data;
using System;

namespace RSaitov.SoftwareDevelop.Domain
{
    /*
     * Генератор сотрудников с рандомным именем и ролью
     */
    public static class WorkerGenerator
    {
        public static IWorker CreateRandomIWorker()
        {
            string[] names = new string[] { "Jack", "Jane", "Ivan", "Mike", "Arnold", "Peter", "Kate" };
            string[] secondNames = new string[] { "Jackson", "Hammet", "Bowie", "Garett", "Brown", "Black", "Kane" };
            int[] workerRoles = new int[] { 0, 1, 2 };
            var rnd = new Random();

            var name = $"{names[rnd.Next(names.Length)]} {secondNames[rnd.Next(secondNames.Length)]}";
            var workerRole = workerRoles[rnd.Next(workerRoles.Length)];

            return WorkerFactory.GenerateWorker(name, (WorkerRole)workerRole);
        }

        public static WorkerDTO CreateRandomWorker()
        {
            string[] names = new string[] { "Jack", "Jane", "Ivan", "Mike", "Arnold", "Peter", "Kate" };
            string[] secondNames = new string[] { "Jackson", "Hammet", "Bowie", "Garett", "Brown", "Black", "Kane" };
            int[] workerRoles = new int[] { 0, 1, 2 };
            var rnd = new Random();

            var name = $"{names[rnd.Next(names.Length)]} {secondNames[rnd.Next(secondNames.Length)]}";
            var workerRole = workerRoles[rnd.Next(workerRoles.Length)];

            return new WorkerDTO(name, (WorkerRole)workerRole);
        }
    }
}
