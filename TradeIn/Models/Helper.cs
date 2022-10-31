using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TradeIn.Data;

namespace TradeIn.Models
{
    public static class Helper

    {
        // private static TradeInContext _context;
        //public static void HelperConfig(IHttpContextAccessor context)
        //{
        //}

        public static string getBrand(int? Id)
        {
            using (var _context = new TradeIn.Data.TradeInContext())
            {
                var brand = _context.Brands.FirstOrDefault(x => x.Id == Id);
                return brand == null ? "" : brand!.Description;
            }
        }

        public static string getModel(int? Id)
        {
            using (var _context = new TradeIn.Data.TradeInContext())
            {
                var model = _context.Models.FirstOrDefault(x => x.Id == Id);
                return model == null ? "" : model!.Description;
            }
        }

        public static string getGen(int? Id)
        {
            using (var _context = new TradeIn.Data.TradeInContext())
            {
                var gen = _context.Generations.FirstOrDefault(x => x.Id == Id);
                return gen == null ? "" : gen!.Description;
            }
        }

        public static void SendEmails(string msgbody, string sendto, string subject)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string from = configurationRoot.GetSection("SmtpSettings:From").Value;

            if (from != "")
            {
                foreach (string s in sendto.Split(','))
                {
                    if (s != null && s.Trim() != "")
                    {
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress(from);
                        msg.Subject = subject;
                        msg.Body = msgbody;
                        msg.To.Add(s.Trim());
                        msg.IsBodyHtml = true;
                        string SMTPServer =
                            configurationRoot.GetSection("SmtpSettings:SmtpServer") == null ||
                             configurationRoot.GetSection("SmtpSettings:SmtpServer").Value == ""
                                ? "auth.smtp.1and1.co.uk"
                                : configurationRoot.GetSection("SmtpSettings:SmtpServer").Value;
                        string SMTPPassword =
                             configurationRoot.GetSection("SmtpSettings:SmtpPassword") == null ||
                            configurationRoot.GetSection("SmtpSettings:SmtpPassword").Value == ""
                                ? ""
                                : configurationRoot.GetSection("SmtpSettings:SmtpPassword").Value;
                        string SMTPUserName =
                            configurationRoot.GetSection("SmtpSettings:SmtpUserName") == null ||
                            configurationRoot.GetSection("SmtpSettings:SmtpUserName").Value == ""
                                ? ""
                                : configurationRoot.GetSection("SmtpSettings:SmtpUserName").Value;

                        SmtpClient smtpClient = new SmtpClient();
                        NetworkCredential basicCredential = new NetworkCredential(SMTPUserName, SMTPPassword);
                        smtpClient.Host = SMTPServer;
                        if (configurationRoot.GetSection("SmtpSettings:Port") != null &&
                            configurationRoot.GetSection("SmtpSettings:Port").Value.ToString() != "")
                            smtpClient.Port = int.Parse(configurationRoot.GetSection("SmtpSettings:Port").Value.ToString());
                        else
                            smtpClient.Port = 587;
                        if (configurationRoot.GetSection("SmtpSettings:EnableSSL") != null &&
                            configurationRoot.GetSection("SmtpSettings:EnableSSL").Value.ToString() != "")
                            smtpClient.EnableSsl = bool.Parse(configurationRoot.GetSection("SmtpSettings:EnableSSL").Value.ToString());

                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = basicCredential;
                        try
                        {
                            smtpClient.Send(msg);
                        }
                        catch (Exception sendexp)
                        {
                        }
                    }
                }
            }
        }

        public static void SendEmails(string from, string msgbody, string sendto, string subject,
            List<System.Net.Mail.Attachment> attachments)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            if (from == "")

                from = configurationRoot.GetSection("SmtpSettings:From").Value;

            if (from != "")
            {
                foreach (string s in sendto.Split(','))
                {
                    if (s != null && s.Trim() != "")
                    {
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress(from);
                        msg.Subject = subject;
                        msg.Body = msgbody;
                        msg.To.Add(s.Trim());
                        msg.IsBodyHtml = true;
                        if (attachments != null)
                        {
                            foreach (var attach in attachments)
                                msg.Attachments.Add(attach);
                        }

                        string SMTPServer =
                            configurationRoot.GetSection("SmtpSettings:SmtpServer") == null ||
                             configurationRoot.GetSection("SmtpSettings:SmtpServer").Value == ""
                                ? "auth.smtp.1and1.co.uk"
                                : configurationRoot.GetSection("SmtpSettings:SmtpServer").Value;
                        string SMTPPassword =
                             configurationRoot.GetSection("SmtpSettings:SmtpPassword") == null ||
                            configurationRoot.GetSection("SmtpSettings:SmtpPassword").Value == ""
                                ? ""
                                : configurationRoot.GetSection("SmtpSettings:SmtpPassword").Value;
                        string SMTPUserName =
                            configurationRoot.GetSection("SmtpSettings:SmtpUserName") == null ||
                            configurationRoot.GetSection("SmtpSettings:SmtpUserName").Value == ""
                                ? ""
                                : configurationRoot.GetSection("SmtpSettings:SmtpUserName").Value;

                        SmtpClient smtpClient = new SmtpClient();
                        NetworkCredential basicCredential = new NetworkCredential(SMTPUserName, SMTPPassword);
                        smtpClient.Host = SMTPServer;
                        if (configurationRoot.GetSection("SmtpSettings:Port") != null &&
                            configurationRoot.GetSection("SmtpSettings:Port").Value.ToString() != "")
                            smtpClient.Port = int.Parse(configurationRoot.GetSection("SmtpSettings:Port").Value.ToString());
                        else
                            smtpClient.Port = 587;
                        if (configurationRoot.GetSection("SmtpSettings:EnableSSL") != null &&
                            configurationRoot.GetSection("SmtpSettings:EnableSSL").Value.ToString() != "")
                            smtpClient.EnableSsl = bool.Parse(configurationRoot.GetSection("SmtpSettings:EnableSSL").Value.ToString());

                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = basicCredential;
                        try
                        {
                            smtpClient.Send(msg);
                        }
                        catch (Exception sendexp)
                        {
                        }
                    }
                }
            }
        }
    }
}
