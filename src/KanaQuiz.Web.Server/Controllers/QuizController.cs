using System.Collections.Generic;
using KanaQuiz.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanaQuiz.Web.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        [HttpGet]
        public Quiz Get()
        {
            var goodAnwser = new Kana{ Id = 1, Type = KanaType.Hiragana, Romanji = "a", Value = "a"};

            var anwsers = new List<Kana>
            {
                goodAnwser
            };

            return new Quiz {Title = "test", Type = KanaType.Hiragana, GoodAnswer = goodAnwser, Answers = anwsers};
        }
    }
}
