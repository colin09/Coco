using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Utility
{
    public class EnumResult
    {
        public EnumResult() { }
        public EnumResult(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

       // [JsonProperty("name")]
        public string Name { get; set; }

       // [JsonProperty("value")]
        public int Value { get; set; }
    }
    public class EnumConvert
    {
        private static ConcurrentDictionary<Type, EnumResult[]> typeEnumResultsCache = new ConcurrentDictionary<Type, EnumResult[]>();
        private static ConcurrentDictionary<Enum, EnumResult> valueEnumResultCache = new ConcurrentDictionary<Enum, EnumResult>();
        public static EnumResult[] ToEnumResults(Type enumType)
        {
            EnumResult[] enumResults;
            if (typeEnumResultsCache.ContainsKey(enumType))
            {
                if (typeEnumResultsCache.TryGetValue(enumType, out enumResults))
                    return enumResults;
            }

            enumResults = enumType.GetEnumValues().OfType<Enum>().Select(x => ToEnumResult(x)).OrderBy(x => x.Value).ToArray();
            typeEnumResultsCache.TryAdd(enumType, enumResults);

            return enumResults;
        }

        public static string GetDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static EnumResult ToEnumResult(Enum value)
        {
            EnumResult result;
            if (valueEnumResultCache.ContainsKey(value))
            {
                if (valueEnumResultCache.TryGetValue(value, out result))
                    return result;
            }

            result = new EnumResult(GetDescription(value), Convert.ToInt32(value));
            valueEnumResultCache.TryAdd(value, result);
            return result;
        }
    }
}
