using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class CFDS_Producto : ClaseBase
    {
        private Productos _producto = null;
        public bool Agregar { get; set; }
        public long CFDS_Producto_Id { get; set; }
        public long CFDS_Id { get; set; }
        public string Numero_Identificacion { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad_Factura { get; set; }
        public bool isCheckedCantidad_Empaque { get; set; }
        public List<string> lstSeries { get; set; }
        private double _cantidad_empaque;
        private double empaque;
        public double Cantidad_Empaque
        {
            get
            {
                try
                {
                    if (_cantidad_empaque <= 0 && !isCheckedCantidad_Empaque)
                    {
                        if (_producto == null)
                        {
                            _producto = new Productos();
                        }

                        if (_producto.Producto_Id <= 0 && Producto_Id > 0)
                        {
                            _producto.Producto_Id = Producto_Id;
                            if (_producto.Cargar().Result)
                            {
                                _cantidad_empaque = _producto.Cantidad_Empaque;
                            }
                        }
                        else
                        {
                            _cantidad_empaque = empaque;
                        }
                        isCheckedCantidad_Empaque = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return _cantidad_empaque;
            }
            set { _cantidad_empaque = value; isCheckedCantidad_Empaque = true; }
        }

        public double Cantidad { get; set; }
        public double ValorUnitarioOriginal { get; set; }
        public string Unidad { get; set; }
        public string Numero_Serie { get; set; }
        public double Valor_Unitario { get; set; }
        public double Importe { get; set; }
        public long Producto_Id { get; set; }
        public bool isCheckedCodigo_Producto { get; set; }
        private string _codigo_producto;
        public string Codigo_Producto
        {
            get
            {
                try
                {
                    if (!isCheckedCodigo_Producto && string.IsNullOrEmpty(_codigo_producto) && Producto_Id > 0)
                    {
                        if (_producto == null)
                        {
                            _producto = new Productos();
                        }

                        if (_producto.Producto_Id <= 0)
                        {
                            _producto.Producto_Id = Producto_Id;
                            bool b = _producto.Cargar().Result;
                        }

                        _codigo_producto = _producto.Codigo_Producto;
                        isCheckedCodigo_Producto = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return _codigo_producto;
            }
            set { _codigo_producto = value; }
        }

        public bool isCheckedProducto { get; set; }
        private string _producto_descripcion;
        public string Producto
        {
            get
            {
                try
                {
                    if (!isCheckedProducto && string.IsNullOrEmpty(_producto_descripcion) && Producto_Id > 0)
                    {
                        if (_producto == null)
                        {
                            _producto = new Productos();
                        }

                        if (_producto.Producto_Id <= 0)
                        {
                            _producto.Producto_Id = Producto_Id;
                            bool b = _producto.Cargar().Result;
                        }
                        _producto_descripcion = _producto.Descripcion;
                        isCheckedProducto = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return _producto_descripcion;
            }
        }

        public bool isCheckedUltimo_Costo { get; set; }
        private double _ultimo_costo;
        public double Ultimo_Costo
        {
            get
            {
                try
                {
                    if (!isCheckedUltimo_Costo && _ultimo_costo <= 0)
                    {
                        if (_producto == null)
                        {
                            _producto = new Productos();
                        }

                        if (_producto.Producto_Id <= 0 && Producto_Id > 0)
                        {
                            _producto.Producto_Id = Producto_Id;
                            if (_producto.Cargar().Result)
                            {
                                _ultimo_costo = _producto.Ultimo_Costo;
                            }
                        }
                        isCheckedUltimo_Costo = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return _ultimo_costo;
            }
        }
        public double Precio_Sugerido { get; set; }
        private double _diferencia_costo;
        public double Diferencia_Costo
        {
            get
            {
                try
                {
                    double costoActual = Valor_Unitario;

                    if (Ultimo_Costo > 0 && costoActual > 0)
                    {
                        _diferencia_costo = (Math.Round(Ultimo_Costo, 2) - costoActual) * 100.00 / costoActual;
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return _diferencia_costo;
            }
        }

        public bool isCheckedUbicaciones { get; set; }
        private string _ubicaciones;
        public string Ubicaciones
        {
            get
            {
                try
                {
                    if (!isCheckedUbicaciones && string.IsNullOrEmpty(_ubicaciones) && Producto_Id > 0)
                    {
                        if (_producto == null)
                        {
                            _producto = new Productos();
                        }

                        if (_producto.Producto_Id <= 0)
                        {
                            _producto.Producto_Id = Producto_Id;
                            bool b = _producto.Cargar().Result;
                        }
                        //_ubicaciones = string.Join(", ", _producto.Ubicaciones.Select(p => p.Ubicacion).ToArray());
                        isCheckedUbicaciones = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.Message);
                }
                return _ubicaciones;
            }
        }

        public double Descuento_Porciento { get; set; }
        public double Descuento_Monto { get; set; }
        public double Impuesto_Tasa { get; set; }
        public double Impuesto_Monto { get; set; }

        public void ClearProducto()
        {
            Agregar = true;
            Numero_Serie = string.Empty;
            _producto = null;
            _codigo_producto = "";
            _producto_descripcion = "";
            _ultimo_costo = 0;
            _cantidad_empaque = 0;
            _ubicaciones = "";
            lstSeries.Clear();
            empaque = 1;
            ValorUnitarioOriginal = 0;
            Precio_Sugerido = 0;
        }
        public CFDS_Producto()
            : this(-1)
        {
        }

        public CFDS_Producto(long cfds_id)
        {
            Agregar = true;
            TipoClase = ClaseTipo.CFDS_Producto;
            CampoId = "CFDS_Producto_Id";
            CampoBusqueda = "Descripcion";
            QueryGrabar = "CFDS_Producto_Grabar_sp";
            QueryGrabarCodigo = "CFDS_Producto_Grabar_sp_codigo";
            QueryConsultar = "CFDS_Producto_Consultar_sp";
            QueryBorrar = "CFDS_Producto_Borrar_sp";
            lstSeries = new List<string>();
            CFDS_Producto_Id = -1;
            CFDS_Id = cfds_id;
            Cantidad = 0.00;
            Unidad = "";
            Numero_Serie = string.Empty;
            Numero_Identificacion = "";
            Descripcion = "";
            Valor_Unitario = 0.00;
            Importe = 0.00;
            Producto_Id = -1;
            _ultimo_costo = 0;
            _diferencia_costo = 0;
            Cantidad_Factura = 0;
            ValorUnitarioOriginal = 0;
            empaque = 1;
            _cantidad_empaque = 0;
            Precio_Sugerido = 0;
        }

        #region Metodos
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
                CFDS_Producto_Id = Convert.ToInt64(row["CFDS_Producto_Id"]);
                CFDS_Id = Convert.ToInt64(row["CFDS_Id"]);
                Cantidad = Convert.ToDouble(row["Cantidad"]);
                Unidad = Convert.ToString(row["Unidad"]);
                Numero_Identificacion = Convert.ToString(row["Numero_Identificacion"]);
                Codigo_Producto = Numero_Identificacion;
                Descripcion = Convert.ToString(row["Descripcion"]);
                Valor_Unitario = Convert.ToDouble(row["Valor_Unitario"]);
                ValorUnitarioOriginal = Valor_Unitario;
                Importe = Convert.ToDouble(row["Importe"]);
                Producto_Id = Convert.ToInt64(row["Producto_Id"]);
                Cantidad_Factura = Convert.ToDouble(row["Cantidad_Factura"]);
                Descuento_Porciento = Convert.ToDouble(row["Descuento_Porciento"]);
                Descuento_Monto = Convert.ToDouble(row["Descuento_Monto"]);
                Impuesto_Tasa = Convert.ToDouble(row["Impuesto_Tasa"]);
                Impuesto_Monto = Convert.ToDouble(row["Impuesto_Monto"]);
                empaque = Convert.ToDouble(row["Cantidad_Empaque"]);

                resultado = true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                resultado = false;
            }

            return resultado;
        }
        #endregion;
    }
}
