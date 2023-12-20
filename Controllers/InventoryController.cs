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
    [Dou.Misc.Attr.MenuDef(Name = "盤點紀錄", MenuPath = "總務專區", Action = "Index", Index = 88, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class InventoryController : Dou.Controllers.AGenericModelController<AssetInventoryLog>
    {
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }

        protected override void AddDBObject(IModelEntity<AssetInventoryLog> dbEntity, IEnumerable<AssetInventoryLog> objs)
        {
            base.AddDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssetInventoryLog();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
            FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        }
        protected override void UpdateDBObject(IModelEntity<AssetInventoryLog> dbEntity, IEnumerable<AssetInventoryLog> objs)
        {
            base.UpdateDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssetInventoryLog();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
            FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        }
        protected override void DeleteDBObject(IModelEntity<AssetInventoryLog> dbEntity, IEnumerable<AssetInventoryLog> objs)
        {
            base.DeleteDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssetInventoryLog();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
            FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        }
        protected override IModelEntity<AssetInventoryLog> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetInventoryLog>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetInventories>(new DouImp.Models.DouModelContextExt());
        }
    }
}