using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{
  public  class NEmail
    {

        public static bool SendTo(string ServerAdrress,string SendEmail,string SendPw,string SendName,string ToEmail,string ToName,string SubContent,string SubTitle)
        {
            try {
            System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress(SendEmail, SendName); //填写电子邮件地址，和显示名称

            System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(ToEmail, ToName); //填写邮件的收件人地址和名称
            //设置好发送地址，和接收地址，接收地址可以是多个
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = from;
            mail.To.Add(to);
            mail.Subject = SubTitle;
            mail.Body = SubContent;
            mail.IsBodyHtml = true;//设置显示htmls
            //设置好发送邮件服务地址
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = ServerAdrress;
            //填写服务器地址相关的用户名和密码信息
            client.Credentials = new System.Net.NetworkCredential(SendEmail, SendPw);
            //发送邮件
            client.Send(mail);
            return true;
            }
            catch (Exception ex) {
            
            
            }

            return false;
        }
    }
}
