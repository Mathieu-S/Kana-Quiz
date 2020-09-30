using KanaQuiz.Core.Models;
using KanaQuiz.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanaQuiz.Web.Data
{
    public class KanaData : IRepository<Kana>
    {
        private readonly IEnumerable<Kana> _hiraganas;

        public KanaData()
        {
            _hiraganas = new List<Kana>
            {
                new Kana {Id = 1, Romanji = "a", Value = "あ", Type = KanaType.Hiragana},
                new Kana {Id = 2, Romanji = "i", Value = "い", Type = KanaType.Hiragana},
                new Kana {Id = 3, Romanji = "u", Value = "う", Type = KanaType.Hiragana},
                new Kana {Id = 4, Romanji = "e", Value = "え", Type = KanaType.Hiragana},
                new Kana {Id = 5, Romanji = "o", Value = "お", Type = KanaType.Hiragana}
            };
        }

        public Kana Get(uint id)
        {
            return _hiraganas.FirstOrDefault(x => x.Id == id);
        }

        public Task<Kana> GetAsync(uint id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Kana> GetAll()
        {
            return _hiraganas;
        }

        public Task<IEnumerable<Kana>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Kana> GetAllByType(KanaType type)
        {
            return _hiraganas.Where(x => x.Type == type).ToList();
        }

        public Task<IEnumerable<Kana>> GetAllByTypeAsync(KanaType type)
        {
            throw new System.NotImplementedException();
        }

        public uint CountByType(KanaType type)
        {
            return (uint) _hiraganas.Where(x => x.Type == type).Count();
        }

        public Task<uint> CountByTypeAsync(KanaType type)
        {
            throw new System.NotImplementedException();
        }
    }
}
