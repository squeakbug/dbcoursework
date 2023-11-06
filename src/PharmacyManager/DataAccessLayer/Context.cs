using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using DataAccessLayer.Entities;

namespace DataAccessLayer
{
    public class Context : DbContext
    {
        public Context(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        { }
        public DbSet<Category> Category { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<HasCategory> Has_category { get; set; }
        public DbSet<IncludeProduct> Receipt_marked_product { get; set; }
        public DbSet<IncomeProduct> Income_product { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<MarkedProduct> Marked_Product { get; set; }
        public DbSet<MarkInfo> Mark_info { get; set; }
        public DbSet<PersonMetadata> Person_metadata { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductRequest> Product_request { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<WriteOffProduct> Write_off_product { get; set; }
    }
}
