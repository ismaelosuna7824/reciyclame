using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecyclameV2.Clases;

namespace RecyclameV2
{
    public class Cliente : ClaseBase
    {
        public long Cliente_Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC
        {
            get;
            set;
        }
        public string Razon_Social
        {
            get;
            set;
        }
        public string Cuenta_Contable
        {
            get;
            set;
        }
        public Cliente()
        {
            CampoId = "Cliente_Id";
            CampoBusqueda = "Nombre";
            QueryGrabar = "Cliente_Grabar_sp";
            QueryGrabarCodigo = "Cliente_Grabar_sp_codigo";
            QueryConsultar = "Cliente_Consultar_sp";
            QueryBorrar = "Cliente_Borrar_sp";
            Cliente_Id = -1;
            FechaAlta = DateTime.Now;
            RFC = "";
            ApellidoPaterno = "";
            ApellidoMaterno = "";
            Nombre = "";
            Localidad = "";
            Ciudad = "";
            Razon_Social = "";
            Calle = "";
            NumInt = "";
            NumExt = "";
            Colonia = "";
            Codigo_Postal = "";
            Estado = "";
            Pais = "";
            Cuenta_Contable = "";
            Telefono = 0;
            Telefono2 = 0;
            Telefono3 = 0;
            Email = "";
            Email2 = "";
            Email3 = "";
            Domicilio = "";
            Status = "";
            Comentario = "";
            Dias_de_Credito = 0;
            Saldo = 0;
            Monto_Credito = 0;
        }

        public DateTime FechaAlta
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
        public string Codigo_Postal
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
        public String Domicilio
        {
            get;
            set;
        }
        public long Telefono
        {
            get;
            set;
        }
        public long Telefono2
        {
            get;
            set;
        }
        public long Telefono3
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
        public string Email3
        {
            get;
            set;
        }
        public string Comentario
        {
            get;
            set;
        }
        public bool Activo
        {
            get;
            set;
        }
        public int Dias_de_Credito
        {
            get;
            set;
        }
        public double Saldo
        {
            get;
            set;
        }
        public String Status
        {
            get;
            set;
        }
        public double Monto_Credito
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
                Cliente_Id = Convert.ToInt64(row["IdCliente"]);
                Nombre = row["Nombre"].ToString();
                ApellidoMaterno = row["ApeMaterno"].ToString();
                ApellidoPaterno = row["ApePaterno"].ToString();
                Domicilio = row["Domicilio"].ToString();
                Localidad = row["Localidad"].ToString();
                Ciudad = row["Ciudad"].ToString();
                if (row["FechaAlta"] != null)
                {
                    FechaAlta = Convert.ToDateTime(row["FechaAlta"]);
                }
                RFC = Convert.ToString(row["RFC"]);
                Calle = row["Calle"].ToString();
                NumInt = row["NumInt"].ToString();
                NumExt = row["NumExt"].ToString();
                Colonia = row["Colonia"].ToString();
                Codigo_Postal = Convert.ToString(row["CodigoPostal"]);
                Estado = Convert.ToString(row["Estado"]);
                Pais = Convert.ToString(row["Pais"]);
                Comentario = Convert.ToString(row["Comentario"]);
                Razon_Social = Convert.ToString(row["RazonSocial"]);
                Telefono = Convert.ToInt64(row["Telefono1"]);
                Telefono2 = Convert.ToInt64(row["Telefono2"]);
                Telefono3 = Convert.ToInt64(row["Telefono3"]);
                Email = Convert.ToString(row["Email1"]);
                Email2 = Convert.ToString(row["Email2"]);
                Email3 = Convert.ToString(row["Email3"]);
                Cuenta_Contable = Convert.ToString(row["CuentaContable"]);
                Activo = Convert.ToBoolean(row["Status"]);
                Status = Convert.ToString(row["ClienteStatus"]);
                Dias_de_Credito = Convert.ToInt32(row["DiasCredito"]);
                Saldo = Convert.ToDouble(row["Saldo"]);
                Monto_Credito = Convert.ToDouble(row["MontoCredito"]);
                resultado = true;

                resultado = true;
            }
            catch (Exception ex)
            {
                //Log.Logger.addLogEntry(ex.Message);
                resultado = false;
            }

            return resultado;
        }
    }
}
