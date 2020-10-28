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

namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {


        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbCommand com;
        private OleDbDataReader dr;

        public UserRegistration()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    string id = "22";//Queries.xml ID

                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdoc.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;
                    query = query + "(" + "'" + tfUser.Text + "'" + "," + "'" + tfPassword.Password + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + "0" + "'" + ");";

                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();

                    tblUserReg.Text = " Korisničko ime novog korisnika je : " + tfUser.Text;
                    tblPassReg.Text = " Šifra novog korisnika je : " + tfPassword.Password;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();

            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
    }
}
