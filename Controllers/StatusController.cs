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
    [Dou.Misc.Attr.MenuDef(Name = "狀態", MenuPath = "總務專區", Action = "Index", Index = 84, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class StatusController : Dou.Controllers.AGenericModelController<AssetStatus>
    {
        // GET: Status
        public ActionResult Index()
        {
            return View();
        }
        protected override void AddDBObject(IModelEntity<AssetStatus> dbEntity, IEnumerable<AssetStatus> objs)
        {
            base.AddDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllStatus();
            FtisHelperAsset.DB.StatusSelectItemsClassImp.ResetStatus();
        }
        protected override void UpdateDBObject(IModelEntity<AssetStatus> dbEntity, IEnumerable<AssetStatus> objs)
        {
            base.UpdateDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllStatus();
            FtisHelperAsset.DB.StatusSelectItemsClassImp.ResetStatus();
        }
        protected override void DeleteDBObject(IModelEntity<AssetStatus> dbEntity, IEnumerable<AssetStatus> objs)
        {
            base.DeleteDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllStatus();
            FtisHelperAsset.DB.StatusSelectItemsClassImp.ResetStatus();
        }
        protected override IModelEntity<AssetStatus> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetStatus>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetStatus>(new DouImp.Models.DouModelContextExt());
        }
    }
}