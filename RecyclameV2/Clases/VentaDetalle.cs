using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class VentaDetalle : ClaseBase
    {
        public long Id_Venta_Detalle { get; set; }
        public long Id_Venta { get; set; }
        public long Id_Producto { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public string Unidad_Medida { get; set; }
        public double Tara { get; set; }
        public double Precio_Venta { get; set; }
        public double Precio_Original { get; set; }
        public string Quien_Surte { get; set; }
        public long Id_Sucursal { get; set; }
        public int Surtido { get; set; }
        public decimal Precio_Mayoreo { get; set; }
        public decimal IEPS { get; set; }
        public decimal IVA { get; set; }
        public double IEPSimporte { get; set; }
        public double IVAimporte { get; set; }
        public decimal Precio_Compra { get; set; }
        public decimal ISR_PorCiento { get; set; }
        public double Descuento_ISR { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteReal { get; set; }
        public double Existencia { get; set; }
        public long IdDatosFiscales { get; set; }
        public string IdVentas { get; set; }
        public double UltimaCantidad { get; set; }
        public double UltimaTara { get; set; }
        override public string CampoId { get { return "Id_Venta_Detalle"; } }
        override public string CampoBusqueda { get { return "Id_Venta"; } }
        protected override string QueryGrabar { get { return "Venta_Detalle_Grabar_sp"; } }
        protected override string QueryConsultar { get { return "Venta_Detalle_Consultar_sp"; } }
        private long tipo;
        public VentaDetalle()
            : this(-1)
        {
        }

        public VentaDetalle(long id_venta)
        {
            IdDatosFiscales = -1;
            Id_Venta_Detalle = -1;
            Tara = 0;
            Id_Venta = id_venta;
            Id_Producto = -1;
            Cantidad = -1;
            Quien_Surte = "";
            Id_Sucursal = -1;
            Surtido = 1;
            Precio_Mayoreo = -1;
            IEPS = -1;
            IVA = -1;
            Precio_Compra = -1;
            Descripcion = "";
            Descuento_ISR = -1;
            Precio_Venta = -1;
            Precio_Original = -1;
            Existencia = -1;
            Importe = -1;
            ISR_PorCiento = -1;
            tipo = -1;
        }

        public void setTipo(long tipo)
        {
            this.tipo = tipo;
        }
        #region Metodos
        /// <summary>
        /// Ejecuta el metodo Grabar.
        /// </summary>
        /// <returns>El valor que se obtiene despues de ejecutar el metodo</returns>
        override public bool Grabar()
        {
            bool resultado = false;
            List<SqlParameter> parametros = new List<SqlParameter>();

            SqlParameter paramId = new SqlParameter();
            paramId.ParameterName = "@P_Id_Venta_Detalle";
            paramId.Value = Id_Venta_Detalle;
            paramId.Direction = System.Data.ParameterDirection.InputOutput;
            parametros.Add(paramId);

            parametros.Add(new SqlParameter() { ParameterName = "@P_idVenta", Value = Id_Venta });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Producto", Value = Id_Producto });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Cantidad", Value = Cantidad });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Quien_Surte", Value = Quien_Surte });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Sucursal", Value = Id_Sucursal });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Surtido", Value = Surtido });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Precio_Mayoreo", Value = Precio_Mayoreo });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IEPS", Value = IEPS });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IVA", Value = IVA });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Precio_Promocion", Value = Precio_Compra });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Descuento_Precio", Value = Descuento_ISR });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Precio_Venta", Value = Precio_Venta });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IVAimporte", Value = IVAimporte });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IEPSimporte", Value = IEPSimporte });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Importe", Value = Importe });
            parametros.Add(new SqlParameter() { ParameterName = "@P_DescuentoPorCiento", Value = ISR_PorCiento });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Tipo", Value = tipo });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Tara", Value = Tara });


            resultado = (BaseDatos.ejecutarProcedimiento(QueryGrabar, parametros) > 0);
            if (resultado && Id_Venta_Detalle == -1)
                Id_Venta_Detalle = Convert.ToInt64(paramId.Value);

            return resultado;
        }

        public override bool Cargar(System.Data.DataRowView row)
        {
            return Cargar(row.Row);
        }

        public override bool Cargar(System.Data.DataRow row)
        {
            bool resultado = false;

            try
            {
                Id_Venta_Detalle = Convert.ToInt64(row["IdVentaDetalle"]);
                Id_Venta = Convert.ToInt64(row["IdVenta"]);
                Id_Producto = Convert.ToInt64(row["IdProducto"]);
                Descripcion = Convert.ToString(row["Descripcion"]);
                Unidad_Medida = Convert.ToString(row["UnidadMedida"]);
                Existencia = Convert.ToDouble(row["Existencia"]);
                Cantidad = Convert.ToDouble(row["Cantidad"]);
                Precio_Venta = Convert.ToDouble(row["PrecioVenta"]);
                Precio_Compra = Convert.ToDecimal(row["PrecioPromocion"]);
                Precio_Original = Precio_Venta;
                IVA = Convert.ToDecimal(row["IVA"]);
                IEPS = Convert.ToDecimal(row["IEPS"]);
                IVAimporte = Convert.ToDouble(row["IVAimporte"]);
                IEPSimporte = Convert.ToDouble(row["IEPSimporte"]);
                Importe = Convert.ToDecimal(row["Importe"]);
                Descuento_ISR = Convert.ToDouble(row["DescuentoPrecio"]);
                ISR_PorCiento = Convert.ToDecimal(row["DescuentoPorCiento"]);
                if (row.Table.Columns.Contains("Tara"))
                {
                    Tara = Convert.ToDouble(row["Tara"]);
                    UltimaTara = Tara;
                }
                UltimaCantidad = Cantidad;
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.ErrorException(ex.Message, ex);
                resultado = false;
            }

            return resultado;
        }

        public bool CargarDetalleListado(System.Data.DataRow row)
        {
            bool resultado = false;

            try
            {
                Id_Venta_Detalle = Convert.ToInt64(row["IdVentaDetalle"]);
                Id_Venta = Convert.ToInt64(row["IdVenta"]);
                Id_Producto = Convert.ToInt64(row["IdProducto"]);
                if (row.Table.Columns.Contains("Descripcion"))
                {
                    Descripcion = Convert.ToString(row["Descripcion"]);
                }
                if (row.Table.Columns.Contains("UnidadMedida"))
                {
                    Unidad_Medida = Convert.ToString(row["UnidadMedida"]);
                }
                Cantidad = Convert.ToDouble(row["Cantidad"]);
                Precio_Venta = Convert.ToDouble(row["Precio"]);
                Importe = Convert.ToDecimal(row["Importe"]);
                if (row.Table.Columns.Contains("PrecioCompra"))
                {
                    Precio_Compra = Convert.ToDecimal(row["PrecioCompra"]);
                    ImporteReal = Convert.ToInt64(Precio_Compra * Convert.ToDecimal(Cantidad));
                }
                if (row.Table.Columns.Contains("Tara"))
                {
                    Tara = Convert.ToDouble(row["Tara"]);
                }
                Precio_Original = Precio_Venta;
                IVA = Convert.ToDecimal(row["IVA"]);
                IEPS = Convert.ToDecimal(row["IEPS"]);
                IVAimporte = Convert.ToDouble(row["IVA_Importe"]);
                IEPSimporte = Convert.ToDouble(row["IEPS_Importe"]);
                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.ErrorException(ex.Message, ex);
                resultado = false;
            }

            return resultado;
        }
        public override System.Data.DataTable Listado()
        {
            DataTable resultado = new DataTable();
            List<SqlParameter> parametros = new List<SqlParameter>();

            //parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Venta_Detalle", Value = 0 });
            parametros.Add(new SqlParameter() { ParameterName = "@P_Id_Venta", Value = Id_Venta });
            parametros.Add(new SqlParameter() { ParameterName = "@P_IdVentas", Value = null });

            DataSet dataset = BaseDatos.ejecutarProcedimientoConsulta(QueryConsultar, parametros);
            if (dataset != null && dataset.Tables.Count > 0)
            {
                resultado = dataset.Tables[QueryConsultar];
            }
            return resultado;
        }
        #endregion;
    }
}
