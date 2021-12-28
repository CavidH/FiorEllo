using Microsoft.AspNetCore.Http;

namespace FiorEllo.Services.Utilities
{
    public static class Extension
    {
        public static bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckFileSize(this IFormFile file, int sizekb)
        {
            return  file.Length/1024<=sizekb;
        }
    }
}