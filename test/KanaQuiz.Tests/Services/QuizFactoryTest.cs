using System.Collections.Generic;
using System.Linq;
using KanaQuiz.Core.Exceptions;
using KanaQuiz.Core.Models;
using KanaQuiz.Core.Repositories;
using KanaQuiz.Core.Services;
using NSubstitute;
using Xunit;

namespace KanaQuiz.Tests.Services
{
    public class QuizFactoryTest
    {
        private readonly IEnumerable<Kana> _hiraganas;
        private readonly IEnumerable<Kana> _katakana;

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

            _katakana = new List<Kana>
            {
                new Kana {Id = 6, Romanji = "a", Value = "ア", Type = KanaType.Katakana},
                new Kana {Id = 7, Romanji = "i", Value = "イ", Type = KanaType.Katakana},
                new Kana {Id = 8, Romanji = "u", Value = "ウ", Type = KanaType.Katakana},
                new Kana {Id = 9, Romanji = "e", Value = "エ", Type = KanaType.Katakana},
                new Kana {Id = 10, Romanji = "o", Value = "オ", Type = KanaType.Katakana}
            };
        }

        [Theory]
        [InlineData(KanaType.Hiragana)]
        [InlineData(KanaType.Katakana)]
        public void CreateQuiz_Test(KanaType type)
        {
            // Arrange
            var kanaRepository = Substitute.For<IKanaRepository>();
            kanaRepository.GetAllByType(type).Returns(_hiraganas);
            switch (type)
            {
                case KanaType.Hiragana:
                    kanaRepository.CountByType(Arg.Any<KanaType>()).Returns((byte) _hiraganas.Count());
                    break;
                case KanaType.Katakana:
                    kanaRepository.CountByType(Arg.Any<KanaType>()).Returns((byte) _katakana.Count());
                    break;
            }

            // Act
            var quizFactory = new QuizFactory(kanaRepository);
            var result = quizFactory.CreateQuiz(type);

            // Assert
            Assert.Equal(4, result.Answers.Count());
            Assert.Contains(result.GoodAnswer, result.Answers);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        public void CreateHiraganaQuiz_Test(byte nbAnwsers)
        {
            // Arrange
            var kanaRepository = Substitute.For<IKanaRepository>();
            kanaRepository.GetAllByType(Arg.Any<KanaType>()).Returns(_hiraganas);
            kanaRepository.CountByType(Arg.Any<KanaType>()).Returns((byte) _hiraganas.Count());

            // Act
            var quizFactory = new QuizFactory(kanaRepository);
            var result = quizFactory.CreateHiraganaQuiz(nbAnwsers);

            // Assert
            Assert.Equal(nbAnwsers, result.Answers.Count());
            Assert.Equal(KanaType.Hiragana, result.Type);
            Assert.Contains(result.GoodAnswer, result.Answers);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        public void CreateHiraganaQuiz_OutOfRange_Test(byte nbAnwsers)
        {
            // Arrange
            var kanaRepository = Substitute.For<IKanaRepository>();
            kanaRepository.GetAllByType(Arg.Any<KanaType>()).Returns(_hiraganas);
            kanaRepository.CountByType(Arg.Any<KanaType>()).Returns((byte) _hiraganas.Count());

            // Act
            var quizFactory = new QuizFactory(kanaRepository);

            // Assert
            Assert.Throws<KanaQuizException>(() => quizFactory.CreateHiraganaQuiz(nbAnwsers));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        public void CreateKatakanaQuiz_Test(byte nbAnwsers)
        {
            // Arrange
            var kanaRepository = Substitute.For<IKanaRepository>();
            kanaRepository.GetAllByType(Arg.Any<KanaType>()).Returns(_katakana);
            kanaRepository.CountByType(Arg.Any<KanaType>()).Returns((byte) _katakana.Count());

            // Act
            var quizFactory = new QuizFactory(kanaRepository);
            var result = quizFactory.CreateKatakanaQuiz(nbAnwsers);

            // Assert
            Assert.Equal(nbAnwsers, result.Answers.Count());
            Assert.Equal(KanaType.Katakana, result.Type);
            Assert.Contains(result.GoodAnswer, result.Answers);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        public void CreateKatakanaQuiz_OutOfRange_Test(byte nbAnwsers)
        {
            // Arrange
            var kanaRepository = Substitute.For<IKanaRepository>();
            kanaRepository.GetAllByType(Arg.Any<KanaType>()).Returns(_katakana);
            kanaRepository.CountByType(Arg.Any<KanaType>()).Returns((byte) _katakana.Count());

            // Act
            var quizFactory = new QuizFactory(kanaRepository);

            // Assert
            Assert.Throws<KanaQuizException>(() => quizFactory.CreateKatakanaQuiz(nbAnwsers));
        }
    }
}