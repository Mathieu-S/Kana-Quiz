using System;
using System.Collections.Generic;
using KanaQuiz.Core.Models;
using KanaQuiz.Core.Repositories;

namespace KanaQuiz.Core.Services
{
    /// <summary>
    /// A Quiz Factory.
    /// </summary>
    public class QuizFactory
    {
        private readonly IRepository<Kana> _kanaRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kanaRepository"></param>
        public QuizFactory(IRepository<Kana> kanaRepository)
        {
            _kanaRepository = kanaRepository;
        }

        /// <summary>
        /// Create a quiz whit given type question.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nbAnwsers"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Quiz CreateQuiz(KanaType type, byte nbAnwsers = 4)
        {
            Quiz quiz = type switch
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
        /// Create a quiz whit hiragana question.
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <returns></returns>
        public Quiz CreateHiraganaQuiz(byte nbAnwsers = 4)
        {
            return GenerateQuiz(nbAnwsers, KanaType.Hiragana);
        }
        
        /// <summary>
        /// Create a quiz whit katakana question.
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <returns></returns>
        public Quiz CreateKatakanaQuiz(byte nbAnwsers = 4)
        {
            return GenerateQuiz(nbAnwsers, KanaType.Katakana);
        }
        
        /// <summary>
        /// Build the quiz.
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Quiz GenerateQuiz(byte nbAnwsers, KanaType type)
        {
            IsCreatable(nbAnwsers, type);
            
            var rng = new Random();
            var answers = new List<Kana>();

            // Get all the hiraganas
            var kanas = (List<Kana>) _kanaRepository.GetAll();
            
            // Add answers to quiz
            for (var i = 0; i < nbAnwsers; i++)
            {
                answers.Add(kanas[rng.Next(0, kanas.Count)]);
            }
            
            // Selection a good answer randomly
            var goodAnswer = answers[rng.Next(0, answers.Count)];
            
            // Create quiz
            var quiz = new Quiz()
            {
                Title = "Guess this Hiragana",
                Type = type,
                Answers = answers,
                GoodAnswer = goodAnswer
            };

            return quiz;
        }

        /// <summary>
        /// Determines if the quiz can be generated.
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private bool IsCreatable(byte nbAnwsers, KanaType type)
        {
            if (nbAnwsers >= 2 && nbAnwsers <= _kanaRepository.CountByType(type))
            {
                return true;
            }

            throw new ArgumentException("The number of responses requested is invalid");
        }
    }
}