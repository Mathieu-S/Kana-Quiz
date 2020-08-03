using System.Collections.Generic;
using KanaQuiz.Core.Models;

namespace KanaQuiz.Core.Repositories
{
    public interface IRepository<out T>
    {
        T Get();
        IEnumerable<T> GetAll();
        uint CountByType(KanaType type);
    }
}