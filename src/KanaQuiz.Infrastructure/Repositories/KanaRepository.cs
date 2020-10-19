using KanaQuiz.Core.Models;
using KanaQuiz.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanaQuiz.Infrastructure.Repositories
{
    public class KanaRepository : IKanaRepository
    {
        private readonly KanaQuizContext _context;

        public KanaRepository(KanaQuizContext context)
        {
            _context = context;
        }
        
        public Kana Get(uint id)
        {
            return _context.Kanas.AsNoTracking().First(x => x.Id == id);
        }
        
        public async Task<Kana> GetAsync(uint id)
        {
            return await _context.Kanas.AsNoTracking().FirstAsync(x => x.Id == id);
        }

        public IEnumerable<Kana> GetAll()
        {
            return _context.Kanas.AsNoTracking().ToList();
        }

        public async Task<IEnumerable<Kana>> GetAllAsync()
        {
            return await _context.Kanas.AsNoTracking().ToListAsync();
        }

        public IEnumerable<Kana> GetAllByType(KanaType type)
        {
            return _context.Kanas.AsNoTracking().Where(x => x.Type == type).ToList();
        }

        public async Task<IEnumerable<Kana>> GetAllByTypeAsync(KanaType type)
        {
            return await _context.Kanas.AsNoTracking().Where(x => x.Type == type).ToListAsync();
        }

        public uint CountByType(KanaType type)
        {
            return (uint) _context.Kanas.AsNoTracking().Count(x => x.Type == type);
        }

        public async Task<uint> CountByTypeAsync(KanaType type)
        {
            return (uint) await _context.Kanas.AsNoTracking().CountAsync(x => x.Type == type);
        }
    }
}
