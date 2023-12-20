using Dou.Models.DB;
using DouImp.Models;
using FtisHelperAsset.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DouImp.Controllers
{
    [Dou.Misc.Attr.MenuDef(Name = "維修", MenuPath = "資產功能管理", Action = "Index", Index = 72, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class RepairController : Dou.Controllers.AGenericModelController<AssetRepairs>
    {
        // GET: Repairs
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<AssetRepairs> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetRepairs>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetRepairs>(new DouImp.Models.DouModelContextExt());
        }
    }
}