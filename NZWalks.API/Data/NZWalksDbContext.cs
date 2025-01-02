using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Difficulty> Difficulties { get; set; }

    public DbSet<Region> Regions { get; set; }

    public DbSet<Walk> Walks { get; set; }

    //Seed some data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Seed for difficuties
        var difficuties = new List<Difficulty>()
        {
            new Difficulty()
            {
                Id = Guid.Parse("9D02E97F-24B5-4B84-A332-4B40F4011D10"),
                Name = "Easy"
            },
            new Difficulty()
            {
                Id = Guid.Parse("77995945-772B-41D0-86B4-3237A93A5AD5"),
                Name = "Medium"
            },
            new Difficulty()
            {
                Id = Guid.Parse("209F2379-6F08-4879-AAC4-D48E74F9BA19"),
                Name = "Hard"
            }
        };

        //Seed data to difficulties
        modelBuilder.Entity<Difficulty>().HasData(difficuties);

        var regions = new List<Region>()
        {
            new Region
            {
                Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                Name = "Auckland",
                Code = "AKL",
                ImageUrl =
                    "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                Name = "Northland",
                Code = "NTL",
                ImageUrl = null
            },
            new Region
            {
                Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                Name = "Bay Of Plenty",
                Code = "BOP",
                ImageUrl = null
            },
            new Region
            {
                Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                Name = "Wellington",
                Code = "WGN",
                ImageUrl =
                    "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                Name = "Nelson",
                Code = "NSN",
                ImageUrl =
                    "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                Name = "Southland",
                Code = "STL",
                ImageUrl = null
            },
        };

        modelBuilder.Entity<Region>().HasData(regions);
    }
}