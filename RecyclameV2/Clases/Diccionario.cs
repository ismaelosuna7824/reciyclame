using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Diccionario : ClaseBase
    {
        public long Diccionario_Id { get; set; }
        public long Producto_Id { get; set; }
        public long Provedor_Id { get; set; }
        public string Valor { get; set; }
        public Diccionario()
        {
            TipoClase = ClaseTipo.Diccionario;
            CampoId = "Diccionario_Id";
            CampoBusqueda = "Valor";
            QueryGrabar = "Diccionario_Grabar_sp";
            QueryGrabarCodigo = "Diccionario_Grabar_sp_codigo";
            QueryConsultar = "Diccionario_Consultar_sp";
            Diccionario_Id = -1;
            Producto_Id = -1;
            Provedor_Id = -1;
            Valor = "";
        }

        #region Metodos
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
                Diccionario_Id = Convert.ToInt64(row["Diccionario_Id"]);
                Producto_Id = Convert.ToInt64(row["Producto_Id"]);
                Provedor_Id = Convert.ToInt64(row["Provedor_Id"]);
                Valor = Convert.ToString(row["Valor"]);

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

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Diccionario_Id", Value = 0 });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        resultado = dataset.Tables[QueryConsultar];
        //    }
        //    return resultado;
        //}

        #endregion;

        public Diccionario Buscar(long provedor_Id, string valor)
        {
            Diccionario resultado = new Diccionario();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter() { ParameterName = "@P_Diccionario_Id", Value = 0 });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Proveedor_Id", Value = provedor_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Valor", Value = valor });

            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[QueryConsultar].Rows)
                {
                    resultado.Cargar(row);
                    break;
                }
            }
            return resultado;
        }
    }
}
