using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class FacturaVenta : ClaseBase
    {
        private long _lId = 0;
        private long _lFacturaId = 0;
        private long _lVentaId = 0;

        public FacturaVenta()
        {
            Inicializar();
        }

        public void Inicializar()
        {
            QueryGrabar = "FacturaVenta_Grabar_sp";
            QueryConsultar = "FacturaVenta_Consultar_sp";
            this.Id = 0;
            this.FacturaId = 0;
            this.VentaId = 0;
        }

        public long Id
        {
            get { return _lId; }
            set { _lId = value; }
        }

        public bool Cancelada
        {
            get;
            set;
        }

        public long FacturaId
        {
            get { return _lFacturaId; }
            set { _lFacturaId = value; }
        }

        public long VentaId
        {
            get { return _lVentaId; }
            set { _lVentaId = value; }
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
                Id = Convert.ToInt64(row["IdFacturaVenta"]);
                FacturaId = Convert.ToInt64(row["IdFactura"]);
                VentaId = Convert.ToInt64(row["IdVenta"]);
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
