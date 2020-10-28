using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Xml.Linq;

namespace caffeKambodzaApplication
{
    public class Logger
    {
        public static OleDbConnection conLogger = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_LOGGER);
        public static OleDbConnection conLoggerNumber = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_LOGGERNUMBER);
        private static OleDbCommand com;
        private static OleDbDataReader dr;

        public static int LogNodeNumber;

        public static void loadNodeNumber() 
        {
            try
            {
               
                string id = "47";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;

                conLoggerNumber.Open();
                com = new OleDbCommand(query, conLoggerNumber);
                dr = com.ExecuteReader();




                while (dr.Read())
                { 
                    bool isNum = int.TryParse(dr["LogNumber"].ToString(), out LogNodeNumber);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
            }
            finally 
            {
                if (conLoggerNumber != null)
                {
                    conLoggerNumber.Close();
                }

                if (dr != null) 
                {
                    dr.Close();
                }
            }

        }

        public static void writeNode(string status, string text) 
        {
            LogNodeNumber++;
            try
            {
               

                string id = "48";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;


                conLogger.Open();
                com = new OleDbCommand(query, conLogger);
                com.Parameters.AddWithValue("@NodeNumber", LogNodeNumber);
                com.Parameters.AddWithValue("@StoreItemName", status);
                com.Parameters.AddWithValue("@Type", text);
                com.Parameters.AddWithValue("@DateTimeWrite", DateTime.Now.ToString());


                com.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
            }
            finally
            {
                if (conLogger != null)
                {
                    conLogger.Close();
                }
            }
 
        }

    }
}
