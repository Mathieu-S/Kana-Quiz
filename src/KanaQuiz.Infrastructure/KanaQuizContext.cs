using KanaQuiz.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KanaQuiz.Infrastructure
{
    public class KanaQuizContext : DbContext
    {
        public DbSet<Kana> Kanas { get; set; }

        public KanaQuizContext()
        {
        }

        public KanaQuizContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Database=KanaQuiz;Username=postgres;Password=admin");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kana>().HasData(
                new Kana {Id = 1, Romanji = "a", Value = "あ", Type = KanaType.Hiragana},
                new Kana {Id = 2, Romanji = "i", Value = "い", Type = KanaType.Hiragana},
                new Kana {Id = 3, Romanji = "u", Value = "う", Type = KanaType.Hiragana},
                new Kana {Id = 4, Romanji = "e", Value = "え", Type = KanaType.Hiragana},
                new Kana {Id = 5, Romanji = "o", Value = "お", Type = KanaType.Hiragana});
        }
    }
}
