﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BE_BackOffice;
using BL_BackOffice;

namespace UI_BackOffice.Formularios.Clientes_Y_Proveedores.Clientes
{
    internal enum MiOpcion
    {
        Vendedor = 1
    }
    public partial class frmBusquedaVendedor : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        internal MiOpcion opcion = MiOpcion.Vendedor;
        public string descripcion = "", codigo = "";

        public frmBusquedaVendedor()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmBusquedaVendedor_Load(object sender, EventArgs e)
        {
            Inicializar();
        }
        public void Inicializar()
        {
            LlenarDataGrid();
        }

        private void LlenarDataGrid()
        {
            try
            {
                switch (opcion)
                {
                    case MiOpcion.Vendedor:
                        List<eTrabajador> ListVendedor = new List<eTrabajador>();
                        ListVendedor = unit.Clientes.ListarVendedores<eTrabajador>(6);
                        bsListadoVendedor.DataSource = null; bsListadoVendedor.DataSource = ListVendedor;
                        gvListadoVendedor.SetAutoFilterValue(gvListadoVendedor.Columns["dsc_nombre_completo"], "", DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmBusquedaVendedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void gvListadoVendedor_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2 && e.RowHandle >= 0)
            {
                PasarDatos();
                this.Close();
            }
        }

        private void gvListadoVendedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && gvListadoVendedor.FocusedRowHandle >= 0)
            {
                PasarDatos();
                this.Close();
            }
        }

        private void gvListadoVendedor_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvListadoVendedor_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        public void PasarDatos()
        {
            switch (opcion)
            {
                case MiOpcion.Vendedor:
                    eTrabajador eTrab = gvListadoVendedor.GetFocusedRow() as eTrabajador;
                    descripcion = eTrab.dsc_nombres_completos;
                    codigo = eTrab.cod_trabajador;
                    break;
            }
        }
    }
}