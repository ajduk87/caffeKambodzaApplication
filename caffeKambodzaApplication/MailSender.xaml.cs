using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;

namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for MailSender.xaml
    /// </summary>
    public partial class MailSender : System.Windows.Controls.UserControl
    {
        public MailSender()
        {
            InitializeComponent();
        }

        private void btnChooseFileForMail_Click(object sender, RoutedEventArgs e)
        {

            string dirPath = String.Empty;
            OpenFileDialog fileDlg = new OpenFileDialog();

            // Show open file dialog box 
            DialogResult result = fileDlg.ShowDialog();

            // Process open file dialog box results 
            if (result == DialogResult.OK)
            {
                dirPath = fileDlg.FileName;
            }

            tfPathForMailSending.Text = dirPath;

        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tfPathForMailSending.Text.EndsWith(".xls") || tfPathForMailSending.Text.EndsWith(".xlsx"))
                {
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Morate izabrati excel file. To su fajlovi sa .xls ili .xlsx zavrsetkom!!");
                    return;
                }

                Ping ping = new Ping();
                PingReply pingReply = ping.Send("8.8.8.8");

                if (pingReply.Status == IPStatus.Success)
                {
                    //Machine is alive

                    //send to gmail
                    MailMessage message = new MailMessage();

                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        //Credentials = new NetworkCredential("caffekambodzaapplication@gmail.com", "draganagaga"),
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network
                    };
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("caffekambodzaapplication@gmail.com", "draganagaga");
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                          System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                          System.Security.Cryptography.X509Certificates.X509Chain chain,
                          System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    message.From = new MailAddress("caffekambodzaapplication@gmail.com");
                    message.To.Add(new MailAddress("caffekambodzaapplication@gmail.com"));
                    message.Subject = "Izvestaj u vremenu : " + DateTime.Now;
                    message.Body = "\r\n" + "Izvestaj " + DateTime.Now + "      Ovo je automatska poruka programa caffeKambodzaApplication!!!";
                    message.Attachments.Add(new Attachment(tfPathForMailSending.Text));
                    client.Send(message);

                    System.Windows.Forms.MessageBox.Show("Uspesno slanje fajla : " + tfPathForMailSending.Text);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Niste konektovani na INTERNET  !!!! Neuspesno slanje maila !!!!");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error message : " + ex.Message);
            }
        }
    }
}
