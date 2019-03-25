using System.Collections.Generic;
using System.IO;

namespace OrdersManager.Core
{
    public interface IFilesReader
    {
        IEnumerable<string> Files { get; }
        IEnumerable<string> SupportedTypes { get; }

        void ReadFiles(string dirPath, SearchOption option);
    }
}