using System;
using System.Collections.Generic;

using com.mh.common.ioc;
using com.mh.common.Logger;
using com.mh.common.configuration;

namespace com.mh.common.email
{
    public class EMailClient
    {
        private static ILog log = IocProvider.GetService<ILog>();

        public static bool SendByReport(string recipient, string subject, string body, List<string> copyTo, List<string> attachment)
        {
            try
            {
                var host = ConfigManager.EMailSenderSmtp;
                var port = ConfigManager.EMailSenderPort;
                var username = ConfigManager.EMailSenderAddress;
                var password = ConfigManager.EMailSenderPwd;

                var from = ConfigManager.EMailSenderAddress;
                var fromName = ConfigManager.EMailSenderName;

                var mail = new Mail(from, fromName, recipient,"", subject, body, host, port, username, password,true);
                if (attachment != null)
                    mail.Attachment = attachment;
                if (copyTo != null)
                    mail.CopyTo = copyTo;

                return mail.Send();

            }
            catch (Exception ex)
            {
                log.Info(ex);
                return false;
            }
        }

        public static bool SendByReport(string recipient, string subject, string body)
        {
            return SendByReport(recipient, subject, body, null, null);
        }
    }
}
