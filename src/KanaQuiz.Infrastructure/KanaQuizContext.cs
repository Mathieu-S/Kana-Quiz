﻿using KanaQuiz.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KanaQuiz.Infrastructure
{
    public class KanaQuizContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Kana> Kanas { get; set; }

        public KanaQuizContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //=> optionsBuilder.UseNpgsql("Host=postgre;Database=KanaQuiz;Username=postgres;Password=admin");
            => optionsBuilder.UseNpgsql(_connectionString);
        
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