using System;
using Newtonsoft.Json;
namespace UnityUploader.Services
{
    public class JSONSerializer
    {

        public static string Serialize(Object obj)
        {
           return JsonConvert.SerializeObject(obj);
        }

        public static Object Deserialize(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

    }
}
