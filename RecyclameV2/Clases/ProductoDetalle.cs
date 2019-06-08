using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class ProductoDetalle : ClaseBase
    {
        public long Id { get; set; }
        public long Producto_Id { get; set; }
        public long Proveedor_Id { get; set; }
        public String Marca { get; set; }
        public String Color { get; set; }
        public double Costo_Proveedor { get; set; }
        public double Precio_General { get; set; }
        public int Cantidad_Minima { get; set; }
        public int Cantidad_Maxima { get; set; }
        public String Codigo_de_Barras { get; set; }
        public long IVA { get; set; }
        public long IEPS { get; set; }
        public double Precio_Mayoreo { get; set; }
        public double Cantidad_Mayoreo { get; set; }
        public double Precio_Compra { get; set; }
        public double Cantidad { get; set; }
        public ProductoDetalle()
        {
            //CampoId = "Id";
            //CampoBusqueda = "Descripcion";
            QueryGrabar = "Producto_Detalle_Grabar_sp";
            QueryGrabarCodigo = "Producto_Detalle_Grabar_sp_codigo";
            QueryConsultar = "Producto_Detalle_Consultar_sp";
            QueryBorrar = "Producto_Detalle_Borrar_sp";
            Id = -1;
            Producto_Id = -1;
            Proveedor_Id = -1;
            Marca = string.Empty;
            Color = string.Empty;
            Costo_Proveedor = 0;
            Precio_General = 0;
            Cantidad_Minima = 0;
            Cantidad_Maxima = 0;
            Codigo_de_Barras = string.Empty;
            IVA = -1;
            IEPS = -1;
            Precio_Mayoreo = 0;
            Cantidad_Mayoreo = 0;
            Precio_Compra = 0;
            Cantidad = 0;
        }

        public void setQueryGrabar(string query)
        {
            QueryGrabar = query;
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
                DataColumnCollection Columns = row.Table.Columns;
                if (Columns.Contains("IdProductoDetalle"))
                {
                    Id = Convert.ToInt64(row["IdProductoDetalle"]);
                }
                if (Columns.Contains("IdProducto"))
                {
                    Producto_Id = Convert.ToInt64(row["IdProducto"]);
                }
                if (Columns.Contains("IdProveedor"))
                {
                    Proveedor_Id = Convert.ToInt64(row["IdProveedor"]);
                }
                if (Columns.Contains("Marca"))
                {
                    Marca = Convert.ToString(row["Marca"]);
                }
                if (Columns.Contains("Color"))
                {
                    Color = Convert.ToString(row["Color"]);
                }
                if (Columns.Contains("CostoProveedor"))
                {
                    Costo_Proveedor = Convert.ToDouble(row["CostoProveedor"]);
                }
                if (Columns.Contains("PrecioGeneral"))
                {
                    Precio_General = Convert.ToDouble(row["PrecioGeneral"]);
                }
                if (Columns.Contains("CantidadMinima"))
                {
                    Cantidad_Minima = Convert.ToInt32(row["CantidadMinima"]);
                }
                if (Columns.Contains("CantidadMaxima"))
                {
                    Cantidad_Maxima = Convert.ToInt32(row["CantidadMaxima"]);
                }
                //Codigo_de_Barras = Convert.ToString(row["CodigoBarra"]);
                if (Columns.Contains("IVA"))
                {
                    IVA = Convert.ToInt64(row["IVA"]);
                }
                if (Columns.Contains("IEPS"))
                {
                    IEPS = Convert.ToInt64(row["IEPS"]);
                }
                if (Columns.Contains("PrecioMayoreo"))
                {
                    Precio_Mayoreo = Convert.ToDouble(row["PrecioMayoreo"]);
                }
                if (Columns.Contains("CantidadMayoreo"))
                {
                    Cantidad_Mayoreo = Convert.ToInt32(row["CantidadMayoreo"]);
                }
                if (Columns.Contains("PrecioPromocion"))
                {
                    Precio_Compra = Convert.ToDouble(row["PrecioPromocion"]);
                }
                if (Columns.Contains("Existencia"))
                {
                    Cantidad = Convert.ToDouble(row["Existencia"]);
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
