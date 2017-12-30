namespace Markdown2Json.Services
{
    public interface IFileService
    {
        void CreateIfNotExists(string path);
        void OverwriteFile(string path, string text);
    }
}