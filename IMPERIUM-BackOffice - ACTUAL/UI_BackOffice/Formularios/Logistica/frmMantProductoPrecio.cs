using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL_BackOffice;
using BE_BackOffice;
using UI_BackOffice.Formularios.Shared;

namespace UI_BackOffice.Formularios.Logistica
{
    internal enum TarifaProducto
    {
        Nuevo = 0,
        Editar = 1,
        Vista = 2
    }

    public partial class frmMantProductoPrecio : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        frmListadoProductoPrecios frmHandler;
        internal TarifaProducto MiAccion = TarifaProducto.Nuevo;
        List<eProductos.eProductosTarifas> listHistoricoTarifas = new List<eProductos.eProductosTarifas>();
        public string cod_empresa = "", cod_tipo_servicio = "", cod_subtipo_servicio = "", cod_producto = "", dsc_ruc = "", cod_proveedor = "", dsc_proveedor = "", dsc_producto = "";
        public DateTime fch_inicio;
        public decimal imp_costo;
        
        public bool ActualizarListado = false;

        public frmMantProductoPrecio()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
        public frmMantProductoPrecio(frmListadoProductoPrecios frm)
        {
            InitializeComponent();
            frmHandler = frm;
            unit = new UnitOfWork();
        }

        private void frmMantProductoPrecio_Load(object sender, EventArgs e)
        {
            lblNombreProducto.AppearanceItemCaption.ForeColor = Program.Sesion.Colores.Verde;
            Inicializar();
        }

        private void Inicializar()
        {
            unit.Logistica.CargaCombosLookUp("ProveedorProducto", lkpProveedor, "cod_proveedor", "dsc_proveedor", "", valorDefecto: false, cod_tipo_servicio: cod_tipo_servicio, cod_subtipo_servicio: cod_subtipo_servicio, cod_producto: cod_producto);

            switch (MiAccion)
            {
                case TarifaProducto.Nuevo:
                    break;
                case TarifaProducto.Editar:
                    ObtenerDatos_PrecioProducto();
                    break;
            }
        }
        private void ObtenerDatos_PrecioProducto()
        {
            dtFecha.EditValue = DateTime.Today;
            lkpProveedor.EditValue = cod_proveedor;
            lblNombreProducto.Text = dsc_producto;
            txtMontoUnitarioActual.EditValue = imp_costo;
            ObtenerDatos_HistoricoPrecios();
        }
        private void ObtenerDatos_HistoricoPrecios()
        {
            listHistoricoTarifas.Clear();
            listHistoricoTarifas = unit.Logistica.Obtener_ListadosProductos<eProductos.eProductosTarifas>(8, cod_tipo_servicio: cod_tipo_servicio, cod_subtipo_servicio: cod_subtipo_servicio, cod_producto: cod_producto, cod_proveedor: lkpProveedor.EditValue.ToString());
            bsHistoricoTarifas.DataSource = listHistoricoTarifas; gVHistoricoTarifas.RefreshData();
        }

        private void frmMantProductoPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (MiAccion == TarifaProducto.Editar && e.KeyCode == Keys.Escape) this.Close();
        }

        private void lkpProveedor_EditValueChanged(object sender, EventArgs e)
        {
            ObtenerDatos_HistoricoPrecios();
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                eProductos.eProductosTarifas obj = new eProductos.eProductosTarifas();
                obj.cod_empresa = cod_empresa; obj.cod_tipo_servicio = cod_tipo_servicio; obj.cod_subtipo_servicio = cod_subtipo_servicio;
                obj.cod_producto = cod_producto; obj.num_item = 0; obj.dsc_ruc = dsc_ruc;
                obj.cod_proveedor = cod_proveedor; obj.fch_inicio = Convert.ToDateTime(dtFecha.EditValue);
                obj.fch_fin = new DateTime(2999, 12, 31); obj.imp_costo = Convert.ToDecimal(txtMontoNuevo.EditValue);
                eProductos.eProductosTarifas eObj = unit.Logistica.Insertar_Actualizar_ProductoCostos<eProductos.eProductosTarifas>(obj);
                if (eObj == null) { MessageBox.Show("Error al insertar costo", "", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                MessageBox.Show("Se guardaron los datos de manera satisfactoria", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                imp_costo = Convert.ToDecimal(txtMontoNuevo.EditValue); txtMontoUnitarioActual.EditValue = imp_costo; txtMontoNuevo.EditValue = 0;
                ObtenerDatos_HistoricoPrecios();
                ActualizarListado = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNuevoProveedor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Busqueda("", "Proveedor");
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;
            
            switch (tipo)
            {
                case "Proveedor":
                    frm.entidad = frmBusquedas.MiEntidad.Proveedor;
                    frm.filtroRUC = filtroRUC;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Proveedor":
                    lkpProveedor.Tag = frm.codigo;
                    lkpProveedor.Text = frm.descripcion;
                    if (frm.descripcion == "") { lkpProveedor.Tag = null; }
                    if (frm.codigo != "")
                    {
                        eProveedor eProv = new eProveedor();
                        eProv = unit.Proveedores.ObtenerProveedor<eProveedor>(2, frm.codigo);
                    }
                    break;
            }
        }

        private void gVHistoricoTarifas_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        { 
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gVHistoricoTarifas_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }
    }
}