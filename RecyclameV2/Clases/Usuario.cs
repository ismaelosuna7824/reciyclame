using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Usuario : ClaseBase
    {
        public enum TIPO
        {
            EMPLEADO = 0,
            ADMINISTRADOR = 1,
        }
        private long _lId = -1;
        private long _lIdEmpleado = -1;
        private long _lIdHuella = -1;
        private string _strNombre = string.Empty;
        private string _strContraseña = string.Empty;
        private TIPO _eTipo = TIPO.EMPLEADO;
        public Usuario()
        {
            CampoId = "Id";
            CampoBusqueda = "IdEmpleado";
            QueryGrabar = "Usuario_Grabar_sp";
            QueryConsultar = "Usuario_Consultar_sp";
            QueryCancelar = "Usuario_Borrar_sp";
        }
        public Usuario(long lId, string strNombre, TIPO eTipo, long idhuella, long idempleado)
        {
            this.Id = lId;
            this._lIdEmpleado = idempleado;
            this.IdHuella = idhuella;
            this.Nombre = strNombre;
            this.Tipo = eTipo;
        }
        public Usuario(long lId, string strNombre, TIPO eTipo, string strContraseña, long idempleado)
        {
            this.Id = lId;
            this._strContraseña = strContraseña;
            this.Nombre = strNombre;
            this.Tipo = eTipo;
            this._lIdEmpleado = idempleado;
        }

        public long Id
        {
            get { return _lId; }
            set { _lId = value; }
        }
        public long IdHuella
        {
            get { return _lIdHuella; }
            set { _lIdHuella = value; }
        }
        public long IdEmpleado
        {
            get { return _lIdEmpleado; }
            set { _lIdEmpleado = value; }
        }

        public string Nombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        public string Contraseña
        {
            get { return _strContraseña; }
            set { _strContraseña = value; }
        }

        public TIPO Tipo
        {
            get { return _eTipo; }
            set { _eTipo = value; }
        }

        public static string ObtenerTipo(TIPO eTipo)
        {
            if (eTipo == TIPO.ADMINISTRADOR)
            {
                return "Administrador";
            }
            if (eTipo == TIPO.EMPLEADO)
            {
                return "Invitado";
            }

            return string.Empty;
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
                if (row.Table.Columns.Contains("IdHuella"))
                {
                    IdHuella = Convert.ToInt64(row["IdHuella"]);
                }
                Id = Convert.ToInt64(row["Id"]);
                IdEmpleado = Convert.ToInt64(row["IdEmpleado"]);
                Nombre = Convert.ToString(row["Nombre"]);
                if (row.Table.Columns.Contains("Password"))
                {
                    Contraseña = Convert.ToString(row["Password"]);
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
