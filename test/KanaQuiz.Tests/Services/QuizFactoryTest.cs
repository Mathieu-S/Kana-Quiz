using System.Collections.Generic;
using System.Linq;
using KanaQuiz.Core.Models;
using KanaQuiz.Core.Repositories;
using KanaQuiz.Core.Services;
using NSubstitute;
using Xunit;

namespace KanaQuiz.Tests.Services
{
    public class QuizFactoryTest
    {
        public QuizFactoryTest()
        {
            _hiraganas = new List<Kana>
            {
                new Kana {Id = 1, Romanji = "a", Value = "あ", Type = KanaType.Hiragana},
                new Kana {Id = 2, Romanji = "i", Value = "い", Type = KanaType.Hiragana},
                new Kana {Id = 3, Romanji = "u", Value = "う", Type = KanaType.Hiragana},
                new Kana {Id = 4, Romanji = "e", Value = "え", Type = KanaType.Hiragana},
                new Kana {Id = 5, Romanji = "o", Value = "お", Type = KanaType.Hiragana}
            };
        }

        private readonly IEnumerable<Kana> _hiraganas;

        [Fact]
        public void CreateHiraganaQuiz_Test()
        {
            // Arrange
            var kanaRepository = Substitute.For<IRepository<Kana>>();
            kanaRepository.GetAll().Returns(_hiraganas);

            // Act
            var quizFactory = new QuizFactory(kanaRepository);
            var result = quizFactory.CreateHiraganaQuiz();

            // Assert
            Assert.Equal(4, result.Answers.Count());
            Assert.Contains(result.GoodAnswer, result.Answers);
        }
    }
}
