using Microsoft.EntityFrameworkCore;

namespace PizzaOrderWebApi.Models
{
    public class PizzaOrderDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaOrder> PizzaOrders { get; set; }

        public PizzaOrderDbContext(DbContextOptions<PizzaOrderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // RelationShip
            modelBuilder.Entity<PizzaOrder>()
             .HasOne(p => p.Customer)
             .WithMany(c => c.PizzaOrders)
             .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<PizzaOrder>()
                .HasOne(p => p.Pizza)
                .WithMany()
                .HasForeignKey(p => p.PizzaId);

            // Seed Customers table
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "User1", Address = "123 Main St", PhoneNumber = "91", Email = "User1@gmail.com" },
                new Customer { Id = 2, Name = "User2", Address = "456 Oak Ave", PhoneNumber = "91", Email = "User2@gmail.comm" },
                new Customer { Id = 3, Name = "User3", Address = "789 Elm St", PhoneNumber = "91", Email = "User3@gmail.com" }
            );

            // Seed Pizzas table
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { Id = 1, Name = "Cheese", Description = "Mozzarella cheese and tomato sauce", Price = 10.99m },
                new Pizza { Id = 2, Name = "Pepperoni", Description = "Mozzarella cheese, tomato sauce, and pepperoni", Price = 12.99m },
                new Pizza { Id = 3, Name = "Supreme", Description = "Mozzarella cheese, tomato sauce, pepperoni, sausage, onions, peppers, and mushrooms", Price = 15.99m }
            );
        }
    }
}
