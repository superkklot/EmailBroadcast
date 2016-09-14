using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBroadcast.From
{
    public class ClientInfoService
    {
        private const string _fileName = "FromAddress.txt";
        private string _fullPath = "";
        public ClientInfoService()
        {
            var basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            _fullPath = basePath + _fileName;
        }

        public List<ClientInfo> GetClientInfo()
        {
            var exists = File.Exists(_fullPath);
            if (!exists)
                return null;

            var fileStream = File.Open(_fullPath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fileStream);
            List<ClientInfo> rets = new List<ClientInfo>();
            while (!sr.EndOfStream)
            {
                var tmpStr = sr.ReadLine();
                if (tmpStr.StartsWith("//"))
                    continue;

                var strList = tmpStr.Split('\t');
                if (strList.Length >= 6)
                {
                    ClientInfo ret = new ClientInfo();
                    ret.Host = strList[0];
                    ret.UserName = strList[1];
                    ret.Password = strList[2];
                    ret.FromAddress = strList[3];
                    ret.EnableSsl = strList[4] == "true";
                    ret.MaxTos = int.Parse(strList[5]);
                    rets.Add(ret);
                }
            }
            sr.Close();
            fileStream.Close();
            return rets;
        }
    }
}
