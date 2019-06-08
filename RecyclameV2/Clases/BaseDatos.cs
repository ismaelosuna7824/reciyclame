using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class BaseDatos
    {
        private static SqlConnection abrirBD()
        {
            SqlConnection conn = null;
            if (Global.CadenaConexion == null ||
                Global.CadenaConexion == "")
            {
                Log.Logger.Error(new ConnectionStringException().Message);
                throw new ConnectionStringException();
            }
            try
            {
                conn = new SqlConnection(Global.CadenaConexion);
                conn.Open();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine(e.ToString());
                Log.Logger.Error(e, e.Message);
                if (e.Number == 18456)
                {
                    throw new LoginException(string.Format("Inicio de Sesión incorrecto para Usuario [{0}]", Global.Usuario));
                }
            }
            return conn;
        }

        public static int ejecutarProcedimiento(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            int resultado = 0;
            SqlConnection conn = abrirBD();

            try
            {
                using (SqlCommand com = new SqlCommand(sNombreProcedimiento, conn))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.CommandTimeout = 600;

                    if (parametros != null)
                    {
                        foreach (SqlParameter par in parametros)
                        {
                            com.Parameters.Add(par);
                        }
                    }

                    resultado = com.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                Log.Logger.Error(sqlex.Message, sqlex);
                if (sqlex.Number == 229)
                {
                    //throw new PermissionException(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento), sqlex);
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento));
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                Log.Logger.Error(e, e.Message);
                DevExpress.XtraEditors.XtraMessageBox.Show(e.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return resultado;
        }

        public static int ejecutarProcedimientoTexto(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            int resultado = 0;
            SqlConnection conn = abrirBD();

            try
            {
                using (SqlCommand com = new SqlCommand(sNombreProcedimiento, conn))
                {
                    com.CommandType = System.Data.CommandType.Text;
                    com.CommandTimeout = 600;

                    if (parametros != null)
                    {
                        foreach (SqlParameter par in parametros)
                        {
                            com.Parameters.Add(par);
                        }
                    }

                    resultado = com.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                Log.Logger.Error(sqlex.Message, sqlex);
                if (sqlex.Number == 229)
                {
                    //throw new PermissionException(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento), sqlex);
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento));
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                Log.Logger.Error(e, e.Message);
                DevExpress.XtraEditors.XtraMessageBox.Show(e.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return resultado;
        }
        public static DataSet ejecutarProcedimientoConsulta(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            return ejecutarProcedimientoConsulta(sNombreProcedimiento, parametros, new string[] { sNombreProcedimiento });
        }

        public static DataTable ejecutarProcedimientoConsultaDataTable(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            DataTable dataTable = null;
            SqlConnection conn = abrirBD();
            try
            {
                using (SqlCommand com = new SqlCommand(sNombreProcedimiento, conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    com.CommandTimeout = 200;

                    if (parametros != null)
                    {
                        foreach (SqlParameter par in parametros)
                        {
                            com.Parameters.Add(par);
                        }
                    }
                    dataTable = new DataTable();
                    SqlDataReader dr = com.ExecuteReader();
                    dataTable.Load(dr, LoadOption.OverwriteChanges);
                    dr.Close();

                    foreach (System.Data.DataColumn col in dataTable.Columns)
                        col.ReadOnly = false;

                    return dataTable;
                }
            }
            catch (SqlException sqlex)
            {
                Log.Logger.Error(sqlex.Message, sqlex);
                if (sqlex.Number == 229)
                {
                    //throw new PermissionException(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento), sqlex);
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento));
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, e.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }
        public static DataRow ejecutarProcedimientoConsultaDataRow(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            DataRow row = null;
            DataTable dataTable = null;
            SqlConnection conn = abrirBD();
            try
            {
                using (SqlCommand com = new SqlCommand(sNombreProcedimiento, conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    com.CommandTimeout = 200;

                    if (parametros != null)
                    {
                        foreach (SqlParameter par in parametros)
                        {
                            com.Parameters.Add(par);
                        }
                    }
                    dataTable = new DataTable();
                    SqlDataReader dr = com.ExecuteReader();
                    dataTable.Load(dr, LoadOption.OverwriteChanges);
                    dr.Close();

                    foreach (System.Data.DataColumn col in dataTable.Columns)
                        col.ReadOnly = false;

                    if (dataTable.Rows.Count > 0)
                    {
                        row = dataTable.Rows[0];
                    }
                    return row;
                }
            }
            catch (SqlException sqlex)
            {
                Log.Logger.Error(sqlex.Message, sqlex);
                if (sqlex.Number == 229)
                {
                    //throw new PermissionException(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento), sqlex);
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento));
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, e.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }

        public static DataSet ejecutarProcedimientoConsulta(string sNombreProcedimiento, List<SqlParameter> parametros, string[] nombreTablas)
        {
            DataSet dataset = null;
            SqlConnection conn = abrirBD();
            try
            {
                using (SqlCommand com = new SqlCommand(sNombreProcedimiento, conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    com.CommandTimeout = 200;

                    if (parametros != null)
                    {
                        foreach (SqlParameter par in parametros)
                        {
                            com.Parameters.Add(par);
                        }
                    }
                    dataset = new DataSet();
                    SqlDataReader dr = com.ExecuteReader();
                    dataset.Load(dr, LoadOption.OverwriteChanges, nombreTablas);
                    dr.Close();

                    foreach (DataTable tabla in dataset.Tables)
                        foreach (System.Data.DataColumn col in tabla.Columns)
                            col.ReadOnly = false;

                    return dataset;
                }
            }
            catch (SqlException sqlex)
            {
                Log.Logger.Error(sqlex.Message, sqlex);
                if (sqlex.Number == 229)
                {
                    //throw new PermissionException(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento), sqlex);
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento));
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, e.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }

        public static DataSet ejecutarProcedimientoConsultaTextoDataSet(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            return obtenerDataSet(sNombreProcedimiento, parametros, new string[] { sNombreProcedimiento });
        }

        private static DataSet obtenerDataSet(string sNombreProcedimiento, List<SqlParameter> parametros, string[] nombreTablas)
        {
            DataSet dataset = null;
            SqlConnection conn = abrirBD();
            try
            {
                using (SqlCommand com = new SqlCommand(sNombreProcedimiento, conn) { CommandType = System.Data.CommandType.Text })
                {
                    com.CommandTimeout = 200;

                    if (parametros != null)
                    {
                        foreach (SqlParameter par in parametros)
                        {
                            com.Parameters.Add(par);
                        }
                    }
                    dataset = new DataSet();
                    SqlDataReader dr = com.ExecuteReader();
                    dataset.Load(dr, LoadOption.OverwriteChanges, nombreTablas);
                    dr.Close();

                    foreach (DataTable tabla in dataset.Tables)
                        foreach (System.Data.DataColumn col in tabla.Columns)
                            col.ReadOnly = false;

                    return dataset;
                }
            }
            catch (SqlException sqlex)
            {
                Log.Logger.Error(sqlex.Message, sqlex);
                if (sqlex.Number == 229)
                {
                    //throw new PermissionException(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento), sqlex);
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento));
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, e.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }

        public static DataTable ejecutarProcedimientoConsultaTextoDataTable(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            return obtenerDataTable(sNombreProcedimiento, parametros);
        }

        private static DataTable obtenerDataTable(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            DataTable dataTable = null;
            SqlConnection conn = abrirBD();
            try
            {
                using (SqlCommand com = new SqlCommand(sNombreProcedimiento, conn) { CommandType = System.Data.CommandType.Text })
                {
                    com.CommandTimeout = 200;

                    if (parametros != null)
                    {
                        foreach (SqlParameter par in parametros)
                        {
                            com.Parameters.Add(par);
                        }
                    }
                    dataTable = new DataTable();
                    SqlDataReader dr = com.ExecuteReader();
                    dataTable.Load(dr, LoadOption.OverwriteChanges);
                    dr.Close();

                    foreach (System.Data.DataColumn col in dataTable.Columns)
                        col.ReadOnly = false;

                    return dataTable;
                }
            }
            catch (SqlException sqlex)
            {
                Log.Logger.Error(sqlex.Message, sqlex);
                if (sqlex.Number == 229)
                {
                    //throw new PermissionException(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento), sqlex);
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento));
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, e.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }

        public static DataRow ejecutarProcedimientoConsultaTextoDataRow(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            return obtenerDataRow(sNombreProcedimiento, parametros);
        }

        private static DataRow obtenerDataRow(string sNombreProcedimiento, List<SqlParameter> parametros)
        {
            DataRow dataRow = null;
            DataTable dataTable = null;
            SqlConnection conn = abrirBD();
            try
            {
                using (SqlCommand com = new SqlCommand(sNombreProcedimiento, conn) { CommandType = System.Data.CommandType.Text })
                {
                    com.CommandTimeout = 200;

                    if (parametros != null)
                    {
                        foreach (SqlParameter par in parametros)
                        {
                            com.Parameters.Add(par);
                        }
                    }
                    dataTable = new DataTable();
                    SqlDataReader dr = com.ExecuteReader();
                    dataTable.Load(dr, LoadOption.OverwriteChanges);
                    dr.Close();

                    foreach (System.Data.DataColumn col in dataTable.Columns)
                        col.ReadOnly = false;

                    if (dataTable.Rows.Count > 0)
                    {
                        dataRow = dataTable.Rows[0];
                    }

                    return dataRow;
                }
            }
            catch (SqlException sqlex)
            {
                Log.Logger.Error(sqlex.Message, sqlex);
                if (sqlex.Number == 229)
                {
                    //throw new PermissionException(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento), sqlex);
                    DevExpress.XtraEditors.XtraMessageBox.Show(string.Format("No tiene permisos suficientes para el procedimiento [{0}].", sNombreProcedimiento));
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, e.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }
    }
}
