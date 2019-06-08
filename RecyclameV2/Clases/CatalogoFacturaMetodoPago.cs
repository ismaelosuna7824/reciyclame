using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class CatalogoFacturaMetodoPago : ClaseBase
    {
        public long Catalogo_Id { get; set; }
        public int Clave_Sat { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public bool Activo { get; set; }
        public CatalogoFacturaMetodoPago()
        {
            CampoId = "Id";
            CampoBusqueda = "Nombre";
            QueryGrabar = "Catalogo_Metodo_Pago_Grabar_sp";
            QueryConsultar = "Catalogo_Metodo_Pago_Consultar_sp";
            QueryBorrar = "Catalogo_Metodo_Pago_Borrar_sp";
            Catalogo_Id = -1;
            Clave_Sat = 0;
            Nombre = "";
            Estado = "";
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
                Catalogo_Id = Convert.ToInt64(row["Id"]);
                Clave_Sat = Convert.ToInt32(row["ClaveSat"]);
                Nombre = Convert.ToString(row["Nombre"]);
                Activo = Convert.ToBoolean(row["Status"]);
                if (Activo)
                {
                    Estado = "VIGENTE";
                }
                else
                {
                    Estado = "CANCELADO";
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
