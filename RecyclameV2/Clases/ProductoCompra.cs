using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class ProductoCompra : ClaseBase
    {
        private Productos _producto = null;
        public long Producto_Compra_Id { get; set; }
        public long Producto_Id { get; set; }
        public long Compra_Id { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad_Compra { get; set; }
        public List<string> lstSeries { get; set; }
        private double _cantidad_empaque;
        private double empaque;
        public double Cantidad_Empaque
        {
            get
            {
                return _cantidad_empaque;
            }
            set { _cantidad_empaque = value; }
        }
        public double Cantidad { get; set; }
        public double ValorUnitarioOriginal { get; set; }
        public string Codigo_Barras { get; set; }
        public string Unidad { get; set; }
        public string Numero_Serie { get; set; }
        public double Valor_Unitario { get; set; }
        public double Importe { get; set; }
        public double Ultimo_Costo { get; set; }
        private double _diferencia_costo;
        public double Diferencia_Costo
        {
            get
            {
                try
                {
                    double costoActual = Valor_Unitario;

                    if (Ultimo_Costo > 0 && costoActual > 0)
                    {
                        _diferencia_costo = (Math.Round(Ultimo_Costo, 2) - costoActual) * 100.00 / costoActual;
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return _diferencia_costo;
            }
        }
        public double Descuento_Porciento { get; set; }
        public double Descuento_Monto { get; set; }
        public double IVA_Tasa { get; set; }
        public double IVA_Monto { get; set; }
        public double IEPS_Tasa { get; set; }
        public double IEPS_Monto { get; set; }
        public bool TieneNumeroSerie { get; set; }
        public ProductoCompra() : this(-1)
        {
        }

        public ProductoCompra(long cfds_id)
        {
            CampoId = "Producto_Compra_Id";
            CampoBusqueda = "Descripcion";
            QueryGrabar = "Producto_CompraGrabar_sp";
            QueryConsultar = "Producto_CompraConsultar_sp";
            QueryBorrar = "Producto_CompraBorrar_sp";
            lstSeries = new List<string>();
            Producto_Id = -1;
            Compra_Id = cfds_id;
            Cantidad = 0.00;
            Unidad = "";
            Codigo_Barras = "";
            Numero_Serie = string.Empty;
            Producto_Compra_Id = -1;
            Descripcion = "";
            Valor_Unitario = 0.00;
            Importe = 0.00;
            Producto_Id = -1;
            Ultimo_Costo = 0;
            _diferencia_costo = 0;
            Cantidad_Compra = 0;
            ValorUnitarioOriginal = 0;
            empaque = 1;
            _cantidad_empaque = 0;
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
                Producto_Compra_Id = Convert.ToInt64(row["CompraDetalleId"]);
                Producto_Id = Convert.ToInt64(row["Producto_Id"]);
                Compra_Id = Convert.ToInt64(row["compra_Id"]);
                Cantidad = Convert.ToDouble(row["Cantidad"]);
                Unidad = Convert.ToString(row["Unidad"]);
                Descripcion = Convert.ToString(row["Descripcion"]);
                Valor_Unitario = Convert.ToDouble(row["Valor_Unitario"]);
                ValorUnitarioOriginal = Valor_Unitario;
                Importe = Convert.ToDouble(row["Importe"]);
                Producto_Id = Convert.ToInt64(row["Producto_Id"]);
                Cantidad_Compra = Convert.ToDouble(row["Cantidad_Factura"]);
                Descuento_Porciento = Convert.ToDouble(row["Descuento_Porciento"]);
                Descuento_Monto = Convert.ToDouble(row["Descuento_Monto"]);
                IVA_Tasa = Convert.ToDouble(row["Impuesto_Tasa"]);
                IVA_Monto = Convert.ToDouble(row["Impuesto_Monto"]);

                empaque = Convert.ToDouble(row["Cantidad_Empaque"]);
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
