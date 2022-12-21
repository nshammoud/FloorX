using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KQF.Floor.Data.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace KQF.Floor.Data
{
    public class FloorDataContext : DbContext
    {
        public FloorDataContext(DbContextOptions<FloorDataContext> options) : base(options)
        {
        }

        public DbSet<ConsumptionItemTransaction> Transactions { get; set; }


        public DbSet<JFJobHistory> JobHistory { get; set; }


        public async Task<IEnumerable<JFJobHistory>> SPJFJobHistory(string jobNo)
        {
            var jobArg = new SqlParameter("JobNo", jobNo);

            var history = await this.JobHistory
                .FromSqlRaw("EXECUTE dbo.sp_GetJobHistory @jobNo", jobArg)
                .ToListAsync();

            return history;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<ConsumptionItemTransaction>()
                .HasIndex(x => x.ContainerNumber).IsUnique(false);
            modelBuilder.Entity<ConsumptionItemTransaction>()
                .HasIndex(x => x.ProductionOrderNumber).IsUnique(false);
            modelBuilder.Entity<ConsumptionItemTransaction>()
                .HasIndex(x => x.ItemNumber).IsUnique(false);
            modelBuilder.Entity<ConsumptionItemTransaction>()
                .HasIndex(x => x.ParentItemNumber).IsUnique(false);
            modelBuilder.Entity<ConsumptionItemTransaction>()
                .HasIndex(x => x.IsPosted).IsUnique(false);

            modelBuilder.Entity<ConsumptionItemTransaction>().ToTable("Transactions")
                .HasKey(x => x.Id);


        }
    }
}
