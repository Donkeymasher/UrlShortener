using Microsoft.AspNetCore.WebUtilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortner.Services.Data.DatabaseModels
{
    public class ShortenedUrl
    {
        [Key]
        public int ShortenedUrlId { get; set; }
        public string OriginalUrl { get; set; }

        public string GetUrlToken()
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(ShortenedUrlId));
        }

        public static int GetIdFromUrlToken(string UrlToken)
        {
            return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(UrlToken));
        }
    }
}
