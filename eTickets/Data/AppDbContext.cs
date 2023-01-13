using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eTickets.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //This method is called by the framework when your context is first created to build the model and its mappings in memory
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //The ModelBuilder is the class which is responsible for building the Model
            //here I make a combination of primary keys
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });
            //2 one-to-mny relationships
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey( m => m.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);
            base.OnModelCreating(modelBuilder);
        }

        //DbSet is entity set that can be used for create, read, update, and delete operations
        //table names for each model:
        public DbSet<Actor> Actors { get; set; } //table name for Actor model
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }

        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
