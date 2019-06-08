using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Movimientos : ClaseBase
    {
        public long Movimiento_Id { get; set; }
        public long CFDS_Id { get; set; }
        public DateTime Fecha_Movimiento { get; set; }
        public long Tipo_Movimiento_Id { get; set; }
        public string Sucursal { get; set; }
        public double Flete { get; set; }

        public BindingList<Movimiento_Detalle> Detalles;
        public BindingList<Movimiento_Gastos_Detalle> DetallesGastos;

        //override public string CampoId { get { return "Movimiento_Id"; } }
        //override public string CampoBusqueda { get { return "Fecha_Movimiento"; } }
        //protected override string QueryGrabar { get { return "Movimientos_Grabar_sp"; } }
        //protected override string QueryGrabarCodigo { get { return "Movimientos_Grabar_sp_codigo"; } }
        //protected override string QueryConsultar { get { return "Movimientos_Consultar_sp"; } }
        //protected override string QueryGrabarFlete { get { return "Movimientos_Grabar_sp_flete"; } }

        public Movimientos()
        {
            TipoClase = ClaseTipo.Movimientos;
            CampoId = "Movimiento_Id";
            CampoBusqueda = "Fecha_Movimiento";
            QueryGrabar = "Movimientos_Grabar_sp";
            QueryGrabarCodigo = "Movimientos_Grabar_sp_codigo";
            QueryConsultar = "Movimientos_Consultar_sp";
            Movimiento_Id = -1;
            CFDS_Id = -1;
            Fecha_Movimiento = Global.MinDate;
            Tipo_Movimiento_Id = -1;
            Sucursal = "";
            Detalles = new BindingList<Movimiento_Detalle>()
            {
                AllowEdit = true,
                AllowNew = true,
                AllowRemove = true,
                RaiseListChangedEvents = true
            };
            DetallesGastos = new BindingList<Movimiento_Gastos_Detalle>()
            {
                AllowEdit = true,
                AllowNew = true,
                AllowRemove = true,
                RaiseListChangedEvents = true
            };
        }

        #region Metodos

        ///// <summary>
        ///// Ejecuta el metodo Grabar.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        //override public bool Grabar()
        //{   
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    SqlParameter paramId = new SqlParameter();
        //    paramId.ParameterName = "@P_Movimiento_Id";
        //    paramId.Value = Movimiento_Id;
        //    paramId.Direction = System.Data.ParameterDirection.InputOutput;
        //    parametros.Add(paramId);

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = CFDS_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha_Movimiento", Value = Fecha_Movimiento });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = Tipo_Movimiento_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Sucursal", Value = Sucursal });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Flete", Value = Flete });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
        //    if (resultado && Movimiento_Id == -1)
        //        Movimiento_Id = Convert.ToInt64(paramId.Value);

        //    foreach (Movimiento_Detalle detalle in Detalles)
        //        detalle.Movimiento_Id = Movimiento_Id;

        //    if (resultado)
        //    {
        //        if (!GrabarDetalle())
        //            resultado = false;
        //    }

        //    return resultado;
        //}

        public bool GrabarMaestro(double dFlete)
        {
            bool resultado = false;
            List<SqlParameter> parametros = new List<SqlParameter>();

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@P_Movimiento_Id";
            paramId.Value = Movimiento_Id;
            paramId.Direction = System.Data.ParameterDirection.InputOutput;
            parametros.Add(paramId);

            parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = CFDS_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha_Movimiento", Value = Fecha_Movimiento });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = Tipo_Movimiento_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Sucursal", Value = Sucursal });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Flete", Value = dFlete });

            resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
            if (resultado && Movimiento_Id == -1)
                Movimiento_Id = Convert.ToInt64(paramId.Value);

            /*if (resultado)
            {
                GrabarDetalle_codigo();
            }*/

            return resultado;
        }

        /*public bool GrabarCodigo(int P_Movimiento_Id)
        {
            bool resultado = false;
            
            foreach (Movimiento_Detalle detalle in Detalles)
                detalle.Movimiento_Id = P_Movimiento_Id;

            if (resultado)
            {
                GrabarDetalle();
            }

            return resultado;
        }*/
        public bool GuardarDetalle()
        {
            return GrabarDetalle();
        }
        private bool GrabarDetalle()
        {
            bool resultado = true;

            BorrarDetalle();

            foreach (Movimiento_Detalle detalle in Detalles)
            {
                if (!(resultado = detalle.Grabar()))
                {
                    resultado = false;
                    break;
                }
            }
            return resultado;
        }
        public bool GrabarDetalle_codigo(Movimiento_Detalle detalles)
        {
            bool resultado = true;
            resultado = detalles.Grabar();
            return resultado;
        }

        public bool GuardarDetalleGastos()
        {
            return GrabarDetalleGastos();
        }
        private bool GrabarDetalleGastos()
        {
            bool resultado = true;

            BorrarDetalleGastos();

            foreach (Movimiento_Gastos_Detalle detalle in DetallesGastos)
            {
                if (!(resultado = detalle.Grabar()))
                {
                    resultado = false;
                    break;
                }
            }
            return resultado;
        }
        public bool GrabarDetalleGastos_codigo(Movimiento_Gastos_Detalle detalles)
        {
            bool resultado = true;
            resultado = detalles.Grabar();
            return resultado;
        }
        /*private bool GrabarDetalleFlete()
        {
            bool resultado = true;

            BorrarDetalleFlete();

            foreach (Movimiento_Detalle detalle in Detalles)
            {
                if (!(resultado = detalle.GrabarFlete()))
                    break;
            }

            return resultado;
        }*/

        private bool BorrarDetalle()
        {
            bool resultado = true;
            bool existe = false;
            Movimientos movimiento = new Movimientos();
            movimiento.Movimiento_Id = Movimiento_Id;
            if (movimiento.Cargar().Result)
            {
                foreach (Movimiento_Detalle detalleAnt in movimiento.Detalles)
                {
                    existe = false;
                    foreach (Movimiento_Detalle detalleNva in Detalles)
                    {
                        if (detalleAnt.Movimiento_Detalle_Id == detalleNva.Movimiento_Detalle_Id)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado = detalleAnt.Borrar();
                        if (!resultado)
                            break;
                    }
                }
            }
            return resultado;
        }
        private bool BorrarDetalleGastos()
        {
            bool resultado = true;
            bool existe = false;
            Movimientos movimiento = new Movimientos();
            movimiento.Movimiento_Id = Movimiento_Id;
            if (movimiento.Cargar().Result)
            {
                foreach (Movimiento_Gastos_Detalle detalleAnt in movimiento.DetallesGastos)
                {
                    existe = false;
                    foreach (Movimiento_Gastos_Detalle detalleNva in DetallesGastos)
                    {
                        if (detalleAnt.Movimiento_Gasto_Detalle_Id == detalleNva.Movimiento_Gasto_Detalle_Id)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado = detalleAnt.Borrar();
                        if (!resultado)
                            break;
                    }
                }
            }
            return resultado;
        }
        ///// <summary>
        ///// Carga en los controles la informacion de un registro.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        //public override bool Cargar()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = Movimiento_Id });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        foreach (DataRow row in dataset.Tables[QueryConsultar].Rows)
        //        {
        //            Cargar(row);
        //        }
        //        resultado = dataset.Tables[QueryConsultar].Rows.Count > 0;
        //    }
        //    return resultado;
        //}

        //public override bool Cargar_Mov_CFDI(int nCFDI)
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDI_Id", Value = nCFDI });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta("Movimientos_Consultar_sp_cfdi", parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        foreach (DataRow row in dataset.Tables["Movimientos_Consultar_sp_cfdi"].Rows)
        //        {
        //            Cargar(row);
        //        }
        //        resultado = dataset.Tables["Movimientos_Consultar_sp_cfdi"].Rows.Count > 0;
        //    }
        //    return resultado;
        //}

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
                Movimiento_Id = Convert.ToInt64(row["Movimiento_Id"]);
                CFDS_Id = Convert.ToInt64(row["CFDS_Id"]);
                Fecha_Movimiento = Convert.ToDateTime(row["Fecha_Movimiento"]);
                Tipo_Movimiento_Id = Convert.ToInt64(row["Tipo_Movimiento_Id"]);
                Sucursal = Convert.ToString(row["Sucursal"]);
                /*Flete = Convert.ToDouble(row["Flete"]);*/

                CargarDetalle();
                CargarDetalleGastos();
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

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = 0 });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        resultado = dataset.Tables[QueryConsultar];
        //    }
        //    return resultado;
        //}

        #endregion;

        private bool CargarDetalle()
        {
            bool resultado = true;
            Movimiento_Detalle movimientoDetalle = new Movimiento_Detalle(Movimiento_Id);
            Movimiento_Detalle detalle;
            DataTable tabla = movimientoDetalle.Listado();
            Detalles.Clear();
            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    detalle = new Movimiento_Detalle(Movimiento_Id);
                    if (detalle.Cargar(row))
                        Detalles.Add(detalle);
                    else
                    {
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }
        private bool CargarDetalleGastos()
        {
            bool resultado = true;
            Movimiento_Gastos_Detalle movimientoDetalle = new Movimiento_Gastos_Detalle(Movimiento_Id);
            Movimiento_Gastos_Detalle detalle;
            DataTable tabla = movimientoDetalle.Listado();
            Detalles.Clear();
            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    detalle = new Movimiento_Gastos_Detalle(Movimiento_Id);
                    if (detalle.Cargar(row))
                        DetallesGastos.Add(detalle);
                    else
                    {
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }
    }
}
