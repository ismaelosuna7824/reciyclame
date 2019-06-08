using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class FacturaProductos : ClaseBase
    {
        private long _lId = 0;
        private long _lFacturaId = 0;
        private double _dCantidad = 0;
        private string _strUnidadMedida = string.Empty;
        private long _lProductoId = 0;
        private string _strCaracteristicas = string.Empty;
        private double _dPrecio = 0;
        private double _dTotal = 0;
        private double _dIva = 0;
        private double _dIeps = 0;
        private double _dIvaporcentaje = 0;
        private double _dIepsporcentaje = 0;
        public FacturaProductos()
        {
            Inicializar();
        }

        public void Inicializar()
        {
            QueryGrabar = "FacturaProductos_Grabar_sp";
            QueryConsultar = "FacturaProductos_Consultar_sp";
            this.Id = 0;
            this.FacturaId = 0;
            this.Cantidad = 0;
            this.UnidadMedida = string.Empty;
            this.ProductoId = 0;
            this.Descripcion = string.Empty;
            this.Precio = 0;
            this.Total = 0;
            this.IVA = 0;
            //this.EsUnidadMayoreo = false;
        }

        //public bool EsUnidadMayoreo
        //{
        //    get { return esUnidadMayoreo; }
        //    set { esUnidadMayoreo = value; }
        //}

        public string Descripcion
        {
            get { return _strCaracteristicas; }
            set { _strCaracteristicas = value; }
        }

        public long Id
        {
            get { return _lId; }
            set { _lId = value; }
        }

        public long FacturaId
        {
            get { return _lFacturaId; }
            set { _lFacturaId = value; }
        }

        public double Cantidad
        {
            get { return _dCantidad; }
            set { _dCantidad = value; }
        }

        public string UnidadMedida
        {
            get { return _strUnidadMedida; }
            set { _strUnidadMedida = value; }
        }

        public long ProductoId
        {
            get { return _lProductoId; }
            set { _lProductoId = value; }
        }

        public double Precio
        {
            get { return _dPrecio; }
            set { _dPrecio = value; }
        }

        public double Total
        {
            get { return _dTotal; }
            set { _dTotal = value; }
        }

        public double IVA
        {
            get { return _dIva; }
            set { _dIva = value; }
        }

        public double IVAPorcentaje
        {
            get { return _dIvaporcentaje; }
            set { _dIvaporcentaje = value; }
        }

        public double IEPS
        {
            get { return _dIeps; }
            set { _dIeps = value; }
        }

        public double IEPsPorcentaje
        {
            get { return _dIepsporcentaje; }
            set { _dIepsporcentaje = value; }
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
                System.Data.DataColumnCollection columns = row.Table.Columns;
                if (columns.Contains("IdFacturaProducto"))
                {
                    Id = Convert.ToInt64(row["IdFacturaProducto"]);
                }
                if (columns.Contains("IdFactura"))
                {
                    FacturaId = Convert.ToInt64(row["IdFactura"]);
                }
                if (columns.Contains("IdProducto"))
                {
                    ProductoId = Convert.ToInt64(row["IdProducto"]);
                }
                if (columns.Contains("Cantidad"))
                {
                    Cantidad = Convert.ToDouble(row["Cantidad"]);
                }
                if (columns.Contains("total"))
                {
                    Total = Convert.ToDouble(row["total"]);
                }
                if (columns.Contains("Precio"))
                {
                    Precio = Convert.ToDouble(row["Precio"]);
                }
                if (columns.Contains("UnidadMedida"))
                {
                    UnidadMedida = Convert.ToString(row["UnidadMedida"]);
                }
                if (columns.Contains("Descripcion"))
                {
                    Descripcion = Convert.ToString(row["Descripcion"]);
                }
                if (columns.Contains("IVA"))
                {
                    IVAPorcentaje = Convert.ToDouble(row["IVA"]);
                }
                if (columns.Contains("IEPS"))
                {
                    IEPsPorcentaje = Convert.ToDouble(row["IEPS"]);
                }
                if (columns.Contains("IVAImporte"))
                {
                    IVA = Convert.ToDouble(row["IVAImporte"]);
                }
                if (columns.Contains("IEPSImporte"))
                {
                    IEPS = Convert.ToDouble(row["IEPSImporte"]);
                }
                resultado = true;
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
