using RecyclameV2.PAC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public enum ClaseTipo
    {
        CFDS,
        CFDS_Archivo,
        CFDS_Producto,
        Diccionario,
        Producto_Ubicacion,
        Productos,
        Provedor,
        Tipo_Movimiento,
        Movimientos,
        Movimiento_Detalle,
        Sucursales
    }
    public abstract class ClaseBase
    {
        private ClaseTipo claseTipo = ClaseTipo.Provedor;
        public ClaseTipo TipoClase
        {
            set { claseTipo = value; }
            get { return claseTipo; }
        }

        private string campoid = string.Empty;
        public virtual string CampoId
        {
            set { campoid = value; }
            get { return campoid; }
        }

        private string campobusqueda = string.Empty;
        public virtual string CampoBusqueda
        {
            set { campobusqueda = value; }
            get { return campobusqueda; }
        }
        private string queryGrabar = string.Empty;
        protected virtual string QueryGrabar
        {
            set { queryGrabar = value; }
            get { return queryGrabar; }
        }
        private string queryGrabarCodigo = string.Empty;
        protected virtual string QueryGrabarCodigo
        {
            set { queryGrabarCodigo = value; }
            get { return queryGrabarCodigo; }
        }
        //abstract protected string QueryGrabarFlete { get; }
        private string queryConsultar = string.Empty;
        protected virtual string QueryConsultar
        {
            set { queryConsultar = value; }
            get { return queryConsultar; }
        }
        private string queryCancelar = string.Empty;
        protected virtual string QueryCancelar
        {
            set { queryCancelar = value; }
            get { return queryCancelar; }
        }
        private string queryBorrar = string.Empty;
        protected virtual string QueryBorrar
        {
            set { queryBorrar = value; }
            get { return queryBorrar; }
        }

        virtual public bool Grabar()
        {
            bool resultado = false;
            List<SqlParameter> parametros = new List<SqlParameter>();
            SqlParameter paramId = new SqlParameter();
            object objClass = this;
            if (objClass is CFDS)
            {
                paramId.ParameterName = "@P_CFDS_Id";
                paramId.Value = ((CFDS)objClass).CFDS_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = ((CFDS)objClass).RFC_Emisor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Emisor", Value = ((CFDS)objClass).Nombre_Emisor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Receptor", Value = ((CFDS)objClass).RFC_Receptor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Receptor", Value = ((CFDS)objClass).Nombre_Receptor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Serie", Value = ((CFDS)objClass).Serie });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Folio", Value = ((CFDS)objClass).Folio });

                if (((CFDS)objClass).Fecha > DateTime.MinValue)
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = ((CFDS)objClass).Fecha });

                parametros.Add(new SqlParameter() { ParameterName = "@P_Folio_Fiscal", Value = ((CFDS)objClass).Folio_Fiscal });

                if (((CFDS)objClass).Fecha > DateTime.MinValue)
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha_Fiscal", Value = ((CFDS)objClass).Fecha_Fiscal });

                parametros.Add(new SqlParameter() { ParameterName = "@P_Numero_Aprobacion", Value = ((CFDS)objClass).Numero_Aprobacion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Sello", Value = ((CFDS)objClass).Sello });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Certificado", Value = ((CFDS)objClass).Certificado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Comprobante", Value = ((CFDS)objClass).Tipo_Comprobante });
                parametros.Add(new SqlParameter() { ParameterName = "@P_SubTotal", Value = ((CFDS)objClass).SubTotal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento", Value = ((CFDS)objClass).Descuento });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = ((CFDS)objClass).Total });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Impuesto", Value = ((CFDS)objClass).Impuesto });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tasa", Value = ((CFDS)objClass).Tasa });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Importe_IVA", Value = ((CFDS)objClass).Importe_IVA });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Id", Value = ((CFDS)objClass).Tipo_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estatus", Value = ((CFDS)objClass).Estatus });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Comentario", Value = ((CFDS)objClass).Comentario });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = ((CFDS)objClass).Tipo_Movimiento_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Sucursal", Value = ((CFDS)objClass).IdSucursal.ToString() });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Flete", Value = ((CFDS)objClass).Flete });
            }
            else if (objClass is CFDS_Producto)
            {
                paramId.ParameterName = "@P_CFDS_Producto_Id";
                paramId.Value = ((CFDS_Producto)objClass).CFDS_Producto_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS_Producto)objClass).CFDS_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((CFDS_Producto)objClass).Cantidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Unidad", Value = ((CFDS_Producto)objClass).Unidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Numero_Identificacion", Value = ((CFDS_Producto)objClass).Numero_Identificacion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((CFDS_Producto)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor_Unitario", Value = ((CFDS_Producto)objClass).Valor_Unitario });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Importe", Value = ((CFDS_Producto)objClass).Importe });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((CFDS_Producto)objClass).Producto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad_Factura", Value = ((CFDS_Producto)objClass).Cantidad_Factura });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad_Empaque", Value = ((CFDS_Producto)objClass).Cantidad_Empaque });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento_Porciento", Value = ((CFDS_Producto)objClass).Descuento_Porciento });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento_Monto", Value = ((CFDS_Producto)objClass).Descuento_Monto });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Impuesto_Tasa", Value = ((CFDS_Producto)objClass).Impuesto_Tasa });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Impuesto_Monto", Value = ((CFDS_Producto)objClass).Impuesto_Monto });
            }
            /*else if (objClass is CFDS_Gasto)
            {
                paramId.ParameterName = "@P_CFDS_Gasto_Id";
                paramId.Value = ((CFDS_Gasto)objClass).CFDS_Gasto_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS_Gasto)objClass).CFDS_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Unidad", Value = ((CFDS_Gasto)objClass).Unidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Numero_Identificacion", Value = ((CFDS_Gasto)objClass).Numero_Identificacion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((CFDS_Gasto)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor_Unitario", Value = ((CFDS_Gasto)objClass).Valor_Unitario });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Importe", Value = ((CFDS_Gasto)objClass).Importe });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = ((CFDS_Gasto)objClass).Gasto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((CFDS_Gasto)objClass).Cantidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento_Porciento", Value = ((CFDS_Gasto)objClass).Descuento_Porciento });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento_Monto", Value = ((CFDS_Gasto)objClass).Descuento_Monto });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Impuesto_Tasa", Value = ((CFDS_Gasto)objClass).Impuesto_Tasa });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Impuesto_Monto", Value = ((CFDS_Gasto)objClass).Impuesto_Monto });
            }*/
            else if (objClass is CFDS_Archivo)
            {
                paramId.ParameterName = "@P_CFDS_Archivo_Id";
                paramId.Value = ((CFDS_Archivo)objClass).CFDS_Archivo_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS_Archivo)objClass).CFDS_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Archivo_Id", Value = ((CFDS_Archivo)objClass).Tipo_Archivo_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Archivo", Value = ((CFDS_Archivo)objClass).Nombre_Archivo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Archivo", Value = ((CFDS_Archivo)objClass).Archivo });
            }
            else if (objClass is Diccionario)
            {
                paramId.ParameterName = "@P_Diccionario_Id";
                paramId.Value = ((Diccionario)objClass).Diccionario_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((Diccionario)objClass).Producto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Provedor_Id", Value = ((Diccionario)objClass).Provedor_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor", Value = ((Diccionario)objClass).Valor });
            }
            /*else if (objClass is DiccionarioGastos)
            {
                paramId.ParameterName = "@P_Diccionario_Id";
                paramId.Value = ((DiccionarioGastos)objClass).Diccionario_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = ((DiccionarioGastos)objClass).Gasto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Provedor_Id", Value = ((DiccionarioGastos)objClass).Provedor_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor", Value = ((DiccionarioGastos)objClass).Valor });
            }*/
            else if (objClass is Movimiento_Detalle)
            {
                paramId.ParameterName = "@P_Movimiento_Detalle_Id";
                paramId.Value = ((Movimiento_Detalle)objClass).Movimiento_Detalle_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = ((Movimiento_Detalle)objClass).Movimiento_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((Movimiento_Detalle)objClass).Producto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((Movimiento_Detalle)objClass).Cantidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Renglon", Value = -1 });
            }
            else if (objClass is Movimiento_Gastos_Detalle)
            {
                paramId.ParameterName = "@P_Movimiento_Gasto_Detalle_Id";
                paramId.Value = ((Movimiento_Gastos_Detalle)objClass).Movimiento_Gasto_Detalle_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = ((Movimiento_Gastos_Detalle)objClass).Movimiento_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = ((Movimiento_Gastos_Detalle)objClass).Gasto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((Movimiento_Gastos_Detalle)objClass).Cantidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Renglon", Value = -1 });
            }
            else if (objClass is Movimientos)
            {
                paramId.ParameterName = "@P_Movimiento_Id";
                paramId.Value = ((Movimientos)objClass).Movimiento_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((Movimientos)objClass).CFDS_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha_Movimiento", Value = ((Movimientos)objClass).Fecha_Movimiento });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = ((Movimientos)objClass).Tipo_Movimiento_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Sucursal", Value = ((Movimientos)objClass).Sucursal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Flete", Value = ((Movimientos)objClass).Flete });
            }
            /*else if (objClass is Producto_Ubicacion)
            {
                paramId.ParameterName = "@P_Producto_Ubicacion_Id";
                paramId.Value = ((Producto_Ubicacion)objClass).Producto_Ubicacion_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((Producto_Ubicacion)objClass).Producto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Ubicacion", Value = ((Producto_Ubicacion)objClass).Ubicacion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estimado", Value = ((Producto_Ubicacion)objClass).Estimado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Picking", Value = ((Producto_Ubicacion)objClass).Picking });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Existencia", Value = ((Producto_Ubicacion)objClass).Existencia });
            }*/
            else if (objClass is Productos)
            {
                paramId.ParameterName = "@P_Producto_Id";
                paramId.Value = ((Productos)objClass).Producto_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((Productos)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Codigo_Producto", Value = ((Productos)objClass).Codigo_Producto.Replace("'", "").Replace("\"", "") });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Codigo_de_Barras", Value = ((Productos)objClass).Codigo_de_Barras.Replace("'", "").Replace("\"", "") });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Unidad_de_Medida", Value = ((Productos)objClass).Unidad_de_Medida });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad_Empaque", Value = ((Productos)objClass).Cantidad_Empaque });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdLinea1", Value = ((Productos)objClass).IdLinea1 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdLinea2", Value = ((Productos)objClass).IdLinea2 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdLinea3", Value = ((Productos)objClass).IdLinea3 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((Productos)objClass).Activo });
                if (!((Productos)objClass).TieneNumeroSerie && ((Productos)objClass).lstSeries.Count > 0)
                {
                    ((Productos)objClass).TieneNumeroSerie = true;
                }
                parametros.Add(new SqlParameter() { ParameterName = "@P_Serie", Value = ((Productos)objClass).TieneNumeroSerie });
            }
            /*else if (objClass is Gasto)
            {
                paramId.ParameterName = "@P_IdGastos";
                paramId.Value = ((Gasto)objClass).Gasto_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_IdProveedor", Value = ((Gasto)objClass).Proveedor_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_TipoDocumento", Value = ((Gasto)objClass).Tipo_Documento });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdSucursal", Value = ((Gasto)objClass).Sucursal_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdOrdenCompra", Value = ((Gasto)objClass).Orden_Compra_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaCompra", Value = ((Gasto)objClass).Fecha_Compra });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaFinal", Value = ((Gasto)objClass).Fecha_Final });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdDepartamento", Value = ((Gasto)objClass).Departamento_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaVencimiento", Value = ((Gasto)objClass).Fecha_Vencimiento });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = ((Gasto)objClass).Total });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((Gasto)objClass).Estado });
            }
            else if (objClass is GastoDetalle)
            {
                paramId.ParameterName = "@P_IdGastosDetalle";
                paramId.Value = ((GastoDetalle)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_IdGastos", Value = ((GastoDetalle)objClass).Gasto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdConceptoGastos", Value = ((GastoDetalle)objClass).GastoConcepto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = ((GastoDetalle)objClass).Total });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Unidad", Value = ((GastoDetalle)objClass).Unidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((GastoDetalle)objClass).Cantidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Pendiente", Value = ((GastoDetalle)objClass).Pendiente });
            }
            else if (objClass is GastoConcepto)
            {
                paramId.ParameterName = "@P_IdConceptoGasto";
                paramId.Value = ((GastoConcepto)objClass).GastoConcepto_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Concepto", Value = ((GastoConcepto)objClass).Concepto });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((GastoConcepto)objClass).Activo });
            }*/
            else if (objClass is ProductoDetalle)
            {
                paramId.ParameterName = "@P_Producto_Detalle_Id";
                paramId.Value = ((ProductoDetalle)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((ProductoDetalle)objClass).Producto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Proveedor_Id", Value = ((ProductoDetalle)objClass).Proveedor_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Marca", Value = ((ProductoDetalle)objClass).Marca });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Color", Value = ((ProductoDetalle)objClass).Color });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CostoProveedor", Value = ((ProductoDetalle)objClass).Costo_Proveedor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_PrecioGeneral", Value = ((ProductoDetalle)objClass).Precio_General });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CantidadMinima", Value = ((ProductoDetalle)objClass).Cantidad_Minima });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CantidadMaxima", Value = ((ProductoDetalle)objClass).Cantidad_Maxima });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Codigo_de_Barras", Value = ((ProductoDetalle)objClass).Codigo_de_Barras.Replace("'", "").Replace("\"", "") });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IVA", Value = ((ProductoDetalle)objClass).IVA == -1 ? 0 : ((ProductoDetalle)objClass).IVA });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IEPS", Value = ((ProductoDetalle)objClass).IEPS == -1 ? 0 : ((ProductoDetalle)objClass).IEPS });
                parametros.Add(new SqlParameter() { ParameterName = "@P_PrecioMayoreo", Value = ((ProductoDetalle)objClass).Precio_Mayoreo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CantidadMayoreo", Value = ((ProductoDetalle)objClass).Cantidad_Mayoreo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_PrecioPromocion", Value = ((ProductoDetalle)objClass).Precio_Compra });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((ProductoDetalle)objClass).Cantidad });
            }
            /*else if (objClass is Producto_Serie)
            {
                paramId.ParameterName = "@P_Producto_Serie_Id";
                paramId.Value = ((Producto_Serie)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((Producto_Serie)objClass).Producto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FolioFiscal", Value = ((Producto_Serie)objClass).Folio_Fiscal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Serie", Value = ((Producto_Serie)objClass).Numero_de_Serie });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaEntrada", Value = ((Producto_Serie)objClass).Fecha_de_Entrada });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaSalida", Value = ((Producto_Serie)objClass).Fecha_Salida });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((Producto_Serie)objClass).Status });
            }*/
            else if (objClass is Provedor)
            {
                paramId.ParameterName = "@P_Provedor_Id";
                paramId.Value = ((Provedor)objClass).Provedor_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = ((Provedor)objClass).Nombre });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RazonSocial", Value = ((Provedor)objClass).Razon_Social });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Localidad", Value = ((Provedor)objClass).Localidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Ciudad", Value = ((Provedor)objClass).Ciudad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = ((Provedor)objClass).RFC });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Calle", Value = ((Provedor)objClass).Calle });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumInt", Value = ((Provedor)objClass).NumInt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumExt", Value = ((Provedor)objClass).NumExt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Colonia", Value = ((Provedor)objClass).Colonia });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CodigoPostal", Value = ((Provedor)objClass).Codigo_Postal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((Provedor)objClass).Estado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Pais", Value = ((Provedor)objClass).Pais });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono1", Value = ((Provedor)objClass).Telefono });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono2", Value = ((Provedor)objClass).Telefono2 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono3", Value = ((Provedor)objClass).Telefono3 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email1", Value = ((Provedor)objClass).Email });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email2", Value = ((Provedor)objClass).Email2 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email3", Value = ((Provedor)objClass).Email3 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaAlta", Value = ((Provedor)objClass).FechaAlta });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CuentaContable", Value = ((Provedor)objClass).Cuenta_Contable });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((Provedor)objClass).Activo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Comentario", Value = ((Provedor)objClass).Comentario });
                parametros.Add(new SqlParameter() { ParameterName = "@P_DiasCredito", Value = ((Provedor)objClass).Dias_de_Credito });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Saldo", Value = ((Provedor)objClass).Saldo });
            }
            else if (objClass is Cliente)
            {
                paramId.ParameterName = "@P_Cliente_Id";
                paramId.Value = ((Cliente)objClass).Cliente_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = ((Cliente)objClass).Nombre });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ApePaterno", Value = ((Cliente)objClass).ApellidoPaterno });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ApeMaterno", Value = ((Cliente)objClass).ApellidoMaterno });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RazonSocial", Value = ((Cliente)objClass).Razon_Social });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Localidad", Value = ((Cliente)objClass).Localidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Ciudad", Value = ((Cliente)objClass).Ciudad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = ((Cliente)objClass).RFC });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Calle", Value = ((Cliente)objClass).Calle });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumInt", Value = ((Cliente)objClass).NumInt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumExt", Value = ((Cliente)objClass).NumExt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Colonia", Value = ((Cliente)objClass).Colonia });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CodigoPostal", Value = ((Cliente)objClass).Codigo_Postal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((Cliente)objClass).Estado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Pais", Value = ((Cliente)objClass).Pais });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono1", Value = ((Cliente)objClass).Telefono });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono2", Value = ((Cliente)objClass).Telefono2 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono3", Value = ((Cliente)objClass).Telefono3 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email1", Value = ((Cliente)objClass).Email });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email2", Value = ((Cliente)objClass).Email2 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email3", Value = ((Cliente)objClass).Email3 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaAlta", Value = ((Cliente)objClass).FechaAlta });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CuentaContable", Value = ((Cliente)objClass).Cuenta_Contable });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((Cliente)objClass).Activo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Comentario", Value = ((Cliente)objClass).Comentario });
                parametros.Add(new SqlParameter() { ParameterName = "@P_DiasCredito", Value = ((Cliente)objClass).Dias_de_Credito });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Saldo", Value = ((Cliente)objClass).Saldo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_MontoCredito", Value = ((Cliente)objClass).Monto_Credito });
            }
            else if (objClass is Tipo_Movimiento)
            {
                paramId.ParameterName = "@P_Tipo_Movimiento_Id";
                paramId.Value = ((Tipo_Movimiento)objClass).Tipo_Movimiento_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento", Value = ((Tipo_Movimiento)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Clave", Value = ((Tipo_Movimiento)objClass).Clave });
                parametros.Add(new SqlParameter() { ParameterName = "@P_EntradaSalida", Value = ((Tipo_Movimiento)objClass).EntradaSalida });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Activo", Value = ((Tipo_Movimiento)objClass).Activo });
            }
            else if (objClass is Empleado)
            {
                paramId.ParameterName = "@P_IdEmpleado";
                paramId.Value = ((Empleado)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = ((Empleado)objClass).Nombre });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ApePaterno", Value = ((Empleado)objClass).ApellidoPaterno });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ApeMaterno", Value = ((Empleado)objClass).ApellidoMaterno });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaNacimiento", Value = ((Empleado)objClass).FechaNacimiento });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NSS", Value = ((Empleado)objClass).NSS });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Localidad", Value = ((Empleado)objClass).Localidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Ciudad", Value = ((Empleado)objClass).Ciudad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CURP", Value = ((Empleado)objClass).Curp });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = ((Empleado)objClass).RFC });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Calle", Value = ((Empleado)objClass).Calle });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumInt", Value = ((Empleado)objClass).NumInt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumExt", Value = ((Empleado)objClass).NumExt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Colonia", Value = ((Empleado)objClass).Colonia });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CodigoPostal", Value = ((Empleado)objClass).CodigoPostal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((Empleado)objClass).Estado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Pais", Value = ((Empleado)objClass).Pais });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono1", Value = ((Empleado)objClass).Telefono });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono2", Value = ((Empleado)objClass).Telefono2 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email1", Value = ((Empleado)objClass).Email });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email2", Value = ((Empleado)objClass).Email2 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaAlta", Value = ((Empleado)objClass).FechaAlta });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdHuella", Value = ((Empleado)objClass).IdHuella });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((Empleado)objClass).Activo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Usuario", Value = ((Empleado)objClass).Usuario });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Password", Value = ((Empleado)objClass).Password });
            }
            /*else if (objClass is Sucursales)
            {
                paramId.ParameterName = "@P_IdSucursal";
                paramId.Value = ((Sucursales)objClass).IdSucursal;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = ((Sucursales)objClass).Nombre });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Localidad", Value = ((Sucursales)objClass).Localidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Ciudad", Value = ((Sucursales)objClass).Ciudad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Calle", Value = ((Sucursales)objClass).Calle });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumInt", Value = ((Sucursales)objClass).NumInt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumExt", Value = ((Sucursales)objClass).NumExt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Colonia", Value = ((Sucursales)objClass).Colonia });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CodigoPostal", Value = ((Sucursales)objClass).CodigoPostal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((Sucursales)objClass).Estado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Pais", Value = ((Sucursales)objClass).Pais });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono", Value = ((Sucursales)objClass).Telefono });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo", Value = ((Sucursales)objClass).Tipo });
            }
            else if (objClass is Huella)
            {
                paramId.ParameterName = "@P_IdHuella";
                paramId.Value = ((Huella)objClass).IdHuella;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Huella)objClass).IdEmpleado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_dedo", Value = ((Huella)objClass).Dedo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Huella64", Value = ((Huella)objClass).Huella_64 });
            }*/
            else if (objClass is Usuario)
            {
                paramId.ParameterName = "@P_IdUsuario";
                paramId.Value = ((Usuario)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Usuario)objClass).IdEmpleado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = ((Usuario)objClass).Nombre });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Contraseña", Value = ((Usuario)objClass).Contraseña });
            }
            else if (objClass is Preferencias)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((Preferencias)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_ImpresoraTickets", Value = ((Preferencias)objClass).ImpresoraTickets });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ImpresoraFacturas", Value = ((Preferencias)objClass).ImpresoraFacturas });
            }
            else if (objClass is CertificadoDigital)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((CertificadoDigital)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_RutaCertificado", Value = ((CertificadoDigital)objClass).RutaCertificado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RutaClave", Value = ((CertificadoDigital)objClass).RutaClave });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Password", Value = ((CertificadoDigital)objClass).Password });
            }
            else if (objClass is DatosFacturacion)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((DatosFacturacion)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_NombreFiscal", Value = ((DatosFacturacion)objClass).Razon_Social });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Localidad", Value = ((DatosFacturacion)objClass).Localidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Municipio", Value = ((DatosFacturacion)objClass).Municipio });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = ((DatosFacturacion)objClass).RFC });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Calle", Value = ((DatosFacturacion)objClass).Calle });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumInt", Value = ((DatosFacturacion)objClass).NumInt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumExt", Value = ((DatosFacturacion)objClass).NumExt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Colonia", Value = ((DatosFacturacion)objClass).Colonia });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CodigoPostal", Value = ((DatosFacturacion)objClass).CodigoPostal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((DatosFacturacion)objClass).Estado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Pais", Value = ((DatosFacturacion)objClass).Pais });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono", Value = ((DatosFacturacion)objClass).Telefono });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Regimen", Value = ((DatosFacturacion)objClass).Regimen });
            }
            else if (objClass is UbicacionFiscal)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((UbicacionFiscal)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Localidad", Value = ((UbicacionFiscal)objClass).Localidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Municipio", Value = ((UbicacionFiscal)objClass).Municipio });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Calle", Value = ((UbicacionFiscal)objClass).Calle });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumInt", Value = ((UbicacionFiscal)objClass).NumInt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NumExt", Value = ((UbicacionFiscal)objClass).NumExt });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Colonia", Value = ((UbicacionFiscal)objClass).Colonia });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CodigoPostal", Value = ((UbicacionFiscal)objClass).CodigoPostal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((UbicacionFiscal)objClass).Estado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Pais", Value = ((UbicacionFiscal)objClass).Pais });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Telefono", Value = ((UbicacionFiscal)objClass).Telefono });
            }
            /*else if (objClass is FileDropbox)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((FileDropbox)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Cursor", Value = ((FileDropbox)objClass).Cursor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((FileDropbox)objClass).Estado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdFolder", Value = ((FileDropbox)objClass).IdFolder });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Name", Value = ((FileDropbox)objClass).Nombre });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Path", Value = ((FileDropbox)objClass).Path });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Rev", Value = ((FileDropbox)objClass).Rev });
            }
            else if (objClass is ImageDropbox)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((ImageDropbox)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Imagen_64", Value = ((ImageDropbox)objClass).Imagen_64 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((ImageDropbox)objClass).Estado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ProductoId", Value = ((ImageDropbox)objClass).ProductoId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdImagen", Value = ((ImageDropbox)objClass).IdImagen });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Name", Value = ((ImageDropbox)objClass).Nombre });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Path", Value = ((ImageDropbox)objClass).Path });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Rev", Value = ((ImageDropbox)objClass).Rev });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo", Value = ((ImageDropbox)objClass).Tipo });
            }
            else if (objClass is CatalogoLinea1)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((CatalogoLinea1)objClass).Catalogo_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((CatalogoLinea1)objClass).Activo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((CatalogoLinea1)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor", Value = ((CatalogoLinea1)objClass).Nombre_Departamento });
            }
            else if (objClass is CatalogoLinea2)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((CatalogoLinea2)objClass).Catalogo_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((CatalogoLinea2)objClass).Activo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdCatalogo1", Value = ((CatalogoLinea2)objClass).Catalogo_Linea1_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((CatalogoLinea2)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor", Value = ((CatalogoLinea2)objClass).Nombre_Modelo });
            }
            else if (objClass is CatalogoLinea3)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((CatalogoLinea3)objClass).Catalogo_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((CatalogoLinea3)objClass).Activo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((CatalogoLinea3)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor", Value = ((CatalogoLinea3)objClass).Nombre_Marca });
            }*/
            else if (objClass is IVA)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((IVA)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((IVA)objClass).Activo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor", Value = ((IVA)objClass).Porcentaje });
            }
            else if (objClass is IEPS)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((IEPS)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((IEPS)objClass).Activo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor", Value = ((IEPS)objClass).Porcentaje });
            }
            else if (objClass is CatalogoFacturaMetodoPago)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((CatalogoFacturaMetodoPago)objClass).Catalogo_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((CatalogoFacturaMetodoPago)objClass).Activo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClaveSat", Value = ((CatalogoFacturaMetodoPago)objClass).Clave_Sat });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = ((CatalogoFacturaMetodoPago)objClass).Nombre });
            }
            else if (objClass is Compra)
            {
                paramId.ParameterName = "@P_Compra_Id";
                paramId.Value = ((Compra)objClass).Compra_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = ((Compra)objClass).RFC_Emisor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Emisor", Value = ((Compra)objClass).Nombre_Emisor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Serie", Value = ((Compra)objClass).Serie });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Folio", Value = ((Compra)objClass).Folio });

                if (((Compra)objClass).Fecha > DateTime.MinValue)
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = ((Compra)objClass).Fecha });

                parametros.Add(new SqlParameter() { ParameterName = "@P_SubTotal", Value = ((Compra)objClass).SubTotal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento", Value = ((Compra)objClass).Descuento });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = ((Compra)objClass).Total });
                parametros.Add(new SqlParameter() { ParameterName = "@P_TasaIVA", Value = ((Compra)objClass).IVA_Tasa });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Importe_IVA", Value = ((Compra)objClass).Importe_IVA });
                parametros.Add(new SqlParameter() { ParameterName = "@P_TasaIEPS", Value = ((Compra)objClass).IEPS_Tasa });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Importe_IEPS", Value = ((Compra)objClass).Importe_IEPS });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estatus", Value = ((Compra)objClass).Estatus });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = ((Compra)objClass).Tipo_Movimiento_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Sucursal", Value = ((Compra)objClass).IdSucursal.ToString() });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdDatosFiscales", Value = ((Compra)objClass).IdDatosFiscales });
            }
            else if (objClass is ProductoCompra)
            {
                paramId.ParameterName = "@P_Compra_Producto_Id";
                paramId.Value = ((ProductoCompra)objClass).Producto_Compra_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Compra_Id", Value = ((ProductoCompra)objClass).Compra_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((ProductoCompra)objClass).Cantidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Unidad", Value = ((ProductoCompra)objClass).Unidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((ProductoCompra)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Valor_Unitario", Value = ((ProductoCompra)objClass).Valor_Unitario });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Importe", Value = ((ProductoCompra)objClass).Importe });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((ProductoCompra)objClass).Producto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad_Factura", Value = ((ProductoCompra)objClass).Cantidad_Compra });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad_Empaque", Value = ((ProductoCompra)objClass).Cantidad_Empaque });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento_Porciento", Value = ((ProductoCompra)objClass).Descuento_Porciento });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento_Monto", Value = ((ProductoCompra)objClass).Descuento_Monto });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Impuesto_Tasa", Value = ((ProductoCompra)objClass).IVA_Tasa });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Impuesto_Monto", Value = ((ProductoCompra)objClass).IVA_Monto });
            }
            /*else if (objClass is Obsequio)
            {
                paramId.ParameterName = "@P_Obsequio_Id";
                paramId.Value = ((Obsequio)objClass).Obsequio_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = ((Obsequio)objClass).RFC_Emisor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Emisor", Value = ((Obsequio)objClass).Nombre_Emisor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Serie", Value = ((Obsequio)objClass).Serie });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Folio", Value = ((Obsequio)objClass).Folio });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = ((Obsequio)objClass).Fecha });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estatus", Value = ((Obsequio)objClass).Estatus });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = ((Obsequio)objClass).Tipo_Movimiento_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Sucursal", Value = ((Obsequio)objClass).IdSucursal.ToString() });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdDatosFiscales", Value = ((Obsequio)objClass).IdDatosFiscales });
            }
            else if (objClass is ObsequioDetalle)
            {
                paramId.ParameterName = "@P_Obsequio_Detalle_Id";
                paramId.Value = ((ObsequioDetalle)objClass).Obsequio_Detalle_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Obsequio_Id", Value = ((ObsequioDetalle)objClass).Obsequio_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((ObsequioDetalle)objClass).Cantidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Unidad", Value = ((ObsequioDetalle)objClass).Unidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((ObsequioDetalle)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((ObsequioDetalle)objClass).ProductoId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad_Factura", Value = ((ObsequioDetalle)objClass).Cantidad_Factura });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad_Empaque", Value = ((ObsequioDetalle)objClass).Cantidad_Empaque });
            }
            else if (objClass is GastoManual)
            {
                paramId.ParameterName = "@P_Gasto_Manual_Id";
                paramId.Value = ((GastoManual)objClass).Gasto_Manual_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = ((GastoManual)objClass).Gasto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = ((GastoManual)objClass).RFC_Emisor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre_Emisor", Value = ((GastoManual)objClass).Nombre_Emisor });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Folio", Value = ((GastoManual)objClass).Folio });

                if (((GastoManual)objClass).Fecha > DateTime.MinValue)
                    parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = ((GastoManual)objClass).Fecha });

                parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = ((GastoManual)objClass).Total });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estatus", Value = ((GastoManual)objClass).Estatus });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = ((GastoManual)objClass).Tipo_Movimiento_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_SucursalId", Value = ((GastoManual)objClass).IdSucursal });
            }
            else if (objClass is OrdenGasto)
            {
                paramId.ParameterName = "@P_Orden_Gasto_Id";
                paramId.Value = ((OrdenGasto)objClass).Orden_Gasto_Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_IdProveedor", Value = ((OrdenGasto)objClass).ProveedorId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdSucursal", Value = ((OrdenGasto)objClass).IdSucursal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_SubTotal", Value = ((OrdenGasto)objClass).SubTotal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = ((OrdenGasto)objClass).Total });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaOrdenGasto", Value = ((OrdenGasto)objClass).Fecha });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaFinal", Value = ((OrdenGasto)objClass).Fecha });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IVA", Value = ((OrdenGasto)objClass).IVA });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IEPS", Value = ((OrdenGasto)objClass).IEPS });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estatus", Value = ((OrdenGasto)objClass).Estatus });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((OrdenGasto)objClass).Empleado_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_NombreEmpleadosolicita", Value = ((OrdenGasto)objClass).Empleado_Solicita });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Comentario", Value = ((OrdenGasto)objClass).Comentario });
            }
            else if (objClass is OrdenGastoDetalle)
            {
                paramId.ParameterName = "@P_Orden_Gasto_Detalle_Id";
                paramId.Value = ((OrdenGastoDetalle)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Orden_Gasto_Id", Value = ((OrdenGastoDetalle)objClass).Orden_Gasto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdGastoConcepto", Value = ((OrdenGastoDetalle)objClass).GastoConcepto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((OrdenGastoDetalle)objClass).Cantidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Precio", Value = ((OrdenGastoDetalle)objClass).Precio });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IvaImporte", Value = ((OrdenGastoDetalle)objClass).IVA_Importe });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IepsImporte", Value = ((OrdenGastoDetalle)objClass).IEPS_Importe });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Unidad", Value = ((OrdenGastoDetalle)objClass).Unidad });
            }*/
            else if (objClass is Tokens)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_AccessToken", Value = ((Tokens)objClass).AccessToken });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RefreshToken", Value = ((Tokens)objClass).RefreshToken });
            }
            else if (objClass is MetodoPago)
            {
                paramId.ParameterName = "@P_Id";
                paramId.Value = ((MetodoPago)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Metodo", Value = ((MetodoPago)objClass).Metodo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Clave", Value = ((MetodoPago)objClass).Clave });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((MetodoPago)objClass).Activo });
            }
            else if (objClass is ClientesCargos)
            {
                paramId.ParameterName = "@P_ClienteCargo_Id";
                paramId.Value = ((ClientesCargos)objClass).IdClienteCargo;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Cliente_Id", Value = ((ClientesCargos)objClass).IdCliente });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Venta_Id", Value = ((ClientesCargos)objClass).IdVenta });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = ((ClientesCargos)objClass).Fecha });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Concepto", Value = ((ClientesCargos)objClass).Concepto });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((ClientesCargos)objClass).Estado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = ((ClientesCargos)objClass).Saldo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cargos", Value = ((ClientesCargos)objClass).Cargos });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Abonos", Value = ((ClientesCargos)objClass).Abonos });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdAbono", Value = ((ClientesCargos)objClass).IdAbono });
                parametros.Add(new SqlParameter() { ParameterName = "@P_TipoCargo", Value = ((ClientesCargos)objClass).Tipo_Cargo });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((ClientesCargos)objClass).Activo });
            }
            /*else if (objClass is Promocion)
            {
                paramId.ParameterName = "@P_Promocion_Id";
                paramId.Value = ((Promocion)objClass).Id_Promocion;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((Promocion)objClass).Id_Producto });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((Promocion)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Precio", Value = ((Promocion)objClass).Precio_Compra });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaInicio", Value = ((Promocion)objClass).Fecha_Inicio });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaFin", Value = ((Promocion)objClass).Fecha_Fin });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((Promocion)objClass).Activa });
            }*/
            else if (objClass is FacturaDigital)
            {
                paramId.ParameterName = "@P_FacturaId";
                paramId.Value = ((FacturaDigital)objClass).FacturaId;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_UUID", Value = ((FacturaDigital)objClass).UUID });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((FacturaDigital)objClass).Activa });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = ((FacturaDigital)objClass).Fecha });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdDatosFiscales", Value = ((FacturaDigital)objClass).IdDatosFiscales });
            }
            else if (objClass is FacturaVenta)
            {
                paramId.ParameterName = "@P_FacturaVentaId";
                paramId.Value = ((FacturaVenta)objClass).Id;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_FacturaId", Value = ((FacturaVenta)objClass).FacturaId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_VentaId", Value = ((FacturaVenta)objClass).VentaId });
            }
            else if (objClass is FacturaProductos)
            {
                paramId.ParameterName = "@P_FacturaProductoId";
                paramId.Value = ((FacturaProductos)objClass).FacturaId;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_FacturaId", Value = ((FacturaProductos)objClass).FacturaId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ProductoId", Value = ((FacturaProductos)objClass).ProductoId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = ((FacturaProductos)objClass).Cantidad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_UnidadMedida", Value = ((FacturaProductos)objClass).UnidadMedida });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Descripcion", Value = ((FacturaProductos)objClass).Descripcion });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Precio", Value = ((FacturaProductos)objClass).Precio });
                parametros.Add(new SqlParameter() { ParameterName = "@P_TotaL", Value = ((FacturaProductos)objClass).Total });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IVA", Value = ((FacturaProductos)objClass).IVAPorcentaje });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IEPS", Value = ((FacturaProductos)objClass).IEPsPorcentaje });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IVAImporte", Value = ((FacturaProductos)objClass).IVA });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IEPSImporte", Value = ((FacturaProductos)objClass).IEPS });
            }
            else if (objClass is FacturaCFDI)
            {
                paramId.ParameterName = "@P_FacturaCFDIId";
                paramId.Value = ((FacturaCFDI)objClass).FacturaId;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_FacturaId", Value = ((FacturaCFDI)objClass).FacturaId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_XMLString", Value = ((FacturaCFDI)objClass).XML });
                parametros.Add(new SqlParameter() { ParameterName = "@P_XMLPath", Value = ((FacturaCFDI)objClass).RutaXml });
                parametros.Add(new SqlParameter() { ParameterName = "@P_PDFString", Value = ((FacturaCFDI)objClass).PDF });
                parametros.Add(new SqlParameter() { ParameterName = "@P_PDFPath", Value = ((FacturaCFDI)objClass).RutaPdf });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteId", Value = ((FacturaCFDI)objClass).ClienteId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteNombreFiscal", Value = ((FacturaCFDI)objClass).ClienteNombreFiscal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteCalle", Value = ((FacturaCFDI)objClass).ClienteCalle });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteNumeroExterior", Value = ((FacturaCFDI)objClass).ClienteNumeroExterior });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteNumeroInterior", Value = ((FacturaCFDI)objClass).ClienteNumeroInterior });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteColonia", Value = ((FacturaCFDI)objClass).ClienteColonia });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteCodigoPostal", Value = ((FacturaCFDI)objClass).ClienteCodigoPostal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteCiudad", Value = ((FacturaCFDI)objClass).ClienteCiudad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteEstado", Value = ((FacturaCFDI)objClass).ClienteEstado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteRFC", Value = ((FacturaCFDI)objClass).ClienteRFC });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email", Value = ((FacturaCFDI)objClass).Email });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = ((FacturaCFDI)objClass).Total });
                parametros.Add(new SqlParameter() { ParameterName = "@P_SubTotal", Value = ((FacturaCFDI)objClass).SubTotal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IVA", Value = ((FacturaCFDI)objClass).IvaPorcentaje });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IEPS", Value = ((FacturaCFDI)objClass).IepsPorcentaje });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IVAImporte", Value = ((FacturaCFDI)objClass).IvaImporte });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IEPSImporte", Value = ((FacturaCFDI)objClass).IepsImporte });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaTimbrado", Value = ((FacturaCFDI)objClass).Fecha_Timbrado });
            }
            else if (objClass is FacturaNotaCredito)
            {
                paramId.ParameterName = "@P_FacturaNotaCreditoId";
                paramId.Value = ((FacturaNotaCredito)objClass).FacturaNotaCreditoId;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_FacturaId", Value = ((FacturaNotaCredito)objClass).FacturaId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_UUID", Value = ((FacturaNotaCredito)objClass).UUID });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Status", Value = ((FacturaNotaCredito)objClass).Activa });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Fecha", Value = ((FacturaNotaCredito)objClass).Fecha });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdDatosFiscales", Value = ((FacturaNotaCredito)objClass).IdDatosFiscales });
            }
            else if (objClass is FacturaNotaCreditoCFDI)
            {
                paramId.ParameterName = "@P_FacturaCFDIId";
                paramId.Value = ((FacturaNotaCreditoCFDI)objClass).FacturaId;
                paramId.Direction = System.Data.ParameterDirection.InputOutput;
                parametros.Add(paramId);

                parametros.Add(new SqlParameter() { ParameterName = "@P_FacturaId", Value = ((FacturaNotaCreditoCFDI)objClass).FacturaId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_XMLString", Value = ((FacturaNotaCreditoCFDI)objClass).XML });
                parametros.Add(new SqlParameter() { ParameterName = "@P_XMLPath", Value = ((FacturaNotaCreditoCFDI)objClass).RutaXml });
                parametros.Add(new SqlParameter() { ParameterName = "@P_PDFString", Value = ((FacturaNotaCreditoCFDI)objClass).PDF });
                parametros.Add(new SqlParameter() { ParameterName = "@P_PDFPath", Value = ((FacturaNotaCreditoCFDI)objClass).RutaPdf });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteId", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteId });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteNombreFiscal", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteNombreFiscal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteCalle", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteCalle });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteNumeroExterior", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteNumeroExterior });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteNumeroInterior", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteNumeroInterior });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteColonia", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteColonia });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteCodigoPostal", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteCodigoPostal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteCiudad", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteCiudad });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteEstado", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteEstado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_ClienteRFC", Value = ((FacturaNotaCreditoCFDI)objClass).ClienteRFC });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Email", Value = ((FacturaNotaCreditoCFDI)objClass).Email });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Total", Value = ((FacturaNotaCreditoCFDI)objClass).Total });
                parametros.Add(new SqlParameter() { ParameterName = "@P_SubTotal", Value = ((FacturaNotaCreditoCFDI)objClass).SubTotal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IVA", Value = ((FacturaNotaCreditoCFDI)objClass).IvaPorcentaje });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IEPS", Value = ((FacturaNotaCreditoCFDI)objClass).IepsPorcentaje });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IVAImporte", Value = ((FacturaNotaCreditoCFDI)objClass).IvaImporte });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IEPSImporte", Value = ((FacturaNotaCreditoCFDI)objClass).IepsImporte });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaTimbrado", Value = ((FacturaNotaCreditoCFDI)objClass).Fecha_Timbrado });
            }
            resultado = (BaseDatos.ejecutarProcedimiento(((ClaseBase)objClass).QueryGrabar, parametros) > 0);
            if (resultado)
            {
                ExtensionCargar(objClass, paramId, resultado);
            }
            return resultado;
        }

        private void ExtensionCargar(object objClass, SqlParameter paramId, bool resultado)
        {
            if (objClass is CFDS)
            {
                if (resultado && ((CFDS)objClass).CFDS_Id == -1)
                {
                    ((CFDS)objClass).CFDS_Id = Convert.ToInt64(paramId.Value);
                }

                foreach (CFDS_Archivo archivo in ((CFDS)objClass).Archivos)
                    archivo.CFDS_Id = ((CFDS)objClass).CFDS_Id;

                foreach (CFDS_Producto producto in ((CFDS)objClass).Productos)
                    producto.CFDS_Id = ((CFDS)objClass).CFDS_Id;

                if (resultado)
                {
                    ((CFDS)objClass).GuardarArchivos();
                    if ((TIPO_FACTURA)((CFDS)objClass).Tipo_Id != TIPO_FACTURA.GASTOS)
                    {
                        ((CFDS)objClass).GuardarProductos();
                    }
                    else
                    {
                        //((CFDS)objClass).GuardarGastos();
                    }
                }
            }
            else if (objClass is CFDS_Producto)
            {
                if (resultado && ((CFDS_Producto)objClass).CFDS_Producto_Id == -1)
                    ((CFDS_Producto)objClass).CFDS_Producto_Id = Convert.ToInt64(paramId.Value);
            }
            /*else if (objClass is CFDS_Gasto)
            {
                if (resultado && ((CFDS_Gasto)objClass).CFDS_Gasto_Id == -1)
                    ((CFDS_Gasto)objClass).CFDS_Gasto_Id = Convert.ToInt64(paramId.Value);
            }*/
            else if (objClass is CFDS_Archivo)
            {
                if (resultado && ((CFDS_Archivo)objClass).CFDS_Archivo_Id == -1)
                    ((CFDS_Archivo)objClass).CFDS_Archivo_Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is Diccionario)
            {
                if (resultado && ((Diccionario)objClass).Diccionario_Id == -1)
                    ((Diccionario)objClass).Diccionario_Id = Convert.ToInt64(paramId.Value);
            }
            /*else if (objClass is DiccionarioGastos)
            {
                if (resultado && ((DiccionarioGastos)objClass).Diccionario_Id == -1)
                    ((DiccionarioGastos)objClass).Diccionario_Id = Convert.ToInt64(paramId.Value);
            }*/
            else if (objClass is Movimiento_Detalle)
            {
                if (resultado && ((Movimiento_Detalle)objClass).Movimiento_Detalle_Id == -1)
                    ((Movimiento_Detalle)objClass).Movimiento_Detalle_Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is Movimiento_Gastos_Detalle)
            {
                if (resultado && ((Movimiento_Gastos_Detalle)objClass).Movimiento_Gasto_Detalle_Id == -1)
                    ((Movimiento_Gastos_Detalle)objClass).Movimiento_Gasto_Detalle_Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is Movimientos)
            {
                if (resultado && ((Movimientos)objClass).Movimiento_Id == -1)
                    ((Movimientos)objClass).Movimiento_Id = Convert.ToInt64(paramId.Value);

                if (((Movimientos)objClass).Detalles.Count > 0)
                {
                    foreach (Movimiento_Detalle detalle in ((Movimientos)objClass).Detalles)
                        detalle.Movimiento_Id = ((Movimientos)objClass).Movimiento_Id;
                }
                if (((Movimientos)objClass).DetallesGastos.Count > 0)
                {
                    foreach (Movimiento_Gastos_Detalle detalle in ((Movimientos)objClass).DetallesGastos)
                        detalle.Movimiento_Id = ((Movimientos)objClass).Movimiento_Id;
                }
                if (resultado)
                {
                    if (((Movimientos)objClass).Detalles.Count > 0)
                    {
                        if (!((Movimientos)objClass).GuardarDetalle())
                            resultado = false;
                    }
                    if (((Movimientos)objClass).DetallesGastos.Count > 0)
                    {
                        if (!((Movimientos)objClass).GuardarDetalleGastos())
                            resultado = false;
                    }
                }
            }
            /*else if (objClass is Producto_Ubicacion)
            {
                if (resultado && ((Producto_Ubicacion)objClass).Producto_Ubicacion_Id == -1)
                    ((Producto_Ubicacion)objClass).Producto_Ubicacion_Id = Convert.ToInt64(paramId.Value);
            }*/
            else if (objClass is Productos)
            {
                if (resultado && ((Productos)objClass).Producto_Id == -1)
                    ((Productos)objClass).Producto_Id = Convert.ToInt64(paramId.Value);
                if (resultado && ((Productos)objClass).Producto_Id > 0)
                {
                    ((Productos)objClass).Detalle.Producto_Id = ((Productos)objClass).Producto_Id;
                    ((Productos)objClass).Detalle.Grabar();
                    if (((Productos)objClass).lstSeries.Count > 0)
                    {
                        /*Producto_Serie serie = null;
                        foreach (string s in ((Productos)objClass).lstSeries)
                        {
                            serie = new Producto_Serie(((Productos)objClass).Producto_Id);
                            serie.Fecha_de_Entrada = DateTime.Now;
                            serie.Numero_de_Serie = s;
                            serie.Producto = ((Productos)objClass).Descripcion;
                            serie.Status = "VIGENTE";
                            serie.Folio_Fiscal = ((Productos)objClass).FolioFiscal;
                            serie.Grabar();
                            serie = null;
                        }*/
                    }
                    /*if (((Productos)objClass).Ubicaciones != null && ((Productos)objClass).Ubicaciones.Count > 0)
                    {
                        foreach (Producto_Ubicacion ubicacion in ((Productos)objClass).Ubicaciones)
                            ubicacion.Producto_Id = ((Productos)objClass).Producto_Id;

                        ((Productos)objClass).GuardarUbicacion();
                    }*/
                }
            }
            /*else if (objClass is Gasto)
            {
                if (resultado && ((Gasto)objClass).Gasto_Id == -1)
                    ((Gasto)objClass).Gasto_Id = Convert.ToInt64(paramId.Value);
                if (resultado)
                {
                    if (((Gasto)objClass).Detalles.Count > 0)
                    {
                        foreach (GastoDetalle detalle in ((Gasto)objClass).Detalles)
                        {
                            detalle.Gasto_Id = ((Gasto)objClass).Gasto_Id;
                            detalle.Grabar();
                        }
                    }
                }
            }
            else if (objClass is GastoConcepto)
            {
                if (resultado && ((GastoConcepto)objClass).GastoConcepto_Id == -1)
                    ((GastoConcepto)objClass).GastoConcepto_Id = Convert.ToInt64(paramId.Value);
            }*/
            else if (objClass is Provedor)
            {
                if (resultado && !(paramId.Value is DBNull) && ((Provedor)objClass).Provedor_Id == -1)
                    ((Provedor)objClass).Provedor_Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is Cliente)
            {
                if (resultado && !(paramId.Value is DBNull) && ((Cliente)objClass).Cliente_Id == -1)
                    ((Cliente)objClass).Cliente_Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is Tipo_Movimiento)
            {
                if (resultado && ((Tipo_Movimiento)objClass).Tipo_Movimiento_Id == -1)
                    ((Tipo_Movimiento)objClass).Tipo_Movimiento_Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is Empleado)
            {
                if (resultado && !(paramId.Value is DBNull) && ((Empleado)objClass).Id == -1)
                    ((Empleado)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            /*else if (objClass is Sucursales)
            {
                if (resultado && ((Sucursales)objClass).IdSucursal == -1)
                    ((Sucursales)objClass).IdSucursal = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is Huella)
            {
                if (resultado && ((Huella)objClass).IdHuella == -1)
                    ((Huella)objClass).IdHuella = Convert.ToInt64(paramId.Value);
            }*/
            else if (objClass is Usuario)
            {
                if (resultado && ((Usuario)objClass).Id == -1)
                    ((Usuario)objClass).IdHuella = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is Preferencias)
            {
                if (resultado && ((Preferencias)objClass).Id == -1)
                    ((Preferencias)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            /*else if (objClass is ImageDropbox)
            {
                if (resultado && ((ImageDropbox)objClass).Id == -1)
                    ((ImageDropbox)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is FileDropbox)
            {
                if (resultado && ((FileDropbox)objClass).Id == -1)
                    ((FileDropbox)objClass).Id = Convert.ToInt64(paramId.Value);
            }*/
            else if (objClass is CatalogoFacturaMetodoPago)
            {
                if (resultado && ((CatalogoFacturaMetodoPago)objClass).Catalogo_Id == -1)
                    ((CatalogoFacturaMetodoPago)objClass).Catalogo_Id = Convert.ToInt64(paramId.Value);
            }
            /*else if (objClass is CatalogoLinea1)
            {
                if (resultado && ((CatalogoLinea1)objClass).Catalogo_Id == -1)
                    ((CatalogoLinea1)objClass).Catalogo_Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is CatalogoLinea2)
            {
                if (resultado && ((CatalogoLinea2)objClass).Catalogo_Id == -1)
                    ((CatalogoLinea2)objClass).Catalogo_Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is CatalogoLinea3)
            {
                if (resultado && ((CatalogoLinea3)objClass).Catalogo_Id == -1)
                    ((CatalogoLinea3)objClass).Catalogo_Id = Convert.ToInt64(paramId.Value);
            }*/
            else if (objClass is IVA)
            {
                if (resultado && ((IVA)objClass).Id == -1)
                    ((IVA)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is IEPS)
            {
                if (resultado && ((IEPS)objClass).Id == -1)
                    ((IEPS)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is Compra)
            {
                if (resultado && ((Compra)objClass).Compra_Id == -1)
                    ((Compra)objClass).Compra_Id = Convert.ToInt64(paramId.Value);
                if (resultado)
                {
                    foreach (ProductoCompra producto in ((Compra)objClass).Productos)
                    {
                        producto.Compra_Id = ((Compra)objClass).Compra_Id;
                    }
                    ((Compra)objClass).GuardarProductos();
                }
            }
            else if (objClass is ProductoCompra)
            {
                if (resultado && ((ProductoCompra)objClass).Producto_Compra_Id == -1)
                    ((ProductoCompra)objClass).Producto_Compra_Id = Convert.ToInt64(paramId.Value);
            }
            /*else if (objClass is Obsequio)
            {
                if (resultado && ((Obsequio)objClass).Obsequio_Id == -1)
                    ((Obsequio)objClass).Obsequio_Id = Convert.ToInt64(paramId.Value);
                if (resultado)
                {
                    foreach (ObsequioDetalle producto in ((Obsequio)objClass).Productos)
                    {
                        producto.Obsequio_Id = ((Obsequio)objClass).Obsequio_Id;
                    }
                    ((Obsequio)objClass).GuardarProductos();
                }
            }
            else if (objClass is GastoManual)
            {
                if (resultado && ((GastoManual)objClass).Gasto_Manual_Id == -1)
                    ((GastoManual)objClass).Gasto_Manual_Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is OrdenGasto)
            {
                if (resultado && ((OrdenGasto)objClass).Orden_Gasto_Id == -1)
                    ((OrdenGasto)objClass).Orden_Gasto_Id = Convert.ToInt64(paramId.Value);
                if (resultado)
                {
                    if (((OrdenGasto)objClass).Detalles.Count > 0)
                    {
                        foreach (OrdenGastoDetalle detalle in ((OrdenGasto)objClass).Detalles)
                        {
                            detalle.Orden_Gasto_Id = ((OrdenGasto)objClass).Orden_Gasto_Id;
                            detalle.Grabar();
                        }
                    }
                }
            }*/
            else if (objClass is MetodoPago)
            {
                if (resultado && ((MetodoPago)objClass).Id == -1)
                    ((MetodoPago)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is ClientesCargos)
            {
                if (resultado && ((ClientesCargos)objClass).IdClienteCargo == -1)
                    ((ClientesCargos)objClass).IdClienteCargo = Convert.ToInt64(paramId.Value);
            }
            /*else if (objClass is Promocion)
            {
                if (resultado && ((Promocion)objClass).Id_Promocion == -1)
                    ((Promocion)objClass).Id_Promocion = Convert.ToInt64(paramId.Value);
            }*/
            else if (objClass is FacturaDigital)
            {
                if (resultado && ((FacturaDigital)objClass).FacturaId <= 0)
                    ((FacturaDigital)objClass).FacturaId = Convert.ToInt64(paramId.Value);

                if (((FacturaDigital)objClass).CFDI != null)
                {
                    ((FacturaDigital)objClass).CFDI.FacturaId = ((FacturaDigital)objClass).FacturaId;
                    ((FacturaDigital)objClass).CFDI.Grabar();
                }
                if (((FacturaDigital)objClass).FacturaVentas.Count > 0)
                {
                    foreach (FacturaVenta venta in ((FacturaDigital)objClass).FacturaVentas)
                    {
                        venta.FacturaId = ((FacturaDigital)objClass).FacturaId;
                        venta.Grabar();
                    }
                }

                if (((FacturaDigital)objClass).FacturaProductos.Count > 0)
                {
                    foreach (FacturaProductos producto in ((FacturaDigital)objClass).FacturaProductos)
                    {
                        producto.FacturaId = ((FacturaDigital)objClass).FacturaId;
                        producto.Grabar();
                    }
                }
            }
            else if (objClass is FacturaProductos)
            {
                if (resultado && ((FacturaProductos)objClass).Id <= 0)
                    ((FacturaProductos)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is FacturaVenta)
            {
                if (resultado && ((FacturaVenta)objClass).Id <= 0)
                    ((FacturaVenta)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is FacturaCFDI)
            {
                if (resultado && ((FacturaCFDI)objClass).FacturaCFDIId <= 0)
                    ((FacturaCFDI)objClass).FacturaCFDIId = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is FacturaNotaCredito)
            {
                if (resultado && ((FacturaNotaCredito)objClass).FacturaNotaCreditoId <= 0)
                    ((FacturaNotaCredito)objClass).FacturaNotaCreditoId = Convert.ToInt64(paramId.Value);

                if (((FacturaNotaCredito)objClass).CFDI != null)
                {
                    ((FacturaNotaCredito)objClass).CFDI.FacturaId = ((FacturaNotaCredito)objClass).FacturaNotaCreditoId;
                    ((FacturaNotaCredito)objClass).CFDI.Grabar();
                }
            }
            else if (objClass is FacturaNotaCreditoCFDI)
            {
                if (resultado && ((FacturaNotaCreditoCFDI)objClass).FacturaCFDIId <= 0)
                    ((FacturaNotaCreditoCFDI)objClass).FacturaCFDIId = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is DatosFacturacion)
            {
                if (resultado && ((DatosFacturacion)objClass).Id <= 0)
                    ((DatosFacturacion)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is UbicacionFiscal)
            {
                if (resultado && ((UbicacionFiscal)objClass).Id <= 0)
                    ((UbicacionFiscal)objClass).Id = Convert.ToInt64(paramId.Value);
            }
            else if (objClass is CertificadoDigital)
            {
                if (resultado && ((CertificadoDigital)objClass).Id <= 0)
                    ((CertificadoDigital)objClass).Id = Convert.ToInt64(paramId.Value);
            }
        }

        virtual public async Task<bool> Cargar()
        {
            object objClass = this;
            bool resultado = false;
            List<SqlParameter> parametros = new List<SqlParameter>();
            if (objClass is CFDS)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS)objClass).CFDS_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Folio_Fiscal", Value = ((CFDS)objClass).Folio_Fiscal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Id", Value = ((CFDS)objClass).Tipo_Id });
            }
            else if (objClass is CFDS_Archivo)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Archivo_Id", Value = ((CFDS_Archivo)objClass).CFDS_Archivo_Id });
            }
            else if (objClass is CFDS_Producto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Producto_Id", Value = ((CFDS_Producto)objClass).CFDS_Producto_Id });
            }
            else if (objClass is Producto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CodigoProducto", Value = ((Producto)objClass).CodigoProducto });
            }
            /*else if (objClass is CFDS_Gasto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Gasto_Id", Value = ((CFDS_Gasto)objClass).CFDS_Gasto_Id });
            }*/
            else if (objClass is Diccionario)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Diccionario_Id", Value = ((Diccionario)objClass).Diccionario_Id });
            }
            /*else if (objClass is DiccionarioGastos)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Diccionario_Id", Value = ((DiccionarioGastos)objClass).Diccionario_Id });
            }*/
            else if (objClass is Movimiento_Detalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Detalle_Id", Value = ((Movimiento_Detalle)objClass).Movimiento_Detalle_Id });
            }
            else if (objClass is Movimientos)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = ((Movimientos)objClass).Movimiento_Id });
            }
            /*else if (objClass is Producto_Ubicacion)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Ubicacion_Id", Value = ((Producto_Ubicacion)objClass).Producto_Ubicacion_Id });
            }*/
            else if (objClass is Productos)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((Productos)objClass).Producto_Id });
                if (((Productos)objClass).Codigo_de_Barras != "") parametros.Add(new SqlParameter() { ParameterName = "@P_Codigo_de_Barras", Value = ((Productos)objClass).Codigo_de_Barras.Replace("'", "").Replace("\"", "") });
                if (((Productos)objClass).Codigo_Producto != "") parametros.Add(new SqlParameter() { ParameterName = "@P_Codigo_Producto", Value = ((Productos)objClass).Codigo_Producto.Replace("'", "").Replace("\"", "") });
            }
            else if (objClass is ProductoDetalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((ProductoDetalle)objClass).Producto_Id });
            }
            /*else if (objClass is Gasto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = ((Gasto)objClass).Gasto_Id });
            }
            else if (objClass is GastoDetalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = ((GastoDetalle)objClass).Gasto_Id });
            }
            else if (objClass is GastoConcepto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_GastoConcepto_Id", Value = ((GastoConcepto)objClass).GastoConcepto_Id });
            }*/
            else if (objClass is Provedor)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Provedor_Id", Value = ((Provedor)objClass).Provedor_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = ((Provedor)objClass).RFC });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = ((Provedor)objClass).Nombre });
            }
            else if (objClass is Cliente)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cliente_Id", Value = ((Cliente)objClass).Cliente_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = ((Cliente)objClass).RFC });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = ((Cliente)objClass).Nombre });
            }
            /*else if (objClass is Sucursales)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Sucursal", Value = ((Sucursales)objClass).IdSucursal });
            }*/
            else if (objClass is Tipo_Movimiento)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = ((Tipo_Movimiento)objClass).Tipo_Movimiento_Id });
            }
            else if (objClass is Empleado)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Empleado)objClass).Id });
            }
            /*else if (objClass is Huella)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdHuella", Value = ((Huella)objClass).IdHuella });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Huella)objClass).IdEmpleado });
            }*/
            else if (objClass is Usuario)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdUsuario", Value = ((Usuario)objClass).Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Usuario)objClass).IdEmpleado });
            }
            /*else if (objClass is ImageDropbox)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdProducto", Value = ((ImageDropbox)objClass).ProductoId });
            }*/
            else if (objClass is CatalogoFacturaMetodoPago)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CatalogoFacturaMetodoPago)objClass).Catalogo_Id });
            }
            /*else if (objClass is CatalogoLinea1)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CatalogoLinea1)objClass).Catalogo_Id });
            }
            else if (objClass is CatalogoLinea2)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CatalogoLinea2)objClass).Catalogo_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdLinea1", Value = ((CatalogoLinea2)objClass).Catalogo_Linea1_Id });
            }
            else if (objClass is CatalogoLinea3)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CatalogoLinea3)objClass).Catalogo_Id });
            }*/
            else if (objClass is IVA)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((IVA)objClass).Id });
            }
            else if (objClass is IEPS)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((IEPS)objClass).Id });
            }
            else if (objClass is Compra)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Compra_Id", Value = ((Compra)objClass).Compra_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Folio", Value = ((Compra)objClass).Folio });
            }
            else if (objClass is ProductoCompra)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Compra_Producto_Id", Value = ((ProductoCompra)objClass).Producto_Compra_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Compra_Id", Value = ((ProductoCompra)objClass).Compra_Id });
            }
            /*else if (objClass is GastoManual)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = ((GastoManual)objClass).Gasto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Folio", Value = ((GastoManual)objClass).Folio });
            }
            else if (objClass is OrdenGasto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Orden_Gasto_Id", Value = ((OrdenGasto)objClass).Orden_Gasto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdSucursal", Value = ((OrdenGasto)objClass).IdSucursal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((OrdenGasto)objClass).Empleado_Id });
            }
            else if (objClass is OrdenGastoDetalle)
            {
                // parametros.Add(new SqlParameter() { ParameterName = "@P_Orden_Gasto_Detalle_Id", Value = ((OrdenGastoDetalle)objClass).Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Orden_Gasto_Id", Value = ((OrdenGastoDetalle)objClass).Orden_Gasto_Id });
            }*/
            //else if (objClass is DatosFacturacion)
            //{
            //    parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((DatosFacturacion)objClass).Id});
            //}
            //else if (objClass is UbicacionFiscal)
            //{
            //    parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((UbicacionFiscal)objClass).EmpresaId });
            //}
            //else if (objClass is CertificadoDigital)
            //{
            //    parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CertificadoDigital)objClass).EmpresaId });
            //}
            else if (objClass is MetodoPago)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((MetodoPago)objClass).Id });
            }
            else if (objClass is Venta)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Venta", Value = ((Venta)objClass).Id_Venta });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdCliente", Value = ((Venta)objClass).Id_Cliente });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdSucursal", Value = ((Venta)objClass).Id_Sucursal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Venta)objClass).Id_Empleado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaInicioVenta", Value = ((Venta)objClass).FechaInicio });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaFinVenta", Value = ((Venta)objClass).FechaFin });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdFactura", Value = ((Venta)objClass).Id_Factura });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdDatosFiscales", Value = ((Venta)objClass).IdDatosFiscales });
            }
            else if (objClass is VentaDetalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Venta", Value = ((VentaDetalle)objClass).Id_Venta });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdVentas", Value = ((VentaDetalle)objClass).IdVentas });
            }
            /*else if (objClass is Promocion)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((Promocion)objClass).Id_Producto });
            }*/
            string query = ((ClaseBase)objClass).QueryConsultar;
            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(query, parametros);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                foreach (DataRow row in dataset.Tables[query].Rows)
                {
                    ((ClaseBase)objClass).Cargar(row);
                }
                resultado = dataset.Tables[query].Rows.Count > 0;
            }
            return resultado;
        }
        virtual public bool Cargar_Mov_CFDI(int nCFDI, long tipoMovimiento)
        {
            bool resultado = false;
            object objClass = this;
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter() { ParameterName = "@P_CFDI_Id", Value = nCFDI });
            if (objClass is Movimientos)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Movimiento_Id", Value = tipoMovimiento });
                DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta("Movimientos_Consultar_sp_cfdi", parametros);
                if (dataset != null && dataset.Tables.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables["Movimientos_Consultar_sp_cfdi"].Rows)
                    {
                        ((Movimientos)objClass).Cargar(row);
                    }
                    resultado = dataset.Tables["Movimientos_Consultar_sp_cfdi"].Rows.Count > 0;
                }
            }
            else
            {
                string query = ((ClaseBase)objClass).QueryConsultar;
                DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(query, parametros);
                if (dataset != null && dataset.Tables.Count > 0)
                {
                    foreach (DataRow row in dataset.Tables[query].Rows)
                    {
                        ((ClaseBase)objClass).Cargar(row);
                    }
                    resultado = dataset.Tables[query].Rows.Count > 0;
                }
            }
            return resultado;
        }
        virtual public bool Cargar(DataRowView row)
        {
            return false;
        }
        virtual public bool Cargar(DataRow row)
        {
            return false;
        }
        virtual public DataTable Listado()
        {
            object objClass = this;
            DataTable resultado = new DataTable();
            List<SqlParameter> parametros = new List<SqlParameter>();
            if (objClass is CFDS)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = 0 });
                if (((CFDS)objClass).RFC_Emisor.Trim().Length > 0 || ((CFDS)objClass).RFC_Receptor.Trim().Length > 0) parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo_Id", Value = ((CFDS)objClass).Tipo_Id });
                if (((CFDS)objClass).RFC_Emisor.Trim().Length > 0) parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = ((CFDS)objClass).RFC_Emisor });
                if (((CFDS)objClass).RFC_Receptor.Trim().Length > 0) parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Receptor", Value = ((CFDS)objClass).RFC_Receptor });
                if (((CFDS)objClass).Estatus.Trim().Length > 0) parametros.Add(new SqlParameter() { ParameterName = "@P_Estado", Value = ((CFDS)objClass).Estatus });
            }
            else if (objClass is CFDS_Archivo)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Archivo_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS_Archivo)objClass).CFDS_Id });
            }
            else if (objClass is CFDS_Producto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Producto_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS_Producto)objClass).CFDS_Id });
            }
            /*else if (objClass is CFDS_Gasto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Gasto_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS_Gasto)objClass).CFDS_Id });
            }*/
            else if (objClass is Diccionario)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Diccionario_Id", Value = 0 });
            }
            /*else if (objClass is DiccionarioGastos)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Diccionario_Id", Value = 0 });
            }*/
            else if (objClass is Movimiento_Detalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Detalle_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = ((Movimiento_Detalle)objClass).Movimiento_Id });
            }
            else if (objClass is Movimiento_Gastos_Detalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Gasto_Detalle_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = ((Movimiento_Gastos_Detalle)objClass).Movimiento_Id });
            }
            else if (objClass is Movimientos)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = 0 });
            }
            /*else if (objClass is Producto_Ubicacion)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Ubicacion_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((Producto_Ubicacion)objClass).Producto_Id });
            }*/
            else if (objClass is Productos)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = 0 });
            }
            else if (objClass is ProductoDetalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = 0 });
            }
            /*else if (objClass is Gasto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = 0 });
            }
            else if (objClass is GastoDetalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = 0 });
            }
            else if (objClass is GastoConcepto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_GastoConcepto_Id", Value = 0 });
            }*/
            else if (objClass is Provedor)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Provedor_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = string.Empty });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = null });
            }
            else if (objClass is Cliente)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Cliente_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Nombre", Value = string.Empty });
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = null });
            }
            /*else if (objClass is Sucursales)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdSucursal", Value = 0 });
            }*/
            else if (objClass is Empleado)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = 0 });//((Empleado)objClass).Id });
            }
            /*else if (objClass is ImageDropbox)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdProducto", Value = 0 });
            }
            else if (objClass is CatalogoFacturaMetodoPago || objClass is CatalogoLinea1
               || objClass is CatalogoLinea3 || objClass is IVA || objClass is IEPS)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = 0 });
            }
            else if (objClass is CatalogoLinea2)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CatalogoLinea2)objClass).Catalogo_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdLinea1", Value = ((CatalogoLinea2)objClass).Catalogo_Linea1_Id });
            }*/
            else if (objClass is Compra)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Compra_Id", Value = 0 });
                if (((Compra)objClass).RFC_Emisor.Trim().Length > 0) parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = ((Compra)objClass).RFC_Emisor });
            }
            else if (objClass is ProductoCompra)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Compra_Producto_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Compra_Id", Value = ((ProductoCompra)objClass).Compra_Id });
            }
            /*else if (objClass is GastoManual)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Id", Value = 0 });
                if (((GastoManual)objClass).RFC_Emisor.Trim().Length > 0) parametros.Add(new SqlParameter() { ParameterName = "@P_RFC_Emisor", Value = ((GastoManual)objClass).RFC_Emisor });
            }
            else if (objClass is OrdenGasto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Orden_Gasto_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdSucursal", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = 0 });
            }
            else if (objClass is OrdenGastoDetalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Orden_Gasto_Id", Value = ((OrdenGastoDetalle)objClass).Orden_Gasto_Id });
            }*/
            else if (objClass is DatosFacturacion)
            {

            }
            else if (objClass is UbicacionFiscal)
            {
                //parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((UbicacionFiscal)objClass).EmpresaId });
            }
            else if (objClass is CertificadoDigital)
            {
                //parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CertificadoDigital)objClass).EmpresaId });
            }
            else if (objClass is MetodoPago)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((MetodoPago)objClass).Id });
            }
            else if (objClass is Entradas)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Producto_Id", Value = 0 });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((Entradas)objClass).CFDS_Id });
            }
            else if (objClass is Venta)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Venta", Value = ((Venta)objClass).Id_Venta });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdCliente", Value = ((Venta)objClass).Id_Cliente });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdSucursal", Value = ((Venta)objClass).Id_Sucursal });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Venta)objClass).Id_Empleado });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaInicioVenta", Value = ((Venta)objClass).FechaInicio });
                parametros.Add(new SqlParameter() { ParameterName = "@P_FechaFinVenta", Value = ((Venta)objClass).FechaFin });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdFactura", Value = ((Venta)objClass).Id_Factura });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdDatosFiscales", Value = ((Venta)objClass).IdDatosFiscales });
            }
            else if (objClass is VentaDetalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Venta", Value = ((VentaDetalle)objClass).Id_Venta });
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdVentas", Value = ((VentaDetalle)objClass).IdVentas });
            }
            /*else if (objClass is Promocion)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = -1 });
            }*/
            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(((ClaseBase)objClass).QueryConsultar, parametros);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                resultado = dataset.Tables[((ClaseBase)objClass).QueryConsultar];
            }
            return resultado;
        }

        virtual public bool Borrar()
        {
            bool resultado = false;
            object objClass = this;
            List<SqlParameter> parametros = new List<SqlParameter>();
            if (objClass is CFDS)
            {

            }
            else if (objClass is CFDS_Archivo)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS_Archivo)objClass).CFDS_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Archivo_Id", Value = ((CFDS_Archivo)objClass).CFDS_Archivo_Id });
            }
            else if (objClass is CFDS_Producto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS_Producto)objClass).CFDS_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Producto_Id", Value = ((CFDS_Producto)objClass).CFDS_Producto_Id });
            }
            /*else if (objClass is CFDS_Gasto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Id", Value = ((CFDS_Gasto)objClass).CFDS_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_CFDS_Producto_Id", Value = ((CFDS_Gasto)objClass).CFDS_Gasto_Id });
            }*/
            else if (objClass is Movimiento_Detalle)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Id", Value = ((Movimiento_Detalle)objClass).Movimiento_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Movimiento_Detalle_Id", Value = ((Movimiento_Detalle)objClass).Movimiento_Detalle_Id });
            }
            /*else if (objClass is Producto_Ubicacion)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Id", Value = ((Producto_Ubicacion)objClass).Producto_Id });
                parametros.Add(new SqlParameter() { ParameterName = "@P_Producto_Ubicacion_Id", Value = ((Producto_Ubicacion)objClass).Producto_Ubicacion_Id });
            }*/
            else if (objClass is Empleado)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Empleado)objClass).Id });
            }
            /*else if (objClass is Huella)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Huella)objClass).IdEmpleado });
            }*/
            else if (objClass is Usuario)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdEmpleado", Value = ((Usuario)objClass).IdEmpleado });
            }
            /*else if (objClass is Sucursales)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdSucursal", Value = ((Sucursales)objClass).IdSucursal });
            }*/
            else if (objClass is Provedor)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_RFC", Value = ((Provedor)objClass).RFC });
            }
            else if (objClass is Cliente)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdCliente", Value = ((Cliente)objClass).Cliente_Id });
            }
            /*else if (objClass is ImageDropbox)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_ProductoId", Value = ((ImageDropbox)objClass).ProductoId });
            }
            else if (objClass is FileDropbox)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdFolder", Value = ((FileDropbox)objClass).IdFolder });
            }*/
            else if (objClass is Productos)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdProducto", Value = ((Productos)objClass).Producto_Id });
            }
            else if (objClass is ProductoListado)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdProducto", Value = ((ProductoListado)objClass).Producto_Id });
            }
            else if (objClass is CatalogoFacturaMetodoPago)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CatalogoFacturaMetodoPago)objClass).Catalogo_Id });
            }
            /*else if (objClass is CatalogoLinea1)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CatalogoLinea1)objClass).Catalogo_Id });
            }
            else if (objClass is CatalogoLinea2)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CatalogoLinea2)objClass).Catalogo_Id });
            }
            else if (objClass is CatalogoLinea3)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CatalogoLinea3)objClass).Catalogo_Id });
            }*/
            else if (objClass is IVA)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((IVA)objClass).Id });
            }
            else if (objClass is IEPS)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((IEPS)objClass).Id });
            }
            else if (objClass is Compra)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((Compra)objClass).Compra_Id });
            }
            /*else if (objClass is Gasto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdGasto", Value = ((Gasto)objClass).Gasto_Id });
            }
            else if (objClass is GastoListado)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdGasto", Value = ((GastoListado)objClass).Gasto_Id });
            }
            else if (objClass is GastoConcepto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdConceptoGasto", Value = ((GastoConcepto)objClass).GastoConcepto_Id });
            }
            else if (objClass is GastoManual)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Gasto_Manual_Id", Value = ((GastoManual)objClass).Gasto_Manual_Id });
            }
            else if (objClass is OrdenGasto)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Orden_Gasto", Value = ((OrdenGasto)objClass).Orden_Gasto_Id });
            }*/
            else if (objClass is DatosFacturacion)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((DatosFacturacion)objClass).Id });
            }
            else if (objClass is UbicacionFiscal)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((UbicacionFiscal)objClass).Id });
            }
            else if (objClass is CertificadoDigital)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((CertificadoDigital)objClass).Id });
            }
            else if (objClass is MetodoPago)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id", Value = ((MetodoPago)objClass).Id });
            }
            else if (objClass is Venta)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Venta", Value = ((Venta)objClass).Id_Venta });
            }
            /*else if (objClass is Promocion)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_IdPromocion", Value = ((Promocion)objClass).Id_Promocion });
            }
            else if (objClass is FacturaDigital)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_FacturaId", Value = ((FacturaDigital)objClass).FacturaId });
            }*/
            else if (objClass is FacturaNotaCredito)
            {
                parametros.Add(new SqlParameter() { ParameterName = "@P_FacturaId", Value = ((FacturaNotaCredito)objClass).FacturaNotaCreditoId });
            }
            resultado = (BaseDatos.ejecutarProcedimiento(((ClaseBase)objClass).QueryBorrar, parametros) > 0);
            return resultado;
        }

        /// <summary>
        /// Obtiene un listado.
        /// </summary>
        /// <param name="bSoloActivos">Especifica si se obtendrán sólo Activos o también Inactivos.</param>
        /// <returns>El DataTable que se obtiene despues de ejecutar el metodo</returns>
        public System.Data.DataTable Listado(bool bSoloActivos)
        {
            DataTable resultado = new DataTable();
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Cliente", Value = 0 });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Activo", Value = bSoloActivos });

            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                resultado = dataset.Tables[QueryConsultar];
            }
            return resultado;
        }
    }
}