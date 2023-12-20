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
    [Dou.Misc.Attr.MenuDef(Name = "主分類", MenuPath = "總務專區", Action = "Index", Index = 81, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class CategoryController : Dou.Controllers.AGenericModelController<AssetCategories>
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }
        protected override void AddDBObject(IModelEntity<AssetCategories> dbEntity, IEnumerable<AssetCategories> objs)
        {
            base.AddDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllCategories();
            FtisHelperAsset.DB.CatesSelectItemsClassImp.ResetCategories();
        }
        protected override void UpdateDBObject(IModelEntity<AssetCategories> dbEntity, IEnumerable<AssetCategories> objs)
        {
            base.UpdateDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllCategories();
            FtisHelperAsset.DB.CatesSelectItemsClassImp.ResetCategories();
        }
        protected override void DeleteDBObject(IModelEntity<AssetCategories> dbEntity, IEnumerable<AssetCategories> objs)
        {
            base.DeleteDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllCategories();
            FtisHelperAsset.DB.CatesSelectItemsClassImp.ResetCategories();
        }
        protected override IModelEntity<AssetCategories> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetCategories>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetCategories>(new DouImp.Models.DouModelContextExt());
        }
    }
}