using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class CertificadoDigital : ClaseBase
    {
        public long Id { get; set; }
        public string RutaCertificado { get; set; }
        public string RutaClave { get; set; }
        public string Password { get; set; }
        public long EmpresaId { get; set; }
        public CertificadoDigital()
        {
            CampoId = "Id";
            QueryGrabar = "Requisitos_Digitales_Facturacion_Grabar_sp";
            QueryConsultar = "Requisitos_Digitales_Facturacion_Consultar_sp";
            Id = -1;
            RutaCertificado = "";
            RutaClave = "";
            Password = "";
            EmpresaId = -1;
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
                RutaCertificado = Convert.ToString(row["RutaCertificado"]);
                RutaClave = Convert.ToString(row["RutaClave"]);
                Password = Convert.ToString(row["Password"]);
                EmpresaId = Convert.ToInt64(row["IdDatosFiscales"]);
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
