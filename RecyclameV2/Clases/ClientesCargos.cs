using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class ClientesCargos : ClaseBase
    {
        public long IdClienteCargo { get; set; }
        public long IdCliente { get; set; }
        public long IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Concepto { get; set; }
        public string Estado { get; set; }
        public double Saldo { get; set; }
        public double Cargos { get; set; }
        public double Abonos { get; set; }
        public long IdAbono { get; set; }
        public long Tipo_Cargo { get; set; }
        public bool Activo { get; set; }
        public string Status { get; set; }
        public ClientesCargos()
        {
            CampoId = "Cliente_Id";
            CampoBusqueda = "Nonbre";
            QueryGrabar = "Clientes_Cargos_Grabar_sp";
            QueryConsultar = "Clientes_Cargos_Consultar_sp";
            QueryBorrar = "Clientes_Cargos_Borrar_sp";
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
                IdClienteCargo = Convert.ToInt64(row["IdClienteCargo"]);
                IdCliente = Convert.ToInt64(row["IdCliente"]);
                IdVenta = Convert.ToInt64(row["IdVenta"]);
                Fecha = Convert.ToDateTime(row["Fecha"]);
                Concepto = Convert.ToString(row["Concepto"]);
                Estado = Convert.ToString(row["Estado"]);
                Saldo = Convert.ToDouble(row["Total"]);
                Cargos = Convert.ToDouble(row["Cargos"]);
                Abonos = Convert.ToDouble(row["Abonos"]);
                Activo = Convert.ToBoolean(row["Status"]);
                IdAbono = Convert.ToInt32(row["IdAbono"]);
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
