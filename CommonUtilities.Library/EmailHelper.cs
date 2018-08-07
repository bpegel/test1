using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace CommonUtilities.Library
{
    public class EmailHelper
    {
        public class EmailProperties
        {
            public string SmtpServer { get; set; }
            public bool EnableSSL { get; set; } 
            public string From { get; set; }
            public string To { get; set; }
            public string CC { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public IList<string> Attachments { get; set; }

        }


        public static bool SendEmail(EmailProperties emailProperties, ref string outputMessage)
        {
            bool mailStatus = true;
            try
            {
                if (string.IsNullOrEmpty(emailProperties.SmtpServer))
                {
                    outputMessage += "Email Module : Empty/Null SmtpServer specified." + Environment.NewLine;
                    mailStatus = false;
                }
                if (string.IsNullOrEmpty(emailProperties.To))
                {
                    outputMessage = "Email Module : Empty/Null ToAddress specified." + Environment.NewLine;
                    mailStatus = false;
                }

                emailProperties.From = string.IsNullOrEmpty(emailProperties.From) ? "tfsservice@molinahealthcare.com" : emailProperties.From;
                emailProperties.Subject = string.IsNullOrEmpty(emailProperties.Subject) ? "Email subject is not specified." : emailProperties.Subject;
                emailProperties.Body = string.IsNullOrEmpty(emailProperties.Body) ? "Email body is not specified. " : emailProperties.Body;

                // Process email only if there are no email property issues
                if (mailStatus)
                {
                    SmtpClient objSMTPClient = new SmtpClient();
                    objSMTPClient.Host      = emailProperties.SmtpServer;
                    objSMTPClient.EnableSsl = emailProperties.EnableSSL;

                    if (!string.IsNullOrEmpty(emailProperties.UserName) && !string.IsNullOrEmpty(emailProperties.Password))
                    {
                        objSMTPClient.Credentials = new NetworkCredential(emailProperties.UserName, emailProperties.Password);
                        objSMTPClient.UseDefaultCredentials = true;
                    }
                    else
                        objSMTPClient.UseDefaultCredentials = true;

                    // Prepare MailMessage since we need to setup CC
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From        = new MailAddress(emailProperties.From);
                    mailMessage.Subject     = emailProperties.Subject;
                    mailMessage.Body        = emailProperties.Body;

                    if (emailProperties.To?.Length > 0)
                    {
                        foreach (var toAddress in emailProperties.To.Split(new[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries))
                            mailMessage.To.Add(toAddress);
                    }

                    if (emailProperties.CC?.Length > 0)
                    {
                        foreach (var ccAddress in emailProperties.CC.Split(new[] { ";" , ","}, StringSplitOptions.RemoveEmptyEntries))
                            mailMessage.CC.Add(ccAddress);
                    }

                    if (emailProperties.Attachments?.Count>0)
                    {
                        foreach(string attachmentFileName in emailProperties.Attachments)
                        {
                            if (System.IO.File.Exists(attachmentFileName))
                            {
                                // Create  the file attachment for this e-mail message.
                                Attachment data = new Attachment(attachmentFileName, MediaTypeNames.Application.Octet);
                                
                                // Add time stamp information for the file.
                                ContentDisposition disposition = data.ContentDisposition;
                                disposition.CreationDate = System.IO.File.GetCreationTime(attachmentFileName);
                                disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachmentFileName);
                                disposition.ReadDate = System.IO.File.GetLastAccessTime(attachmentFileName);
                                
                                // Add the file attachment to this e-mail message.
                                mailMessage.Attachments.Add(data);
                            }
                        }
                    }

                    //mailMessage.IsBodyHtml = true;

                    // Finally send the message
                    objSMTPClient.Send(mailMessage);
                }

                return mailStatus;
            }
            catch (Exception ex)
            {
                outputMessage = "Email Module : EXCEPTION " + ex.Message + Environment.NewLine;
                return false;
            }
        }
    }
}
