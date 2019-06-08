using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Tokens : ClaseBase
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Tokens()
        {
            AccessToken = "";
            RefreshToken = "";
            QueryGrabar = "Tokens_Grabar_sp";
            QueryConsultar = "Tokens_Consultar_sp";
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
                AccessToken = Convert.ToString(row["AccessToken"]);
                RefreshToken = Convert.ToString(row["RefreshToken"]);
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
