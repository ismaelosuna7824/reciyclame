using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class CFDS : ClaseBase
    {
        public long CFDS_Id { get; set; }
        public DateTime Fecha { get; set; }
        public string RFC_Emisor { get; set; }
        public string Nombre_Emisor { get; set; }
        public string RFC_Receptor { get; set; }
        public string Nombre_Receptor { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public string Folio_Fiscal { get; set; }
        public DateTime Fecha_Fiscal { get; set; }
        public string Numero_Aprobacion { get; set; }
        public string Sello { get; set; }
        public string Certificado { get; set; }
        public string Tipo_Comprobante { get; set; }
        public double SubTotal { get; set; }
        public double Descuento { get; set; }
        public double Total { get; set; }
        public string Impuesto { get; set; }
        public string Tasa { get; set; }
        public double Importe_IVA { get; set; }
        public int Tipo_Id { get; set; }
        public string Tipo
        {
            get
            {
                string valor = "";
                try
                {
                    if (Tipo_Id >= 0)
                        valor = ((TIPO_ARCHIVO)Tipo_Id).ToString().Replace("_", " ");
                }
                catch { }
                return valor;
            }
        }
        public string Estatus { get; set; }
        public string Comentario { get; set; }
        public long Tipo_Movimiento_Id { get; set; }
        public long IdSucursal { get; set; }
        public string Sucursal { get; set; }
        public double Flete { get; set; }
        public int IdDatosFiscales { get; set; }

        public BindingList<CFDS_Archivo> Archivos;
        public BindingList<CFDS_Producto> Productos;
        //public BindingList<CFDS_Gasto> Gastos;
        public string Moneda { get; set; }
        public double TipoCambio { get; set; }

        //override public string CampoId { get { return "CFDS_Id"; } }
        //override public string CampoBusqueda { get { return "Folio_Fiscal"; } }
        //protected override string QueryGrabar { get { return "CFDS_Grabar_sp"; } }
        //protected override string QueryGrabarCodigo { get { return "CFDS_Grabar_sp_codigo"; } }
        ////protected override string QueryGrabarFlete { get { return "CFDS_Grabar_sp_flete"; } }
        //protected override string QueryConsultar { get { return "CFDS_Consultar_sp"; } }
        //protected string QueryCancelar { get { return "CFDS_Cancelar_sp"; } }

        public CFDS()
        {
            TipoClase = ClaseTipo.CFDS;
            CampoId = "CFDS_Id";
            CampoBusqueda = "Folio_Fiscal";
            QueryGrabar = "CFDS_Grabar_sp";
            QueryGrabarCodigo = "CFDS_Grabar_sp_codigo";
            QueryConsultar = "CFDS_Consultar_sp";
            QueryCancelar = "CFDS_Cancelar_sp";
            CFDS_Id = -1;
            IdSucursal = -1;
            RFC_Emisor = "";
            Nombre_Emisor = "";
            RFC_Receptor = "";
            Nombre_Receptor = "";
            Serie = "";
            Folio = "";
            Fecha = new DateTime(1900, 1, 1);
            Folio_Fiscal = "";
            Fecha_Fiscal = new DateTime(1900, 1, 1);
            Numero_Aprobacion = "";
            Sello = "";
            Certificado = "";
            Tipo_Comprobante = "";
            SubTotal = 0.00;
            Descuento = 0.00;
            Total = 0.00;
            Impuesto = "";
            Tasa = "";
            Importe_IVA = 0.00;
            Tipo_Id = -1;
            Estatus = "NUEVO";
            Comentario = "";
            Tipo_Movimiento_Id = 0;
            Sucursal = "";
            Moneda = "";
            TipoCambio = 1;
            Flete = 0.00;
            IdDatosFiscales = 0;

            Archivos = new BindingList<CFDS_Archivo>()
            {
                AllowEdit = true,
                AllowNew = true,
                AllowRemove = true,
                RaiseListChangedEvents = true
            };

            Productos = new BindingList<CFDS_Producto>()
            {
                AllowEdit = true,
                AllowNew = true,
                AllowRemove = true,
                RaiseListChangedEvents = true
            };
            /*Gastos = new BindingList<CFDS_Gasto>()
            {
                AllowEdit = true,
                AllowNew = true,
                AllowRemove = true,
                RaiseListChangedEvents = true
            };*/

        }

        #region Metodos

        ///// <summary>
        ///// Ejecuta el metodo Grabar.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        //override public bool Grabar()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    SqlParameter paramId = new SqlParameter();
        //    paramId.ParameterName = "@P_CFDS_Id";
        //    paramId.Value = CFDS_Id;
        //    paramId.Direction = System.Data.ParameterDirection.InputOutput;
        //    parametros.Add(paramId);

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = RFC_Emisor });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Emisor", Value = Nombre_Emisor });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Receptor", Value = RFC_Receptor });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Receptor", Value = Nombre_Receptor });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Serie", Value = Serie });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Folio", Value = Folio });

        //    if (Fecha > DateTime.MinValue)
        //        parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = Fecha });

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Folio_Fiscal", Value = Folio_Fiscal });

        //    if (Fecha > DateTime.MinValue)
        //        parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha_Fiscal", Value = Fecha_Fiscal });

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Numero_Aprobacion", Value = Numero_Aprobacion });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Sello", Value = Sello });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Certificado", Value = Certificado });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Comprobante", Value = Tipo_Comprobante });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_SubTotal", Value = SubTotal });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento", Value = Descuento });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = Total });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Impuesto", Value = Impuesto });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tasa", Value = Tasa });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Importe_IVA", Value = Importe_IVA });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Id", Value = Tipo_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Estatus", Value = Estatus });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Comentario", Value = Comentario });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = Tipo_Movimiento_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Sucursal", Value = Sucursal });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Flete", Value = Flete });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
        //    if (resultado && CFDS_Id == -1)
        //    {
        //        CFDS_Id = Convert.ToInt64(paramId.Value);
        //    }

        //    foreach (CFDS_Archivo archivo in Archivos)
        //        archivo.CFDS_Id = CFDS_Id;

        //    foreach (CFDS_Producto producto in Productos)
        //        producto.CFDS_Id = CFDS_Id;

        //    if (resultado)
        //    {
        //        GrabarArchivos();
        //        GrabarProductos();
        //    }

        //    return resultado;
        //}

        ///// <summary>
        ///// Ejecuta el metodo GrabarFlete.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        ///*override public bool GrabarFlete()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    SqlParameter paramId = new SqlParameter();
        //    paramId.ParameterName = "@P_CFDS_Id";
        //    paramId.Value = CFDS_Id;
        //    paramId.Direction = System.Data.ParameterDirection.InputOutput;
        //    parametros.Add(paramId);

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = RFC_Emisor });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Emisor", Value = Nombre_Emisor });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Receptor", Value = RFC_Receptor });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Receptor", Value = Nombre_Receptor });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Serie", Value = Serie });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Folio", Value = Folio });

        //    if (Fecha > DateTime.MinValue)
        //        parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = Fecha });

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Folio_Fiscal", Value = Folio_Fiscal });

        //    if (Fecha > DateTime.MinValue)
        //        parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha_Fiscal", Value = Fecha_Fiscal });

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Numero_Aprobacion", Value = Numero_Aprobacion });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Sello", Value = Sello });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Certificado", Value = Certificado });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Comprobante", Value = Tipo_Comprobante });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_SubTotal", Value = SubTotal });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento", Value = Descuento });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = Total });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Impuesto", Value = Impuesto });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tasa", Value = Tasa });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Importe_IVA", Value = Importe_IVA });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Id", Value = Tipo_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Estatus", Value = Estatus });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Comentario", Value = Comentario });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = Tipo_Movimiento_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Sucursal", Value = Sucursal });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Flete", Value = Flete });

        //    resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabarFlete, parametros) > 0);
        //    if (resultado && CFDS_Id == -1)
        //    {
        //        CFDS_Id = Convert.ToInt64(paramId.Value);
        //    }

        //    foreach (CFDS_Archivo archivo in Archivos)
        //        archivo.CFDS_Id = CFDS_Id;

        //    foreach (CFDS_Producto producto in Productos)
        //        producto.CFDS_Id = CFDS_Id;

        //    if (resultado)
        //    {
        //        GrabarArchivosFletes();
        //        GrabarProductosFlete();
        //    }

        //    return resultado;
        //}*/

        public bool GuardarArchivos()
        {
            return GrabarArchivos();
        }
        private bool GrabarArchivos()
        {
            bool resultado = true;

            BorrarArchivos();

            foreach (CFDS_Archivo archivo in Archivos)
            {
                if (!(resultado = archivo.Grabar()))
                    break;
            }

            return resultado;
        }

        /*private bool GrabarArchivosFletes()
        {
            bool resultado = true;

            BorrarArchivosFletes();

            foreach (CFDS_Archivo archivo in Archivos)
            {
                if (!(resultado = archivo.GrabarFlete()))
                    break;
            }

            return resultado;
        }*/

        private bool BorrarArchivos()
        {
            bool resultado = true;
            bool existe = false;
            CFDS cfds = new CFDS();
            cfds.CFDS_Id = CFDS_Id;
            cfds.Folio_Fiscal = Folio_Fiscal;
            if (cfds.Cargar().Result)
            {
                foreach (CFDS_Archivo archivoAnt in cfds.Archivos)
                {
                    existe = false;
                    foreach (CFDS_Archivo archivoNva in Archivos)
                    {
                        if (archivoAnt.CFDS_Archivo_Id == archivoNva.CFDS_Archivo_Id)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado = archivoAnt.Borrar();
                        if (!resultado)
                            break;
                    }
                }
            }
            return resultado;
        }

        private bool BorrarArchivosFletes()
        {
            bool resultado = true;
            bool existe = false;
            CFDS cfds = new CFDS();
            cfds.CFDS_Id = CFDS_Id;
            if (cfds.Cargar().Result)
            {
                foreach (CFDS_Archivo archivoAnt in cfds.Archivos)
                {
                    existe = false;
                    foreach (CFDS_Archivo archivoNva in Archivos)
                    {
                        if (archivoAnt.CFDS_Archivo_Id == archivoNva.CFDS_Archivo_Id)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado = archivoAnt.Borrar();
                        if (!resultado)
                            break;
                    }
                }
            }
            return resultado;
        }

        public bool GuardarProductos()
        {
            return GrabarProductos();
        }
        private bool GrabarProductos()
        {
            bool resultado = true;

            BorrarProductos();

            foreach (CFDS_Producto producto in Productos)
            {
                if (producto.Agregar)
                {
                    if (!(resultado = producto.Grabar()))
                        break;
                }
            }

            return resultado;
        }

        /*public bool GuardarGastos()
        {
            return GrabarGastos();
        }
        private bool GrabarGastos()
        {
            bool resultado = true;

            BorrarGastos();

            foreach (CFDS_Gasto gasto in Gastos)
            {
                gasto.CFDS_Id = CFDS_Id;
                if (!(resultado = gasto.Grabar()))
                    break;

            }

            return resultado;
        }*/

        /*private bool BorrarGastos()
        {
            bool resultado = true;
            bool existe = false;
            CFDS cfds = new CFDS();
            cfds.CFDS_Id = CFDS_Id;
            cfds.Folio_Fiscal = Folio_Fiscal;
            if (cfds.Cargar().Result)
            {
                foreach (CFDS_Gasto gastoAnt in cfds.Gastos)
                {
                    existe = false;
                    foreach (CFDS_Gasto gastoNva in Gastos)
                    {
                        if (gastoAnt.CFDS_Gasto_Id == gastoNva.CFDS_Gasto_Id)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado = gastoAnt.Borrar();
                        if (!resultado)
                            break;
                    }
                }
            }
            return resultado;
        }*/
        private bool BorrarProductos()
        {
            bool resultado = true;
            bool existe = false;
            CFDS cfds = new CFDS();
            cfds.CFDS_Id = CFDS_Id;
            cfds.Folio_Fiscal = Folio_Fiscal;
            if (cfds.Cargar().Result)
            {
                foreach (CFDS_Producto productoAnt in cfds.Productos)
                {
                    existe = false;
                    foreach (CFDS_Producto productoNva in Productos)
                    {
                        if (productoAnt.CFDS_Producto_Id == productoNva.CFDS_Producto_Id)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado = productoAnt.Borrar();
                        if (!resultado)
                            break;
                    }
                }
            }
            return resultado;
        }

        private bool BorrarProductosFlete()
        {
            bool resultado = true;
            bool existe = false;
            CFDS cfds = new CFDS();
            cfds.CFDS_Id = CFDS_Id;
            if (cfds.Cargar().Result)
            {
                foreach (CFDS_Producto productoAnt in cfds.Productos)
                {
                    existe = false;
                    foreach (CFDS_Producto productoNva in Productos)
                    {
                        if (productoAnt.CFDS_Producto_Id == productoNva.CFDS_Producto_Id)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        resultado = productoAnt.Borrar();
                        if (!resultado)
                            break;
                    }
                }
            }
            return resultado;
        }

        public bool Grabar_Cancelaciones(string UID, string ARCHIVOXML)
        {
            string ARCHIVOXML1 = ARCHIVOXML.Replace("TMP.xml", ".xml").Replace("TMP.xml", ".XML");
            bool resultado = false;
            List<SqlParameter> parametros = new List<SqlParameter>();

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@v_aplica";
            paramId.Value = '0';
            paramId.Direction = System.Data.ParameterDirection.InputOutput;
            parametros.Add(paramId);


            parametros.Add(new SqlParameter() { ParameterName = "@V_UID", Value = UID });
            parametros.Add(new SqlParameter() { ParameterName = "@V_ARCHIVO_ORIGEN", Value = Path.GetFileName(ARCHIVOXML1) });
            parametros.Add(new SqlParameter() { ParameterName = "@V_ARCHIVO_DESTINO", Value = Path.GetFileName(ARCHIVOXML1) });
            parametros.Add(new SqlParameter() { ParameterName = "@V_FECHA_ACUSE", Value = Fecha });

            resultado = (BaseDatos.ejecutarProcedimiento(QueryCancelar, parametros) > 0);

            //if (Convert.ToInt64(paramId.Value) == 0)
            // {
            ////       resultado = false;
            // }
            return resultado;
        }

        ///// <summary>
        ///// Carga en los controles la informacion de un registro.
        ///// </summary>
        ///// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        //public override bool Cargar()
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = CFDS_Id });
        //    parametros.Add(new SqlParameter() { ParameterName = "@P_Folio_Fiscal", Value = Folio_Fiscal });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        foreach (DataRow row in dataset.Tables[QueryConsultar].Rows)
        //        {
        //            Cargar(row);
        //        }
        //        resultado = dataset.Tables[QueryConsultar].Rows.Count > 0;
        //    }
        //    return resultado;
        //}

        //public override bool Cargar_Mov_CFDI(int nCFDI)
        //{
        //    bool resultado = false;
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDI_Id", Value = nCFDI });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        foreach (DataRow row in dataset.Tables[QueryConsultar].Rows)
        //        {
        //            Cargar(row);
        //        }
        //        resultado = dataset.Tables[QueryConsultar].Rows.Count > 0;
        //    }
        //    return resultado;
        //}

        /// <summary>
        /// Carga en los controles la informacion de un registro.
        /// </summary>
        /// <param name="row">
        /// Row con la información a cargar
        /// </param>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        public override bool Cargar(System.Data.DataRowView row)
        {
            return Cargar(row.Row);
        }

        /// <summary>
        /// Carga en los controles la informacion de un registro.
        /// </summary>
        /// <param name="row">
        /// Row con la información a cargar
        /// </param>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        public override bool Cargar(System.Data.DataRow row)
        {
            bool resultado = false;

            try
            {
                CFDS_Id = Convert.ToInt64(row["CFDS_Id"]);
                RFC_Emisor = row["RFC_Emisor"].ToString();
                Nombre_Emisor = row["Nombre_Emisor"].ToString();
                RFC_Receptor = row["RFC_Receptor"].ToString();
                Nombre_Receptor = row["Nombre_Receptor"].ToString();
                Serie = row["Serie"].ToString();
                Folio = row["Folio"].ToString();
                Fecha = Convert.ToDateTime(row["Fecha"]);
                Folio_Fiscal = row["Folio_Fiscal"].ToString();
                Fecha_Fiscal = Convert.ToDateTime(row["Fecha_Fiscal"]);
                Numero_Aprobacion = row["Numero_Aprobacion"].ToString();
                Sello = row["Sello"].ToString();
                Certificado = row["Certificado"].ToString();
                Tipo_Comprobante = row["Tipo_Comprobante"].ToString();
                SubTotal = Convert.ToDouble(row["SubTotal"]);
                Descuento = Convert.ToDouble(row["Descuento"]);
                Total = Convert.ToDouble(row["Total"]);
                Impuesto = row["Impuesto"].ToString();
                Tasa = row["Tasa"].ToString();
                Importe_IVA = Convert.ToDouble(row["Importe_IVA"]);
                Tipo_Id = Convert.ToInt32(row["Tipo_Id"]);
                Estatus = row["Estatus"].ToString();
                Comentario = row["Comentario"].ToString();
                Tipo_Movimiento_Id = Convert.ToInt64(row["Tipo_Movimiento_Id"]);
                Sucursal = Convert.ToString(row["Sucursal"]);
                IdSucursal = Convert.ToInt64(row["IdSucursal"]);
                Flete = Convert.ToDouble(row["Flete"]);
                IdDatosFiscales = 0;

                CargarArchivos();
                if ((TIPO_FACTURA)Tipo_Id != TIPO_FACTURA.GASTOS)
                {
                    CargarProductos();
                }
                else
                {
                    //CargarGastos();
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                resultado = false;
            }

            return resultado;
        }

        ///// <summary>
        ///// Obtiene un listado.
        ///// </summary>
        ///// <returns>El DataTable que se obtiene despues de ejecutar el metodo</returns>
        //public override System.Data.DataTable Listado()
        //{
        //    DataTable resultado = new DataTable();
        //    List<SqlParameter> parametros = new List<SqlParameter>();

        //    parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = 0 });
        //    if (RFC_Emisor.Trim().Length > 0 || RFC_Receptor.Trim().Length > 0) parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Id", Value = Tipo_Id });
        //    if (RFC_Emisor.Trim().Length > 0) parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = RFC_Emisor });
        //    if (RFC_Receptor.Trim().Length > 0) parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Receptor", Value = RFC_Receptor });

        //    DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
        //    if (dataset != null && dataset.Tables.Count > 0)
        //    {
        //        resultado = dataset.Tables[QueryConsultar];
        //    }
        //    return resultado;
        //}

        #endregion;

        private bool CargarArchivos()
        {
            bool resultado = true;
            CFDS_Archivo cfdsArchivo = new CFDS_Archivo(CFDS_Id);
            CFDS_Archivo archivo;
            DataTable tabla = cfdsArchivo.Listado();
            Archivos.Clear();
            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    archivo = new CFDS_Archivo(CFDS_Id);
                    if (archivo.Cargar(row))
                        Archivos.Add(archivo);
                    else
                    {
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }

        private bool CargarProductos()
        {
            bool resultado = true;
            CFDS_Producto cfdsProducto = new CFDS_Producto(CFDS_Id);
            CFDS_Producto producto;
            DataTable tabla = cfdsProducto.Listado();
            Productos.Clear();
            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    producto = new CFDS_Producto(CFDS_Id);
                    if (producto.Cargar(row))
                        Productos.Add(producto);
                    else
                    {
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }

        /*private bool CargarGastos()
        {
            bool resultado = true;
            CFDS_Gasto cfdsGasto = new CFDS_Gasto(CFDS_Id);
            CFDS_Gasto gasto;
            DataTable tabla = cfdsGasto.Listado();
            Gastos.Clear();
            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    gasto = new CFDS_Gasto(CFDS_Id);
                    if (gasto.Cargar(row))
                        Gastos.Add(gasto);
                    else
                    {
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }*/

        public DataTable CargarFacturasSeleccionar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = Estatus });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Id", Value = Tipo_Id });
            parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = RFC_Emisor });
            return BaseDatos.ejecutarProcedimientoConsultaDataTable("CFDS_Seleccionar_sp", parametros);
        }
    }
}
