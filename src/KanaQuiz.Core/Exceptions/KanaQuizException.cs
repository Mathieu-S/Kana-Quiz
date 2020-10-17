using System;

namespace KanaQuiz.Core.Exceptions
{
    public class KanaQuizException : Exception
    {
        public KanaQuizException()
        {
        }

        public KanaQuizException(string message) : base(message)
        {
        }

        public KanaQuizException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
