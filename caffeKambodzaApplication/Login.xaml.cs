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
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Data.OleDb;
using System.Management;

namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory +  Constants.DATABASECONNECTION_APP);
        private OleDbCommand com;
        private OleDbDataReader dr;

        private MainWindow window;
        private UserRegistration reg;
        private UpdatePassword upPass;
        private bool ISOK = false;
        
        
       




        public Login()
        {
            InitializeComponent();

            string cpuid, mac;

            cpuid = getCPUID();
            mac = GetSystemMACID();


            //security check

            if ((cpuid.Equals("BFEBFBFF000206A7") && mac.Equals("B8-88-E3-42-97-55")) || (cpuid.Equals("BFEBFBFF000206A7") && mac.Equals("E0-06-E6-27-96-5B")) || ((cpuid.Equals("BFEBFBFF000306A9") && mac.Equals("D4-3D-7E-BB-98-81"))))
            {
                Console.WriteLine("Everything is OK! Run this application!");
            }
            else
            {
                MessageBox.Show("Pogrešna instalacija!");
                Logger.writeNode(Constants.ERROR, "Pogrešna instalacija!");
                System.Environment.Exit(0);
            }

            //security check

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            reg = new UserRegistration();
            upPass = new UpdatePassword();
            window = new MainWindow();
            ISOK = false;


        }

       

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string id = "23";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                
                con.Open();
                com = new OleDbCommand(query, con);
                dr = com.ExecuteReader();


                string user = String.Empty;
                string pass = String.Empty;
				
				
                while (dr.Read())
                {
                    user = dr["UserName"].ToString();
                    pass = dr["UserPassword"].ToString();

                    if (tfUser.Text.Equals(user) && tfPassword.Password.Equals(pass))
                    {
                        ISOK = true;
                        this.Hide();
                        break;
                    }
                   
                }

                if (ISOK) 
                {
                    window.Show();
                    Logger.writeNode(Constants.INFORMATION, "Ulogovao se korisnik " + user);
                }
                else if (ISOK == false)
                {
                    MessageBox.Show("Niste uneli ispravnu šifru ili korisničko ime!" , "DALJE NEĆEŠ MOĆI");
                    Logger.writeNode(Constants.MESSAGEBOX, "Niste uneli ispravnu šifru ili korisničko ime!");
                    Logger.writeNode(Constants.INFORMATION, "Neuspešno logovanje korisnika " + user);
                    System.Environment.Exit(0);
                }
               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                if (dr != null)
                {
                    dr.Close();
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
              MessageBox.Show("Kliknuli ste na dugme za izlazak iz aplikacije!", "IZLAZAK IZ APLIKACIJE");
              Logger.writeNode(Constants.MESSAGEBOX, "Kliknuli ste na dugme za izlazak iz aplikacije!");
              Logger.writeNode(Constants.INFORMATION, "Izlazak iz aplikacije korisnika " + tfUser.Text);
              System.Environment.Exit(0);
      
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            reg.Show();
            Logger.writeNode(Constants.INFORMATION, "Registracija novog korisnika");
        }

        private void btnPassUpdate_Click(object sender, RoutedEventArgs e)
        {
            upPass.Show();
            Logger.writeNode(Constants.INFORMATION, "Promena lozinke korisnika " + tfUser);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            Logger.writeNode(Constants.INFORMATION, "Korisnik " + tfUser + " je kliknuo na iks dugme da bi izašao iz aplikacije");
            System.Environment.Exit(0);
        }



          private  string GetSystemMACID()
         {
            string systemName = System.Windows.Forms.SystemInformation.ComputerName;
            try
            {
                ManagementScope theScope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
                ObjectQuery theQuery = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter");
                ManagementObjectSearcher theSearcher = new ManagementObjectSearcher(theScope, theQuery);
                ManagementObjectCollection theCollectionOfResults = theSearcher.Get();

                foreach (ManagementObject theCurrentObject in theCollectionOfResults)
                {
                    if (theCurrentObject["MACAddress"] != null)
                    {
                        string macAdd = theCurrentObject["MACAddress"].ToString();
                        return macAdd.Replace(':', '-');
                    }
                }
            }
            catch (ManagementException e)
            {
            }
            catch (System.UnauthorizedAccessException e)
            {

            }
            return string.Empty;
        }



       private String getCPUID()
       {
	        String cpuid = "";
	    try
	    {
		    ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
		    ManagementObjectCollection mbsList = mbs.Get();
    
		    foreach (ManagementObject mo in mbsList)
		    {
			    cpuid = mo["ProcessorID"].ToString();
		    }
		    return cpuid;
	    }
	    catch (Exception) 
        { 
            return cpuid; 
        }
       }


       }//end of class

    }// end of namespace

