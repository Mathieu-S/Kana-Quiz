using System;
using System.Collections.Generic;
using KanaQuiz.Core.Models;
using KanaQuiz.Core.Repositories;

namespace KanaQuiz.Core.Services
{
    public class QuizFactory
    {
        private readonly IRepository<Kana> _kanaRepository;

        /// <summary>
        /// Cttor
        /// </summary>
        /// <param name="kanaRepository"></param>
        public QuizFactory(IRepository<Kana> kanaRepository)
        {
            _kanaRepository = kanaRepository;
        }
        
        /// <summary>
        /// Create a quiz whit hiragana question
        /// </summary>
        /// <param name="nbAnwsers"></param>
        /// <returns></returns>
        public Quiz CreateHiraganaQuiz(sbyte nbAnwsers = 4)
        {
            var rng = new Random();
            var answers = new List<Kana>();

            // Get all the hiraganas
            var hiraganas = (List<Kana>) _kanaRepository.GetAll();
            
            // Add answers to quiz
            for (var i = 0; i < nbAnwsers; i++)
            {
                answers.Add(hiraganas[rng.Next(0, hiraganas.Count)]);
            }
            
            // Selection a good answer randomly
            var goodAnswer = answers[rng.Next(0, answers.Count)];
            
            // Create quiz
            var quiz = new Quiz()
            {
                Title = "Guess this Hiragana",
                Type = KanaType.Hiragana,
                Answers = answers,
                GoodAnswer = goodAnswer
            };

            return quiz;
        }
    }
}