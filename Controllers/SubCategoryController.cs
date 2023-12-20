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
    [Dou.Misc.Attr.MenuDef(Name = "次分類", MenuPath = "總務專區", Action = "Index", Index = 82, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class SubCategoryController : Dou.Controllers.AGenericModelController<AssetSubCategories>
    {
        // GET: SubCategories
        public ActionResult Index()
        {
            return View();
        }
        protected override void AddDBObject(IModelEntity<AssetSubCategories> dbEntity, IEnumerable<AssetSubCategories> objs)
        {
            base.AddDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllSubCategories();
            FtisHelperAsset.DB.SubCatesSelectItemsClassImp.ResetSubCategories();
        }
        protected override void UpdateDBObject(IModelEntity<AssetSubCategories> dbEntity, IEnumerable<AssetSubCategories> objs)
        {
            base.UpdateDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllSubCategories();
            FtisHelperAsset.DB.SubCatesSelectItemsClassImp.ResetSubCategories();
        }
        protected override void DeleteDBObject(IModelEntity<AssetSubCategories> dbEntity, IEnumerable<AssetSubCategories> objs)
        {
            base.DeleteDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllSubCategories();
            FtisHelperAsset.DB.SubCatesSelectItemsClassImp.ResetSubCategories();
        }
        protected override IModelEntity<AssetSubCategories> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetSubCategories>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetSubCategories>(new DouImp.Models.DouModelContextExt());
        }
    }
}