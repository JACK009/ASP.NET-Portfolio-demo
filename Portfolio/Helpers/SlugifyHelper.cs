using System.Text;
using System.Text.RegularExpressions;

namespace Portfolio.Helpers
{
    public class SlugifyHelper
    {
        public static string GenerateSlug(string url)
        {
            url = url.ToLowerInvariant();

            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(url);
            url = Encoding.ASCII.GetString(bytes);
            url = Regex.Replace(url, @"\s", "-");
            url = Regex.Replace(url, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);
            url = url.Trim('-', '_');
            url = Regex.Replace(url, @"([-_]){2,}", "$1", RegexOptions.Compiled);
            
            return url;
        }
    }
}