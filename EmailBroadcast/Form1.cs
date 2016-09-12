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
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ToAddressService toService = new ToAddressService();
            var ll = toService.GetToAddresses();
            ClientInfoService clientService = new ClientInfoService();
            var cc = clientService.GetClientInfo();
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
            catch(Exception ex)
            {

            }
        }
    }
}
