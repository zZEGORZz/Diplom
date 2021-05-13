using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POCHTI_KURSACH.Models.Entities;

namespace POCHTI_KURSACH.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Bag> Bags { get; set; }
        public DbSet<Entities.Role> Roles { get; set; }
        public DbSet<Entities.Product> Products { get; set; }
        public DbSet<Entities.Operation> Operations { get; set; }
        public DbSet<Entities.Category> Categories { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                new Role { Id = 1, Name = "admin" },
                new Role { Id = 2, Name = "user" }
            });

            modelBuilder.Entity<Entities.User>().HasData(new Entities.User[] {
                new Entities.User
                {
                    Id = 1,
                    Login = "admin",
                    Password = "admin",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    Login = "user",
                    Password = "user",
                    RoleId = 2
                }

            });
            modelBuilder.Entity<Entities.Category>().HasData(new Entities.Category[]
            {
                new Entities.Category { Id = 1, Name = "алкоголь"},
                new Entities.Category { Id = 2, Name = "напитки"},
                new Entities.Category { Id = 3, Name = "хлебобулочные изделия"},
                new Entities.Category { Id = 4, Name = "мясные изделия"},
                new Entities.Category { Id = 5, Name = "кондитерские изделия"}
            });
            modelBuilder.Entity<Entities.Product>().HasData(new Entities.Product[] {
                new Entities.Product
                {
                    Id = 1,
                    Name = "Молоко",
                    image = "/img/1-27.png",
                    Price = 2,
                    Garant = 14,
                    CategoryId = 2,
                    
                    Amount = 5
                },

                 new Entities.Product
                {
                    Id = 2,
                    Name = "Кока-Кола",
                    image = "/img/12345.png",
                    Price = 1,
                    Garant = 14,
                    CategoryId = 2,
                    Amount = 6
                },
                  new Entities.Product
                {
                    Id = 3,
                    Name = "Пиво",
                    CategoryId = 1,
                    image = "/img/beer.png",
                    Price = 2,
                    Garant = 14,
                    Amount = 4
                },
                   new Entities.Product
                {
                    Id = 4,
                    Name = "Хлеб",
                    CategoryId = 3,
                    image = "/img/bread.png",
                    Price = 2,
                    Garant = 14,
                    Amount = 9
                },
                    new Entities.Product
                {
                    Id = 5,
                    Name = "Пельмешки домашние",
                    CategoryId = 4,
                    image = "/img/pelm.png",
                    Price = 4,
                    Garant = 14,
                    Amount = 7
                },
                     new Entities.Product
                {
                    Id = 6,
                    Name = "Сало",
                    CategoryId = 4,
                    image = "/img/salo.png",
                    Price = 5,
                    Garant = 14,
                    Amount = 13
                },
                      new Entities.Product
                {
                    Id = 7,
                    Name = "Макарошки",
                    image = "/img/pasta.png",
                    CategoryId = 3,
                    Price = 3,
                    Garant = 14,
                    Amount = 8
                },
                       new Entities.Product
                {
                    Id = 8,
                    Name = "Сосисончики",
                    CategoryId = 4,
                    image = "/img/sosiski.png",
                    Price = 2,
                    Garant = 14,
                    Amount = 8
                }
            }) ;
        }
    }
}
