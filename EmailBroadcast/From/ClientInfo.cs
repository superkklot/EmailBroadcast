using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBroadcast.From
{
    public class ClientInfo
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FromAddress { get; set; }
        public bool EnableSsl { get; set; }
    }
}
