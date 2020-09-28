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
        IEnumerable<T> GetAllByType(KanaType type);
        Task<IEnumerable<T>> GetAllByTypeAsync(KanaType type);
        uint CountByType(KanaType type);
        Task<uint> CountByTypeAsync(KanaType type);
    }
}