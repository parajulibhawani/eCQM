using System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;


namespace eQM.Models
{
    public class eCQMDBContext
    {
        public DbSet<ECQM> ECQMs { get; set; }
       

        public eCQMDBContext(DbContextOptions<eCQMDBContext> options)
            : base(options)
        {
        }

        public eCQMDBContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=eCQM;integrated security=True");
        }
    }
}
