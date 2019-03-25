using System.Collections.Generic;
using System.IO;

namespace OrdersManager.Core
{
    public interface IFilesReader
    {
        IEnumerable<string> Files { get; }

        void ReadFiles(string dirPath, SearchOption option);
    }
}