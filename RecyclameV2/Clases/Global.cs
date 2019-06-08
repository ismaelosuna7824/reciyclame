using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using RecyclameV2.PAC;
using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecyclameV2.Clases
{
    public static class Global
    {
        public static class MENSAJES
        {
            public static string EJECUTANDO = "WM_EJECUTANDO";
        }
        private static readonly string clientID = "966254033375-9vfh3bja168517plbgcqclve50637rhg.apps.googleusercontent.com";
        private static readonly string clientSecret = "PnlMASvaYAKoyK18eRMtGyoC";
        private static readonly string redirectURL = "urn:ietf:wg:oauth:2.0:oob";
        public static readonly string STR_NOMBRE_SISTEMA = "Recyclame";
        public static readonly string RFC_PUBLICO_GENERAL_NACIONAL = "XAXX010101000";
        public static readonly string RFC_PUBLICO_GENERAL_EXTRANJERO = "XEXX010101000";
        public static readonly string NOMBRE_FISCAL_PUBLICO_GENERAL = "PÚBLICO GENERAL";
        private static readonly string USER_EMAIL = "recyclame.facturacion@gmail.com";
        private static readonly string PASS_EMAIL = "r3cicl4m3";
        private static readonly string RFC_DEFAULT = "AEAC8310318VA";
        private static readonly string USER_TIMBRADO = "recyclame";
        private static readonly string PASS_TIMBRADO = "R3cycl4m3";
        //private static string acessToken = Properties.Settings.Default.GmailAccessToken;
        //private static string refreshtoken = Properties.Settings.Default.GmailRefreshToken;
        public static DateTime _dtDefaultDateTime = new DateTime(1900, 1, 1);
        private static readonly string CORTE_EMAIL = string.Empty;//"paulopinav@gmail.com";
        private static readonly bool PRODUCCION = true;
        public static bool getProduccion()
        {
            return PRODUCCION;
        }
        public static string getRFC_DEFAULT()
        {
            return RFC_DEFAULT;
        }
        public static string getEmailPublicoGeneral()
        {
            return USER_EMAIL;
        }
        public static string getUserTimbrado()
        {
            return USER_TIMBRADO;
        }

        public static string getPassTimbrado()
        {
            return PASS_TIMBRADO;
        }
        public static string CadenaConexion
        {
            get
            {
                return Properties.Settings.Default.CadenaConexion;
            }
        }

        /*public static string Access_Token
        {
            get
            {
                return acessToken;
            }
            set
            {
                acessToken = value;
            }
        }
        public static string Refresh_Token
        {
            get
            {
                return refreshtoken;
            }
            set
            {
                refreshtoken = value;
            }
        }*/

        public static string getClientId
        {
            get
            {
                return clientID;
            }
        }
        public static string RedirectURL
        {
            get
            {
                return redirectURL;
            }
        }
        public static string getClientSecret
        {
            get
            {
                return clientSecret;
            }
        }
        public static string Usuario
        {
            get
            {
                return Properties.Settings.Default.Usuario;
            }
        }
        /*public static bool InicioLector
        {
            get
            {
                return Properties.Settings.Default.Lector;
            }
        }
        public static bool AgregarNumeroSerie
        {
            get
            {
                return Properties.Settings.Default.NumSerie;
            }
        }*/

        public static DevExpress.Utils.WaitDialogForm WaitDialog { get; set; }
        public static DateTime MinDate
        {
            get
            {
                return new DateTime(1900, 1, 1);
            }
        }

        /*public static string TOKEN
        {
            get
            {
                return Properties.Settings.Default.AccessToken;
            }
        }

        public static async Task<bool> CancelarPromocionesPasadas()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = DateTime.Now });
            bool resultado = (BaseDatos.ejecutarProcedimiento("Promocion_Pasadas_Borrar_sp", parametros) > 0);
            return resultado;
        }*/

        public static async void llenarDiccionarioFacturacion()
        {
            if (FormRecyclame._datosFacturacion.Cargar().Result)
            {
                if (FormRecyclame.requisitosFacturacion == null)
                {
                    FormRecyclame.requisitosFacturacion = new RequisitosFacturacion();
                }
                FormRecyclame.requisitosFacturacion.datosFacturacion = FormRecyclame._datosFacturacion;

            }
            if (FormRecyclame._ubicacionFiscal.Cargar().Result)
            {
                if (FormRecyclame.requisitosFacturacion == null)
                {
                    FormRecyclame.requisitosFacturacion = new RequisitosFacturacion();
                }
                FormRecyclame.requisitosFacturacion.ubicacionFiscal = FormRecyclame._ubicacionFiscal;
            }
            CertificadoDigital certificado = new CertificadoDigital();
            if (certificado.Cargar().Result)
            {
                if (FormRecyclame.requisitosFacturacion == null)
                {
                    FormRecyclame.requisitosFacturacion = new RequisitosFacturacion();
                }
                FormRecyclame.requisitosFacturacion.certificadoDigital = certificado;
            }
        }

        public static RequisitosFacturacion obtenerDatosFacturacionDefault()
        {
            if (FormRecyclame.dicFacturacion != null && FormRecyclame.dicFacturacion.Count > 0)
            {
                foreach (KeyValuePair<long, RequisitosFacturacion> kv in FormRecyclame.dicFacturacion)
                {
                    if (string.Compare(kv.Value.datosFacturacion.RFC, RFC_DEFAULT, true) == 0)
                    {
                        return kv.Value;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public static bool hayInternet(bool mostrarMensaje)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in nics)
            {
                if (string.Compare(nic.Name, "hamachi", true) == 0)
                {
                    continue;
                }

                if (
                    (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel) &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
            }
            if (mostrarMensaje)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Hay una falla en su conexión de internet.\nFavor de verificar su conexión de internet e intente más tarde.", Global.STR_NOMBRE_SISTEMA, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return false;
        }
        public static byte[] ImageToByte(System.Drawing.Image pImagen)
        {
            byte[] mImage = null;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            pImagen.Save(ms, pImagen.RawFormat);
            mImage = ms.GetBuffer();
            ms.Close();
            return mImage;
        }
        public static void moveFocusToNextControl(System.Windows.Forms.Keys KeyCode)
        {
            if (KeyCode == System.Windows.Forms.Keys.Enter)
            {
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        public static void moveFocusToNextControl()
        {
            System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        public static string DoubleToString(double dNumero)
        {
            return dNumero.ToString("N2", NumberFormatInfo.InvariantInfo);
        }

        public static double StringToDouble(string strNumero)
        {
            double dResultado = 0;

            double.TryParse(strNumero.Trim().Replace("$", ""), NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out dResultado);

            return dResultado;
        }

        public static string NumeroALetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = StringToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales < 10)
            {
                dec = " PESOS CON 0" + decimales.ToString() + "/100 MN";
            }
            else
            {
                dec = " PESOS CON " + decimales.ToString() + "/100 MN";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }
            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }

        public static int RowIndexClicked(DevExpress.XtraGrid.Views.Grid.GridView gridView1)
        {
            try
            {
                Point pt = gridView1.GridControl.PointToClient(Control.MousePosition);
                GridHitInfo info = gridView1.CalcHitInfo(pt);

                if (info.InRow || info.InRowCell)
                {
                    return info.RowHandle;
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, STR_NOMBRE_SISTEMA, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return -1;
        }

        public static string ObtenerEmpresaLabel(DatosFacturacion sucursal)
        {
            string strResultado = sucursal.Razon_Social;
            strResultado += Environment.NewLine;
            strResultado += "Suc. " + sucursal.Municipio;
            strResultado += Environment.NewLine;
            if (sucursal.NumExt.Length > 0)
            {
                if (sucursal.NumInt.Length > 0)
                {
                    strResultado += sucursal.Calle + " Ext. " + sucursal.NumExt + " Int. " + sucursal.NumInt;
                }
                else
                {
                    strResultado += sucursal.Calle + " " + sucursal.NumExt;
                }
            }
            else if (sucursal.NumInt.Length > 0)
            {
                strResultado += sucursal.Calle + " Int. " + sucursal.NumInt;
            }
            strResultado += Environment.NewLine;
            strResultado += sucursal.Colonia;
            strResultado += Environment.NewLine;
            strResultado += sucursal.Municipio + ", " + sucursal.Estado + "  C.P. " + sucursal.CodigoPostal;
            if (sucursal.Telefono.Trim().Length > 0)
            {
                strResultado += Environment.NewLine;
                strResultado += "Tel. " + sucursal.Telefono;
            }
            return strResultado;
        }

        public static bool EsCantidad(object strText)
        {
            bool bIsNumber = false;
            if (Convert.ToString(strText).Trim().Length > 0)
            {
                double dValue = 0;
                bIsNumber = Double.TryParse(Convert.ToString(strText).Trim(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out dValue);
            }

            return bIsNumber;
        }

        public static bool EsVentaId(object strText, ref string id)
        {
            bool bIsNumber = false;
            if (Convert.ToString(strText).Trim().Length > 0)
            {
                long lValue = 0;
                bIsNumber = long.TryParse(Convert.ToString(strText).Trim(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out lValue);
                if (lValue > 0)
                {
                    id = lValue.ToString();
                }
                else
                {
                    id = strText.ToString();
                }
            }

            return bIsNumber;
        }


        public static bool EsFolioId(object strText, ref string id)
        {
            bool bIsNumber = false;
            if (Convert.ToString(strText).Trim().Length > 0)
            {
                long lValue = 0;
                bIsNumber = long.TryParse(Convert.ToString(strText).Trim(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out lValue);
                if (lValue > 0)
                {
                    id = lValue.ToString();
                }
                else
                {
                    id = strText.ToString();
                }
            }

            return bIsNumber;
        }
        public static bool esMismoDia(DateTime fecha)
        {
            if (DateTime.Now.Year == fecha.Year && DateTime.Now.Month == fecha.Month && DateTime.Now.Day == fecha.Day)
            {
                return true;
            }
            return false;
        }

        public static bool esMismoDia(DateTime fecha, DateTime fecha2)
        {
            if (fecha.Year == fecha2.Year && fecha.Month == fecha2.Month && fecha.Day == fecha2.Day)
            {
                return true;
            }
            return false;
        }
        public static bool esFechaFinalValida(DateTime fechaInicio, DateTime fechaFin)
        {
            return (Convert.ToInt32((fechaFin - fechaInicio).TotalDays) >= 0);
        }
        public static bool esFechaInicioValida(DateTime fecha)
        {
            if (fecha.Year >= DateTime.Now.Year)
            {
                if (fecha.Year > DateTime.Now.Year)
                {
                    return true;
                }
                else
                {
                    if (fecha.Month >= DateTime.Now.Month)
                    {
                        if (fecha.Month > DateTime.Now.Month)
                        {
                            return true;
                        }
                        else
                        {
                            if (fecha.Day >= DateTime.Now.Day)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public static string ObtenerImpresoraTickets()
        {
            string strResultado = string.Empty;
            Preferencias preferencias = new Preferencias();
            if (preferencias.Cargar().Result)
            {
                if (preferencias != null)
                {
                    strResultado = preferencias.ImpresoraTickets;
                }

                bool bSeleccionarImpresora = false;

                if (strResultado == string.Empty)
                {
                    bSeleccionarImpresora = true;
                }
                else
                {
                    if (!EsImpresoraValida(strResultado))
                    {
                        bSeleccionarImpresora = true;
                    }
                }

                if (bSeleccionarImpresora)
                {
                    FrmImpresora frm = new FrmImpresora();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        strResultado = frm.ObtenerImpresora();

                        if (strResultado != string.Empty)
                        {
                            preferencias.ImpresoraTickets = strResultado;
                            preferencias.Grabar();
                        }
                    }
                }
            }
            return strResultado;
        }

        public static string ObtenerImpresoraFacturas()
        {
            string strResultado = string.Empty;
            Preferencias preferencias = new Preferencias();
            if (preferencias.Cargar().Result)
            {
                if (preferencias != null)
                {
                    strResultado = preferencias.ImpresoraFacturas;
                }

                bool bSeleccionarImpresora = false;

                if (strResultado == string.Empty)
                {
                    bSeleccionarImpresora = true;
                }
                else
                {
                    if (!EsImpresoraValida(strResultado))
                    {
                        bSeleccionarImpresora = true;
                    }
                }

                if (bSeleccionarImpresora)
                {
                    FrmImpresora frm = new FrmImpresora();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        strResultado = frm.ObtenerImpresora();

                        if (strResultado != string.Empty)
                        {
                            preferencias.ImpresoraFacturas = strResultado;
                            preferencias.Grabar();
                        }
                    }
                }
            }
            return strResultado;
        }

        public static List<string> ObtenerImpresoras()
        {
            List<string> lstImpresoras = new List<string>();

            foreach (string strImpresoraInstalada in PrinterSettings.InstalledPrinters)
            {
                lstImpresoras.Add(strImpresoraInstalada);
            }

            return lstImpresoras;
        }

        public static bool EsImpresoraValida(string strImpresora)
        {
            List<string> lstImpresoras = ObtenerImpresoras();

            if (lstImpresoras.Contains(strImpresora))
            {
                return true;
            }

            return false;
        }
        public static BindingList<object> CargarListaGrid(DataTable dt, string option)
        {
            BindingList<object> objs = null;
            object obj = null;
            double total = 0;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    /*if (string.Compare(option, "sucursal", true) == 0)
                    {
                        obj = new Sucursales();
                    }
                    else */
                    if (string.Compare(option, "empleado", true) == 0)
                    {
                        obj = new Empleado();
                    }
                    else if (string.Compare(option, "proveedor", true) == 0)
                    {
                        obj = new Provedor();
                    }
                    else if (string.Compare(option, "producto", true) == 0)
                    {
                        obj = new ProductoListado();
                    }
                    /*else if (string.Compare(option, "auditoria", true) == 0)
                    {
                        obj = new ProductoAuditoria();
                    }*/
                    else if (string.Compare(option, "cliente", true) == 0)
                    {
                        obj = new Cliente();
                    }
                    /*else if (string.Compare(option, "gasto", true) == 0)
                    {
                        obj = new GastoListado();
                    }
                    else if (string.Compare(option, "ordengasto", true) == 0)
                    {
                        obj = new OrdenGasto();
                    }*/
                    else if (string.Compare(option, "datosfacturacion", true) == 0)
                    {
                        obj = new DatosFacturacion();
                    }
                    else if (string.Compare(option, "metodospago", true) == 0)
                    {
                        obj = new MetodoPago();
                    }
                    else if (string.Compare(option, "ordenentrada", true) == 0)
                    {
                        obj = new ListadoEntradasInvenrtarios();
                    }
                    /*else if (string.Compare(option, "promocion", true) == 0)
                    {
                        obj = new Promocion();
                    }*/
                    if (objs == null)
                    {
                        objs = new BindingList<object>();
                    }
                    if (obj != null && ((ClaseBase)obj).Cargar(row))
                    {
                        objs.Add(obj);
                        /*if (string.Compare(option, "gasto", true) == 0)
                        {
                            total += ((GastoListado)obj).Total;
                        }*/
                    }
                    else
                    {
                        break;
                    }
                    obj = null;
                }
            }
            /*if (objs != null && objs.Count > 0 && string.Compare(option, "gasto", true) == 0)
            {
                obj = new GastoListado();
                ((GastoListado)obj).Concepto = "TOTAL DE GASTOS";
                ((GastoListado)obj).Total = total;
                ((GastoListado)obj).Fecha = DateTime.Now;
                objs.Add(obj);
                obj = null;
            }*/
            return objs;
        }

        public static BindingList<object> CargarListaGrid(DataTable dt, string option, ref DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            BindingList<object> objs = null;
            object obj = null;
            double total = 0;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    /*if (string.Compare(option, "sucursal", true) == 0)
                    {
                        obj = new Sucursales();
                    }
                    else if*/
                    if(string.Compare(option, "empleado", true) == 0)
                    {
                        obj = new Empleado();
                    }
                    else if (string.Compare(option, "proveedor", true) == 0)
                    {
                        obj = new Provedor();
                    }
                    else if (string.Compare(option, "producto", true) == 0)
                    {
                        obj = new ProductoListado();
                    }
                    /*else if (string.Compare(option, "auditoria", true) == 0)
                    {
                        obj = new ProductoAuditoria();
                    }*/
                    else if (string.Compare(option, "cliente", true) == 0)
                    {
                        obj = new Cliente();
                    }
                    /*else if (string.Compare(option, "gasto", true) == 0)
                    {
                        obj = new GastoListado();
                    }*/
                    if (objs == null)
                    {
                        objs = new BindingList<object>();
                    }
                    if (obj != null && ((ClaseBase)obj).Cargar(row))
                    {
                        objs.Add(obj);
                        if (string.Compare(option, "gasto", true) == 0)
                        {
                            //total += ((GastoListado)obj).Total;
                            //view.DataSource = ((GastoListado)obj).Detalles;
                        }
                    }
                    else
                    {
                        break;
                    }
                    obj = null;
                }
            }
            /*if (objs != null && objs.Count > 0 && string.Compare(option, "gasto", true) == 0)
            {
                obj = new GastoListado();
                ((GastoListado)obj).Concepto = "TOTAL DE GASTOS";
                ((GastoListado)obj).Total = total;
                ((GastoListado)obj).Fecha = DateTime.Now;
                objs.Add(obj);
                obj = null;
            }*/
            return objs;
        }

        public static bool ExportGridTodocument(DevExpress.XtraGrid.GridControl grid, string option)
        {
            if (grid.DataSource != null)
            {
                string sFiltro = string.Empty;
                if (string.Compare(option, "pdf", true) == 0)
                {
                    sFiltro = "Documento PDF (*.pdf)|*.pdf";
                }
                else if (string.Compare(option, "excel", true) == 0)
                {
                    sFiltro = "Libro Excel (*.xlsx)|*.xlsx";
                }
                else
                {
                    sFiltro = "Archivo CSV (*.csv)|*.csv";
                }
                using (SaveFileDialog Dir = new SaveFileDialog() { Filter = sFiltro })
                {
                    if (Dir.ShowDialog() == DialogResult.OK)
                    {
                        if (string.Compare(option, "pdf", true) == 0)
                        {
                            grid.ExportToPdf(Dir.FileNames[0]);
                        }
                        else if (string.Compare(option, "excel", true) == 0)
                        {
                            grid.ExportToXlsx(Dir.FileNames[0]);
                        }
                        else
                        {
                            grid.ExportToCsv(Dir.FileNames[0]);
                        }
                        return true;
                    }
                }
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("No hay ningún registro a exportar.");
            }
            return false;
        }

        public static bool hayInternet()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in nics)
            {
                if (string.Compare(nic.Name, "hamachi", true) == 0)
                {
                    continue;
                }

                if (
                    (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel) &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
            }
            return false;

        }
        /*public static bool refreshToken()
        {
            bool result = false;
            if (hayInternet())
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                request.Method = "POST";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*;q=0.8";
                request.Timeout = 10000;
                request.Headers.Add("Accept-Language", "en-us,en;q=0.5");
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
                request.KeepAlive = true;

                string strPosData = "client_id=" + clientID + "&client_secret=" + clientSecret + "&refresh_token=" + Refresh_Token + "&grant_type=refresh_token";
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
                            if (token.Length > 0)
                            {
                                Access_Token = token;
                                result = true;
                                Tokens t = new Tokens();
                                t.AccessToken = token;
                                t.RefreshToken = Refresh_Token;
                                t.Grabar();
                                t = null;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            }
            return result;
        }

        public static bool IsValidToken()
        {
            bool result = false;
            if (hayInternet())
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + Access_Token);
                request.Method = "GET";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*;q=0.8";
                request.Timeout = 10000;
                request.Headers.Add("Accept-Language", "en-us,en;q=0.5");
                //request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
                request.KeepAlive = true;
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        string strHTML = string.Empty;
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            strHTML = reader.ReadToEnd();
                            reader.Close();
                            string email = Utility.GetRegExParsedValue("email\": \"(?<RetVal>.*?)\"", strHTML);
                            if (email.Length > 0)
                            {
                                result = true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            }
            return result;
        }*/

        //public static Huella Huellas { get; set; }

        #region Facturacion
        /*public static bool enviarFactura(string correo, string pathxml, string pathpdf)
        {
            try
            {
                return Utility.enviarFactura(correo, pathxml, pathpdf);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return false;
        }

        public static bool enviarNotaCredito(string correo, string pathxml, string pathpdf, long facturaid)
        {
            try
            {
                try
                {
                    return Utility.enviarNotaCredito(correo, pathxml, pathpdf, facturaid);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
                return true;
            }
            catch (SmtpFailedRecipientException e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return false;
        }
        public static bool enviarCorteCaja(DateTime fecha, string pathpdf)
        {
            try
            {
                try
                {
                    return Utility.enviarCorteDeCaja(CORTE_EMAIL, fecha, pathpdf);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
                return true;
            }
            catch (SmtpFailedRecipientException e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return false;
        }*/

        public static string crearDirectorioCortesCaja()
        {
            string strFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Spazio";
            string strDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Spazio";
            if (!System.IO.Directory.Exists(strDirectory))
            {
                System.IO.Directory.CreateDirectory(strDirectory);
            }
            strDirectory = strDirectory + "\\CortesCaja";
            if (!System.IO.Directory.Exists(strDirectory))
            {
                System.IO.Directory.CreateDirectory(strDirectory);
            }
            return strDirectory;
        }

        public static void obtenerLugarDeExpedicion(ref XSDs.cfdv32.t_Ubicacion ubicacion, UbicacionFiscal _ubicacionFiscal, DatosFacturacion _datosFacturacion)
        {
            if (_ubicacionFiscal != null)
            {
                if (_ubicacionFiscal.Calle.Length > 0)
                {
                    ubicacion.calle = _ubicacionFiscal.Calle;
                }
                if (_ubicacionFiscal.CodigoPostal.Length > 0)
                {
                    ubicacion.codigoPostal = _ubicacionFiscal.CodigoPostal;
                }
                if (_ubicacionFiscal.Colonia.Length > 0)
                {
                    ubicacion.colonia = _ubicacionFiscal.Colonia;
                }
                if (_ubicacionFiscal.Estado.Length > 0)
                {
                    ubicacion.estado = _ubicacionFiscal.Estado;
                }
                if (_ubicacionFiscal.Localidad.Length > 0)
                {
                    ubicacion.localidad = _ubicacionFiscal.Localidad;
                }
                if (_ubicacionFiscal.Municipio.Length > 0)
                {
                    ubicacion.municipio = _ubicacionFiscal.Municipio;
                }
                if (_ubicacionFiscal.NumExt.Length > 0)
                {
                    ubicacion.noExterior = _ubicacionFiscal.NumExt;
                }
                else
                {
                    ubicacion.noExterior = "S/N";
                }
                if (_ubicacionFiscal.NumInt.Length > 0)
                {
                    ubicacion.noInterior = _ubicacionFiscal.NumInt;
                }
                else
                {
                    ubicacion.noInterior = "S/N";
                }
                ubicacion.pais = _ubicacionFiscal.Pais;
            }
            else
            {
                if (_datosFacturacion.Calle.Length > 0)
                {
                    ubicacion.calle = _datosFacturacion.Calle;
                }
                if (_datosFacturacion.CodigoPostal.Length > 0)
                {
                    ubicacion.codigoPostal = _datosFacturacion.CodigoPostal;
                }
                if (_datosFacturacion.Colonia.Length > 0)
                {
                    ubicacion.colonia = _datosFacturacion.Colonia;
                }
                if (_datosFacturacion.Estado.Length > 0)
                {
                    ubicacion.estado = _datosFacturacion.Estado;
                }
                if (_datosFacturacion.Localidad.Length > 0)
                {
                    ubicacion.localidad = _datosFacturacion.Localidad;
                }
                if (_datosFacturacion.Municipio.Length > 0)
                {
                    ubicacion.municipio = _datosFacturacion.Municipio;
                }
                if (_datosFacturacion.NumExt.Length > 0)
                {
                    ubicacion.noExterior = _datosFacturacion.NumExt;
                }
                else
                {
                    ubicacion.noExterior = "S/N";
                }
                if (_datosFacturacion.NumInt.Length > 0)
                {
                    ubicacion.noInterior = _datosFacturacion.NumInt;
                }
                else
                {
                    ubicacion.noInterior = "S/N";
                }
                ubicacion.pais = _datosFacturacion.Pais;
            }
        }

        public static string obtenerLugarDeExpedicion(UbicacionFiscal ubicacion, DatosFacturacion empresa)
        {
            if (ubicacion != null)
            {
                return ubicacion.Municipio + ", " + ubicacion.Estado;
            }
            else
            {
                return empresa.Municipio + ", " + empresa.Estado;
            }
        }

        public static bool CancelarNotaCredito(DatosFacturacion empresa, CertificadoDigital certificado, FacturaNotaCredito nota, ref string message, ref int error)
        {
            return CancelacionNotaCredito(empresa, certificado, nota, ref message, ref error);
        }

        public static bool CancelarFactura(DatosFacturacion empresa, CertificadoDigital certificado, FacturaDigital factura, ref string message, ref int error)
        {
            return CancelacionFactura(empresa, certificado, factura, ref message, ref error);
        }
        private static bool CancelacionNotaCredito(DatosFacturacion empresa, CertificadoDigital certificado, FacturaNotaCredito factura, ref string message, ref int error)
        {
            try
            {
                if (File.Exists(certificado.RutaCertificado) && File.Exists(certificado.RutaClave))
                {
                    if (esCertificadoValido(empresa, certificado, ref error))
                    {
                        Random r = new Random();
                        int ntimes = 0;
                        string pac = string.Empty;
                    Again:
                        try
                        {
                            pac = r.Next(1, 9).ToString();
                            WSMFRespuesta respuesta = WSMultifacturas.Cancelar(pac, Global.getUserTimbrado(), Global.getPassTimbrado(), factura.UUID, certificado.RutaCertificado, certificado.RutaClave, certificado.Password);
                            if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(respuesta.CodigoMFTexto, "UUID CANCELADO CORRECTAMENTE", CompareOptions.IgnoreCase) != -1)
                            {
                                return true;
                            }
                            else
                            {
                                if (ntimes < 8)
                                {
                                    ntimes++;
                                    goto Again;
                                }
                                else
                                {
                                    message = "La nota de crédito no se ha podido cancelar debido a " + mensajeErroWEbService(respuesta.CodigoMFNumero, respuesta.codigo_mf_texto);
                                    return false;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine(e.ToString());
                            if (ntimes < 8)
                            {
                                ntimes++;
                                goto Again;
                            }
                            else
                            {
                                message = "La nota de crédito no se ha podido cancelar debido a " + e.ToString();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (error == 1)
                        {
                            message = "La nota de crédito no pudo ser generada debido a que el certificado digital no corresponde al del rfc fiscal del emisor.\nFavor de asignar el certificado digital correspondiente al emisor.";
                        }
                        else
                        {
                            error = 1;
                            message = "La nota de crédito no ha sido cancelada debido a que el certificado digital no es válido o ha expirado.\nFavor de proporcionar un certificado válido.\nVaya a Reportes, seleccione Facturas e intente nuevamente.";
                        }
                    }
                }
                else
                {
                    if (error == 1)
                    {
                        message = "La nota de crédito no pudo ser generada debido a que el certificado digital no corresponde al del rfc fiscal del emisor.\nFavor de asignar el certificado digital correspondiente al emisor.";
                    }
                    else
                    {
                        error = 1;
                        message = "La nota de crédito no ha sido cancelada debido a que el certificado y/o la llave digital no existe(n).\nFavor de proporcionar un certificado y/o llave digital válida.\nVaya a Reportes, seleccione Facturas e intente nuevamente.";
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return false;
        }
        private static bool CancelacionFactura(DatosFacturacion empresa, CertificadoDigital certificado, FacturaDigital factura, ref string message, ref int error)
        {
            try
            {
                if (File.Exists(certificado.RutaCertificado) && File.Exists(certificado.RutaClave))
                {
                    if (esCertificadoValido(empresa, certificado, ref error))
                    {
                        Random r = new Random();
                        int ntimes = 0;
                        string pac = string.Empty;
                    Again:
                        try
                        {
                            //string uuid = "8b722f17-855c-4fde-8bee-81da41e2433e";
                            pac = r.Next(1, 9).ToString();
                            //WSMFRespuesta respuesta = WSMultifacturas.Cancelar(pac, Global.getUserTimbrado(), Global.getPassTimbrado(), uuid, certificado.RutaCertificado, certificado.RutaClave, certificado.Password);
                            WSMFRespuesta respuesta = WSMultifacturas.Cancelar(pac, Global.getUserTimbrado(), Global.getPassTimbrado(), factura.UUID, certificado.RutaCertificado, certificado.RutaClave, certificado.Password);
                            if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(respuesta.CodigoMFTexto, "UUID CANCELADO CORRECTAMENTE", CompareOptions.IgnoreCase) != -1)
                            {
                                return true;
                            }
                            else
                            {
                                if (ntimes < 8)
                                {
                                    ntimes++;
                                    goto Again;
                                }
                                else
                                {
                                    message = "La factura no se ha podido cancelar debido a " + mensajeErroWEbService(respuesta.CodigoMFNumero, respuesta.codigo_mf_texto);
                                    return false;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine(e.ToString());
                            if (ntimes < 8)
                            {
                                ntimes++;
                                goto Again;
                            }
                            else
                            {
                                message = "La factura no se ha podido cancelar debido a " + e.ToString();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (error == 1)
                        {
                            message = "La factura no pudo ser generada debido a que el certificado digital no corresponde al del rfc fiscal del emisor.\nFavor de asignar el certificado digital correspondiente al emisor.";
                        }
                        else
                        {
                            error = 1;
                            message = "La factura no ha sido cancelada debido a que el certificado digital no es válido o ha expirado.\nFavor de proporcionar un certificado válido.\nVaya a Reportes, seleccione Facturas e intente nuevamente.";
                        }
                    }
                }
                else
                {
                    if (error == 1)
                    {
                        message = "La factura no pudo ser generada debido a que el certificado digital no corresponde al del rfc fiscal del emisor.\nFavor de asignar el certificado digital correspondiente al emisor.";
                    }
                    else
                    {
                        error = 1;
                        message = "La factura no ha sido cancelada debido a que el certificado y/o la llave digital no existe(n).\nFavor de proporcionar un certificado y/o llave digital válida.\nVaya a Reportes, seleccione Facturas e intente nuevamente.";
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return false;
        }

        private static string mensajeErroWEbService(string code, string exception)
        {
            string error = string.Empty;
            switch (code)
            {
                case "0":
                    error = exception;
                    break;
                case "1":
                    error = "saldo insufuciente con su servicio de timbrado.\nFavor de consultar su saldo con su disribuidor de servicio de timbrado.";
                    break;
                case "2":
                    error = "un error al timbrar.\n" + exception;
                    break;
                case "3":
                    error = "un error al conectarse al servidor de timbrado.\nCheque su conexión a internet e intentelo más tarde.";
                    break;
                case "4":
                    error = "un error de autentificación con su servicio de timbrado.\nFavor de reportarlo con su distribuidor de servicio de timbrado.\nEspere un momento (entre 10-20 min) e intente nuevamente.";
                    break;
                case "5":
                    error = "un error de autentificación con su servicio de timbrado.\nFavor de verificar que su usuario y contraseña\nsean los mismos que los le proporcionó su\ndistribuidor de servicio de timbrado.";
                    break;
                case "6":
                    error = "un error al timbrar por parte de su distribuidor de servicio de timbrado.\nfavor de reportarlo con su distribuidor.\nEspere un momento (entre 10-20 min) e intente nuevamente.";
                    break;
                case "7":
                    error = "un error interno del servidor.\n" + exception;
                    break;
                case "8":
                    error = "un error al generar el ticket.";
                    break;
            }
            return error;
        }

        public static double obtenerDecimalesCantidad(double cantidad)
        {
            long valorSinDecimal = (long)cantidad;
            return (cantidad - (double)valorSinDecimal);
        }
        public static decimal obtenerPorcentajeDecuento(double precionormal, double preciodescuento)
        {
            return decimal.Round(((Convert.ToDecimal(preciodescuento) * 100M) / Convert.ToDecimal(precionormal)), 2);
        }
        public static bool esCertificadoValido(DatosFacturacion empresa, CertificadoDigital certificado, ref int error)
        {
            X509Certificate2 x509 = null;
            try
            {
                x509 = new X509Certificate2(certificado.RutaCertificado);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            if (x509 != null)
            {
                DateTime dtExpiration = x509.NotAfter;
                DateTime dtNow = DateTime.Now;
                if (dtExpiration.Year >= dtNow.Year)
                {
                    string RFC = empresa.RFC;
                    if (dtExpiration.Year > dtNow.Year)
                    {
                        if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(x509.Subject, RFC, CompareOptions.IgnoreCase) != -1)
                        {
                            return true;
                        }
                        else
                        {
                            error = 1;
                            return false;
                        }
                    }
                    else
                    {
                        if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(x509.Subject, RFC, CompareOptions.IgnoreCase) != -1)
                        {
                            if (dtExpiration.Month > dtNow.Month)
                            {
                                return true;
                            }
                            else
                            {
                                if (dtExpiration.Month == dtNow.Month)
                                {
                                    if (dtExpiration.Day >= dtNow.Day)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            error = 1;
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion facturacion
    }
}
