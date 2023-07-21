using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace UI_BackOffice.Formularios.Sistema.Accesos
{
    public partial class frmLogDeAccesos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly UnitOfWork unit;

        public frmLogDeAccesos()
        {
            InitializeComponent();
            unit = new UnitOfWork();
        }
    }
}