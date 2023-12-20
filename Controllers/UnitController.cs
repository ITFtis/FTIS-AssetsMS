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
    [Dou.Misc.Attr.MenuDef(Name = "資產單位", MenuPath = "總務專區", Action = "Index", Index = 85, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class UnitController : Dou.Controllers.AGenericModelController<AssetUnits>
    {
        // GET: Units
        public ActionResult Index()
        {
            return View();
        }
        protected override void AddDBObject(IModelEntity<AssetUnits> dbEntity, IEnumerable<AssetUnits> objs)
        {
            base.AddDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllUnits();
            FtisHelperAsset.DB.UnitSelectItemsClassImp.ResetUnits();
        }
        protected override void UpdateDBObject(IModelEntity<AssetUnits> dbEntity, IEnumerable<AssetUnits> objs)
        {
            base.UpdateDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllUnits();
            FtisHelperAsset.DB.UnitSelectItemsClassImp.ResetUnits();
        }
        protected override void DeleteDBObject(IModelEntity<AssetUnits> dbEntity, IEnumerable<AssetUnits> objs)
        {
            base.DeleteDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllUnits();
            FtisHelperAsset.DB.UnitSelectItemsClassImp.ResetUnits();
        }
        protected override IModelEntity<AssetUnits> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetUnits>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetUnits>(new DouImp.Models.DouModelContextExt());
        }
    }
}