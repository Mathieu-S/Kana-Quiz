using System.Collections.Generic;

namespace KanaQuiz.Core.Models
{
    public class Quiz
    {
        public string Title { get; set; }
        public KanaType Type { get; set; }
        public IEnumerable<Kana> Answers { get; set; }
        public Kana GoodAnswer { get; set; }
    }
}