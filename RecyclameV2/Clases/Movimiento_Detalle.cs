using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Movimiento_Detalle : ClaseBase
    {
        public long Movimiento_Detalle_Id { get; set; }
        public long Movimiento_Id { get; set; }
        public long Producto_Id { get; set; }
        public double Cantidad { get; set; }
        public Movimiento_Detalle()
            : this(-1)
        {
        }

        public Movimiento_Detalle(long movimiento_id)
        {
            TipoClase = ClaseTipo.Movimiento_Detalle;
            CampoId = "Movimiento_Detalle_Id";
            CampoBusqueda = "Movimiento_Id";
            QueryGrabar = "Movimiento_Detalle_Grabar_sp";
            QueryGrabarCodigo = "Movimiento_Detalle_Grabar_sp";
            QueryConsultar = "Movimiento_Detalle_Consultar_sp";
            QueryBorrar = "Movimiento_Detalle_Borrar_sp";
            Movimiento_Detalle_Id = -1;
            Movimiento_Id = movimiento_id;
            Producto_Id = -1;
            Cantidad = 0;
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
        //    paramId.ParameterName = "@P_Movimiento_Detalle_Id";
        //    paramId.Value = Movimiento_Detalle_Id;
        //    paramId.Direction = System.Data.ParameterDirection.InputOutput;
        //    parametros.Add(paramId);

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = Movimiento_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = Producto_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = Cantidad });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Renglon", Value = -1 });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
        //    if (resultado && Movimiento_Detalle_Id == -1)
        //        Movimiento_Detalle_Id = Convert.ToInt64(paramId.Value);
        //    return resultado;
        //}

        public bool GrabarDetalles(int nRenglon)
        {
            bool resultado = false;
            nRenglon = nRenglon + 1;
            List<SqlParameter> parametros = new List<SqlParameter>();

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@P_Movimiento_Detalle_Id";
            paramId.Value = Movimiento_Detalle_Id;
            paramId.Direction = System.Data.ParameterDirection.InputOutput;
            parametros.Add(paramId);

            parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = Movimiento_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = Producto_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = Cantidad });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Renglon", Value = nRenglon });

            resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
            if (resultado && Movimiento_Detalle_Id == -1)
                Movimiento_Detalle_Id = Convert.ToInt64(paramId.Value);
            return resultado;
        }

        ///// <summary>
        ///// Ejecuta el metodo GrabarFlete.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        ///*override public bool GrabarFlete()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    SqlParameter paramId = new SqlParameter();
        //    paramId.ParameterName = "@P_Movimiento_Detalle_Id";
        //    paramId.Value = Movimiento_Detalle_Id;
        //    paramId.Direction = System.Data.ParameterDirection.InputOutput;
        //    parametros.Add(paramId);

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = Movimiento_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = Producto_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = Cantidad });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabarFlete, parametros) > 0);
        //    if (resultado && Movimiento_Detalle_Id == -1)
        //        Movimiento_Detalle_Id = Convert.ToInt64(paramId.Value);
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

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Detalle_Id", Value = Movimiento_Detalle_Id });

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
                Movimiento_Detalle_Id = Convert.ToInt64(row["Movimiento_Detalle_Id"]);
                Movimiento_Id = Convert.ToInt64(row["Movimiento_Id"]);
                Producto_Id = Convert.ToInt64(row["Producto_Id"]);
                Cantidad = Convert.ToDouble(row["Cantidad"]);

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

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Detalle_Id", Value = 0 });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = Movimiento_Id });

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

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = Movimiento_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Detalle_Id", Value = Movimiento_Detalle_Id });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryBorrar, parametros) > 0);
        //    return resultado;
        //}
    }
}
