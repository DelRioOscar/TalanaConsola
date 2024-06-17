using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using Talana.Response;
using System.Reflection;
using System;
using System.Reflection.Metadata;
using Newtonsoft.Json.Linq;
using Talana.Enums;

namespace Talana.Services
{
    public static class TalanaService
    {
        public static LoginResponse LoginCheckAsync(string username, string password)
        {
            try
            {
                var url = "https://talana.com/es/api/login/check-login";

                var requestContent = new
                {
                    username,
                    password,
                    company = false,
                    identifier = false,
                    zendesk_params = new { }
                };

                var jsonRequest = JsonConvert.SerializeObject(requestContent);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var httpClient = new HttpClient();

                // Configuración de headers como se especifica en el comando curl
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"124\", \"Microsoft Edge\";v=\"124\", \"Not-A.Brand\";v=\"99\"");
                httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                httpClient.DefaultRequestHeaders.Add("X-CSRFToken", "V4bgrFBor4hc3KU6yEOpFZYUdW3LzIfPrRcIwKZ0WgZ77HzVom0r4Hhrot5i4VSd");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0");
                httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                httpClient.DefaultRequestHeaders.Add("host", "talana.com");
                httpClient.DefaultRequestHeaders.Add("Cookie", "sessionid=xluuw99u28ut55n1nn08lvuuokh3w80g");

                var response = httpClient.PostAsync(url, content).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;

                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                return loginResponse;
            }
            catch
            {
                return null;
            }
        }

        public static bool MarcarEntrada(string token)
        {
            var url = "https://api.talana.com/v1/validateMark/getCreateMarkMobile/";

            var requestData = new
            {
                direction = "E",
                photo = "",
                sucursal = "",
                coordenadas = new { lat = 0, lng = 0 },
                dispositivo = "Portal del trabajador",
                sourceMark = "desktop",
                uuid = "",
                manufacturer = "",
                model = "",
                TS = ""
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"124\", \"Microsoft Edge\";v=\"124\", \"Not-A.Brand\";v=\"99\"");
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {token}");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0");
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            httpClient.DefaultRequestHeaders.Add("host", "api.talana.com");

            var response = httpClient.PostAsync(url, content).Result;
            return response.IsSuccessStatusCode;
        }

        public static bool MarcarSalida(string token)
        {
            var url = "https://api.talana.com/v1/validateMark/getCreateMarkMobile/";

            var requestData = new
            {
                direction = "X",
                photo = "",
                sucursal = "",
                coordenadas = new { lat = 0, lng = 0 },
                dispositivo = "Portal del trabajador",
                sourceMark = "desktop",
                uuid = "",
                manufacturer = "",
                model = "",
                TS = ""
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"124\", \"Microsoft Edge\";v=\"124\", \"Not-A.Brand\";v=\"99\"");
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {token}");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0");
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            httpClient.DefaultRequestHeaders.Add("host", "api.talana.com");

            var response = httpClient.PostAsync(url, content).Result;
            return response.IsSuccessStatusCode;
        }

        public static bool PuedeMarcar(string token, MarcaEnum marca)
        {
            string tipoMarca = EnumExtensions.GetDescription(marca);

            var hoy = DateTime.Now;
            var url = $"https://talana.com/api/v2/asistencia/marks/get-by-day/?year={hoy.Year}&month={hoy.Month}";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"124\", \"Microsoft Edge\";v=\"124\", \"Not-A.Brand\";v=\"99\"");
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {token}");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 Edg/124.0.0.0");
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            httpClient.DefaultRequestHeaders.Add("host", "api.talana.com");

            var response = httpClient.GetAsync(url).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var responseAsObject = JsonConvert.DeserializeObject<List<MarkResponse>>(responseContent);

            var marcaHoy = responseAsObject.FirstOrDefault(p => p.Date == hoy.Date);
            return !marcaHoy.Marks.Any(p => p.Direction == tipoMarca);
        }

        public static bool EsFeriadoHoy()
        {
            try
            {
                var url = "https://apis.digital.gob.cl/fl/feriados";
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.GetAsync(url).Result;
                if(response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    var responseAsObject = JsonConvert.DeserializeObject<List<FeriadoResponse>>(responseContent);
                    return responseAsObject.Any(p => p.Fecha.Date == DateTime.Now.Date);
                }

                return false;
               
            }
            catch
            {
                return false;
            }
        }
    }
}
