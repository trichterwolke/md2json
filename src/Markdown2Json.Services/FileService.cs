using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Markdown2Json.Services
{
    public class FileService : IFileService
    {
        public void OverwriteFile(string path, string text)
        {
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
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
