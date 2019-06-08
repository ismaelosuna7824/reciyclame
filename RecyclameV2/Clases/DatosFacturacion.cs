using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class DatosFacturacion : ClaseBase
    {
        public long Id
        {
            get;
            set;
        }
        public string Razon_Social
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
        public string Regimen
        {
            get;
            set;
        }
        public string RFC
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
        public bool Activo
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }

        public DatosFacturacion()
        {
            CampoId = "Id";
            CampoBusqueda = "RazonSocial";
            QueryGrabar = "Datos_Facturacion_Grabar_sp";
            QueryConsultar = "Datos_Facturacion_Consultar_sp";
            QueryCancelar = "Datos_Facturacion_Borrar_sp";
            QueryBorrar = "Datos_Facturacion_Borrar_sp";
            Id = -1;
            Razon_Social = string.Empty;
            Regimen = string.Empty;
            Localidad = "";
            Municipio = "";
            RFC = "";
            Calle = "";
            NumInt = "";
            NumExt = "";
            Colonia = "";
            CodigoPostal = "";
            Estado = "";
            Pais = "";
            Telefono = "";
            Activo = true;
            Status = "";
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
                Razon_Social = row["RazonSocial"].ToString();
                Regimen = row["Regimen"].ToString();
                Localidad = row["Localidad"].ToString();
                Municipio = row["Municipio"].ToString();
                RFC = Convert.ToString(row["RFC"]);
                Calle = row["Calle"].ToString();
                NumInt = row["NumInt"].ToString();
                NumExt = row["NumExt"].ToString();
                Colonia = row["Colonia"].ToString();
                CodigoPostal = Convert.ToString(row["CodigoPostal"]);
                Estado = Convert.ToString(row["Estado"]);
                Pais = Convert.ToString(row["Pais"]);
                Telefono = row["Telefono"].ToString();
                Activo = Convert.ToBoolean(row["Status"]);
                if (Activo)
                {
                    Status = "VIGENTE";
                }
                else
                {
                    Status = "CANCELADA";
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
