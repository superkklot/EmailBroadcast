using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBroadcast
{
    public class Log : IDisposable
    {
        private const string _fileName = "log.txt";
        private string _fullPath = "";
        private FileStream _stream;
        private StreamWriter _streamWriter;
        public Log()
        {
            _fullPath = System.AppDomain.CurrentDomain.BaseDirectory + _fileName;
            _stream = File.Open(_fullPath, FileMode.OpenOrCreate, FileAccess.Read);
            _streamWriter = new StreamWriter(_stream);
        }

        public void Info(string msg)
        {
            msg = string.Format("{0}：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg);
            _streamWriter.WriteLine(msg);
        }

        public void Dispose()
        {
            _streamWriter.Close();
            _streamWriter.Close();
        }
    }
}
