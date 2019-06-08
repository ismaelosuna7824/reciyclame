using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace RecyclameV2
{
    public partial class FrmImpresora : MetroForm
    {
        public FrmImpresora()
        {
            InitializeComponent();
            LlenarImpresoras();
        }

        public List<string> LlenarImpresoras()
        {
            cmbImpresoras.EditValue = null;
            List<string> lstImpresoras = new List<string>();
            foreach (string strImpresoraInstalada in PrinterSettings.InstalledPrinters)
            {
                lstImpresoras.Add(strImpresoraInstalada);
            }
            cmbImpresoras.Properties.DataSource = lstImpresoras;
            cmbImpresoras.Properties.ForceInitialize();
            cmbImpresoras.Properties.PopulateColumns();
            cmbImpresoras.Refresh();
            cmbImpresoras.Properties.Columns[0].Caption = "Impresoras";
            return lstImpresoras;
        }

        public string ObtenerImpresora()
        {
            if (cmbImpresoras.EditValue != null)
            {
                return cmbImpresoras.Text;
            }

            return string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
