using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DouImp.Models
{
    [Table("User")]
    public class User : Dou.Models.UserBase { }
}
//    {
//        string SortedSet = typeof(FtisHelper.DB.Model.Department).AssemblyQualifiedName;
//        //public const string FtisHeplerAssemblyQualifiedName = "FtisHelper.DB.FtisModelContext, FtisHelper";
//        //public const string DepartmentAssemblyQualifiedName = "FtisHelper.DB.Model.Department, FtisHelper";
//        public User() : base()
//        {
//            Enabled = false;
//            Boss = false;
//        }
//        [Display(Name = "使用者名稱", Order = 1)]
//        [ColumnDef(Filter = true, FilterAssign = FilterAssignType.Contains)]
//        [Required]
//        [StringLength(50)]
//        public override string Name { get; set; }

//        [Display(Name = "密碼", Order = 0)]
//        [StringLength(80)] //System.Web.Helpers.Crypto.HashPassword會超過預設50
//        [Required]
//        [ColumnDef(Visible = false)]
//        public override string Password { get; set; }

//        [Required]
//        [StringLength(2)]
//        [Display(Name = "部門")]
//        [ColumnDef(VisibleEdit = true, Sortable = true, Filter = true, EditType = EditType.Select, SelectItemsClassNamespace = DepartmentSelectItemsClassImp.AssemblyQualifiedName)]
//        public string Dep { set; get; }

//        [StringLength(50)]
//        [ColumnDef(EditType = EditType.Email)]
//        public string EMail { set; get; }

//        [ColumnDef(EditType = EditType.Select, SelectItems = "{\"true\":\"是\",\"false\":\"否\"}")]
//        [Display(Name = "權責人員", Order = int.MaxValue)]
//        [Required]
//        public bool Boss { get; set; }

//        [ColumnDef(Filter = true, EditType = EditType.Select, SelectItems = "{\"true\":\"啟用\",\"false\":\"未啟用\"}")]
//        [Display(Name = "狀態", Order = int.MaxValue)]
//        public override bool? Enabled { get; set; }
//    }

//    public class DepartmentSelectItemsClassImp : SelectItemsClass
//    {
//        public const string AssemblyQualifiedName = "DouImp.Models.DepartmentSelectItemsClassImp, DouImp";

//        protected static IEnumerable<FtisHelper.DB.Model.Department> _deps;
//        protected static IEnumerable<FtisHelper.DB.Model.Department> DEPS
//        {
//            get
//            {
//                if (_deps == null)
//                {
//                    using (var fdb = FtisHelper.DB.FtisModelContext.Create())
//                    {
//                        _deps = fdb.Department.Where(s => s.DUse == "Y").ToArray();
//                    }
//                }
//                return _deps;
//            }
//        }

//        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
//        {
//            return DEPS.Select(s => new KeyValuePair<string, object>(s.DCode, s.DName));
//        }
//    }

//    public class DepartmentSelectItemsClassImpForConsultRecord : SelectItemsClass
//    {
//        public const string AssemblyQualifiedName = "DouImp.Models.DepartmentSelectItemsClassImpForConsultRecord, DouImp";

//        protected static IEnumerable<FtisHelper.DB.Model.Department> _deps;
//        protected static IEnumerable<FtisHelper.DB.Model.Department> DEPS
//        {
//            get
//            {
//                if (_deps == null)
//                {
//                    using (var fdb = FtisHelper.DB.FtisModelContext.Create())
//                    {
//                        _deps = fdb.Department.Where(s => s.DUse == "Y").ToArray();
//                        using (var db = new DouImp.Models.DouModelContextExt())
//                        {
//                            string[] exclude = new string[] { "01", "14", "15", "16", "17", "22", "99" };
//                            var cdeps = db.User.Where(s => s.Dep != null).Select(s => s.Dep).Distinct().ToArray().Where(s => !exclude.Contains(s));

//                            _deps = _deps.Where(s => cdeps.Contains(s.DCode));
//                        }
//                    }
//                }
//                return _deps;
//            }
//        }
//        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
//        {
//            return DEPS.Select(s => new KeyValuePair<string, object>(s.DCode, s.DName));
//        }
//    }
//}