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
    [Dou.Misc.Attr.MenuDef(Name = "所在位置", MenuPath = "總務專區", Action = "Index", Index = 83, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class LocationController : Dou.Controllers.AGenericModelController<AssetLocations>
    {
        // GET: Locations
        public ActionResult Index()
        {
            return View();
        }
        protected override void AddDBObject(IModelEntity<AssetLocations> dbEntity, IEnumerable<AssetLocations> objs)
        {
            base.AddDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllLocations();
            FtisHelperAsset.DB.LocationsSelectItemsClassImp.ResetLocations();
        }
        protected override void UpdateDBObject(IModelEntity<AssetLocations> dbEntity, IEnumerable<AssetLocations> objs)
        {
            base.UpdateDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllLocations();
            FtisHelperAsset.DB.LocationsSelectItemsClassImp.ResetLocations();
        }
        protected override void DeleteDBObject(IModelEntity<AssetLocations> dbEntity, IEnumerable<AssetLocations> objs)
        {
            base.DeleteDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllLocations();
            FtisHelperAsset.DB.LocationsSelectItemsClassImp.ResetLocations();
        }
        protected override IModelEntity<AssetLocations> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetLocations>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetLocations>(new DouImp.Models.DouModelContextExt());
        }
    }
}