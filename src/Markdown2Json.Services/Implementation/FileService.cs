namespace Markdown2Json.Services.Implementation
{
    using System.IO;
    using System.Text;

    public class FileService : IFileService
    {
        public void OverwriteFile(string path, string text)
        {
            //var mode = File.Exists(path) ? FileMode.Truncate : FileMode.cr 

            using (var stream = new FileStream(path, FileMode.Create))
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {               
                writer.Write(text);
            }
        }

        public void CreateIfNotExists(string path)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }
    }
}
