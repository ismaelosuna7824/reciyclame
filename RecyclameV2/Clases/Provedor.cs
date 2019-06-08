using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Provedor : ClaseBase
    {
        public long Provedor_Id { get; set; }
        public string Nombre { get; set; }
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
        public Provedor()
        {
            TipoClase = ClaseTipo.Provedor;
            CampoId = "Provedor_Id";
            CampoBusqueda = "RFC";
            QueryGrabar = "Proveedor_Grabar_sp";
            QueryGrabarCodigo = "Provedores_Grabar_sp_codigo";
            QueryConsultar = "Proveedor_Consultar_sp";
            QueryBorrar = "Proveedor_Borrar_sp";
            Provedor_Id = -1;
            FechaAlta = DateTime.Now;
            RFC = "";
            Nombre = "";
            Localidad = "";
            Ciudad = "";
            Razon_Social = "";
            RFC = "";
            Calle = "";
            NumInt = "";
            NumExt = "";
            Colonia = "";
            Codigo_Postal = "";
            Estado = "";
            Pais = "";
            Cuenta_Contable = "";
            Telefono = "";
            Telefono2 = "";
            Telefono3 = "";
            Email = "";
            Email2 = "";
            Email3 = "";
            Domicilio = "";
            Status = "";
            Comentario = "";
            Dias_de_Credito = 0;
            Saldo = 0;
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
        public string Telefono3
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
        #region Metodos


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
                Provedor_Id = Convert.ToInt64(row["IdProveedor"]);
                Nombre = row["Nombre"].ToString();
                Domicilio = row["Domicilio"].ToString();
                Localidad = row["Localidad"].ToString();
                Ciudad = row["Ciudad"].ToString();
                FechaAlta = Convert.ToDateTime(row["FechaAlta"]);
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
                Telefono = row["Telefono1"].ToString();
                Telefono2 = row["Telefono2"].ToString();
                Telefono3 = row["Telefono3"].ToString();
                Email = Convert.ToString(row["Email1"]);
                Email2 = Convert.ToString(row["Email2"]);
                Email3 = Convert.ToString(row["Email3"]);
                Cuenta_Contable = Convert.ToString(row["CuentaContable"]);
                Activo = Convert.ToBoolean(row["Status"]);
                Status = Convert.ToString(row["ProveedorStatus"]);
                Dias_de_Credito = Convert.ToInt32(row["DiasCredito"]);
                Saldo = Convert.ToDouble(row["Saldo"]);
                resultado = true;

                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                resultado = false;
            }

            return resultado;
        }

        ///// <summary>
        ///// Obtiene un listado.
        ///// </summary>
        ///// <returns>El DataTable que se obtiene despues de ejecutar el metodo</returns>
        //public override System.Data.DataTable Listado()
        //{
        //    DataTable resultado = new DataTable();
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Provedor_Id", Value = 0 });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        resultado = dataset.Tables[QueryConsultar];
        //    }
        //    return resultado;
        //}

        #endregion;
    }
}
