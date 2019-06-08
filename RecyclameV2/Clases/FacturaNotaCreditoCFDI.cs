using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class FacturaNotaCreditoCFDI : ClaseBase
    {
        private long FacturaCFDI_Id = 0;
        private long facturaId = 0;
        private DateTime _dtFecha = Global._dtDefaultDateTime;
        private long _lClienteId = 0;
        private string _strClienteNombreFiscal = string.Empty;
        private string _strClienteCalle = string.Empty;
        private string _strClienteNumeroExterior = string.Empty;
        private string _strClienteNumeroInterior = string.Empty;
        private string _strClienteColonia = string.Empty;
        private string _strClienteCodigoPostal = string.Empty;
        private string _strClienteCiudad = string.Empty;
        private string _strClienteEstado = string.Empty;
        private string _strClienteRFC = string.Empty;
        private string _strCantidadLetra = string.Empty;
        private string _strEmail = string.Empty;
        private string _strCadenaOriginal = string.Empty;
        private string _strRutaXml = string.Empty;
        private string _strRutaPdf = string.Empty;
        private double _dSubTotal = 0;
        private double _dIvaPorcentaje = 0;
        private double _dIvaImporte = 0;
        private double _dIepsPorcentaje = 0;
        private double _dIepsImporte = 0;
        private double _dTotal = 0;
        public FacturaNotaCreditoCFDI()
        {
            QueryGrabar = "FacturaNotaCreditoCFDI_Grabar_sp";
            QueryConsultar = "FacturaNotaCreditoCFDI_Consultar_sp";
            XML = string.Empty;
            Folio_Fiscal = string.Empty;
            PDF = string.Empty;
        }

        public long FacturaCFDIId
        {
            get { return FacturaCFDI_Id; }
            set { FacturaCFDI_Id = value; }
        }
        public long FacturaId
        {
            get { return facturaId; }
            set { facturaId = value; }
        }
        public string Folio_Fiscal { get; set; }
        public string XML { get; set; }
        public string PDF { get; set; }
        public string CadenaOriginal
        {
            get { return _strCadenaOriginal; }
            set { _strCadenaOriginal = value; }
        }
        public DateTime Fecha_Timbrado
        {
            get { return _dtFecha; }
            set { _dtFecha = value; }
        }

        public string Email
        {
            get { return _strEmail; }
            set { _strEmail = value; }
        }

        public string RutaXml
        {
            get { return _strRutaXml; }
            set { _strRutaXml = value; }
        }

        public string RutaPdf
        {
            get { return _strRutaPdf; }
            set { _strRutaPdf = value; }
        }

        public long ClienteId
        {
            get { return _lClienteId; }
            set { _lClienteId = value; }
        }

        public string ClienteNombreFiscal
        {
            get { return _strClienteNombreFiscal; }
            set { _strClienteNombreFiscal = value; }
        }

        public string ClienteCalle
        {
            get { return _strClienteCalle; }
            set { _strClienteCalle = value; }
        }

        public string ClienteNumeroExterior
        {
            get { return _strClienteNumeroExterior; }
            set { _strClienteNumeroExterior = value; }
        }

        public string ClienteNumeroInterior
        {
            get { return _strClienteNumeroInterior; }
            set { _strClienteNumeroInterior = value; }
        }

        public string ClienteColonia
        {
            get { return _strClienteColonia; }
            set { _strClienteColonia = value; }
        }

        public string ClienteCodigoPostal
        {
            get { return _strClienteCodigoPostal; }
            set { _strClienteCodigoPostal = value; }
        }

        public string ClienteCiudad
        {
            get { return _strClienteCiudad; }
            set { _strClienteCiudad = value; }
        }

        public string ClienteEstado
        {
            get { return _strClienteEstado; }
            set { _strClienteEstado = value; }
        }

        public string ClienteRFC
        {
            get { return _strClienteRFC; }
            set { _strClienteRFC = value; }
        }

        public string CantidadLetra
        {
            get { return _strCantidadLetra; }
            set { _strCantidadLetra = value; }
        }

        public double SubTotal
        {
            get { return _dSubTotal; }
            set { _dSubTotal = value; }
        }

        public double IvaPorcentaje
        {
            get { return _dIvaPorcentaje; }
            set { _dIvaPorcentaje = value; }
        }

        public double IvaImporte
        {
            get { return _dIvaImporte; }
            set { _dIvaImporte = value; }
        }

        public double IepsImporte
        {
            get { return _dIepsImporte; }
            set { _dIepsImporte = value; }
        }

        public double IepsPorcentaje
        {
            get { return _dIepsPorcentaje; }
            set { _dIepsPorcentaje = value; }
        }
        public double Total
        {
            get { return _dTotal; }
            set { _dTotal = value; }
        }

        /// <summary>
        /// Carga en los controles la informacion de un registro.
        /// </summary>
        /// <param name="row">
        /// Row con la información a cargar
        /// </param>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        public override bool Cargar(System.Data.DataRowView row)
        {
            return Cargar(row.Row);
        }

        /// <summary>
        /// Carga en los controles la informacion de un registro.
        /// </summary>
        /// <param name="row">
        /// Row con la información a cargar
        /// </param>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        public override bool Cargar(System.Data.DataRow row)
        {
            bool resultado = true;

            try
            {
                System.Data.DataColumnCollection columns = row.Table.Columns;
                if (columns.Contains("IdFacturaCFDI"))
                {
                    FacturaCFDIId = Convert.ToInt64(row["IdFacturaCFDI"]);
                }
                if (row.Table.Columns.Contains("IdFacturaNotaCredito"))
                {
                    FacturaId = Convert.ToInt64(row["IdFacturaNotaCredito"]);
                }
                else if (columns.Contains("IdFactura"))
                {
                    FacturaId = Convert.ToInt64(row["IdFactura"]);
                }
                if (columns.Contains("UUID"))
                {
                    Folio_Fiscal = Convert.ToString(row["UUID"]);
                }
                if (columns.Contains("PDFPath"))
                {
                    RutaPdf = Convert.ToString(row["PDFPath"]);
                }
                if (columns.Contains("XMLPath"))
                {
                    RutaXml = Convert.ToString(row["XMLPath"]);
                }
                if (columns.Contains("XMLString"))
                {
                    XML = Convert.ToString(row["XMLString"]);
                }
                if (columns.Contains("PDFString"))
                {
                    PDF = Convert.ToString(row["PDFString"]);
                }
                if (columns.Contains("ClienteId"))
                {
                    ClienteId = Convert.ToInt64(row["ClienteId"]);
                }
                if (columns.Contains("ClienteNombrefiscal"))
                {
                    ClienteNombreFiscal = Convert.ToString(row["ClienteNombrefiscal"]);
                }
                if (columns.Contains("ClienteCalle"))
                {
                    ClienteCalle = Convert.ToString(row["ClienteCalle"]);
                }
                if (columns.Contains("ClienteNumeroExterior"))
                {
                    ClienteNumeroExterior = Convert.ToString(row["ClienteNumeroExterior"]);
                }
                if (columns.Contains("ClienteNumeroInterior"))
                {
                    ClienteNumeroInterior = Convert.ToString(row["ClienteNumeroInterior"]);
                }
                if (columns.Contains("ClienteColonia"))
                {
                    ClienteColonia = Convert.ToString(row["ClienteColonia"]);
                }
                if (columns.Contains("ClienteCodigoPostal"))
                {
                    ClienteCodigoPostal = Convert.ToString(row["ClienteCodigoPostal"]);
                }
                if (columns.Contains("ClienteCiudad"))
                {
                    ClienteCiudad = Convert.ToString(row["ClienteCiudad"]);
                }
                if (columns.Contains("ClienteRFC"))
                {
                    ClienteRFC = Convert.ToString(row["ClienteRFC"]);
                }
                if (columns.Contains("Email"))
                {
                    Email = Convert.ToString(row["Email"]);
                }
                if (columns.Contains("Total"))
                {
                    Total = Convert.ToDouble(row["Total"]);
                }
                if (columns.Contains("SubTotal"))
                {
                    SubTotal = Convert.ToDouble(row["SubTotal"]);
                }
                if (columns.Contains("IVA"))
                {
                    IvaPorcentaje = Convert.ToDouble(row["IVA"]);
                }
                if (columns.Contains("IEPS"))
                {
                    IepsPorcentaje = Convert.ToDouble(row["IEPS"]);
                }
                if (columns.Contains("IVAImporte"))
                {
                    IvaImporte = Convert.ToDouble(row["IVAImporte"]);
                }
                if (columns.Contains("IEPSImporte"))
                {
                    IepsImporte = Convert.ToDouble(row["IEPSImporte"]);
                }
                if (columns.Contains("FechaTimbrado"))
                {
                    Fecha_Timbrado = Convert.ToDateTime(row["FechaTimbrado"]);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                resultado = false;
            }

            return resultado;
        }
    }
}
