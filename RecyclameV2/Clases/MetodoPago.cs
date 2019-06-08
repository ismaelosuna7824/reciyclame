using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class MetodoPago : ClaseBase
    {
        public long Id { get; set; }
        public string Metodo { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
        public string Status { get; set; }
        public MetodoPago()
        {
            CampoId = "Clave";
            CampoBusqueda = "Metodo";
            QueryGrabar = "Metodo_Pago_Grabar_sp";
            QueryConsultar = "Metodo_Pago_Consultar_sp";
            QueryCancelar = "Metodo_Pago__Borrar_sp";
            QueryBorrar = "Metodo_Pago__Borrar_sp";
            Id = -1;
            Metodo = "";
            Clave = "";
            Status = "";
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
                Metodo = row["Metodo"].ToString();
                Clave = row["Clave"].ToString();
                Activo = Convert.ToBoolean(row["Status"]);
                if (Activo)
                {
                    Status = "VIGENTE";
                }
                else
                {
                    Status = "ELIMINADO";
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
