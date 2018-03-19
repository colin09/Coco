using System.Text;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

using com.mh.common.ioc;
using com.mh.common.Logger;

namespace com.mh.common.email
{
    /// <summary>
    /// 电子邮件操作类。
    /// </summary>
    public class Mail
    {


        private static ILog log = IocProvider.GetService<ILog>();
        /// <summary>
        /// 初始化一个电子邮件操作类的空实例。
        /// </summary>
        public Mail()
        {
        }
        /// <summary>
        /// 初始化一个电子邮件操作类的实例。
        /// </summary>
        /// <param name="from">发件人的电子邮件地址。</param>
        /// <param name="fromName">发件人的姓名。</param>
        /// <param name="recipient">收件人的电子邮件地址。</param>
        /// <param name="recipientName">收件人的姓名。</param>
        /// <param name="subject">电子邮件的主题。</param>
        /// <param name="body">电子邮件的内容。</param>
        /// <param name="host">电子邮件的服务器地址。</param>
        /// <param name="port">电子邮件的主机端口号。</param>
        /// <param name="username">登录电子邮件服务器的用户名。</param>
        /// <param name="password">登录电子邮件服务器的用户密码。</param>
        /// <param name="isBodyHtml">邮件的正文是否是HTML格式。</param>
        /// <param name="file">邮件附件的文件路径。</param>
        public Mail(string from, string fromName, string recipient, string recipientName, string subject, string body, string host, int port, string username, string password, bool isBodyHtml, string filepath)
        {
            this._from = from;
            this._fromName = fromName;
            this._recipient = recipient;
            this._recipientName = recipientName;
            this._subject = subject;
            this._body = body;
            this._serverHost = host;
            this._serverPort = port;
            this._username = username;
            this._password = password;
            this._isBodyHtml = isBodyHtml;
            this._attachment.Add(filepath);
        }
        /// <summary>
        /// 初始化一个电子邮件操作类的实例。
        /// </summary>
        /// <param name="from">发件人的电子邮件地址。</param>
        /// <param name="fromName">发件人的姓名。</param>
        /// <param name="recipient">收件人的电子邮件地址。</param>
        /// <param name="recipientName">收件人的姓名。</param>
        /// <param name="subject">电子邮件的主题。</param>
        /// <param name="body">电子邮件的内容。</param>
        /// <param name="host">电子邮件的服务器地址。</param>
        /// <param name="port">电子邮件的主机端口号。</param>
        /// <param name="username">登录电子邮件服务器的用户名。</param>
        /// <param name="password">登录电子邮件服务器的用户密码。</param>
        /// <param name="isBodyHtml">邮件的正文是否是HTML格式。</param>
        public Mail(string from, string fromName, string recipient, string recipientName, string subject, string body, string host, int port, string username, string password, bool isBodyHtml)
            : this(from, fromName, recipient, recipientName, subject, body, host, 25, username, password, isBodyHtml, null)
        {
        }

        /// <summary>
        /// 初始化一个电子邮件操作类的实例。
        /// </summary>
        /// <param name="from">发件人的电子邮件地址。</param>
        /// <param name="fromName">发件人的姓名。</param>
        /// <param name="recipient">收件人的电子邮件地址。</param>
        /// <param name="recipientName">收件人的姓名。</param>
        /// <param name="subject">电子邮件的主题。</param>
        /// <param name="body">电子邮件的内容。</param>
        /// <param name="host">电子邮件的服务器地址。</param>
        /// <param name="port">电子邮件的主机端口号。</param>
        /// <param name="username">登录电子邮件服务器的用户名。</param>
        /// <param name="password">登录电子邮件服务器的用户密码。</param>
        /// <param name="isBodyHtml">邮件的正文是否是HTML格式。</param>
        public Mail(string from, string fromName, string recipient, string recipientName, string subject, string body, string host, string username, string password)
            : this(from, fromName, recipient, recipientName, subject, body, host, 25, username, password, false, null)
        {
        }
        /// <summary>
        /// 初始化一个电子邮件操作类的实例（邮件的正文非HTML格式，端口默认25）。
        /// </summary>
        /// <param name="from">发件人的电子邮件地址。</param>
        /// <param name="recipient">收件人的电子邮件地址。</param>
        /// <param name="subject">电子邮件的主题。</param>
        /// <param name="body">电子邮件的内容。</param>
        /// <param name="host">电子邮件的服务器地址。</param>
        /// <param name="username">登录电子邮件服务器的用户名。</param>
        /// <param name="password">登录电子邮件服务器的用户密码。</param>
        /// <param name="isBodyHtml">邮件的正文是否是HTML格式。</param>
        public Mail(string from, string recipient, string subject, string body, string host, string username, string password)
            : this(from, null, recipient, null, subject, body, host, 25, username, password, false, null)
        {
        }
        /// <summary>
        /// 初始化一个电子邮件操作类的实例（邮件的正文非HTML格式，端口默认25）。
        /// </summary>
        /// <param name="from">发件人的电子邮件地址。</param>
        /// <param name="recipient">收件人的电子邮件地址。</param>
        /// <param name="subject">电子邮件的主题。</param>
        /// <param name="body">电子邮件的内容。</param>
        /// <param name="port">电子邮件的主机端口号。</param>
        /// <param name="host">电子邮件的服务器地址。</param>
        /// <param name="username">登录电子邮件服务器的用户名。</param>
        /// <param name="password">登录电子邮件服务器的用户密码。</param>
        public Mail(string from, string recipient, string subject, string body, string host, int port, string username, string password)
            : this(from, null, recipient, null, subject, body, host, port, username, password, false, null)
        {
        }

