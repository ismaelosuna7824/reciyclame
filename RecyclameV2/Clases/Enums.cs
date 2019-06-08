using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    enum FORMAS
    {
        // ENTRADA
        CargaFacturaEntrada,
        EntradaInventario,
        // SALIDA
        CargaFacturaSalida,
        SalidaInventario,
        // REPORTES
        ListadoMovimientos,
        // CONFIGURACION
        LocalizacionProductos,
        TipoMovimientos,
    }

    public enum TIPO_ARCHIVO
    {
        NO_SOPORTADO,
        PDF,
        XML,
        IMAGEN,
    }
    public enum TIPO_FACTURA
    {
        ENTRADA,
        SALIDA,
        GASTOS,
    }
    public enum TIPO_MOVIMIENTO
    {
        COMPRA = 0,
        VENTA = 1
    }
}
