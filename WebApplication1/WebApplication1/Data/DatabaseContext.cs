using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Title> Titles { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Character>().HasData(new List<Character>
            {
                new Character {
                    Id = 1,
                    FirstName = "Ed",
                    LastName = "Sheeran"
                },
                new Character {
                    Id = 2,
                    FirstName = "Anna",
                    LastName = "Salem"
                }
            });

            modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
            {
                new Backpack {
                    ItemId = 1,
                    CharacterId = 2
                },
                new Backpack {
                    ItemId = 2,
                    CharacterId = 1
                }
            });

            modelBuilder.Entity<Title>().HasData(new List<Title>
            {
                new Title
                {
                    Id = 1,
                    Name = "Truth",
                },
                new Title
                {
                    Id = 2,
                    Name = "Fake",
                   
                },
                new Title
                {
                    Id = 3,
                    Name = "Creativity",
                  
                }
            });

            modelBuilder.Entity<Item>().HasData(new List<Item>
            {
                new Item
                {
                   Id=1,
                   Name="smth",
                   Weight = 10
                },
                new Item
                {
                    Id = 2,
                    Name="smth",
                    Weight = 10
                },
                new Item
                {
                    Id = 3,
                    Name="smth",
                    Weight = 10
                }
            });

            modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
            {
                new CharacterTitle
                {
                    AcquiredAt = DateTime.Parse("2024-05-04"),
                    CharacterId = 1,
                    TitleId = 2
                },
                new CharacterTitle
                {
                    AcquiredAt = DateTime.Parse("2024-06-04"),
                    CharacterId = 2,
                    TitleId = 3
                },
                new CharacterTitle
                {
                    AcquiredAt = DateTime.Parse("2010-05-04"),
                    CharacterId = 2,
                    TitleId = 1
                },
                new CharacterTitle
                {
                    AcquiredAt = DateTime.Parse("2024-09-04"),
                    CharacterId = 1,
                    TitleId = 2
                }
            });
    }
}