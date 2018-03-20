using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class JsonParser
    {
        public static IDictionary<string, object> GetDictionary(string json)
        {
            if (!(json.StartsWith("{") && json.EndsWith("}")))
                json = "{" + json + "}";

            var jsonObject = JObject.Parse(json);

            return GetDictionary(jsonObject);
        }

        private static IDictionary<string, object> GetDictionary(JToken json)
        {
            var jsonObject = json.Value<JObject>();

            return GetDictionary(jsonObject);
        }

        private static IDictionary<string, object> GetDictionary(JObject json)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var item in json)
            {
                var value = GetObject(item.Value);

                if (value != null)
                {
                    dictionary.Add(item.Key, value);
                }
            }

            return dictionary;
        }

        private static object GetObject(JToken jToken)
        {
            object obj = null;

            switch (jToken.Type)
            {
                case JTokenType.Integer:
                    obj = Convert.ToInt32(jToken.ToString());
                    break;
                case JTokenType.Float:
                    obj = Convert.ToDecimal(jToken.ToString());
                    break;
                case JTokenType.String:
                    obj = jToken.ToString();
                    break;
                case JTokenType.Boolean:
                    obj = Convert.ToBoolean(jToken.ToString());
                    break;
                case JTokenType.Date:
                    obj = Convert.ToDateTime(jToken.ToString());
                    break;
                case JTokenType.Array:
                    obj = GetArray(jToken);
                    break;
                case JTokenType.Object:
                    obj = GetDictionary(jToken);
                    break;
            }

            return obj;
        }

        private static IEnumerable<object> GetArray(JToken jToken)
        {
            var valueList = new List<object>();

            var jArray = JArray.Parse(jToken.ToString());

            foreach (var item in jArray)
            {
                var value = GetObject(item);
                valueList.Add(value);
            }

            return valueList;
        }

    }
}
