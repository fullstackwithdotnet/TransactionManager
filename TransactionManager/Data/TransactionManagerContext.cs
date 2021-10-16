using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionManager.Models;

namespace TransactionManager.Data
{
    public class TransactionManagerContext : DbContext
    {
        public TransactionManagerContext (DbContextOptions<TransactionManagerContext> options)
            : base(options)
        {
        }

        public DbSet<CompanyModel> Companies { get; set; }

        public DbSet<TransactionRecordModel> Transactions { get; set; }
        public DbSet<TransactionTypeModel> TransactionTypes { get; set; }
    }
}
