using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Util.Converter
{
    public class CusLongConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ulong);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //return base.ReadJson(reader, objectType, existingValue, serializer);
            return reader.Value?.ToString() ?? "0";
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var longValue = value?.ToString() ?? "0";
            writer.WriteValue(longValue);
        }
    }
}
