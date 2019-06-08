using DevExpress.XtraPrinting;
using RecyclameV2.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Venta : ClaseBase
    {
        private PrinterSettings prnSettings;
        public enum TIPO_VENTA
        {
            CONTADO = 0,
            CREDITO = 1,
            DEMOSTRACION = 2,
            COMPRA = 3,
        }
        public bool Excluir { get; set; }
        public long Id_Venta { get; set; }
        public long Id_Cliente { get; set; }
        public long Id_Sucursal { get; set; }
        public long Id_Empleado { get; set; }
        public string Empleado { get; set; }
        public DateTime Fecha_Venta { get; set; }
        public DateTime Hora_Venta { get; set; }
        public long Id_Cotizacion { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal IEPS { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha_Tipo_Cambio { get; set; }
        public long Id_Tipo_Cambio { get; set; }
        public string Comentario { get; set; }
        public string Status { get; set; }
        public decimal Descuento { get; set; }
        public long Id_Factura { get; set; }
        public long Id_Metodo_pago { get; set; }
        public int Referencia { get; set; }
        public long Id_Tipo_Venta { get; set; }
        public decimal Su_Pago { get; set; }
        public decimal Su_Cambio { get; set; }
        public decimal Debe { get; set; }
        public long Id_Promocion { get; set; }
        public long IdDatosFiscales { get; set; }
        public decimal IVAimporte { get; set; }
        public decimal IEPSimporte { get; set; }
        public decimal ISR_PorCiento { get; set; }

        public BindingList<VentaDetalle> Detalles;
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        protected override string QueryGrabar { get { return "Venta_Grabar_sp"; } }
        override public string CampoId { get { return "Id_Venta"; } }
        override public string CampoBusqueda { get { return "Fecha_Venta"; } }
        protected override string QueryConsultar { get { return "[Ventas_Consultar_sp]"; } }
        public string cliente { get; set; }
        public int Caja { get; set; }
        public long IdAutorizo { get; set; }
        public long TotalArticulos { get; set; }
        public TIPO_MOVIMIENTO TIPO { get; set; }
        private bool Asigno { get; set; }
        private bool demostracion;
        public Venta()
        {
            Id_Venta = -1;
            Id_Cliente = 1;
            Id_Sucursal = -1;
            Id_Empleado = -1;
            QueryBorrar = "Venta_Cancelar_sp";
            Fecha_Venta = Global.MinDate;
            Hora_Venta = Global.MinDate;
            Empleado = "";
            Id_Cotizacion = -1;
            Subtotal = 0;
            IVA = 0;
            IEPS = 0;
            Total = 0;
            Fecha_Tipo_Cambio = Global.MinDate;
            Id_Tipo_Cambio = -1;
            Comentario = "";
            Status = "";
            Descuento = 0;
            Id_Factura = -1;
            Id_Metodo_pago = -1;
            Referencia = -1;
            Id_Tipo_Venta = -1;
            Su_Pago = 0;
            Su_Cambio = 0;
            Debe = 0;
            Id_Promocion = -1;
            IdDatosFiscales = -1;
            IVAimporte = 0;
            IEPSimporte = 0;
            Caja = 1;
            IdAutorizo = -1;
            TotalArticulos = 0;
            Excluir = false;
            demostracion = false;
            Detalles = new BindingList<VentaDetalle>()
            {
                AllowEdit = true,
                AllowNew = true,
                AllowRemove = true,
                RaiseListChangedEvents = true
            };
            TIPO = TIPO_MOVIMIENTO.VENTA;
        }

        public void setDemostracion(bool value)
        {
            demostracion = value;
        }

        #region Metodos

        public void setAsigno(bool value)
        {
            Asigno = value;
        }
        public bool getAsigno()
        {
            return Asigno;
        }
        /// <summary>
        /// Ejecuta el metodo Grabar.
        /// </summary>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        override public bool Grabar()
        {
            bool resultado = false;
            List<SqlParameter> parametros = new List<SqlParameter>();

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@P_Id_Venta";
            paramId.Value = Id_Venta;
            paramId.Direction = System.Data.ParameterDirection.InputOutput;
            parametros.Add(paramId);

            parametros.Add(new SqlParameter() { ParameterName = "@P_IdCliente", Value = Id_Cliente });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IdSucursal", Value = Id_Sucursal });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = Id_Empleado });
            parametros.Add(new SqlParameter() { ParameterName = "@P_FechaVenta", Value = Fecha_Venta });
            parametros.Add(new SqlParameter() { ParameterName = "@P_HoraVenta", Value = Hora_Venta });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IdCotizacion", Value = Id_Cotizacion });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Subtotal", Value = Subtotal });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IVA", Value = IVA });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IEPS", Value = IEPS });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = Total });
            parametros.Add(new SqlParameter() { ParameterName = "@P_FechaTipoCambio", Value = Fecha_Tipo_Cambio });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IdTipoCambio", Value = Id_Tipo_Cambio });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Comentario", Value = Comentario });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = Status });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento", Value = Descuento });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IdFactura", Value = Id_Factura });
            parametros.Add(new SqlParameter() { ParameterName = "@P_MetodoPago", Value = Id_Metodo_pago });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Referencia", Value = Referencia });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IdTipoPago", Value = Id_Tipo_Venta });
            parametros.Add(new SqlParameter() { ParameterName = "@P_SuPago", Value = Su_Pago });
            parametros.Add(new SqlParameter() { ParameterName = "@P_SuCambio", Value = Su_Cambio });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Debe", Value = Debe });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IdPromocion", Value = Id_Promocion });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IVAimporte", Value = IVAimporte });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IEPSimporte", Value = IEPSimporte });
            parametros.Add(new SqlParameter() { ParameterName = "@P_DescuentoPorCiento", Value = ISR_PorCiento });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo", Value = Convert.ToInt32(TIPO) });


            resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
            if (resultado && Id_Venta == -1)
                Id_Venta = Convert.ToInt64(paramId.Value);

            string empleado = string.Empty;
            foreach (VentaDetalle detalle in Detalles)
            {
                detalle.Id_Venta = Id_Venta;
                if (empleado.Length == 0)
                {
                    empleado = detalle.Quien_Surte;
                }
            }
            if (resultado)
            {
                if (GrabarDetalle())
                {
                    if (!demostracion)
                    {
                        XtraImprimeTicket ticket = new XtraImprimeTicket(this);
                        DevExpress.XtraReports.UI.ReportPrintTool pt = new DevExpress.XtraReports.UI.ReportPrintTool(ticket);
                        try
                        {
                            pt.PrintingSystem.StartPrint += new PrintDocumentEventHandler(printingSystem_StartPrint);
                            pt.Print();
                        }
                        catch (Exception ex)
                        {
                            Log.Logger.ErrorException(ex.Message, ex);
                        }
                    }
                }
                if (TIPO == TIPO_MOVIMIENTO.VENTA)
                {
                    ClientesCargos clientecargo = new ClientesCargos();
                    clientecargo.Tipo_Cargo = Id_Tipo_Venta;
                    clientecargo.Fecha = Fecha_Venta;
                    clientecargo.IdVenta = Id_Venta;
                    clientecargo.IdCliente = Id_Cliente;
                    clientecargo.Concepto = Comentario.Replace("Venta ", "Venta Folio No." + Id_Venta + " ");
                    if (((TIPO_VENTA)Id_Tipo_Venta) == TIPO_VENTA.CONTADO)
                    {
                        clientecargo.Cargos = Convert.ToDouble(Total);
                        clientecargo.Abonos = Convert.ToDouble(Total);
                        clientecargo.Saldo = 0;
                    }
                    else
                    {
                        clientecargo.Cargos = Convert.ToDouble(Total);
                        clientecargo.Abonos = Convert.ToDouble(Su_Pago);
                        clientecargo.Saldo = Convert.ToDouble(Debe);
                    }
                    clientecargo.Estado = Status;
                    clientecargo.Activo = true;
                    clientecargo.Grabar();
                    if (((TIPO_VENTA)Id_Tipo_Venta) == TIPO_VENTA.CREDITO)
                    {
                        parametros.Clear();
                        parametros.Add(new SqlParameter() { ParameterName = "@P_Cliente_Id", Value = Id_Cliente });
                        parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = Debe });
                        resultado = (BaseDatos.ejecutarProcedimiento("Clientes_Saldo_Aumentar_sp", parametros) > 0);
                    }
                }
            }

            return resultado;
        }

        private void printingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            // Set the printer name.
            e.PrintDocument.PrinterSettings.PrinterName = Global.ObtenerImpresoraTickets();
        }

        private bool GrabarDetalle()
        {
            bool resultado = true;

            //BorrarDetalle();

            foreach (VentaDetalle detalle in Detalles)
            {
                detalle.setTipo(Convert.ToInt64(TIPO));
                if (!(resultado = detalle.Grabar()))
                    break;
            }

            return resultado;
        }
        public override bool Cargar(System.Data.DataRowView row)
        {
            return Cargar(row.Row);
        }

        public override bool Cargar(System.Data.DataRow row)
        {
            bool resultado = false;

            try
            {
                Id_Venta = Convert.ToInt64(row["IdVenta"]);
                Id_Cliente = Convert.ToInt64(row["IdCliente"]);
                Id_Sucursal = Convert.ToInt64(row["IdSucursal"]);
                Id_Empleado = Convert.ToInt64(row["IdEmpleado"]);
                Fecha_Venta = Convert.ToDateTime(row["Fecha_Venta"]);
                string time = Convert.ToString(row["Hora_Venta"]);
                Hora_Venta = Convert.ToDateTime(Fecha_Venta.ToString("dd/MM/yyyy") + " " + time);
                Id_Cotizacion = Convert.ToInt64(row["IdCotizacion"]);
                Subtotal = Convert.ToDecimal(row["Subtotal"]);
                IVA = Convert.ToDecimal(row["IVA"]);
                IEPS = Convert.ToDecimal(row["IEPS"]);
                Total = Convert.ToDecimal(row["Total"]);
                Fecha_Tipo_Cambio = Convert.ToDateTime(row["FechaTipoCambio"]);
                Id_Tipo_Cambio = Convert.ToInt64(row["IdTipoCambio"]);
                Comentario = Convert.ToString(row["Comentario"]);
                Status = Convert.ToString(row["Status"]);
                Descuento = Convert.ToDecimal(row["Descuento"]);
                Id_Factura = Convert.ToInt64(row["IdFactura"]);
                Id_Metodo_pago = Convert.ToInt64(row["IdMetodoPago"]);
                Referencia = Convert.ToInt32(row["Referencia"]);
                Id_Tipo_Venta = Convert.ToInt64(row["IdTipoVenta"]);
                Su_Pago = Convert.ToDecimal(row["SuPago"]);
                Su_Cambio = Convert.ToDecimal(row["SuCambio"]);
                Debe = Convert.ToDecimal(row["Debe"]);
                Id_Promocion = Convert.ToInt64(row["IdPromocion"]);
                IVAimporte = Convert.ToDecimal(row["IVAimporte"]);
                IEPSimporte = Convert.ToDecimal(row["IEPSimporte"]);
                IdDatosFiscales = Convert.ToInt32(row["IdDatosFiscales"]);
                ISR_PorCiento = Convert.ToDecimal(row["DescuentoPorCiento"]);
                Caja = Convert.ToInt32(row["Caja"]);
                IdAutorizo = Convert.ToInt64(row["IdAutorizo"]);
                TotalArticulos = Convert.ToInt64(row["TotalArticulos"]);
                cliente = Convert.ToString(row["Cliente"]);
                TIPO = (TIPO_MOVIMIENTO)Convert.ToInt32(row["Tipo"]);
                CargarDetalle();

                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.ErrorException(ex.Message, ex);
                resultado = false;
            }

            return resultado;
        }

        public bool CargarVentaListado(System.Data.DataRow row)
        {
            bool resultado = false;

            try
            {
                Id_Venta = Convert.ToInt64(row["Folio"]);
                Id_Cliente = Convert.ToInt64(row["IdCliente"]);
                Id_Sucursal = Convert.ToInt64(row["IdSucursal"]);
                Id_Empleado = Convert.ToInt64(row["IdEmpleado"]);
                Fecha_Venta = Convert.ToDateTime(row["FechaVenta"]);
                string time = Convert.ToString(row["HoraVenta"]);
                Hora_Venta = Convert.ToDateTime(Fecha_Venta.ToString("dd/MM/yyyy") + " " + time);
                Subtotal = Convert.ToDecimal(row["Subtotal"]);
                IVA = Convert.ToDecimal(row["IVA"]);
                IEPS = Convert.ToDecimal(row["IEPS"]);
                Total = Convert.ToDecimal(row["Total"]);
                Id_Factura = Convert.ToInt64(row["IdFactura"]);
                Id_Metodo_pago = Convert.ToInt64(row["IdMetodoPago"]);
                Referencia = Convert.ToInt32(row["Referencia"]);
                Su_Pago = Convert.ToDecimal(row["SuPago"]);
                Su_Cambio = Convert.ToDecimal(row["SuCambio"]);
                Debe = Convert.ToDecimal(row["Debe"]);
                IVAimporte = Convert.ToDecimal(row["IVAimporte"]);
                IEPSimporte = Convert.ToDecimal(row["IEPSimporte"]);
                IdDatosFiscales = Convert.ToInt32(row["IdDatosFiscales"]);
                Caja = 1;
                if (row.Table.Columns.Contains("DescuentoPorCiento"))
                {
                    ISR_PorCiento = Convert.ToDecimal(row["DescuentoPorCiento"]);
                }
                if (row.Table.Columns.Contains("Descuento"))
                {
                    Descuento = Convert.ToDecimal(row["Descuento"]);
                }
                TotalArticulos = Convert.ToInt64(row["TotalArticulos"]);
                cliente = Convert.ToString(row["Cliente"]);
                if (row.Table.Columns.Contains("Excluir"))
                {
                    Excluir = Convert.ToBoolean(row["Excluir"]);
                }
                if (row.Table.Columns.Contains("TotalArticulos"))
                {
                    TotalArticulos = Convert.ToInt64(row["TotalArticulos"]);
                }
                if (row.Table.Columns.Contains("Tipo"))
                {
                    TIPO = (TIPO_MOVIMIENTO)Convert.ToInt32(row["Tipo"]);
                }
                if (row.Table.Columns.Contains("Comentario"))
                {
                    Comentario = Convert.ToString(row["Comentario"]);
                }
                if (row.Table.Columns.Contains("Debe"))
                {
                    Debe = Convert.ToDecimal(row["Debe"]);
                    if (Debe < 0)
                    {
                        Debe = 0;
                    }
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.ErrorException(ex.Message, ex);
                resultado = false;
            }

            return resultado;
        }

        public bool CargarVentaCorteGlobal(System.Data.DataRow row)
        {
            bool resultado = false;

            try
            {
                Id_Venta = Convert.ToInt64(row["Folio"]);
                Id_Empleado = Convert.ToInt64(row["IdEmpleado"]);
                Caja = Convert.ToInt32(row["Caja"]);
                Empleado = Convert.ToString(row["Empleado"]);
                IEPS = Convert.ToDecimal(row["IEPS"]);
                IVA = Convert.ToDecimal(row["IVA"]);
                Subtotal = Convert.ToDecimal(row["Subtotal"]);
                IVAimporte = Convert.ToDecimal(row["IVAImporte"]);
                IEPSimporte = Convert.ToDecimal(row["IEPSImporte"]);
                Total = Convert.ToDecimal(row["Total"]);
                IdDatosFiscales = Convert.ToInt32(row["IdDatosFiscales"]);
                Su_Pago = Convert.ToDecimal(row["SuPago"]);
                Su_Cambio = Convert.ToDecimal(row["SuCambio"]);
                Id_Metodo_pago = Convert.ToInt64(row["IdMetodoPago"]);
                Id_Tipo_Venta = Convert.ToInt64(row["IdTipoVenta"]);
                Id_Factura = Convert.ToInt64(row["IdFactura"]);
                //Excluir = Convert.ToBoolean(row["Excluir"]);
                if (row.Table.Columns.Contains("Tipo"))
                {
                    TIPO = (TIPO_MOVIMIENTO)Convert.ToInt32(row["Tipo"]);
                }
                if (row.Table.Columns.Contains("Comentario"))
                {
                    Comentario = Convert.ToString(row["Comentario"]);
                }
                if (row.Table.Columns.Contains("Debe"))
                {
                    Debe = Convert.ToDecimal(row["Debe"]);
                    if (Debe < 0)
                    {
                        Debe = 0;
                    }
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.ErrorException(ex.Message, ex);
                resultado = false;
            }

            return resultado;
        }

        public bool cargarDetallesListado(DataRow row)
        {
            VentaDetalle ventaDetalle = new VentaDetalle(Id_Venta);
            if (ventaDetalle.CargarDetalleListado(row))
            {
                Detalles.Add(ventaDetalle);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override System.Data.DataTable Listado()
        {
            System.Data.DataTable tabla = new System.Data.DataTable();
            return tabla;
        }

        #endregion;

        private bool CargarDetalle()
        {
            bool resultado = true;
            VentaDetalle ventaDetalle = new VentaDetalle(Id_Venta);
            VentaDetalle detalle;
            DataTable tabla = ventaDetalle.Listado();
            Detalles.Clear();
            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    detalle = new VentaDetalle(Id_Venta);
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

        public static long FolioSiguiente()
        {
            long resultado = 0;
            List<SqlParameter> parametros = new List<SqlParameter>();

            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta("Venta_Folio_Siguiente_sp", parametros);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                foreach (DataRow row in dataset.Tables["Venta_Folio_Siguiente_sp"].Rows)
                {
                    resultado = Convert.ToInt64(row["Max_Folio"]);
                }
            }
            return resultado;
        }

        public static void ImprimirTicket(Venta venta, string cliente, string empleado)
        {
            try
            {
                string strImpresoraTickets = Global.ObtenerImpresoraTickets();
                DatosFacturacion empresa = new DatosFacturacion();
                empresa.Id = venta.IdDatosFiscales;
                bool b = empresa.Cargar().Result;
                string empresaLabel = Global.ObtenerEmpresaLabel(empresa);
                Utils.ImprimirTicket.CreaTicket.AbreCajon(strImpresoraTickets);
                Utils.ImprimirTicket.imprimeTicket(venta, strImpresoraTickets, cliente, empresaLabel, empleado, "1");
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
        }
    }
}
