﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CVGS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CVGSEntities : DbContext
    {
        public CVGSEntities()
            : base("name=CVGSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
    }
}
