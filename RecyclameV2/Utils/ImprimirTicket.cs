using RecyclameV2.Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Utils
{
    public class ImprimirTicket
    {
        private Font _printFont;
        public static void imprimeTicket(Venta venta, string impresora, string cliente, string empresa, string empleado, string caja)
        {
            try
            {
                CreaTicket Ticket1 = new CreaTicket();
                Ticket1.asignarimpresora(impresora);
                StringBuilder message = new System.Text.StringBuilder();
                //(ESC @)             
                StringBuilder barcodeString = new System.Text.StringBuilder();
                barcodeString.Append(new[]
                        {
                        (char) 0x1B, '@', '\0', //Initialize printer
                    });
                message.Append(barcodeString);
                Ticket1.TextoCentro(GetLogo(), ref message);
                Ticket1.TextoIzquierda("\n", ref message);
                Ticket1.TextoCentro(empresa.Replace("Ñ", "N").Replace("ñ", "n").Replace("Ú", "U").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u"), ref message); // imprime en el centro "Venta mostrador"            
                Ticket1.TextoIzquierda("\n", ref message);
                Ticket1.TextoIzquierda("Folio: " + venta.Id_Venta.ToString(), ref message);
                Ticket1.TextoIzquierda("Empleado: " + empleado.Replace("Ú", "U").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u"), ref message);
                Ticket1.TextoIzquierda("Caja: " + caja.ToString(), ref message);
                Ticket1.TextoExtremos("Fecha: " + venta.Fecha_Venta.ToString("dd/MMM/yyyy"), "Hora: " + venta.Fecha_Venta.ToString("H:mm tt").ToUpper(), ref message);
                Ticket1.TextoIzquierda("Cliente: " + cliente.Replace("Ñ", "N").Replace("ñ", "n").Replace("Ú", "U").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u"), ref message);
                Ticket1.LineasGuion(ref message);
                Ticket1.EncabezadoVenta(ref message);
                /* Poner datos de la empresa
                 * foreach (VentaDetalle detalle in venta.Detalles)
                {
                    string strCantidad = detalle.Cantidad.ToString();
                    if (strCantidad.IndexOf(".") != -1)
                    {
                        string hh = strCantidad.Substring(strCantidad.IndexOf(".") + 1);
                        if (hh.Length > 3)
                        {
                            strCantidad = hh.Substring(0, 3);// productoVendido.Cantidad.ToString("N3", System.Globalization.NumberFormatInfo.InvariantInfo);
                        }
                        else
                        {
                            strCantidad = Global.DoubleToString(detalle.Cantidad);
                        }
                    }

                    string strProducto = detalle.Descripcion;
                    string strPrecio = Global.DoubleToString(detalle.Precio_Venta);
                    string strTotal = Global.DoubleToString(Convert.ToDouble(detalle.Importe));
                    Ticket1.AgregaArticulo(strProducto.Replace("Ñ", "N").Replace("ñ", "n").Replace("Ú", "U").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u"), Convert.ToDouble(strCantidad), detalle.Precio_Venta, Convert.ToDouble(detalle.Importe), ref message); //imprime una linea de descripcion                
                }*/
                Ticket1.LineasGuion(ref message);
                Ticket1.AgregaTotales("                  SubTotal:", Convert.ToDouble(venta.Subtotal), ref message);
                Ticket1.AgregaTotales("                 Descuento:", Convert.ToDouble(venta.Descuento), ref message);
                //Ticket1.AgregaTotales("                       IEPS:", venta.IEPS, ref message);
                Ticket1.AgregaTotales("                       IVA:", Convert.ToDouble(venta.IVA), ref message);
                Ticket1.LineasTotales(ref message);
                Ticket1.AgregaTotales("             Total a Pagar:", Convert.ToDouble(venta.Total), ref message);
                if (venta.Id_Tipo_Venta == Convert.ToInt32(Venta.TIPO_VENTA.CONTADO))
                {
                    Ticket1.LineasGuion(ref message);
                    Ticket1.AgregaTotales("                   Su Pago:", Convert.ToDouble(venta.Su_Pago), ref message);
                    Ticket1.AgregaTotales("                 Su Cambio:", Convert.ToDouble(venta.Su_Cambio), ref message);
                }
                Ticket1.TextoIzquierda("\n", ref message);
                Ticket1.TextoCantidadLetra(string.Format("(** {0} **)", Global.NumeroALetras(Global.DoubleToString(Convert.ToDouble(venta.Total)))), ref message); // imprime en el centro            
                if (venta.Id_Tipo_Venta == Convert.ToInt32(Venta.TIPO_VENTA.CREDITO))
                {
                    Ticket1.TextoIzquierda("\n", ref message);
                    Ticket1.TextoCentro("______________________________", ref message); // imprime en el centro                            
                    Ticket1.TextoCentro("Recibi de Conformidad", ref message); // imprime en el centro                            
                }
                Ticket1.TextoIzquierda("\n", ref message);
                Ticket1.TextoIzquierda("*5 DIAS PARA DEVOLUCIONES EN COMPRAS DE CONTADO", ref message);
                Ticket1.TextoIzquierda("*NO SE ACEPTAN DEVOLUCIONES EN COMPRAS DE APARTADO", ref message);
                Ticket1.TextoIzquierda("\n", ref message);
                Ticket1.TextoCentroCodigoDeBarras(venta.Id_Venta, barcodeString, ref message);
                //impresora = "Microsoft XPS Document Writer";
                RawPrinterHelper.SendStringToPrinter(impresora, message.ToString()); // avanza
                message.Clear();
                Ticket1.CortaTicket(ref message);
                if (message.Length > 0)
                {
                    RawPrinterHelper.SendStringToPrinter(impresora, message.ToString()); // avanza
                }
                message.Clear();
                message = null;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        //    public static void imprimeTicketAbono(Abonos.Abono abono, string impresora, string cliente, string empresa, bool esAbono)
        //    {
        //        try
        //        {
        //            CreaTicket Ticket1 = new CreaTicket();
        //            Ticket1.asignarimpresora(impresora);
        //            StringBuilder message = new System.Text.StringBuilder();
        //            //(ESC @)             
        //            StringBuilder barcodeString = new System.Text.StringBuilder();
        //            barcodeString.Append(new[]
        //                    {
        //                    (char) 0x1B, '@', '\0', //Initialize printer
        //                });
        //            message.Append(barcodeString);
        //            Ticket1.TextoCentro(empresa.Replace("Ñ", "N").Replace("ñ", "n").Replace("Ú", "U").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u"), ref message); // imprime en el centro "Venta mostrador"            
        //            Ticket1.TextoIzquierda("\n", ref message);
        //            Ticket1.TextoIzquierda("Folio: " + abono.Id.ToString(), ref message);
        //            Ticket1.TextoIzquierda("Empleado: " + abono.NombreCajero.Replace("Ú", "U").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u"), ref message);
        //            Ticket1.TextoIzquierda("Caja: " + abono.NoCaja.ToString(), ref message);
        //            Ticket1.TextoExtremos("Fecha: " + abono.Fecha.ToString("dd/MMM/yyyy"), "Hora: " + abono.Fecha.ToString("H:mm tt").ToUpper(), ref message);
        //            if (esAbono)
        //            {
        //                Ticket1.TextoIzquierda("Cliente: " + cliente.Replace("Ñ", "N").Replace("ñ", "n").Replace("Ú", "U").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u"), ref message);
        //            }
        //            else
        //            {
        //                Ticket1.TextoIzquierda("Proveedor: " + cliente.Replace("Ñ", "N").Replace("ñ", "n").Replace("Ú", "U").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u"), ref message);
        //            }
        //            Ticket1.LineasGuion(ref message);
        //            if (esAbono)
        //            {
        //                Ticket1.EncabezadoAbono(ref message);
        //                foreach (Venta venta in abono.Ventas)
        //                {
        //                    string folio = venta.Id.ToString();

        //                    string strFecha = venta.Fecha.ToString("dd/MMM/yyyy");
        //                    string strSaldoAnterior = venta.AbonoCredito;
        //                    string strAbono = venta.SaldoCredito;
        //                    Ticket1.AgregaArticulo(strFecha, Convert.ToInt32(folio), Global.StringToDouble(strSaldoAnterior), Global.StringToDouble(strAbono), ref message); //imprime una linea de descripcion                
        //                }
        //            }
        //            else
        //            {
        //                Ticket1.EncabezadoAbonoCompra(ref message);
        //                foreach (Almacen.Compras compra in abono.Compras)
        //                {
        //                    string folio = compra.Id.ToString();

        //                    string strFecha = compra.Fecha.ToString("dd/MMM/yyyy");
        //                    string strSaldoAnterior = compra.AbonoCredito;
        //                    string strAbono = compra.SaldoCredito;
        //                    Ticket1.AgregaArticulo(strFecha, Convert.ToInt32(folio), Global.StringToDouble(strSaldoAnterior), Global.StringToDouble(strAbono), ref message); //imprime una linea de descripcion                
        //                }
        //            }
        //            Ticket1.LineasGuion(ref message);
        //            Ticket1.AgregaTotales("            Saldo Anterior:", abono.SaldoAnterior, ref message);
        //            Ticket1.AgregaTotales("                     Abono:", abono.Importe, ref message);
        //            Ticket1.LineasTotales(ref message);
        //            Ticket1.AgregaTotales("               Nuevo Saldo:", abono.SaldoNuevo, ref message);
        //            Ticket1.TextoIzquierda("\n", ref message);
        //            Ticket1.TextoCantidadLetra(string.Format("(** {0} **)", Global.NumeroALetras(Global.DoubleToString(abono.Importe))), ref message); // imprime en el centro                        
        //            Ticket1.TextoIzquierda("\n", ref message);
        //            Ticket1.TextoCentroCodigoDeBarras(abono.Id, barcodeString, ref message);
        //            //RawPrinterHelper.SendStringToPrinter(impresora, message.ToString()); // avanza
        //            Ticket1.TextoIzquierda("\n", ref message);
        //            Ticket1.TextoCantidadLetra("*** Gracias por su Preferencia ***", ref message); // imprime en el centro       
        //            string ti = message.ToString();
        //            RawPrinterHelper.SendStringToPrinter(impresora, message.ToString()); // avanza
        //            message.Clear();
        //            Ticket1.CortaTicket(ref message);
        //            if (message.Length > 0)
        //            {
        //                RawPrinterHelper.SendStringToPrinter(impresora, message.ToString()); // avanza
        //            }
        //            message.Clear();
        //            message = null;
        //        }
        //        catch (Exception e)
        //        {
        //            System.Windows.Forms.MessageBox.Show(e.ToString());
        //        }
        //    }
        //}
        #region Clase para generar ticket
        // La clase "CreaTicket" tiene varios metodos para imprimir con diferentes formatos (izquierda, derecha, centrado, desripcion precio,etc), a
        // continuacion se muestra el metodo con ejemplo de parametro que acepta, longitud maxima y un ejemplo de como imprimira, esta clase esta
        // basada en una impresora Epson de matriz de puntos con impresion maxima de 40 caracteres por renglon
        // METODO                                      MAX_LONG                        EJEMPLOS
        //--------------------------------------------------------------------------------------------------------------------------
        // TextoIzquierda("Empleado 1")                    40                      Empleado 1      
        // TextoDerecha("Caja 1")                          40                                                        Caja 1
        // TextoCentro("Ticket")                           40                                         Ticket   
        // TextoExtremos("Fecha 6/1/2011","Hora:13:25")     18 y 18                 Fecha 6/1/2011                Hora:13:25
        // EncabezadoVenta()                                n/a                     Articulo        Can    P.Unit    Importe
        // LineasGuion()                                    n/a                     ----------------------------------------
        // AgregaArticulo("Aspirina","2",45.25,90.5)        16,3,10,11              Aspirina          2    $45.25     $90.50
        // LineasTotales()                                  n/a                                                ----------
        // AgregaTotales("Subtotal",235.25)                 25 y 15                Subtotal                         $235.25
        // LineasAsterisco()                                n/a                     ****************************************
        // LineasIgual()                                    n/a                     ========================================
        // CortaTicket()
        // AbreCajon()
        public class CreaTicket
        {
            string ticket = "";
            string parte1, parte2;
            string impresora = string.Empty; // nombre exacto de la impresora como esta en el panel de control
            int max, cort;
            public CreaTicket() { }
            public void asignarimpresora(string nombreimpresora)
            {
                impresora = nombreimpresora;
            }
            public void LineasGuion(ref StringBuilder message)
            {
                ticket = "------------------------------------------------\n";   // agrega lineas separadoras -            
                message.Append(ticket);
                //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
            }
            public void LineasAsterisco()
            {
                ticket = "****************************************\n";   // agrega lineas separadoras *
                RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
            }
            public void LineasIgual()
            {
                ticket = "========================================\n";   // agrega lineas separadoras =
                RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
            }
            public void LineasTotales(ref StringBuilder meesage)
            {
                ticket = "                                     -----------\n"; ;   // agrega lineas de total            
                meesage.Append(ticket);
                //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
            }
            public void EncabezadoVenta(ref StringBuilder message)
            {
                ticket = "Cant    Producto           P.Unit        Importe\n";   // agrega lineas de  encabezados            
                message.Append(ticket);
                //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
            }

            public void EncabezadoAbono(ref StringBuilder message)
            {
                ticket = "Venta   Fecha              Saldo         Abono\n";   // agrega lineas de  encabezados
                message.Append(ticket);
            }

            public void EncabezadoAbonoCompra(ref StringBuilder message)
            {
                ticket = "Compra  Fecha              Saldo         Abono\n";   // agrega lineas de  encabezados
                message.Append(ticket);
            }

            public void TextoIzquierda(string par1, ref StringBuilder message)                          // agrega texto a la izquierda
            {
                string part = string.Empty;
                max = par1.Length;
                if (max > 48)                                 // **********
                {
                    cort = max - 48;
                    parte1 = par1.Remove(48, cort);        // si es mayor que 40 caracteres, lo corta
                    part = parte1;
                }
                else { parte1 = par1; }                      // **********
                ticket = parte1 + "\n";
                message.Append(ticket);
                //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                if (part.Length > 0)
                {
                    do
                    {
                        max = part.Length;
                        if (max > 48)                                 // **********
                        {
                            cort = max - 48;
                            parte1 = part.Remove(48, cort);        // si es mayor que 40 caracteres, lo corta
                            part = parte1;
                        }
                        else { parte1 = part; part = string.Empty; }                      // **********
                        ticket = parte1 + "\n";
                        message.Append(ticket);
                        //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    }
                    while (part.Length > 0);
                }
            }
            public void TextoDerecha(string par1)
            {
                ticket = "";
                max = par1.Length;
                if (max > 40)                                 // **********
                {
                    cort = max - 40;
                    parte1 = par1.Remove(40, cort);           // si es mayor que 40 caracteres, lo corta
                }
                else { parte1 = par1; }                      // **********
                max = 40 - par1.Length;                     // obtiene la cantidad de espacios para llegar a 40
                for (int i = 0; i < max; i++)
                {
                    ticket += " ";                          // agrega espacios para alinear a la derecha
                }
                ticket += parte1 + "\n";                    //Agrega el texto
                RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
            }
            public void TextoCentro(string par1, ref StringBuilder message)
            {
                if (par1 != null)
                {
                    string[] parts = null;
                    if (par1.IndexOf("\r\n") != -1)
                    {
                        parts = par1.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    if (parts != null)
                    {
                        int length = parts.Length;
                        for (int i = 0; i < length; i++)
                        {
                            ticket = "";
                            par1 = parts[i];
                            max = par1.Length;
                            if (max > 48)                                 // **********
                            {
                                cort = max - 48;
                                parte1 = par1.Remove(48, cort);          // si es mayor que 40 caracteres, lo corta
                            }
                            else { parte1 = par1; }                      // **********
                            max = (int)(48 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
                            for (int j = 0; j < max; j++)                // **********
                            {
                                ticket += " ";                           // Agrega espacios antes del texto a centrar
                            }                                            // **********
                            ticket += parte1 + "\n";
                            message.Append(ticket);
                            //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                        }
                    }
                    else
                    {
                        ticket = "";
                        max = par1.Length;
                        if (max > 48)                                 // **********
                        {
                            cort = max - 48;
                            parte1 = par1.Remove(48, cort);          // si es mayor que 40 caracteres, lo corta
                        }
                        else { parte1 = par1; }                      // **********
                        max = (int)(48 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
                        for (int i = 0; i < max; i++)                // **********
                        {
                            ticket += " ";                           // Agrega espacios antes del texto a centrar
                        }                                            // **********
                        ticket += parte1 + "\n";
                        message.Append(ticket);
                        //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    }
                }
            }

            public void TextoCentroCodigoDeBarras(long codigo, StringBuilder sbReset, ref StringBuilder message)
            {
                string barcode = codigo.ToString("D10");
                string par1 = barcode;
                //Ticket1.GenerateBarCode(venta.Id.ToString("D10"), CreaTicket.Mode.EpsonCode39)
                string[] parts = null;
                if (par1.IndexOf("\r\n") != -1)
                {
                    parts = par1.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (parts != null)
                {
                    int length = parts.Length;
                    for (int i = 0; i < length; i++)
                    {
                        ticket = "";
                        par1 = parts[i];
                        max = par1.Length;
                        if (max > 48)                                 // **********
                        {
                            cort = max - 48;
                            parte1 = par1.Remove(48, cort);          // si es mayor que 40 caracteres, lo corta
                        }
                        else { parte1 = par1; }                      // **********
                        max = (int)(48 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
                        for (int j = 0; j < max; j++)                // **********
                        {
                            ticket += " ";                           // Agrega espacios antes del texto a centrar
                        }                                            // **********
                        ticket += parte1 + "\n";
                        message.Append(ticket);
                        //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    }
                }
                else
                {
                    message.Append(GenerateBarCode(barcode, CreaTicket.Mode.EpsonCode128));
                    message.Append(sbReset.ToString());
                    //RawPrinterHelper.SendStringToPrinter(impresora, GenerateBarCode(barcode, CreaTicket.Mode.EpsonCode128));
                    //RawPrinterHelper.SendStringToPrinter(impresora, sbReset.ToString()); // imprime texto                        
                    ticket = "";
                    par1 = "Folio: " + par1;
                    max = par1.Length;
                    if (max > 48)                                 // **********
                    {
                        cort = max - 48;
                        parte1 = par1.Remove(48, cort);          // si es mayor que 40 caracteres, lo corta
                    }
                    else { parte1 = par1; }                      // **********
                    max = (int)(48 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
                    for (int i = 0; i < max; i++)                // **********
                    {
                        ticket += " ";                           // Agrega espacios antes del texto a centrar
                    }
                    ticket += parte1 + "\n";
                    message.Append(ticket);
                    //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto               
                    ticket = "";
                }
            }

            public void TextoCantidadLetra(string par1, ref StringBuilder message)
            {
                string part = string.Empty;
                ticket = "";
                max = par1.Length;
                if (max > 48)                                 // **********
                {
                    cort = max - 48;
                    parte1 = par1.Remove(48, cort);          // si es mayor que 40 caracteres, lo corta
                    part = par1.Substring(48);
                }
                else { parte1 = par1; }                      // **********
                max = (int)(48 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
                for (int i = 0; i < max; i++)                // **********
                {
                    ticket += " ";                           // Agrega espacios antes del texto a centrar
                }                                            // **********
                ticket += parte1 + "\n";
                message.Append(ticket);
                //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                if (part.Length > 0)
                {
                    do
                    {
                        ticket = "";
                        max = part.Length;
                        if (max > 48)                                 // **********
                        {
                            cort = max - 48;
                            parte1 = part.Remove(48, cort);          // si es mayor que 40 caracteres, lo corta
                            part = part.Substring(48);
                        }
                        else { parte1 = part; part = string.Empty; }                      // **********
                        max = (int)(48 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
                        for (int i = 0; i < max; i++)                // **********
                        {
                            ticket += " ";                           // Agrega espacios antes del texto a centrar
                        }                                            // **********
                        ticket += parte1 + "\n";
                        message.Append(ticket);
                        //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    } while (part.Length > 0);
                }
            }
            public void TextoExtremos(string par1, string par2, ref StringBuilder message)
            {
                max = par1.Length;
                if (max > 20)                                 // **********
                {
                    cort = max - 20;
                    parte1 = par1.Remove(20, cort);          // si par1 es mayor que 18 lo corta
                }
                else { parte1 = par1; }                      // **********
                ticket = parte1;                             // agrega el primer parametro
                max = par2.Length;
                if (max > 20)                                 // **********
                {
                    cort = max - 20;
                    parte2 = par2.Remove(20, cort);          // si par2 es mayor que 18 lo corta
                }
                else { parte2 = par2; }
                max = 47 - (parte1.Length + parte2.Length);
                for (int i = 0; i < max; i++)                 // **********
                {
                    ticket += " ";                            // Agrega espacios para poner par2 al final
                }                                             // **********
                ticket += parte2 + "\n";                     // agrega el segundo parametro al final
                message.Append(ticket);
                //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
            }
            public void AgregaTotales(string par1, double total, ref StringBuilder message)
            {
                max = par1.Length;
                if (max > 27)                                 // **********
                {
                    cort = max - 27;
                    parte1 = par1.Remove(27, cort);          // si es mayor que 25 lo corta
                }
                else { parte1 = par1; }                      // **********
                ticket = parte1;
                parte2 = total.ToString("c");
                max = 48 - (parte1.Length + parte2.Length);
                for (int i = 0; i < max; i++)                // **********
                {
                    ticket += " ";                           // Agrega espacios para poner el valor de moneda al final
                }                                            // **********
                ticket += parte2 + "\n";
                message.Append(ticket);
                //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
            }
            public void AgregaArticulo(string par1, double cant, double precio, double total, ref StringBuilder message)
            {
                //if (cant.ToString().Length <= 10 && precio.ToString("c").Length <= 11 && total.ToString("c").Length <= 14) // valida que cant precio y total esten dentro de rango
                {
                    max = cant.ToString().Length;
                    ticket = cant.ToString();                   // agrega cantidad
                    max = 8 - max;//(4 - par1.ToString().Length) + (16 - parte1.Length); //par1.Length;
                    for (int i = 0; i < max; i++)                // **********
                    {
                        ticket += " ";                           // Agrega espacios para poner el valor de cantidad
                    }
                    string part = string.Empty;
                    if (par1.Length > 15)                                 // **********
                    {
                        cort = par1.Length - 15;
                        parte1 = par1.Remove(15, cort);                     // corta a 16 la descripcion del articulo
                        part = par1.Substring(15);
                    }
                    else { parte1 = par1; }                      // **********
                    ticket += parte1;                             // agrega articulo
                    max = ticket.Length;
                    max = 23 - max;
                    for (int i = 0; i < max; i++)                // **********
                    {
                        ticket += " ";                           // Agrega espacios
                    }                                            // **********
                    ticket += string.Format("{0,11:C2}", precio); // agrega precio
                    max = ticket.Length;
                    max = 34 - max;
                    for (int i = 0; i < max; i++)                // **********
                    {
                        ticket += " ";                           // Agrega espacios
                    }                                            // **********
                    ticket += string.Format("{0,14:C2}", total) + "\n"; // agrega precio      
                    message.Append(ticket);
                    //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    if (part.Length > 0)
                    {
                        do
                        {
                            ticket = "";
                            max = part.Length;
                            if (max > 16)                                 // **********
                            {
                                cort = max - 16;
                                parte1 = part.Remove(16, cort);          // si es mayor que 40 caracteres, lo corta
                                part = part.Substring(16);
                            }
                            else { parte1 = part; part = string.Empty; }                      // **********                        
                            for (int i = 0; i < 8; i++)                // **********
                            {
                                ticket += " ";                           // Agrega espacios antes del texto a centrar
                            }                                            // **********
                            ticket += parte1 + "\n";
                            message.Append(ticket);
                            //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto

                        } while (part.Length > 0);
                    }

                }
                //else
                //{
                //    //System.Windows.Forms.MessageBox.Show("Valores fuera de rango");
                //    RawPrinterHelper.SendStringToPrinter(impresora, "Error, valor fuera de rango\n"); // imprime texto
                //}
            }


            public void AgregaAbono(string par1, int cant, double precio, double total, ref StringBuilder message)
            {
                // if (cant.ToString().Length <= 10 && precio.ToString("c").Length <= 11 && total.ToString("c").Length <= 14) // valida que cant precio y total esten dentro de rango
                {
                    max = cant.ToString().Length;
                    ticket = cant.ToString();                   // agrega cantidad
                    max = 8 - max;//(4 - par1.ToString().Length) + (16 - parte1.Length); //par1.Length;
                    for (int i = 0; i < max; i++)                // **********
                    {
                        ticket += " ";                           // Agrega espacios para poner el valor de cantidad
                    }
                    string part = string.Empty;
                    if (par1.Length > 15)                                 // **********
                    {
                        cort = par1.Length - 15;
                        parte1 = par1.Remove(15, cort);                     // corta a 16 la descripcion del articulo
                        part = par1.Substring(15);
                    }
                    else { parte1 = par1; }                      // **********
                    ticket += parte1;                             // agrega articulo
                    max = ticket.Length;
                    max = 23 - max;
                    for (int i = 0; i < max; i++)                // **********
                    {
                        ticket += " ";                           // Agrega espacios
                    }                                            // **********
                    ticket += string.Format("{0,11:C2}", precio); // agrega precio
                    max = ticket.Length;
                    max = 34 - max;
                    for (int i = 0; i < max; i++)                // **********
                    {
                        ticket += " ";                           // Agrega espacios
                    }                                            // **********
                    ticket += string.Format("{0,14:C2}", total) + "\n"; // agrega precio      
                    message.Append(ticket);
                    //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
                    if (part.Length > 0)
                    {
                        do
                        {
                            ticket = "";
                            max = part.Length;
                            if (max > 16)                                 // **********
                            {
                                cort = max - 16;
                                parte1 = part.Remove(16, cort);          // si es mayor que 40 caracteres, lo corta
                                part = part.Substring(16);
                            }
                            else { parte1 = part; part = string.Empty; }                      // **********                        
                            for (int i = 0; i < 8; i++)                // **********
                            {
                                ticket += " ";                           // Agrega espacios antes del texto a centrar
                            }                                            // **********
                            ticket += parte1 + "\n";
                            message.Append(ticket);
                            //RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto

                        } while (part.Length > 0);
                    }

                }
                //else
                //{
                //    System.Windows.Forms.MessageBox.Show("Valores fuera de rango");
                //    RawPrinterHelper.SendStringToPrinter(impresora, "Error, valor fuera de rango\n"); // imprime texto
                //}
            }
            public void CortaTicket()
            {
                string corte = "\x1B" + "m";                  // caracteres de corte
                string avance = "\x1B" + "d" + "\x09";        // avanza 9 renglones            
                RawPrinterHelper.SendStringToPrinter(impresora, avance); // avanza
                RawPrinterHelper.SendStringToPrinter(impresora, corte); // corta
            }

            public void CortaTicket(ref StringBuilder message)
            {
                //message.Append("\x1B" + "m").Append("\n");
                //message.Append("\x1B" + "d" + "\x09");
                string corte = "\x1B" + "m";                  // caracteres de corte
                string avance = "\x1B" + "d" + "\x09";        // avanza 9 renglones            
                RawPrinterHelper.SendStringToPrinter(impresora, avance); // avanza
                RawPrinterHelper.SendStringToPrinter(impresora, corte); // corta
            }
            public void AbreCajon()
            {
                string cajon0 = "\x1B" + "p" + "\x00" + "\x0F" + "\x96";                  // caracteres de apertura cajon 0
                //string cajon1 = "\x1B" + "p" + "\x01" + "\x0F" + "\x96";                 // caracteres de apertura cajon 1
                RawPrinterHelper.SendStringToPrinter(impresora, cajon0); // abre cajon0
                //RawPrinterHelper.SendStringToPrinter(impresora, cajon1); // abre cajon1
            }


            public static void AbreCajon(string impresora)
            {
                string cajon0 = "\x1B" + "p" + "\x00" + "\x0F" + "\x96";                  // caracteres de apertura cajon 0
                //string cajon1 = "\x1B" + "p" + "\x01" + "\x0F" + "\x96";                 // caracteres de apertura cajon 1
                RawPrinterHelper.SendStringToPrinter(impresora, cajon0); // abre cajon0
                //RawPrinterHelper.SendStringToPrinter(impresora, cajon1); // abre cajon1
            }

            public static void AbreCajon2(string impresora)
            {
                string cajon1 = "\x1B" + "p" + "\x01" + "\x0F" + "\x96";                 // caracteres de apertura cajon 1
                RawPrinterHelper.SendStringToPrinter(impresora, cajon1); // abre cajon1
            }

            public enum Mode
            {
                EpsonCode39,
                EpsonCode128
            }

            public string GenerateBarCode(string barcode, Mode mode)
            {
                StringBuilder barcodeString = new StringBuilder();

                switch (mode)
                {
                    #region case printerModel.EpsonCode39
                    case Mode.EpsonCode39:

                        barcodeString.Append(new[]
                    {
                        (char) 0x1B, '@', '\0', //Initialize printer
                    });
                        barcodeString.Append(new[]
                    {
                        (char) 0x1B, 'a', (char) 1, //Selects center print position
                        (char) 0x1D, 'w', (char) 2, //Module width
                        (char) 0x1D, 'h', (char) 64, //Bar lenght(Height)
                        //(char) 0x1D, 'H', (char) 1, //HRI at top
                        (char) 0x1D, 'k', (char) 69, (char) (barcode.Length + 2) //Imprimir código de barra + 2*
                    });
                        barcodeString.Append("{barCodeData}");
                        barcodeString.Append(new[]
                    {
                        (char) 0x1B, '{', (char) 1, //Activate 180º rotation
                        (char) 0x1B, 'a', (char) 2, //Selects left (right x 180º) print position
                        (char) 0x1B, 'V', (char) 1, //Activate 90º rotation (270º)
                        (char) 0x1B, '3', (char) 15 //Adjusts line separation
                    });
                        //barcodeString.Append(new[] { '\n' });
                        for (int i = barcode.Length - 1; i >= 0; i--)
                            barcodeString.Append(new[] { barcode[i], '\n' });
                        barcodeString.Append(new[]
                    {
                        (char)0x0C //print and feed label to print start position (FF)
                    });

                        barcodeString = barcodeString.Replace("{barCodeData}", '*' + barcode + '*');

                        break;
                    #endregion

                    #region case printerModel.EpsonCode128
                    case Mode.EpsonCode128:

                        barcodeString.Append(new[]
                    {
                        (char) 0x1B, '@', '\0', //Initialize printer
                    });
                        barcodeString.Append(new[]
                    {
                        (char) 0x1B, 'a', (char) 1, //Selects center print position
                        (char) 0x1D, 'w', (char) 2, //Module width
                        (char) 0x1D, 'h', (char) 64, //Bar lenght(Height)
                        //(char) 0x1D, 'H', (char) 1, //HRI at top
                        (char) 0x1D, 'k', (char) 73, (char) (barcode.Length + 2), //Imprimir 'Tipo A' + código de barra
                        (char) 123, (char) 66 //Typo A (of code 128), use 67 or 68 for B and C
                    });
                        barcodeString.Append("{barCodeData}");
                        barcodeString.Append(new[]
                    {
                        (char) 0x1B, '{', (char) 1, //Activate 180º rotation
                        (char) 0x1B, 'a', (char) 2, //Selects left (right x 180º) print position
                        (char) 0x1B, 'V', (char) 1, //Activate 90º rotation (270º)
                        (char) 0x1B, '3', (char) 15 //Adjusts line separation
                    });
                        //for (int i = barcode.Length - 1; i >= 0; i--)
                        //    barcodeString.Append(new[] { barcode[i], '\n' });
                        barcodeString.Append(new[]
                    {
                        (char)0x0C //print and feed label to print start position (FF)
                    });

                        //int checkSum = 103;
                        //for (int i = 0; i < barcode.Length; i++)
                        //{
                        //    checkSum += (barcode[i] - 32)*(i+1);
                        //}
                        //checkSum = checkSum%103;

                        barcodeString = barcodeString.Replace("{barCodeData}", barcode);

                        break;
                        #endregion
                }

                return barcodeString.ToString();
            }
        }
        #endregion
        #region Clase para enviar a imprsora texto plano
        public class RawPrinterHelper
        {
            // Structure and API declarions:
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class DOCINFOA
            {
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDocName;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pOutputFile;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDataType;
            }

            [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

            [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool ClosePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

            [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndDocPrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

            // SendBytesToPrinter()
            // When the function is given a printer name and an unmanaged array
            // of bytes, the function sends those bytes to the print queue.
            // Returns true on success, false on failure.
            public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
            {
                Int32 dwError = 0, dwWritten = 0;
                IntPtr hPrinter = new IntPtr(0);
                DOCINFOA di = new DOCINFOA();
                bool bSuccess = false; // Assume failure unless you specifically succeed.

                di.pDocName = "ARAConsultoriaySoluciones";
                di.pDataType = "RAW";

                //Open the printer.
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page.
                        if (StartPagePrinter(hPrinter))
                        {
                            // Write your bytes.
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                // If you did not succeed, GetLastError may give more information
                // about why not.
                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
                return bSuccess;
            }

            public static bool SendStringToPrinter(string szPrinterName, string szString)
            {
                IntPtr pBytes;
                Int32 dwCount;
                // How many characters are in the string?
                dwCount = szString.Length;
                // Assume that the printer is expecting ANSI text, and then convert
                // the string to ANSI text.
                pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                // Send the converted ANSI string to the printer.
                SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                Marshal.FreeCoTaskMem(pBytes);
                return true;
            }
        }

        public static string GetLogo()
        {
            string logo = "";
            //if (!System.IO.File.Exists(Properties.Settings.Default.Logo))
            //    return null;
            BitmapData data = GetBitmapData();//Properties.Settings.Default.Logo);
            BitArray dots = data.Dots;
            byte[] width = BitConverter.GetBytes(data.Width);

            int offset = 0;
            MemoryStream stream = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(stream);

            bw.Write((char)0x1B);
            bw.Write('@');

            bw.Write((char)0x1B);
            bw.Write('3');
            bw.Write((byte)24);

            while (offset < data.Height)
            {
                bw.Write((char)0x1B);
                bw.Write('*');         // bit-image mode
                bw.Write((byte)33);    // 24-dot double-density
                bw.Write(width[0]);  // width low byte
                bw.Write(width[1]);  // width high byte

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;
                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;
                            // Calculate the location of the pixel we want in the bit array.
                            // It'll be at (y * width) + x.
                            int i = (y * data.Width) + x;

                            // If the image is shorter than 24 dots, pad with zero.
                            bool v = false;
                            if (i < dots.Length)
                            {
                                v = dots[i];
                            }
                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }

                        bw.Write(slice);
                    }
                }
                offset += 24;
                bw.Write((char)0x0A);
            }
            // Restore the line spacing to the default of 30 dots.
            bw.Write((char)0x1B);
            bw.Write('3');
            bw.Write((byte)30);

            bw.Flush();
            byte[] bytes = stream.ToArray();
            return logo + Encoding.Default.GetString(bytes);
        }

        public static BitmapData GetBitmapData()
        {
            //new Bitmap((Bitmap)Properties.Resources.Spazio_fw);
            using (var bitmap = (Bitmap)Properties.Resources.RecyclameLogo)//(Bitmap)Bitmap.FromFile(bmpFileName))
            {
                var threshold = 127;
                var index = 0;
                double multiplier = 570; // this depends on your printer model. for Beiyang you should use 1000
                double scale = (double)(multiplier / (double)bitmap.Width);
                int xheight = (int)(bitmap.Height * scale);
                int xwidth = (int)(bitmap.Width * scale);
                var dimensions = xwidth * xheight;
                var dots = new BitArray(dimensions);

                for (var y = 0; y < xheight; y++)
                {
                    for (var x = 0; x < xwidth; x++)
                    {
                        var _x = (int)(x / scale);
                        var _y = (int)(y / scale);
                        var color = bitmap.GetPixel(_x, _y);
                        var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        dots[index] = (luminance < threshold);
                        index++;
                    }
                }

                return new BitmapData()
                {
                    Dots = dots,
                    Height = (int)(bitmap.Height * scale),
                    Width = (int)(bitmap.Width * scale)
                };
            }
        }

        public class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }
        #endregion

        static public Font ChangeFontSize(Font font, float fontSize, GraphicsUnit unit)
        {
            if (font != null)
            {
                float currentSize = font.Size;
                if (currentSize != fontSize)
                {
                    font = new Font(font.Name, fontSize,
                        font.Style, unit,
                        font.GdiCharSet, font.GdiVerticalFont);
                }
            }
            return font;
        }
    }
}
