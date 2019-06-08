using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Movimiento_Gastos_Detalle : ClaseBase
    {
        public long Movimiento_Gasto_Detalle_Id { get; set; }
        public long Movimiento_Id { get; set; }
        public long Gasto_Id { get; set; }
        public double Cantidad { get; set; }
        public Movimiento_Gastos_Detalle()
            : this(-1)
        {

        }
        public Movimiento_Gastos_Detalle(long movimiento_id)
        {
            TipoClase = ClaseTipo.Movimiento_Detalle;
            CampoId = "Movimiento_Gasto_Detalle_Id";
            CampoBusqueda = "Movimiento_Id";
            QueryGrabar = "Movimiento_Gasto_Detalle_Grabar_sp";
            QueryGrabarCodigo = "Movimiento_Gasto_Detalle_Grabar_sp";
            QueryConsultar = "Movimiento_Gasto_Detalle_Consultar_sp";
            QueryBorrar = "Movimiento_Gasto_Detalle_Borrar_sp";
            Movimiento_Gasto_Detalle_Id = -1;
            Movimiento_Id = movimiento_id;
            Gasto_Id = -1;
            Cantidad = 0;
        }

        public bool GrabarDetalles(int nRenglon)
        {
            bool resultado = false;
            nRenglon = nRenglon + 1;
            List<SqlParameter> parametros = new List<SqlParameter>();

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@P_Movimiento_Gasto_Detalle_Id";
            paramId.Value = Movimiento_Gasto_Detalle_Id;
            paramId.Direction = System.Data.ParameterDirection.InputOutput;
            parametros.Add(paramId);

            parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = Movimiento_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = Gasto_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = Cantidad });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Renglon", Value = nRenglon });

            resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
            if (resultado && Movimiento_Gasto_Detalle_Id == -1)
                Movimiento_Gasto_Detalle_Id = Convert.ToInt64(paramId.Value);
            return resultado;
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
                Movimiento_Gasto_Detalle_Id = Convert.ToInt64(row["Movimiento_Gasto_Detalle_Id"]);
                Movimiento_Id = Convert.ToInt64(row["Movimiento_Id"]);
                Gasto_Id = Convert.ToInt64(row["Gasto_Id"]);
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

    }
}
