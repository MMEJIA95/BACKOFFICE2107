using BE_BackOffice;
using DevExpress.XtraSplashScreen;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace UI_BackOffice.Formularios.Logistica
{
    internal class RequerimientosHelper
    {
        private readonly UnitOfWork unit;
        public RequerimientosHelper(UnitOfWork unit) { this.unit = unit; }

        internal eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle CrearDetalle(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj)
        {
            eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle eDetOC = new eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle();

            eDetOC.cod_empresa = obj.cod_empresa;
            eDetOC.cod_sede_empresa = obj.cod_sede_empresa;
            eDetOC.cod_orden_compra_servicio = obj.cod_orden_compra_servicio;
            eDetOC.flg_solicitud = obj.flg_solicitud;
            eDetOC.dsc_anho = obj.dsc_anho;
            eDetOC.num_item = obj.num_item;
            eDetOC.cod_requerimiento = obj.cod_requerimiento;
            eDetOC.cod_proveedor = obj.cod_proveedor;
            eDetOC.dsc_ruc = obj.dsc_ruc;
            eDetOC.cod_tipo_servicio = obj.cod_tipo_servicio;
            eDetOC.cod_subtipo_servicio = obj.cod_subtipo_servicio;
            eDetOC.cod_producto = obj.cod_producto;
            eDetOC.dsc_servicio = "";
            eDetOC.cod_unidad_medida = obj.cod_unidad_medida;
            eDetOC.num_cantidad = obj.num_cantidad;
            eDetOC.imp_unitario = obj.imp_unitario;
            eDetOC.imp_total_det = obj.imp_total_det;

            /*-----*Adicionar flg_solicitaOC a producto que se ha ordenado*-----*/
            eDetOC.flg_solicitaOC = "SI";

            eDetOC = unit.OrdenCompra_Servicio.Ins_Act_Detalle_OrdenCompra_Servicio<eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle>(eDetOC, Program.Sesion.Usuario.cod_usuario);

            return eDetOC;
        }
        internal eOrdenCompra_Servicio CreaCabecera(eOrdenCompra_Servicio.eOrdenCompra_Servicio_Detalle obj, string solicitud, string cod_ocs = "", string cod_ord_estado = "GEN")
        {
            eOrdenCompra_Servicio eOC = new eOrdenCompra_Servicio();

            eOC.cod_empresa = obj.cod_empresa;
            eOC.cod_sede_empresa = obj.cod_sede_empresa;
            eOC.cod_orden_compra_servicio = cod_ocs;
            eOC.num_cotizacion = "N/A";
            eOC.cod_proveedor = obj.cod_proveedor;
            eOC.dsc_ruc = obj.dsc_ruc;
            eOC.flg_solicitud = solicitud;
            eOC.cod_almacen = "";
            eOC.cod_modalidad_pago = "";
            eOC.dsc_direccion_despacho = "";
            eOC.fch_emision = DateTime.Now;
            eOC.fch_despacho = new DateTime(1999, 01, 01);
            eOC.dsc_terminos_condiciones = "";
            eOC.imp_subtotal = 0;
            eOC.imp_igv = 0;
            eOC.imp_total = 0;
            eOC.dsc_imp_total = "";
            eOC.prc_CV = 0;
            eOC.prc_LI = 0;
            eOC.prc_CB = 0;
            eOC.prc_GG = 0;
            eOC.prc_ADM = 0;
            eOC.prc_OPER = 0;
            eOC.prc_GV = 0;
            eOC.dsc_observaciones = "";
            eOC.cod_estado_orden = cod_ord_estado;

            eOC = unit.OrdenCompra_Servicio.Ins_Act_OrdenCompra_Servicio<eOrdenCompra_Servicio>(eOC, Program.Sesion.Usuario.cod_usuario);

            return eOC;
        }



        internal static List<eRequerimiento.eRequerimiento_Detalle> GetListImportarRequerimientosDeFormatoExcel(string d)
        {
            var detalle = new List<eRequerimiento.eRequerimiento_Detalle>();

            //var x = HNG.Excel.GetList_fromExcel<eRequerimiento.eRequerimiento_Detalle>("");
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Archivos de Excel (*.xls;*.xlsx)|*.xls;*.xlsx";
            openFile.Title = "Documento de requerimientos";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;

            var open = openFile.ShowDialog();
            if (open == DialogResult.OK)
            {
                string ruta = openFile.FileName;
                var table = HNG.Excel.GetDataTable_fromExcel(ruta);// "ruta del excel");
            }
            return detalle;
        }

        private class Importar3Column
        {
            public object Id { get; set; }
            public object Name { get; set; }
            public object Value { get; set; }
        }
        internal static List<eRequerimiento.eRequerimiento_Detalle> GetListImportarRequerimientosDeFormatoExcel(
            List<eProductos> productos,
            List<eProductoEmpresaProveedor> productoProveedorEmpresa)
        {
            var detalle = new List<eRequerimiento.eRequerimiento_Detalle>();
            var itemsErrados = new List<Importar3Column>();
            var productosImportados = HNG.Excel.GetList_fromExcel<Importar3Column>(out _, iniciaRow: 3, iniciaRangoColumn: 1, terminaRangoColumn: 3);


            //unit.Globales.Abrir_SplashScreenManager(typeof(Formularios.Shared.FrmSplashCarga), "Obteniendo productos", "Cargando...");

            if (productosImportados == null) return default;
            foreach (var imported in productosImportados)
            {
                if (imported.Id != null && imported.Value != null)
                {
                    string cod_producto = imported.Id.ToString();
                    //if ((new string[] { "0000000463" }).Contains(cod_producto))
                    //{
                    //    var si_contiene = "";
                    //    var ss = "";
                    //}


                    decimal.TryParse(imported.Value.ToString(), out decimal cantidad);
                    //if (cantidad < 1)
                    //{
                    //    var enr = "entr";
                    //}
                    if (cantidad >= 0)
                    {
                        var producto = productos.FirstOrDefault((product) => product.cod_producto.Equals(cod_producto));
                        if (producto != null)
                        {
                            if (!ValidarVinculacionDeProductos(productoProveedorEmpresa: productoProveedorEmpresa,
                                tipo_servicio: producto.cod_tipo_servicio,
                               subtipo_servicio: producto.cod_subtipo_servicio,
                               producto: producto.cod_producto,
                               flg_vigente: "SI",
                               flg_con_proveedor: "SI"))
                            {
                                //index++;
                                //productosNoAgregados[index] = obj.dsc_producto.ToString();
                                //continue;

                                //llenar una lista de los productos que no se tienen relación con proveedor y precios
                                itemsErrados.Add(new Importar3Column
                                {
                                    Id = imported.Id,
                                    Name = $"{imported.Name} - (Este producto no tiene proveedor asignado)",
                                    Value = imported.Value,
                                });
                            }
                            else
                            {
                                detalle.Add(new eRequerimiento.eRequerimiento_Detalle
                                {
                                    cod_tipo_servicio = producto.cod_tipo_servicio,
                                    dsc_tipo_servicio = producto.dsc_tipo_servicio,
                                    cod_subtipo_servicio = producto.cod_subtipo_servicio,
                                    dsc_subtipo_servicio = producto.dsc_subtipo_servicio,
                                    cod_producto = producto.cod_producto,
                                    dsc_producto = producto.dsc_producto,
                                    cod_unidad_medida = producto.cod_unidad_medida,
                                    dsc_simbolo = producto.dsc_simbolo,
                                    flg_generaOC = "SI",
                                    Sel_generaOC = true,
                                    num_cantidad = cantidad,
                                });
                            }
                        }
                        else
                        {
                            //llenar una lista de los productos que no se han encontrado en la DB
                            itemsErrados.Add(new Importar3Column
                            {
                                Id = imported.Id,
                                Name = $"{imported.Name} - (Este producto no existe en Imperium)",
                                Value = imported.Value,
                            });
                        }
                    }
                    else
                    {
                        //llenar una lista de items que no se ha podido rescatar por que el formato cantidad es incorrcto.
                        itemsErrados.Add(new Importar3Column
                        {
                            Id = imported.Id,
                            Name = $"{imported.Name} - (La cantidad no está en el formato correcto)",
                            Value = imported.Value,
                        });
                    }
                }
            }

            //Descargar::productos que no se han podido incluir en el requerimiento
            if (itemsErrados.Count > 0)
            {
                HNG.MessageWarning("Se han encontrado productos que no se ha incluido en los requerimientos, detalles en la descarga." +
                    "\n¡Se abrirá el documento con items errados!", "Requerimientos importados");
                HNG.Excel.GenerateExcel_fromList(itemsErrados, $"ItemsErrados_{DateTime.Today.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}");
            }
            //SplashScreenManager.CloseForm();
            return detalle;
        }

        private static bool ValidarVinculacionDeProductos(List<eProductoEmpresaProveedor> productoProveedorEmpresa,
            string tipo_servicio,
            string subtipo_servicio,
            string producto,
            string flg_vigente,
            string flg_con_proveedor)
        {
            return productoProveedorEmpresa.Exists((ex) => ex.cod_tipo_servicio.Equals(tipo_servicio)
            && ex.cod_subtipo_servicio.Equals(subtipo_servicio)
            && ex.cod_producto.Equals(producto)
            && ex.flg_vigente.Equals(flg_vigente)
            && ex.flg_con_proveedor.Equals(flg_con_proveedor));
        }
    }
}
