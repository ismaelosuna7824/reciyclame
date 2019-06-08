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
    class Producto : ClaseBase
    {
        public long Id_Producto { get; set; }
        [DisplayName("Producto")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Existencia { get; set; }
        public double Precio_Venta { get; set; }
        public string CodigoProducto { get; set; }
        public string CodigoBarras { get; set; }
        public string Color { get; set; }
        public string Talla { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public bool Activo { get; set; }

        override public string CampoId { get { return "Id_Producto"; } }
        override public string CampoBusqueda { get { return "NombreProducto"; } }
        protected override string QueryGrabar { get { return "Producto_Grabar_sp"; } }
        protected override string QueryConsultar { get { return "Productos_Consultar_sp"; } }
        protected string QueryConsultar2 { get { return "Producto_Consultar_Con_Existencia_Cero_sp"; } }


        public Producto()
        {
            Id_Producto = -1;
            Nombre = "";
            Descripcion = "";
            Existencia = 0;
            Precio_Venta = 0;
            CodigoProducto = "";
            CodigoBarras = "";
            Color = "";
            Talla = "";
            Modelo = "";
            Marca = "";
            Activo = false;
        }

        #region Metodos

        /// <summary>
        /// Ejecuta el metodo Grabar.
        /// </summary>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        override public bool Grabar()
        {
            bool resultado = false;
            List<SqlParameter> parametros = new List<SqlParameter>();

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@P_Id_Producto";
            paramId.Value = Id_Producto;
            paramId.Direction = System.Data.ParameterDirection.InputOutput;
            parametros.Add(paramId);

            parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = Nombre });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = Descripcion });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Existencia", Value = Existencia });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Precio_Venta", Value = Precio_Venta });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Codigo_Producto", Value = CodigoProducto.Replace("'", "").Replace("\"", "") });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Codigo_de_Barras", Value = CodigoBarras.Replace("'", "").Replace("\"", "") });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Color", Value = Color });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Talla", Value = Talla });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Modelo", Value = Modelo });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Marca", Value = Marca });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Activo", Value = Activo });

            resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
            if (resultado && Id_Producto == -1)
                Id_Producto = Convert.ToInt64(paramId.Value);

            return resultado;
        }



        /// <summary>
        /// Carga en los controles la informacion de un registro.
        /// </summary>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        /*public override bool Cargar()
        {
            Task<bool> resultado = false;
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Producto", Value = Id_Producto });

            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[QueryConsultar].Rows)
                {
                    Cargar(row);
                }
                resultado = dataset.Tables[QueryConsultar].Rows.Count > 0;
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
            Id_Producto = -1;
            Nombre = "";
            Descripcion = "";
            Existencia = 0;
            Precio_Venta = 0;
            CodigoProducto = "";
            CodigoBarras = "";
            Color = "";
            Talla = "";
            Modelo = "";
            Marca = "";
            Activo = false;

            try
            {
                Id_Producto = Convert.ToInt64(row["Id_Producto"]);
                Nombre = Convert.ToString(row["Nombre"]);
                Descripcion = Convert.ToString(row["Descripcion"]);
                Existencia = Convert.ToInt32(row["Existencia"]);
                Precio_Venta = Convert.ToDouble(row["Precio_Venta"]);
                CodigoProducto = Convert.ToString(row["CodigoProducto"]);
                CodigoBarras = Convert.ToString(row["CodigodeBarras"]);
                Color = Convert.ToString(row["Color"]);
                Talla = Convert.ToString(row["Talla"]);
                Marca = Convert.ToString(row["Marca"]);
                Modelo = Convert.ToString(row["Modelo"]);
                Activo = Convert.ToBoolean(row["Activo"]);

                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.ErrorException(ex.Message, ex);
                resultado = false;
            }

            return resultado;
        }
        /// <summary>
        /// Obtiene un listado.
        /// </summary>
        /// <returns>El DataTable que se obtiene despues de ejecutar el metodo</returns>
        public override System.Data.DataTable Listado()
        {
            return Listado(false);
        }

        /// <summary>
        /// Obtiene un listado.
        /// </summary>
        /// <param name="bSoloActivos">Especifica si se obtendrán sólo Activos o también Inactivos.</param>
        /// <returns>El DataTable que se obtiene despues de ejecutar el metodo</returns>

        public System.Data.DataTable Listado(bool bSoloActivos)
        {
            return Listado(bSoloActivos, false);
        }

        /// <summary>
        /// Obtiene un listado.
        /// </summary>
        /// <param name="bSoloActivos">Especifica si se obtendrán sólo Activos o también Inactivos.</param>
        /// <returns>El DataTable que se obtiene despues de ejecutar el metodo</returns>
        public System.Data.DataTable Listado(bool bSoloActivos, bool bConExistencia)
        {
            DataTable resultado = new DataTable();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Producto", Value = 0 });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Activo", Value = bSoloActivos });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Con_Existencia", Value = bConExistencia });

            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                resultado = dataset.Tables[QueryConsultar];
            }
            return resultado;
        }

        #endregion;
    }
}
