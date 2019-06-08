using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RecyclameV2.PAC
{
    internal class Parametro
    {
        internal string nombre;
        internal string valor;

        public Parametro(string nombre, string valor)
        {
            this.nombre = nombre;
            this.valor = valor;
        }
    }

    public class WSMFRespuesta
    {
        internal string soapResponse;
        internal string cfdi;
        internal string png;
        internal string idpac;
        internal string pac;
        internal string produccion;
        internal string codigo_mf_numero;
        internal string codigo_mf_texto;
        internal string mensaje_original_json;
        internal string cancelada;
        internal string saldo;
        internal string uuid;
        internal string servidor;
        internal string ejecucion;

        internal WSMFRespuesta(string soap)
        {
            soapResponse = soap;
        }

        public string SoapResponse
        {
            get { return soapResponse; }
        }

        public string CFDI
        {
            get { return cfdi; }
        }

        public string PNG
        {
            get { return png; }
        }

        public string IdPac
        {
            get { return idpac; }
        }

        public string Pac
        {
            get { return pac; }
        }

        public string Produccion
        {
            get { return produccion; }
        }

        public string CodigoMFNumero
        {
            get { return codigo_mf_numero; }
        }

        public string CodigoMFTexto
        {
            get { return codigo_mf_texto; }
        }

        public string MensajeOriginalJSON
        {
            get { return mensaje_original_json; }
        }

        public string Cancelada
        {
            get { return cancelada; }
        }

        public string Saldo
        {
            get { return saldo; }
        }

        public string UUID
        {
            get { return uuid; }
        }

        public string Servidor
        {
            get { return servidor; }
        }

        public string Ejecucion
        {
            get { return ejecucion; }
        }
    }

    public static class WSMultifacturas
    {
        static string url = "http://pac{0}.multifacturas.com/pac/?wsdl";
        static string[] nodos = new string[] { "cfdi", "png", "idpac", "produccion", "codigo_mf_numero", "codigo_mf_texto", "mensaje_original_pac_json", "cancelada", "saldo", "servidor", "ejecucion" };

        public static WSMFRespuesta Timbrar(string pac, string rfc, string clave, string xml, bool produccion)
        {
            Parametro[] parametros = new Parametro[] { new Parametro("rfc", rfc), new Parametro("clave", clave), new Parametro("xml", xml), new Parametro("produccion", (produccion) ? "SI" : "NO") };
            return ProcesaRespuesta(CallWebService(pac, "urn:wservicewsdl#timbrar", parametros));
        }

        public static WSMFRespuesta Timbrar64(string pac, string rfc, string clave, string xml, bool produccion)
        {
            Parametro[] parametros = new Parametro[] { new Parametro("rfc", rfc), new Parametro("clave", clave), new Parametro("xml", Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(xml))), new Parametro("produccion", (produccion) ? "SI" : "NO") };
            return ProcesaRespuesta(CallWebService(pac, "urn:wservicewsdl#timbrar64", parametros));
        }

        public static WSMFRespuesta Cancelar(string pac, string rfc, string clave, string uuid, string cer, string key, string pass_cer)
        {
            Parametro[] parametros = new Parametro[] { new Parametro("rfc", rfc), new Parametro("clave", clave), new Parametro("uuid", uuid), new Parametro("cer", FileToBase64(cer)), new Parametro("key", FileToBase64(key)), new Parametro("pass_cer", Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(pass_cer))) };
            return ProcesaRespuesta(CallWebService(pac, "urn:wservicewsdl#cancelar", parametros));
        }

        public static WSMFRespuesta Saldo(string pac, string rfc, string clave)
        {
            Parametro[] parametros = new Parametro[] { new Parametro("rfc", rfc), new Parametro("clave", clave) };
            return ProcesaRespuesta(CallWebService(pac, "urn:wservicewsdl#saldo", parametros));
        }

        /// <summary>
        /// Convierte un archivo en una cadena base64.
        /// </summary>
        /// <param name="file">Archivo a convertir</param>
        /// <see cref="http://stackoverflow.com/a/25919641"/>
        private static string FileToBase64(string file)
        {
            Byte[] bytes = File.ReadAllBytes(file);
            return Convert.ToBase64String(bytes);
        }

        private static WSMFRespuesta ProcesaRespuesta(XmlDocument soapResult)
        {
            WSMFRespuesta respuesta = new WSMFRespuesta(soapResult.OuterXml);
            XmlNodeList elems;
            foreach (string nodo in nodos)
            {
                elems = soapResult.GetElementsByTagName(nodo);
                if (elems.Count > 0)
                    switch (nodo)
                    {
                        case "cfdi":
                            //Encoding utf8 = Encoding.UTF8;
                            //byte[] utf = Encoding.UTF8.GetBytes(elems[0].InnerText);
                            //StringBuilder sb = new StringBuilder();
                            //foreach (byte bt in utf)
                            //    sb.Append((char)bt);
                            respuesta.cfdi = elems[0].InnerText;
                            //respuesta.cfdi = sb.ToString();
                            break;
                        case "png":
                            respuesta.png = elems[0].InnerText;
                            break;
                        case "idpac":
                            respuesta.idpac = elems[0].InnerText;
                            break;
                        case "pac":
                            respuesta.pac = elems[0].InnerText;
                            break;
                        case "produccion":
                            respuesta.produccion = elems[0].InnerText;
                            break;
                        case "codigo_mf_numero":
                            respuesta.codigo_mf_numero = elems[0].InnerText;
                            break;
                        case "codigo_mf_texto":
                            respuesta.codigo_mf_texto = elems[0].InnerText;
                            break;
                        case "mensaje_original_pac_json":
                            respuesta.mensaje_original_json = elems[0].InnerText;
                            break;
                        case "cancelada":
                            respuesta.cancelada = elems[0].InnerText;
                            break;
                        case "saldo":
                            respuesta.saldo = elems[0].InnerText;
                            break;
                        case "uuid":
                            respuesta.uuid = elems[0].InnerText;
                            break;
                        case "servidor":
                            respuesta.servidor = elems[0].InnerText;
                            break;
                        case "ejecucion":
                            respuesta.ejecucion = elems[0].InnerText;
                            break;
                    }
            }

            return respuesta;
        }

        /// <summary>
        /// Llama a un Web Service
        /// </summary>
        /// <param name="action">Action.</param>
        /// <param name="param">Parametros</param>
        /// <see cref="http://stackoverflow.com/a/4791932"/>
        /// <seealso cref="http://stackoverflow.com/a/561242"/>
        private static XmlDocument CallWebService(string pac, string action, Parametro[] param)
        {
            string[] funcion = action.Split('#');

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(funcion[1], param);
            string urlTimbrar = string.Format(url, pac);
            HttpWebRequest webRequest = CreateWebRequest(urlTimbrar, action);

            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // Ignore invalid SSL Cert
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            WebResponse webResponse = webRequest.EndGetResponse(asyncResult);
            StreamReader rd = new StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
            soapResult = rd.ReadToEnd();
            XmlDocument respuesta = new XmlDocument();
            respuesta.LoadXml(soapResult);
            return respuesta;
            /*using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }

                XmlDocument respuesta = new XmlDocument();
                respuesta.LoadXml(soapResult);
                return respuesta;
            }*/
        }

        /// <summary>
        /// Creates the web request.
        /// </summary>
        /// <see cref="http://stackoverflow.com/a/4791932"/>
        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            /*webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";*/
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml;charset=utf-8";
            return webRequest;
        }

        /// <see cref="http://stackoverflow.com/a/4791932"/>
        private static XmlDocument CreateSoapEnvelope(string funcion, Parametro[] param)
        {
            // Documento XML del soapEnvelope
            XmlDocument soapEnvelop = new XmlDocument();

            // Cadena soapEnvelop
            string envelope = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""><soap:Body>";

            // Se agrega la funcion
            envelope += @"<" + funcion + @" xmlns=""urn:wservicewsdl"">";

            // Se agregan los parametros
            foreach (Parametro par in param)
                envelope += "<" + par.nombre + ">" + WebUtility.HtmlEncode(par.valor) + "</" + par.nombre + ">";

            // Se cierra la funcion
            envelope += "</" + funcion + ">";

            // Se cierra el soapEnvelope
            envelope += "</soap:Body></soap:Envelope>";

            UTF8Encoding utf8 = new UTF8Encoding();

            byte[] encodedBytes = utf8.GetBytes(envelope);
            StringBuilder aux = new StringBuilder();
            foreach (byte bt in encodedBytes)
                aux.Append((char)bt);

            // Se carga el soapEnvelop
            soapEnvelop.LoadXml(aux.ToString());

            // Se retorna el soapEnvelop
            return soapEnvelop;
        }

        /// <see cref="http://stackoverflow.com/a/4791932"/>
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        /// <seealso cref="http://stackoverflow.com/a/561242"/>
        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
