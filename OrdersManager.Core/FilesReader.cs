using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace OrdersManager.Core
{

    public class FilesReader : IFilesReader
    {
        public IEnumerable<string> Files { get; protected set; }
        private readonly IEnumerable<string> _supportedTypes;

        public FilesReader()
        {
            _supportedTypes = Enum
                .GetValues(typeof(SupportedTypes))
                .Cast<SupportedTypes>()
                .Select(x => x.ToString());
        }

        public void ReadFiles(string dirPath, SearchOption option)
        {
            try
            {
                Files = Directory.GetFiles(dirPath, "*.*", option)
                .Where(file => _supportedTypes.Any(x => file.EndsWith($".{x}", StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception)
            {
                throw;
            }  
        }        
    }
}
