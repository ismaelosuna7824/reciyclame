using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class RequisitosFacturacion
    {
        public DatosFacturacion datosFacturacion { get; set; }
        public UbicacionFiscal ubicacionFiscal { get; set; }
        public CertificadoDigital certificadoDigital { get; set; }
        public RequisitosFacturacion()
        {

        }
    }
}
