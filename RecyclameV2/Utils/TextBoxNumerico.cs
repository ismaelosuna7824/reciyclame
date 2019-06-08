using System;
using System.Windows.Forms;
using System.ComponentModel;
using RecyclameV2.Utils;
using RecyclameV2.Clases;

namespace RecyclameV2
{
    public partial class TextBoxNumerico : TextBox
    {
        private FormatoNumerico _eFormatoNumerico = FormatoNumerico.Numerico;

        public enum FormatoNumerico
        {
            Numerico,
            Decimal
        }

        public enum AliniacionTexto
        {
            Derecha,
        }

        public TextBoxNumerico()
        {
            this.Size = new System.Drawing.Size(this.Size.Width, 24);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            InitializeComponent();
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Formato = _eFormatoNumerico;
            this.TextAlign = HorizontalAlignment.Right;
            this.Numero = 0;

            base.ContextMenu = new ContextMenu(); // Eliminar el context menu
            //base.TextAlign   = HorizontalAlignment.Right;
            //base.TextAlign = HorizontalAlignment.Right;
        }

        #region Propiedades

        // Description. Descripcion que aparece cuando seleccionas el control.
        // Category. Indica el grupo al que pertenece si las propiedades se muestran por categorias.

        [Description("Formato en el que se va a mostrar el texto."), Category("Behavior")]
        public FormatoNumerico Formato
        {
            get { return _eFormatoNumerico; }
            set { _eFormatoNumerico = value; }
        }
        /*
        [Description("El texto solo se alinea hacia la derecha."), Category("Behavior")]
        new public AliniacionTexto TextAlign // Ya hay una propiedad que se llama TextAlign, de esta manera se deshabilita y solo queda hacia la derecha.
        {
            get { return AliniacionTexto.Derecha; }
        }
        */
        [Description("Numero que representa el texto."), Category("Behavior")]
        public double Numero
        {
            get { return ObtenerNumero(this.Text); }
            set { base.Text = ConvertirNumeroTexto(value); }
        }

        #endregion Propiedades

        #region override

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Evitar el Pegar por medio de Ctrl + V & Shift + Insert
            if (keyData == (Keys)Shortcut.CtrlV || keyData == (Keys)Shortcut.ShiftIns)
            {
                return true;
            }

            return false;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            /*
            Notas:
                - Suprimir no entra a esta funcion
            */

            char c = e.KeyChar;

            if (_eFormatoNumerico == FormatoNumerico.Numerico)
            {
                /*
                Permitir:
                    - Numeros digitos
                    - Barra de retroseso
                */

                if (!Char.IsNumber(c) && c != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else if (_eFormatoNumerico == FormatoNumerico.Decimal)
            {
                /*
                Permitir:
                    - Numeros digitos
                    - Barra de retroseso
                    - Puntos ( pero solo uno )
                */

                bool bPermitirPunto = true; // Permitir solo un punto

                int iIndicePunto = this.Text.IndexOf('.');
                if (iIndicePunto >= 0)
                {
                    bPermitirPunto = false;
                }

                if (!Char.IsNumber(c) && c != (char)Keys.Back && c != '.')
                {
                    e.Handled = true;
                }
                else if (c == '.' && !bPermitirPunto)
                {
                    e.Handled = true;
                }
            }

            base.OnKeyPress(e);
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                this.Numero = ObtenerNumero(value);
                base.Text = ConvertirNumeroTexto(this.Numero);
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.Text = ConvertirNumeroTexto(this.Numero);

            base.OnLeave(e);
        }

        //protected override void  OnKeyUp(KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        SendKeys.Send("{TAB}");
        //    }
        //}

        #endregion override

        private double ObtenerNumero(string strTexto)
        {
            return Global.StringToDouble(strTexto);
        }

        private string ConvertirNumeroTexto(double dNumero)
        {
            string strTexto = string.Empty;

            if (_eFormatoNumerico == FormatoNumerico.Numerico)
            {
                strTexto = string.Format("{0:#,0}", dNumero);
            }
            else if (_eFormatoNumerico == FormatoNumerico.Decimal)
            {
                strTexto = Global.DoubleToString(dNumero);
            }

            return strTexto;
        }
    }
}
