using BE_BackOffice;
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

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmListadoSolicitudCompra_VistaRequerimiento : HNG_Tools.SimpleModalForm
    {
        private readonly UnitOfWork unit;
        public frmListadoSolicitudCompra_VistaRequerimiento()
        {
            InitializeComponent();
            unit = new UnitOfWork();
            configurar_formulario();

        }
        private void configurar_formulario()
        {
            unit.Globales.ConfigurarGridView_ClasicStyle(gcListadoRequerimientos, gvListadoRequerimientos);
            //gvListadoRequerimientos.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            //gvListadoRequerimientos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.TitleBackColor = Program.Sesion.Colores.Verde;
        }

        internal void CargarRequerimientos_ProProductos(string cod_producto, string cod_empresa)
        {
            var objList = unit.SolicitudCompra.ConsultaVarias<eSolicitudCompra_Requerimientos>(
                new PQSolicitudCompra() { Opcion = 2, Cod_producto = cod_producto, Cod_empresa = cod_empresa });
            bsListadoRequerimientos.DataSource = null;
            if (objList == null || objList.Count == 0) return;

            bsListadoRequerimientos.DataSource = objList.ToList();
            gvListadoRequerimientos.ExpandAllGroups();
            gvListadoRequerimientos.RefreshData();
        }
        private void frmListadoSolicitudCompra_VistaRequerimiento_Load(object sender, EventArgs e)
        {

        }
    }
}