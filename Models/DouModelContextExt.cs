using System;
using System.Data.Entity;
using System.Linq;

namespace DouImp.Models
{
    public class DouModelContextExt : Dou.Models.ModelContextBase<User, Role>
    {
        public DouModelContextExt() : base("name=DouModelContextExt")
        {
            Database.SetInitializer<DouModelContextExt>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<F22cmmEmpData>().HasOptional(s=>s.Seat);
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AssetCategories>()
        //        .Property(e => e.CateID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetCategories>()
        //        .Property(e => e.Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetCategories>()
        //        .Property(e => e.Description)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetDisposals>()
        //        .Property(e => e.Reason)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetITAttributes>()
        //        .Property(e => e.AssetID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetITAttributes>()
        //        .Property(e => e.OS)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetITAttributes>()
        //        .Property(e => e.Iserise)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetLoans>()
        //        .Property(e => e.BorrowerID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetLocations>()
        //        .Property(e => e.ID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetRepairs>()
        //        .Property(e => e.Description)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetRepairs>()
        //        .Property(e => e.Cost)
        //        .HasPrecision(19, 4);

        //    modelBuilder.Entity<Assets>()
        //        .Property(e => e.AssetID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Assets>()
        //        .Property(e => e.CateID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Assets>()
        //        .Property(e => e.UnitPrice)
        //        .HasPrecision(19, 4);

        //    modelBuilder.Entity<Assets>()
        //        .Property(e => e.PurchasePrice)
        //        .HasPrecision(19, 4);

        //    modelBuilder.Entity<Assets>()
        //        .Property(e => e.Unit_ID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Assets>()
        //        .Property(e => e.SN)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Assets>()
        //        .Property(e => e.CustodianID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Assets>()
        //        .Property(e => e.SupplierID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<Assets>()
        //        .Property(e => e.StatusID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetStatus>()
        //        .Property(e => e.StatusID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetSubCategories>()
        //        .Property(e => e.SubCateID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetSubCategories>()
        //        .Property(e => e.CateID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetSuppliers>()
        //        .Property(e => e.ID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetSuppliers>()
        //        .Property(e => e.Phone)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetUnits>()
        //        .Property(e => e.UnitID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetUsageLog>()
        //        .Property(e => e.AssetID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetUsageLog>()
        //        .Property(e => e.CustodianSourceID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetUsageLog>()
        //        .Property(e => e.CustodianTargetID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AssetUsageLog>()
        //        .Property(e => e.LocationID)
        //        .IsUnicode(false);
        //}
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}