using System.ComponentModel.DataAnnotations;

namespace KanaQuiz.Core.Models
{
    public class Kana
    {
        [Key]
        public uint Id { get; set; }
        [MaxLength(255)]
        public string Value { get; set; }
        public KanaType Type { get; set; }
        [MaxLength(255)]
        public string Romanji { get; set; }
    }
}