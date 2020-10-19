using System.Threading.Tasks;
using KanaQuiz.Core.Models;
using KanaQuiz.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanaQuiz.Web.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly QuizFactory _quizFactory;

        public QuizController(QuizFactory quizFactory)
        {
            _quizFactory = quizFactory;
        }
        
        [HttpGet]
        public async Task<Quiz> Get()
        {
            return await _quizFactory.CreateHiraganaQuizAsync(4);
        }
    }
}
