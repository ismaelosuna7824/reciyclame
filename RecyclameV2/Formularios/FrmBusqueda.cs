using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using MetroFramework.Forms;
using RecyclameV2.Clases;

namespace RecyclameV2.Formularios
{
    public partial class FrmBusqueda : MetroForm
    {
        private object listaBusqueda;
        public List<string> ColumnasOcultar { get; set; }
        public List<string> ColumnasNoMoneda { get; set; }
        public bool AjustarColumnas { get; set; }
        public bool AutosizeColumnas { get; set; }
        public bool BestFitColumns { get; set; }

        public FrmBusqueda(object lista)
        {
            InitializeComponent();
            listaBusqueda = lista;
            AjustarColumnas = true;
            AutosizeColumnas = false;
        }

        public FrmBusqueda(object lista, bool value)
        {
            InitializeComponent();
            listaBusqueda = lista;
            AjustarColumnas = true;
            AutosizeColumnas = false;
            BestFitColumns = value;
        }

        public object FilaDatos
        {
            get
            {
                if (gridViewBusqueda.SelectedRowsCount > 0)
                {
                    return gridViewBusqueda.GetRow(gridViewBusqueda.GetSelectedRows()[0]);
                }
                else
                    return null;
            }
        }

        private void gridBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (gridViewBusqueda.SelectedRowsCount > 0)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
            }
        }

        private void gridBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void FrmBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void FrmBusqueda_Load(object sender, EventArgs e)
        {
            gridViewBusqueda.OptionsView.ColumnAutoWidth = AutosizeColumnas;
            Herramientas.LlenarGrid(gridBusqueda, listaBusqueda);
            if (BestFitColumns)
            {
                gridViewBusqueda.BestFitColumns();
            }
            Herramientas.GridViewEditarColumnas(gridViewBusqueda, true, AjustarColumnas, false, ColumnasOcultar, null, ColumnasNoMoneda);
            gridBusqueda.RefreshDataSource();
            gridViewBusqueda.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            for (int i = 0; i < gridViewBusqueda.Columns.Count; i++)
            {
                if (gridViewBusqueda.Columns[i].Visible)
                {
                    gridViewBusqueda.FocusedColumn = gridViewBusqueda.Columns[i];
                    break;
                }
            }
            gridViewBusqueda.ShowEditor();
            var textedit = ((TextEdit)gridViewBusqueda.ActiveEditor);
            if (textedit != null)
            {
                textedit.Text = "m";
                textedit.Text = "";
            }
        }

        private void gridViewBusqueda_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;

            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DataRow r = view.GetDataRow(1);
            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
