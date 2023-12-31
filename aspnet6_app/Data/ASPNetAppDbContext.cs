﻿using aspnet6_app.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace aspnet6_app.Data

{
    public class ASPNetAppDbContext : DbContext
    {
        public ASPNetAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
