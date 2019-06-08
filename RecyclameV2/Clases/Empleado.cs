using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Empleado : ClaseBase
    {
        public Empleado()
        {
            CampoId = "IdEmpleados";
            CampoBusqueda = "Nombre";
            QueryGrabar = "Empleado_Grabar_sp";
            QueryConsultar = "Empleado_Consultar_sp";
            QueryCancelar = "Empleado_Borrar_sp";
            QueryBorrar = "Empleado_Borrar_sp";
            Id = -1;
            Nombre = string.Empty;
            ApellidoPaterno = string.Empty;
            ApellidoMaterno = string.Empty;
            FechaNacimiento = Global.MinDate;
            FechaAlta = DateTime.Now;
            NSS = string.Empty;
            Localidad = "";
            Ciudad = "";
            Curp = "";
            RFC = "";
            Calle = "";
            NumInt = "";
            NumExt = "";
            Colonia = "";
            CodigoPostal = "";
            Estado = "";
            Pais = "";
            IdHuella = -1;
            Telefono = "";
            Telefono2 = "";
            Email = "";
            Email2 = "";
            Domicilio = "";
            Status = "";
            Activo = true;
        }
        public void setQueryConsultar(string query)
        {
            QueryConsultar = query;
        }
        public long Id
        {
            get;
            set;
        }
        public string Nombre_Completo
        {
            get;
            set;
        }
        public string Nombre
        {
            get;
            set;
        }
        public string ApellidoPaterno
        {
            get;
            set;
        }
        public string ApellidoMaterno
        {
            get;
            set;
        }
        public DateTime FechaNacimiento
        {
            get;
            set;
        }
        public DateTime FechaAlta
        {
            get;
            set;
        }
        public string NSS
        {
            get;
            set;
        }
        public string Localidad
        {
            get;
            set;
        }
        public string Ciudad
        {
            get;
            set;
        }
        public string Curp
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
        public string Telefono2
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Email2
        {
            get;
            set;
        }
        public string Domicilio
        {
            get;
            set;
        }
        public long IdHuella
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
        public string Usuario
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public bool Administrador
        {
            get;
            set;
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
                Id = Convert.ToInt64(row["IdEmpleados"]);
                Nombre = row["Nombre"].ToString();
                Domicilio = row["Domicilio"].ToString();
                ApellidoPaterno = row["ApePaterno"].ToString();
                ApellidoMaterno = row["ApeMaterno"].ToString();
                FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]);
                NSS = row["NSS"].ToString();
                Localidad = row["Localidad"].ToString();
                Ciudad = row["Ciudad"].ToString();
                FechaAlta = Convert.ToDateTime(row["FechaAlta"]);
                Curp = row["CURP"].ToString();
                RFC = Convert.ToString(row["RFC"]);
                Calle = row["Calle"].ToString();
                NumInt = row["NumInt"].ToString();
                NumExt = row["NumExt"].ToString();
                Colonia = row["Colonia"].ToString();
                CodigoPostal = Convert.ToString(row["CodigoPostal"]);
                Estado = Convert.ToString(row["Estado"]);
                Pais = Convert.ToString(row["Pais"]);
                Usuario = Convert.ToString(row["Usuario"]);
                Password = Convert.ToString(row["Password"]);
                Telefono = row["Telefono1"].ToString();
                Telefono2 = row["Telefono2"].ToString();
                Email = Convert.ToString(row["Email1"]);
                Email2 = Convert.ToString(row["Email2"]);
                IdHuella = Convert.ToInt64(row["IdHuella"]);
                Activo = Convert.ToBoolean(row["Status"]);
                Status = Convert.ToString(row["EmpleadoStatus"]);
                Nombre_Completo = Nombre + " " + ApellidoPaterno + " " + ApellidoMaterno;
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
