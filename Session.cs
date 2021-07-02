using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpfApp
{
    public class Session
    {
        private ObservableCollection<Worker> _workers;

        public ObservableCollection<Worker> Workers => _workers;

        public int _studentsHoursCost = 7;

        public int StudentsHoursCost
        {
            get => _studentsHoursCost;
            set => _studentsHoursCost = value;
        }

        public Session()
        {
            _workers = new ObservableCollection<Worker>();
        }

        public void Save(string savePath)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter streamWriter = new StreamWriter(savePath))
            {
                using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jsonWriter, Workers);
                }
            }
        }

        public void Load(string savePath)
        {
            if (File.Exists(savePath) == false)
                return;

            string json = File.ReadAllText(savePath);
            List<Worker> workers = JsonConvert.DeserializeObject<List<Worker>>(json);

            foreach (Worker worker in workers)
            {
                _workers.Add(worker);
            }
        }
    }
}
