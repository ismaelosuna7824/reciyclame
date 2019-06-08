using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class CFDS_Archivo : ClaseBase
    {
        public long CFDS_Archivo_Id { get; set; }
        public long CFDS_Id { get; set; }
        public int Tipo_Archivo_Id { get; set; }
        public string Tipo_Archivo
        {
            get
            {
                string valor = "";
                try
                {
                    if (Tipo_Archivo_Id >= 0)
                        valor = ((TIPO_ARCHIVO)Tipo_Archivo_Id).ToString().Replace("_", " ");
                }
                catch { }
                return valor;
            }
        }
        public string Nombre_Archivo { get; set; }
        public string Archivo { get; set; }

        //override public string CampoId { get { return "CFDS_Archivo_Id"; } }
        //override public string CampoBusqueda { get { return "Nombre_Archivo"; } }
        //protected override string QueryGrabar { get { return "CFDS_Archivo_Grabar_sp"; } }
        //protected override string QueryGrabarCodigo { get { return "CFDS_Archivo_Grabar_sp_codigo"; } }
        ////protected override string QueryGrabarFlete { get { return "CFDS_Archivo_Grabar_sp_flete"; } }
        //protected override string QueryConsultar { get { return "CFDS_Archivo_Consultar_sp"; } }
        //protected string QueryBorrar { get { return "CFDS_Archivo_Borrar_sp"; } }

        public CFDS_Archivo()
            : this(-1)
        {
        }

        public CFDS_Archivo(long cfds_id)
        {
            TipoClase = ClaseTipo.CFDS_Archivo;
            CampoId = "CFDS_Archivo_Id";
            CampoBusqueda = "Nombre_Archivo";
            QueryGrabar = "CFDS_Archivo_Grabar_sp";
            QueryGrabarCodigo = "CFDS_Archivo_Grabar_sp_codigo";
            QueryConsultar = "CFDS_Archivo_Consultar_sp";
            QueryBorrar = "CFDS_Archivo_Borrar_sp";
            CFDS_Archivo_Id = -1;
            CFDS_Id = cfds_id;
            Tipo_Archivo_Id = (int)TIPO_ARCHIVO.NO_SOPORTADO;
            Nombre_Archivo = "";
            Archivo = "";
        }

        #region Metodos

        /// <summary>
        /// Ejecuta el metodo Grabar.
        /// </summary>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        //override public bool Grabar()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    SqlParameter paramId = new SqlParameter();
        //    paramId.ParameterName = "@P_CFDS_Archivo_Id";
        //    paramId.Value = CFDS_Archivo_Id;
        //    paramId.Direction = System.Data.ParameterDirection.InputOutput;
        //    parametros.Add(paramId);

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = CFDS_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Archivo_Id", Value = Tipo_Archivo_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Archivo", Value = Nombre_Archivo });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Archivo", Value = Archivo });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
        //    if (resultado && CFDS_Archivo_Id == -1)
        //        CFDS_Archivo_Id = Convert.ToInt64(paramId.Value);
        //    return resultado;
        //}

        /// <summary>
        /// Ejecuta el metodo Grabar.
        /// </summary>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        /*override public bool GrabarFlete()
        {
            bool resultado = false;
            List<SqlParameter> parametros = new List<SqlParameter>();

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@P_CFDS_Archivo_Id";
            paramId.Value = CFDS_Archivo_Id;
            paramId.Direction = System.Data.ParameterDirection.InputOutput;
            parametros.Add(paramId);

            parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = CFDS_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Archivo_Id", Value = Tipo_Archivo_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Archivo", Value = Nombre_Archivo });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Archivo", Value = Archivo });

            resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabarFlete, parametros) > 0);
            if (resultado && CFDS_Archivo_Id == -1)
                CFDS_Archivo_Id = Convert.ToInt64(paramId.Value);
            return resultado;
        }*/

        ///// <summary>
        ///// Carga en los controles la informacion de un registro.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        //public override bool Cargar()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Archivo_Id", Value = CFDS_Archivo_Id });

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
                CFDS_Archivo_Id = Convert.ToInt64(row["CFDS_Archivo_Id"]);
                CFDS_Id = Convert.ToInt64(row["CFDS_Id"]);
                Tipo_Archivo_Id = Convert.ToInt32(row["Tipo_Archivo_Id"]);
                Nombre_Archivo = Convert.ToString(row["Nombre_Archivo"]);
                Archivo = Convert.ToString(row["Archivo"]);

                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                resultado = false;
            }

            return resultado;
        }

        ///// <summary>
        ///// Obtiene un listado.
        ///// </summary>
        ///// <returns>El DataTable que se obtiene despues de ejecutar el metodo</returns>
        //public override System.Data.DataTable Listado()
        //{
        //    DataTable resultado = new DataTable();
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Archivo_Id", Value = 0 });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = CFDS_Id });
        //    //parametros.Add(new SqlParameter() { ParameterName = "@P_ACTIVO", Value = bSoloActivos });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        resultado = dataset.Tables[QueryConsultar];
        //    }
        //    return resultado;
        //}
        #endregion;

        //public bool Borrar()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = CFDS_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Archivo_Id", Value = CFDS_Archivo_Id });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryBorrar, parametros) > 0);
        //    return resultado;
        //}
    }
}
