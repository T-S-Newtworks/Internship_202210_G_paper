﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Internship_Template.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DPContext : DbContext
    {
        public DPContext()
            : base("name=DPContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_CHART> T_CHART { get; set; }
        public virtual DbSet<T_DEPARTMENT> T_DEPARTMENT { get; set; }
        public virtual DbSet<T_DOCTOR> T_DOCTOR { get; set; }
        public virtual DbSet<T_HOSPITAL> T_HOSPITAL { get; set; }
        public virtual DbSet<T_KENGEN> T_KENGEN { get; set; }
        public virtual DbSet<T_PATIENT> T_PATIENT { get; set; }
        public virtual DbSet<USER> USER { get; set; }
    }
}
