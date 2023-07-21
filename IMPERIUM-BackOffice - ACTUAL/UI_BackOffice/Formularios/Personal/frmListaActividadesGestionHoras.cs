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

namespace UI_BackOffice.Formularios.Personal
{
    public partial class frmListaActividadesGestionHoras : DevExpress.XtraEditors.XtraForm
    {
        private readonly UnitOfWork unit;
        List<eControlHoras> listSegmento, listGrupo, listActividad = new List<eControlHoras>();
        public frmListaActividadesGestionHoras()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }

        private void frmListaActvidadesGestionHoras_Load(object sender, EventArgs e)
        {
            //this.layoutControl2.Visible = false;
            cargarListado();
        }

        private void gvSegmento_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvGrupo_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvActividad_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            unit.Globales.Pintar_CabeceraColumnas(e);
        }

        private void gvActividad_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvGrupo_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvSegmento_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0) unit.Globales.Pintar_EstiloGrilla(sender, e);
        }

        private void gvSegmento_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                eControlHoras objSeg = gvSegmento.GetFocusedRow() as eControlHoras;
                if (String.IsNullOrEmpty(objSeg.dsc_segmento.Replace(" ", ""))) { cargarListado(); return; }
                objSeg.dsc_segmento = objSeg.dsc_segmento.ToUpper();
                objSeg.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                eControlHoras obj = unit.Trabajador.InsertarActualizar_ActividadesGestionHoras<eControlHoras>(1, objSeg);
                if (obj == null) MessageBox.Show("Error al guardar Segmento.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                objSeg.cod_segmento = obj.cod_segmento;
                gvSegmento.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvGrupo_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                eControlHoras objSeg = gvSegmento.GetFocusedRow() as eControlHoras;
                if (objSeg == null) { cargarGrupo(""); return; }
                eControlHoras objGru = gvGrupo.GetFocusedRow() as eControlHoras; objGru.cod_segmento = objSeg.cod_segmento;
                if (String.IsNullOrEmpty(objGru.dsc_grupo.Replace(" ", String.Empty))) { cargarGrupo(objSeg.cod_segmento); return; }
                objGru.dsc_grupo = objGru.dsc_grupo.ToUpper();
                objGru.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                eControlHoras obj = unit.Trabajador.InsertarActualizar_ActividadesGestionHoras<eControlHoras>(2, objGru);
                if (obj == null) MessageBox.Show("Error al guardar Grupo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                objGru.cod_grupo = obj.cod_grupo;
                gvGrupo.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvActividad_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                eControlHoras objSeg = gvSegmento.GetFocusedRow() as eControlHoras;
                eControlHoras objGru = gvGrupo.GetFocusedRow() as eControlHoras;
                if (objSeg == null || objGru == null) { cargarActividad("", ""); return; }
                eControlHoras objAct = gvActividad.GetFocusedRow() as eControlHoras; objAct.cod_segmento = objSeg.cod_segmento; objAct.cod_grupo = objGru.cod_grupo;
                if (objAct == null) { return; }
                objAct.dsc_actividad = objAct.dsc_actividad.ToUpper();
                objAct.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
                eControlHoras obj = unit.Trabajador.InsertarActualizar_ActividadesGestionHoras<eControlHoras>(3, objAct);
                if (obj == null) MessageBox.Show("Error al guardar Actividad.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                objAct.cod_actividad = obj.cod_actividad;
                gvActividad.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvSegmento_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                eControlHoras obj = gvSegmento.GetFocusedRow() as eControlHoras;

                if (obj != null)
                {
                    cargarGrupo(obj.cod_segmento);
                    if (listGrupo.Count == 0) { bsActividad.DataSource = null; gvActividad.RefreshData(); }
                    else
                    {
                        cargarActividad(obj.cod_segmento, listGrupo.First().cod_grupo);
                    }

                }
                else { bsGrupo.DataSource = null; gvGrupo.RefreshData(); }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvSegmento_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void gvGrupo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                eControlHoras obj = gvGrupo.GetFocusedRow() as eControlHoras;
                if (obj != null)
                {
                    cargarActividad(obj.cod_segmento, obj.cod_grupo);
                }
                else { bsActividad.DataSource = null; gvActividad.RefreshData(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gvGrupo_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void gvActividad_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void gvActividad_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void gvSegmento_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            eControlHoras obj = gvSegmento.GetRow(e.RowHandle) as eControlHoras; obj.flg_activo = "SI";
        }

        private void btnEliminar_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eControlHoras obj = gvSegmento.GetFocusedRow() as eControlHoras;
                if (obj == null) return;

                obj.flg_activo = "NO";
                unit.Trabajador.InsertarActualizar_ActividadesGestionHoras<eControlHoras>(1, obj);
                cargarListado();
            }
        }

        private void btnEliminarGrupo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eControlHoras obj = gvGrupo.GetFocusedRow() as eControlHoras;
                if (obj == null) return;

                obj.flg_activo = "NO";
                unit.Trabajador.InsertarActualizar_ActividadesGestionHoras<eControlHoras>(2, obj);

                eControlHoras obj2 = gvGrupo.GetFocusedRow() as eControlHoras;
                if (obj2 != null)
                {
                    cargarGrupo(obj2.cod_segmento);
                }
            }
        }

        private void btnEliminarActividad_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnEliminarActividad_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el registro?" + Environment.NewLine + "Esta acción es irreversible.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eControlHoras obj = gvActividad.GetFocusedRow() as eControlHoras;
                if (obj == null) return;

                obj.flg_activo = "NO";
                unit.Trabajador.InsertarActualizar_ActividadesGestionHoras<eControlHoras>(3, obj);

                eControlHoras obj2 = gvGrupo.GetFocusedRow() as eControlHoras;
                if (obj2 != null)
                {
                    cargarActividad(obj2.cod_segmento, obj2.cod_grupo);
                }
                else { bsActividad.DataSource = null; gvActividad.RefreshData(); }
            }
        }

        private void btnGuardar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if(txtSegmento.EditValue != null)
            //{
            //    eControlHoras obj = new eControlHoras();
            //    obj.dsc_segmento = txtSegmento.EditValue.ToString();
            //    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            //    unit.Trabajador.InsertarActualizar_ActividadesGestionHoras<eControlHoras>(1, obj);
            //}

            //if (txtGrupo.EditValue != null)
            //{
            //    eControlHoras obj = new eControlHoras();
            //    obj.dsc_grupo = txtGrupo.EditValue.ToString();
            //    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            //    unit.Trabajador.InsertarActualizar_ActividadesGestionHoras<eControlHoras>(2, obj);
            //}

            //if (txtActividad.EditValue != null)
            //{
            //    eControlHoras obj = new eControlHoras();
            //    obj.dsc_actividad = txtActividad.EditValue.ToString();
            //    obj.cod_usuario_registro = Program.Sesion.Usuario.cod_usuario;
            //    unit.Trabajador.InsertarActualizar_ActividadesGestionHoras<eControlHoras>(3, obj);
            //}

            MessageBox.Show("Se ha guardado satisfactoriamente", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cargarListado();
        }

        public void cargarListado()
        {
            listSegmento = unit.Trabajador.ListarGestionActividades<eControlHoras>(30);
            bsSegmento.DataSource = listSegmento;
            gvSegmento.RefreshData();

            cargarGrupo(listSegmento.First().cod_segmento);
            cargarActividad(listSegmento.First().cod_segmento, listGrupo.First().cod_grupo);
            
        }

        public void cargarGrupo(string cod_segmento)
        {
            listGrupo = unit.Trabajador.ListarGestionActividades<eControlHoras>(31, cod_segmento);
            bsGrupo.DataSource = listGrupo;
            gvGrupo.RefreshData();
        }

        public void cargarActividad(string cod_segmento, string cod_grupo)
        {
            listActividad = unit.Trabajador.ListarGestionActividades<eControlHoras>(32, cod_segmento, cod_grupo);
            bsActividad.DataSource = listActividad;
            gvActividad.RefreshData();
        }

    }
}