using KanaQuiz.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanaQuiz.Core.Repositories
{
    public interface IKanaRepository : IRepository<Kana>
    {
        IEnumerable<Kana> GetAllByType(KanaType type);
        Task<IEnumerable<Kana>> GetAllByTypeAsync(KanaType type);
        uint CountByType(KanaType type);
        Task<uint> CountByTypeAsync(KanaType type);
    }
}
