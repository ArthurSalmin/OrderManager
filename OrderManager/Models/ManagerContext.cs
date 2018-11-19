using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OrderManager.Models
{
    public class ManagerContext : DbContext
    {
        public DbSet<Quests> Quests { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Executors> Executors { get; set; }
        public ManagerContext() : base("DefaultConnection")
        {

        }
    }
}
