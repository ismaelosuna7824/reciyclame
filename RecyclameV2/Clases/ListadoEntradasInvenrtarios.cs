using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class ListadoEntradasInvenrtarios : ClaseBase
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

        public List<Entradas> Productos;
        public string Moneda { get; set; }
        public double TipoCambio { get; set; }
        public ListadoEntradasInvenrtarios()
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

            Productos = new List<Entradas>();
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
            Entradas cfdsProducto = new Entradas(CFDS_Id);
            Entradas producto;
            DataTable tabla = cfdsProducto.Listado();
            Productos.Clear();
            if (tabla != null)
            {
                foreach (DataRow row in tabla.Rows)
                {
                    producto = new Entradas(CFDS_Id);
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