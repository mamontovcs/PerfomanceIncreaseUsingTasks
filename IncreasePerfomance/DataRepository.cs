using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace IncreasePerfomance
{
    class DataRepository
    {
        public void Serialize(object o, int index)
        {
            string output = JsonConvert.SerializeObject(o);
            File.WriteAllText($"File_{index}", output);
        }

        public List<int> Deserialize(int index)
        {
            var list = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText($"File_{index}"));
            return list;
        }
    }
}
