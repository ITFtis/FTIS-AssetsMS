using Dou.Controllers;
using Dou.Misc;
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
    [Dou.Misc.Attr.MenuDef(Name = "個人資產資料", MenuPath = "", Action = "Index", Index = 60, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class UserAssetController : Dou.Controllers.AGenericModelController<Assets>
    {
        string userid = Dou.Context.CurrentUser<User>().Id;
        // GET: UserAsset
        public ActionResult Index()
        {
            return View();
        }
        //protected override void AddDBObject(IModelEntity<Assets> dbEntity, IEnumerable<Assets> objs)
        //{
        //    base.AddDBObject(dbEntity, objs);
        //    FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
        //    FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        //}
        //protected override void UpdateDBObject(IModelEntity<Assets> dbEntity, IEnumerable<Assets> objs)
        //{
        //    base.UpdateDBObject(dbEntity, objs);
        //    FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
        //    FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        //}
        //protected override void DeleteDBObject(IModelEntity<Assets> dbEntity, IEnumerable<Assets> objs)
        //{
        //    base.DeleteDBObject(dbEntity, objs);
        //    FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
        //    FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        //}
        protected override IEnumerable<Assets> GetDataDBObject(IModelEntity<Assets> dbEntity, params KeyValueParams[] paras)
        {
            return (base.GetDataDBObject(dbEntity, paras) as IQueryable<Assets>).Where(o => o.CustodianID == userid);
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var options = base.GetDataManagerOptions();
            //options.GetFiled("CateID").visible = false;
            //options.GetFiled("CateID").visibleEdit = false;
            //options.GetFiled("SubCateID").visible = false;
            //options.GetFiled("SubCateID").visibleEdit = false;
            options.GetFiled("UnitPrice").visible = false;
            options.GetFiled("UnitPrice").visibleEdit = false;
            options.GetFiled("PurchasePrice").visible = false;
            options.GetFiled("PurchasePrice").visibleEdit = false;
            //options.GetFiled("PurchaseDate").visible = false;
            //options.GetFiled("PurchaseDate").visibleEdit = false;
            options.GetFiled("Warranty").visible = false;
            options.GetFiled("Warranty").visibleEdit = false;
            options.GetFiled("Durability").visible = false;
            options.GetFiled("Durability").visibleEdit = false;
            options.GetFiled("SupplierID").visible = false;
            options.GetFiled("SupplierID").visibleEdit = false;
            options.GetFiled("OrderNo").visible = false;
            options.GetFiled("OrderNo").visibleEdit = false;
            options.GetFiled("RecorderID").visible = false;
            options.GetFiled("RecorderID").visibleEdit = false;

            options.singleDataEdit = false;
            //options.singleDataEditCompletedReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer == null ?
            //    Dou.Context.CurrentUser<User>().DefaultPage : System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
            //options.datas = new User[] { Dou.Context.CurrentUser<User>() };
            var dbe = GetModelEntity();
            options.datas = dbe.GetAll(s => s.CustodianID == userid);
            //(Dou.Misc.HelperUtilities.GetKeyValues<F22cmmEmpData>(((Dou.Models.DB.ModelEntity<F22cmmEmpData>)dbe)._context) };
            //options.editformWindowStyle = "showEditformOnly";
            return options;
        }
        protected override IModelEntity<Assets> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<Assets>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<Assets>(new DouImp.Models.DouModelContextExt());
        }
    }
}