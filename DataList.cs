using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace WpfApp
{
    public class DataList<T>
    {
        public List<T> Items = new List<T>();

        public void Save(string savePath)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter streamWriter = new StreamWriter(savePath))
            {
                using(JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jsonWriter, Items);
                }
            }
        }

        public void Load(string savePath)
        {
            if (File.Exists(savePath) == false)
                return;

            string json = File.ReadAllText(savePath);
            Items = JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
