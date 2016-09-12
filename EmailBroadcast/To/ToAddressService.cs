using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBroadcast.To
{
    public class ToAddressService
    {
        private const string _fileName = "ToAddress.txt";
        private string _fullPath = "";
        public ToAddressService()
        {
            var basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            _fullPath = basePath + _fileName;
        }

        public List<string> GetToAddresses()
        {
            var exists = File.Exists(_fullPath);
            if (!exists)
                return null;

            var fileStream = File.Open(_fullPath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fileStream);
            List<string> rets = new List<string>();
            while(!sr.EndOfStream)
            {
                var tmpStr = sr.ReadLine();
                rets.Add(tmpStr);
            }
            sr.Close();
            fileStream.Close();
            return rets;
        }
    }
}