        private string _subject;
        private string _body;
        private string _from;
        private string _fromName;
        private string _recipientName;
        private string _serverHost;
        private int _serverPort;
        private string _username;
        private string _password;
        private bool _isBodyHtml;
        private string _recipient;
        private List<string> _attachment = new List<string> { };
        private List<string> _copyCollection = new List<string> { };

        /// <summary>
        /// 获取或设置邮件的主题。
        /// </summary>
        public string Subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }

        /// <summary>
        /// 获取或设置邮件的正文内容。
        /// </summary>
        public string Body
        {
            get { return this._body; }
            set { this._body = value; }
        }

        /// <summary>
        /// 获取或设置发件人的邮件地址。
        /// </summary>
        public string From
        {
            get { return this._from; }
            set { this._from = value; }
        }

        /// <summary>
        /// 获取或设置发件人的姓名。
        /// </summary>
        public string FromName
        {
            get { return this._fromName; }
            set { this._fromName = value; }
        }

        /// <summary>
        /// 获取或设置收件人的姓名。
        /// </summary>
        public string RecipientName
        {
            get { return this._recipientName; }
            set { this._recipientName = value; }
        }

        /// <summary>
        /// 获取或设置收件人的邮件地址。
        /// </summary>
        public string Recipient
        {
            get { return this._recipient; }
            set { this._recipient = value; }
        }

        /// <summary>
        /// 获取或设置邮件服务器主机地址。
        /// </summary>
        public string ServerHost
        {
            get { return this._serverHost; }
            set { this._serverHost = value; }
        }

        /// <summary>
        /// 获取或设置邮件服务器的端口号。
        /// </summary>
        public int ServerPort
        {
            set { this._serverPort = value; }
            get { return this._serverPort; }
        }


        /// <summary>
        /// 获取或设置SMTP认证时使用的用户名。
        /// </summary>
        public string Username
        {
            get { return this._username; }
            set
            {
                if (value.Trim() != "")
                {
                    this._username = value.Trim();
                }
                else
                {
                    this._username = "";
                }
            }
        }

        /// <summary>
        /// 获取或设置SMTP认证时使用的密码。
        /// </summary>
        public string Password
        {
            set { this._password = value; }
            get { return this._password; }
        }

        /// <summary>
        /// 获取或设置指示邮件正文是否为 Html 格式的值。
        /// </summary>
        public bool IsBodyHtml
        {
            get { return this._isBodyHtml; }
            set { this._isBodyHtml = value; }
        }

        /// <summary>
        /// 获取电子邮件附件。
        /// </summary>
        public List<string> Attachment
        {
            get { return this._attachment; }
            set { this._attachment = value; }
        }
        public List<string> CopyTo
        {
            get { return this._copyCollection; }
            set { this._copyCollection = value; }
        }

        ///// <summary>
        ///// 添加一个收件人的邮箱地址。
        ///// </summary>
        ///// <param name="mailList">联系人列表。</param>
        ///// <returns></returns>
        //public bool AddRecipient(params string[] mailList)
        //{
        //    this.Recipient = mailList[0].Trim();
        //    return true;
        //}

        /// <summary>
        /// 添加电子邮件附件。
        /// </summary>
        /// <param name="fileList">文件列表。</param>
        /// <returns>是否添加成功。</returns>
        public bool AddAttachment(params string[] fileList)
        {
            if (fileList.Length > 0)
            {
                foreach (string file in fileList)
                {
                    if (file != null)
                    {
                        this._attachment.Add(file);
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 发送电子邮件。
        /// </summary>
        /// <returns>是否发送成功。</returns>
        public bool Send()
        {
            //初始化 MailMessage 对象。
            MailMessage mail = new MailMessage();
            Encoding encoding = Encoding.GetEncoding("utf-8");
            mail.From = new MailAddress(this.From, this.FromName, encoding);
            mail.To.Add(new MailAddress(this.Recipient, this.RecipientName));

            if (this.CopyTo.Count > 0)
            {
                foreach (string address in this.CopyTo)
                {
                    mail.CC.Add(new MailAddress(address));
                }
            }


            mail.Subject = this.Subject;
            mail.IsBodyHtml = this.IsBodyHtml;
            mail.Body = this.Body;
            mail.Priority = MailPriority.Normal;
            mail.BodyEncoding = encoding;
            if (this.Attachment.Count > 0)
            {
                foreach (string file in this.Attachment)
                {
                    mail.Attachments.Add(new Attachment(file));
                }
            }
            //初始化 SmtpClient 对象。
            SmtpClient smtp = new SmtpClient();
            smtp.Timeout = 5 * 60 * 1000;
            smtp.Credentials = new NetworkCredential(this.Username, this.Password);
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Host = this.ServerHost;
            smtp.Port = this.ServerPort;
            if (smtp.Port != 25)
            {
                smtp.EnableSsl = true;
            }
            try
            {
                //log.Info($"send by {smtp.Host}:{smtp.Port}/{this.Username}@{this.Password}");
                smtp.Send(mail);
            }
            catch (SmtpException ex)
            {
                string message = ex.Message;
                log.Info(ex);
                return false;
            }
            return true;
        }
    }
}
