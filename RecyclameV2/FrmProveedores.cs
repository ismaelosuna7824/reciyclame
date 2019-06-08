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
using System.Data.SqlClient;

namespace RecyclameV2
{
    public partial class FrmProveedores : MetroForm
    {
        public FrmProveedores()
        {
            InitializeComponent();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                        Provedor proveedor = obtieneDatosProveedor();

                        proveedor.Grabar();
                        DevExpress.XtraEditors.XtraMessageBox.Show(this, "El proveedor ha sido ingresado correctamente.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarProveedor();

                
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(this, string.Format("Error al Intentar guardar el proveedor. Detalle:{0}", ex.Message),
                    this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Provedor obtieneDatosProveedor() {

            Provedor proveedor = new Provedor();
            proveedor.Nombre = txtNombre.Text;
            if (txtApellidoP.Text.Length > 0)
            {
                if (proveedor.Nombre.Length > 0)
                {
                    proveedor.Nombre += " ";
                }
                proveedor.Nombre += txtApellidoP.Text;
            }
            if (txtApellidoM.Text.Length > 0)
            {
                if (proveedor.Nombre.Length > 0)
                {
                    proveedor.Nombre += " ";
                }
                proveedor.Nombre += txtApellidoM.Text;
            }

            proveedor.FechaAlta = Convert.ToDateTime(lblAlta.Text);
            proveedor.RFC = txtRFC.Text;
            proveedor.Razon_Social = txtRazonSocial.Text;
            proveedor.Cuenta_Contable = txtCuentaContable.Text;
            proveedor.Telefono = txtTelefono.Text;
            proveedor.Email = txtMail.Text;
            proveedor.Calle = txtCalle.Text;
            proveedor.Ciudad = txtCiudad.Text;
            proveedor.Colonia = txtColonia.Text;
            proveedor.NumExt = txtNumExt.Text;
            proveedor.NumInt = txtNumInt.Text;
            proveedor.Codigo_Postal = txtCodigoPostal.Text;
            proveedor.Pais = txtPais.Text;
            proveedor.Estado = txtEstado.Text;
            proveedor.Comentario = txtComentario.Text;
            proveedor.Activo = true;
            return proveedor;
        }

        private bool esProveedorValido(Provedor proveedor)
        {
            string strMensaje = string.Empty;
            bool bFocus = false;


            if (proveedor.RFC.Trim().Length == 0)
            {
                if (strMensaje != string.Empty) { strMensaje += Environment.NewLine; }

                strMensaje += "- El RFC del proveedor es requerido.";

                if (!bFocus)
                {
                    txtRFC.Focus();
                    bFocus = true;
                }
            }


            if (strMensaje != string.Empty)
            {
                strMensaje = "El proveedor no puede ser guardado debido a que: " + Environment.NewLine + Environment.NewLine + strMensaje;
                DevExpress.XtraEditors.XtraMessageBox.Show(this, strMensaje, Global.STR_NOMBRE_SISTEMA, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
            return true;
        }
        private void LimpiarProveedor()
        {
            txtNombre.Text = string.Empty;
            txtApellidoP.Text = string.Empty;
            txtApellidoM.Text = string.Empty;
            txtRFC.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtComentario.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtColonia.Text = string.Empty;
            txtNumExt.Text = string.Empty;
            txtNumInt.Text = string.Empty;
            txtCodigoPostal.Text = string.Empty;
            txtPais.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtCuentaContable.Text = string.Empty;
            lblAlta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtNombre.Focus();
        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            lblAlta.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void txtBuscarProveedor_TextChanged(object sender, EventArgs e)
        {
            buscarProveedores(txtBuscarProveedor.Text);
        }

        private void buscarProveedores(string search)
        {
            try
            {
                gridProveedores.DataSource = null;
                List<SqlParameter> parametros = new List<SqlParameter>();
                if (txtBuscarProveedor.Text.Length > 0)
                {
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Provedor_ID", Value = txtBuscarProveedor.Text });
                    parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = txtBuscarProveedor.Text });
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = txtBuscarProveedor.Text });
                }
                else
                {
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Provedor_ID", Value = 0 });
                    parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = null });
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = string.Empty });
                }
                gridProveedores.DataSource = Global.CargarListaGrid(BaseDatos.ejecutarProcedimientoConsultaDataTable("Proveedor_Consultar_sp", parametros), "proveedor");
                if (gridProveedores.DataSource != null)
               {
                    gridView1.BestFitColumns();
                    List<string> listColumnasOcultar = new List<string>() { "Provedor_Id", "TipoClase", "CampoId", "CampoBusqueda", "FechaAlta", "FechaNacimiento", "Calle", "ApellidoPaterno", "ApellidoMaterno", "NumExt", "NumInt", "Estado", "Pais", "Colonia", "Ciudad", "Localidad", "Codigo_Postal", "Activo", "Telefono2", "Telefono3", "Email2", "Email3", "Comentario", "Dias_de_Credito" };
                    Herramientas.GridViewEditarColumnas(gridView1, true, true, false, listColumnasOcultar, new List<string>(), new List<string>());
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("BUSCAR Proveedores EXCEPTION: " + e.ToString());
            }
        }
    }
}
