using MetroFramework.Forms;
using RecyclameV2.Clases;
using RecyclameV2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecyclameV2
{
    public partial class FrmFormaPago : MetroForm
    {
        string metodoPago = string.Empty;
        long idMetodoPago = -1;
        //DataTable table = null;
        public enum FORMA_PAGO
        {
            EFECTIVO = 0,
            CREDITO = 1,
            TARJETA = 2,
            TARJETA_EFECTIVO = 3,
            CHEQUE = 4
        }
        FORMA_PAGO pago = FORMA_PAGO.EFECTIVO;
        FrmCompraVenta frmVenta = null;
        Timer t = null;

        public FrmFormaPago(FrmCompraVenta venta)
        {
            InitializeComponent();
            //MetodoPago p = new MetodoPago();
            //table = p.Listado();
            frmVenta = venta;
            if (frmVenta.tipo == TIPO_MOVIMIENTO.COMPRA)
            {
                //btnBuscar.BackgroundImage = (Bitmap)RecyclameV2.Properties.Resources.pagar;
                metroLabel5.Text = "F[10] Pagar";
                cargarControles();
                txtSuPago.Numero = Convert.ToDouble(frmVenta.getTotalVenta());
            }
            else
            {
                cargarControles();
            }
            actualizarUI("btnEfectivo", true);
        }
        public static long obtenerMetodoId(string opcion)
        {
            if (FormRecyclame._dataTableMetodosPago != null)
            {
                foreach (DataRow row in FormRecyclame._dataTableMetodosPago.Rows)
                {
                    if (string.Compare(row["Metodo"].ToString(), opcion, true) == 0)
                    {
                        return Convert.ToInt64(row["Id"]);
                    }
                }
            }
            return -1;
        }
        private void actualizarUI(string option, bool firstTime)
        {
            if (firstTime)
            {
                if (frmVenta.tipo == TIPO_MOVIMIENTO.VENTA)
                {
                    txtCambio.Text = "0.00";
                    txtSuPago.Numero = 0;
                    txtRestante.Numero = 0;
                    txtReferencia.Numero = 0;
                }
            }
            if (string.Compare(option, "btnefectivo", true) == 0)
            {
                btnCredito.BackColor = Color.White;
                metodoPago = "01 Efectivo";
                pago = FORMA_PAGO.EFECTIVO;
                lblSuPago.Text = "Su Pago:";
                lblCambio.Text = "Su Cambio:";
                lblRestan.Visible = false;
                txtRestante.Visible = false;
                lblReferencia.Visible = false;
                txtReferencia.Visible = false;
                lblSuPago.Location = new Point(88, 76);
                lblCambio.Location = new Point(73, 114);
                txtCambio.Location = new Point(180, 108);
                btnEfectivo.Select();
                idMetodoPago = obtenerMetodoId("efectivo");
            }
            else if (string.Compare(option, "btncredito", true) == 0)
            {
                btnEfectivo.BackColor = Color.White;
                pago = FORMA_PAGO.CREDITO;
                //lblSuPago.Text = "Su Pago con Tarjeta:";
                //lblRestan.Text = "Su Pago en Efectivo";
                //lblRestan.Enabled = true;
                //lblRestan.Location = new Point(30, 114);
                //lblRestan.Visible = true;
                //txtRestante.Visible = true;
                //lblReferencia.Visible = true;
                //txtReferencia.Visible = true;
                lblSuPago.Location = new Point(88, 76);
                lblCambio.Text = "Restan:";
                lblCambio.Location = new Point(93, 114);
                //txtCambio.Location = new Point(180, 144);
                //lblReferencia.Location = new Point(87, 185);
                //txtReferencia.Location = new Point(180, 179);
                btnCredito.Select();
            }
            if (frmVenta.tipo == TIPO_MOVIMIENTO.VENTA)
            {
                txtSuPago.Focus();
            }
            else
            {
                btnBuscar.Focus();
            }
        }

        private void cargarControles()
        {
            txtTotalPago.Text = frmVenta.getTotalVenta();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (pago == FORMA_PAGO.EFECTIVO)
            {
                try
                {
                    double dTotal_Venta = Convert.ToDouble(txtTotalPago.Text);
                    double dPago_Efectivo = txtSuPago.Numero;
                    double dPago_Tarjeta = 0;
                    if (dTotal_Venta <= (dPago_Efectivo + dPago_Tarjeta))
                    {
                        long empleadoid = -1;
                        string empleado = string.Empty;
                        empleadoid = frmVenta.obtenerEmpleadoId();
                        empleado = frmVenta.obtenerNombreEmpleado();
                        double TotalPagar = Global.StringToDouble(txtTotalPago.Text);
                        double SuPago = txtSuPago.Numero;
                        int count = 1;
                        bool result = true;
                        int length = frmVenta.gridViewDetalleVenta.RowCount;
                        Venta v = new Venta();
                        decimal subTotal = 0;
                        double iva = 0;
                        double descuento = 0;
                        for (int i = 0; i < length; i++)
                        {
                            VentaDetalle detalle = (VentaDetalle)frmVenta.gridViewDetalleVenta.GetRow(i);
                            descuento += detalle.Descuento_ISR;
                            iva += 0;
                            subTotal += detalle.Importe;
                            detalle.Id_Sucursal = 0;
                            detalle.Quien_Surte = empleado;
                            v.Detalles.Add(detalle);
                        }
                        v.IdDatosFiscales = 0;
                        v.Id_Empleado = empleadoid;
                        frmVenta.agregarDatosVenta(ref v);
                        //v.Id_Tipo_Venta = Convert.ToInt32(Venta.TIPO_VENTA.CONTADO);
                        v.Id_Metodo_pago = idMetodoPago;
                        v.cliente = frmVenta.obtenerNombreCliente();
                        v.Status = "PAGADA";
                        v.Subtotal = subTotal - Convert.ToDecimal(iva);
                        v.IVAimporte = 0;
                        v.Total = subTotal;// +Convert.ToDecimal(iva);
                        v.Descuento = Convert.ToDecimal(descuento);
                        if (v.TIPO == TIPO_MOVIMIENTO.VENTA)
                        {
                            v.Comentario = "Venta de contado con un total de $" + txtTotalPago.Text + " en efectivo.";
                        }
                        else
                        {
                            v.Comentario = "Compra de contado con un total de $" + txtTotalPago.Text + " en efectivo.";
                        }
                        v.Su_Pago = (decimal)SuPago;
                        v.Su_Cambio = v.Su_Pago - v.Total;
                        count++;
                        if (result)
                        {
                            result = v.Grabar();
                        }
                        if (result)
                        {
                            if (v.TIPO == TIPO_MOVIMIENTO.VENTA)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(this, "La venta se guardó correctamente.", this.ProductName, MessageBoxButtons.OK);
                            }
                            else
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(this, "La compra se guardó correctamente.", this.ProductName, MessageBoxButtons.OK);
                            }
                            frmVenta.Limpiar();
                            this.Close();
                        }
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "El pago no cubre el total de la venta.", this.ProductName, MessageBoxButtons.OK);
                        txtSuPago.Focus();
                    }
                    //}
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    double dTotal_Venta = Convert.ToDouble(txtTotalPago.Text);
                    double dPago_Efectivo = 0;
                    double dPago_Tarjeta = 0;
                    dPago_Tarjeta = txtRestante.Numero;
                    dPago_Efectivo = txtSuPago.Numero;
                    bool adelante = false;
                    if (txtRestante.Numero > 0)
                    {
                        if (txtReferencia.Numero == 0)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show("Favor de agregar los últimos 4 digitos de la tarjeta o cuenta para referenciar la venta.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtReferencia.Focus();
                            return;
                        }
                    }
                    adelante = (dTotal_Venta >= (dPago_Efectivo + dPago_Tarjeta) && Global.StringToDouble(txtCambio.Text) >= 0);
                    if (adelante)//(dTotal_Venta <= (dPago_Efectivo + dPago_Tarjeta)) || pago == FORMA_PAGO.CREDITO)
                    {
                        long empleadoid = -1;
                        string empleado = string.Empty;
                        empleadoid = frmVenta.obtenerEmpleadoId();
                        empleado = frmVenta.obtenerNombreEmpleado();
                        double TotalPagar = Global.StringToDouble(txtTotalPago.Text);
                        double SuPago = dPago_Efectivo + dPago_Tarjeta;
                        int count = 1;
                        bool result = true;
                        int length = frmVenta.gridViewDetalleVenta.RowCount;
                        Venta v = new Venta();
                        decimal subTotal = 0;
                        double iva = 0;
                        double descuento = 0;
                        for (int i = 0; i < length; i++)
                        {
                            VentaDetalle detalle = (VentaDetalle)frmVenta.gridViewDetalleVenta.GetRow(i);
                            descuento += 0;
                            iva += 0;
                            subTotal += detalle.Importe;
                            detalle.Id_Sucursal = 0;
                            detalle.Quien_Surte = empleado;
                            v.Detalles.Add(detalle);
                        }
                        v.IdDatosFiscales = 0;
                        v.Id_Empleado = empleadoid;
                        frmVenta.agregarDatosVenta(ref v);
                        v.Id_Tipo_Venta = Convert.ToInt32(Venta.TIPO_VENTA.CREDITO);
                        v.Id_Metodo_pago = idMetodoPago;
                        v.cliente = frmVenta.obtenerNombreCliente();
                        v.Status = "CREDITO";
                        v.Subtotal = subTotal - Convert.ToDecimal(iva);
                        v.IVAimporte = 0;
                        v.Total = subTotal;// +Convert.ToDecimal(iva);
                        v.Descuento = Convert.ToDecimal(descuento);
                        if (dPago_Efectivo > 0)
                        {
                            if (v.TIPO == TIPO_MOVIMIENTO.VENTA)
                            {
                                v.Comentario = "Venta a crédito con enganche o abono de $" + Global.DoubleToString(dPago_Efectivo) + " en efectivo. Quedando un saldo de $" + txtCambio.Text + ".";
                            }
                            else
                            {
                                v.Comentario = "Compra a crédito con enganche o abono de $" + Global.DoubleToString(dPago_Efectivo) + " en efectivo. Quedando un saldo de $" + txtCambio.Text + ".";
                            }
                            v.Su_Pago = Convert.ToDecimal(dPago_Efectivo);
                            v.Debe = Convert.ToDecimal(Global.StringToDouble(txtCambio.Text));
                        }
                        else
                        {
                            if (v.TIPO == TIPO_MOVIMIENTO.VENTA)
                            {
                                v.Comentario = "Venta a crédito con un saldo de $" + txtTotalPago.Text;
                            }
                            else
                            {
                                v.Comentario = "Compra a crédito con un saldo de $" + txtTotalPago.Text;
                            }
                            v.Debe = Convert.ToDecimal(txtTotalPago.Text);
                        }
                        count++;
                        if (result)
                        {
                            result = v.Grabar();
                        }
                        if (result)
                        {
                            if (v.TIPO == TIPO_MOVIMIENTO.VENTA)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(this, "La venta se guardó correctamente.", this.ProductName, MessageBoxButtons.OK);
                            }
                            else
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(this, "La compra se guardó correctamente.", this.ProductName, MessageBoxButtons.OK);
                            }
                            frmVenta.Limpiar();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
