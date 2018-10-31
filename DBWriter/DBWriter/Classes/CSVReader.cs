using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace DBWriter.Classes
{
    public class CSVReader
    {
        private readonly string _path;
        private readonly List<FileInfo> _files;
        private List<string> _records; 

        public CSVReader()
        {
            try
            {
                _path = ConfigurationSettings.AppSettings.Get("rootFolder");
                _files = ReadFilesInFolder();
                _records = new List<string>();
                foreach (FileInfo f in _files)
                {
                    ReadSingleCsv(f.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

        }

        public List<string> Start()
        {
            return _records;
        }

        private List<FileInfo> ReadFilesInFolder()
        {
            try
            {
                List<string> files = new List<string>();
                DirectoryInfo d = new DirectoryInfo(_path);
                FileInfo[] f = d.GetFiles("*.csv");
                return f.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
            
        }

        private void ReadSingleCsv(string FileName)
        {
            try
            {
                string completePath = _path + @"\" + FileName;
                using (var reader = new StreamReader(completePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        line += "";
                        _records.Add(line.Insert(0, FileName + ","));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void CSVKiller(string FileName)
        {
            string completePath = _path + @"\" + FileName;
            File.Delete(completePath);
        }
    }
}
