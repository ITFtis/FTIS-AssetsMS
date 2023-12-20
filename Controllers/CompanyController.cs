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
    [Dou.Misc.Attr.MenuDef(Name = "公司", MenuPath = "總務專區", Action = "Index", Index = 89, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class CompanyController : Dou.Controllers.AGenericModelController<AssetCompanies>
    {
        // GET: Status
        public ActionResult Index()
        {
            return View();
        }
        protected override void AddDBObject(IModelEntity<AssetCompanies> dbEntity, IEnumerable<AssetCompanies> objs)
        {
            base.AddDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllCompanies();
            FtisHelperAsset.DB.CompanySelectItemsClassImp.ResetCompanies();
        }
        protected override void UpdateDBObject(IModelEntity<AssetCompanies> dbEntity, IEnumerable<AssetCompanies> objs)
        {
            base.UpdateDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllCompanies();
            FtisHelperAsset.DB.CompanySelectItemsClassImp.ResetCompanies();
        }
        protected override void DeleteDBObject(IModelEntity<AssetCompanies> dbEntity, IEnumerable<AssetCompanies> objs)
        {
            base.DeleteDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllCompanies();
            FtisHelperAsset.DB.CompanySelectItemsClassImp.ResetCompanies();
        }
        protected override IModelEntity<AssetCompanies> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetCompanies>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetStatus>(new DouImp.Models.DouModelContextExt());
        }
    }
}