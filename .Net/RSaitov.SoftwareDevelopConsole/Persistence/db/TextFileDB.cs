using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RSaitov.SoftwareDevelop.Data
{
    public class TextFileDB : IRepository
    {
        private static string _fileFolder = $"{Environment.GetEnvironmentVariable("temp")}\\SoftwareDevelopFiles\\";
        private static string _personsFile = $"{_fileFolder}person.csv";
        private static string _managerHoursFile = $"{_fileFolder}manager_hours.csv";
        private static string _employeeHoursFile = $"{_fileFolder}employee_hours.csv";
        private static string _freelanceHoursFile = $"{_fileFolder}freelance_hours.csv";

        private static string[] _filesDB = new string[] { _personsFile, _managerHoursFile, _employeeHoursFile, _freelanceHoursFile };

        public TextFileDB()
        {
            CreateFolderIfNotExist(_fileFolder);
            CreateFilesIfNotExist();
        }
        private static void CreateFolderIfNotExist(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }
        private static void CreateFilesIfNotExist()
        {
            foreach (var file in _filesDB)
                CreateFileIfNotExist(file);
        }
        private static void CreateFileIfNotExist(string fileNameWithPath)
        {
            if (!File.Exists(fileNameWithPath))
                File.Create(fileNameWithPath).Close();
        }
        private string ChooseWorkerFileByWorkerRole(WorkerRole workerRole)
        {
            string fileName = "";
            switch (workerRole)
            {
                case WorkerRole.Manager:
                    fileName = _managerHoursFile;
                    break;
                case WorkerRole.Employee:
                    fileName = _employeeHoursFile;
                    break;
                case WorkerRole.Freelancer:
                    fileName = _freelanceHoursFile;
                    break;
            }
            return fileName;
        }

        public IEnumerable<WorkerDTO> SelectWorkers()
        {
            var list = new HashSet<WorkerDTO>();
            using (var file = File.OpenText(_personsFile))
            {
                string s = "";
                while ((s = file.ReadLine()) != null)
                {
                    var stringMas = s.Split(',');                    
                    list.Add(new WorkerDTO(stringMas[0], (WorkerRole)Int32.Parse(stringMas[1])));
                }
            }
            return list;
        }
        public bool InsertWorker(WorkerDTO worker)
        {
            var row = string.Join(",", worker.Name, (int)worker.Role);
            using (var sw = File.AppendText(_personsFile))
                sw.WriteLine(row);
            return true;
        }

        public WorkerDTO SelectWorker(string name)
        {
            var persons = SelectWorkers();
            return persons.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<TimeRecord> SelectTimeRecords(WorkerRole workerRole)
        {
            var list = new List<TimeRecord>();
            string fileName = ChooseWorkerFileByWorkerRole(workerRole);
            using (var file = File.OpenText(fileName))
            {
                string s = "";
                while ((s = file.ReadLine()) != null)
                {
                    var stringMas = s.Split(',');
                    var timeRecord = new TimeRecord(
                        Convert.ToDateTime(stringMas[0]),
                        stringMas[1].ToString(),
                        Convert.ToByte(stringMas[2]),
                        stringMas[3].ToString()
);
                    list.Add(timeRecord);
                }
            }
            return list;
        }

        public bool InsertTimeRecord(TimeRecord timeRecord, WorkerRole workerRole)
        {
            var row = string.Join(",",
                timeRecord.Date.ToShortDateString(),
                timeRecord.Name,
                timeRecord.Hours,
                timeRecord.Description
            );
            var fileName = ChooseWorkerFileByWorkerRole(workerRole);
            using (var sw = File.AppendText(fileName))
                sw.WriteLine(row);
            return true;
        }
    }
}
