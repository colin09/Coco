using Newtonsoft.Json;

namespace com.fs.common.Extension{
    public static class StringExt{


        public static string ToJson(this object obj){
            return JsonConvert.SerializeObject(obj);
        }
    }
}
