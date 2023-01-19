using RSaitov.SoftwareDevelop.Data;

namespace RSaitov.SoftwareDevelop.Domain
{
    /*
     * Фабричный метод
     * Создание в памяти объекта сотрудника нужного типа в зависимости от передаваемой роли сотрудника WorkerRole
     */
    public static class WorkerFactory
    {
        public static IWorker GenerateWorker(string name, WorkerRole role)
        {
            switch (role)
            {
                case WorkerRole.Manager:
                    return new Manager(name);
                case WorkerRole.Employee:
                    return new Employee(name);
                case WorkerRole.Freelancer:
                    return new Freelancer(name);
                default:
                    return null;
            }
            
        }
    }
}
