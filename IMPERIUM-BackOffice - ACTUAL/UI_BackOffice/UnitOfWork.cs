using BL_BackOffice;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_BackOffice
{
    public class UnitOfWork : blUnitOfWork { public UnitOfWork() : base(Program.Sesion.Acceso.Key) { } }
}
