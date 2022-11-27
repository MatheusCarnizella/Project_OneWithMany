using Microsoft.EntityFrameworkCore;
using Project_RestricaoVideos.Models;

namespace Project_RestricaoVideos.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    { }

    public DbSet<Video>? Videos { get; set; }
    public DbSet<Restricao>? Restricoes { get; set; }

    protected override void OnModelCreating (ModelBuilder model)
    {
        model.Entity<Video>().HasKey(v => v.videosId);
        model.Entity<Video>().Property(v => v.videosId).ValueGeneratedOnAdd();
        model.Entity<Video>().Property(v => v.videoNome).IsRequired();
        model.Entity<Video>().Property(v => v.videoDescricao).IsRequired();
        model.Entity<Video>().Property(v => v.videoData).IsRequired();

        model.Entity<Restricao>().HasKey(r => r.retricaoId);
        model.Entity<Restricao>().Property(r => r.restricaoNome).IsRequired();

        model.Entity<Restricao>()
            .HasMany(x => x.Videos)
            .WithOne(x => x.Restricao)
            .HasForeignKey(x => x.restricaoId);

    }
}
