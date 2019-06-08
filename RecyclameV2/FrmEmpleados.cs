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
    public partial class FrmEmpleados : MetroForm
    {
        public FrmEmpleados()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Empleado empleado = obtieneDatosEmpleado();
            empleado.Grabar();
            DevExpress.XtraEditors.XtraMessageBox.Show(this, "El empleado ha sido ingresado correctamente.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            lblAlta.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private Empleado obtieneDatosEmpleado()
        {
            Empleado empleado = new Empleado();
            empleado.Nombre = txtNombre.Text;
            empleado.ApellidoPaterno = txtApellidoP.Text;
            empleado.ApellidoMaterno = txtApellidoM.Text;
            empleado.FechaNacimiento = dtNacimiento.Value;
            empleado.RFC = txtRFC.Text;
            empleado.Curp = txtCurp.Text;
            empleado.NSS = txtSeguroSocial.Text;
            empleado.Telefono = txtTelefono.Text;
            empleado.Email = txtMail.Text;
            empleado.Calle = txtCalle.Text;
            empleado.Ciudad = txtCiudad.Text;
            empleado.Colonia = txtColonia.Text;
            empleado.NumExt = txtNumExt.Text;
            empleado.NumInt = txtNumInt.Text;
            empleado.CodigoPostal = txtCodigoPostal.Text;
            empleado.Pais = txtPais.Text;
            empleado.Estado = txtEstado.Text;
            empleado.Usuario = txtUsuario.Text;
            empleado.Password = txtContrasena.Text;
            empleado.FechaAlta = DateTime.Parse(lblAlta.Text);
            empleado.Activo = true;
            return empleado;
        }

        private void txtBuscarEmpleado_TextChanged(object sender, EventArgs e)
        {
            buscarEmpleados(txtBuscarEmpleado.Text);
        }

        private void buscarEmpleados(string search)
        {
            try
            {
                gridEmpleados.DataSource = null;
                List<SqlParameter> parametros = new List<SqlParameter>();
                if (txtBuscarEmpleado.Text.Length > 0)
                {
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Texto", Value = txtBuscarEmpleado.Text });
                }
                else
                {
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Texto", Value = string.Empty });
                }
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = -1 });
                gridEmpleados.DataSource = Global.CargarListaGrid(BaseDatos.ejecutarProcedimientoConsultaDataTable("Empleados_Consultar_Grid_sp", parametros), "empleado");
                gridView1.BestFitColumns();
                List<string> listColumnasOcultar = new List<string>() { "Id", "TipoClase", "CampoId", "CampoBusqueda", "Nombre", "FechaAlta", "FechaNacimiento", "Calle", "ApellidoPaterno", "ApellidoMaterno", "NumExt", "NumInt", "Estado", "Pais", "Colonia", "Ciudad", "Localidad", "CodigoPostal", "Activo", "IdHuella", "Telefono2", "Email2", "Curp", "Usuario", "Password" };

                Herramientas.GridViewEditarColumnas(gridView1, true, true, false, listColumnasOcultar, new List<string>(), new List<string>());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("BUSCAR EMPLEADOS EXCEPTION: " + e.ToString());
            }
        }

        private void btnEditarEmpleado_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                int rowHandle = gridView1.GetSelectedRows()[0];
                editarEmpleado(rowHandle);
            }
        }
        private bool editarEmpleado(int rowHandle)
        {
            Empleado empleado = (Empleado)gridView1.GetRow(rowHandle);
            if (empleado != null)
            {
                if (empleado.Activo)
                {
                   // cerrarLector();
                    frmEmpleado emp = new frmEmpleado(empleado);
                    if (emp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        empleado = emp.obtenerEmpleado();
                        empleado.Nombre_Completo = empleado.Nombre + " " + empleado.ApellidoPaterno + " " + empleado.ApellidoMaterno;
                        BindingList<object> lst = (BindingList<object>)gridEmpleados.DataSource;
                        int length = lst.Count;
                        Empleado e = null;
                        for (int i = 0; i < length; i++)
                        {
                            e = (Empleado)lst.ElementAt(i);
                            if (e.Id == empleado.Id)
                            {
                                lst.RemoveAt(i);
                                lst.Insert(i, empleado);
                                break;
                            }
                        }
                        gridEmpleados.DataSource = lst;
                        //gridView1.RefreshRow(rowHandle);
                        gridEmpleados.RefreshDataSource();
                        return true;
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "No se puede editar el empleado porque ha sido dado de baja.\r\nfavor de seleccionar un empleado que esté activo.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            int index = Global.RowIndexClicked(gridView1);
            if (index > -1)
            {
                editarEmpleado(index);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    int rowHandle = gridView1.GetSelectedRows()[0];
                    editarEmpleado(rowHandle);
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    int rowHandle = gridView1.GetSelectedRows()[0];
                    //eliminarEmpleado(rowHandle);
                }
            }
        }
    }
}
