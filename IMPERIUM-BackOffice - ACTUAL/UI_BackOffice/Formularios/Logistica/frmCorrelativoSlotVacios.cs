using System;
using System.Linq;
using UI_BackOffice.ViewModels;

namespace UI_BackOffice.Formularios.Logistica
{
    public partial class frmCorrelativoSlotVacios : UI_BackOffice.Tools.ModalView
    {
        private readonly string codEmpresa;
        private readonly string tipoDocumento;
        private readonly UnitOfWork unitOfWork;
        public frmCorrelativoSlotVacios(string codEmpresa = null, UnitOfWork unitOfWork = null, string tipoDocumento = null)
        {
            InitializeComponent();
            this.codEmpresa = codEmpresa;
            this.unitOfWork = unitOfWork;
            this.tipoDocumento = tipoDocumento;
            configuraciones();
        }
        private void configuraciones()
        {
            this.TitleBackColor = System.Drawing.Color.FromArgb(89, 139, 125);
            unitOfWork.Globales.ConfigurarGridView_ClasicStyle(gcCorrelativoList, gvCorrelativoList);
        }
        private void CorrelativoListar()
        {
            var correlativos = unitOfWork.Logistica.Obtener_correlativosVacios<ItemSelectViewModel>(
                fch_inicio: DateTime.Now,
                fch_fin: DateTime.Now,
                cod_empresa: codEmpresa,
                tipo_documento: tipoDocumento);
            gcCorrelativoList.DataSource = correlativos.ToList();
        }

        private void frmCorrelativoSlotVacios_Load(object sender, EventArgs e)
        {
            CorrelativoListar();
        }
    }
}