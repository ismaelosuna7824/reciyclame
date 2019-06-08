namespace RecyclameV2.Formularios
{
    partial class FrmBusqueda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridViewBusqueda = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridBusqueda = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBusqueda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBusqueda)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewBusqueda
            // 
            this.gridViewBusqueda.GridControl = this.gridBusqueda;
            this.gridViewBusqueda.Name = "gridViewBusqueda";
            this.gridViewBusqueda.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewBusqueda.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewBusqueda.OptionsView.ShowAutoFilterRow = true;
            this.gridViewBusqueda.OptionsView.ShowGroupPanel = false;
            this.gridViewBusqueda.DoubleClick += new System.EventHandler(this.gridViewBusqueda_DoubleClick);
            // 
            // gridBusqueda
            // 
            this.gridBusqueda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBusqueda.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridBusqueda.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridBusqueda.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridBusqueda.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridBusqueda.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridBusqueda.Location = new System.Drawing.Point(20, 60);
            this.gridBusqueda.MainView = this.gridViewBusqueda;
            this.gridBusqueda.Name = "gridBusqueda";
            this.gridBusqueda.Size = new System.Drawing.Size(812, 230);
            this.gridBusqueda.TabIndex = 1;
            this.gridBusqueda.UseEmbeddedNavigator = true;
            this.gridBusqueda.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBusqueda});
            this.gridBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridBusqueda_KeyPress);
            this.gridBusqueda.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridBusqueda_KeyUp);
            // 
            // FrmBusqueda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 310);
            this.Controls.Add(this.gridBusqueda);
            this.Name = "FrmBusqueda";
            this.Text = "FrmBusqueda";
            this.Load += new System.EventHandler(this.FrmBusqueda_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBusqueda_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBusqueda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBusqueda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBusqueda;
        private DevExpress.XtraGrid.GridControl gridBusqueda;
    }
}