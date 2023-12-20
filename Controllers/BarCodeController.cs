using Dou.Models.DB;
using DouImp.Models;
using FtisHelperAsset.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using ZXing;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using FTIS_BarCode.Models;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Data.Entity;
using FtisHelperAsset.DB;
using System.Security.Cryptography;
using FtisHelperAsset.DB.Helpe;
using System.Data;

namespace DouImp.Controllers
{
    [Dou.Misc.Attr.MenuDef(Name = "盤點", MenuPath = "", Action = "Index", Index = 50, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]

    public class BarCodeController : Dou.Controllers.AGenericModelController<AssetInventoryLog>
    {
        //public static DouModelContextExt context = new DouModelContextExt();
        public static string devicedName = "";
        // GET: BarCode
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 接收與回傳資料
        /// </summary>
        /// <param name="base64image"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveImage(string base64image, string txBstr)
        {
            if (txBstr == "")
                return Json(VideoCaptureDevice_NewFrameAsync(base64image));
          
            return Json(ConsoleResult(txBstr, "K"));
        }

        /// <summary>
        /// 接收與回傳資料
        /// </summary>
        /// <param name="base64image"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult checkBarcode(string strBarcode, string txBstr)
        {
            if (txBstr == "")
                return Json(ConsoleResult(strBarcode.Trim(), "B"));

            return Json(ConsoleResult(txBstr.Trim(), "K"));
        }

        /// <summary>
        /// 驗證輸入字串格式(暫留)
        /// </summary>
        /// <returns></returns>
        private bool ckecktxBstr(string txBstr)
        {
            if (txBstr.Length == 13)
            {
                if (txBstr.IndexOf("-") < 0)
                    return true;
            }

            if (txBstr.Length == 14)
            {
                if (txBstr.IndexOf("-") >0)
                {
                    if (txBstr.Substring(0,txBstr.IndexOf("-")).Length == 5 && txBstr.Substring(txBstr.IndexOf("-")+1, txBstr.Length).Length == 7)
                        return true;
                }    
            }
            return false;
        }

        /// <summary>
        /// 呼叫API影像處理
        /// </summary>
        /// <param name="bas64image"></param>
        /// <returns></returns>
        private string VideoCaptureDevice_NewFrameAsync(string bas64image)
        {
            string[] bas64image_ls = bas64image.Split(',');
            using (var client = new HttpClient())
            {
                //設定Header - Accept的資料型別
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //設定需傳遞物件及呼叫網址
                var apipar = new API() { imgeBase64 = bas64image_ls[1] };
                var postTask = client.PostAsJsonAsync("http://120.100.100.227:8089/FtisBarcode/Barcode/decode", apipar);
                postTask.Wait();

                //成功回傳Result
                //AddInventorylog("30905-A108101");
                //return AssetData("30905-A108101");
                //return (ConsoleResult("30907-A108026", "B"));
                if (postTask.Result.IsSuccessStatusCode)
                {
                    var res = postTask.Result.Content.ReadAsStringAsync();
                    return (ConsoleResult(res.Result.ToString(),"B"));
                }

                return "圖片清晰度不足";
            }
        }

        private string ConsoleResult(string assetid, string inputstr)
        {
            var _assetid = assetid.IndexOf("-") > -1 ? assetid.Replace("-", "") : assetid;
            //20230807, add by markhong 掃描機會掃出尾數是逗點或句點
            if (_assetid.Length > 12)
                _assetid = _assetid.Substring(0, 12);
            var rsAD = (from c in new FtisAssetModelContext().Assets
                       where c.AssetID.Replace("-", "") == _assetid
                       select c).ToList();       
            var reslog = rsAD.Count > 0 ? rsAD[0].AssetID.ToString() : "";
            
            //回傳資產資料 
            //var reslog = AssetData(_assetid);
            var reslog2 = reslog == "" ? "查無該資產資料" : "盤點成功";

            //log紀錄
            AddInventorylog(assetid, reslog2, inputstr);

            //更新Asset Status
            if (reslog2 == "盤點成功")
                UpdateAssetStatus(rsAD, assetid, reslog2);

            return (string.Format("{0} {1}",assetid,reslog2));
        }

        /// <summary>
        /// 新增Barcode紀錄於AssetInventoryLog
        /// </summary>
        /// <param name="strBarcode"></param>
        public static void AddInventorylog(string strBarcode, string reslog, string strInput)
        {
            var Invtlog = new AssetInventoryLog()
            {
                AssetID = strBarcode.Trim(),
                InventoryFno = Dou.Context.CurrentUser<User>().Id,
                InventoryTime = DateTime.Now,
                InventoryYear = DateTime.Now.Year.ToString(),
                InventoryInput = strInput,
                InventoryMemo = reslog,
            };

            FtisHelperAsset.DB.Helpe.Asset.AddInventoryLog(Invtlog);

            FtisHelperAsset.DB.Helpe.Asset.ResetGetAssetInventoryLogTime();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssetInventoryLog();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
            FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        }

        /// <summary>
        /// 更新Asset的Status資料
        /// </summary>
        /// <param name="strBarcode"></param>
        private void UpdateAssetStatus(List<Assets> rsAsset, string strBarcode, string reslog)
        {           
            //找出該資產紀錄
            if (rsAsset.Count == 0) return;
            string nowYYYY = DateTime.Now.Year.ToString();
            //狀態不為待報廢(S003)或已報廢(S004)
            if (rsAsset[0].StatusID != "S003" && rsAsset[0].StatusID != "S004")
            {
                var _assetid = strBarcode.IndexOf("-") > 0 ? strBarcode.Replace("-", "") : strBarcode;
                //找出該資產所有的盤點紀錄
                //盤點時間否為今年
                var rsInventorylog = (from c in new FtisAssetModelContext().AssetInventoryLog.AsEnumerable()
                                      where c.AssetID.Replace("-", "") == _assetid &&
                                            c.InventoryTime.ToString("yyyy") == nowYYYY
                                      orderby c.InventoryTime descending
                                      select c).ToList();
                //若有現年的盤點紀錄則更新該資產的狀態(Status) = 已盤點(S002)
                if (rsInventorylog.Count > 0)
                {
                    rsAsset[0].StatusID = "S002";
                    //20230727, 盤點如果沒有保管人更新會出錯
                    if (rsAsset[0].CustodianID == null) 
                        rsAsset[0].CustodianID = "F00007";
                    FtisHelperAsset.DB.Helpe.Asset.UpdateAssetStatus(rsAsset);
                }
            }

            FtisHelperAsset.DB.Helpe.Asset.ResetGetAssetInventoryLogTime();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssetInventoryLog();
            FtisHelperAsset.DB.Helpe.Asset.ResetGetAllAssets();
            FtisHelperAsset.DB.AssetIDSelectItemsClassImp.ResetAssets();
        }

        /// <summary>
        /// 取得資產資料
        /// </summary>
        /// <param name="strBarcode"></param>
        /// <returns></returns>
        private string AssetData(string strBarcode)
        {
            //盤點程序
            var rsa = (from c in new FtisAssetModelContext().Assets
                       where c.AssetID.Replace("-","") == strBarcode
                       select c).ToList();
            if (rsa.Count > 0)
            {
                //string Prodlog = "資產名稱," + rsa[0].Name.ToString();
                //Prodlog += ",購買日期," + rsa[0].PurchaseDate.Value.Year.ToString() + "年,";
                //Prodlog += rsa[0].PurchaseDate.Value.Month.ToString() + "月,";
                //Prodlog += rsa[0].PurchaseDate.Value.Day.ToString() + "日";
                string Prodlog = rsa[0].AssetID.ToString();
                return Prodlog;
            }
            return "";
        }
        protected override IModelEntity<AssetInventoryLog> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetInventoryLog>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetITAttributes>(new DouImp.Models.DouModelContextExt());
        }
    }
}