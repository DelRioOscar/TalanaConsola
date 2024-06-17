using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talana.Response
{
    public class MarkResponse
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("marks")]
        public Mark[] Marks { get; set; }
    }

    public class Mark
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }

        [JsonProperty("office")]
        public long Office { get; set; }

        [JsonProperty("photo")]
        public object Photo { get; set; }

        [JsonProperty("TS")]
        public DateTimeOffset Ts { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; }

        [JsonProperty("lat")]
        public long Lat { get; set; }

        [JsonProperty("lng")]
        public long Lng { get; set; }

        [JsonProperty("sourceMark")]
        public string SourceMark { get; set; }

        [JsonProperty("phoneModel")]
        public object PhoneModel { get; set; }

        [JsonProperty("staticmap")]
        public object Staticmap { get; set; }
    }

    public class Person
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("rut")]
        public string Rut { get; set; }

        [JsonProperty("apellidoPaterno")]
        public string ApellidoPaterno { get; set; }

        [JsonProperty("apellidoMaterno")]
        public string ApellidoMaterno { get; set; }

        [JsonProperty("sexo")]
        public string Sexo { get; set; }
    }
}
