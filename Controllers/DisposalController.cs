using Dou.Models.DB;
using DouImp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FtisHelperAsset.DB.Model;

namespace DouImp.Controllers
{
    [Dou.Misc.Attr.MenuDef(Name = "報廢", MenuPath = "資產功能管理", Action = "Index", Index = 70, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class DisposalController : Dou.Controllers.AGenericModelController<AssetDisposals>
    {
        // GET: Disposals
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<AssetDisposals> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetDisposals>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetDisposals>(new DouImp.Models.DouModelContextExt());
        }
    }
}