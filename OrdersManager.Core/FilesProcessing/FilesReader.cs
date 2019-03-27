using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrdersManager.Core.FilesProcessing
{
    public class FilesReader : IFilesReader
    {
        public IEnumerable<string> Files { get; protected set; }
        public IEnumerable<string> SupportedExtensions { get; }

        public FilesReader()
        {
            SupportedExtensions = Enum
                .GetValues(typeof(SupportedExtensions))
                .Cast<SupportedExtensions>()
                .Select(x => x.ToString());
        }

        public void ReadFiles(string dirPath, SearchOption option)
        {
            try
            {
                Files = Directory.GetFiles(dirPath, "*.*", option)
                .Where(file => SupportedExtensions.Any(x => file.EndsWith($".{x}", StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
