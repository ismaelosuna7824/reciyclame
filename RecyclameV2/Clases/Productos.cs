using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Productos : ClaseBase
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
        public ProductoDetalle Detalle { get; set; }
        public long IdLinea1 { get; set; }
        public long IdLinea2 { get; set; }
        public long IdLinea3 { get; set; }
        public string Numero_Serie { get; set; }

        //public BindingList<Producto_Ubicacion> Ubicaciones;
        public List<string> lstSeries { get; set; }
        public string FolioFiscal { get; set; }
        public bool TieneNumeroSerie { get; set; }
        public int IdDatosFiscales { get; set; }
        private bool FromVenta { get; set; }
        public Productos()
        {
            TipoClase = ClaseTipo.Productos;
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
            lstSeries = new List<string>();
            Detalle = new ProductoDetalle();
            FolioFiscal = "";
            Descripcion = "";
            Codigo_Producto = "";
            Codigo_de_Barras = "";
            Unidad_de_Medida = "";
            Numero_Serie = "";
            Ultimo_Costo = 0;
            IdDatosFiscales = 0;
            Cantidad_Empaque = 1;
            Estado = "";
            /*Ubicaciones = new BindingList<Producto_Ubicacion>()
            {
                AllowEdit = true,
                AllowNew = true,
                AllowRemove = true,
                RaiseListChangedEvents = true
            };*/
        }

        #region Metodos
        public void setFromVenta(bool value)
        {
            FromVenta = value;
        }
        /*public bool GuardarUbicacion()
        {
            return GrabarUbicacion();
        }
        private bool GrabarUbicacion()
        {
            bool resultado = true;

            BorrarUbicacion();

            foreach (Producto_Ubicacion ubicacion in Ubicaciones)
            {
                if (!(resultado = ubicacion.Grabar()))
                    break;
            }

            return resultado;
        }*/

        /*private bool GrabarUbicacionFlete()
        {
            bool resultado = true;

            BorrarUbicacion();

            foreach (Producto_Ubicacion ubicacion in Ubicaciones)
            {
                if (!(resultado = ubicacion.GrabarFlete()))
                    break;
            }

            return resultado;
        }*/

        /*private bool BorrarUbicacion()
        {
            bool resultado = true;
            bool existe = false;
            Productos producto = new Productos();
            producto.Producto_Id = Producto_Id;
            if (producto.Cargar().Result)
            {
                foreach (Producto_Ubicacion ubicacionAnt in producto.Ubicaciones)
                {
                    existe = false;
                    foreach (Producto_Ubicacion ubicacionNva in Ubicaciones)
                    {
                        if (ubicacionAnt.Producto_Ubicacion_Id == ubicacionNva.Producto_Ubicacion_Id)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado = ubicacionAnt.Borrar();
                        if (!resultado)
                            break;
                    }
                }
            }
            return resultado;
        }*/

        /*private bool BorrarUbicacionFlete()
        {
            bool resultado = true;
            bool existe = false;
            Productos producto = new Productos();
            producto.Producto_Id = Producto_Id;
            if (producto.Cargar().Result)
            {
                foreach (Producto_Ubicacion ubicacionAnt in producto.Ubicaciones)
                {
                    existe = false;
                    foreach (Producto_Ubicacion ubicacionNva in Ubicaciones)
                    {
                        if (ubicacionAnt.Producto_Ubicacion_Id == ubicacionNva.Producto_Ubicacion_Id)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado = ubicacionAnt.Borrar();
                        if (!resultado)
                            break;
                    }
                }
            }
            return resultado;
        }*/
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
                if (row.Table.Columns.Contains("IdLinea1"))
                {
                    IdLinea1 = Convert.ToInt64(row["IdLinea1"]);
                }
                if (row.Table.Columns.Contains("IdLinea2"))
                {
                    IdLinea2 = Convert.ToInt64(row["IdLinea2"]);
                }
                if (row.Table.Columns.Contains("IdLinea3"))
                {
                    IdLinea3 = Convert.ToInt64(row["IdLinea3"]);
                }
                if (row.Table.Columns.Contains("Descripcion"))
                {
                    Descripcion = Convert.ToString(row["Descripcion"]);
                }
                if (row.Table.Columns.Contains("CodigoProducto"))
                {
                    Codigo_Producto = Convert.ToString(row["CodigoProducto"]);
                }
                if (row.Table.Columns.Contains("CodigoBarra"))
                {
                    Codigo_de_Barras = Convert.ToString(row["CodigoBarra"]);
                }
                if (row.Table.Columns.Contains("UnidadMedida"))
                {
                    Unidad_de_Medida = Convert.ToString(row["UnidadMedida"]);
                }
                if (row.Table.Columns.Contains("Status"))
                {
                    Activo = Convert.ToBoolean(row["Status"]);
                    if (Activo)
                    {
                        Estado = "VIGENTE";
                    }
                    else
                    {
                        Estado = "CANCELADO";
                    }
                }
                if (row.Table.Columns.Contains("IdDatosFiscales"))
                {
                    IdDatosFiscales = Convert.ToInt32(row["IdDatosFiscales"]);
                }
                if (row.Table.Columns.Contains("Ultimo_Costo"))
                {
                    try
                    {
                        Ultimo_Costo = Convert.ToDouble(row["Ultimo_Costo"]);
                    }
                    catch { }
                }
                if (row.Table.Columns.Contains("Cantidad_Empaque"))
                {
                    Cantidad_Empaque = Convert.ToInt32(row["Cantidad_Empaque"]);
                    Detalle.Producto_Id = Producto_Id;
                    bool b = Detalle.Cargar().Result;
                    //CargarUbicacion();
                }
                if (row.Table.Columns.Contains("Serie"))
                {
                    TieneNumeroSerie = Convert.ToBoolean(row["Serie"]);
                }
                if (FromVenta)
                {
                    Detalle.Cargar(row);
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
        #endregion;

        /*private bool CargarUbicacion()
        {
            bool resultado = true;
            Producto_Ubicacion productoUbicacion = new Producto_Ubicacion(Producto_Id);
            Producto_Ubicacion ubicacion;
            DataTable tabla = productoUbicacion.Listado();
            Ubicaciones.Clear();
            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    ubicacion = new Producto_Ubicacion(Producto_Id);
                    if (ubicacion.Cargar(row))
                        Ubicaciones.Add(ubicacion);
                    else
                    {
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }*/

        public object buscarProductoVenta(string text)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = text.Replace("'", "").Replace("\"", "") });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Codigo_de_Barras", Value = text.Replace("'", "").Replace("\"", "") });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = DateTime.Now });
            string query = "Productos_Busqueda_Venta_Consultar";
            DataTable resultado = new DataTable();
            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(query, parametros);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                resultado = dataset.Tables[query];
                if (resultado.Rows.Count > 1)
                {
                    return resultado;
                }
                else if (resultado.Rows.Count == 1)
                {
                    foreach (DataRow row in resultado.Rows)
                    {
                        FromVenta = true;
                        Cargar(row);
                    }
                    return this;
                }
                return dataset.Tables[query].Rows.Count > 0;
            }
            return null;
        }
    }
}
