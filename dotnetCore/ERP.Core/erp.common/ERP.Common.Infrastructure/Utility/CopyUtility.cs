using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Utility
{
    public class CopyUtility
    {
        public static T DeepCopy<T>(T model)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            string jsonString = JsonConvert.SerializeObject(model, setting);

            return JsonConvert.DeserializeObject<T>(jsonString, setting);
        }
    }
}
