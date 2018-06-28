using Microsoft.EntityFrameworkCore;
using Stacks.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stacks.API
{
    public class StacksContext: DbContext
    {
        public StacksContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Stack> Stacks { get; set; }
        public DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Settings:StacksConnection"];
            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
