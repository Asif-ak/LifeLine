using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LifeLine_WebApi.Models;
namespace LifeLine_WebApi.DBConfiguration
{
    public class LifeLineContext:DbContext
    {
        public LifeLineContext(DbContextOptions<LifeLineContext> options):base(options)
        {
            
        }
        public DbSet<Donor> Donors {get;set;}
        public DbSet<Requests> Requests {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
        
    }

}