using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using RecyclameV2.Clases;
using RecyclameV2.Utils;

namespace RecyclameV2.Reportes
{
    public partial class XtraImprimeTicket : DevExpress.XtraReports.UI.XtraReport
    {
        string _NombreEmpresa = "";
        string _Domicilio = "";
        string _Ciudad = "";
        string _Cajero = "";
        string _Cliente = "";
        string _RFC = "";

        int _Articulos = 0;
        long subtotal = 0;
        TIPO_MOVIMIENTO tipo = TIPO_MOVIMIENTO.COMPRA;
        public XtraImprimeTicket(long nVentaId)
        {
            InitializeComponent();
        }

        public XtraImprimeTicket(Venta venta)
        {
            InitializeComponent();
            try
            {
                string strImpresoraTickets = Global.ObtenerImpresoraTickets();
                //DatosFacturacion empresa = new DatosFacturacion();
                //empresa.Id = venta.IdDatosFiscales;
                //bool b = empresa.Cargar().Result;
                //_NombreEmpresa = Global.ObtenerEmpresaLabel(empresa.datosFacturacion);
                
                RequisitosFacturacion empresa = Global.obtenerDatosFacturacionDefault();
                if (empresa != null)
                {
                    _NombreEmpresa = empresa.datosFacturacion.Razon_Social;
                    _RFC = empresa.datosFacturacion.RFC + " Telefono: " + empresa.datosFacturacion.Telefono;
                    _Domicilio = empresa.datosFacturacion.Calle + " Ext. " + empresa.datosFacturacion.NumExt + " Int. " + empresa.datosFacturacion.NumInt + " " + empresa.datosFacturacion.Colonia + " C.P. " + empresa.datosFacturacion.CodigoPostal;
                    _Ciudad = empresa.datosFacturacion.Municipio + " " + empresa.datosFacturacion.Estado;
                }
                bsVenta.DataSource = venta;                
                _Cliente = venta.cliente;
                tipo = venta.TIPO;                
                if (venta.TIPO == TIPO_MOVIMIENTO.COMPRA)
                {                    
                    xrTableCell2.Text = "SUBTOTAL COMPRA:";
                    foreach (VentaDetalle d in venta.Detalles)
                    {
                        d.Precio_Venta = Convert.ToDouble(d.Precio_Compra);
                        d.Importe = d.ImporteReal;
                    }
                    venta.Descuento = Convert.ToInt64(venta.Descuento);
                    venta.Subtotal = Convert.ToInt64(venta.Subtotal) + Convert.ToInt64(venta.Descuento);
                    xrLabel10.Visible = false;
                    xrTableCell10.Visible = true;
                    xrTableCell11.Visible = true;
                }
                else
                {
                    xrLabel1.Text = xrLabel1.Text.Replace("Tara", "   ");
                    xrTableCell5.Visible = false;
                    xrLabel10.Visible = true;
                    xrTableCell10.Visible = false;
                    xrTableCell11.Visible = false;
                }
                bsVentaDetalle.DataSource = venta.Detalles;
                string strEmpleado = "";
                foreach (VentaDetalle detalle in venta.Detalles)
                {
                    if (strEmpleado.Length == 0)
                    {
                        strEmpleado = detalle.Quien_Surte;
                    }
                    _Articulos += Convert.ToInt32(detalle.Cantidad);
                }
                _Cajero = strEmpleado;
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
        }

        private void XtraImprimeTicket_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (tipo == TIPO_MOVIMIENTO.COMPRA)
            //{
            //    xrTableCell3.Text = subtotal.ToString("N2");
            //}            
            xrNombreEmpresa.Text = _NombreEmpresa;
            xrCajero.Text = _Cajero;
            xrTotalArticulos.Text = _Articulos.ToString();
            xrDomicilio.Text = _Domicilio;
            xrCiudad.Text = _Ciudad;
            xrNombreCliente.Text = _Cliente;
            xrRFC.Text = _RFC;
        }

    }
}
