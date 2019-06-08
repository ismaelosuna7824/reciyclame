using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class IEPS : ClaseBase
    {
        public long Id { get; set; }
        public long Porcentaje { get; set; }
        public bool Activo { get; set; }
        public IEPS()
        {
            CampoId = "Id";
            CampoBusqueda = "Porcentaje";
            QueryGrabar = "IEPS_Grabar_sp";
            QueryConsultar = "IEPS_Consultar_sp";
            QueryBorrar = "IEPS_Borrar_sp";
            Id = -1;
            Porcentaje = 0;
            Activo = true;
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
                Id = Convert.ToInt64(row["Id"]);
                Porcentaje = Convert.ToInt64(row["Porcentaje"]);
                Activo = Convert.ToBoolean(row["Status"]);
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
