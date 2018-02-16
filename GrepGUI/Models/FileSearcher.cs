using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrepGUI.Models
{
    public static class FileSearcher
    {
        public static IEnumerable<FileInfo> Search(string filePath, string keyword) => GetAllFiles(filePath).Select(x => new FileInfo() { FilePath = Path.GetDirectoryName(x), FileName = Path.GetFileName(x), Line = 0, String = "" });

        public static List<string> GetAllFiles(string DirPath)
        {
            var lstStr = new List<string>();

            try
            {
                // ファイル取得
                lstStr.AddRange(Directory.GetFiles(DirPath));

                // ディレクトリの取得
                foreach (var dir in Directory.GetDirectories(DirPath))
                {
                    lstStr.AddRange(GetAllFiles(dir));
                }
            }
            catch (UnauthorizedAccessException)
            {
                // アクセスできなかったので無視
            }

            return lstStr;
        }
    }
}
