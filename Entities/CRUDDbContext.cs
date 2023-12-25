using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CRUDDbContext : DbContext
    {
        public CRUDDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }


    }
}
