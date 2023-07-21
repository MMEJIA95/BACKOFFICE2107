using DA_BackOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_BackOffice
{
    public class blUnitOfWork : IDisposable
    {
        daSQL _sql;
        public blUnitOfWork(string key)
        {
            _sql = new daSQL(key);

            Encripta = new blEncrypta(key);
            ConsultaSunat = new blConsultaSunat(key);
            Globales = new blGlobales(_sql, key);
            Clientes = new blClientes(_sql);
            Factura = new blFactura(_sql);
            Proveedores = new blProveedores(_sql);
            Requerimiento = new blRequerimiento(_sql);
            CajaChica = new blCajaChica(_sql);
            CreditoVehicular = new blCreditoVehicular(_sql);
            Logistica = new blLogistica(_sql);
            OrdenCompra_Servicio = new blOrdenCompra_Servicio(_sql);
            Productos_Empresa = new blProductos_Empresa(_sql);
            Sistema = new blSistema(_sql);
            Trabajador = new blTrabajador(_sql);
            Usuario = new blUsuario(_sql);
            Version = new blVersion(_sql);
            SolicitudCompra = new blSolicitudCompra(_sql);
            Aprobaciones = new blAprobaciones(_sql);
        }


        // Solo Llave
        public blEncrypta Encripta { get; private set; }
        public blConsultaSunat ConsultaSunat { get; private set; }
        // Llave y SQL
        public blGlobales Globales { get; private set; }
        // SQL

        public blClientes Clientes { get; private set; }
        public blFactura Factura { get; private set; }
        public blProveedores Proveedores { get; private set; }
        public blRequerimiento Requerimiento { get; private set; }
        public blCajaChica CajaChica { get; private set; }
        public blCreditoVehicular CreditoVehicular { get; private set; }
        public blLogistica Logistica { get; private set; }
        public blOrdenCompra_Servicio OrdenCompra_Servicio { get; private set; }
        public blProductos_Empresa Productos_Empresa { get; private set; }
        public blSistema Sistema { get; private set; }
        public blTrabajador Trabajador { get; private set; }
        public blUsuario Usuario { get; private set; }
        public blVersion Version { get; private set; }
        public blSolicitudCompra SolicitudCompra { get; private set; }

        public blAprobaciones Aprobaciones { get; private set; }

        public void Dispose()
        {
            // destruir instancia
        }

    }
}
