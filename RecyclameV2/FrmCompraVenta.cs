using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using MetroFramework.Forms;
using RecyclameV2.Clases;
using RecyclameV2.Formularios;

namespace RecyclameV2
{
    public partial class FrmCompraVenta : MetroForm
    {
        bool _actualizarTotales = false;
        long _clienteId = 0;
        Timer t = null;
        // public static readonly Dictionary<long, List<VentaDetalle>> dicVentas = new Dictionary<long, List<VentaDetalle>>();
        public static readonly Dictionary<string, Venta> dicVentasPendientes = new Dictionary<string, Venta>();
        DataTable movimientos = new DataTable();
        public TIPO_MOVIMIENTO tipo = TIPO_MOVIMIENTO.COMPRA;
        string ultimoclienteSeleccionado = string.Empty;
        string ultimocliente = string.Empty;
        string column = string.Empty;
        Timer tColumn = new Timer();

        public FrmCompraVenta()
        {
            InitializeComponent();
            dicVentasPendientes.Clear();
            lblFolio.Text = String.Format("{0:######0}", Venta.FolioSiguiente());
            t = new Timer();
            t.Enabled = true;
            t.Interval = 50;
            t.Tick += t_Tick;
            tColumn.Interval = 50;
            tColumn.Tick += tColumn_Tick; ;
            movimientos.Clear();
            movimientos.Columns.Add("IdMovimiento", typeof(decimal));
            movimientos.Columns.Add("Nombre", typeof(string));
            DataRow _movimiento = movimientos.NewRow();
            _movimiento["IdMovimiento"] = 0;
            _movimiento["Nombre"] = "Compra";
            movimientos.Rows.Add(_movimiento);
            _movimiento = movimientos.NewRow();
            _movimiento["IdMovimiento"] = 1;
            _movimiento["Nombre"] = "Venta";
            movimientos.Rows.Add(_movimiento);
            //cargarEmpleados();
            cargarCombosSucursales();
            txtEmpleado.Text = FormRecyclame._Empleado.Nombre;
        }

