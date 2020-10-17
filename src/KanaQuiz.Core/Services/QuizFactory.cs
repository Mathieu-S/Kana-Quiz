using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KanaQuiz.Core.Exceptions;
using KanaQuiz.Core.Models;
using KanaQuiz.Core.Repositories;

namespace KanaQuiz.Core.Services
{
    /// <summary>
    ///     A Quiz Factory.
    /// </summary>
    public class QuizFactory
    {
        private readonly IKanaRepository _kanaRepository;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="kanaRepository"></param>
        public QuizFactory(IKanaRepository kanaRepository)
        {
            _kanaRepository = kanaRepository;
        }

        /// <summary>
        ///     Create a quiz with given type question.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nbAnwsers"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Quiz CreateQuiz(KanaType type, byte nbAnwsers = 4)
        {
            var quiz = type switch
            {
                KanaType.Hiragana => CreateHiraganaQuiz(nbAnwsers),
                KanaType.Katakana => CreateKatakanaQuiz(nbAnwsers),
                KanaType.Kanji => throw new NotImplementedException(),
                KanaType.Romanji => throw new NotImplementedException(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "KanaType unknow")
            };

            return quiz;
        }

        /// <summary>
        ///     Create a quiz with given type question asynchronous.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="nbAnwsers">The nb anwsers.</param>
        /// <returns></returns>
        public Task<Quiz> CreateQuizAsync(KanaType type, byte nbAnwsers = 4)
        {
            return Task.FromResult(CreateQuiz(type, nbAnwsers));
        }

        /// <summary>
        ///     Create a quiz with hiragana question.
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <returns></returns>
        public Quiz CreateHiraganaQuiz(byte nbAnwsers = 4)
        {
            return GenerateQuiz(nbAnwsers, KanaType.Hiragana);
        }

        /// <summary>
        ///     Create a quiz with hiragana question asynchronously.
        /// </summary>
        /// <param name="nbAnwsers">The nb anwsers.</param>
        /// <returns></returns>
        public Task<Quiz> CreateHiraganaQuizAsync(byte nbAnwsers)
        {
            return Task.FromResult(CreateHiraganaQuiz(nbAnwsers));
        }

        /// <summary>
        ///     Create a quiz with katakana question.
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <returns></returns>
        public Quiz CreateKatakanaQuiz(byte nbAnwsers = 4)
        {
            return GenerateQuiz(nbAnwsers, KanaType.Katakana);
        }

        /// <summary>
        ///     Create a quiz with katakana question asynchronously.
        /// </summary>
        /// <param name="nbAnwsers">The nb anwsers.</param>
        /// <returns></returns>
        public Task<Quiz> CreateKatakanaQuizAsync(byte nbAnwsers)
        {
            return Task.FromResult(CreateKatakanaQuiz(nbAnwsers));
        }

        /// <summary>
        ///     Build the quiz.
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Quiz GenerateQuiz(byte nbAnwsers, KanaType type)
        {
            IsCreatable(nbAnwsers, type);

            var rng = new Random();
            var answers = new List<Kana>();

            // Get all the kana by type
            var kanas = (List<Kana>) _kanaRepository.GetAllByType(type);

            // Add answers to quiz
            for (var i = 0; i < nbAnwsers; i++)
            {
                var idKana = kanas[rng.Next(0, kanas.Count)];
                answers.Add(idKana);
                kanas.Remove(idKana);
            }

            // Selection a good answer randomly
            var goodAnswer = answers[rng.Next(0, answers.Count)];

            // Create quiz
            var quiz = new Quiz
            {
                Title = "Guess this Hiragana",
                Type = type,
                Answers = answers,
                GoodAnswer = goodAnswer
            };

            return quiz;
        }
        
        /// <summary>
        ///     Build the quiz asynchronously.
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private async Task<Quiz> GenerateQuizAsync(byte nbAnwsers, KanaType type)
        {
            IsCreatable(nbAnwsers, type);

            var rng = new Random();
            var answers = new List<Kana>();

            // Get all the kana by type
            var kanas = (List<Kana>) await _kanaRepository.GetAllByTypeAsync(type);

            // Add answers to quiz
            for (var i = 0; i < nbAnwsers; i++)
            {
                var idKana = kanas[rng.Next(0, kanas.Count)];
                answers.Add(idKana);
                kanas.Remove(idKana);
            }

            // Selection a good answer randomly
            var goodAnswer = answers[rng.Next(0, answers.Count)];

            // Create quiz
            var quiz = new Quiz
            {
                Title = "Guess this Hiragana",
                Type = type,
                Answers = answers,
                GoodAnswer = goodAnswer
            };

            return quiz;
        }

        /// <summary>
        ///     Determines if the quiz can be generated.
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private bool IsCreatable(byte nbAnwsers, KanaType type)
        {
            if (nbAnwsers >= 2 && nbAnwsers <= _kanaRepository.CountByType(type)) return true;

            throw new KanaQuizException("The number of responses requested is invalid");
        }
    }
}