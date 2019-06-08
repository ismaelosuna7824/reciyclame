using RecyclameV2.Clases;
using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.PAC
{
    public class FacturaDigital : ClaseBase
    {
        private long facturaId = 0;
        private long iddatosfiscales = 0;
        private string _strNo = string.Empty;
        private DateTime _dtFecha = Global._dtDefaultDateTime;
        private bool activa = false;
        private FacturaCFDI cfdi = null;
        private List<FacturaProductos> _lstProductos = new List<FacturaProductos>();
        private List<FacturaVenta> _lstFacturaVentas = new List<FacturaVenta>();
        public FacturaDigital()
        {
            Inicializar();
        }

        public void Inicializar()
        {
            QueryGrabar = "FacturaDigital_Grabar_sp";
            QueryConsultar = "FacturaDigital_Consultar_sp";
            QueryBorrar = "FacturaDigital_Borrar_sp";
            this.FacturaId = 0;
            this.UUID = string.Empty;
            this.Fecha = Global._dtDefaultDateTime;
            Activa = true;
            this.CFDI = new FacturaCFDI();
            this.FacturaProductos = new List<FacturaProductos>();
            this.FacturaVentas = new List<FacturaVenta>();
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

        public FacturaCFDI CFDI
        {
            get { return cfdi; }
            set { cfdi = value; }
        }
        public bool Activa
        {
            get { return activa; }
            set { activa = value; }
        }

        public List<FacturaProductos> FacturaProductos
        {
            get { return _lstProductos; }
            set { _lstProductos = value; }
        }
        public List<FacturaVenta> FacturaVentas
        {
            get { return _lstFacturaVentas; }
            set { _lstFacturaVentas = value; }
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
                if (row.Table.Columns.Contains("IdFactura"))
                {
                    FacturaId = Convert.ToInt64(row["IdFactura"]);
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
            }
            catch (Exception ex)
            {
                //Logger.Error(ex, ex.Message);
                resultado = false;
            }

            return resultado;
        }
    }
}
