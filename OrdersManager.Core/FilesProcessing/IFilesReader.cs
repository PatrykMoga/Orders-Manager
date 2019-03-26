using System.Collections.Generic;
using System.IO;

namespace OrdersManager.Core.FilesProcessing
{
    public interface IFilesReader
    {
        IEnumerable<string> Files { get; }
        IEnumerable<string> SupportedExtensions { get; }

        void ReadFiles(string dirPath, SearchOption option);
    }
}