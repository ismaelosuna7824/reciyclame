namespace RecyclameV2
{
    partial class FrmFormaPago
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
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.txtCambio = new MetroFramework.Controls.MetroTextBox();
            this.txtTotalPago = new MetroFramework.Controls.MetroTextBox();
            this.lblRestan = new MetroFramework.Controls.MetroLabel();
            this.lblCambio = new MetroFramework.Controls.MetroLabel();
            this.lblSuPago = new MetroFramework.Controls.MetroLabel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblReferencia = new MetroFramework.Controls.MetroLabel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnCredito = new System.Windows.Forms.Button();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.btnEfectivo = new System.Windows.Forms.Button();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.txtRestante = new RecyclameV2.TextBoxNumerico();
            this.txtReferencia = new RecyclameV2.TextBoxNumerico();
            this.txtSuPago = new RecyclameV2.TextBoxNumerico();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabel5
            // 
            this.metroLabel5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel5.Location = new System.Drawing.Point(242, 308);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(102, 25);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Black;
            this.metroLabel5.TabIndex = 129;
            this.metroLabel5.Text = "[F10] Cobrar";
            // 
            // txtCambio
            // 
            this.txtCambio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCambio.Enabled = false;
            this.txtCambio.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtCambio.Location = new System.Drawing.Point(209, 149);
            this.txtCambio.MaxLength = 45;
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.ReadOnly = true;
            this.txtCambio.Size = new System.Drawing.Size(145, 29);
            this.txtCambio.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCambio.TabIndex = 125;
            this.txtCambio.Text = "0.00";
            this.txtCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalPago
            // 
            this.txtTotalPago.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTotalPago.Enabled = false;
            this.txtTotalPago.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtTotalPago.Location = new System.Drawing.Point(209, 33);
            this.txtTotalPago.MaxLength = 45;
            this.txtTotalPago.Name = "txtTotalPago";
            this.txtTotalPago.ReadOnly = true;
            this.txtTotalPago.Size = new System.Drawing.Size(145, 29);
            this.txtTotalPago.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTotalPago.TabIndex = 122;
            this.txtTotalPago.Text = "0.00";
            this.txtTotalPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRestan
            // 
            this.lblRestan.AutoSize = true;
            this.lblRestan.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblRestan.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblRestan.Location = new System.Drawing.Point(97, 114);
            this.lblRestan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRestan.Name = "lblRestan";
            this.lblRestan.Size = new System.Drawing.Size(75, 25);
            this.lblRestan.TabIndex = 47;
            this.lblRestan.Text = "Restan:";
            // 
            // lblCambio
            // 
            this.lblCambio.AutoSize = true;
            this.lblCambio.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblCambio.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblCambio.Location = new System.Drawing.Point(67, 153);
            this.lblCambio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(107, 25);
            this.lblCambio.TabIndex = 45;
            this.lblCambio.Text = "Su Cambio:";
            // 
            // lblSuPago
            // 
            this.lblSuPago.AutoSize = true;
            this.lblSuPago.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblSuPago.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblSuPago.Location = new System.Drawing.Point(88, 76);
            this.lblSuPago.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSuPago.Name = "lblSuPago";
            this.lblSuPago.Size = new System.Drawing.Size(86, 25);
            this.lblSuPago.TabIndex = 43;
            this.lblSuPago.Text = "Su Pago:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(252, 241);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(76, 64);
            this.btnBuscar.TabIndex = 128;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblReferencia
            // 
            this.lblReferencia.AutoSize = true;
            this.lblReferencia.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblReferencia.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblReferencia.Location = new System.Drawing.Point(67, 185);
            this.lblReferencia.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(108, 25);
            this.lblReferencia.TabIndex = 40;
            this.lblReferencia.Text = "Referencia:";
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel8.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel8.Location = new System.Drawing.Point(50, 35);
            this.metroLabel8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(129, 25);
            this.metroLabel8.TabIndex = 38;
            this.metroLabel8.Text = "Total a Pagar:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(350, 76);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(51, 19);
            this.labelControl4.TabIndex = 44;
            this.labelControl4.Text = "Crédito";
            // 
            // btnCredito
            // 
            this.btnCredito.BackColor = System.Drawing.Color.White;
            this.btnCredito.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCredito.FlatAppearance.BorderSize = 0;
            this.btnCredito.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnCredito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCredito.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCredito.ForeColor = System.Drawing.Color.White;
            this.btnCredito.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCredito.Location = new System.Drawing.Point(320, 7);
            this.btnCredito.Name = "btnCredito";
            this.btnCredito.Size = new System.Drawing.Size(100, 67);
            this.btnCredito.TabIndex = 43;
            this.btnCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCredito.UseVisualStyleBackColor = false;
            // 
            // labelControl24
            // 
            this.labelControl24.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl24.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl24.Location = new System.Drawing.Point(173, 76);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(55, 19);
            this.labelControl24.TabIndex = 34;
            this.labelControl24.Text = "Efectivo";
            // 
            // btnEfectivo
            // 
            this.btnEfectivo.BackColor = System.Drawing.Color.White;
            this.btnEfectivo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEfectivo.FlatAppearance.BorderSize = 0;
            this.btnEfectivo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnEfectivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEfectivo.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEfectivo.ForeColor = System.Drawing.Color.White;
            this.btnEfectivo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEfectivo.Location = new System.Drawing.Point(148, 7);
            this.btnEfectivo.Name = "btnEfectivo";
            this.btnEfectivo.Size = new System.Drawing.Size(100, 67);
            this.btnEfectivo.TabIndex = 33;
            this.btnEfectivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEfectivo.UseVisualStyleBackColor = false;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroLabel5);
            this.metroPanel1.Controls.Add(this.txtReferencia);
            this.metroPanel1.Controls.Add(this.txtRestante);
            this.metroPanel1.Controls.Add(this.txtSuPago);
            this.metroPanel1.Controls.Add(this.txtCambio);
            this.metroPanel1.Controls.Add(this.txtTotalPago);
            this.metroPanel1.Controls.Add(this.lblRestan);
            this.metroPanel1.Controls.Add(this.lblCambio);
            this.metroPanel1.Controls.Add(this.lblSuPago);
            this.metroPanel1.Controls.Add(this.btnBuscar);
            this.metroPanel1.Controls.Add(this.lblReferencia);
            this.metroPanel1.Controls.Add(this.metroLabel8);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(15, 160);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(559, 313);
            this.metroPanel1.TabIndex = 35;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.labelControl4);
            this.metroPanel2.Controls.Add(this.btnCredito);
            this.metroPanel2.Controls.Add(this.labelControl24);
            this.metroPanel2.Controls.Add(this.btnEfectivo);
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(20, 60);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(617, 100);
            this.metroPanel2.TabIndex = 36;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // txtRestante
            // 
            this.txtRestante.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRestante.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtRestante.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtRestante.Formato = RecyclameV2.TextBoxNumerico.FormatoNumerico.Decimal;
            this.txtRestante.Location = new System.Drawing.Point(180, 108);
            this.txtRestante.Name = "txtRestante";
            this.txtRestante.Numero = 0D;
            this.txtRestante.Size = new System.Drawing.Size(145, 23);
            this.txtRestante.TabIndex = 124;
            this.txtRestante.Text = "0.00";
            this.txtRestante.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtReferencia
            // 
            this.txtReferencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReferencia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtReferencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtReferencia.Formato = RecyclameV2.TextBoxNumerico.FormatoNumerico.Numerico;
            this.txtReferencia.Location = new System.Drawing.Point(180, 179);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Numero = 0D;
            this.txtReferencia.Size = new System.Drawing.Size(145, 23);
            this.txtReferencia.TabIndex = 126;
            this.txtReferencia.Text = "0";
            this.txtReferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSuPago
            // 
            this.txtSuPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSuPago.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSuPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSuPago.Formato = RecyclameV2.TextBoxNumerico.FormatoNumerico.Decimal;
            this.txtSuPago.Location = new System.Drawing.Point(180, 70);
            this.txtSuPago.Name = "txtSuPago";
            this.txtSuPago.Numero = 0D;
            this.txtSuPago.Size = new System.Drawing.Size(145, 23);
            this.txtSuPago.TabIndex = 123;
            this.txtSuPago.Text = "0.00";
            this.txtSuPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmFormaPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 520);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.metroPanel2);
            this.Name = "FrmFormaPago";
            this.Text = "Método de Pago";
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel2;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private System.Windows.Forms.Button btnEfectivo;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel lblCambio;
        private MetroFramework.Controls.MetroLabel lblSuPago;
        private System.Windows.Forms.Button btnBuscar;
        private MetroFramework.Controls.MetroLabel lblReferencia;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel lblRestan;
        private MetroFramework.Controls.MetroTextBox txtCambio;
        private MetroFramework.Controls.MetroTextBox txtTotalPago;
        private TextBoxNumerico txtRestante;
        private TextBoxNumerico txtSuPago;
        private TextBoxNumerico txtReferencia;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.Button btnCredito;
    }
}