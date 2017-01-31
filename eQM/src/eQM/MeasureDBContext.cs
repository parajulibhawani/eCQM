using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Builder;
namespace eQM
{
    public class MeasureDBContext : DbContext
    {
        public virtual DbSet<Measure> Measures { get; set; }
        public virtual DbSet<References> References { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MeasureDb;integrated security=True");
        }
    }
}