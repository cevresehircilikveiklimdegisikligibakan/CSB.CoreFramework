namespace CSB.Core.Services
{
    internal class TextService : ITextService
    {
        public string ReplaceTurkishCharacters(string text)
        {
            string result = text.Replace("ç", "c")
                                .Replace("ğ", "g")
                                .Replace("ı", "i")
                                .Replace("ö", "o")
                                .Replace("ş", "s")
                                .Replace("ü", "u")
                                .Replace("Ç", "C")
                                .Replace("Ğ", "G")
                                .Replace("İ", "I")
                                .Replace("Ö", "O")
                                .Replace("Ş", "S")
                                .Replace("Ü", "U");
            return result;
        }
    }
}