using Dou.Models.DB;
using DouHelper;
using DouImp.Models;
using FtisHelperAsset.DB.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;

namespace DouImp.Controllers
{
    [Dou.Misc.Attr.MenuDef(Name = "資訊資產明細", MenuPath = "總務專區", Action = "Index", Index = 87, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class ITAttributesController : Dou.Controllers.AGenericModelController<AssetITAttributes>
    {
        // GET: ITAttributes
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //public JsonResult UpdateObject(string AssetID, string ASN, string AOS, string AVer, string ACPU, string ARam, string ASSD, string AHDD)
        public JsonResult UpdateObject(AssetITAttributes Attr)
        {
            var sss = new AssetITAttributes()
            {
                AssetsAssetID = Attr.AssetsAssetID,
                SN = Attr.SN == null ? "" : Attr.SN,
                OS = Attr.OS == null ? "" : Attr.OS,
                OfficeVersion = Attr.OfficeVersion == null ? "" : Attr.OfficeVersion,
                Iserise = Attr.Iserise == null ? "" : Attr.Iserise,
                RAM = Attr.RAM == null ? "" : Attr.RAM,
                SSD = Attr.SSD == null ? "" : Attr.SSD,
                HDD = Attr.HDD == null ? "" : Attr.HDD,
            };

            FtisHelperAsset.DB.Helpe.Asset.AddOrUpdateAttrs(sss);
            return Json("YA");
        }

        protected override IModelEntity<AssetITAttributes> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetITAttributes>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetITAttributes>(new DouImp.Models.DouModelContextExt());
        }
    }
}