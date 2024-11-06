using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialize
{
    internal class Character
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public Actor PlayedBy { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        public void ToJsonFile(string path)
        {
            //File.WriteAllText($"C:\\{this.FirstName}-{this.LastName}.json", $"C:\\{this.FirstName}-{this.LastName}.json");

            //using (StreamWriter writetext = new StreamWriter($"C:\\{this.FirstName}-{this.LastName}.txt"))
            //{
            //    writetext.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(this));
            //}

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }
        }
        public string ToCharacter(string path)
        {
            string result = "";
            //File.WriteAllText($"C:\\{this.FirstName}-{this.LastName}.json", $"C:\\{this.FirstName}-{this.LastName}.json");

            using (StreamReader readtext = new StreamReader(path))
            {
                result = readtext.ReadLine();
            }

            var test = JsonConvert.DeserializeObject<Character>(result);
            foreach (var character in this.GetType().GetProperties().ToList())
            {
                character.Name = test[character.Name];
            }
            return ;
        }
    }
}
