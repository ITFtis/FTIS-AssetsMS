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
    [Dou.Misc.Attr.MenuDef(Name = "供應商", MenuPath = "總務專區", Action = "Index", Index = 86, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class SupplierController : Dou.Controllers.AGenericModelController<AssetSuppliers>
    {
        // GET: Suppliers
        public ActionResult Index()
        {
            return View();
        }
        protected override void AddDBObject(IModelEntity<AssetSuppliers> dbEntity, IEnumerable<AssetSuppliers> objs)
        {
            base.AddDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllSuppliers();
            FtisHelperAsset.DB.SuppliersSelectItemsClassImp.ResetSuppliers();
        }
        protected override void UpdateDBObject(IModelEntity<AssetSuppliers> dbEntity, IEnumerable<AssetSuppliers> objs)
        {
            base.UpdateDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllSuppliers();
            FtisHelperAsset.DB.SuppliersSelectItemsClassImp.ResetSuppliers();
        }
        protected override void DeleteDBObject(IModelEntity<AssetSuppliers> dbEntity, IEnumerable<AssetSuppliers> objs)
        {
            base.DeleteDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllSuppliers();
            FtisHelperAsset.DB.SuppliersSelectItemsClassImp.ResetSuppliers();
        }
        protected override IModelEntity<AssetSuppliers> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetSuppliers>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetSuppliers>(new DouImp.Models.DouModelContextExt());
        }
    }
}