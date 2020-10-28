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
using System.Data.OleDb;
using System.Xml.Linq;

namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for UpdatePassword.xaml
    /// </summary>
    public partial class UpdatePassword : Window
    {


        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbCommand com;
        private OleDbDataReader dr;
        private OleDbDataReader dr2;
        private bool exist = false;

        public UpdatePassword()
        {
            InitializeComponent();
            exist = false;
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
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
                        exist = true;
                        break;
                    }
                }

                if (exist)
                {
                    //update database, table users
                    query = "UPDATE users SET UserPassword = " + "'" + tfPasswordNew.Password + "'" + " WHERE UserName =" + "'" + tfUser.Text + "'" + ";";
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();


                    string queryStorehouse = "UPDATE users SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE UserName =" + "'" + tfUser.Text + "'" + ";";
                    com = new OleDbCommand(queryStorehouse, con);
                    com.ExecuteNonQuery();

                    query = "SELECT NumberOfUpdates FROM users WHERE UserName = " + "'" + tfUser.Text + "'" + ";";
                    com = new OleDbCommand(query, con);
                    dr2 = com.ExecuteReader();
                    int oldUpNum = 0;
                    while (dr2.Read())
                    {
                        bool isNum = int.TryParse(dr2["NumberOfUpdates"].ToString(), out oldUpNum);
                    }


                    int upNum = oldUpNum + 1;
                    queryStorehouse = "UPDATE users SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE UserName =" + "'" + tfUser.Text + "'" + ";";
                    com = new OleDbCommand(queryStorehouse, con);
                    com.ExecuteNonQuery();




                    tblInformation.Text = " Vaša nova šifra je : " + tfPasswordNew.Password;
                }
                else 
                {
                    MessageBox.Show(" Ne postoji korisnik u sistemu sa ovim korisničkim imenom!", "KORISNIK " + tfUser.Text + " NE POSTOJI");
                    Logger.writeNode(Constants.MESSAGEBOX, " Ne postoji korisnik u sistemu sa ovim korisničkim imenom!");
                    MessageBox.Show(" Ili ste zaboravili vašu šifru!");
                    Logger.writeNode(Constants.MESSAGEBOX, " Ili ste zaboravili vašu šifru!");
                  
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
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
                if (dr2 != null)
                {
                    dr2.Close();
                }
            }
        }
    }
}
