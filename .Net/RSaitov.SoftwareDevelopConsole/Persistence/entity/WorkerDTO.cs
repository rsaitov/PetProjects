namespace RSaitov.SoftwareDevelop.Data
{
    public class WorkerDTO : Person
    {
        public WorkerRole Role { get; set; }

        public WorkerDTO(string name, WorkerRole role) : base(name)
        {
            Role = role;
        }
    }
}
