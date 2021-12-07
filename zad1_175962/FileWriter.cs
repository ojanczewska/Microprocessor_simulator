using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2_175962
{
    class FileWriter
    {
        private string path;
        private string content;

        public FileWriter(string path, string content)
        {
            this.path = path;
            this.content = content;
        }

        public void WriteToFile()
        {
            try
            {
                System.IO.File.WriteAllText(path, content);
            }
            catch (System.UnauthorizedAccessException)
            {
                path = @"C:\\Users\Public\\";
                System.IO.File.WriteAllText(path, content);
            }
        }
    }
}
