using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Entradas : ClaseBase
    {
        public bool Agregar { get; set; }
        public long CFDS_Producto_Id { get; set; }
        public long CFDS_Id { get; set; }
        public string Numero_Identificacion { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad_Factura { get; set; }
        public bool isCheckedCantidad_Empaque { get; set; }
        private double _cantidad_empaque;
        public double Cantidad_Empaque { get; set; }
        public double Cantidad { get; set; }
        public double ValorUnitarioOriginal { get; set; }
        public string Unidad { get; set; }
        public string Numero_Serie { get; set; }
        public double Valor_Unitario { get; set; }
        public double Importe { get; set; }
        public long Producto_Id { get; set; }
        public bool isCheckedCodigo_Producto { get; set; }
        private string _codigo_producto;
        public string Codigo_Producto { get; set; }
        public double Descuento_Porciento { get; set; }
        public double Descuento_Monto { get; set; }
        public double Impuesto_Tasa { get; set; }
        public double Impuesto_Monto { get; set; }
        public Entradas()
        {

        }
        public Entradas(long cfds_id)
        {
            Agregar = true;
            TipoClase = ClaseTipo.CFDS_Producto;
            CampoId = "CFDS_Producto_Id";
            CampoBusqueda = "Descripcion";
            QueryGrabar = "CFDS_Producto_Grabar_sp";
            QueryGrabarCodigo = "CFDS_Producto_Grabar_sp_codigo";
            QueryConsultar = "CFDS_Producto_Consultar_sp";
            QueryBorrar = "CFDS_Producto_Borrar_sp";
            CFDS_Producto_Id = -1;
            CFDS_Id = cfds_id;
            Cantidad = 0.00;
            Unidad = "";
            Numero_Serie = string.Empty;
            Numero_Identificacion = "";
            Descripcion = "";
            Valor_Unitario = 0.00;
            Importe = 0.00;
            Producto_Id = -1;
            Cantidad_Factura = 0;
            ValorUnitarioOriginal = 0;
            _cantidad_empaque = 1;
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
                CFDS_Producto_Id = Convert.ToInt64(row["CFDS_Producto_Id"]);
                CFDS_Id = Convert.ToInt64(row["CFDS_Id"]);
                Cantidad = Convert.ToDouble(row["Cantidad"]);
                Unidad = Convert.ToString(row["Unidad"]);
                Numero_Identificacion = Convert.ToString(row["Numero_Identificacion"]);
                Descripcion = Convert.ToString(row["Descripcion"]);
                Valor_Unitario = Convert.ToDouble(row["Valor_Unitario"]);
                ValorUnitarioOriginal = Valor_Unitario;
                Importe = Convert.ToDouble(row["Importe"]);
                Producto_Id = Convert.ToInt64(row["Producto_Id"]);
                Cantidad_Factura = Convert.ToDouble(row["Cantidad_Factura"]);
                Descuento_Porciento = Convert.ToDouble(row["Descuento_Porciento"]);
                Descuento_Monto = Convert.ToDouble(row["Descuento_Monto"]);
                Impuesto_Tasa = Convert.ToDouble(row["Impuesto_Tasa"]);
                Impuesto_Monto = Convert.ToDouble(row["Impuesto_Monto"]);
                Cantidad_Empaque = Convert.ToDouble(row["Cantidad_Empaque"]);

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
