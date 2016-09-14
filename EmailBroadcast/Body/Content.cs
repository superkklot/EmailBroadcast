using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBroadcast.Body
{
    public class Content
    {
        private const string _fileName = "Body.html";
        private string _fullPath = "";
        private string _body = "";

        public Content()
        {
            _fullPath = System.AppDomain.CurrentDomain.BaseDirectory + _fileName;
            _body = Load();
        }

        public string FullPath { get { return _fullPath; } }
        public string Body { get { return _body; } }

        private string Load()
        {
            var exists = File.Exists(_fullPath);
            if (!exists)
                return "";

            var fileStream = File.Open(_fullPath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fileStream);
            var body = sr.ReadToEnd();
            sr.Close();
            fileStream.Close();
            return body;
        }
    }
}
