using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Preferencias : ClaseBase
    {
        private long _lId = -1;
        private string _strImpresoraTickets = string.Empty;
        private string _strImpresoraFacturas = string.Empty;

        public Preferencias()
        {
            CampoId = "Id";
            QueryGrabar = "Preferencias_Grabar_sp";
            QueryConsultar = "Preferencias_Consultar_sp";
            QueryCancelar = "Preferencias_Borrar_sp";
            QueryBorrar = "Preferencias_Borrar_sp";
            Inicializar();
        }

        public void Inicializar()
        {
            this.Id = -1;
            this.ImpresoraTickets = string.Empty;
            this.ImpresoraFacturas = string.Empty;
        }

        public long Id
        {
            get { return _lId; }
            set { _lId = value; }
        }

        public string ImpresoraTickets
        {
            get { return _strImpresoraTickets; }
            set { _strImpresoraTickets = value; }
        }

        public string ImpresoraFacturas
        {
            get { return _strImpresoraFacturas; }
            set { _strImpresoraFacturas = value; }
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
                ImpresoraTickets = Convert.ToString(row["ImpresoraTickets"]);
                ImpresoraFacturas = Convert.ToString(row["ImpresoraFacturas"]);
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
