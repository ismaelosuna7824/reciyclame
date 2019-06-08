using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RecyclameV2.Clases
{
    delegate void Function();
    static class Herramientas
    {
        internal static void LlenarGrid(DevExpress.XtraGrid.GridControl gridControl, object lista)
        {
            gridControl.DataSource = lista;
            gridControl.RefreshDataSource();
        }

        internal static void GridViewEditarColumnas(DevExpress.XtraGrid.Views.Grid.GridView view, bool bSoloLectura, bool bAjustarColumnas, bool bFechaHora, List<string> ColumnasOcultar, List<string> ColumnasEditar, List<string> ColumnasNoMoneda)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn columna in view.Columns)
            {
                if (columna.FieldName.EndsWith("_Id"))
                    columna.Visible = false;

                columna.Caption = columna.FieldName.Replace("_", " ").ToUpper();
                columna.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;

                if (bSoloLectura && (ColumnasEditar == null || !ColumnasEditar.Contains(columna.FieldName)))
                {
                    columna.OptionsColumn.ReadOnly = true;
                    columna.OptionsColumn.AllowEdit = false;
                }

                if (ColumnasOcultar != null && ColumnasOcultar.Contains(columna.FieldName))
                    columna.Visible = false;

                if (columna.ColumnType == typeof(DateTime))
                {
                    if (bFechaHora)
                    {
                        columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        columna.DisplayFormat.FormatString = "g";
                    }
                    else
                    {
                        columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        columna.DisplayFormat.FormatString = "d";
                    }
                }

                if ((columna.ColumnType == typeof(double) || columna.ColumnType == typeof(Decimal)) && (ColumnasNoMoneda == null || !ColumnasNoMoneda.Contains(columna.FieldName)))
                {
                    columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    if (string.Compare(columna.FieldName, "importe", true) == 0)
                    {
                        columna.DisplayFormat.FormatString = "c0";
                    }
                    else
                    {
                        columna.DisplayFormat.FormatString = "c2";
                    }
                }
            }
            if (bAjustarColumnas)
                view.BestFitColumns();
        }
        internal static void GridViewEditarColumnasFacturacion(DevExpress.XtraGrid.Views.Grid.GridView view, bool bSoloLectura, bool bAjustarColumnas, bool bFechaHora, List<string> ColumnasOcultar, List<string> ColumnasEditar, List<string> ColumnasNoMoneda)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn columna in view.Columns)
            {
                if (columna.FieldName.EndsWith("_Id"))
                    columna.Visible = false;

                columna.Caption = columna.FieldName.Replace("_", " ").ToUpper();
                columna.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;

                if (bSoloLectura && (ColumnasEditar == null || !ColumnasEditar.Contains(columna.FieldName)))
                {
                    columna.OptionsColumn.ReadOnly = true;
                    columna.OptionsColumn.AllowEdit = false;
                }

                if (ColumnasOcultar != null && ColumnasOcultar.Contains(columna.FieldName))
                    columna.Visible = false;

                if (columna.ColumnType == typeof(DateTime))
                {
                    if (bFechaHora)
                    {
                        columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        columna.DisplayFormat.FormatString = "g";
                    }
                    else
                    {
                        columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        columna.DisplayFormat.FormatString = "d";
                    }
                }

                if ((columna.ColumnType == typeof(double) || columna.ColumnType == typeof(Decimal)) && (ColumnasNoMoneda == null || !ColumnasNoMoneda.Contains(columna.FieldName)))
                {
                    columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    columna.DisplayFormat.FormatString = "c2";
                }
            }
            if (bAjustarColumnas)
                view.BestFitColumns();
        }

        internal static void GridEditarColumnas(DevExpress.XtraGrid.GridControl grid, bool bSoloLectura, bool bAjusteColumnas, List<string> ColumnasHabilitar, List<string> ColumnasOcultar)
        {
            GridEditarColumnas(grid, bSoloLectura, bAjusteColumnas, ColumnasOcultar, new List<string>(), ColumnasHabilitar);
        }

        internal static void GridEditarColumnas(DevExpress.XtraGrid.GridControl grid, bool bSoloLectura, bool bAjusteColumnas, List<string> ColumnasOcultar, List<string> OcultarCamposDefault, List<string> ColumnasHabilitar)
        {
            GridEditarColumnas(grid, bSoloLectura, bAjusteColumnas, ColumnasOcultar, false, OcultarCamposDefault, ColumnasHabilitar);
        }

        internal static void GridEditarColumnas(DevExpress.XtraGrid.GridControl grid, bool bSoloLectura, bool bAjusteColumnas, List<string> ColumnasOcultar, bool bFechaHora, List<string> OcultarCamposDefault, List<string> ColumnasHabilitar)
        {
            for (int i = 0; i < grid.Views.Count; i++)
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)grid.Views[i];

                if (view != null)
                {
                    view.ViewCaption = view.ViewCaption.Replace("_", " ");
                    foreach (DevExpress.XtraGrid.Columns.GridColumn columna in view.Columns)
                    {
                        if (columna.FieldName.EndsWith("_Id"))
                            columna.Visible = false;
                        columna.Caption = columna.FieldName.Replace("_", " ").ToUpper();
                        columna.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                        if (bSoloLectura && !ColumnasHabilitar.Contains(columna.FieldName))
                        {
                            columna.OptionsColumn.ReadOnly = true;
                            columna.OptionsColumn.AllowEdit = false;
                        }

                        if (ColumnasOcultar != null && ColumnasOcultar.Contains(columna.FieldName))
                            columna.Visible = false;

                        if (OcultarCamposDefault.Contains(columna.FieldName))
                            columna.Visible = false;


                        if (columna.ColumnType == typeof(DateTime))
                        {
                            if (bFechaHora)
                            {
                                columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                columna.DisplayFormat.FormatString = "g";
                            }
                            else
                            {
                                columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                columna.DisplayFormat.FormatString = "d";
                            }
                        }

                        if (columna.ColumnType == typeof(double))
                        {
                            columna.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            columna.DisplayFormat.FormatString = "c2";
                        }
                    }
                    if (bAjusteColumnas)
                        view.BestFitColumns();
                }
            }
        }

        internal static void LlenarCombo(DevExpress.XtraEditors.LookUpEdit combo, object lista, string campoId, string campoMostrar)
        {
            if (lista != null)
            {
                combo.Properties.DataSource = lista;
                combo.Properties.DisplayMember = campoMostrar;
                combo.Properties.ValueMember = campoId;
                combo.Properties.ForceInitialize();
                combo.Properties.PopulateColumns();
                combo.Refresh();
                for (int i = 0; i < combo.Properties.Columns.Count; i++)
                {
                    combo.Properties.Columns[i].Visible = false;
                }
                //Poner visible la columna CampoBusqueda                
                combo.Properties.Columns[campoMostrar].Visible = true;
            }
        }

        internal static object objetoComboSeleccionar(DataTable dt, string campoBuscar, string campoSacar, object valor)
        {
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row[campoBuscar] is string)
                    {
                        if (string.Compare(row[campoBuscar].ToString(), valor.ToString(), true) == 0)
                        {
                            return Convert.ToString(row[campoSacar]);
                        }
                    }
                    else if (row[campoBuscar] is DateTime)
                    {
                        if (Convert.ToDateTime(valor).CompareTo(Convert.ToDateTime(row[campoBuscar])) == 0)
                        {
                            return Convert.ToDateTime(row[campoSacar]);
                        }
                    }
                    else if (row[campoBuscar] is Int32)
                    {
                        if (Convert.ToInt32(valor) == Convert.ToInt32(row[campoBuscar]))
                        {
                            return Convert.ToInt32(row[campoSacar]);
                        }
                    }
                    else if (row[campoBuscar] is Int64)
                    {
                        if (Convert.ToInt64(valor) == Convert.ToInt64(row[campoBuscar]))
                        {
                            return Convert.ToInt64(row[campoSacar]);
                        }
                    }
                    else if (row[campoBuscar] is double)
                    {
                        if (Convert.ToDouble(valor) == Convert.ToDouble(row[campoBuscar]))
                        {
                            return Convert.ToDouble(row[campoSacar]);
                        }
                    }
                    else if (row[campoBuscar] is bool)
                    {
                        if (Convert.ToBoolean(valor) == Convert.ToBoolean(row[campoBuscar]))
                        {
                            return Convert.ToBoolean(row[campoSacar]);
                        }
                    }
                    else if (row[campoBuscar] is decimal)
                    {
                        if (Convert.ToDecimal(valor) == Convert.ToDecimal(row[campoBuscar]))
                        {
                            return Convert.ToDecimal(row[campoSacar]);
                        }
                    }
                }
            }
            return null;
        }

        internal static void LlenarCombo(DevExpress.XtraEditors.LookUpEdit combo, object lista, string campoId, string campoMostrar, long valueSelected)
        {
            if (lista != null)
            {
                combo.Properties.DataSource = lista;
                combo.Properties.DisplayMember = campoMostrar;
                combo.Properties.ValueMember = campoId;
                combo.Properties.ForceInitialize();
                combo.Properties.PopulateColumns();
                combo.Refresh();
                for (int i = 0; i < combo.Properties.Columns.Count; i++)
                {
                    combo.Properties.Columns[i].Visible = false;
                }
                //Poner visible la columna CampoBusqueda                
                combo.Properties.Columns[campoMostrar].Visible = true;
            }
        }
        internal static void MostrarWaitDialog(string mensaje)
        {
            MostrarWaitDialog(mensaje, "");
        }

        internal static void MostrarWaitDialog(string mensaje, string titulo)
        {
            if (Global.WaitDialog == null)
                Global.WaitDialog = new DevExpress.Utils.WaitDialogForm(mensaje, titulo);
            else
            {
                Global.WaitDialog.Caption = mensaje;
                Global.WaitDialog.Text = titulo;
            }
            Global.WaitDialog.Show();
        }

        internal static void CerrarWaitDialog()
        {
            if (Global.WaitDialog != null)
            {
                Global.WaitDialog.Close();
                Global.WaitDialog = null;
            }
        }

        public static Clases.CFDS CargarXMLFactura(string RutaArchivo, TIPO_FACTURA tipo)
        {
            Clases.CFDS CFDS = null;
            Provedor provedor = null;
            bool found = false;
            try
            {
                string xml = File.ReadAllText(RutaArchivo);
                XDocument doc = XDocument.Parse(xml);
                XNamespace cfdi = doc.Root.Name.Namespace;
                CFDS = new Clases.CFDS();

                XElement comprobante = doc.Element(cfdi + "Comprobante");
                if (comprobante != null)
                {
                    XAttribute atributo = comprobante.Attribute("certificado");
                    CFDS.Certificado = (atributo != null) ? atributo.Value.ToString() : "";

                    atributo = comprobante.Attribute("serie");
                    CFDS.Serie = (atributo != null) ? atributo.Value.ToString() : "";

                    atributo = comprobante.Attribute("folio");
                    CFDS.Folio = (atributo != null) ? atributo.Value.ToString() : "";

                    atributo = comprobante.Attribute("noAprobacion");
                    CFDS.Numero_Aprobacion = (atributo != null) ? atributo.Value.ToString() : "";

                    atributo = comprobante.Attribute("fecha");
                    if (atributo == null)
                        CFDS.Fecha = CFDS.Fecha;
                    else
                        CFDS.Fecha = Convert.ToDateTime(atributo.Value);

                    atributo = comprobante.Attribute("TipoCambio");
                    CFDS.TipoCambio = (atributo != null) ? Convert.ToDouble(atributo.Value) : 1.00;

                    atributo = comprobante.Attribute("Moneda");
                    CFDS.Moneda = (atributo != null) ? atributo.Value.ToString() : "";

                    atributo = comprobante.Attribute("subTotal");
                    CFDS.SubTotal = CFDS.TipoCambio * ((atributo != null) ? Convert.ToDouble(atributo.Value) : 0.00);

                    atributo = comprobante.Attribute("descuento");
                    CFDS.Descuento = CFDS.TipoCambio * ((atributo != null) ? Convert.ToDouble(atributo.Value) : 0.00);

                    atributo = comprobante.Attribute("total");
                    CFDS.Total = CFDS.TipoCambio * ((atributo != null) ? Convert.ToDouble(atributo.Value) : 0.00);

                    atributo = comprobante.Attribute("sello");
                    CFDS.Sello = (atributo != null) ? atributo.Value.ToString() : "";

                    atributo = comprobante.Attribute("tipoDeComprobante");
                    CFDS.Tipo_Comprobante = (atributo != null) ? atributo.Value.ToString() : "";

                    provedor = new Provedor();
                    XElement emisor = comprobante.Element(cfdi + "Emisor");
                    if (emisor != null)
                    {
                        XAttribute att = emisor.Attribute("rfc");
                        CFDS.RFC_Emisor = (att != null) ? att.Value.ToString() : "";

                        att = emisor.Attribute("nombre");
                        CFDS.Nombre_Emisor = (att != null) ? att.Value.ToString() : "";
                    }
                    if (tipo == TIPO_FACTURA.ENTRADA || tipo == TIPO_FACTURA.GASTOS)
                    {
                        provedor.Nombre = CFDS.Nombre_Emisor;
                        provedor.Razon_Social = CFDS.Nombre_Emisor;
                        provedor.RFC = CFDS.RFC_Emisor;
                        found = provedor.Cargar().Result;
                        if (!found)
                        {
                            XElement domicilioFiscal = emisor.Element(cfdi + "DomicilioFiscal");
                            if (domicilioFiscal != null)
                            {
                                XAttribute att = domicilioFiscal.Attribute("calle");
                                provedor.Calle = (att != null) ? att.Value.ToString() : "";

                                att = domicilioFiscal.Attribute("codigoPostal");
                                provedor.Codigo_Postal = (att != null) ? att.Value.ToString() : "";

                                att = domicilioFiscal.Attribute("colonia");
                                provedor.Colonia = (att != null) ? att.Value.ToString() : "";

                                att = domicilioFiscal.Attribute("estado");
                                provedor.Estado = (att != null) ? att.Value.ToString() : "";

                                att = domicilioFiscal.Attribute("localidad");
                                provedor.Localidad = (att != null) ? att.Value.ToString() : "";

                                att = domicilioFiscal.Attribute("municipio");
                                provedor.Ciudad = (att != null) ? att.Value.ToString() : "";

                                att = domicilioFiscal.Attribute("noExterior");
                                provedor.NumExt = (att != null) ? att.Value.ToString() : "";

                                att = domicilioFiscal.Attribute("noInterior");
                                provedor.NumInt = (att != null) ? att.Value.ToString() : "";

                                att = domicilioFiscal.Attribute("pais");
                                provedor.Pais = (att != null) ? att.Value.ToString() : "";
                            }
                            provedor.Activo = true;
                            provedor.Grabar();
                        }
                    }
                    XElement receptor = comprobante.Element(cfdi + "Receptor");
                    if (receptor != null)
                    {
                        XAttribute att = receptor.Attribute("rfc");
                        CFDS.RFC_Receptor = (att != null) ? att.Value.ToString() : "";

                        att = receptor.Attribute("nombre");
                        CFDS.Nombre_Receptor = (att != null) ? att.Value.ToString() : "";
                    }

                    XElement complemento = comprobante.Element(cfdi + "Complemento");
                    if (complemento != null)
                    {
                        if (complemento.FirstNode != null)
                        {
                            XNamespace tfd = ((System.Xml.Linq.XElement)complemento.FirstNode).Name.Namespace;

                            XElement timbre = complemento.Element((tfd ?? "") + "TimbreFiscalDigital");
                            if (timbre != null)
                            {
                                XAttribute att = timbre.Attribute("UUID");
                                CFDS.Folio_Fiscal = (att != null) ? att.Value.ToString() : "";

                                att = timbre.Attribute("FechaTimbrado");
                                if (att != null)
                                    CFDS.Fecha_Fiscal = Convert.ToDateTime(att.Value);
                            }
                        }
                    }

                    XElement impuestos = comprobante.Element(cfdi + "Impuestos");
                    if (impuestos != null)
                    {
                        XElement traslados = impuestos.Element(cfdi + "Traslados");
                        if (traslados != null)
                        {
                            XElement traslado = traslados.Element(cfdi + "Traslado");
                            if (traslado != null)
                            {
                                XAttribute att = traslado.Attribute("impuesto");
                                CFDS.Impuesto = (att != null) ? att.Value.ToString() : "";

                                att = traslado.Attribute("tasa");
                                CFDS.Tasa = (att != null) ? att.Value.ToString() : "";

                                att = traslado.Attribute("importe");
                                CFDS.Importe_IVA = CFDS.TipoCambio * ((att != null) ? Convert.ToDouble(att.Value) : 0.00);
                            }
                        }
                    }

                    XElement conceptos = comprobante.Element(cfdi + "Conceptos");
                    if (conceptos != null)
                    {
                        //CFDS_Gasto gasto = null;
                        CFDS_Producto producto = null;
                        foreach (XElement concepto in conceptos.Elements(cfdi + "Concepto"))
                        {
                            XAttribute att = concepto.Attribute("cantidad");
                            if (tipo == TIPO_FACTURA.GASTOS)
                            {
                                /*gasto = new CFDS_Gasto();
                                gasto.Cantidad = (att != null) ? Convert.ToDouble(att.Value) : 0.00;
                                att = concepto.Attribute("unidad");
                                gasto.Unidad = (att != null) ? att.Value.ToString() : "";
                                att = concepto.Attribute("noIdentificacion");
                                gasto.Numero_Identificacion = (att != null) ? att.Value.ToString() : "";

                                att = concepto.Attribute("descripcion");
                                gasto.Descripcion = (att != null) ? att.Value.ToString() : "";

                                att = concepto.Attribute("valorUnitario");
                                gasto.Valor_Unitario = CFDS.TipoCambio * ((att != null) ? Convert.ToDouble(att.Value) : 0.00);
                                att = concepto.Attribute("importe");
                                gasto.Importe = CFDS.TipoCambio * ((att != null) ? Convert.ToDouble(att.Value) : 0.00);*/
                            }
                            else
                            {
                                producto = new CFDS_Producto();
                                producto.Cantidad_Factura = (att != null) ? Convert.ToDouble(att.Value) : 0.00;
                                att = concepto.Attribute("unidad");
                                producto.Unidad = (att != null) ? att.Value.ToString() : "";
                                att = concepto.Attribute("noIdentificacion");
                                producto.Numero_Identificacion = (att != null) ? att.Value.ToString() : "";
                                producto.Codigo_Producto = producto.Numero_Identificacion;

                                att = concepto.Attribute("descripcion");
                                producto.Descripcion = (att != null) ? att.Value.ToString() : "";

                                att = concepto.Attribute("valorUnitario");
                                producto.Valor_Unitario = CFDS.TipoCambio * ((att != null) ? Convert.ToDouble(att.Value) : 0.00);
                                producto.ValorUnitarioOriginal = producto.Valor_Unitario;
                                att = concepto.Attribute("importe");
                                producto.Importe = CFDS.TipoCambio * ((att != null) ? Convert.ToDouble(att.Value) : 0.00);
                            }
                            //Provedor provedor = new Provedor();
                            provedor.RFC = (tipo == TIPO_FACTURA.ENTRADA || tipo == TIPO_FACTURA.GASTOS) ? CFDS.RFC_Emisor : CFDS.RFC_Receptor;

                            if ((tipo == TIPO_FACTURA.ENTRADA || tipo == TIPO_FACTURA.GASTOS) && found)// provedor.Cargar(provedor).Result)
                            {
                                if (tipo == TIPO_FACTURA.ENTRADA)
                                {
                                    if (producto.Producto_Id <= 0 && producto.Numero_Identificacion.Trim().Length > 0)
                                    {
                                        Diccionario diccionario = new Diccionario();
                                        Diccionario resultado = null;
                                        resultado = diccionario.Buscar(provedor.Provedor_Id, producto.Numero_Identificacion);
                                        producto.Producto_Id = resultado.Producto_Id;
                                    }

                                    if (producto.Producto_Id <= 0 && producto.Descripcion.Trim().Length > 0)
                                    {
                                        Diccionario diccionario = new Diccionario();
                                        Diccionario resultado = null;
                                        resultado = diccionario.Buscar(provedor.Provedor_Id, producto.Descripcion);
                                        producto.Producto_Id = resultado.Producto_Id;
                                    }
                                }
                                /*else
                                {
                                    if (gasto.Gasto_Id <= 0 && gasto.Numero_Identificacion.Trim().Length > 0)
                                    {
                                        DiccionarioGastos diccionario = new DiccionarioGastos();
                                        DiccionarioGastos resultado = null;
                                        resultado = diccionario.Buscar(provedor.Provedor_Id, gasto.Numero_Identificacion);
                                        gasto.Gasto_Id = resultado.Gasto_Id;
                                    }

                                    if (gasto.Gasto_Id <= 0 && gasto.Descripcion.Trim().Length > 0)
                                    {
                                        DiccionarioGastos diccionario = new DiccionarioGastos();
                                        DiccionarioGastos resultado = null;
                                        resultado = diccionario.Buscar(provedor.Provedor_Id, gasto.Descripcion);
                                        gasto.Gasto_Id = resultado.Gasto_Id;
                                    }
                                }*/
                            }
                            if (tipo != TIPO_FACTURA.GASTOS)
                            {
                                if (producto.Producto_Id < 1)
                                {
                                    if (producto.Numero_Identificacion.Trim().Length > 0)
                                    {
                                        Productos prod = new Productos();
                                        if (producto.Numero_Identificacion.Trim().Length > 12)
                                            prod.Codigo_de_Barras = producto.Numero_Identificacion.Trim();
                                        else
                                        {
                                            prod.Codigo_de_Barras = "";
                                            prod.Codigo_Producto = producto.Numero_Identificacion.Trim();
                                        }

                                        if (prod.Cargar().Result)
                                        {
                                            producto.ClearProducto();
                                            producto.Producto_Id = prod.Producto_Id;
                                        }
                                    }
                                }
                            }
                            if (tipo == TIPO_FACTURA.SALIDA)
                            {
                                if (producto.Numero_Identificacion.Trim().Length > 0)
                                {
                                    Productos prod = new Productos();
                                    prod.Codigo_Producto = producto.Numero_Identificacion.Trim();
                                    if (prod.Cargar().Result)
                                    {
                                        producto.ClearProducto();
                                        producto.Producto_Id = prod.Producto_Id;
                                    }
                                }
                            }
                            if (tipo == TIPO_FACTURA.GASTOS)
                            {
                                /*CFDS.Gastos.Add(gasto);
                                gasto = null;*/
                            }
                            else
                            {
                                CFDS.Productos.Add(producto);
                                producto = null;
                            }
                        }
                    }

                    XElement adenda = doc.Element(cfdi + "Addenda");
                    if (adenda != null)
                    {
                        XNamespace ecfd = adenda.Name.Namespace;

                        XElement ECFD = adenda.Element(ecfd + "ECFD");
                        if (ECFD != null)
                        {
                            XElement documento = adenda.Element(ecfd + "Documento");
                            if (documento != null)
                            {
                                foreach (XElement detalle in documento.Elements(ecfd + "Detalle"))
                                {
                                    XElement elemento = detalle.Element(ecfd + "NroLinDet");
                                    int nroLinDet = nroLinDet = elemento != null ? Convert.ToInt32(elemento.Value) : 0;

                                    if (nroLinDet > 0)
                                    {
                                        try
                                        {
                                            if (tipo == TIPO_FACTURA.GASTOS)
                                            {
                                                /*CFDS_Gasto gasto = CFDS.Gastos[nroLinDet - 1];

                                                if (gasto.Descuento_Porciento <= 0)
                                                {
                                                    elemento = detalle.Element("DescuentoPct");
                                                    gasto.Descuento_Porciento = (elemento != null) ? Convert.ToDouble(elemento.Value) : 0.00;
                                                }

                                                if (gasto.Descuento_Monto <= 0)
                                                {
                                                    elemento = detalle.Element("DescuentoMonto");
                                                    gasto.Descuento_Monto = CFDS.TipoCambio * ((elemento != null) ? Convert.ToDouble(elemento.Value) : 0.00);

                                                    if (gasto.Descuento_Monto <= 0 && gasto.Descuento_Porciento > 0)
                                                    {
                                                        gasto.Descuento_Monto = gasto.Importe * (gasto.Descuento_Porciento / (double)100);
                                                    }

                                                }

                                                XElement impuestoDet = detalle.Element("ImpuestosDet");
                                                if (impuestoDet != null)
                                                {
                                                    if (gasto.Impuesto_Tasa <= 0)
                                                    {
                                                        elemento = impuestoDet.Element("TasaImp");
                                                        gasto.Impuesto_Tasa = (elemento != null) ? Convert.ToDouble(elemento.Value) : 0.00;
                                                    }

                                                    if (gasto.Impuesto_Monto <= 0)
                                                    {
                                                        elemento = impuestoDet.Element("MontoImp");
                                                        gasto.Impuesto_Monto = CFDS.TipoCambio * ((elemento != null) ? Convert.ToDouble(elemento.Value) : 0.00);
                                                    }
                                                }*/
                                            }
                                            else
                                            {
                                                CFDS_Producto producto = CFDS.Productos[nroLinDet - 1];

                                                if (producto.Descuento_Porciento <= 0)
                                                {
                                                    elemento = detalle.Element("DescuentoPct");
                                                    producto.Descuento_Porciento = (elemento != null) ? Convert.ToDouble(elemento.Value) : 0.00;
                                                }

                                                if (producto.Descuento_Monto <= 0)
                                                {
                                                    elemento = detalle.Element("DescuentoMonto");
                                                    producto.Descuento_Monto = CFDS.TipoCambio * ((elemento != null) ? Convert.ToDouble(elemento.Value) : 0.00);

                                                    if (producto.Descuento_Monto <= 0 && producto.Descuento_Porciento > 0)
                                                    {
                                                        producto.Descuento_Monto = producto.Importe * (producto.Descuento_Porciento / (double)100);
                                                    }

                                                }

                                                XElement impuestoDet = detalle.Element("ImpuestosDet");
                                                if (impuestoDet != null)
                                                {
                                                    if (producto.Impuesto_Tasa <= 0)
                                                    {
                                                        elemento = impuestoDet.Element("TasaImp");
                                                        producto.Impuesto_Tasa = (elemento != null) ? Convert.ToDouble(elemento.Value) : 0.00;
                                                    }

                                                    if (producto.Impuesto_Monto <= 0)
                                                    {
                                                        elemento = impuestoDet.Element("MontoImp");
                                                        producto.Impuesto_Monto = CFDS.TipoCambio * ((elemento != null) ? Convert.ToDouble(elemento.Value) : 0.00);
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception innerex)
                                        {
                                            Log.Logger.Error(innerex, innerex.Message);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                string RutaArchivoOrigen = RutaArchivo.Replace("TMP.xml", ".xml");

                string FolioA, SerieA;
                FolioA = "";
                SerieA = "";

                if (CFDS.Folio_Fiscal != "")
                {
                    FolioA = CFDS.Folio_Fiscal;
                    SerieA = "";
                }
                else if (CFDS.Serie == "" && CFDS.Folio == "")
                {
                    CFDS.Folio = FolioA;
                }
                else
                {
                    FolioA = CFDS.Folio;
                    SerieA = CFDS.Serie;
                }

                CFDS.Tipo_Id = (int)tipo;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                CFDS = null;
            }

            return CFDS;
        }
        public static CFDS CargarXMLCancelacion(string RutaArchivo)
        {
            CFDS CFDS = null;
            try
            {
                XDocument doc = XDocument.Load(RutaArchivo);
                XNamespace cfdi = doc.Root.Name.Namespace;

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(RutaArchivo);
                CFDS = new CFDS();

                XElement comprobante = doc.Element(cfdi + "Acuse");
                if (comprobante != null)
                {
                    XAttribute atributo = comprobante.Attribute("RfcEmisor");
                    CFDS.RFC_Emisor = (atributo != null) ? atributo.Value.ToString() : "";

                    atributo = comprobante.Attribute("Fecha");
                    CFDS.Fecha = Convert.ToDateTime(atributo.Value);

                    string RutaArchivoOrigen = RutaArchivo.Replace("TMP.xml", ".xml");
                    //CFDS.Archivo_Origen = Path.GetFileName(RutaArchivoOrigen);
                    //CFDS.Archivo_Destino = CFDS.Archivo_Origen.ToUpper();

                }

                XmlNodeList personas = xDoc.GetElementsByTagName("Acuse");
                XmlNodeList lista = ((XmlElement)personas[0]).GetElementsByTagName("Folios");
                foreach (XmlElement nodo in lista)
                {
                    XmlNodeList nNombre = nodo.GetElementsByTagName("UUID");
                    CFDS.Folio_Fiscal = (nNombre[0].InnerText != null) ? nNombre[0].InnerText : "";
                    CFDS.Grabar_Cancelaciones(nNombre[0].InnerText, RutaArchivo);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                CFDS = null;
            }
            return CFDS;
        }

        internal static void addNewRowInGroupMode(DevExpress.XtraGrid.Views.Grid.GridView View)
        {
            //Get the handle of the source data row 
            //The row will provide group column values for a new row 
            int rowHandle = View.GetDataRowHandleByGroupRowHandle(View.FocusedRowHandle);
            //Store group column values 
            object[] groupValues = null;
            int groupColumnCount = View.GroupedColumns.Count;
            if (groupColumnCount > 0)
            {
                groupValues = new object[groupColumnCount];
                for (int i = 0; i < groupColumnCount; i++)
                {
                    groupValues[i] = View.GetRowCellValue(rowHandle, View.GroupedColumns[i]);
                }
            }
            //Add a new row 
            View.AddNewRow();
            //Get the handle of the new row 
            int newRowHandle = View.FocusedRowHandle;
            object newRow = View.GetRow(newRowHandle);
            //Set cell values corresponding to group columns 
            if (groupColumnCount > 0)
            {
                for (int i = 0; i < groupColumnCount; i++)
                {
                    View.SetRowCellValue(newRowHandle, View.GroupedColumns[i], groupValues[i]);
                }
            }
            //Accept the new row 
            //The row moves to a new position according to the current group settings 
            View.UpdateCurrentRow();
            //Locate the new row 
            for (int n = 0; n < View.DataRowCount; n++)
            {
                if (View.GetRow(n).Equals(newRow))
                {
                    View.FocusedRowHandle = n;
                    break;
                }
            }
        }
        public static string Letras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            //if (decimales > 0)
            {
                dec = " PESOS " + decimales.ToString() + "/100 MN";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }

        /*public static Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();
            Bitmap bitmap = null;
            Convertor.ConvertToPicture(Sample, ref bitmap);
            return bitmap;
        }*/

        public static void GridViewSumarColumnas(DevExpress.XtraGrid.Views.Grid.GridView view, List<string> ColumnasSumar)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn columna in view.Columns)
            {
                if (columna.FieldName.EndsWith("_Id"))
                    columna.Visible = false;

                columna.Caption = columna.FieldName.Replace("_", " ").ToUpper();
                columna.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;

                if (ColumnasSumar.Contains(columna.FieldName))
                {
                    columna.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                    columna.SummaryItem.DisplayFormat = "Total : {0:c2}";
                }
            }
        }
    }
}
