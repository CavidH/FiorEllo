using System.IO;

namespace FiorEllo.Services.Utilities
{
    public static class Helper
    {
        public static void RemoveFile(string root, string image ,params string[] folders)
        {
            string path = Path.Combine(root, folders[0],folders[1],image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        public  enum UserRoles
        {
            Admin,
            Member,
            Moderator
            
        }
    }
}
