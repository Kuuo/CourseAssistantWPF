﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseAssistantWPF.Model {
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class CourseInfoDBEntities : DbContext {
        public CourseInfoDBEntities()
            : base("name=CourseInfoDBEntities") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Chairman> Chairmen { get; set; }
        public virtual DbSet<HWGroup> HWGroups { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Rejoinder> Rejoinders { get; set; }
        public virtual DbSet<Student> Students { get; set; }
    }
}
