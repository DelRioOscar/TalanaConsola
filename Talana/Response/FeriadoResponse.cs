using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talana.Response
{
    public partial class FeriadoResponse
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("comentarios")]
        public string Comentarios { get; set; }

        [JsonProperty("fecha")]
        public DateTimeOffset Fecha { get; set; }

        [JsonProperty("irrenunciable")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Irrenunciable { get; set; }

        [JsonProperty("tipo")]
        public Tipo Tipo { get; set; }

        [JsonProperty("leyes", NullValueHandling = NullValueHandling.Ignore)]
        public Leye[] Leyes { get; set; }
    }

    public partial class Leye
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public enum Tipo { Civil, Religioso };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TipoConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class TipoConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Tipo) || t == typeof(Tipo?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Civil":
                    return Tipo.Civil;
                case "Religioso":
                    return Tipo.Religioso;
            }
            throw new Exception("Cannot unmarshal type Tipo");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Tipo)untypedValue;
            switch (value)
            {
                case Tipo.Civil:
                    serializer.Serialize(writer, "Civil");
                    return;
                case Tipo.Religioso:
                    serializer.Serialize(writer, "Religioso");
                    return;
            }
            throw new Exception("Cannot marshal type Tipo");
        }

        public static readonly TipoConverter Singleton = new TipoConverter();
    }
}
