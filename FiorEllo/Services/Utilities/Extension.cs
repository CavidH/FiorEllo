using System;
using System.IO;
using System.Threading.Tasks;
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
            return file.Length / 1024 <= sizekb;
        }

        public async static Task<string> SaveFileAsync(this IFormFile file, string root, params string[] folder)
        {
            string filename = DateTime.Now.ToString("MMddyyyyhhmmss") + "_" + file.FileName;
            string resultPath = Path.Combine(root, folder[0],folder[1], filename);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filename;
        }
    }
}