        void tColumn_Tick(object sender, EventArgs e)
        {
            tColumn.Stop();
            if (string.Compare(column, "cantidad", true) == 0)
            {
                gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[4];
                gridViewDetalleVenta.ShowEditor();
            }
            else if (string.Compare(column, "tara", true) == 0)
            {
                gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[6];
                gridViewDetalleVenta.ShowEditor();
            }

        }
        private void cargarCombosSucursales()
        {
            Herramientas.LlenarCombo(cboTipo, movimientos, "IdMovimiento", "Nombre");
            cboTipo.EditValue = Convert.ToDecimal(0);
        }
        //private void cargarEmpleados()
        //{
        //    if (cmbEmpleado.ItemIndex == -1)
        //    {
        //        object value = cmbEmpleado.EditValue;
        //        Empleado empleado = new Empleado();
        //        List<SqlParameter> parametros = new List<SqlParameter>();
        //        DataTable table = BaseDatos.ejecutarProcedimientoConsultaDataTable("Empleados_Consultar_Orden_Gasto_sp", null);
        //        Herramientas.LlenarCombo(cmbEmpleado, table, empleado.CampoId, empleado.CampoBusqueda);
        //        cmbEmpleado.EditValue = (Decimal)obtenerIdEmpleadoSeleccionar(table);
        //    }
        //}
        private long obtenerIdEmpleadoSeleccionar(DataTable table)
        {
            try
            {
                foreach (DataRow row in table.Rows)
                {
                    return Convert.ToInt64(row["IdEmpleados"]);
                }
            }
            catch { }
            return 0;
        }
        void t_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dddd, dd MMMM, yyyy h:mm tt");
        }
        private void FrmVenta_Load(object sender, EventArgs e)
        {
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);
            txtCliente.Focus();
            Limpiar();
        }
        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDetalleVenta.SelectedRowsCount > 0)
                {
                    int rowHandle = gridViewDetalleVenta.GetSelectedRows()[0];
                    var rowview = (VentaDetalle)gridViewDetalleVenta.GetRow(rowHandle);
                    gridViewDetalleVenta.DeleteRow(rowHandle);
                    RefrescarMontos();
                }
                else
                {
                    txtProducto.Focus();
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(this, ex.Message, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefrescarMontos()
        {
            double subtotal = 0;
            double total = 0;
            double pagoEfectivo = 0;
            double pagoTarjeta = 0;
            double cambio = 0;
            double descuento = 0;
            if (gridViewDetalleVenta.RowCount > 0)
            {
                int length = gridViewDetalleVenta.RowCount;
                //decimal ivaporciento = 0;
                for (int i = 0; i < length; i++)
                {
                    VentaDetalle detalle = (VentaDetalle)gridViewDetalleVenta.GetRow(i);
                    detalle.Importe = Convert.ToInt64(detalle.Importe);
                    subtotal += Convert.ToDouble(detalle.Importe);
                    descuento += detalle.Descuento_ISR;
                    //detalle.IVAimporte = Convert.ToDouble(Math.Round((detalle.Importe - (detalle.Importe / ivaporciento)), 2));
                }
            }
            double importe = subtotal;
            //double importeiva = 0;

            //importeiva = 0;// Math.Round((importe - (importe / 1.16)), 2);
            txtTotalVenta.Text = Global.DoubleToString(total = subtotal);
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtProducto.Text.Trim().Length > 0)
                {
                    MuestraProductos();
                    RefrescarMontos();

                }
                else
                {
                    btnProducto_Click(null, null);
                }
            }
        }

        private void btnCantidad_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = string.Empty });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Codigo_de_Barras", Value = string.Empty });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = DateTime.Now });
            string query = "Productos_Busqueda_Venta_Consultar";
            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(query, parametros);
            DataTable resultado = null;
            if (dataset != null && dataset.Tables.Count > 0)
            {
                resultado = dataset.Tables[query];
            }
            if (resultado != null)
            {
                fillSeleccionarProducto(resultado, true);
            }
            else
            {
                Productos productos = new Productos();
                fillSeleccionarProducto(productos.Listado(), false);
            }
        }

        private void fillSeleccionarProducto(DataTable table, bool fromVenta)
        {
            CFDS_Producto producto = new CFDS_Producto();
            Productos productos = new Productos();
            using (FrmBusqueda busqueda = new FrmBusqueda(table)
            {
                Width = 800,
                Text = "Productos",
                AjustarColumnas = true,
                ColumnasOcultar = new List<string> { "IdProducto", "CampoId", "CampoBusqueda", "IdLinea1", "IdLinea2", "IdLinea3", "Status", "Serie", "IdDatosFiscales", "CodigoProducto", "IVA", "IEPS", "PrecioMayoreo", "CantidadMayoreo", "PrecioPromocion" }
            })
            {
                if (busqueda.ShowDialog() == DialogResult.OK)
                {
                    productos.setFromVenta(fromVenta);
                    if (busqueda.FilaDatos != null && productos.Cargar((DataRowView)busqueda.FilaDatos))
                    {
                        producto.ClearProducto();
                        producto.Producto_Id = productos.Producto_Id;
                        MuestraProductos(productos);
                        gridDetalleVenta.RefreshDataSource();
                        RefrescarMontos();
                    }
                }
            }
        }
        private void BuscarProducto(int rowHandle)
        {
            this.Cursor = Cursors.WaitCursor;
            CFDS_Producto producto = (CFDS_Producto)gridViewDetalleVenta.GetRow(rowHandle);
            if (producto != null)
            {
                Productos productos = new Productos();
                using (FrmBusqueda busqueda = new FrmBusqueda(productos.Listado())
                {
                    Width = 800,
                    Text = "Productos",
                    AjustarColumnas = true,
                    ColumnasOcultar = new List<string> { "IdProducto", "CampoId", "CampoBusqueda", "IdLinea1", "IdLinea2", "IdLinea3", "Status", "Serie", "IdDatosFiscales", "CodigoProducto", "IVA", "IEPS", "PrecioMayoreo", "CantidadMayoreo", "PrecioPromocion" }
                })
                {
                    if (busqueda.ShowDialog() == DialogResult.OK)
                    {
                        if (busqueda.FilaDatos != null && productos.Cargar((DataRowView)busqueda.FilaDatos))
                        {
                            producto.ClearProducto();
                            producto.Producto_Id = productos.Producto_Id;
                            if (producto.Diferencia_Costo > 0) { }

                            gridDetalleVenta.RefreshDataSource();
                        }
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        public string obtenerNombreCliente()
        {
            return txtCliente.Text;
        }
        public void MuestraProductos()
        {
            try
            {
                Productos producto = new Productos();
                object objProducto = producto.buscarProductoVenta(txtProducto.Text);
                if (objProducto != null)
                {
                    if (objProducto is DataTable)
                    {
                        fillSeleccionarProducto((DataTable)objProducto, true);
                    }
                    else
                    {
                        producto = (Productos)objProducto;
                        if (tipo == TIPO_MOVIMIENTO.VENTA)
                        {
                            if (producto.Detalle.Cantidad <= 0)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("El Producto se encuentra agotado en el Sistema.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtProducto.Text = "";
                                txtProducto.Focus();
                            }
                            else
                            {
                                bool bSobreVenta = false;
                                BindingList<VentaDetalle> lista = (BindingList<VentaDetalle>)gridDetalleVenta.DataSource;
                                if (gridDetalleVenta.DataSource != null)
                                {
                                    foreach (VentaDetalle p in lista)
                                    {
                                        if (p.Id_Producto == producto.Producto_Id)
                                        {
                                            p.Cantidad++;
                                            if (producto.Detalle.Cantidad < p.Cantidad)
                                            {
                                                DevExpress.XtraEditors.XtraMessageBox.Show(this, "No puedes vender mas articulos de los que tienes dado de alta en el Sistema", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                bSobreVenta = true;
                                                p.Cantidad--;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (!bSobreVenta)
                                {
                                    AgregarVentaGrid(producto);
                                    Herramientas.GridViewEditarColumnas(
                                        gridViewDetalleVenta,
                                        true,
                                        true,
                                        false,
                                        new List<string>() { "Id_Venta_Detalle", "Id_Venta", "Id_Producto", "CampoId", "Quien_Surte", "Id_Sucursal", "Surtido", "IEPS", "IVA", "CampoBusqueda", "TipoClase", "Precio_Compra", "Precio_Mayoreo", "Precio_Original", "IEPSimporte", "IVAimporte", "IdDatosFiscales", "IdVentas", "UltimaCantidad", "UltimaTara", "IVA", "ImporteReal" },
                                        new List<string> { "Cantidad", "Precio_Venta", "Tara" },
                                        new List<string> { "Cantidad", "ISR_PorCiento", "Existencia", "Tara" }
                                        );
                                    RefrescarMontos();
                                    txtProducto.Text = "";
                                    //txtProducto.Focus();
                                }
                            }
                        }
                        else
                        {
                            BindingList<VentaDetalle> lista = (BindingList<VentaDetalle>)gridDetalleVenta.DataSource;
                            if (gridDetalleVenta.DataSource != null)
                            {
                                int length = lista.Count;
                                for (int i = 0; i < length; i++)
                                {
                                    VentaDetalle p = lista[i];
                                    if (p.Id_Producto == producto.Producto_Id)
                                    {
                                        p.Cantidad++;
                                        actualizarPrecios(ref p);
                                    }
                                }
                            }
                            AgregarVentaGrid(producto);
                            mostrarOculatrcolumnas(tipo);
                            RefrescarMontos();
                            txtProducto.Text = "";
                            //txtProducto.Focus();
                        }
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("El Producto no está definido en el catálogo.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProducto.Text = "";
                    //txtProducto.Focus();
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(this, ex.Message, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void seleccionarRow(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            GridViewInfo viewinfo = view.GetViewInfo() as GridViewInfo;
            if (viewinfo.VScrollBarPresence == ScrollBarPresence.Visible)
            {
                view.MakeRowVisible(rowHandle);

            }
            view.SelectRow(rowHandle);
            view.FocusedRowHandle = rowHandle;
            view.FocusedColumn = view.Columns[4];
        }
        public void MuestraProductos(Productos producto)
        {
            try
            {
                if (tipo == TIPO_MOVIMIENTO.VENTA)
                {
                    if (producto.Detalle.Cantidad <= 0)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("El Producto se encuentra agotado en el Sistema.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtProducto.Text = "";
                        txtProducto.Focus();
                    }
                    else
                    {
                        bool bSobreVenta = false;
                        BindingList<VentaDetalle> lista = (BindingList<VentaDetalle>)gridDetalleVenta.DataSource;
                        if (gridDetalleVenta.DataSource != null)
                        {
                            int length = lista.Count;
                            for (int i = 0; i < length; i++)
                            {
                                VentaDetalle p = lista[i];
                                if (p.Id_Producto == producto.Producto_Id)
                                {
                                    double cantidad = p.Cantidad;
                                    if (producto.Detalle.Cantidad < cantidad)
                                    {
                                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "No puedes vender mas articulos de los que tienes dado de alta en el Sistema.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        bSobreVenta = true;
                                        p.Cantidad--;
                                        break;
                                    }
                                    else
                                    {
                                        p.Cantidad++;
                                        actualizarPrecios(ref p);
                                    }
                                }
                            }
                        }
                        if (!bSobreVenta)
                        {
                            AgregarVentaGrid(producto);
                            mostrarOculatrcolumnas(tipo);
                            RefrescarMontos();
                            txtProducto.Text = "";
                            //txtProducto.Focus();
                        }
                    }
                }
                else
                {
                    BindingList<VentaDetalle> lista = (BindingList<VentaDetalle>)gridDetalleVenta.DataSource;
                    if (gridDetalleVenta.DataSource != null)
                    {
                        int length = lista.Count;
                        for (int i = 0; i < length; i++)
                        {
                            VentaDetalle p = lista[i];
                            if (p.Id_Producto == producto.Producto_Id)
                            {
                                p.Cantidad++;
                                actualizarPrecios(ref p);
                            }
                        }
                    }
                    AgregarVentaGrid(producto);
                    mostrarOculatrcolumnas(tipo);
                    RefrescarMontos();
                    txtProducto.Text = "";
                    //txtProducto.Focus();
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(this, ex.Message, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarVentaGrid(Productos producto)
        {
            BindingList<VentaDetalle> lista = (BindingList<VentaDetalle>)gridDetalleVenta.DataSource;
            VentaDetalle p = null;
            bool found = false;
            int index = -1;
            if (gridDetalleVenta.DataSource != null)
            {
                foreach (VentaDetalle v in lista)
                {
                    if (v.Id_Producto == producto.Producto_Id)
                    {
                        if (tipo == TIPO_MOVIMIENTO.VENTA)
                        {
                            if (producto.Detalle.Cantidad < v.Cantidad)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(this, "No puedes vender mas articulos de los que tienes dado de alta en el Sistema", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        found = true;
                    }
                    index++;
                    if (found)
                    {
                        break;
                    }
                }
                if (!found)
                {
                    p = new VentaDetalle();
                    if (tipo == TIPO_MOVIMIENTO.COMPRA)
                    {
                        p.ISR_PorCiento = 5;
                    }
                    p.Precio_Original = producto.Detalle.Precio_General;
                    p.Id_Producto = producto.Producto_Id;
                    p.IdDatosFiscales = producto.IdDatosFiscales;
                    p.Descripcion = producto.Descripcion;
                    p.Unidad_Medida = producto.Unidad_de_Medida;
                    p.Cantidad = 1;
                    p.Precio_Venta = producto.Detalle.Precio_General;
                    p.Precio_Compra = Convert.ToDecimal(producto.Detalle.Precio_Compra);
                    p.Precio_Mayoreo = p.Precio_Compra;
                    p.Existencia = Convert.ToInt32(producto.Detalle.Cantidad);
                    p.IVA = 0;
                    p.IEPS = producto.Detalle.IEPS;
                    actualizarPrecios(ref p);
                    lista.Add(p);
                    index++;
                    gridDetalleVenta.DataSource = lista;
                }
                gridDetalleVenta.RefreshDataSource();
            }
            else
            {
                lista = new BindingList<VentaDetalle>();
                p = new VentaDetalle();
                if (tipo == TIPO_MOVIMIENTO.COMPRA)
                {
                    p.ISR_PorCiento = 5;
                }
                p.Precio_Original = producto.Detalle.Precio_General;
                p.Unidad_Medida = producto.Unidad_de_Medida;
                p.Descripcion = producto.Descripcion;
                p.Cantidad = Convert.ToInt32(producto.Detalle.Cantidad);
                p.Precio_Venta = producto.Detalle.Precio_General;
                p.Precio_Compra = Convert.ToDecimal(producto.Detalle.Precio_Compra);
                p.Precio_Mayoreo = p.Precio_Compra;
                p.Existencia = Convert.ToInt32(producto.Detalle.Cantidad);
                p.Id_Producto = producto.Producto_Id;
                p.IdDatosFiscales = producto.IdDatosFiscales;
                p.IVA = 0;
                p.IEPS = producto.Detalle.IEPS;
                p.Cantidad = 1;
                actualizarPrecios(ref p);
                lista.Add(p);
                gridDetalleVenta.DataSource = lista;
            }
            gridViewDetalleVenta.Focus();
            if (index > -1)
            {
                seleccionarRow(gridViewDetalleVenta, index);
            }
            else
            {
                seleccionarRow(gridViewDetalleVenta, lista.Count - 1);
            }
        }

        private void actualizarPrecios(ref VentaDetalle p)
        {
            if (tipo == TIPO_MOVIMIENTO.VENTA)
            {
                p.ISR_PorCiento = 0;
            }
            else
            {
                p.ISR_PorCiento = 5;
            }
            if (tipo == TIPO_MOVIMIENTO.VENTA)
            {
                if (p.ISR_PorCiento > 0)
                {
                    double descuento = p.Precio_Venta * Convert.ToDouble((p.ISR_PorCiento / 100));//p.Precio_Original * Convert.ToDouble((p.ISR_PorCiento / 100));
                    //p.Precio_Venta = p.Precio_Venta - descuento;
                    p.Descuento_ISR = descuento;
                }
                else
                {
                    p.Descuento_ISR = 0;
                }
                p.Importe = (decimal)(p.Precio_Venta * p.Cantidad);
            }
            else
            {
                decimal preciodescento = p.Precio_Compra;
                if (p.ISR_PorCiento > 0)
                {
                    double descuento = Convert.ToDouble(p.Precio_Compra) * Convert.ToDouble((p.ISR_PorCiento / 100));//p.Precio_Original * Convert.ToDouble((p.ISR_PorCiento / 100));
                    preciodescento = p.Precio_Compra - Convert.ToDecimal(descuento);
                    p.Descuento_ISR = descuento;
                }
                else
                {
                    p.Descuento_ISR = 0;
                }
                p.Importe = (decimal)(preciodescento * Convert.ToDecimal(p.Cantidad));
                p.ImporteReal = p.Precio_Compra * Convert.ToDecimal(p.Cantidad);
            }
        }

        private void actualizarPreciosConPromocion(ref VentaDetalle p)
        {
            if (p.ISR_PorCiento > 0)
            {
                double descuento = p.Precio_Original * Convert.ToDouble((p.ISR_PorCiento / 100));
                p.Precio_Venta = p.Precio_Original - descuento;
                p.Descuento_ISR = descuento;
            }
            else
            {
                p.Precio_Venta = p.Precio_Original;
                p.Descuento_ISR = 0;
            }
            p.Importe = (decimal)(p.Precio_Venta * p.Cantidad);
        }
        private void btnCliente_Click(object sender, EventArgs e)
        {
            if (tipo == TIPO_MOVIMIENTO.VENTA)
            {
                Cliente cliente = new Cliente();
                using (FrmBusqueda busqueda = new FrmBusqueda(cliente.Listado())
                {
                    Width = 1000,
                    Text = "Clientes",
                    AjustarColumnas = true,
                    ColumnasOcultar = new List<string> { "CampoId", "CampoBusqueda", "IdCliente", "CuentaContable", "FechaAlta", "Localidad", "Ciudad", "Calle", "NumInt", "NumExt", "Colonia", "CodigoPostal", "Estado", "Pais", "Telefono1", "Telefono2", "Telefono3", "Email1", "Email2", "Email3", "Comentario", "Activo", "DiasCredito", "Saldo", "Status", "ClienteStatus", "MontoCredito" },
                    ColumnasNoMoneda = new List<string> { "Tasa" }
                })
                    if (busqueda.ShowDialog() == DialogResult.OK)
                    {
                        if (busqueda.FilaDatos != null && cliente.Cargar((DataRowView)busqueda.FilaDatos))
                        {
                            _clienteId = cliente.Cliente_Id;
                            txtCliente.Text = cliente.Nombre + " " + cliente.ApellidoPaterno + " " + cliente.ApellidoMaterno;
                            imgBoxControl.Items.Add(txtCliente.Text.Trim() + "_" + DateTime.Now.Ticks.ToString(), 0);
                        }
                    }
            }
            else
            {
                Provedor proveedor = new Provedor();
                using (FrmBusqueda busqueda = new FrmBusqueda(proveedor.Listado())
                {
                    Width = 1000,
                    Text = "Proveedores",
                    AjustarColumnas = true,
                    ColumnasOcultar = new List<string> { "CampoId", "CampoBusqueda", "IdProveedor", "CuentaContable", "FechaAlta", "Localidad", "Ciudad", "Calle", "NumInt", "NumExt", "Colonia", "CodigoPostal", "Estado", "Pais", "Telefono1", "Telefono2", "Telefono3", "Email1", "Email2", "Email3", "Comentario", "Activo", "DiasCredito", "Saldo", "Status", "ClienteStatus", "MontoCredito", "Domicilio" },
                    ColumnasNoMoneda = new List<string> { "Tasa" }
                })
                    if (busqueda.ShowDialog() == DialogResult.OK)
                    {
                        if (busqueda.FilaDatos != null && proveedor.Cargar((DataRowView)busqueda.FilaDatos))
                        {
                            _clienteId = proveedor.Provedor_Id;
                            txtCliente.Text = proveedor.Nombre;
                            imgBoxControl.Items.Add(txtCliente.Text.Trim() + "_" + DateTime.Now.Ticks.ToString(), 0);
                        }
                    }
            }
        }

        public string getTotalVenta()
        {
            return txtTotalVenta.Text;
        }
        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (gridViewDetalleVenta.RowCount == 0)
            {
                if (cboTipo.EditValue == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "No se ha agregado ningún producto para vender o comprar.", this.ProductName, MessageBoxButtons.OK);
                }
                else
                {
                    if (Convert.ToInt32(cboTipo.EditValue) == 0)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "No se ha agregado ningún producto para comprar.", this.ProductName, MessageBoxButtons.OK);
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "No se ha agregado ningún producto para vender.", this.ProductName, MessageBoxButtons.OK);
                    }
                }
                return;
            }
            else
            {
                if (txtCliente.Text.Trim().Length == 0)
                {
                    if (tipo == TIPO_MOVIMIENTO.COMPRA)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "No se puede pagar la compra porque no se ha agregado un proveedor.\r\nFavor de asignar un proveedor a la compra ", this.ProductName, MessageBoxButtons.OK);
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "No se puede cobrar la venta porque nose ha agregado un cliente.\r\nFavor de asignar un cliente a la venta.", this.ProductName, MessageBoxButtons.OK);
                    }
                    return;
                }
                if (Global.StringToDouble(txtTotalVenta.Text) > 0)
                {
                    FrmFormaPago formaPago = new FrmFormaPago(this);
                    formaPago.ShowDialog();
                    return;
                }
                else
                {
                    if (tipo == TIPO_MOVIMIENTO.COMPRA)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "No se puede pagar la compra porque el total a pagar es 0.\r\nFavor de asignar el precio de compra al o los productos a comprar.", this.ProductName, MessageBoxButtons.OK);
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "No se puede cobrar la venta porque el total a cobrar es 0.\r\nFavor de asignar el precio de venta al o los productos a vender.", this.ProductName, MessageBoxButtons.OK);
                    }
                }
            }
        }

        public long obtenerClienteId()
        {
            return _clienteId;
        }
        public long obtenerEmpleadoId()
        {
            return FormRecyclame._Empleado.Id;// Convert.ToInt64(cmbEmpleado.EditValue ?? 0);
        }
        public string obtenerNombreEmpleado()
        {
            return txtEmpleado.Text;
            //if (cmbEmpleado.EditValue != null)
            //{
            //    return cmbEmpleado.Text;
            //}
            //else
            //{
            //    return string.Empty;
            //}
        }
        public void agregarDatosVenta(ref Venta v)
        {
            v.Id_Empleado = FormRecyclame._Empleado.Id;//Convert.ToInt64(cmbEmpleado.EditValue ?? 0);
            v.Id_Cliente = _clienteId;
            v.Id_Sucursal = 0;
            v.Fecha_Venta = Convert.ToDateTime(lblFecha.Text);
            v.Hora_Venta = v.Fecha_Venta;
            v.Id_Cotizacion = -1;
            v.TIPO = tipo;
            v.IVA = 0;
            v.IEPS = 0;
            v.Fecha_Tipo_Cambio = Global.MinDate;
            v.Id_Tipo_Cambio = -1;
            v.Comentario = "";
            v.Id_Factura = -1;
            v.Id_Metodo_pago = -1;
            v.Referencia = -1;
            if (tipo == TIPO_MOVIMIENTO.VENTA)
            {
                v.Id_Tipo_Venta = Convert.ToInt64(Venta.TIPO_VENTA.CONTADO);
                v.ISR_PorCiento = 0;
            }
            else
            {
                v.ISR_PorCiento = 5;
                v.Id_Tipo_Venta = Convert.ToInt64(Venta.TIPO_VENTA.COMPRA);
            }
            v.Debe = -1;
            v.Id_Promocion = -1;
        }
        public void Limpiar()
        {
            ultimoclienteSeleccionado = string.Empty;
            ultimocliente = string.Empty;
            if (imgBoxControl.SelectedValue != null)
            {
                if (dicVentasPendientes.ContainsKey(imgBoxControl.SelectedValue.ToString()))
                {
                    dicVentasPendientes.Remove(imgBoxControl.SelectedValue.ToString());
                }
            }
            _clienteId = 0;
            txtProducto.Text = "";
            txtTotalVenta.Text = "0.00";
            txtCliente.Text = string.Empty;
            gridDetalleVenta.DataSource = null;
            mostrarOculatrcolumnas(TIPO_MOVIMIENTO.COMPRA);
            lblFolio.Text = Venta.FolioSiguiente() + "";
            txtCliente.Focus();
            int index = imgBoxControl.SelectedIndex;
            if (index >= 0)
            {
                imgBoxControl.Items.RemoveAt(imgBoxControl.SelectedIndex);
            }
        }

        public void LimpiarVenta()
        {

            txtProducto.Text = "";
            txtTotalVenta.Text = "0.00";
            gridDetalleVenta.DataSource = null;
            txtProducto.Focus();
        }
        private void btnApartado_Click(object sender, EventArgs e)
        {
            //if (_clienteId > 0)
            //{
            //    if (Global.StringToDouble(getTotalVenta()) <= 0)
            //    {
            //        DevExpress.XtraEditors.XtraMessageBox.Show(this, "El total en $ de los productos a demostrar no puede ser menor o igual a 0.\r\nFavor de asignar un precio de venta a cada uno de los productos.", this.ProductName, MessageBoxButtons.OK);
            //    }                
            //}
            //else
            //{
            //    DevExpress.XtraEditors.XtraMessageBox.Show(this, "No haz seleccionado el cliente al cual se le hará la demostración.\r\nFavor de seleccionar un cliente para la demostración", this.ProductName, MessageBoxButtons.OK);
            //    btnCliente.Focus();
            //}
        }

        private void KeyEvent(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    btnEliminarProducto_Click(null, null);
                    break;
                case Keys.F3:
                    btnCantidad_Click(null, null);
                    break;
                case Keys.F10:
                    btnCobrar_Click(null, null);
                    break;
            }
        }

        private void txtPagoEfectivo_TextChanged(object sender, EventArgs e)
        {
            if (_actualizarTotales)
            {
                _actualizarTotales = false;

                RefrescarMontos();

                _actualizarTotales = true;
            }
        }

        private void AgregarListaVentaGrid(Productos producto)
        {
            BindingList<VentaDetalle> lista = (BindingList<VentaDetalle>)gridDetalleVenta.DataSource;
            VentaDetalle p = null;
            if (gridDetalleVenta.DataSource != null)
            {
                lista = new BindingList<VentaDetalle>();
                p = new VentaDetalle();
                p.Precio_Original = producto.Detalle.Precio_General;
                p.Descripcion = producto.Descripcion;
                p.Cantidad = Convert.ToInt32(producto.Detalle.Cantidad);
                if (tipo == TIPO_MOVIMIENTO.VENTA)
                {
                    p.Precio_Venta = producto.Detalle.Precio_General;
                }
                else
                {
                    p.Precio_Compra = Convert.ToDecimal(producto.Detalle.Precio_Compra);
                }
                p.Existencia = Convert.ToInt32(producto.Detalle.Cantidad);
                p.Id_Producto = producto.Producto_Id;
                p.IdDatosFiscales = producto.IdDatosFiscales;
                p.IVA = 0;
                p.IEPS = producto.Detalle.IEPS;
                p.Cantidad = 1;
                lista.Add(p);
                //gridListaVenta.DataSource = lista;
            }
        }

        private void RefrescarCantidades3(int index, string colum, ref bool redo)
        {
            if (gridViewDetalleVenta.FocusedRowHandle != index)
            {
                gridViewDetalleVenta.SelectRow(index);
                gridViewDetalleVenta.FocusedRowHandle = index;
            }
            object r = gridViewDetalleVenta.GetRow(gridViewDetalleVenta.FocusedRowHandle);
            VentaDetalle v = (VentaDetalle)r;
            if (string.Compare(colum, "colCantidad", true) == 0 || string.Compare(colum, "colTara", true) == 0)
            {
                if (tipo == TIPO_MOVIMIENTO.VENTA)
                {
                    if (((VentaDetalle)r).Cantidad <= ((VentaDetalle)r).Existencia)
                    {
                        ((VentaDetalle)r).Importe = Convert.ToDecimal(((VentaDetalle)r).Precio_Venta * ((VentaDetalle)r).Cantidad);
                        ((VentaDetalle)r).Descuento_ISR = ((VentaDetalle)r).Descuento_ISR * ((VentaDetalle)r).Cantidad;
                        ((VentaDetalle)r).UltimaCantidad = ((VentaDetalle)r).Cantidad;
                        RefrescarMontos();
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("No puedes vender mas articulos de los que tienes dado de alta en el Sistema.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ((VentaDetalle)r).Cantidad = ((VentaDetalle)r).UltimaCantidad;
                    }
                }
                else
                {
                    if (((VentaDetalle)r).Tara > 0)
                    {
                        if (((VentaDetalle)r).Cantidad > ((VentaDetalle)r).Tara)
                        {
                            ((VentaDetalle)r).Cantidad -= ((VentaDetalle)r).Tara;
                            ((VentaDetalle)r).UltimaTara = ((VentaDetalle)r).Tara;
                            ((VentaDetalle)r).UltimaCantidad = ((VentaDetalle)r).Cantidad;
                        }
                        else
                        {
                            redo = true;
                            if (string.Compare(colum, "colCantidad", true) == 0)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("La cantidad de producto a comprar no puede ser menor o igual al peso de la tara.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ((VentaDetalle)r).Cantidad = ((VentaDetalle)r).UltimaCantidad;
                            }
                            else
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("El peso de la tara no puede ser mayor o igual a la cantidad de producto a comprar.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ((VentaDetalle)r).Tara = ((VentaDetalle)r).UltimaTara;
                            }
                            return;
                        }
                    }
                    ((VentaDetalle)r).ImporteReal = ((VentaDetalle)r).Precio_Compra * Convert.ToDecimal(((VentaDetalle)r).Cantidad);
                    double descuento = Convert.ToDouble(((VentaDetalle)r).Precio_Compra) * Convert.ToDouble((((VentaDetalle)r).ISR_PorCiento / 100));
                    decimal preciodescuento = ((VentaDetalle)r).Precio_Compra - Convert.ToDecimal(descuento);
                    ((VentaDetalle)r).Importe = preciodescuento * Convert.ToDecimal(((VentaDetalle)r).Cantidad);
                    ((VentaDetalle)r).Descuento_ISR = descuento * ((VentaDetalle)r).Cantidad;
                    RefrescarMontos();
                }

            }
            else if (string.Compare(colum, "colPrecio_Venta", true) == 0)
            {
                ((VentaDetalle)r).Importe = Convert.ToDecimal(((VentaDetalle)r).Precio_Venta * ((VentaDetalle)r).Cantidad);
                RefrescarMontos();
            }
            else if (string.Compare(colum, "colPrecio_Compra", true) == 0)
            {
                ((VentaDetalle)r).ImporteReal = ((VentaDetalle)r).Precio_Compra * Convert.ToDecimal(((VentaDetalle)r).Cantidad);
                double descuento = Convert.ToDouble(((VentaDetalle)r).Precio_Compra) * Convert.ToDouble((((VentaDetalle)r).ISR_PorCiento / 100));
                decimal preciodescuento = ((VentaDetalle)r).Precio_Compra - Convert.ToDecimal(descuento);
                ((VentaDetalle)r).Importe = preciodescuento * Convert.ToDecimal(((VentaDetalle)r).Cantidad);
                ((VentaDetalle)r).Descuento_ISR = descuento * ((VentaDetalle)r).Cantidad;
                RefrescarMontos();
            }
            gridViewDetalleVenta.RefreshRow(gridViewDetalleVenta.FocusedRowHandle);

        }

        private void gridViewDetalleVenta_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bool redo = false;
            column = string.Empty;
            RefrescarCantidades3(e.RowHandle, e.Column.Name, ref redo);
            if (redo)
            {
                if (e.Column.View.IsEditing)
                {
                    e.Column.View.HideEditor();
                }
                e.Column.View.CancelUpdateCurrentRow();
                column = e.Column.FieldName;
                //if (string.Compare(e.Column.FieldName, "cantidad", true) == 0)
                //{                    
                //    gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[4];                    
                //}
                //else if (string.Compare(e.Column.FieldName, "tara", true) == 0)
                //{
                //    gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[6];                    
                //}
                tColumn.Start();
            }
        }

        private void gridViewDetalleVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnEliminarProducto_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (gridViewDetalleVenta.FocusedRowHandle >= 0)
                {
                    GridColumn column = gridViewDetalleVenta.FocusedColumn;
                    if (column != null)
                    {
                        if (tipo == TIPO_MOVIMIENTO.VENTA)
                        {
                            if (string.Compare(column.FieldName, "cantidad", true) != 0 &&
                                string.Compare(column.FieldName, "precio_venta", true) != 0)
                            {
                                gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[4];
                                gridViewDetalleVenta.ShowEditor();
                            }
                            else if (string.Compare(column.FieldName, "cantidad", true) == 0)
                            {
                                gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[7];
                                gridViewDetalleVenta.ShowEditor();
                            }
                            else if (string.Compare(column.FieldName, "precio_venta", true) == 0)
                            {
                                gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[3];
                                txtProducto.Text = string.Empty;
                                txtProducto.Focus();
                            }
                        }
                        else
                        {
                            if (string.Compare(column.FieldName, "cantidad", true) != 0 &&
                                string.Compare(column.FieldName, "tara", true) != 0 &&
                                string.Compare(column.FieldName, "precio_compra", true) != 0)
                            {
                                gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[4];
                                gridViewDetalleVenta.ShowEditor();
                            }
                            else if (string.Compare(column.FieldName, "cantidad", true) == 0)
                            {
                                gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[6];
                                gridViewDetalleVenta.ShowEditor();
                            }
                            else if (string.Compare(column.FieldName, "tara", true) == 0)
                            {
                                gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[17];
                                gridViewDetalleVenta.ShowEditor();
                            }
                            else if (string.Compare(column.FieldName, "precio_compra", true) == 0)
                            {
                                gridViewDetalleVenta.FocusedColumn = gridViewDetalleVenta.Columns[3];
                                txtProducto.Text = string.Empty;
                                txtProducto.Focus();
                            }
                        }
                    }
                }
            }
        }
        private void imgBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.ImageListBoxControl)sender).SelectedItem != null)
            {
                if (ultimoclienteSeleccionado.Length > 0)
                {
                    agregarVentaImaControl(ultimoclienteSeleccionado);
                }
                ImageListBoxItem item = (ImageListBoxItem)((DevExpress.XtraEditors.ImageListBoxControl)sender).SelectedItem;
                ultimoclienteSeleccionado = item.Value.ToString();
                ultimocliente = ultimoclienteSeleccionado;
                gridDetalleVenta.DataSource = null;
                if (dicVentasPendientes.ContainsKey(item.Value.ToString()))
                {
                    cargarVenta(dicVentasPendientes[item.Value.ToString()]);
                }
                else
                {
                    agregarVentaImaControl(ultimoclienteSeleccionado);
                    LimpiarVenta();
                }
            }
        }

        private void imgBoxControl_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            ImageListBoxItem item = imgBoxControl.Items[e.Index];
            Image image = (item.Images as ImageCollection).Images[item.ImageIndex];
            int indent = 1;
            Rectangle bounds = e.Bounds;
            SizeF textSize = e.Appearance.CalcTextSize(e.Cache, e.Item.ToString(), bounds.Width);
            bounds.Width = Math.Max(textSize.ToSize().Width, image.Width);
            bounds.Inflate(5, 0);
            e.Appearance.FillRectangle(e.Cache, bounds);
            bounds.Inflate(-5, 0);
            bounds.Height -= indent * 2;
            bounds.Y += indent;
            bounds.X += 15;
            bounds.Height -= textSize.ToSize().Height;
            e.Graphics.DrawImageUnscaled(image, bounds);
            bounds.Y = bounds.Bottom;
            bounds.Height = textSize.ToSize().Height;
            e.Appearance.DrawString(e.Cache, e.Item.ToString(), bounds);
            e.Handled = true;
        }

        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCliente.Text.Trim().Length > 0)
                {
                    imgBoxControl.Items.Add(txtCliente.Text.Trim() + "_" + DateTime.Now.Ticks.ToString(), 0);
                    if (imgBoxControl.SelectedItem != null)
                    {
                        imgBoxControl.SelectedIndex = imgBoxControl.Items.Count - 1;
                    }
                }
            }
        }

        private void cargarVenta(Venta v)
        {
            txtTotalVenta.Text = Global.DoubleToString(Convert.ToDouble(v.Total));
            cboTipo.EditValue = Convert.ToDecimal(Convert.ToInt32(v.TIPO));
            tipo = v.TIPO;
            _clienteId = v.Id_Cliente;
            txtCliente.Text = v.cliente;
            txtEmpleado.Text = v.Empleado;
            //cmbEmpleado.EditValue = Convert.ToDecimal(v.Id_Empleado);
            gridDetalleVenta.DataSource = null;
            if (v.Detalles != null && v.Detalles.Count > 0)
            {
                BindingList<VentaDetalle> lst = new BindingList<VentaDetalle>();
                foreach (VentaDetalle d in v.Detalles)
                {
                    lst.Add(d);
                }
                gridDetalleVenta.DataSource = lst;
            }
            mostrarOculatrcolumnas(v.TIPO);
        }
        private Venta obtenerVentacontroles(string cliente)
        {
            Venta v = new Venta();
            v.TIPO = tipo;
            v.Total = Convert.ToDecimal(Global.StringToDouble(txtTotalVenta.Text));
            v.Su_Cambio = 0;
            v.Id_Cliente = _clienteId;
            v.cliente = cliente.Substring(0, cliente.IndexOf("_"));
            v.Fecha_Venta = Convert.ToDateTime(lblFecha.Text);
            v.Id_Empleado = FormRecyclame._Empleado.Id;
            v.Empleado = txtEmpleado.Text;//cmbEmpleado.Text != null ? cmbEmpleado.Text : string.Empty;
            int length = gridViewDetalleVenta.RowCount;
            v.Detalles.Clear();
            for (int i = 0; i < length; i++)
            {
                v.Detalles.Add((VentaDetalle)gridViewDetalleVenta.GetRow(i));
            }
            return v;
        }

        private void obtenerVentacontroles(ref Venta v)
        {
            v.Total = Convert.ToDecimal(Global.StringToDouble(txtTotalVenta.Text));
            v.Su_Cambio = 0;
            v.TIPO = tipo;
            v.Fecha_Venta = Convert.ToDateTime(lblFecha.Text);
            v.Id_Empleado = FormRecyclame._Empleado.Id;
            v.Empleado = txtEmpleado.Text;//cmbEmpleado.Text != null ? cmbEmpleado.Text : string.Empty;
            int length = gridViewDetalleVenta.RowCount;
            v.Detalles.Clear();
            for (int i = 0; i < length; i++)
            {
                v.Detalles.Add((VentaDetalle)gridViewDetalleVenta.GetRow(i));
            }
        }
        private void agregarVentaImaControl(string header)
        {
            if (!dicVentasPendientes.ContainsKey(header))
            {
                dicVentasPendientes.Add(header, obtenerVentacontroles(header));

            }
            else
            {
                Venta v = dicVentasPendientes[header];
                obtenerVentacontroles(ref v);
                dicVentasPendientes[header] = v;
            }
        }

        private void cboTipo_EditValueChanged(object sender, EventArgs e)
        {
            tipo = (TIPO_MOVIMIENTO)Convert.ToInt32(cboTipo.EditValue);
            mostrarOculatrcolumnas(tipo);
            actualizarGridTipo();
            RefrescarMontos();
        }

        private void actualizarGridTipo()
        {
            int length = gridViewDetalleVenta.RowCount;
            for (int i = 0; i < length; i++)
            {
                VentaDetalle p = (VentaDetalle)gridViewDetalleVenta.GetRow(i);
                actualizarPrecios(ref p);
                gridViewDetalleVenta.RefreshRow(i);
            }
        }
        private void mostrarOculatrcolumnas(TIPO_MOVIMIENTO t)
        {
            List<string> lstEditar = new List<string>() { "Cantidad" };
            List<string> lstOcultar = new List<string>() { "Id_Venta_Detalle", "Id_Venta", "Id_Producto", "CampoId", "Quien_Surte", "Id_Sucursal", "Surtido", "IEPS", "IVA", "CampoBusqueda", "TipoClase", "Precio_Mayoreo", "Precio_Original", "IEPSimporte", "IVAimporte", "IdDatosFiscales", "IdVentas", "UltimaCantidad", "UltimaTara", "ImporteReal" };
            if (t == TIPO_MOVIMIENTO.VENTA)
            {
                if (gridViewDetalleVenta.Columns.Count > 0)
                {
                    gridViewDetalleVenta.Columns["Precio_Venta"].Visible = true;
                    gridViewDetalleVenta.Columns["Precio_Venta"].OptionsColumn.ReadOnly = false;
                    gridViewDetalleVenta.Columns["Precio_Venta"].OptionsColumn.AllowEdit = true;
                }
                lstEditar.Add("Precio_Venta");
                lstOcultar.Add("Precio_Compra");
                lstOcultar.Add("Tara");
                //lstOcultar.Add("ISR_PorCiento");
                //lstOcultar.Add("Descuento_ISR");
                //btnCobrar.BackgroundImage = (Bitmap)RecyclameV2.Properties.Resources.cobrar;
                metroLabel5.Text = "[F10] Cobrar";
            }
            else
            {
                if (gridViewDetalleVenta.Columns.Count > 0)
                {
                    gridViewDetalleVenta.Columns["Precio_Compra"].Visible = true;
                    gridViewDetalleVenta.Columns["Precio_Compra"].OptionsColumn.ReadOnly = false;
                    gridViewDetalleVenta.Columns["Precio_Compra"].OptionsColumn.AllowEdit = true;
                    gridViewDetalleVenta.Columns["Tara"].Visible = true;
                    gridViewDetalleVenta.Columns["Tara"].OptionsColumn.ReadOnly = false;
                    gridViewDetalleVenta.Columns["Tara"].OptionsColumn.AllowEdit = true;
                    //gridViewDetalleVenta.Columns["ISR_PorCiento"].Visible = true;
                    //gridViewDetalleVenta.Columns["Descuento_ISR"].Visible = true;                    
                }
                lstEditar.Add("Tara");
                lstEditar.Add("Precio_Compra");
                lstOcultar.Add("Precio_Venta");
                //btnCobrar.BackgroundImage = (Bitmap)RecyclameV2.Properties.Resources.pagar;
                metroLabel5.Text = "[F10] Pagar";
            }

            Herramientas.GridViewEditarColumnas(gridViewDetalleVenta, true, true, false, lstOcultar, lstEditar, new List<string>() { "Existencia", "Cantidad", "ISR_PorCiento", "Tara" });
            RefrescarMontos();
        }

        private void txtCliente_MouseEnter(object sender, EventArgs e)
        {
            txtCliente.SelectAll();
        }
    }
}