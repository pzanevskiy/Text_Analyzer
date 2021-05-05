using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.DAL.UOW;

namespace Text_Analyzer.BL.Utils
{
    public class ModuleBL : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
        }
    }
}
