﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_BackOffice;
using BL_BackOffice;
using UI_BackOffice.Formularios.Shared;

namespace UI_BackOffice.Formularios.Cuentas_Pagar
{
    public partial class frmListarProyectos : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        List<eProyecto> ListProyecto = new List<eProyecto>();
        List<eProyecto.eProyecto_Producto_Receta> ListProyectoServicio = new List<eProyecto.eProyecto_Producto_Receta>();
        
        public string cod_empresa, dsc_empresa;

        public frmListarProyectos()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListarProyectos_Load(object sender, EventArgs e)
        {
            lblNombreEmpresa.Text = dsc_empresa;
            Inicializar();
        }

        private void Inicializar()
        {
            ListProyecto = unit.Factura.ObtenerDatosProyecto<eProyecto>(58, cod_empresa);
            bsListadoProyecto.DataSource = ListProyecto; gvListadoProyecto.RefreshData();
        }

        private void gvListadoProyecto_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvListadoProyecto_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProyecto_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoProyecto_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ListProyectoServicio.Clear(); bsListadoProyectoServicio.DataSource = ListProyectoServicio; gvListadoProyectoServicio.RefreshData();
            if (e.FocusedRowHandle >= 0)
            {
                eProyecto obj = gvListadoProyecto.GetRow(e.FocusedRowHandle) as eProyecto;
                ListProyectoServicio = unit.Factura.ObtenerDatosProyecto<eProyecto.eProyecto_Producto_Receta>(59, cod_empresa, obj.cod_proyecto);
                bsListadoProyectoServicio.DataSource = ListProyectoServicio;
            }
        }

        private void btnCrearProyecto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCrearProyecto frm = new frmCrearProyecto();
            frm.dsc_empresa = dsc_empresa;
            frm.ShowDialog();
        }

        private void btnAgregarProducto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Busqueda("", "Productos");
        }

        public void Busqueda(string dato, string tipo, string filtroRUC = "NO")
        {
            frmBusquedas frm = new frmBusquedas();
            frm.filtro = dato;
            
            switch (tipo)
            {
                case "Productos":
                    eProyecto obj = gvListadoProyecto.GetFocusedRow() as eProyecto;
                    frm.BotonAgregarVisible = 1;
                    frm.cod_empresa = cod_empresa;
                    frm.cod_proyecto = obj.cod_proyecto;
                    frm.entidad = frmBusquedas.MiEntidad.ProductosProyecto;
                    frm.filtro = dato;
                    break;
            }
            frm.ShowDialog();
            if (frm.codigo == "" || frm.codigo == null) { return; }
            switch (tipo)
            {
                case "Productos":
                    ListProyectoServicio.Clear(); bsListadoProyectoServicio.DataSource = ListProyectoServicio; gvListadoProyectoServicio.RefreshData();
                    eProyecto obj = gvListadoProyecto.GetFocusedRow() as eProyecto;
                    ListProyectoServicio = unit.Factura.ObtenerDatosProyecto<eProyecto.eProyecto_Producto_Receta>(59, cod_empresa, obj.cod_proyecto);
                    bsListadoProyectoServicio.DataSource = ListProyectoServicio;
                    break;
            }
        }

        private void gvListadoProyectoServicio_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.Column.FieldName == "Sel") return;
            //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos obj = bgvProgramacionPagos.GetFocusedRow() as eFacturaProveedor.eFaturaProveedor_ProgramacionPagos;
            //if (obj == null) return;
            //if (e.Column.FieldName == "cod_estado" && obj.cod_estado == "EJE")
            //{
            //    if (MessageBox.Show("¿Esta seguro de ejecutar el pago?", "Ejecutar pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        obj.cod_usuario_ejecucion = Program.Sesion.Usuario.cod_usuario; obj.dsc_usuario_ejecucion = Program.Sesion.Usuario.dsc_usuario; obj.fch_ejecucion = DateTime.Today;
            //    }
            //    else
            //    {
            //        obj.cod_estado = "PRO";
            //    }
            //}

            //obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            //eFacturaProveedor.eFaturaProveedor_ProgramacionPagos eProgFact = new eFacturaProveedor.eFaturaProveedor_ProgramacionPagos();
            //eProgFact = unit.Factura.InsertarProgramacionPagosFacturaProveedor<eFacturaProveedor.eFaturaProveedor_ProgramacionPagos>(obj);
            //if (eProgFact == null) MessageBox.Show("Error al grabar programación de pago.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //if (e.Column.FieldName == "cod_estado" && obj.cod_estado == "EJE")
            //{
            //    int nRow = bgvProgramacionPagos.FocusedRowHandle;
            //    BuscarFacturas();
            //    bgvProgramacionPagos.FocusedRowHandle = nRow;
            //}
            //else
            //{
            //    CalcularTOTALES();
            //}
            //bgvProgramacionPagos.RefreshData();
        }

        private void gvListadoProyecto_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            ListProyectoServicio.Clear(); bsListadoProyectoServicio.DataSource = ListProyectoServicio; gvListadoProyectoServicio.RefreshData();
            if (e.RowHandle >= 0)
            {
                eProyecto obj = gvListadoProyecto.GetRow(e.RowHandle) as eProyecto;
                ListProyectoServicio = unit.Factura.ObtenerDatosProyecto<eProyecto.eProyecto_Producto_Receta>(59, cod_empresa, obj.cod_proyecto);
                bsListadoProyectoServicio.DataSource = ListProyectoServicio;
            }
        }

        private void gvListadoProyectoServicio_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvListadoProyectoServicio_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvListadoProyectoServicio_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

    }
}