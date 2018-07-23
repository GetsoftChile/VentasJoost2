using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace crm_valvulas_industriales
{
    public class Comun
    {
        public static bool esHoraValida(string hora)
        {
            Regex r = new Regex(@"([0-1][0-9]|2[0-3]):[0-5][0-9]");
            Match m = r.Match(hora);
            return m.Success;
        }

        public static string Right(string param, int length)
        {
            int value = param.Length - length;
            string result = param.Substring(value, length);
            return result;
        }

        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        public String formatearRutSinPuntos(String rut)
        {
            int cont = 0;
            String format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");

                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;

                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "" + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }

        public String formatearRutConPuntos(String rut)
        {
            int cont = 0;
            String format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;

                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }

        public bool verificaRut(int rut, string dv)
        {
            int Digito;
            int Contador;
            int Multiplo;
            int Acumulador;
            string RutDigito;

            Contador = 2;
            Acumulador = 0;

            while (rut != 0)
            {
                Multiplo = (rut % 10) * Contador;
                Acumulador = Acumulador + Multiplo;
                rut = rut / 10;
                Contador = Contador + 1;

                if (Contador == 8)
                {
                    Contador = 2;
                }
            }

            Digito = 11 - (Acumulador % 11);
            RutDigito = Digito.ToString().Trim();
            if (Digito == 10)
            {
                RutDigito = "K";
            }
            if (Digito == 11)
            {
                RutDigito = "0";
            }

            if (RutDigito.ToString() == dv.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void enviarEmail(string usuario, string email, string asunto, string texto)
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            SmtpClient client = new SmtpClient();

            correo.From = new MailAddress("<alertas@dydcomchile.cl>");
            correo.To.Add(new MailAddress(email));

            correo.Subject = asunto;
            correo.IsBodyHtml = true;
            correo.Body = texto;

            client.Send(correo);
        }

        public bool validarRut(string rut)
        {
            bool validacion = false;
            try
            {
               int index = rut.IndexOf("-");
               if (index == -1)
               {
                   validacion = false;
                   return validacion;
               }
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }



    }
}