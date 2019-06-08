using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class UbicacionFiscal : ClaseBase
    {
        public long Id
        {
            get;
            set;
        }
        public string Localidad
        {
            get;
            set;
        }
        public string Municipio
        {
            get;
            set;
        }
        public string Calle
        {
            get;
            set;
        }
        public string NumInt
        {
            get;
            set;
        }
        public string NumExt
        {
            get;
            set;
        }
        public string Colonia
        {
            get;
            set;
        }
        public string CodigoPostal
        {
            get;
            set;
        }
        public string Estado
        {
            get;
            set;
        }
        public string Pais
        {
            get;
            set;
        }
        public string Telefono
        {
            get;
            set;
        }
        public long EmpresaId
        {
            get;
            set;
        }
        public UbicacionFiscal()
        {
            CampoId = "Id";
            QueryGrabar = "Ubicacion_Fiscal_Grabar_sp";
            QueryConsultar = "Ubicacion_Fiscal_Consultar_sp";
            Id = -1;
            Localidad = "";
            Municipio = "";
            Calle = "";
            NumInt = "";
            NumExt = "";
            Colonia = "";
            CodigoPostal = "";
            Estado = "";
            Pais = "";
            Telefono = "";
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
                Localidad = row["Localidad"].ToString();
                Municipio = row["Municipio"].ToString();
                Calle = row["Calle"].ToString();
                NumInt = row["NumInt"].ToString();
                NumExt = row["NumExt"].ToString();
                Colonia = row["Colonia"].ToString();
                CodigoPostal = Convert.ToString(row["CodigoPostal"]);
                Estado = Convert.ToString(row["Estado"]);
                Pais = Convert.ToString(row["Pais"]);
                Telefono = row["Telefono"].ToString();
                EmpresaId = Convert.ToInt64(row["IdDatosFiscales"]);
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
