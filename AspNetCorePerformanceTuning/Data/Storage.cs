// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using AspNetCorePerformanceTuning.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCorePerformanceTuning.Data
{
  public class Storage : DbContext
  {
    public DbSet<Category> Categories { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Photo> Photos { get; set; }

    public Storage(DbContextOptions<Storage> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Category>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.ToTable("Categories");
        }
      );

	    modelBuilder.Entity<Article>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
          etb.Property(e => e.Description).IsRequired().HasMaxLength(2048);
          etb.ToTable("Articles");
        }
      );

      modelBuilder.Entity<Photo>(etb =>
        {
          etb.HasKey(e => e.Id);
          etb.Property(e => e.Id).ValueGeneratedOnAdd();
          etb.Property(e => e.Filename).HasMaxLength(64);
          etb.ToTable("Photos");
        }
      );
    }
  }
}