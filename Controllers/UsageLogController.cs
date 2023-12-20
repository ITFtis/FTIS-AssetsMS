using Dou.Controllers;
using Dou.Models.DB;
using DouImp.Models;
using FtisHelperAsset.DB.Helpe;
using FtisHelperAsset.DB.Model;
using FtisHelperAsset.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DouImp.Controllers
{
    public class UsageLogController : Dou.Controllers.AGenericModelController<AssetUsageLog>
    {
        // GET: UsageLog
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 更新異動者與異動時間
        /// </summary>
        /// <param name="dbEntity"></param>
        /// <param name="objs"></param>
        protected override void UpdateDBObject(IModelEntity<AssetUsageLog> dbEntity, IEnumerable<AssetUsageLog> objs)
        {
            //20230503, edit 只開放修改備註 by markhong
            //foreach (var obj in objs)
            //{
            //    obj.UpdateMan = Dou.Context.CurrentUser<User>().Id;
            //    obj.UpdateTime = DateTime.Now;
            //}
            dbEntity.Update(objs);
        }

        protected override IModelEntity<AssetUsageLog> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<AssetUsageLog>(FtisHelperAsset.DB.Helper.CreateFtisAssetModelContext());
            //return new Dou.Models.DB.ModelEntity<AssetUsageLog>(new DouImp.Models.DouModelContextExt());
        }



        //public override Task<ActionResult> GetData(params KeyValueParams[] paras)
        //{
        //    return base.GetData(paras);
        //}
        //protected override void UpdateDBObject(IModelEntity<AssetUsageLog> dbEntity, IEnumerable<AssetUsageLog> objs)
        //{
        //    base.UpdateDBObject(dbEntity, objs);
        //}
        //protected override void AddDBObject(IModelEntity<AssetUsageLog> dbEntity, IEnumerable<AssetUsageLog> objs)
        //{
        //    base.AddDBObject(dbEntity, objs);
        //}
    }
}