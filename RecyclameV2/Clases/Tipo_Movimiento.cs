using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Tipo_Movimiento : ClaseBase
    {
        public long Tipo_Movimiento_Id { get; set; }
        public string Descripcion { get; set; }
        public string Clave { get; set; }
        public string EntradaSalida { get; set; }
        public bool Activo { get; set; }
        public Tipo_Movimiento()
        {
            TipoClase = ClaseTipo.Tipo_Movimiento;
            CampoId = "Tipo_Movimiento_Id";
            CampoBusqueda = "Tipo_Movimiento";
            QueryGrabar = "Tipo_Movimiento_Grabar_sp";
            QueryGrabarCodigo = "Tipo_Movimiento_Grabar_sp_codigo";
            QueryConsultar = "Tipo_Movimiento_Consultar_sp";
            Tipo_Movimiento_Id = -1;
            Descripcion = "";
            Clave = "";
            EntradaSalida = "";
            Activo = false;
        }

        #region Metodos

        ///// <summary>
        ///// Ejecuta el metodo Grabar.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        //override public bool Grabar()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    SqlParameter paramId = new SqlParameter();
        //    paramId.ParameterName = "@P_Tipo_Movimiento_Id";
        //    paramId.Value = Tipo_Movimiento_Id;
        //    paramId.Direction = System.Data.ParameterDirection.InputOutput;
        //    parametros.Add(paramId);

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento", Value = Descripcion });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Clave", Value = Clave });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_EntradaSalida", Value = EntradaSalida });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Activo", Value = Activo });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
        //    if (resultado && Tipo_Movimiento_Id == -1)
        //        Tipo_Movimiento_Id = Convert.ToInt64(paramId.Value);

        //    return resultado;
        //}

        ///// <summary>
        ///// Ejecuta el metodo Grabar.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        ///*override public bool GrabarFlete()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    SqlParameter paramId = new SqlParameter();
        //    paramId.ParameterName = "@P_Tipo_Movimiento_Id";
        //    paramId.Value = Tipo_Movimiento_Id;
        //    paramId.Direction = System.Data.ParameterDirection.InputOutput;
        //    parametros.Add(paramId);

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento", Value = Descripcion });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Clave", Value = Clave });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_EntradaSalida", Value = EntradaSalida });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Activo", Value = Activo });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabarFlete, parametros) > 0);
        //    if (resultado && Tipo_Movimiento_Id == -1)
        //        Tipo_Movimiento_Id = Convert.ToInt64(paramId.Value);

        //    return resultado;
        //}*/

        ///// <summary>
        ///// Carga en los controles la informacion de un registro.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        //public override bool Cargar()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = Tipo_Movimiento_Id });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        foreach (DataRow row in dataset.Tables[QueryConsultar].Rows)
        //        {
        //            Cargar(row);
        //        }
        //        resultado = dataset.Tables[QueryConsultar].Rows.Count > 0;
        //    }
        //    return resultado;
        //}

        //public override bool Cargar_Mov_CFDI(int nCFDI)
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDI_Id", Value = nCFDI });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        foreach (DataRow row in dataset.Tables[QueryConsultar].Rows)
        //        {
        //            Cargar(row);
        //        }
        //        resultado = dataset.Tables[QueryConsultar].Rows.Count > 0;
        //    }
        //    return resultado;
        //}

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
                Tipo_Movimiento_Id = Convert.ToInt64(row["Tipo_Movimiento_Id"]);
                Descripcion = Convert.ToString(row["Tipo_Movimiento"]);
                Clave = Convert.ToString(row["Clave"]);
                EntradaSalida = Convert.ToString(row["EntradaSalida"]);
                Activo = Convert.ToBoolean(row["Activo"]);

                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
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
            return Listado(false, "");
        }

        /// <summary>
        /// Obtiene un listado.
        /// </summary>
        /// <param name="bSoloActivos">Especifica si se obtendrán sólo Activos o también Inactivos.</param>
        /// <returns>El DataTable que se obtiene despues de ejecutar el metodo</returns>
        public System.Data.DataTable Listado(bool bSoloActivos, string strEntradaSalida)
        {
            DataTable resultado = new DataTable();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = 0 });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Activo", Value = bSoloActivos });
            if (strEntradaSalida.Trim().Length > 0)
                parametros.Add(new SqlParameter() { ParameterName = "@P_EntradaSalida", Value = strEntradaSalida });

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
