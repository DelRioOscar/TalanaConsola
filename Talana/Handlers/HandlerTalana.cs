using Newtonsoft.Json;
using Talana.Enums;
using Talana.Helpers;
using Talana.Models;
using Talana.Response;
using Talana.Services;

namespace Talana.Handlers
{
    public static class HandlerTalana
    {
        private static string username = "";
        private static string password = "";

        private static readonly DateTime today = DateTime.Now;
        private static LoginResponse? response = null;

        public static void StartHandler()
        {
            VerificarArchivoJsonCreado();
            VerificarFeriado();
            VerificarDiaLaboral();
            Console.WriteLine("Iniciando sesión...");
            response = TalanaService.LoginCheckAsync(username, password);
            VerificarInicioSesion();
            MarcarAsistencia();
        }

        static void PedirCredentials()
        {
            Console.WriteLine("Ingrese su RUT (Ej. 123456789):");
            username = Console.ReadLine();
            Console.WriteLine("Ingrese su contraseña:");
            password = Console.ReadLine();

            var credencial = new Credencial
            {
                UserName = username,
                Password = password
            };

            GuardarCredencial(credencial);
        }

        static void GuardarCredencial(Credencial credencial)
        {
            Console.WriteLine("Guardando credenciales.");
            string json = JsonConvert.SerializeObject(credencial, Formatting.Indented);
            File.WriteAllText("credenciales.json", json);
            Console.WriteLine("Credenciales guardadas con éxito.");
        }

        static Credencial LeerCredencial()
        {
            Console.WriteLine("Leyendo credenciales.");
            string json = File.ReadAllText("credenciales.json");
            var credencial = JsonConvert.DeserializeObject<Credencial>(json);
            Console.WriteLine("Credenciales leídas con éxito.");
            return credencial;
        }

        static void VerificarUsuarioYPassword()
        {
            var credencial = LeerCredencial();
            if (credencial.UserName == null || credencial.Password == null)
            {
                Console.WriteLine("No se encontró el usuario o la contraseña.");
                PedirCredentials();
            }
            else
            {
                username = credencial.UserName;
                password = credencial.Password;
            }
        }

        static void VerificarArchivoJsonCreado()
        {
            Console.WriteLine("Verificando si existe el archivo credenciales.json...");
            var existe = File.Exists("credenciales.json");
            if (!existe)
            {
                PedirCredentials();
            }
            else
            {
               VerificarUsuarioYPassword();
            }
        }

        static void MarcarAsistencia()
        {
            if (today.Hour == 9)
            {
                MarcarEntrada();
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                if (today.Hour == 14)
                {
                    MarcarSalida();
                }
            }
            else if (today.Hour == 14)
            {
                MarcarEntradaAlmuerzo();
            }
            else if (today.Hour == 15)
            {
                MarcarSalidaAlmuerzo();
            }
            else if (today.Hour == 18 && today.Minute >= 45)
            {
                MarcarSalida();
            }
            else
            {
                Console.WriteLine("No es hora de marcar asistencia.");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }

        static void MarcarEntrada()
        {
            //var puedeMarcarEntrada = PuedeMarcarEntrada();
            //if (!puedeMarcarEntrada)
            //{
            //    Console.WriteLine("Ya se marcó la entrada.");
            //    Thread.Sleep(3000);
            //    Environment.Exit(0);
            //}

            Console.WriteLine("Marcando entrada...");
            var marcadoConExito = TalanaService.MarcarEntrada(response.Token);
            if (marcadoConExito)
            {
                Console.WriteLine("Entrada marcada con éxito");
            }
            else
            {
                Console.WriteLine("Error al marcar entrada");
            }
            Thread.Sleep(5000);
            Environment.Exit(0);
        }

        static void MarcarSalida()
        {
            //var puedeMarcarSalida = PuedeMarcarSalida();
            //if (!puedeMarcarSalida)
            //{
            //    Console.WriteLine("Ya se marcó la salida.");
            //    Thread.Sleep(3000);
            //    Environment.Exit(0);
            //}

            Console.WriteLine("Marcando salida...");
            var marcadoConExito = TalanaService.MarcarSalida(response.Token);
            if (marcadoConExito)
            {
                Console.WriteLine("Salida marcada con éxito");
                ApagarComputadora();
            }
            else
            {
                Console.WriteLine("Error al marcar salida");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
        }

        static void MarcarEntradaAlmuerzo()
        {
            //Console.WriteLine("Marcando entrada almuerzo...");
            //var marcadoConExito = TalanaService.MarcarEntradaAlmuerzo(response.Token);
            //if (marcadoConExito)
            //{
            //    Console.WriteLine("Entrada almuerzo marcada con éxito");
            //}
            //else
            //{
            //    Console.WriteLine("Error al marcar entrada almuerzo");
            //}
            //Thread.Sleep(5000);
            //Environment.Exit(0);
        }

        static void MarcarSalidaAlmuerzo()
        {
            //Console.WriteLine("Marcando salida almuerzo...");
            //var marcadoConExito = TalanaService.MarcarSalidaAlmuerzo(response.Token);
            //if (marcadoConExito)
            //{
            //    Console.WriteLine("Salida almuerzo marcada con éxito");
            //}
            //else
            //{
            //    Console.WriteLine("Error al marcar salida almuerzo");
            //}
            //Thread.Sleep(5000);
            //Environment.Exit(0);
        }

        static void ApagarComputadora()
        {
            Console.WriteLine("Se apagará la computadora en 60 segundos...");
            Console.WriteLine("Presiona cualquier tecla para cancelar el apagado.");

            int tiempoEspera = 60;
            while (tiempoEspera > 0)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    Console.WriteLine("Apagado cancelado.");
                    return;
                }

                Thread.Sleep(1000);
                tiempoEspera--;
            }
            ShutdownHelper.Shutdown();
        }

        static void VerificarDiaLaboral()
        {
            Console.WriteLine("Verificando si hoy es día laboral...");
            bool esDiaLaboral = today.DayOfWeek >= DayOfWeek.Monday && today.DayOfWeek <= DayOfWeek.Friday;
            if (!esDiaLaboral)
            {
                Console.WriteLine("Hoy no es día laboral.");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }

        static void VerificarInicioSesion()
        {
            if (response == null)
            {
                Console.WriteLine("No se pudo iniciar sesión en Talana...");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }

        static bool PuedeMarcarEntrada()
        {
            Console.WriteLine("Verificando si se puede marcar entrada...");
            return TalanaService.PuedeMarcar(response.Token, MarcaEnum.Entrada);
        }

        static bool PuedeMarcarSalida()
        {
            Console.WriteLine("Verificando si se puede marcar salida...");
            return TalanaService.PuedeMarcar(response.Token, MarcaEnum.Salida);
        }


        static void VerificarFeriado()
        {
            Console.WriteLine("Verificando si hoy es feriado...");
            var esFeriado = TalanaService.EsFeriadoHoy();
            if (esFeriado)
            {
                Console.WriteLine("Hoy es feriado, no se marcará asistencia.");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
    }
}
