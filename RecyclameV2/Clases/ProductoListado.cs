using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class ProductoListado : ClaseBase
    {
        public long Producto_Id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo_Producto { get; set; }
        public string Codigo_de_Barras { get; set; }
        public string Unidad_de_Medida { get; set; }
        public double Ultimo_Costo { get; set; }
        public double Cantidad_Empaque { get; set; }
        public bool Activo { get; set; }
        public string Estado { get; set; }
        public long IdLinea1 { get; set; }
        public long IdLinea2 { get; set; }
        public long IdLinea3 { get; set; }
        public bool TieneNumeroSerie { get; set; }
        public long Proveedor_Id { get; set; }
        public String Color { get; set; }
        public double Costo_Proveedor { get; set; }
        public double Precio_General { get; set; }
        public int Cantidad_Minima { get; set; }
        public int Cantidad_Maxima { get; set; }
        public long IVA { get; set; }
        public long IEPS { get; set; }
        public double Precio_Mayoreo { get; set; }
        public double Cantidad_Mayoreo { get; set; }
        public double Precio_Compra { get; set; }
        public double Existencia { get; set; }
        public string Departamento { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public ProductoListado()
        {
            CampoId = "Producto_Id";
            CampoBusqueda = "Descripcion";
            QueryGrabar = "Productos_Grabar_sp";
            QueryGrabarCodigo = "Productos_Grabar_sp_codigo";
            QueryConsultar = "Productos_Consultar_sp";
            QueryBorrar = "Productos_Borrar_sp";
            Producto_Id = -1;
            IdLinea1 = -1;
            IdLinea2 = -1;
            IdLinea3 = -1;
            Descripcion = "";
            Codigo_Producto = "";
            Codigo_de_Barras = "";
            Unidad_de_Medida = "";
            Ultimo_Costo = 0;
            Cantidad_Empaque = 1;
            Estado = "";
            Marca = "";
            Modelo = "";
            Departamento = "";
            Proveedor_Id = -1;
            Color = string.Empty;
            Costo_Proveedor = 0;
            Precio_General = 0;
            Cantidad_Minima = 0;
            Cantidad_Maxima = 0;
            IVA = -1;
            IEPS = -1;
            Precio_Mayoreo = 0;
            Cantidad_Mayoreo = 0;
            Precio_Compra = 0;
            Existencia = 0;

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
                Producto_Id = Convert.ToInt64(row["IdProducto"]);
                IdLinea1 = Convert.ToInt64(row["IdLinea1"]);
                IdLinea2 = Convert.ToInt64(row["IdLinea2"]);
                IdLinea3 = Convert.ToInt64(row["IdLinea3"]);
                Descripcion = Convert.ToString(row["Descripcion"]);
                Codigo_Producto = Convert.ToString(row["CodigoProducto"]);
                Codigo_de_Barras = Convert.ToString(row["CodigoBarra"]);
                Unidad_de_Medida = Convert.ToString(row["UnidadMedida"]);
                try
                {
                    Ultimo_Costo = Convert.ToDouble(row["Ultimo_Costo"]);
                }
                catch { }
                Cantidad_Empaque = Convert.ToInt32(row["Cantidad_Empaque"]);
                TieneNumeroSerie = Convert.ToBoolean(row["Serie"]);
                IVA = Convert.ToInt64(row["IVA"]);
                IEPS = Convert.ToInt64(row["IEPS"]);
                Precio_Mayoreo = Convert.ToDouble(row["PrecioMayoreo"]);
                Cantidad_Mayoreo = Convert.ToInt32(row["CantidadMayoreo"]);
                Precio_Compra = Convert.ToDouble(row["PrecioPromocion"]);
                Existencia = Convert.ToDouble(row["Existencia"]);
                Color = Convert.ToString(row["Color"]);
                Costo_Proveedor = Convert.ToDouble(row["CostoProveedor"]);
                Precio_General = Convert.ToDouble(row["PrecioGeneral"]);
                Cantidad_Minima = Convert.ToInt32(row["CantidadMinima"]);
                Cantidad_Maxima = Convert.ToInt32(row["CantidadMaxima"]);
                Proveedor_Id = Convert.ToInt64(row["IdProveedor"]);
                Departamento = Convert.ToString(row["Departamento"]);
                Modelo = Convert.ToString(row["Modelo"]);
                Marca = Convert.ToString(row["Marca"]);
                Activo = Convert.ToBoolean(row["Status"]);
                if (Activo)
                {
                    Estado = "VIGENTE";
                }
                else
                {
                    Estado = "CANCELADO";
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
