using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PrisninjaDatabaseAPI
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "EAN")]
        public string EAN { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public float Price { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
