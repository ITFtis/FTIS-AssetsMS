using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using DouImp.Models;
using FtisHelperAsset.DB.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OfficeOpenXml;
using DouHelper;
using FtisHelperAsset.DB.Helpe;
using Newtonsoft.Json;
using FtisHelperAsset.DB;
using System.Security.Cryptography;
using System.Runtime.InteropServices.ComTypes;
using Dou.Misc.Attr;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace DouImp.Controllers
{
    [Dou.Misc.Attr.MenuDef(Name = "總表", MenuPath = "總務專區", Action = "Index", Index = 80, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    [Dou.Misc.Attr.AutoLogger(Status = LoggerEntity.LoggerDataStatus.All, Content = Dou.Misc.Attr.AutoLoggerAttribute.LogContent.AssignContent,
        AssignContent = "KEY:{FKey}, 字串:{FText}")]
    public class AssetController : Dou.Controllers.APaginationModelController<Assets>
    { 
        // GET: Assets
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InventoryRpt()
        {
            //Debug.WriteLine("測試訊息1：" + searchString);
            //Debug.WriteLine("測試訊息2：" + searchString2);

            var fileName1 = Server.MapPath("~/資產清單_v202210.xlsx");
            var fileName2 = Server.MapPath("~/") + "tmp/資產清單" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";

            FileInfo file1 = new FileInfo(fileName1);

            //確認報表範本檔案存在
            if (file1.Exists)
            {
                //存在，就複製一份
                file1.CopyTo(fileName2);


                // 關閉新許可模式通知
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // 沒設置的話會跳出 Please set the excelpackage.licensecontext property


                //開execl檔
                //using (ExcelPackage package = new ExcelPackage(new FileInfo(@"d:\test.xlsx"))) { }

                FileInfo file2 = new FileInfo(@fileName2);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                //載入Excel檔案
                using (ExcelPackage ep = new ExcelPackage(file2))
                {
                    //指定excel哪一個頁籤
                    ExcelWorksheet sheet1 = ep.Workbook.Worksheets[0];


                    //輸出view的欄位
                    //sheet1.Cells["R1"].Value = "【" + searchString + "】";
                    //sheet1.Cells["S1"].Value = "【" + searchString2 + "】";


                    //var sqltxt1 = @"SELECT top 15 b.Name,a.* FROM eqEquips a left join ftis_mis.dbo.cmmEmp b on a.MNO=b.Mno order by a.BUYDATE desc";

                    //var sqltxt1 = @"SELECT top 100 * FROM Assets order by PurchaseDate desc";
                    var sqltxt1 = @"SELECT [AssetID]
      ,[OrderNo]
      ,a.[Name]

	  ,f.Name as CateID

	  ,g.Name as SubCateID
      ,[Specification]
      ,[Quantity]

	  ,h.Name as Unit_ID
      ,[UnitPrice]
      ,[PurchasePrice]
      ,[PurchaseDate]
      ,[Warranty]
      ,[Durability]
      ,i.DName as CustodianDepName
	  ,b.Name as CustodianID

	  ,c.Name as LocationID
      ,[SupplierID]

	  ,d.Name as RecorderID

	  ,e.Name as StatusID
      ,[IsIT]
  FROM [ASSETSYS].[dbo].[Assets] a
  left join [FTIS-T8].[dbo].[F22cmmEmpData] b on a.[CustodianID] = b.[Fno]
  left join [ASSETSYS].[dbo].[AssetLocations] c on a.[LocationID] = c.[ID]
  left join [FTIS-T8].[dbo].[F22cmmEmpData] d on a.[RecorderID] = d.[Fno]
  left join [ASSETSYS].[dbo].[AssetStatus] e on a.[StatusID] = e.[StatusID] 
  left join [ASSETSYS].[dbo].[AssetCategories] f on a.[CateID] = f.[CateID]
  left join [ASSETSYS].[dbo].[AssetSubCategories] g on a.[SubCateID] = g.[SubCateID]
  left join [ASSETSYS].[dbo].[AssetUnits] h on a.[Unit_ID] = h.[UnitID]
  left join [FTIS-T8].[dbo].[F22cmmDep] i on b.[DCode] = i.[DCode]";
                    //var sqltxt1 = @"SELECT * FROM eqEquips order by SCRDATE desc,BUYDATE desc";

                    //var rs1 = new DouImp.Models.FtisModelContext().Database.SqlQuery<cmmEmp>(sqltxt1).ToArray();
                    var rs1 = new FtisHelperAsset.DB.FtisAssetModelContext().Database.SqlQuery<AssetVM>(sqltxt1).ToList();

                    //var tmpaa = @ViewData.ModelState["EQNO"].Value;
                    //string tmpaa = @ViewData.form["EQNO"].Value;
                    //string tmpaa = "";

                    //rs1.Select(e => e.Mno + "_" + e.Quit.ToString()).ToList();
                    var ii = 2;
                    foreach (var rows in rs1)
                    {
                        //資產編號
                        sheet1.Cells["A" + ii].Value = rows.AssetID;
                        //請採購單號
                        sheet1.Cells["B" + ii].Value = rows.OrderNo;
                        //M365代碼
                        sheet1.Cells["C" + ii].Value = "";
                        //資產名稱
                        sheet1.Cells["D" + ii].Value = rows.Name;
                        //規格型號
                        sheet1.Cells["E" + ii].Value = rows.Specification;
                        //數量
                        sheet1.Cells["F" + ii].Value = rows.Quantity;
                        //單位
                        sheet1.Cells["G" + ii].Value = rows.Unit_ID;
                        //單價(未稅)
                        sheet1.Cells["H" + ii].Value = rows.UnitPrice;
                        //部門
                        sheet1.Cells["I" + ii].Value = rows.CustodianDepName;
                        //保管人
                        sheet1.Cells["J" + ii].Value = rows.CustodianID;
                        //所在地點
                        sheet1.Cells["K" + ii].Value = rows.LocationID;
                        //購入日期
                        sheet1.Cells["L" + ii].Value = string.Format("{0:yyyy/MM/dd}", rows.PurchaseDate);
                        //供應商
                        sheet1.Cells["M" + ii].Value = rows.SupplierID;
                        //取得金額
                        sheet1.Cells["N" + ii].Value = rows.PurchasePrice;
                        //保固期
                        sheet1.Cells["O" + ii].Value = rows.Warranty;
                        //耐用年限
                        sheet1.Cells["P" + ii].Value = rows.Durability;
                        //登錄人員
                        sheet1.Cells["Q" + ii].Value = rows.RecorderID;
                        //備註1(待補)
                        sheet1.Cells["R" + ii].Value = "";
                        //異動日期(待補)
                        sheet1.Cells["S" + ii].Value = "";
                        //盤點日期(待補)
                        sheet1.Cells["T" + ii].Value = "";
                        //現況
                        sheet1.Cells["U" + ii].Value = rows.StatusID;
                        //報廢日期(待補)
                        sheet1.Cells["V" + ii].Value = "";
                        //備註2(待補)
                        sheet1.Cells["W" + ii].Value = "";



                        //if (rows.CustodianID != "")
                        //{
                        //    var sqltxt2 = @"SELECT * FROM F22cmmDep where Fno='" + rows.CustodianID + "'";
                        //    //sheet1.Cells["F1"].Value = sqltxt2;

                        //    var rs2 = new FtisHelperV2.DB.FtisT8ModelContext().Database.SqlQuery<F22cmmDep>(sqltxt2).ToArray();
                        //    foreach (var row2 in rs2)
                        //    { sheet1.Cells["J" + ii].Value = row2.DName; }
                        //    //rs2.Clear();
                        //}
                        //else { sheet1.Cells["J" + ii].Value = rows.MNO; }
                        ////sheet1.Cells["J" + ii].Value = rows.MNO;
                        ii++;

                    }


                    //var aa = Convert.ToString(rs1["Mno"]);
                    //sheet1.Cells["S8"].Value = aa;

                    //秀出後端操作者
                    //sheet1.Cells["A1"].Value = UserController.CurrentFtisEmployee.Department.DName;
                    //sheet1.Cells["B1"].Value = UserController.CurrentFtisEmployee.Name;
                    //sheet1.Cells["C1"].Value = "工程師";
                    //sheet1.Cells["D1"].Value = sheet1.Cells["D1"].Value + "[" + tmpaa + "]";


                    //classSession = dataReader["classSession"].ToString();
                    //Id = reader.GetInt32(reader.GetOrdinal("id"))

                    //保存excel
                    ep.Save();
                    rs1.Clear();
                }
                //回傳報表給user
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = file2.Name,
                    Inline = false,
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());
                Response.BufferOutput = false;
                var rptfilename = new FileStream(file2.FullName, FileMode.Open, FileAccess.Read);
                string contentType = MimeMapping.GetMimeMapping(file2.FullName);
                //Response.Write("<script language=javascript>alert(' 檔案下載完成 ');</script>");
                return File(rptfilename, contentType);
            }
            else
            {
                Response.Write("<script language=javascript>alert('原始檔案不存在!!');</" + "script>");
                return View();
                //return RedirectToAction("Upload");
            }
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var options = base.GetDataManagerOptions();
            options.editformWindowStyle = "modal";
            options.editformWindowClasses = "modal-xl";
            options.editformSize.height = "fixed";
            options.editformSize.width = "auto";
            options.GetFiled("CustodianDep").editable = true;
            //options.GetFiled("DCode_").visibleEdit = false;
            return options;
        }

        //protected override IEnumerable<Assets> GetDataDBObject(IModelEntity<Assets> dbEntity, params KeyValueParams[] paras)
        //{
        ////    string userid = Dou.Context.CurrentUser<User>().Id;
        ////    var tmpd1 = base.GetDataDBObject(dbEntity, paras) as IQueryable<Assets>;
        ////    foreach (var obj in tmpd1)
        ////    {
        ////        if (obj.
        ////    }
        ////        tmpd1.Where(x => x.StatusID != "S003" && x.StatusID != "S004");
        //    return (base.GetDataDBObject(dbEntity, paras) as IQueryable<Assets>);
        //}

        protected override IQueryable<Assets> BeforeIQueryToPagedList(IQueryable<Assets> iquery, params KeyValueParams[] paras)
        {
            iquery = base.BeforeIQueryToPagedList(iquery, paras);
            var dep = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "CustodianDep");
            if (dep != null)
            {
                var us = FtisHelperAsset.DB.Helpe.Employee.GetAllEmployee().Where(s => s.DCode == dep).Select(s => s.Fno);
                iquery = iquery.Where(s => us.Contains(s.CustodianID));
            }

            //    var filters = paras.FirstOrDefault(s => s.key == "filter");
            //    var sorts = paras.FirstOrDefault(s => s.key == "sort");
            //    //var fno = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "Fno");
            //    //if (fno == null)//有選員編就不考慮部門
            //    //{
            //    //    var dep = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "Dep");
            //    //    if (dep != null)
            //    //    {
            //    //        var us = FtisHelperV2.DB.Helper.GetAllEmployee().Where(s => s.DCode == dep).Select(s => s.Fno);
            //    //        iquery = iquery.Where(s => us.Contains(s.Fno));
            //    //    }
            //    //}
            //    if (sorts.value.ToString() == "InventoryTime")
            //        iquery = iquery.OrderByDescending(s => s.InventoryTime);
            //    //bool ss = string.IsNullOrEmpty(paras.FirstOrDefault(s => s.key == "sort").value + "");
            //    //if (string.IsNullOrEmpty(paras.FirstOrDefault(s => s.key == "sort").value + ""))
            //    //    iquery = iquery.OrderByDescending(s => s.InventoryTime);
            return iquery;
        }

        protected override void AddDBObject(IModelEntity<Assets> dbEntity, IEnumerable<Assets> objs)
        {
            foreach (var obj in objs)
            {
                obj.RecorderID = Dou.Context.CurrentUser<User>().Id;
                obj.StatusID = "S001"; //20230424, ADD預設未盤點(S001) by markhong
            }
            //dbEntity.Update(objs);

            base.AddDBObject(dbEntity, objs);

            FtisHelperAsset.DB.Helpe.Asset.ResetGetAssetInventoryLogTime();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
            FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        }

        protected override void UpdateDBObject(IModelEntity<Assets> dbEntity, IEnumerable<Assets> objs)
        {
            var obj = objs.FirstOrDefault();
            var mObj = dbEntity.FirstOrDefault((m) => m.AssetID == obj.AssetID);

            //20231219, add by markhong 新增狀態判斷
            if (obj.StatusID == "S002" && mObj.StatusID == "S001")
            {
                //寫一筆新紀錄到[AssetInventoryLog]
                BarCodeController.AddInventorylog(obj.AssetID, "盤點成功", "K");
            };


            obj.UsageLog = obj.UsageLog == null　?　new List<AssetUsageLog>(): obj.UsageLog;
            obj.ITAttr = obj.ITAttr == null ? new List<AssetITAttributes>() : obj.ITAttr;

            var reason = obj.CustodianID != mObj.CustodianID ? "保管人異動" : "";
            //檢驗mobj(DB)與objs(UI)的CustodianID是否一致，若不同則寫入一筆log to AssetUsegLog
            if (obj.CustodianID != mObj.CustodianID && mObj.CustodianID != null)
            {
                reason = "保管人異動";
                var sss = new AssetUsageLog()
                {
                    CustodianSourceID = mObj.CustodianID.ToString(),
                    CustodianTargetID = obj.CustodianID,
                    AssetsAssetID = obj.AssetID,
                    UpdateMan = Dou.Context.CurrentUser<User>().Id,
                    UpdateTime = DateTime.Now,
                    ModifiyReason = reason,
                    LocationID = obj.LocationID,
                    Notes = "-",
                };
                obj.UsageLog.Add(sss);
            }

            //取DB現有資料，需視Master-Deatil載入(積極、消極)方式
            //以下僅用於積極載入
            //var mObj = dbEntity.Find(Dou.Misc.HelperUtilities.GetKeyValues<Master>(obj, ((ModelEntity<Master>)dbEntity)._context));
            //以下用於積極、消極載入皆可
            var nObj = dbEntity.FirstOrDefault((n) => n.AssetID == obj.AssetID, (n) => n.UsageLog, (n) => n.ITAttr);
            //var nObj = dbEntity.FirstOrDefault((n) => n.AssetID == obj.AssetID, (n) => n.UsageLog);

            //從現在資料依obj.Details、mObj.Details取預存於DB details及EntityState
            //20230322, 如果有某一Col為null，會報錯-->[並未將物件參考設定為物件的執行個體。]
            obj.UsageLog = HelperUtilities.MergeEntityState<AssetUsageLog>(((ModelEntity<Assets>)dbEntity)._context, obj.UsageLog, nObj.UsageLog) as ICollection<AssetUsageLog>;
            obj.ITAttr = HelperUtilities.MergeEntityState<AssetITAttributes>(((ModelEntity<Assets>)dbEntity)._context, obj.ITAttr, nObj.ITAttr) as ICollection<AssetITAttributes>;

            base.UpdateDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAssetInventoryLogTime();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
            FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        }


        protected override void DeleteDBObject(IModelEntity<Assets> dbEntity, IEnumerable<Assets> objs)
        {
            //如消極載入detail，且DB schema無建關聯則須額外刪detail
            base.DeleteDBObject(dbEntity, objs);
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAssetInventoryLogTime();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
            FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        }

        protected override IModelEntity<Assets> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<Assets>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
        }
    }

    public class AssetVM
    {
        public string AssetID { get; set; }
        public string OrderNo { get; set; }
        public string Name { get; set; }
        public string CateID { get; set; }
        public string SubCateID { get; set; }
        public string Specification { get; set; }
        public int? Quantity { get; set; }
        public string Unit_ID { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? PurchasePrice { get; set; }
        public virtual string CustodianDepName { get; set; }
        public string CustodianID { get; set; }
        public string LocationID { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string SupplierID { get; set; }
        public string Warranty { get; set; }
        public string Durability { get; set; }
        public string RecorderID { get; set; }
        public DateTime? InventoryTime { get; set; }
        public string StatusID { get; set; }
        public bool? IsIT { get; set; }
    }
}