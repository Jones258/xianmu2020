﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mods
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WarehouseSystemEntities : DbContext
    {
        public WarehouseSystemEntities()
            : base("name=WarehouseSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<BreakageGL> BreakageGL { get; set; }
        public virtual DbSet<CChuRuBaosunProduct> CChuRuBaosunProduct { get; set; }
        public virtual DbSet<ChuBaoPanTuiTypes> ChuBaoPanTuiTypes { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientSite> ClientSite { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Ineventory> Ineventory { get; set; }
        public virtual DbSet<MeasureUnit> MeasureUnit { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<ProductGL> ProductGL { get; set; }
        public virtual DbSet<ProductSort> ProductSort { get; set; }
        public virtual DbSet<QuanXianFP> QuanXianFP { get; set; }
        public virtual DbSet<Refund> Refund { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Seating> Seating { get; set; }
        public virtual DbSet<SeatingTypes> SeatingTypes { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<StStorage> StStorage { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierTypes> SupplierTypes { get; set; }
        public virtual DbSet<SysFunction> SysFunction { get; set; }
        public virtual DbSet<WarehouseType> WarehouseType { get; set; }
    }
}
