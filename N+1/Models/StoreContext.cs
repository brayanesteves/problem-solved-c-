using Microsft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N1.Models {
    public class StoreContext : DbContext {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) {

        }

        public DbSet<Beer> Beers   { get; set; }

        public DbSet<Brand> Brands { get; set; }
    }
}