﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HREntities1 : DbContext
    {
        public HREntities1()
            : base("name=HREntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<bonus> bonus { get; set; }
        public virtual DbSet<config_file_first_kind> config_file_first_kind { get; set; }
        public virtual DbSet<config_file_second_kind> config_file_second_kind { get; set; }
        public virtual DbSet<config_file_third_kind> config_file_third_kind { get; set; }
        public virtual DbSet<config_major> config_major { get; set; }
        public virtual DbSet<config_major_kind> config_major_kind { get; set; }
        public virtual DbSet<config_primary_key> config_primary_key { get; set; }
        public virtual DbSet<config_public_char> config_public_char { get; set; }
        public virtual DbSet<config_question_first_kind> config_question_first_kind { get; set; }
        public virtual DbSet<config_question_second_kind> config_question_second_kind { get; set; }
        public virtual DbSet<engage_answer> engage_answer { get; set; }
        public virtual DbSet<engage_answer_details> engage_answer_details { get; set; }
        public virtual DbSet<engage_exam> engage_exam { get; set; }
        public virtual DbSet<engage_exam_details> engage_exam_details { get; set; }
        public virtual DbSet<engage_interview> engage_interview { get; set; }
        public virtual DbSet<engage_major_release> engage_major_release { get; set; }
        public virtual DbSet<engage_resume> engage_resume { get; set; }
        public virtual DbSet<engage_subjects> engage_subjects { get; set; }
        public virtual DbSet<human_file> human_file { get; set; }
        public virtual DbSet<human_file_dig> human_file_dig { get; set; }
        public virtual DbSet<major_change> major_change { get; set; }
        public virtual DbSet<Quan> Quan { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleQuan> RoleQuan { get; set; }
        public virtual DbSet<salary> salary { get; set; }
        public virtual DbSet<salary_grant> salary_grant { get; set; }
        public virtual DbSet<salary_grant_details> salary_grant_details { get; set; }
        public virtual DbSet<salary_standard> salary_standard { get; set; }
        public virtual DbSet<salary_standard_details> salary_standard_details { get; set; }
        public virtual DbSet<training> training { get; set; }
        public virtual DbSet<users> users { get; set; }
    }
}
