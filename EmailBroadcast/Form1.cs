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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBroadcast
{
    public partial class Form1 : Form
    {
        private ToAddressService _toService;
        private ClientInfoService _fromService;
        private int _unitToAddress;
        private Content _content;

        public Form1()
        {
            InitializeComponent();
            _toService = new ToAddressService();
            _fromService = new ClientInfoService();
            _unitToAddress = ConfigHelper.GetUnitToAddressNumber();
            _content = new Content();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webContent.Url = new Uri(_content.FullPath);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_unitToAddress.ToString());
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
            int page = (int)Math.Ceiling((double)toAddressList.Count / _unitToAddress);
            for(int i = 0; i < page; i++)
            {
                var index = i % fromAddressList.Count;
                var fromAddress = fromAddressList[index];
                var toAddresses = toAddressList.Skip(i * _unitToAddress).Take(_unitToAddress).ToList();
                SendEmail(fromAddress, toAddresses);
            }
            MessageBox.Show("发送成功，请查看日志");
        }

        private void SendEmail(ClientInfo fromAddress, List<string> toAddresses)
        {
            using (var log = new Log())
            {
                try
                {

                }
                catch (Exception ex)
                {
                    log.Info(string.Format("发送失败-{0}", ex.ToString()));
                }
            }
        }


        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                //SmtpClient client = new SmtpClient();
                //client.Host = "smtp.mail.yahoo.com";
                //client.UseDefaultCredentials = true;
                //client.EnableSsl = true;
                //client.Credentials = new NetworkCredential("marytrading001", "Superkklot1");
                //MailMessage msg = new MailMessage();
                //msg.To.Add("mjb-709@163.com");
                //msg.From = new MailAddress("marytrading001@yahoo.com");
                //msg.Subject = "test";
                //msg.Body = "abcdefg";
                //client.Send(msg);

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
