using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class Compra : ClaseBase
    {
        public long Compra_Id { get; set; }
        public string RFC_Emisor { get; set; }
        public string Nombre_Emisor { get; set; }
        public string Serie { get; set; }
        public DateTime Fecha { get; set; }
        public string Folio { get; set; }
        public double SubTotal { get; set; }
        public double Descuento { get; set; }
        public double Total { get; set; }
        public double IVA_Tasa { get; set; }
        public double Importe_IVA { get; set; }
        public double IEPS_Tasa { get; set; }
        public double Importe_IEPS { get; set; }
        public string Estatus { get; set; }
        public long Tipo_Movimiento_Id { get; set; }
        public long IdSucursal { get; set; }
        public long IdDatosFiscales { get; set; }
        public BindingList<ProductoCompra> Productos;
        public Compra()
        {
            CampoId = "Compra_Id";
            CampoBusqueda = "Folio_";
            QueryGrabar = "Compras_Grabar_sp";
            //QueryGrabarCodigo = "Compras_Grabar_sp_codigo";
            QueryConsultar = "Compras_Consultar_sp";
            QueryCancelar = "Compras_Cancelar_sp";
            Compra_Id = -1;
            IdSucursal = -1;
            RFC_Emisor = "";
            Nombre_Emisor = "";
            Serie = "";
            Fecha = new DateTime(1900, 1, 1);
            Folio = "";
            SubTotal = 0.00;
            Descuento = 0.00;
            Total = 0.00;
            IVA_Tasa = 0;
            Importe_IVA = 0.00;
            IEPS_Tasa = 0;
            Importe_IEPS = 0;
            Estatus = "NUEVO";
            Tipo_Movimiento_Id = 0;
            Productos = new BindingList<ProductoCompra>();
        }

        public bool GuardarProductos()
        {
            return GrabarProductos();
        }
        private bool GrabarProductos()
        {
            bool resultado = true;

            foreach (ProductoCompra producto in Productos)
            {
                if (!(resultado = producto.Grabar()))
                    break;
            }

            return resultado;
        }
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
                Compra_Id = Convert.ToInt64(row["Compra_Id"]);
                RFC_Emisor = row["RFC_Emisor"].ToString();
                Nombre_Emisor = row["Nombre_Emisor"].ToString();
                Serie = row["Serie"].ToString();
                Folio = row["Folio"].ToString();
                Fecha = Convert.ToDateTime(row["Fecha"]);
                SubTotal = Convert.ToDouble(row["SubTotal"]);
                Descuento = Convert.ToDouble(row["Descuento"]);
                Total = Convert.ToDouble(row["Total"]);
                Importe_IVA = Convert.ToDouble(row["Importe_IVA"]);
                IVA_Tasa = Convert.ToDouble(row["Tasa_IVA"]);
                Importe_IEPS = Convert.ToDouble(row["Importe_IEPS"]);
                IEPS_Tasa = Convert.ToDouble(row["Tasa_IEPS"]);
                Estatus = row["Estatus"].ToString();
                Tipo_Movimiento_Id = Convert.ToInt64(row["Tipo_Movimiento_Id"]);
                IdSucursal = Convert.ToInt64(row["IdSucursal"]);
                IdDatosFiscales = Convert.ToInt64(row["IdDatosFiscales"]);
                CargarProductos();

                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                resultado = false;
            }

            return resultado;
        }

        private bool CargarProductos()
        {
            bool resultado = true;
            ProductoCompra producto = new ProductoCompra(Compra_Id);
            DataTable tabla = producto.Listado();
            Productos.Clear();
            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
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
    }
}
