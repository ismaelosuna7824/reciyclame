//using ImapLibrary;
//using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RecyclameV2.Clases;
using RecyclameV2.Utils;

namespace RecyclameV2.Utils
{
    public class Utility
    {
        private static readonly byte[] CRLF = { (byte)'\r', (byte)'\n' };
        protected static readonly String BOUNDARY = "AraConsultoriaService_batch";
        protected static readonly String NEWLINE = "\r\n";
        protected static readonly String LINE = "--";
        public static readonly int ALL_ASCII = 1;
        public static readonly int MOSTLY_ASCII = 2;
        public static readonly int MOSTLY_NONASCII = 3;
        public static string GetRegExParsedValue(string strPattern, string strSearch)
        {
            Regex reg = new Regex(strPattern, RegexOptions.Multiline);
            Match mat = reg.Match(strSearch);
            if (mat.Success)
            {
                return mat.Groups["RetVal"].Value.ToString();
            }
            return string.Empty;
        }
        public static int diferenciaDias(DateTime fechaInicio, DateTime fechaFin)
        {
            return Convert.ToInt32((fechaFin - fechaInicio).TotalDays);
        }

        public static bool isToday(DateTime fecha)
        {
            if (DateTime.Now.Year == fecha.Year && DateTime.Now.Month == fecha.Month && DateTime.Now.Day == fecha.Day)
            {
                return true;
            }
            return false;
        }

