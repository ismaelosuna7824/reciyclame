using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using RecyclameV2.Clases;

namespace RecyclameV2
{
    public partial class FormRecyclame : MetroForm
    {
        public bool _bInit = false;
        public static Empleado _Empleado = new Empleado();
        public static DataTable _dataTableMetodosPago = null;
        public static RequisitosFacturacion requisitosFacturacion = null;
        public static readonly Dictionary<long, RequisitosFacturacion> dicFacturacion = new Dictionary<long, RequisitosFacturacion>();
        public static readonly Dictionary<long, MetodoPago> dicMetodoPago = new Dictionary<long, MetodoPago>();
        public static DatosFacturacion _datosFacturacion = new DatosFacturacion();
        public static UbicacionFiscal _ubicacionFiscal = new UbicacionFiscal();

        public FormRecyclame()
        {
            InitializeComponent();
        }

        private void tileItemVenta_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            FrmCompraVenta venta = new FrmCompraVenta();
            venta.ShowDialog();
        }

        private void tileItemProovedor_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            FrmProveedores proveedores = new FrmProveedores();
            proveedores.ShowDialog();
        }

        private void tileItemCliente_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            FrmClientes clientes = new FrmClientes();
            clientes.ShowDialog();
        }

        private void tileItemInventario_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            FrmInventario inventario = new FrmInventario();
            inventario.ShowDialog();
        }

        private void tileItemReporte_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            FrmReportes reportes = new FrmReportes();
            reportes.ShowDialog();
        }

        private void tileItemConfiguracion_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            FrmConfiguracion configuracion = new FrmConfiguracion();
            configuracion.ShowDialog();
        }

        private void tileItemEmpleados_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            FrmEmpleados empleados = new FrmEmpleados();
            empleados.ShowDialog();
        }

        private void tileItemBascula_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            /*FrmBascula bascula = new FrmBascula();
            bascula.ShowDialog();*/
        }
    }
}
