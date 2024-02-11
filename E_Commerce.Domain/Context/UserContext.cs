using E_Commerce.Domain.Models;
using E_Commerce.Domain.Models.Cart;
using E_Commerce.Domain.Models.Discount;
using E_Commerce.Domain.Models.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Productt> Product { get; set; }
        public DbSet<OTPTable> OTPTable { get; set; }
        public DbSet<CartMaster> CartMaster { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<Cardd> Card { get; set; }
        public DbSet<SalesMaster> SalesMasters { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(e => e.UserId);
                builder.Property(e => e.UserId).UseIdentityColumn();
                builder.Property(e => e.Password).HasMaxLength(50).IsRequired(true);
                builder.Property(e => e.Zipcode).HasMaxLength(56).IsRequired(true); ;
                builder.Property(e => e.Mobile).HasMaxLength(50).IsRequired(true); ;
                
               
            });


            modelBuilder.Entity<OTPTable>(OTPTabel =>
            {
                OTPTabel.HasKey(s => s.OtpID);
                OTPTabel.Property(e => e.OtpID).UseIdentityColumn();
                OTPTabel.Property(e => e.OTP).HasMaxLength(10).IsRequired(true);
                OTPTabel.Property(e => e.OtpGenerationDatetime).IsRequired(true);
                OTPTabel.Property(e => e.OtpExpireDatetime).IsRequired(true);
                OTPTabel.Property(e => e.UserID).IsRequired(true);

            });

            modelBuilder.Entity<Productt>(skillBulder =>
            {
                skillBulder.HasKey(s => s.ProductId);
                skillBulder.Property(e => e.ProductId).UseIdentityColumn();
                skillBulder.Property(e => e.ProductCode).HasMaxLength(6).IsRequired(true);

                

            });

            modelBuilder.Entity<CartMaster>(CartMasterTable =>
            {
                CartMasterTable.HasKey(s => s.CartMasterId);
                CartMasterTable.Property(e => e.CartMasterId).UseIdentityColumn();

                //CartDetails is in CartMaster
                CartMasterTable.HasMany<CartDetail>(a=>a.CartDetails).
                 WithOne(s => s.CartMaster).
                HasForeignKey(s => s.CartMasterId);
            });

            modelBuilder.Entity<CartDetail>(CartMasterTable =>
            {
                CartMasterTable.HasKey(s => s.CartDetailId);
                CartMasterTable.Property(e => e.CartDetailId).UseIdentityColumn();

            });

        }


    }
}
