using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class FacturaNotaCredito : ClaseBase
    {
        private long facturaNotaCreditoId = 0;
        private long facturaId = 0;
        private long iddatosfiscales = 0;
        private string _strNo = string.Empty;
        private DateTime _dtFecha = Global._dtDefaultDateTime;
        private bool activa = false;
        private FacturaNotaCreditoCFDI cfdi = null;
        public FacturaNotaCredito()
        {
            Inicializar();
        }
        public void Inicializar()
        {
            QueryGrabar = "FacturaNotaCredito_Grabar_sp";
            QueryConsultar = "FacturaNotaCredito_Consultar_sp";
            QueryBorrar = "FacturaNotaCredito_Borrar_sp";
            this.FacturaId = 0;
            this.UUID = string.Empty;
            this.Fecha = Global._dtDefaultDateTime;
            Activa = true;
            this.CFDI = new FacturaNotaCreditoCFDI();
        }
        public long FacturaNotaCreditoId
        {
            get { return facturaNotaCreditoId; }
            set { facturaNotaCreditoId = value; }
        }

        public long FacturaId
        {
            get { return facturaId; }
            set { facturaId = value; }
        }

        public long IdDatosFiscales
        {
            get { return iddatosfiscales; }
            set { iddatosfiscales = value; }
        }
        public string UUID
        {
            get { return _strNo; }
            set { _strNo = value; }
        }

        public DateTime Fecha
        {
            get { return _dtFecha; }
            set { _dtFecha = value; }
        }

        public FacturaNotaCreditoCFDI CFDI
        {
            get { return cfdi; }
            set { cfdi = value; }
        }
        public bool Activa
        {
            get { return activa; }
            set { activa = value; }
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
            bool resultado = false;

            try
            {
                if (row.Table.Columns.Contains("IdFacturaNotaCredito"))
                {
                    FacturaNotaCreditoId = Convert.ToInt64(row["IdFacturaNotaCredito"]);
                    resultado = true;
                }
                if (row.Table.Columns.Contains("IdFactura"))
                {
                    FacturaId = Convert.ToInt64(row["IdFactura"]);
                    resultado = true;
                }
                else if (row.Table.Columns.Contains("Folio_Factura_Cancelada"))
                {
                    FacturaId = Convert.ToInt64(row["Folio_Factura_Cancelada"]);
                    resultado = true;
                }
                if (row.Table.Columns.Contains("UUID"))
                {
                    UUID = Convert.ToString(row["UUID"]);
                    resultado = true;
                }
                if (row.Table.Columns.Contains("Fecha"))
                {
                    Fecha = Convert.ToDateTime(row["Fecha"]);
                }
                if (row.Table.Columns.Contains("Status"))
                {
                    Activa = Convert.ToBoolean(row["Status"]);
                }
                if (row.Table.Columns.Contains("IdDatosFiscales"))
                {
                    IdDatosFiscales = Convert.ToInt64(row["IdDatosFiscales"]);
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
