using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Marina.Utils
{
    public static class FS
    {
        public static FileInfo GetFile(this DirectoryInfo obj, string filename) => new FileInfo($"{obj.FullName}{Path.DirectorySeparatorChar}{filename}");

        public static DirectoryInfo GetDirectory(this DirectoryInfo obj, string foldername) => new DirectoryInfo($"{obj.FullName}{Path.DirectorySeparatorChar}{foldername.Replace('/', Path.DirectorySeparatorChar)}");

        public static long GetSize(this DirectoryInfo obj)
        {
            IEnumerable<FileInfo> files = obj.EnumerateFilesRecursively();
            return files.Sum((f) => f.Length);
        }

        public static void DeleteContents(this DirectoryInfo obj)
        {
            IEnumerable<DirectoryInfo> Directories = obj.EnumerateDirectories("*", SearchOption.TopDirectoryOnly);
            IEnumerable<FileInfo> Files = obj.EnumerateFiles("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo directory in Directories)
                directory.Delete(true);

            foreach (FileInfo file in Files)
                file.Delete();
        }

        public static IEnumerable<FileInfo> EnumerateFilesRecursively(this DirectoryInfo obj) => obj.EnumerateFiles("*", SearchOption.AllDirectories);

        public static void AppendAllText(this FileInfo obj, string contents) => File.AppendAllText(obj.FullName, contents);
        public static void AppendAllText(this FileInfo obj, string contents, Encoding encoding) => File.AppendAllText(obj.FullName, contents, encoding);

        public static string ReadAllText(this FileInfo obj) => File.ReadAllText(obj.FullName);
        public static string ReadAllText(this FileInfo obj, Encoding encoding) => File.ReadAllText(obj.FullName, encoding);

        public static void WriteAllText(this FileInfo obj, string contents) => File.WriteAllText(obj.FullName, contents);
        public static void WriteAllText(this FileInfo obj, string contents, Encoding encoding) => File.WriteAllText(obj.FullName, contents, encoding);
    }
}
