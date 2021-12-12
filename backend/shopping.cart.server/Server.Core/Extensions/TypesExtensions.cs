namespace Server.Core.Extensions
{
    public static class TypesExtensions
    {
        public static string AsNormalizedString(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return
                    input
                        .ToLower()
                        .Trim()
                        .Replace(" ", string.Empty)
                        .Replace("/", string.Empty)
                        .Replace("\\", string.Empty)
                        .Replace("+", string.Empty)
                        .Replace("أ", "ا")
                        .Replace("إ", "ا")
                        .Replace("ة", "ه")
                        .Replace("-", string.Empty)
                        .Replace(".", string.Empty);
            }

            return input;
        }
    }
}
