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
    
    public partial class Internship_Context : DbContext
    {
        public Internship_Context()
            : base("name=Internship_Context")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_LOGIN> T_LOGIN { get; set; }
        public virtual DbSet<T_USER> T_USER { get; set; }
        public virtual DbSet<T_TEST> T_TEST { get; set; }
        public virtual DbSet<T_GROUP> T_GROUP { get; set; }
        public virtual DbSet<T_TEST2> T_TEST2 { get; set; }
        public virtual DbSet<T_ANSWER> T_ANSWER { get; set; }
        public virtual DbSet<T_CATEGORY> T_CATEGORY { get; set; }
        public virtual DbSet<T_GIFTITEMLIST> T_GIFTITEMLIST { get; set; }
        public virtual DbSet<T_HOWTOGIVE> T_HOWTOGIVE { get; set; }
        public virtual DbSet<T_OPINION> T_OPINION { get; set; }
        public virtual DbSet<T_QUESTION> T_QUESTION { get; set; }
        public virtual DbSet<T_QUESTIONNAIRE> T_QUESTIONNAIRE { get; set; }
        public virtual DbSet<T_SUBCATEGORY> T_SUBCATEGORY { get; set; }
    }
}