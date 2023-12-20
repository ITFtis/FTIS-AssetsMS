using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using DouImp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DouImp.Controllers
{
    [Dou.Misc.Attr.MenuDef(Name = "記錄查詢", MenuPath = "系統管理", Action = "Index", Index = 92, Func = Dou.Misc.Attr.FuncEnum.Delete, AllowAnonymous = false)]
    public class LoggerController : Dou.Controllers.LoggerBaseController<Dou.Models.Logger>
    {
        // GET: Logger
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<Dou.Models.Logger> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<Dou.Models.Logger>(new DouModelContextExt());
        }
    }

}