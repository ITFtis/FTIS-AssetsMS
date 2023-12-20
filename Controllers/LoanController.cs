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
    [Dou.Misc.Attr.MenuDef(Name = "租借", MenuPath = "資產功能管理", Action = "Index", Index = 71, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class LoanController : Dou.Controllers.AGenericModelController<AssetLoans>
    {
        // GET: Loans
        public ActionResult Index()
        {
            return View();
        }
        protected override IModelEntity<AssetLoans> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetLoans>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetLoans>(new DouImp.Models.DouModelContextExt());
        }
    }
}