using EmailBroadcast.Body;
using EmailBroadcast.From;
using EmailBroadcast.To;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBroadcast
{
    public partial class Form1 : Form
    {
        private ToAddressService _toService;
        private ClientInfoService _fromService;
        private int _unitToAddress;
        private int _waitSeconds;
        private Content _content;

        public Form1()
        {
            InitializeComponent();
            _toService = new ToAddressService();
            _fromService = new ClientInfoService();
            _unitToAddress = ConfigHelper.GetUnitToAddressNumber();
            _waitSeconds = ConfigHelper.GetWaitSeconds();
            _content = new Content();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webContent.Url = new Uri(_content.FullPath);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(_unitToAddress.ToString());
            var toAddressList = _toService.GetToAddresses();
            if(toAddressList == null || toAddressList.Count == 0)
            {
                MessageBox.Show("接收地址为空");
                return;
            }
            var fromAddressList = _fromService.GetClientInfo();
            if(fromAddressList == null || fromAddressList.Count == 0)
            {
                MessageBox.Show("发送服务地址为空");
                return;
            }
            //此种方式有限制
            //int page = (int)Math.Ceiling((double)toAddressList.Count / _unitToAddress);
            //for(int i = 0; i < page; i++)
            //{
            //    var index = i % fromAddressList.Count;
            //    var fromAddress = fromAddressList[index];
            //    var toAddresses = toAddressList.Skip(i * _unitToAddress).Take(_unitToAddress).ToList();
            //    SendEmail(fromAddress, toAddresses);
            //}
            int i = 0, j = 0;
            while(i < toAddressList.Count)
            {
                var index = j++ % fromAddressList.Count;
                var fromAddress = fromAddressList[index];
                if (fromAddress.MaxTos <= 0) continue;
                var toAddresses = toAddressList.Skip(i).Take(fromAddress.MaxTos).ToList();
                i += fromAddress.MaxTos;
                SendEmail(fromAddress, toAddresses);
                if(index == 0 && j != 1)
                {
                    Thread.Sleep(_waitSeconds * 1000);
                }
            }
            MessageBox.Show("发送成功，请查看日志");
        }

        private void SendEmail(ClientInfo fromAddress, List<string> toAddresses)
        {
            using (var log = new Log())
            {
                try
                {
                    var client = GetClient(fromAddress);
                    var msg = GetMessage(fromAddress, toAddresses);
                    client.Send(msg);
                    log.Info(string.Format("发送成功，from=[{0}]，tos=[{1}]", fromAddress.FromAddress
                        , string.Join(",", toAddresses)));
                }
                catch (Exception ex)
                {
                    log.Info(string.Format("发送失败，from=[{0}]，tos=[{1}]，exception=[{2}]", fromAddress.FromAddress
                        , string.Join(",", toAddresses), ex.ToString()));
                }
            }
        }

        private SmtpClient GetClient(ClientInfo fromAddress)
        {
            SmtpClient client = new SmtpClient();
            client.Host = fromAddress.Host;
            client.UseDefaultCredentials = true;
            if (fromAddress.EnableSsl)
            {
                client.EnableSsl = true;
            }
            client.Credentials = new NetworkCredential(fromAddress.UserName, fromAddress.Password);
            return client;
        }

        private MailMessage GetMessage(ClientInfo fromAddress, List<string> toAddresses)
        {
            MailMessage msg = new MailMessage();
            toAddresses.ForEach(m => msg.To.Add(m));
            msg.From = new MailAddress(fromAddress.FromAddress);
            msg.Subject = txtSubject.Text ?? "";
            msg.IsBodyHtml = true;
            msg.Body = _content.Body;
            return msg;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.live.com";
                client.UseDefaultCredentials = true;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("marytrading001@hotmail.com", "Superkklot1");
                MailMessage msg = new MailMessage();
                msg.To.Add("mjb-709@163.com");
                msg.From = new MailAddress("marytrading001@hotmail.com");
                msg.Subject = "aaaaaaaaa";
                msg.Body = "你好啊";
                client.Send(msg);

                //SmtpClient client = new SmtpClient();
                //client.Host = "smtp.163.com";
                //client.UseDefaultCredentials = true;
                //client.Credentials = new NetworkCredential("mjb-709", "super709");
                //MailMessage msg = new MailMessage();
                //msg.To.Add("mjb-709@163.com");
                //msg.From = new MailAddress("mjb-709@163.com");
                //msg.Subject = "test";
                //msg.Body = "abcdefg";
                //client.Send(msg);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
