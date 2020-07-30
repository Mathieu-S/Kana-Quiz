namespace KanaQuiz.Core.Models
{
    public class Kana
    {
        public uint Id { get; set; }
        public string Value { get; set; }
        public KanaType Type { get; set; }
        public string Romanji { get; set; }
    }
}