        public static bool esFechaBusquedaValida(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string getUniqueMessageIDValue()
        {
            StringBuilder s = new StringBuilder();
            s.Append("<").Append(s.GetHashCode()).Append('.').
              Append(System.DateTime.Now.Millisecond.ToString()).
              Append("@www.araconsultoriaysoluciones.com").Append(">");
            return s.ToString();
        }
        private static string GenerateBoundary()
        {
            return String.Format("NextPart_{0}", Guid.NewGuid().ToString("D"));
        }
        /*public static bool enviarFactura(string to, string pathXML, string pathPDF)
        {
            return EmailOperation(to, "Envio de factura  - Recyclame Navolato-", pathXML, pathPDF, false);
        }
        private static bool EmailOperation(string to, string subject, string pathXML, string pathPDF, bool nota)
        {
            string boundary = GenerateBoundary();
            bool result = false;
            int times = 0;
        Again:
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/upload/gmail/v1/users/me/messages/send?uploadType=multipart&fields=id");
                request.Headers["GData-Version"] = "3.0";
                request.Headers[HttpRequestHeader.Authorization] = "Bearer " + Global.Access_Token;
                request.Method = "POST";
                request.ContentType = "multipart/related; boundary=\"" + boundary + "\"";
                //using (Stream stream = await request.GetRequestStreamAsync())
                using (Stream stream = request.GetRequestStream())
                {
                    // convert request to byte array
                    //if (!getPostDataEmailOperations(stream, getUniqueMessageIDValue(), boundary, to, subject, pathXML, pathPDF, nota).Result)
                    if (!getPostDataEmailOperationsNormal(stream, getUniqueMessageIDValue(), boundary, to, subject, pathXML, pathPDF, nota))
                    {
                        stream.Dispose();
                        request.Abort();
                        return false;
                    }
                }
                int code = 0;
                string jsonresponse = string.Empty;
                StringBuilder sb = null;
                //using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    code = Convert.ToInt32(response.StatusCode);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = true;
                        using (System.IO.StreamReader sr3 = new System.IO.StreamReader(response.GetResponseStream()))
                        {
                            sb = new StringBuilder(sr3.ReadToEnd());

                        }
                    }
                    response.Dispose();
                    request.Abort();
                }
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.WriteLine("EMAIL OPERATIONS EXCEPTIONS: " + e.ToString());
                if (e.ToString().IndexOf("400") == -1)
                {
                    HttpWebResponse webResponse = e.Response as HttpWebResponse;
                    string strException = string.Empty;
                    if (webResponse != null)
                    {
                        StreamReader readerEx = new StreamReader(webResponse.GetResponseStream());
                        strException = readerEx.ReadToEnd();
                        readerEx.Dispose();
                    }
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(strException);
                    Newtonsoft.Json.Linq.JToken token = null;
                    if (json.TryGetValue("error", out token))
                    {
                        json = (Newtonsoft.Json.Linq.JObject)token;
                        if (json.TryGetValue("message", out token))
                        {
                            if (string.Compare((string)token, "Invalid Credentials", true) == 0)
                            {
                                if (Global.refreshToken())
                                {
                                    if (times < 3)
                                    {
                                        times++;
                                        json = null;
                                        goto Again;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        json = null;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EMAIL OPERATIONS EXCEPTIONS: " + e.ToString());
            }
            return result;
        }

        public static bool enviarNotaCredito(string to, string pathXML, string pathPDF, long facturaId)
        {
            return EmailOperation(to, "Envio de nota de crédito. Folio Factura " + facturaId + "- Recyclame Navolato -", pathXML, pathPDF, true);
        }*/

        /*public static bool enviarCorteDeCaja(string to, DateTime fecha, string pathPDF)
        {
            return EmailOperation(to, string.Format("Envio de corte de caja de dia {0} - Recyclame Navolato -", fecha.ToString("dd/MM/yyyy")), fecha, pathPDF);
        }

        private static bool EmailOperation(string to, string subject, DateTime fecha, string pathPDF)
        {
            string boundary = GenerateBoundary();
            bool result = false;
            int times = 0;
        Again:
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/upload/gmail/v1/users/me/messages/send?uploadType=multipart&fields=id");
                request.Headers["GData-Version"] = "3.0";
                request.Headers[HttpRequestHeader.Authorization] = "Bearer " + Global.Access_Token;
                request.Method = "POST";
                request.ContentType = "multipart/related; boundary=\"" + boundary + "\"";
                using (Stream stream = request.GetRequestStream())
                {
                    // convert request to byte array
                    if (!getPostDataEmailOperations(stream, getUniqueMessageIDValue(), boundary, to, subject, fecha, pathPDF))
                    {
                        stream.Dispose();
                        request.Abort();
                        return false;
                    }
                }
                int code = 0;
                string jsonresponse = string.Empty;
                StringBuilder sb = null;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    code = Convert.ToInt32(response.StatusCode);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = true;
                        using (System.IO.StreamReader sr3 = new System.IO.StreamReader(response.GetResponseStream()))
                        {
                            sb = new StringBuilder(sr3.ReadToEnd());

                        }
                    }
                    response.Dispose();
                    request.Abort();
                }
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.WriteLine("EMAIL OPERATIONS EXCEPTIONS: " + e.ToString());
                if (e.ToString().IndexOf("400") == -1)
                {
                    HttpWebResponse webResponse = e.Response as HttpWebResponse;
                    string strException = string.Empty;
                    if (webResponse != null)
                    {
                        StreamReader readerEx = new StreamReader(webResponse.GetResponseStream());
                        strException = readerEx.ReadToEnd();
                        readerEx.Dispose();
                    }
                    Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.Parse(strException);
                    Newtonsoft.Json.Linq.JToken token = null;
                    if (json.TryGetValue("error", out token))
                    {
                        json = (Newtonsoft.Json.Linq.JObject)token;
                        if (json.TryGetValue("message", out token))
                        {
                            if (string.Compare((string)token, "Invalid Credentials", true) == 0)
                            {
                                if (Global.refreshToken())
                                {
                                    if (times < 3)
                                    {
                                        times++;
                                        json = null;
                                        goto Again;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        json = null;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EMAIL OPERATIONS EXCEPTIONS: " + e.ToString());
            }
            return result;
        }*/

        private static bool getPostDataEmailOperationsNormal(Stream s, string msgId, string boundary, string to, string subject, string pathXML, string pathPDF, bool nota)
        {
            if (subject != null && subject.Length > 0)
            {
                subject = Utility.encodeSubjectText(subject);
            }
            SimpleSMTPHeader sheader = new SimpleSMTPHeader(Global.getEmailPublicoGeneral(), subject);
            Utility.writeBytes(s, "--" + boundary + "\r\n");
            Utility.writeBytes(s, "Content-Type: application/json; charset=UTF-8\r\n\r\n");
            Utility.writeBytes(s, "{\r\n}\r\n\r\n");
            Utility.writeBytes(s, "--" + boundary + "\r\n");
            Utility.writeBytes(s, "Content-Type: message/rfc822\r\n\r\n");
            string boundaryEmail = string.Empty;
            DateTime dt = DateTime.Now;
            fillEmailGmailApi(s, sheader, ref boundaryEmail, msgId, dt, to, subject, nota);
            byte[] buffer = null;
            Base64.Coder coder = null;
            try
            {
                string[] files = new string[2];
                files[0] = pathXML;
                files[1] = pathPDF;
                int read = 0;
                for (int i = 0; i < 2; i++)
                {
                    FileInfo info = new FileInfo(files[i]);
                    coder = new Base64.Encoder(Base64.CRLF | Base64.NO_CLOSE, null);
                    s.Write(CRLF, 0, CRLF.Length);
                    Utility.writeBytes(s, "------" + boundaryEmail + "\r\n");
                    Utility.writeBytes(s, "Content-Type: application/octet-stream\r\n");
                    Utility.writeBytes(s, "Content-Transfer-Encoding: base64\r\n");
                    Utility.writeBytes(s, "Content-Disposition: attachment; filename=\"" + Utility.encodeText(info.Name) + "\"\r\n\r\n");
                    buffer = new byte[8192];
                    read = 0;
                    using (FileStream fs = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                    {
                        while ((read = fs.Read(buffer, 0, 8192)) > 0)
                        {
                            if (read >= 8192)
                            {
                                coder.process(buffer, 0, read, false, s);
                            }
                            else
                            {
                                coder.process(buffer, 0, read, true, s);
                            }
                        }
                        fs.Dispose();
                    }
                    s.Write(CRLF, 0, CRLF.Length);
                    coder = null;
                    info = null;
                }
                buffer = new byte[8192];
                read = 0;
                coder = new Base64.Encoder(Base64.CRLF | Base64.NO_CLOSE, null);
                s.Write(CRLF, 0, CRLF.Length);
                Utility.writeBytes(s, "------" + boundaryEmail + "\r\n");
                Utility.writeBytes(s, "Content-Type: image/png\r\n");
                Utility.writeBytes(s, "Content-Transfer-Encoding: base64\r\n");
                Utility.writeBytes(s, "Content-Disposition: inline; filename=\"" + Utility.encodeText("recyclame.png") + "\"\r\n");
                Utility.writeBytes(s, "Content-ID: <logo>\r\n\r\n");
                using (var img = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Recyclame.Resources.recyclame.fw.jpeg"))
                {
                    using (BinaryReader reader = new BinaryReader(img))
                    {
                        while ((read = reader.Read(buffer, 0, 8192)) > 0)
                        {
                            coder.process(buffer, 0, read, false, s);
                        }
                        reader.Dispose();
                    }
                }
                s.Write(CRLF, 0, CRLF.Length);
                coder = null;

                Utility.writeBytes(s, "------" + boundaryEmail + "--\r\n");
                Utility.writeBytes(s, "--" + boundary + "--\r\n");
                s.Flush();
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EMAIL OPERATION POST DATA EXCEPTION: " + e.ToString());
            }
            return false;
        }

        private static async Task<bool> getPostDataEmailOperations(Stream s, string msgId, string boundary, string to, string subject, string pathXML, string pathPDF, bool nota)
        {
            if (subject != null && subject.Length > 0)
            {
                subject = Utility.encodeSubjectText(subject);
            }
            SimpleSMTPHeader sheader = new SimpleSMTPHeader(Global.getEmailPublicoGeneral(), subject);
            Utility.writeBytes(s, "--" + boundary + "\r\n");
            Utility.writeBytes(s, "Content-Type: application/json; charset=UTF-8\r\n\r\n");
            Utility.writeBytes(s, "{\r\n}\r\n\r\n");
            Utility.writeBytes(s, "--" + boundary + "\r\n");
            Utility.writeBytes(s, "Content-Type: message/rfc822\r\n\r\n");
            string boundaryEmail = string.Empty;
            DateTime dt = DateTime.Now;
            fillEmailGmailApi(s, sheader, ref boundaryEmail, msgId, dt, to, subject, nota);
            byte[] buffer = null;
            Base64.Coder coder = null;
            try
            {
                string[] files = new string[2];
                files[0] = pathXML;
                files[1] = pathPDF;
                int read = 0;
                for (int i = 0; i < 2; i++)
                {
                    FileInfo info = new FileInfo(files[i]);
                    coder = new Base64.Encoder(Base64.CRLF | Base64.NO_CLOSE, null);
                    s.Write(CRLF, 0, CRLF.Length);
                    Utility.writeBytes(s, "------" + boundaryEmail + "\r\n");
                    Utility.writeBytes(s, "Content-Type: application/octet-stream\r\n");
                    Utility.writeBytes(s, "Content-Transfer-Encoding: base64\r\n");
                    Utility.writeBytes(s, "Content-Disposition: attachment; filename=\"" + Utility.encodeText(info.Name) + "\"\r\n\r\n");
                    buffer = new byte[8192];
                    read = 0;
                    using (FileStream fs = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                    {
                        while ((read = fs.Read(buffer, 0, 8192)) > 0)
                        {
                            if (read >= 8192)
                            {
                                coder.process(buffer, 0, read, false, s);
                            }
                            else
                            {
                                coder.process(buffer, 0, read, true, s);
                            }
                        }
                        fs.Dispose();
                    }
                    s.Write(CRLF, 0, CRLF.Length);
                    coder = null;
                    info = null;
                }
                buffer = new byte[8192];
                read = 0;
                coder = new Base64.Encoder(Base64.CRLF | Base64.NO_CLOSE, null);
                s.Write(CRLF, 0, CRLF.Length);
                Utility.writeBytes(s, "------" + boundaryEmail + "\r\n");
                Utility.writeBytes(s, "Content-Type: image/png\r\n");
                Utility.writeBytes(s, "Content-Transfer-Encoding: base64\r\n");
                Utility.writeBytes(s, "Content-Disposition: inline; filename=\"" + Utility.encodeText("recyclame.png") + "\"\r\n");
                Utility.writeBytes(s, "Content-ID: <logo>\r\n\r\n");
                using (var img = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Recyclame.Resources.recyclame.fw.jpeg"))
                {
                    using (BinaryReader reader = new BinaryReader(img))
                    {
                        while ((read = reader.Read(buffer, 0, 8192)) > 0)
                        {
                            coder.process(buffer, 0, read, false, s);
                        }
                        reader.Dispose();
                    }
                }
                s.Write(CRLF, 0, CRLF.Length);
                coder = null;

                Utility.writeBytes(s, "------" + boundaryEmail + "--\r\n");
                Utility.writeBytes(s, "--" + boundary + "--\r\n");
                s.Flush();
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EMAIL OPERATION POST DATA EXCEPTION: " + e.ToString());
            }
            return false;
        }

        private static bool getPostDataEmailOperations(Stream s, string msgId, string boundary, string to, string subject, DateTime fecha, string pathPDF)
        {
            if (subject != null && subject.Length > 0)
            {
                subject = Utility.encodeSubjectText(subject);
            }
            SimpleSMTPHeader sheader = new SimpleSMTPHeader(Global.getEmailPublicoGeneral(), subject);
            Utility.writeBytes(s, "--" + boundary + "\r\n");
            Utility.writeBytes(s, "Content-Type: application/json; charset=UTF-8\r\n\r\n");
            Utility.writeBytes(s, "{\r\n}\r\n\r\n");
            Utility.writeBytes(s, "--" + boundary + "\r\n");
            Utility.writeBytes(s, "Content-Type: message/rfc822\r\n\r\n");
            string boundaryEmail = string.Empty;
            DateTime dt = DateTime.Now;
            fillEmailGmailApi(s, sheader, ref boundaryEmail, msgId, dt, to, subject, fecha);
            byte[] buffer = null;
            Base64.Coder coder = null;
            try
            {
                string[] files = new string[1];
                files[0] = pathPDF;
                int read = 0;
                for (int i = 0; i < 1; i++)
                {
                    FileInfo info = new FileInfo(files[i]);
                    coder = new Base64.Encoder(Base64.CRLF | Base64.NO_CLOSE, null);
                    s.Write(CRLF, 0, CRLF.Length);
                    Utility.writeBytes(s, "------" + boundaryEmail + "\r\n");
                    Utility.writeBytes(s, "Content-Type: application/octet-stream\r\n");
                    Utility.writeBytes(s, "Content-Transfer-Encoding: base64\r\n");
                    Utility.writeBytes(s, "Content-Disposition: attachment; filename=\"" + Utility.encodeText(info.Name) + "\"\r\n\r\n");
                    buffer = new byte[8192];
                    read = 0;
                    using (FileStream fs = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                    {
                        while ((read = fs.Read(buffer, 0, 8192)) > 0)
                        {
                            if (read >= 8192)
                            {
                                coder.process(buffer, 0, read, false, s);
                            }
                            else
                            {
                                coder.process(buffer, 0, read, true, s);
                            }
                        }
                        fs.Dispose();
                    }
                    s.Write(CRLF, 0, CRLF.Length);
                    coder = null;
                    info = null;
                }
                buffer = new byte[8192];
                read = 0;
                coder = new Base64.Encoder(Base64.CRLF | Base64.NO_CLOSE, null);
                s.Write(CRLF, 0, CRLF.Length);
                Utility.writeBytes(s, "------" + boundaryEmail + "\r\n");
                Utility.writeBytes(s, "Content-Type: image/png\r\n");
                Utility.writeBytes(s, "Content-Transfer-Encoding: base64\r\n");
                Utility.writeBytes(s, "Content-Disposition: inline; filename=\"" + Utility.encodeText("recyclame.png") + "\"\r\n");
                Utility.writeBytes(s, "Content-ID: <logo>\r\n\r\n");
                using (var img = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Recyclame.Resources.recyclame.fw.jpeg"))
                {
                    using (BinaryReader reader = new BinaryReader(img))
                    {
                        while ((read = reader.Read(buffer, 0, 8192)) > 0)
                        {
                            coder.process(buffer, 0, read, false, s);
                        }
                        reader.Dispose();
                    }
                }
                s.Write(CRLF, 0, CRLF.Length);
                coder = null;

                Utility.writeBytes(s, "------" + boundaryEmail + "--\r\n");
                Utility.writeBytes(s, "--" + boundary + "--\r\n");
                s.Flush();
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EMAIL OPERATION POST DATA EXCEPTION: " + e.ToString());
            }
            return false;
        }
        public static string encodeText(string text)
        {
            return encodeWord(text, "UTF-8", null, true);
        }

        private static string encodeWord(string text, string charset, string encoding, bool encodingWord)
        {
            int ascii = checkAscii(text.ToCharArray());
            if (ascii == ALL_ASCII)
            {
                return text;
            }
            if (encoding == null)
            {
                if (ascii != MOSTLY_NONASCII)
                    encoding = "Q";
                else
                    encoding = "B";
            }

            //TransferEncoding encodeType = TransferEncoding.SevenBit;
            //Encoding enc = Encoding.GetEncoding(charset);
            //bool b64;
            if (encoding.CompareTo("B") == 0)
            {
                return Base64Encode(text);
                //encodeType = TransferEncoding.Base64;
                //b64 = true;
            }
            else if (encoding.CompareTo("Q") == 0)
            {
                return QEncode(text);
                //encodeType = TransferEncoding.QuotedPrintable;
                //b64 = false;
            }
            else
            {
                return text;
            }
        }

        private static string QEncode(string text)
        {
            throw new NotImplementedException();
        }

        public static int checkAscii(char[] b)
        {
            int ascii = 0, non_ascii = 0;
            int length = b.Length;
            for (int i = 0; i < length; i++)
            {
                // The '&' operator automatically causes b[i] to be promoted
                // to an int, and we mask out the higher bytes in the int 
                // so that the resulting value is not a negative integer.
                if (nonascii(b[i] & 0xff)) // non-ascii
                    non_ascii++;
                else
                    ascii++;
            }

            if (non_ascii == 0)
                return ALL_ASCII;
            if (ascii > non_ascii)
                return MOSTLY_ASCII;

            return MOSTLY_NONASCII;
        }
        public static String fillEmailGmailApi(Stream baos, SimpleSMTPHeader header, ref String boundary, String msgId, DateTime dt, string to, string subject, bool nota)
        {
            String transferencoding = "7BIT";
            try
            {
                string references = null;
                setEnvelopeHeaderSMTPMessage(header, references, msgId, null, false, msgId, dt);
                byte[] bytes = createHtmlEmail(subject, false, nota);
                int encoding = Utility.checkAscii(bytes);
                if (encoding == Utility.ALL_ASCII)
                {
                    transferencoding = "7BIT";
                }
                else if (encoding == Utility.MOSTLY_ASCII)
                {
                    transferencoding = "quoted-printable";
                    bytes = Utility.encodeQP(bytes);
                }
                else
                {
                    bytes = Base64.encode(bytes, Base64.DEFAULT);
                    transferencoding = "base64";
                }

                boundary = GenerateBoundary();// generateBoundary();
                header.addHeaderField("Content-Type", "multipart/mixed; boundary=\"----" + boundary + "\"");
                String[] _to = to.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                if (_to != null)
                {
                    foreach (String recpt in _to)
                    {
                        if (recpt.Trim().Length > 0)
                        {
                            header.addTO(recpt);
                        }
                    }
                }
                header.isAppend(true);
                if (baos != null)
                {
                    byte[] b = Encoding.UTF8.GetBytes(header.toString());
                    baos.Write(b, 0, b.Length);
                    b = null;
                }
                header.clearHeaders();

                header.addBoundaryField(boundary);
                header.addHeaderField("Content-Type", "text/html; charset=\"utf-8\"");
                header.addHeaderField("Content-Transfer-Encoding", transferencoding);
                if (baos != null)
                {
                    byte[] bytesAppend = Encoding.UTF8.GetBytes(header.headerMultiparttoString());//.getBytes("UTF-8");
                    baos.Write(bytesAppend, 0, bytesAppend.Length);
                    bytesAppend = null;
                }
                if (bytes != null)
                {
                    if (baos != null)
                    {
                        baos.Write(bytes, 0, bytes.Length);
                    }
                    bytes = null;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("FILL EMAL HEADERS" + e.ToString());
            }
            return transferencoding;
        }
        public static String fillDraftEmailGmailApi(Base64OutputStream baos, SimpleSMTPHeader header, String msgId, DateTime dt, string to, string subject, string body, bool nota)
        {
            String transferencoding = "7BIT";
            try
            {
                String references = null;

                setEnvelopeHeaderSMTPMessage(header, references, msgId, null, false, msgId, dt);
                byte[] bytes = createHtmlEmail(subject, false, nota);
                //int encoding = Utility.checkAscii(bytes);
                //if (encoding == Utility.ALL_ASCII)
                //{
                //    transferencoding = "7BIT";
                //}
                //else if (encoding == Utility.MOSTLY_ASCII)
                //{
                transferencoding = "quoted-printable";
                //bytes = Utility.encodeQP(bytes);
                //}
                //else
                //{
                //    bytes = Base64.encode(bytes, Base64.DEFAULT);
                //    transferencoding = "base64";
                //}

                //String boundary = null;
                header.addHeaderField("Content-Type", "text/html" + "; charset=\"utf-8\"");
                header.addHeaderField("Content-Transfer-Encoding", transferencoding);//"quoted-printable");
                String[] _to = to.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                if (_to != null)
                {
                    foreach (String recpt in _to)
                    {
                        if (recpt.Trim().Length > 0)
                        {
                            header.addTO(recpt);
                        }
                    }
                }
                header.isAppend(true);
                if (baos != null)
                {
                    Utility.writeBytes(baos, header.toString());
                }
                header.clearHeaders();
                if (bytes != null)
                {
                    if (baos != null)
                    {
                        baos.Write(bytes, 0, bytes.Length);
                    }
                    bytes = null;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("FILL EMAL HEADERS" + e.ToString());
            }
            return transferencoding;
        }

        public static String fillEmailGmailApi(Stream baos, SimpleSMTPHeader header, ref String boundary, String msgId, DateTime dt, string to, string subject, DateTime fechaCorte)
        {
            String transferencoding = "7BIT";
            try
            {
                string references = null;
                setEnvelopeHeaderSMTPMessage(header, references, msgId, null, false, msgId, dt);
                byte[] bytes = createHtmlEmail(subject, false, fechaCorte);
                int encoding = Utility.checkAscii(bytes);
                if (encoding == Utility.ALL_ASCII)
                {
                    transferencoding = "7BIT";
                }
                else if (encoding == Utility.MOSTLY_ASCII)
                {
                    transferencoding = "quoted-printable";
                    bytes = Utility.encodeQP(bytes);
                }
                else
                {
                    bytes = Base64.encode(bytes, Base64.DEFAULT);
                    transferencoding = "base64";
                }

                boundary = GenerateBoundary();// generateBoundary();
                header.addHeaderField("Content-Type", "multipart/mixed; boundary=\"----" + boundary + "\"");
                String[] _to = to.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                if (_to != null)
                {
                    foreach (String recpt in _to)
                    {
                        if (recpt.Trim().Length > 0)
                        {
                            header.addTO(recpt);
                        }
                    }
                }
                header.isAppend(true);
                if (baos != null)
                {
                    byte[] b = Encoding.UTF8.GetBytes(header.toString());
                    baos.Write(b, 0, b.Length);
                    b = null;
                }
                header.clearHeaders();

                header.addBoundaryField(boundary);
                header.addHeaderField("Content-Type", "text/html; charset=\"utf-8\"");
                header.addHeaderField("Content-Transfer-Encoding", transferencoding);
                if (baos != null)
                {
                    byte[] bytesAppend = Encoding.UTF8.GetBytes(header.headerMultiparttoString());//.getBytes("UTF-8");
                    baos.Write(bytesAppend, 0, bytesAppend.Length);
                    bytesAppend = null;
                }
                if (bytes != null)
                {
                    if (baos != null)
                    {
                        baos.Write(bytes, 0, bytes.Length);
                    }
                    bytes = null;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("FILL EMAL HEADERS" + e.ToString());
            }
            return transferencoding;
        }
        public static byte[] createHtmlEmail(String subject, bool useAscii, bool nota)
        {
            string EMAIL_BODY = string.Empty;
            if (!nota)
            {
                using (var resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Recyclame.ResourcesFacturacion.plantilla_correo.html"))
                {
                    using (StreamReader r = new StreamReader(resource))
                    {
                        EMAIL_BODY = r.ReadToEnd();
                        r.Dispose();
                    }
                    //using (var img = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Recyclame.Resources.recyclame.fw.jpeg"))
                    //{
                    //    using (BinaryReader reader = new BinaryReader(img))
                    //    {
                    //        byte[] binaryByteArray = reader.ReadBytes((int)img.Length); // Reads the bytes from binay file and stores into byte array
                    //        EMAIL_BODY = string.Format(EMAIL_BODY, Convert.ToBase64String(binaryByteArray)); // Encodes the byte array to Base64 string and returns the string
                    //    }
                    //}
                    return Utility.getBytes(EMAIL_BODY);
                }
            }
            else
            {
                using (var resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Recyclame.ResourcesFacturacion.plantilla_nota.html"))
                {
                    using (StreamReader r = new StreamReader(resource))
                    {
                        EMAIL_BODY = r.ReadToEnd();
                        r.Dispose();
                    }
                    //using (var img = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Recyclame.Resources.recyclame.fw.jpeg"))
                    //{
                    //    using (BinaryReader reader = new BinaryReader(img))
                    //    {
                    //        byte[] binaryByteArray = reader.ReadBytes((int)img.Length); // Reads the bytes from binay file and stores into byte array
                    //        EMAIL_BODY = string.Format(EMAIL_BODY, Convert.ToBase64String(binaryByteArray)); // Encodes the byte array to Base64 string and returns the string
                    //    }
                    //}
                    return Utility.getBytes(EMAIL_BODY);
                }
            }
        }
        public static byte[] createHtmlEmail(String subject, bool useAscii, DateTime fecha)
        {
            string EMAIL_BODY = string.Empty;
            using (var resource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Recyclame.ResourcesFacturacion.plantilla_corte.html"))
            {
                using (StreamReader r = new StreamReader(resource))
                {
                    EMAIL_BODY = string.Format(r.ReadToEnd(), fecha.ToString("dd/MM/yyyy"));
                    r.Dispose();
                }
                return Utility.getBytes(EMAIL_BODY);
            }
        }

        private static byte[] getBytes(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
        private static byte[] getASCIIBytes(string s)
        {
            return Encoding.ASCII.GetBytes(s);
        }
        private static int checkAscii(byte[] b)
        {
            int ascii = 0, non_ascii = 0;
            int length = b.Length;
            for (int i = 0; i < length; i++)
            {
                // The '&' operator automatically causes b[i] to be promoted
                // to an int, and we mask out the higher bytes in the int 
                // so that the resulting value is not a negative integer.
                if (nonascii(b[i] & 0xff)) // non-ascii
                    non_ascii++;
                else
                    ascii++;
            }

            if (non_ascii == 0)
                return ALL_ASCII;
            if (ascii > non_ascii)
                return MOSTLY_ASCII;

            return MOSTLY_NONASCII;
        }
        private static bool nonascii(int b)
        {
            return b >= 0177 || (b < 040 && b != '\r' && b != '\n' && b != '\t');
        }
        public static byte[] encodeQP(byte[] bb)
        {
            return ToQuotedPrintable(bb);
        }
        private static byte[] ToQuotedPrintable(byte[] b)
        {
            StringReader sr = new StringReader(Encoding.UTF8.GetString(b));
            StringBuilder sb = new StringBuilder();
            Int32 i;

            while ((i = sr.Read()) > 0)
            {
                if (i == 61)
                {
                    sb.AppendFormat("=", Convert.ToString(i, 16).ToUpper());
                }
                else if ((32 < i && i < 127) ||
                    i == 13 ||
                    i == 10 ||
                    i == 9 ||
                    i == 32)
                {
                    sb.Append(Convert.ToChar(i));
                }
                else
                {
                    sb.AppendFormat("=", Convert.ToString(i, 16).ToUpper());
                }
            }
            return Encoding.ASCII.GetBytes(sb.ToString());
        }
        public static void setEnvelopeHeaderSMTPMessage(SimpleSMTPHeader header, String references, String messageid, String inreplyto, bool isAnswer, String myMessageId, DateTime dtSent)
        {
            try
            {
                header.addHeaderField("X-Mailer", "ARAConsultoriaySoluciones.com");
                header.addHeaderField("Date", InternalDateformat(dtSent));
                header.addHeaderField("Message-ID", myMessageId);
                if (isAnswer)
                {
                    if (references == null || references.Length == 0)
                        references = inreplyto;
                    if (messageid != null && messageid.Length > 0)
                    {
                        references = messageid;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("setEnvelopeHeaderSMTPMessage Exception:" + e.ToString());
            }
        }

        public static readonly int MIN_RADIX = 2;
        /**
         * The maximum radix used for conversions between characters and integers.
         */
        public static readonly int MAX_RADIX = 36;
        public static String InternalDateformat(DateTime d)
        {
            /*
             * SimpleDateFormat objects aren't thread safe, so rather
             * than create a separate such object for each request,
             * we create one object and synchronize its use here
             * so that only one thread is using it at a time.  This
             * trades off some potential concurrency for speed in the
             * common case.
             *
             * This method is only used when formatting the date in a
             * message that's being appended to a folder.
             */
            StringBuilder sb = new StringBuilder(d.ToString("dd-MMM-yyyy HH:mm:ss "));
            // compute timezone offset string
            // XXX - Yes, I know this is deprecated	
            int rawOffsetInMins = (int)-TimeZoneInfo.Utc.GetUtcOffset(d).TotalMinutes;

            /*
             * XXX - in JavaMail 1.4 / J2SE 1.4, possibly replace above with:
             *
            TimeZone tz = TimeZone.getDefault();
            int offset = tz.getOffset(d);	// get offset from GMT
            int rawOffsetInMins = offset / 60 / 1000; // offset from GMT in mins
             */
            if (rawOffsetInMins < 0)
            {
                sb.Append('-');
                rawOffsetInMins = (-rawOffsetInMins);
            }
            else
                sb.Append('+');

            int offsetInHrs = rawOffsetInMins / 60;
            int offsetInMins = rawOffsetInMins % 60;

            sb.Append(forDigit((offsetInHrs / 10), 10));
            sb.Append(forDigit((offsetInHrs % 10), 10));
            sb.Append(forDigit((offsetInMins / 10), 10));
            sb.Append(forDigit((offsetInMins % 10), 10));

            return sb.ToString();
        }

        public static char forDigit(int digit, int radix)
        {
            if (MIN_RADIX <= radix && radix <= MAX_RADIX)
            {
                if (digit >= 0 && digit < radix)
                {
                    return (char)(digit < 10 ? digit + '0' : digit + 'a' - 10);
                }
            }
            return Convert.ToChar(0);
        }
        public static int writeBytes(Stream s, string str)
        {
            if (str == null || str.Length == 0)
            {
                return 0;
            }
            int length = str.Length;
            byte[] bytes = new byte[length];
            for (int index = 0; index < length; index++)
            {
                bytes[index] = (byte)str[index];
            }
            s.Write(bytes, 0, length);
            return length;
        }

        public static string encodeSubjectText(string text)
        {
            return encodeSubject(text);
        }
        private static string encodeSubject(string text)
        {
            if (IsASCII(text))
            {
                return text;
            }
            else
            {
                return Base64Encode(text);
            }
        }

        private static bool IsASCII(string text)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Takes a unicode string and encodes it using Base64-encoding.
        /// </summary>
        /// <param name="s">The string to encode.</param>
        /// <returns>The input string encoded as Base64-encoded string containing only ASCII
        /// characters.</returns>
        public static string Base64Encode(string s)
        {
            StringBuilder builder = new StringBuilder("=?UTF-8?B?");
            string b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
            return builder.Append(b64).ToString() + "?=";
        }
        public static void granType(string code)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            request.Method = "POST";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*;q=0.8";
            request.Timeout = 10000;
            request.Headers.Add("Accept-Language", "en-us,en;q=0.5");
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            request.KeepAlive = true;

            string strPosData = "code=" + code + "&client_id=" + Global.getClientId + "&client_secret=" + Global.getClientSecret + "&redirect_uri=" + Global.RedirectURL + "&grant_type=authorization_code";
            request.ContentLength = strPosData.Length;
            StreamWriter sw3 = new StreamWriter(request.GetRequestStream());
            sw3.Write(strPosData);
            sw3.Close();
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    string strHTML = string.Empty;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        strHTML = reader.ReadToEnd();
                        reader.Close();
                        string token = Utility.GetRegExParsedValue("access_token\" : \"(?<RetVal>.*?)\"", strHTML);
                        string refreshtoken = Utility.GetRegExParsedValue("refresh_token\" : \"(?<RetVal>.*?)\"", strHTML);
                        Tokens tokens = new Tokens();
                        tokens.RefreshToken = refreshtoken;
                        tokens.AccessToken = token;
                        tokens.Grabar();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
