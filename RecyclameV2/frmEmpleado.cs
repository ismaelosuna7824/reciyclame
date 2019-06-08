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
    public partial class frmEmpleado : MetroForm
    {
        Empleado _empleado = null;
        public frmEmpleado(Empleado empleado)
        {
            InitializeComponent();
            _empleado = empleado;
            CargarDatosEmpleado(_empleado);

        }

        private void frmEmpleado_Load(object sender, EventArgs e)
        {

        }
        public Empleado obtenerEmpleado()
        {
            return _empleado;
        }
        private void CargarDatosEmpleado(Empleado empleado)
        {
            txtNombre.Text = empleado.Nombre;
            txtApellidoP.Text = empleado.ApellidoPaterno;
            txtApellidoM.Text = empleado.ApellidoMaterno;
            dtNacimiento.Value = empleado.FechaNacimiento;
            txtRFC.Text = empleado.RFC;
            txtCurp.Text = empleado.Curp;
            txtNSS.Text = empleado.NSS;
            txtTelefono.Text = empleado.Telefono;
            txtMail.Text = empleado.Email;
            txtCalle.Text = empleado.Calle;
           // txtLocalidad.Text = empleado.Localidad;
            txtCiudad.Text = empleado.Ciudad;
            txtColonia.Text = empleado.Colonia;
            txtNumExt.Text = empleado.NumExt;
            txtNumInt.Text = empleado.NumInt;
            txtCP.Text = empleado.CodigoPostal;
            txtPais.Text = empleado.Pais;
            txtEstado.Text = empleado.Estado;
            txtUsuario.Text = empleado.Usuario;
            txtPassword.Text = empleado.Password;
            //lblNumEmpleado.Text = empleado.Id.ToString("D8");
            empleado.Activo = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Empleado empleado = obtieneDatosEmpleado();
            empleado.Grabar();
            _empleado = empleado;
            DevExpress.XtraEditors.XtraMessageBox.Show(this, "El empleado ha sido actualizado correctamente", "Validador Huella", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();

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
            empleado.NSS = txtNSS.Text;
            empleado.Telefono = txtTelefono.Text;
            empleado.Email = txtMail.Text;
            empleado.Calle = txtCalle.Text;
           // empleado.Localidad = txtLocalidad.Text;
            empleado.Ciudad = txtCiudad.Text;
            empleado.Colonia = txtColonia.Text;
            empleado.NumExt = txtNumExt.Text;
            empleado.NumInt = txtNumInt.Text;
            empleado.CodigoPostal = txtCP.Text;
            empleado.Pais = txtPais.Text;
            empleado.Estado = txtEstado.Text;
            empleado.Usuario = txtUsuario.Text;
            empleado.Password = txtPassword.Text;
            empleado.Id = _empleado.Id;//Convert.ToInt64(lblNumEmpleado.Text);
            empleado.Activo = true;
            return empleado;
        }
    }
}
