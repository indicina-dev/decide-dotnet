using System;
using Newtonsoft.Json;

namespace IndicinaDecideLibrary
{
    public abstract class JsonSerializable
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

