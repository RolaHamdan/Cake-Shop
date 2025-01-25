using Cake_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cake_Shop.Data
{
    public class ApplicationDbContext: DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Orders> orders { get; set; }

        public DbSet<Products> products { get; set; }

        public DbSet<Customers> customers { get; set; }

        public DbSet<OrderStatus> status { get; set; }


    }
}
