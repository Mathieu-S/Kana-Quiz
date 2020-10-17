using System.Collections.Generic;
using System.Threading.Tasks;
using KanaQuiz.Core.Models;

namespace KanaQuiz.Core.Repositories
{
    public interface IRepository<T>
    {
        T Get(uint id);
        Task<T> GetAsync(uint id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<Kana>> GetAllAsync();
    }
}