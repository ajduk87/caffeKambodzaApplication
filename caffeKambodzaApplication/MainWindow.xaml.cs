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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using System.Globalization;
using System.IO;
using System.Data;
using Nemiro.OAuth;
using Nemiro.OAuth.LoginForms;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

        public SmtpClient client = new SmtpClient();
        public MailMessage msg = new MailMessage();
        public System.Net.NetworkCredential smtpCreds;


        private DropboxForm dropboxForm;

        public bool reportNotYetCreated = false;//ako je ovo true u problemu si


        public string oldprice = String.Empty; 
        public System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Vivaldi_Winter.wav");
        private DateTime dateChangePrice;

       


        //columns for bar book writing
        //this is for add only one item fill
        private ObservableCollection<string> ProductName = new ObservableCollection<string>();
        private ObservableCollection<string> WayOfDisplay = new ObservableCollection<string>();
        private ObservableCollection<string> SoldProductNumber = new ObservableCollection<string>();
        private ObservableCollection<string> PricesOfSoldProductNumber = new ObservableCollection<string>();

        //this is for create report button
        private ObservableCollection<string> YesterdaySupplies = new ObservableCollection<string>();
        private ObservableCollection<string> TodayStateOfStorehouse = new ObservableCollection<string>();
        private ObservableCollection<string> ordinalNumbers = new ObservableCollection<string>();

        public bool IsEnteredMoreBuyedStoreItems = false;
    

        private int _numOfEverCreatedItem;
        private bool _firstpass = false;
        private string _lastProduct = String.Empty;
        private string _lastAmount = String.Empty;//da bi ispisivala REMARKNUMERIC2 napomenu i leave dogadjaju dugmeta dodaj stavku se ispituje da li je bio u pitanju broj ili ne
        private bool _entered = false;
        private string _currency = Constants.CURRENCYDINAR;
        private int _total = 0;
        private string _currDate = String.Empty;
        private DateTime _dateCreatedReport;
        private DateTime _dateOfLastCreatedBarBook;
        public DateTime DateOfLastCreatedBarBook
        {
            get { return _dateOfLastCreatedBarBook; }
            set { _dateOfLastCreatedBarBook = value; }
        }
        private bool _isCodeProductWrite;
      

        public bool isCodeProductWrite
        {
            set { _isCodeProductWrite = value; }
        }

        public string Currency 
        {
            get { return _currency; }
        }

        private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory +  Constants.DATABASECONNECTION_APP);
        private OleDbConnection conHelp = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbConnection conOptions = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory +  Constants.DATABASECONNECTION_APP);
        private OleDbConnection conConnProdStore = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbConnection conCancelItem = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbConnection conMeasure = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbConnection conHistory = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_HISTORY);
        private OleDbConnection conLoggerNumber = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_LOGGERNUMBER);
        public  OleDbConnection conLogger = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_LOGGER);
        private OleDbCommand com;
        private OleDbCommand comOptions;
        private OleDbDataReader dr;
        private OleDbDataReader drReal;
        private OleDbDataReader drOptions;
        private OleDbDataReader drConn;
        private OleDbDataReader drStore;
        private OleDbDataReader drSum;
        private OleDbDataReader drInner;


        private int numberOfEnteredProduct = 0;
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> ProductsWholeInformation
        {
            get { return _products; }
            set { _products = value; }
        }
        private ObservableCollection<ProductWithOrderNumber> _productsWithOrder = new ObservableCollection<ProductWithOrderNumber>();
        public ObservableCollection<ProductWithOrderNumber> ProductsWithOrder
        {
            get { return _productsWithOrder; }
            set { _productsWithOrder = value; }
        }



        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        public ObservableCollection<string> Products = new ObservableCollection<string>();
        public ObservableCollection<string> ProductsWithOrderNames = new ObservableCollection<string>();
        public ObservableCollection<string> Codes = new ObservableCollection<string>();
        public ObservableCollection<string> Measures = new ObservableCollection<string>();



        private void LoadDateOfLastCreatedBarBook() 
        {
            try
            {

                conOptions.Open();
                string query = "SELECT * FROM savedOptions WHERE Options = 'options';";
                comOptions = new OleDbCommand(query, conOptions);
                drOptions = comOptions.ExecuteReader();
                string dateOfLastCreatedBarBook = String.Empty;
               


                if (drOptions.Read())
                {
                    dateOfLastCreatedBarBook = drOptions["DateOfLastCreatedBarBook"].ToString();
                    if (dateOfLastCreatedBarBook.Equals("")) dateOfLastCreatedBarBook = String.Empty;
                }

                if (dateOfLastCreatedBarBook.Equals(String.Empty) == false)
                {
                    _dateOfLastCreatedBarBook = DateTime.Parse(dateOfLastCreatedBarBook);
                }

                MessageBox.Show("Zadnja kreirana knjiga šanka je za datum :" + _dateOfLastCreatedBarBook.ToShortDateString());

                //set enter data in storehouse enter tab3 podtab1
                storehouse.datepicker1.SelectedDate = _dateOfLastCreatedBarBook.AddDays(1);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                if (conOptions != null)
                {
                    conOptions.Close();
                }
                if (drOptions != null)
                {
                    drOptions.Close();
                }
            }
        }
       


        private void testItems()
        {
            for (int i = 0; i < 100; i++)
            {
                Item item = new Item("200", "Lav Pivo",110,i,i*110);
                _items.Add(item);  
            }
        }

        /// <summary>
        /// calculate 26^n
        /// </summary>
        /// <param name="n">n is arbitrary finite number</param>
        /// <returns>returns 26^n</returns>
        private int pow(int n)
        {
            int res = 1;
            for (int i = 0; i < n; i++)
            {
                res = res * 26;
            }
            return res;
        }

        public int excelNumbers(string letters)
        {
            char[] array = letters.ToCharArray();
            Array.Reverse(array);

            int number = 0;
            char figure;
            try
            {
                for (int i = 0; i < array.Length; i++)
                {
                    figure = array[i];

                    switch (figure)
                    {
                        case 'a':
                        case 'A':
                            {
                                if (i == 0) number = number + 1;
                                else number = number + 1 * pow(i);
                                break;
                            }
                        case 'b':
                        case 'B':
                            {
                                if (i == 0) number = number + 2;
                                else number = number + 2 * pow(i);
                                break;
                            }
                        case 'c':
                        case 'C':
                            {
                                if (i == 0) number = number + 3;
                                else number = number + 3 * pow(i);
                                break;
                            }
                        case 'd':
                        case 'D':
                            {
                                if (i == 0) number = number + 4;
                                else number = number + 4 * pow(i);
                                break;
                            }
                        case 'e':
                        case 'E':
                            {
                                if (i == 0) number = number + 5;
                                else number = number + 5 * pow(i);
                                break;
                            }
                        case 'f':
                        case 'F':
                            {
                                if (i == 0) number = number + 6;
                                else number = number + 6 * pow(i);
                                break;
                            }
                        case 'g':
                        case 'G':
                            {
                                if (i == 0) number = number + 7;
                                else number = number + 7 * pow(i);
                                break;
                            }
                        case 'h':
                        case 'H':
                            {
                                if (i == 0) number = number + 8;
                                else number = number + 8 * pow(i);
                                break;
                            }
                        case 'i':
                        case 'I':
                            {
                                if (i == 0) number = number + 9;
                                else number = number + 9 * pow(i);
                                break;
                            }
                        case 'j':
                        case 'J':
                            {
                                if (i == 0) number = number + 10;
                                else number = number + 10 * pow(i);
                                break;
                            }
                        case 'k':
                        case 'K':
                            {
                                if (i == 0) number = number + 11;
                                else number = number + 11 * pow(i);
                                break;
                            }
                        case 'l':
                        case 'L':
                            {
                                if (i == 0) number = number + 12;
                                else number = number + 12 * pow(i);
                                break;
                            }
                        case 'm':
                        case 'M':
                            {
                                if (i == 0) number = number + 13;
                                else number = number + 13 * pow(i);
                                break;
                            }
                        case 'n':
                        case 'N':
                            {
                                if (i == 0) number = number + 14;
                                else number = number + 14 * pow(i);
                                break;
                            }
                        case 'o':
                        case 'O':
                            {
                                if (i == 0) number = number + 15;
                                else number = number + 15 * pow(i);
                                break;
                            }
                        case 'p':
                        case 'P':
                            {
                                if (i == 0) number = number + 16;
                                else number = number + 16 * pow(i);
                                break;
                            }
                        case 'q':
                        case 'Q':
                            {
                                if (i == 0) number = number + 17;
                                else number = number + 17 * pow(i);
                                break;
                            }
                        case 'r':
                        case 'R':
                            {
                                if (i == 0) number = number + 18;
                                else number = number + 18 * pow(i);
                                break;
                            }
                        case 's':
                        case 'S':
                            {
                                if (i == 0) number = number + 19;
                                else number = number + 19 * pow(i);
                                break;
                            }
                        case 't':
                        case 'T':
                            {
                                if (i == 0) number = number + 20;
                                else number = number + 20 * pow(i);
                                break;
                            }
                        case 'u':
                        case 'U':
                            {
                                if (i == 0) number = number + 21;
                                else number = number + 21 * pow(i);
                                break;
                            }
                        case 'v':
                        case 'V':
                            {
                                if (i == 0) number = number + 22;
                                else number = number + 22 * pow(i);
                                break;
                            }
                        case 'w':
                        case 'W':
                            {
                                if (i == 0) number = number + 23;
                                else number = number + 23 * pow(i);
                                break;
                            }
                        case 'x':
                        case 'X':
                            {
                                if (i == 0) number = number + 24;
                                else number = number + 24 * pow(i);
                                break;
                            }
                        case 'y':
                        case 'Y':
                            {
                                if (i == 0) number = number + 25;
                                else number = number + 25 * pow(i);
                                break;
                            }
                        case 'z':
                        case 'Z':
                            {
                                if (i == 0) number = number + 26;
                                else number = number + 26 * pow(i);
                                break;
                            }
                        default: throw new Exception(i + ". figure is not a letter!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message); 
                savenumofitemsEVERCreated();
            }


            return number;

        }



        private void insertRecordIntoproductsWithOrderNumber(ProductWithOrderNumber productWithOrderNumber)
        {
            try
            {
                con.Open();
                
                string id = "59";//Queries.xml ID
                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                //string query = Query.Attribute(Constants.TEXT).Value + "(" + "'" + product.CodeProduct + "'" + "," + "'" + product.KindOfProduct + "'" + "," + "'" + product.NameProduct + "'" + "," + "'" + product.MeasureProduct + "'" + "," + "'" + product.Price.ToString() + "'" + "," + "'" + _currency + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + "0" + "'" + "," + "'" + product.WayDisplayBookBar + "'" + ");";
                string query = Query.Attribute(Constants.TEXT).Value;
                              
                com = new OleDbCommand(query, con);
                com.Parameters.Add("@CodeProduct", productWithOrderNumber.CodeProduct);
                com.Parameters.Add("@KindOfProduct", productWithOrderNumber.KindOfProduct);
                com.Parameters.Add("@NameProduct", productWithOrderNumber.NameProduct);
                com.Parameters.Add("@MeasureProduct", productWithOrderNumber.MeasureProduct);
                com.Parameters.Add("@Price", productWithOrderNumber.Price);
                com.Parameters.Add("@Valuta", Currency);
                com.Parameters.Add("@CreatedDateTime", DateTime.Now.ToString());
                com.Parameters.Add("@LastDateTimeUpdated", DateTime.Now.ToString());
                com.Parameters.Add("@NumberOfUpdates", "0");
                com.Parameters.Add("@WayDisplayBookBar", productWithOrderNumber.WayDisplayBookBar);
                com.Parameters.Add("@NumberOrder", productWithOrderNumber.OrderNumber.ToString());
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        private void insertProductInDatabase(Product product)
        {
            try
            {
                con.Open();
                //string query = "INSERT INTO products(CodeProduct,KindOfProduct,NameProduct,MeasureProduct,Price,Valuta) VALUES(" + "'" + product.CodeProduct + "'" + "," + "'" + product.KindOfProduct + "'"  + "," + "'" + product.NameProduct + "'" + "," + "'" + product.MeasureProduct + "'" + "," + "'" + product.Price.ToString() + "'" + "," + "'" + _currency + "'" + ");";
                string id = "3";//Queries.xml ID
                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value + "(" + "'" + product.CodeProduct + "'" + "," + "'" + product.KindOfProduct + "'" + "," + "'" + product.NameProduct + "'" + "," + "'" + product.MeasureProduct + "'" + "," + "'" + product.Price.ToString() + "'" + "," + "'" + _currency + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + "0" + "'" + "," + "'" + product.WayDisplayBookBar + "'" + ");";

                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                con.Close();
            }
        }
        
        private void removeProductFromDatabase(Product product)
        {
            try
            {
                con.Open();
                string id = "5";//Queries.xml ID
                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value + "'" + product.KindOfProduct + "'" + ";";
                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                con.Close();
            }
        }

        private ObservableCollection<StoreItemProduct> getStoreItemsForOneProduct(Product p)
        {
            try
            {
                ObservableCollection<StoreItemProduct> res = new ObservableCollection<StoreItemProduct>();
                string id = "11";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + " WHERE ConnCodeProduct = " + "'" + p.CodeProduct + "'" + " ;";

                conConnProdStore.Open();
                com = new OleDbCommand(query, conConnProdStore);
                drConn = com.ExecuteReader();
                string codeProduct = String.Empty;
                string codeStoreItem = String.Empty;
                string kindOfProduct = String.Empty;
                string storeItemName = String.Empty;
                string storeItemMeasure = String.Empty;
                int priceStoreItem = -1;
                string storeGroup = String.Empty;
                string isUsed = String.Empty;
                bool isUsedBool;
                double amount = 0.0;


                while (drConn.Read())
                {
                    //codeProduct = drConn["ConnCodeProduct"].ToString();
                    codeStoreItem = drConn["ConnStoreItemCode"].ToString();
                    //kindOfProduct = drConn["ConnKindOfProduct"].ToString();
                    storeItemName = drConn["ConnStoreItemName"].ToString();
                    storeItemMeasure = drConn["ConnStoreItemMeasure"].ToString();
                    storeGroup = drConn["GroupStoreItem"].ToString();
                    isUsed = drConn["isUsed"].ToString();
                    if (isUsed.Equals(Constants.YES)) isUsedBool = true;
                    else isUsedBool = false;

                    int n;
                    bool isNumeric = int.TryParse(drConn["Price"].ToString(), out n);
                    if (isNumeric) { priceStoreItem = Convert.ToInt32(drConn["Price"].ToString()); }

                    string amountStoreItemWithPoint = drConn["AmountStoreItem"].ToString().Replace(',', '.');
                    bool isNum = Double.TryParse(amountStoreItemWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amount);

                    StoreItemProduct storeProduct = new StoreItemProduct(codeStoreItem, storeItemName,storeItemMeasure, priceStoreItem, storeGroup, isUsedBool,amount);

                    //add real amount of store item
                    string idStore = "15";//Queries.xml ID
                    XDocument xdocStore2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement QueryStore = (from xml2 in xdocStore2.Descendants("Query")
                                           where xml2.Element("ID").Value == idStore 
                                           select xml2).FirstOrDefault();
                   
                    string queryStore = QueryStore.Attribute(Constants.TEXT).Value;
                    queryStore = queryStore + "'" + storeProduct.CodeProduct + "'" +";";
                    com = new OleDbCommand(queryStore, conConnProdStore);
                    drReal = com.ExecuteReader();
                    while (drReal.Read())
                    {
                        double d;
                        string realAmountWithPoint = drReal["RealAmount"].ToString().Replace(',', '.');
                        bool isN = Double.TryParse(realAmountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
                        storeProduct.RealAmount = d;
                    }

                    res.Add(storeProduct);
                }

                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
                return new ObservableCollection<StoreItemProduct>();
            }
            finally
            {
                if (conConnProdStore != null)
                {
                    conConnProdStore.Close();
                }
                if (drConn != null)
                {
                    drConn.Close();
                }
                if (drReal != null)
                {
                    drReal.Close();
                }
            }
        }

        private void LoadProductsTab2() 
        {
            try
            {
                con.Open();
                
                string id = "4";//Queries.xml ID
                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                dr = com.ExecuteReader();
                string codeProduct = String.Empty;
                string kindOfProduct = String.Empty;
                string nameProduct = String.Empty;
                string measureProduct = String.Empty;
                string wayofDisplay = String.Empty;
               
                int price = -1;

                Products.Add(Constants.CHOOSEPRODUCT);
                Codes.Add(Constants.CHOOSECODE);
                while (dr.Read())
                {
                    codeProduct = dr["CodeProduct"].ToString();
                    nameProduct = dr["NameProduct"].ToString();
                    wayofDisplay = dr["WayDisplayBookBar"].ToString();
                    measureProduct = dr["MeasureProduct"].ToString();
                  
                   
                    kindOfProduct = nameProduct + " " + measureProduct;
                    int n;
                    bool isNumeric = int.TryParse(dr["Price"].ToString(), out n);
                    if (isNumeric) { price = Convert.ToInt32(dr["Price"].ToString()); }

                    Product product = new Product(codeProduct, kindOfProduct, nameProduct, measureProduct, price);
                    product.WayDisplayBookBar = wayofDisplay;
                  
                    
                    product.StoreItemProducts = getStoreItemsForOneProduct(product);
                    _products.Add(product);
                    Products.Add(product.ComboBoxForm());
                    Codes.Add(product.Code());
                    
                }

                string id2 = "2";//Queries.xml ID
                XDocument xdoc2 = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query2 = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id2
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query2.ToString());
                string query2 = Query2.Attribute(Constants.TEXT).Value;
                string measure = String.Empty;
                com = new OleDbCommand(query2, con);
                dr = com.ExecuteReader();

                Measures.Add(Constants.CHOOSEMEASURE);

                while (dr.Read())
                {
                    measure = dr["MeasureName"].ToString();
                    Measures.Add(measure);
                }


                //load products wit order number
                id = "60";//Queries.xml ID
                xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                dr = com.ExecuteReader();
                codeProduct = String.Empty;
                kindOfProduct = String.Empty;
                nameProduct = String.Empty;
                measureProduct = String.Empty;
                wayofDisplay = String.Empty;

                price = -1;

                ProductsWithOrderNames.Add(Constants.CHOOSEPRODUCT);

                string numOrd = String.Empty;
                int numberOrder = -1;
               
                while (dr.Read())
                {
                    codeProduct = dr["CodeProduct"].ToString();
                    nameProduct = dr["NameProduct"].ToString();
                    wayofDisplay = dr["WayDisplayBookBar"].ToString();
                    measureProduct = dr["MeasureProduct"].ToString();


                    kindOfProduct = nameProduct + " " + measureProduct;
                    int n;
                    bool isNumeric = int.TryParse(dr["Price"].ToString(), out n);
                    if (isNumeric) { price = Convert.ToInt32(dr["Price"].ToString()); }

                    Product product = new Product(codeProduct, kindOfProduct, nameProduct, measureProduct, price);
                    product.WayDisplayBookBar = wayofDisplay;


                    product.StoreItemProducts = getStoreItemsForOneProduct(product);

                    //_products.Add(product);
                    //Products.Add(product.ComboBoxForm());
                    //Codes.Add(product.Code());
                    numOrd = dr["NumberOrder"].ToString();
                    bool isNN = int.TryParse(numOrd, out numberOrder);
                    ProductWithOrderNumber pOrder = new ProductWithOrderNumber(product,numberOrder);
                    _productsWithOrder.Add(pOrder);
                    ProductsWithOrderNames.Add(pOrder.KindOfProduct);
                }

              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        private void LoadProductsTab1()
        {
            cmbNameProductTab1.ItemsSource = ProductsWithOrderNames;
            cmbNameProductTab1.SelectedIndex = 0;
        }

        private void ConnectItemsWithDataGrid()
        {
            dataGrid1.ItemsSource = _items;
            autoscrollToBottom(dataGrid1);
            tblItemValueNumber.Text = "0" + " " + _currency;
            tblTotalValue.Text = _total.ToString() + " " + _currency;
        }

        private void LoadReportOptionsPath()
        {
            try
            {
  
                conOptions.Open();
                string query = "SELECT * FROM savedOptions WHERE Options = 'options';";
                comOptions = new OleDbCommand(query, conOptions);
                drOptions = comOptions.ExecuteReader();
                string directorium = String.Empty;
                string name = String.Empty;
                string extension = String.Empty;
                string databasepath = String.Empty;


                while (drOptions.Read())
                {
                    directorium = drOptions["Directorium"].ToString();
                    name = drOptions["NameCreatedReport"].ToString();
                    extension = drOptions["ExtensionOfCreatedReport"].ToString();
                    databasepath = drOptions["DatabasePath"].ToString();
                }

                if (directorium.Equals(Constants.DEFAULTDIRECTORIUM) == false)
                {
                    this.options.tblDir2.Text = directorium;
                    this.options.tblDir2.Foreground = System.Windows.Media.Brushes.DarkOliveGreen;
                    this.options.btnSaveDir2.IsEnabled = true;
                }
                else
                {
                    this.options.tblDir2.Text = Constants.DEFAULTOPTION;
                    this.options.tblDir2.Foreground = System.Windows.Media.Brushes.Azure;
                }

                if (name.Equals(Constants.DEFAULTNAMEOFCREATEDREPORT) == false)
                {
                    this.options.tblFile2.Text = name;
                    this.options.tblFile2.Foreground = System.Windows.Media.Brushes.DarkOliveGreen;
                    this.options.btnSaveFile2.IsEnabled = true;
                }
                else
                {
                    this.options.tblFile2.Text = Constants.DEFAULTOPTION;
                    this.options.tblFile2.Foreground = System.Windows.Media.Brushes.Azure;
                }

                if (extension.Equals(Constants.DEFAULTEXTENSIONOFCREATEDREPORT) == false)
                {
                    this.options.tblExtension2.Text = extension;
                    this.options.tblExtension2.Foreground = System.Windows.Media.Brushes.DarkOliveGreen;
                    this.options.btnSaveExtension2.IsEnabled = true;
                }
                else
                {
                    this.options.tblExtension2.Text = Constants.DEFAULTOPTION;
                    this.options.tblExtension2.Foreground = System.Windows.Media.Brushes.Azure;
                }

                if (databasepath.Equals(Constants.DEFAULTDATABASEPATH) == false)
                {
                    this.options.tblDatabasePath2.Text = databasepath;
                    this.options.tblDatabasePath2.Foreground = System.Windows.Media.Brushes.DarkOliveGreen;
                    this.options.btnSaveDatabasePath2.IsEnabled = true;
                }
                else
                {
                    this.options.tblDatabasePath2.Text = Constants.DEFAULTOPTION;
                    this.options.tblDatabasePath2.Foreground = System.Windows.Media.Brushes.Azure;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                if (conOptions != null)
                {
                    conOptions.Close();
                }
                if (drOptions != null)
                {
                    drOptions.Close();
                }
            }
        }


        private void LoadReportOptionsApplication()
        {
            //not implemented yet
            try
            {

                conOptions.Open();
                string query = "SELECT * FROM optionsOfApplication WHERE Options = 'application';";
                comOptions = new OleDbCommand(query, conOptions);
                drOptions = comOptions.ExecuteReader();
                string isNameOfCompanyChecked = String.Empty;
                string nameOfCompany = String.Empty;
                string isAuthor = String.Empty;
                string author = String.Empty;
                string soundOn = String.Empty;
                string openCreatedFile = String.Empty;
                string isLandscape = String.Empty;
                string isMaskChecked = String.Empty;
                string isCodeProductChecked = String.Empty;

                string isPathStorehouseChecked = String.Empty;
                string pathStorehouseState = String.Empty;


                while (drOptions.Read())
                {
                    isNameOfCompanyChecked = drOptions["IsNameOfCompanyChecked"].ToString();
                    nameOfCompany = drOptions["NameOfCompany"].ToString();
                    isAuthor = drOptions["IsAuthorChecked"].ToString();
                    author = drOptions["Author"].ToString();
                    soundOn = drOptions["CodeProductCheck"].ToString();
                    openCreatedFile = drOptions["OpenAfterCreating"].ToString();
                    isLandscape = drOptions["IsLandscape"].ToString();
                    isMaskChecked = drOptions["IsMaskChecked"].ToString();
                    isCodeProductChecked = drOptions["IsCodeProductWrite"].ToString();
                    isPathStorehouseChecked = drOptions["IsPathStorehouseChecked"].ToString();
                    pathStorehouseState = drOptions["PathStateStorehouse"].ToString();
                }

                if (isNameOfCompanyChecked.Equals(Constants.YES) == true)
                {
                    this.options.cmbAppCompany.IsChecked = true;
                }
                else 
                {
                    this.options.cmbAppCompany.IsChecked = false;
                }

                if (isAuthor.Equals(Constants.YES) == true)
                {
                    this.options.cmbAppAuthor.IsChecked = true;
                }
                else 
                {
                    this.options.cmbAppAuthor.IsChecked = false;
                }

                this.options.tblPathStateStore2.Text = pathStorehouseState;

                if (isPathStorehouseChecked.Equals(Constants.YES) == true)
                {
                    this.options.cmbAppStateStorehouse.IsChecked = true;
                }
                else
                {
                    this.options.cmbAppStateStorehouse.IsChecked = false;
                }

                if (nameOfCompany.Equals(Constants.DEFAULTNAMEOFCOMPANY) == false)
                {
                    this.options.tblCompany2.Text = nameOfCompany;
                    this.options.tblCompany2.Foreground = System.Windows.Media.Brushes.DarkOliveGreen;
                    this.options.btnreturnCompany.IsEnabled = true;
                }
                else
                {
                    this.options.tblCompany2.Text = Constants.DEFAULTOPTION;
                    this.options.tblCompany2.Foreground = System.Windows.Media.Brushes.Azure;
                    this.options.btnreturnCompany.IsEnabled = false;
                }

                if (author.Equals(Constants.DEFAULTAUTHOR) == false)
                {
                    this.options.tblAuthor2.Text = author;
                    this.options.tblAuthor2.Foreground = System.Windows.Media.Brushes.DarkOliveGreen;
                    this.options.btnReturnAuthor.IsEnabled = true;
                }
                else
                {
                    this.options.tblAuthor2.Text = Constants.DEFAULTOPTION;
                    this.options.tblAuthor2.Foreground = System.Windows.Media.Brushes.Azure;
                    this.options.btnReturnAuthor.IsEnabled = false;
                }

                if (soundOn.Equals(Constants.YES) == true)
                {
                    options.tblSound.Text = Constants.SOUNDON;
                    options.cmbAppSound.IsChecked = true;
                }
                else
                {
                    options.tblSound.Text = Constants.SOUNDOFF;
                    options.cmbAppSound.IsChecked = false;
                }

                if (openCreatedFile.Equals(Constants.YES))
                {
                    options.tblOpenWhenCreated.Text = Constants.OPENFILE;
                    options.cmbAppOpen.IsChecked = true;
                }
                else 
                {
                    options.tblOpenWhenCreated.Text = Constants.NOTOPENFILE;
                    options.cmbAppOpen.IsChecked = false;
                }

                if (isLandscape.Equals(Constants.YES) == true)
                {
                    options.rbtnLandscape.IsChecked = true;
                }
                else
                {
                    options.rbtnPortrait.IsChecked = true;
                }

                if (isMaskChecked.Equals(Constants.YES) == true)
                {
                    options.chkbMask.IsChecked = true;
                }
                else
                {
                    options.chkbMask.IsChecked = false;
                    initialMaskUnchecked();
                }

                if (isCodeProductChecked.Equals(Constants.YES) == true)
                {
                    options.chbtnWriteCode.IsChecked = true;
                    _isCodeProductWrite = true;
                }
                else 
                {
                    options.chbtnWriteCode.IsChecked = false;
                    _isCodeProductWrite = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                conOptions.Close();
                drOptions.Close();
            }
        }

       

      
        private void initialMaskUnchecked()
        {
           
            this.gridTab1.Background = System.Windows.Media.Brushes.White;
            this.dataGrid1.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            this.tfAmount.Background = System.Windows.Media.Brushes.LightGreen;
            this.tfAmount.Foreground = System.Windows.Media.Brushes.Black;
            this.gridTab2.Background = System.Windows.Media.Brushes.White;

            options.gridRoot.Background = System.Windows.Media.Brushes.White;
            options.gridRootApp.Background = System.Windows.Media.Brushes.White;

            //set blue color for path options
            options.tblInitialDir.Foreground = System.Windows.Media.Brushes.Blue;
            options.tblInitialFile.Foreground = System.Windows.Media.Brushes.Blue;
            options.tblInitialExtension.Foreground = System.Windows.Media.Brushes.Blue;
            options.tblInitialDatabasePath.Foreground = System.Windows.Media.Brushes.Blue;

            if (options.tblDir2.Text.Equals(Constants.DEFAULTOPTION))
            {
                options.tblDir2.Foreground = System.Windows.Media.Brushes.Blue;
            }
            if (options.tblFile2.Text.Equals(Constants.DEFAULTOPTION))
            {
                options.tblFile2.Foreground = System.Windows.Media.Brushes.Blue;
            }
            if (options.tblExtension2.Text.Equals(Constants.DEFAULTOPTION))
            {
                options.tblExtension2.Foreground = System.Windows.Media.Brushes.Blue;
            }
            if (options.tblDatabasePath2.Text.Equals(Constants.DEFAULTOPTION))
            {
                options.tblDatabasePath2.Foreground = System.Windows.Media.Brushes.Blue;
            }

            

            //set blue color for application options
            options.tblSound.Foreground = System.Windows.Media.Brushes.Blue;
            options.tblOpenWhenCreated.Foreground = System.Windows.Media.Brushes.Blue;
            options.tblInitialCompany.Foreground = System.Windows.Media.Brushes.Blue;
            options.tblInitialAuthor.Foreground = System.Windows.Media.Brushes.Blue;

            if (options.tblCompany2.Text.Equals(Constants.DEFAULTOPTION))
            {
                options.tblCompany2.Foreground = System.Windows.Media.Brushes.Blue;
            }

            if (options.tblAuthor2.Text.Equals(Constants.DEFAULTOPTION))
            {
                options.tblAuthor2.Foreground = System.Windows.Media.Brushes.Blue;
            }


            //schedule report options
            options.gridScheduleRoot.Background = System.Windows.Media.Brushes.White;
            options.dataGridSchedule.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;

            //storehouse tab5
            storehouse.gridTab5.Background = System.Windows.Media.Brushes.White;
            storehouse.tblFilterStatusTab5.Background = System.Windows.Media.Brushes.White;
            storehouse.dataGridReadStateStorehouse.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //history tab [tab1]
            history.gridHistoryTab1.Background = System.Windows.Media.Brushes.White;
            history.tblFilterStatusTab1.Background = System.Windows.Media.Brushes.White;
            history.dataGridReadHistoryRecipes.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //history tab [tab2]
            history.gridHistoryTab2.Background = System.Windows.Media.Brushes.White;
            history.tblFilterStatusTab2.Background = System.Windows.Media.Brushes.White;
            history.dataGridReadHistoryPrices.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //overviewStorehouse tab [tab1]
            overviewStorehouse.gridHistoryTab1.Background = System.Windows.Media.Brushes.White;
            overviewStorehouse.tblFilterStatusTab1.Background = System.Windows.Media.Brushes.White;
            overviewStorehouse.dataGridReadStore.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //overviewStorehouse tab [tab2]
            overviewStorehouse.gridHistoryTab2.Background = System.Windows.Media.Brushes.White;
            overviewStorehouse.tblFilterStatusTab2.Background = System.Windows.Media.Brushes.White;
            overviewStorehouse.dataGridReadStoreTab2.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //overviewStorehouse tab [tab3]
            overviewStorehouse.gridHistoryTab3.Background = System.Windows.Media.Brushes.White;
            overviewStorehouse.tblFilterStatusTab3.Background = System.Windows.Media.Brushes.White;
            overviewStorehouse.dataGridReadStoreTab3.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //createdReports [tab1]
            createdReports.gridHistoryTab1.Background = System.Windows.Media.Brushes.White;
            createdReports.dataGridRead.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //createdReports [tab2]
            createdReports.gridHistoryTab2.Background = System.Windows.Media.Brushes.White;
            createdReports.dataGridReadByProduct.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //createdReports [tab3]
            createdReports.gridHistoryTab3.Background = System.Windows.Media.Brushes.White;
            createdReports.dataGridReadDeletion.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //createdReports [tab4]
            createdReports.gridHistoryTab4.Background = System.Windows.Media.Brushes.White;
            createdReports.dataGridReadCorrection.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //storehouse [tab1]
            storehouse.gridTab1.Background = System.Windows.Media.Brushes.White;
            //storehouse [tab2]
            storehouse.gridTab2.Background = System.Windows.Media.Brushes.White;
            //storehouse [tab3]
            storehouse.gridTab3.Background = System.Windows.Media.Brushes.White;
            //storehouse [tab4]
            storehouse.gridTab4.Background = System.Windows.Media.Brushes.White;
            //selectUpdateConnProdStore
            selectUpdateConnProdStore.gridAllFilterData.Background = System.Windows.Media.Brushes.White;
            selectUpdateConnProdStore.gridtfsPart.Background = System.Windows.Media.Brushes.White;
            //enterStoreItemsTab2
            enterStoreItemsTab2.gridRoot.Background = System.Windows.Media.Brushes.White; 

        }


        private void InitialData()
        {

            cmbNameProductr.ItemsSource = Products;
            cmbNameProductr.SelectedIndex = 0;
            cmbWayDisplayBookBar.SelectedIndex = 0;
            enterStoreItemsTab2.cmbChooseEarlierProduct.ItemsSource = Products;
            enterStoreItemsTab2.cmbChooseEarlierProduct.SelectedIndex = 0;
            createdReports.cmbProductsTab2.ItemsSource = Products;
            createdReports.cmbProductsTab2.SelectedIndex = 0;
            storehouse.cmbItemStore.ItemsSource = Products;
            storehouse.cmbItemStore.SelectedIndex = 0;


            cmbNameProductr2.ItemsSource = Codes;
            cmbNameProductr2.SelectedIndex = 0;

            cmbMeasure.SelectedIndex = 0;
            cmbMeasure.ItemsSource = Measures;
            cmbRemoveMeasure.SelectedIndex = 0;
            cmbRemoveMeasure.ItemsSource = Measures;



            btnRemoveMeasure.IsEnabled = false;
            btnNewMeasure.IsEnabled = false;
            btnDeletetfMeasure.IsEnabled = false;




            this.storehouse.cmbSItem.ItemsSource = enterStoreItemsTab2.StoreItems;
            this.storehouse.cmbSItem.SelectedIndex = 0;


            storehouse.cmbSGroup.ItemsSource = enterStoreItemsTab2.GroupsItemsInStore;
            storehouse.cmbSGroup.SelectedIndex = 0;
            storehouse.cmbSItem.IsEnabled = false;

            this.storehouse.cmbThresholdGroups.ItemsSource = enterStoreItemsTab2.GroupsItemsInStore;
            this.storehouse.cmbThresholdGroups.SelectedIndex = 0;
            this.storehouse.cmbThresholdItems.ItemsSource = enterStoreItemsTab2.StoreItems;
            this.storehouse.cmbThresholdItems.SelectedIndex = 0;

            storehouse.ItemsThreshold = enterStoreItemsTab2.StoreItemProducts;
            storehouse.cvItemsThreshold = CollectionViewSource.GetDefaultView(storehouse.ItemsThreshold);
            if (storehouse.cvItemsThreshold != null)
            {
                storehouse.dgridThresholds.ItemsSource = storehouse.cvItemsThreshold;
            }


            
            for (int i = 0; i < this.enterStoreItemsTab2.StoreItemProducts.Count; i++)
            {
                StoreItemProduct sip = new StoreItemProduct();
                sip.CodeProduct = this.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).CodeProduct;
                sip.Amount = this.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Amount;
                sip.Group = this.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Group;
                sip.isUsed = this.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).isUsed;
                sip.KindOfProduct = this.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).KindOfProduct;
                sip.Measure = this.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Measure;
                sip.Price = this.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Price;
                sip.RealAmount = this.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).RealAmount;
                sip.Threshold = this.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Threshold;
                this.storehouse.DailyStoreItem.Add(sip);
                this.storehouse.StoreItemBought.Add(sip);
                this.storehouse.StoreItemBought.Last().RealAmount = 0.0;

            }
            //this.storehouse.DailyStoreItem.RemoveAt(0);
            this.storehouse.dgridDailyEnterInStorehouse.ItemsSource = this.storehouse.DailyStoreItem;


            //schedule in options tab3
            this.options.dataGridSchedule.ItemsSource = _productsWithOrder;

        }

        private void LoadNumOfEverCreatedItem()
        {

            try
            {
                string id = "24";//Queries.xml ID
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
                   // user = dr["UserName"].ToString();
                   // pass = dr["UserPassword"].ToString();
                    bool isNum = int.TryParse(dr["NumberOfItemCreated"].ToString(), out _numOfEverCreatedItem);
                
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
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


        public MainWindow()
        {
            InitializeComponent();
            LoadProductsTab2();
            LoadProductsTab1();
            ConnectItemsWithDataGrid();
            checkRemarkDownTab1();
            //testItems();
            LoadReportOptionsPath();
            LoadReportOptionsApplication();
            InitialData();
            cmbShift.SelectedIndex = 0;
            LoadNumOfEverCreatedItem();
            LoadDateOfLastCreatedBarBook();
            
            tblRemarkTab1.Text = Constants.ENTERDATE_REPORT;

            
            tblRemarkTab1.Text = Constants.ENTERDATE_REPORT;

            //datepicker1

            this.Title = this.Title + " version 1.9";
            this.cmbNameProductTab1.SelectedIndex = 0;
            //Logger
            Logger.loadNodeNumber();
            Logger.writeNode(Constants.INFORMATION,"Konstruktor MainWindow");



            chbNotWorkingDay.IsEnabled = false;
        }
        

        #region newremoveItems_SecondTab 

        private void tfNewMeasure_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfNewMeasure.Text.Length > 0)
            {
                btnNewMeasure.IsEnabled = true;
                btnDeletetfMeasure.IsEnabled = true;
            }
            else 
            {
                btnNewMeasure.IsEnabled = false;
                btnDeletetfMeasure.IsEnabled = false;
            }
        }


        private void tfNewMeasure_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    string newMeasure = String.Empty;
                    newMeasure = tfNewMeasure.Text.Replace(" ",String.Empty);
                    newMeasure = newMeasure.Trim();

                    string id = "1";//Queries.xml ID

                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdoc.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;
                    query = query + "(" + "'"  + newMeasure + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + "0" + "'" + ");";

                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();
                    tfNewMeasure.Foreground = System.Windows.Media.Brushes.Green;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
                
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

       

        private void btnNewMeasure_Click(object sender, RoutedEventArgs e)
        {
            try
            {

               
                    string newMeasure = String.Empty;
                    newMeasure = tfNewMeasure.Text.Replace(" ", String.Empty);
                    newMeasure = newMeasure.Trim();
                   

                   
                   
                    string id = "1";//Queries.xml ID

                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdoc.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;
                    query = query + "(" + "'" + newMeasure + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + "0" + "'" + ");";

                    Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab1 Unos nove merne jedinice u sistem. Nova merna jedinica je: " + newMeasure);
                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();
                    tfNewMeasure.Foreground = System.Windows.Media.Brushes.Green;
                    Measures.Add(newMeasure);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();

            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private void deletemeasureFromDatabase(string measure) 
        {
            try
            {
                conMeasure.Open();
                string id = "12";//Queries.xml ID
                
                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "MeasureName=" + "'" + measure + "'" + ";";

                con.Open();
                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();

            }
            finally
            {
                if (conMeasure != null)
                {
                    conMeasure.Close();
                }
            }
        }

        private void btnRemoveMeasure_Click(object sender, RoutedEventArgs e)
        {
            string removeMeasure = String.Empty;
            if(cmbRemoveMeasure.SelectedIndex == 0)
            {
                MessageBox.Show("Niste izabravli stavku za uklanjanje !", "NESELEKTOVANA STAVKA");
                Logger.writeNode(Constants.MESSAGEBOX, "Niste izabravli stavku za uklanjanje !");
                return;
            }
            else
            {
                removeMeasure = cmbRemoveMeasure.SelectedItem.ToString();
                Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab1 Uklanjanje merne jedinice iz sistema. Merna jedinica koja se uklanja je: " + removeMeasure);
                deletemeasureFromDatabase(removeMeasure);
                for (int i = 0; i < Measures.Count; i++ )
                {
                    if (Measures.ElementAt(i).Equals(removeMeasure))
                    {
                        Measures.RemoveAt(i);

                        break;
                    }
                }
                cmbRemoveMeasure.SelectedIndex = 0;
            }
        }


        private void btnDeletetfMeasure_Click(object sender, RoutedEventArgs e)
        {
            Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab1 Brisanje tekstualnog polja za unos nove merne jedinice. Naziv obrisane merne jedinice je: " + tfNewMeasure.Text);
            tfNewMeasure.Text = String.Empty;
            tfNewMeasure.Foreground = System.Windows.Media.Brushes.Black;
        }


       

        private void Click_btnNewItem(object sender, RoutedEventArgs e)
        {
            if (_entered) { tbkRemark.Text = Constants.REMARKENTERED; return; }

            string codeProduct = String.Empty;
            string kindOfProduct = String.Empty;
            string nameProduct = String.Empty;
            string measureProduct = String.Empty;
            int price = -1;


            codeProduct = tfCodeProduct.Text;
            nameProduct = tfNameProduct.Text;
            measureProduct = cmbMeasure.SelectedItem.ToString();
            kindOfProduct = nameProduct + " " + measureProduct;

            Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab1 Unos novog proizvoda kafica. Sifra unetog proizvoda je " + codeProduct + ". Ime unetog proizvoda je " + nameProduct + ". Merna jedinica unetog proizvoda je " + measureProduct + ". Jedinicna cena unetog proizvoda(din) je: " + tfPrice.Text);

            int n;
            bool isNumeric = int.TryParse(tfPrice.Text, out n);
            if (isNumeric)
            {
                price = Convert.ToInt32(tfPrice.Text);
            }

            int n2;
            bool isNumeric2 = int.TryParse(tfPrice.Text, out n2);
            if (isNumeric2)
            {
                price = Convert.ToInt32(tfPrice.Text);
            }


            if (tfNameProduct.Text == String.Empty && tfPrice.Text == String.Empty && cmbMeasure.SelectedIndex == 0)
            {
                tbkRemark.Text = Constants.REMARKPRODUCTANDPRICEANDMEASURE;
                cmbMeasure.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }
            if (tfNameProduct.Text == String.Empty && tfPrice.Text == String.Empty)
            {
                tbkRemark.Text = Constants.REMARKPRODUCTANDPRICE;
                cmbMeasure.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (tfNameProduct.Text == String.Empty && cmbMeasure.SelectedIndex == 0)
            {
                tbkRemark.Text = Constants.REMARKPRODUCTANDMEASURE;
                tfPrice.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (tfPrice.Text == String.Empty && cmbMeasure.SelectedIndex == 0)
            {
                tbkRemark.Text = Constants.REMARKPRICEANDMEASURE;
                tfNameProduct.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (tfNameProduct.Text == String.Empty)
            {
                tbkRemark.Text = Constants.REMARKPRODUCT;
                tfPrice.Foreground = System.Windows.Media.Brushes.Red;
                cmbMeasure.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (tfPrice.Text == String.Empty)
            {
                tbkRemark.Text = Constants.REMARKPRICE;
                tfNameProduct.Foreground = System.Windows.Media.Brushes.Red;
                cmbMeasure.Foreground = System.Windows.Media.Brushes.Red;

                return;
            }
            else if (cmbMeasure.SelectedIndex == 0)
            {
                tbkRemark.Text = Constants.REMARKMEASURE;
                tfNameProduct.Foreground = System.Windows.Media.Brushes.Red;
                tfPrice.Foreground = System.Windows.Media.Brushes.Red;

                return;
            }

            //everything is OK!
            string wayDisplayBookBar = String.Empty;
            if (cmbWayDisplayBookBar.SelectedItem != null)
            {
                if (cmbWayDisplayBookBar.SelectedIndex == 0)
                {
                    wayDisplayBookBar = Constants.KOM;
                }
                else if (cmbWayDisplayBookBar.SelectedIndex == 1)
                {
                    wayDisplayBookBar = Constants.LIT;
                }
                else if (cmbWayDisplayBookBar.SelectedIndex == 2)
                {
                    wayDisplayBookBar = Constants.KG;
                }

            }


            tbkRemark.Text = String.Empty;
            _lastProduct = tfNameProduct.Text;
            _entered = true;
            Product product = new Product(codeProduct, kindOfProduct, nameProduct, measureProduct, price,0.0, wayDisplayBookBar);
            insertProductInDatabase(product);
            _products.Add(product);
            Products.Add(product.ComboBoxForm());
            Codes.Add(product.Code());
            //add product with order
            ProductWithOrderNumber productWithOrder = new ProductWithOrderNumber(product,_products.Count);
            _productsWithOrder.Add(productWithOrder);
            this.options.dataGridSchedule.ItemsSource = _productsWithOrder;
            ProductsWithOrderNames.Add(productWithOrder.KindOfProduct);
            cmbNameProductTab1.ItemsSource = ProductsWithOrderNames;
            //insert record in table productsWithOrderNumber in database
            insertRecordIntoproductsWithOrderNumber(productWithOrder);


            tfCodeProduct.Foreground = System.Windows.Media.Brushes.Green;
            tfNameProduct.Foreground = System.Windows.Media.Brushes.Green;
            tfPrice.Foreground = System.Windows.Media.Brushes.Green;
            cmbMeasure.Foreground = System.Windows.Media.Brushes.Green;
            createdReports.cmbProductsTab2.ItemsSource = Products;
            createdReports.cmbProductsTab2.SelectedIndex = 0;
            System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Da li želite da unesete stavku šanka za upravo uneti proizvod kafića?", "UNOS MAGACINSKE STAVKE", System.Windows.Forms.MessageBoxButtons.YesNo);
            Logger.writeNode(Constants.INFORMATION,"Da li želite da unesete stavku šanka za upravo uneti proizvod kafića?");
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        productStore.IsSelected = true;
                        enterStoreItemsTab2.cmbChooseEarlierProduct.ItemsSource = Products;
                        enterStoreItemsTab2.cmbChooseEarlierProduct.SelectedIndex = enterStoreItemsTab2.cmbChooseEarlierProduct.Items.Count - 1;
                    }
        }

        private void btnDeleteTextbox_Click(object sender, RoutedEventArgs e)
        {
            Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab1 Brisanje tekstualnih polja proizvoda kafica. Obrisana sifra je " + tfCodeProduct.Text + ". Ime obrisanog naziva proizvoda :" + tfNameProduct.Text + ". Ime obrisane merne jedinice :" + cmbMeasure.SelectedItem.ToString() + " .Ime obrisane jedinicne cene(din) :" + tfPrice.Text);
            _entered = false;
            tfCodeProduct.Text = String.Empty;
            tfNameProduct.Text = String.Empty;
            tfPrice.Text = String.Empty;
            cmbMeasure.SelectedIndex = 0;
            tfCodeProduct.Foreground = System.Windows.Media.Brushes.Black;
            tfNameProduct.Foreground = System.Windows.Media.Brushes.Black;
            tfPrice.Foreground = System.Windows.Media.Brushes.Black;
            cmbMeasure.Foreground = System.Windows.Media.Brushes.Black;

        }

        private void cmbRemoveMeasure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbRemoveMeasure.SelectedIndex == 0)
            {
                btnRemoveMeasure.IsEnabled = false;
            }
            else
            {
                btnRemoveMeasure.IsEnabled = true;
            }
        }

        private void cmbNameProductr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbNameProductr.SelectedIndex == 0)
            {
                btnRemoveItem.IsEnabled = false;
            }
            else
            {
                btnRemoveItem.IsEnabled = true;
            }

            if (cmbNameProductr.SelectedIndex != 0)
            {
                cmbNameProductr2.IsEnabled = false;
            }
            else
            {
                cmbNameProductr2.IsEnabled = true;
            }


            //find oldprice
            for (int i = 0; i < _products.Count; i++)
            {
                if (cmbNameProductr.SelectedItem != null)
                {
                    if (_products.ElementAt(i).KindOfProduct.Equals(cmbNameProductr.SelectedItem.ToString()) == true)
                    {
                        oldprice = _products.ElementAt(i).Price.ToString();
                        break;
                    }
                }
            }
        }


        private void cmbNameProductr2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbNameProductr2.SelectedIndex == 0)
            {
                btnRemoveItem.IsEnabled = false;
            }
            else
            {
                btnRemoveItem.IsEnabled = true;
            }

            if (cmbNameProductr2.SelectedIndex != 0)
            {
                cmbNameProductr.IsEnabled = false;
            }
            else
            {
                cmbNameProductr.IsEnabled = true;
            }

            //find oldprice
            for (int i = 0; i < _products.Count; i++)
            {
                if (cmbNameProductr2.SelectedItem != null)
                {
                    if (_products.ElementAt(i).CodeProduct.Equals(cmbNameProductr2.SelectedItem.ToString()) == true)
                    {
                        oldprice = _products.ElementAt(i).Price.ToString();
                        break;
                    }
                }
            }
        }


        #region enterLeaveMouseEvent

        private void redData()
        {
            tfNameProduct.Foreground = System.Windows.Media.Brushes.Red;
            tfPrice.Foreground = System.Windows.Media.Brushes.Red;
            cmbMeasure.Foreground = System.Windows.Media.Brushes.Red;
        }

        private void tfNameProduct_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_lastProduct.Equals(tfNameProduct.Text) == false)
            {
                if (tfCodeProduct.Text.Length == 0)
                {
                    tfNameProduct.IsReadOnly = true;
                    tbkRemark.Text = Constants.REMARKCODE;
                    tbkRemark.Foreground = System.Windows.Media.Brushes.Red;

                    redData();
                }
                checkCode();
            }
        }


        private void tfNameProduct_MouseLeave(object sender, MouseEventArgs e)
        {
            if (tfCodeProduct.Text.Length == 0)
            {
                tbkRemark.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void tfPrice_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_lastProduct.Equals(tfNameProduct.Text) == false)
            {
                if (tfCodeProduct.Text.Length == 0)
                {
                    tfPrice.IsReadOnly = true;
                    tbkRemark.Text = Constants.REMARKCODE;
                    tbkRemark.Foreground = System.Windows.Media.Brushes.Red;

                    redData();
                }
                checkCode();
            }
        }

        private void tfPrice_MouseLeave(object sender, MouseEventArgs e)
        {
            if (tfCodeProduct.Text.Length == 0)
            {
                tbkRemark.Foreground = System.Windows.Media.Brushes.Black;

            }
        }

        private void cmbMeasure_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_lastProduct.Equals(tfNameProduct.Text) == false)
            {
                if (tfCodeProduct.Text.Length == 0)
                {
                    tbkRemark.Text = Constants.REMARKCODE;
                    tbkRemark.Foreground = System.Windows.Media.Brushes.Red;
                    cmbMeasure.IsEnabled = false;

                    redData();
                }
                checkCode();
            }
        }

        private void cmbMeasure_MouseLeave(object sender, MouseEventArgs e)
        {
            if (tfCodeProduct.Text.Length == 0)
            {
                tbkRemark.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        #endregion

        #region tfCodeEvents

        private void tfCodeProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            tfCodeProduct.Foreground = System.Windows.Media.Brushes.Black;
            if (tfCodeProduct.Text.Length > 0)
            {
                tfNameProduct.IsReadOnly = false;
                tfPrice.IsReadOnly = false;
                cmbMeasure.IsEnabled = true;


                tfNameProduct.Foreground = System.Windows.Media.Brushes.Black;
                tfPrice.Foreground = System.Windows.Media.Brushes.Black;
                cmbMeasure.Foreground = System.Windows.Media.Brushes.Black;
                checkCode();
            }
        }

        private void checkCode()
        {
            if (tfCodeProduct.Text.Length > 0)
            {
                for (int i = 0; i < _products.Count; i++)
                {
                    if (tfCodeProduct.Text.Equals(_products.ElementAt(i).CodeProduct) == true)
                    {
                        tbkRemark.Text = Constants.USEDCODEPRODUCT + _products.ElementAt(i).NameProduct + " " + _products.ElementAt(i).MeasureProduct + " ";
                        tfCodeProduct.Foreground = System.Windows.Media.Brushes.Red;
                        tfNameProduct.IsReadOnly = true;
                        tfPrice.IsReadOnly = true;
                        cmbMeasure.IsEnabled = false;
                        cmbMeasure.Foreground = System.Windows.Media.Brushes.Red; ;
                        return;
                    }
                }
                tbkRemark.Text = String.Empty;
            }
        }

        private void tfCodeProduct_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_lastProduct.Equals(tfNameProduct.Text) == false)
            {
                checkCode();
            }
        }

            
        #endregion

        private void enableAfterCodeJustRemoved()
        {
            tfCodeProduct.Foreground = System.Windows.Media.Brushes.Black;
            tfNameProduct.IsReadOnly = false;
            tfPrice.IsReadOnly = false;
            cmbMeasure.IsEnabled = true;
            cmbMeasure.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void datepickerChangePrice_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (dataGrid1.Items.Count > 0)
                {
                    btncreateReport.IsEnabled = true;
                }
                // ... Get DatePicker reference.
                var picker = sender as DatePicker;

                // ... Get nullable DateTime from SelectedDate.
                DateTime? date = picker.SelectedDate;
                if (date == null)
                {
                    // ... A null object.
                    _currDate = String.Empty;
                }
                else
                {
                    // ... No need to display the time.
                    
                    dateChangePrice = date.Value;
                    Logger.writeNode(Constants.INFORMATION, "Tab2 Do kada je neka cena bila vazeca : " + dateChangePrice.ToShortDateString());
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You did not enter date in tab1!!!");
                Logger.writeNode(Constants.MESSAGEBOX, "You did not enter date in tab1!!!");
                savenumofitemsEVERCreated();
            }

        }

        private void insertRecordInHistoryChangePrices(string code, string name, string type, string oldprice, string newprice)
        {
            try
            {
                MainWindow win = (MainWindow)MainWindow.GetWindow(this);


                string id = "45";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;


                conHelp.Open();
                com = new OleDbCommand(query, conHelp);
                com.Parameters.AddWithValue("@ProductCode", code);
                com.Parameters.AddWithValue("@StoreItemName", name);
                com.Parameters.AddWithValue("@Type", type);
                com.Parameters.AddWithValue("@OLDProductAmount", oldprice);
                com.Parameters.AddWithValue("@NEWProductAmount", newprice);
                com.Parameters.AddWithValue("@DateChangeEntered", DateTime.Now.ToString());
                com.Parameters.AddWithValue("@DateValuated", dateChangePrice);


                com.ExecuteNonQuery();



                //refresh hPrices collection

                for (DateTime x = win.history.DateCreatedReportStartTab2; x <= win.history.DateCreatedReportEndTab2; x = x.AddDays(1))
                {
                    string dateCurrStr = x.ToString().Replace("0:00:00", "");
                    dateCurrStr = dateCurrStr.Substring(0, dateCurrStr.Length - 1);


                    HistoryChangePrices hPrice = new HistoryChangePrices(code, name, type, oldprice, newprice, DateTime.Now);

                    DateTime sHistRCorrDate = DateTime.Parse(hPrice.DateChanged);


                    if (dateCurrStr.Equals(sHistRCorrDate.ToShortDateString()) == true)
                    {


                        win.history.hPrices.Add(hPrice);

                        win.history.cvhPrices = CollectionViewSource.GetDefaultView(win.history.hPrices);
                        if (win.history.cvhPrices != null)
                        {
                            win.history.dataGridReadHistoryPrices.ItemsSource = win.history.cvhPrices;
                        }


                        break;
                    }
                }




            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
            }
            finally
            {
                if (conHelp != null)
                {
                    conHelp.Close();
                }
            }
        }


        private void btnChangePrice_Click(object sender, RoutedEventArgs e)
        {
            int index = cmbNameProductr.SelectedIndex;
            int index2 = cmbNameProductr2.SelectedIndex;
            int n;
            bool isNumeric = int.TryParse(tfNewPrice.Text, out n);
            string newprice;
            if (isNumeric)
            {

                if (index == 0 && index2 != 0) 
                {
                    index = index2;
                }

                oldprice = _products.ElementAt(index - 1).Price.ToString();
                _products.ElementAt(index - 1).Price = n;
                newprice = _products.ElementAt(index - 1).Price.ToString();
                Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab1 Promena cene proizvoda kafica. Vrsta proizvoda :" + _products.ElementAt(index - 1).KindOfProduct + ". Stara cena(din): " + oldprice + ". Nova cena(din): " + newprice);
                //insert record in history change prices
                insertRecordInHistoryChangePrices(_products.ElementAt(index - 1).CodeProduct.ToString(), _products.ElementAt(index - 1).KindOfProduct.ToString(), Constants.PRODUCT, oldprice, newprice);

                cmbNameProductr.SelectedIndex = 0;
                tbkRemark.Text = "Cena za proizvod " + _products.ElementAt(index - 1).KindOfProduct + "  je promenjena.Nova cena proizvoda je " + n  + " dinara.";
                tfNewPrice.Text = String.Empty;
                try
                {
                    //save change in database table products
                    con.Open();
                    string query = "UPDATE products SET Price = " + "'" + n + "'" + " WHERE CodeProduct =" + "'" + _products.ElementAt(index - 1).CodeProduct + "'" + ";";
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();

                    string query2 = "UPDATE products SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + " WHERE CodeProduct =" + "'" + _products.ElementAt(index - 1).CodeProduct + "'" + ";";
                    com = new OleDbCommand(query2, con);
                    com.ExecuteNonQuery();

                    string queryConn2 = "SELECT NumberOfUpdates FROM products WHERE CodeProduct = " + "'" + _products.ElementAt(index - 1).CodeProduct + "'" + ";";
                    com = new OleDbCommand(queryConn2, con);
                    dr = com.ExecuteReader();
                    int oldUpNum = 0;
                    while (dr.Read())
                    {
                        bool isNum = int.TryParse(dr["NumberOfUpdates"].ToString(), out oldUpNum);
                    }


                    int upNum = oldUpNum + 1;
                    query = "UPDATE products SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE CodeProduct =" + "'" + _products.ElementAt(index - 1).CodeProduct + "'" + ";";
                    com = new OleDbCommand(query, con);
                    com.ExecuteNonQuery();




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                    savenumofitemsEVERCreated();
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

                return;
            }
            else
            {
                MessageBox.Show("Nova cena nije uneta u obliku broja!! Proizvod ostaje sa starom cenom dok se za novu ne unese broj!", "NEISPRAVAN FORMAT UNOSA NOVE CENE");
                Logger.writeNode(Constants.MESSAGEBOX, "Nova cena nije uneta u obliku broja!! Proizvod ostaje sa starom cenom dok se za novu ne unese broj!");
                return;
            }
        }

        private void deletefromConnectionTable(Product p)
        {
            try
            {
                string id = "10";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "ConnCodeProduct=" + "'" + p.CodeProduct + "'" + ";";

                con.Open();
                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();

            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        private void updateProductsWithOrderInDatabase(int numOrderRemove, string codeproductRemove)
        {

            try
            {
                con.Open();

                string id = "62";//Queries.xml ID
                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
               
                string query = Query.Attribute(Constants.TEXT).Value;

                com = new OleDbCommand(query, con);
                com.Parameters.Add("@CodeProduct", codeproductRemove);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }


            numOrderRemove++;
            for (int i = numOrderRemove; i < _productsWithOrder.Count; i++)
            {

                try
                {
                    con.Open();

                    string query = "UPDATE productsWithOrderNumber SET NumberOrder = " + "'" + _productsWithOrder.ElementAt(i).OrderNumber + "'" + " WHERE CodeProduct = " + "'" + _productsWithOrder.ElementAt(i).CodeProduct + "'" + ";";

                    com = new OleDbCommand(query, con);
                    
                    com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                    savenumofitemsEVERCreated();
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

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {




            int index = cmbNameProductr.SelectedIndex;
            if (cmbNameProductr.SelectedIndex == 0 && cmbNameProductr2.SelectedIndex == 0)
            {
                tbkRemark.Text = Constants.REMOVEITEMREMARK;
            }
            else
            {
               

                tbkRemark.Text = String.Empty;
                if (cmbNameProductr.SelectedIndex != 0)
                {
                    
                    Product product = _products.ElementAt(index - 1);
                    deletefromConnectionTable(product);
                    Products.RemoveAt(index);
                    _products.RemoveAt(index - 1);
                    Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab1  Uklanjanje proizvoda kafica. Vrsta uklonjenog proizvoda kafica je :" + product.KindOfProduct + ". Jedinicna cena uklonjenog proizvoda je :" + product.Price);
                    removeProductFromDatabase(product);
                    cmbNameProductr.ItemsSource = Products;
                    cmbNameProductr.SelectedIndex = 0;
                    enterStoreItemsTab2.cmbChooseEarlierProduct.ItemsSource = Products;
                    enterStoreItemsTab2.cmbChooseEarlierProduct.SelectedIndex = 0;
                    createdReports.cmbProductsTab2.ItemsSource = Products;
                    createdReports.cmbProductsTab2.SelectedIndex = 0;


                    if (product.CodeProduct.Equals(Constants.CODENOTENTERED) == false)
                    {
                        for (int i = 0; i < Codes.Count; i++)
                        {
                            if (product.CodeProduct.Equals(Codes.ElementAt(i)))
                            {
                                string removedCode = Codes.ElementAt(i);
                                if (removedCode.Equals(tfCodeProduct.Text))
                                {
                                    enableAfterCodeJustRemoved();
                                }
                                Codes.RemoveAt(i);
                                break;
                            }
                        }
                        cmbNameProductr2.ItemsSource = Codes;
                        cmbNameProductr2.SelectedIndex = 0;
                    }



                    //work with product with order number
                    int orderNumRemove = -1;
                    int ordNum = ProductsWithOrderNames.IndexOf(product.KindOfProduct);
                    ProductsWithOrderNames.RemoveAt(ordNum);
                    cmbNameProductTab1.ItemsSource = ProductsWithOrderNames;
                    //then remove from collection _productsWithOrder
                    for (int j = 0; j < _productsWithOrder.Count; j++)
                    {
                        if (_productsWithOrder.ElementAt(j).KindOfProduct.Equals(product.KindOfProduct) == true)
                        {

                            //correct orderNumbers for other objects in collection _productsWithOrder
                            for (int k = j + 1; k < _productsWithOrder.Count; k++)
                            {
                                _productsWithOrder.ElementAt(k).OrderNumber--;
                            }

                            orderNumRemove = j;
                            updateProductsWithOrderInDatabase(orderNumRemove, product.CodeProduct);
                            _productsWithOrder.RemoveAt(j);
                            this.options.dataGridSchedule.ItemsSource = _productsWithOrder;
                        }
                    }
                    //END work with product with order number


                }
                if (cmbNameProductr2.SelectedIndex != 0)
                {
                    int ind = cmbNameProductr2.SelectedIndex;
                    string searchkey = cmbNameProductr2.Items[ind].ToString();
                    int prodindex;
                    Product product;
                    for (int i = 0; i < _products.Count; i++)
                    {
                        if (_products.ElementAt(i).CodeProduct.Equals(searchkey))
                        {
                            product = _products.ElementAt(i);
                            Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab1 Uklanjanje proizvoda kafica [preko izabrane sifre proizvoda]. Vrsta uklonjenog proizvoda kafica je :" + product.KindOfProduct + ". Jedinicna cena uklonjenog proizvoda je :" + product.Price);
                            removeProductFromDatabase(product);
                            prodindex = i;
                            Products.RemoveAt(prodindex + 1);
                            cmbNameProductr.ItemsSource = Products;
                            enterStoreItemsTab2.cmbChooseEarlierProduct.ItemsSource = Products;
                            createdReports.cmbProductsTab2.ItemsSource = Products;
                            if (cmbNameProductr.SelectedIndex == prodindex)
                            {
                                cmbNameProductr.SelectedIndex = 0;
                                enterStoreItemsTab2.cmbChooseEarlierProduct.SelectedIndex = 0;
                            }
                            _products.RemoveAt(prodindex);

                            //work with product with order number
                            int orderNumRemove = -1;
                            int ordNum = ProductsWithOrderNames.IndexOf(product.KindOfProduct);
                            ProductsWithOrderNames.RemoveAt(ordNum);
                            cmbNameProductTab1.ItemsSource = ProductsWithOrderNames;
                            //then remove from collection _productsWithOrder
                            for (int j = 0; j < _productsWithOrder.Count; j++)
                            {
                                if (_productsWithOrder.ElementAt(j).KindOfProduct.Equals(product.KindOfProduct) == true)
                                {
                                   
                                    //correct orderNumbers for other objects in collection _productsWithOrder
                                    for (int k = j+1; k < _productsWithOrder.Count; k++)
                                    {
                                        _productsWithOrder.ElementAt(k).OrderNumber--;
                                    }

                                    orderNumRemove = j;
                                    updateProductsWithOrderInDatabase(orderNumRemove,product.CodeProduct);
                                    _productsWithOrder.RemoveAt(j);
                                    this.options.dataGridSchedule.ItemsSource = _productsWithOrder;
                                }
                            }
                            //END work with product with order number

                            break;
                        }
                    }
                    string removedCode = Codes.ElementAt(ind);
                    if (removedCode.Equals(tfCodeProduct.Text))
                    {
                        enableAfterCodeJustRemoved();
                    }
                    Codes.RemoveAt(ind);
                   

                    cmbNameProductr2.ItemsSource = Codes;
                    cmbNameProductr2.SelectedIndex = 0;
                }

            }
        }

       


        #endregion

        #region creatingReports_FirstTab



        private void SelectionChanged_cmbNameProductTab1(object sender, SelectionChangedEventArgs e)
       {
           if (cmbNameProductTab1.SelectedItem != null)
           {
               Logger.writeNode(Constants.INFORMATION, "Tab1 Za proizvod kafica izabran je " + cmbNameProductTab1.SelectedItem.ToString());
               if (cmbNameProductTab1.SelectedIndex == 0) { tblPriceTab1.Text = String.Empty; tfAmount.IsReadOnly = true; return; }
               else { tfAmount.IsReadOnly = false; tblRemarkTab1.Text = String.Empty; }

               int searchindex = cmbNameProductTab1.SelectedIndex;
               string searchword = cmbNameProductTab1.Items[searchindex].ToString();
               Product p;
               for (int i = 0; i < _products.Count; i++)
               {
                   if (_products.ElementAt(i).KindOfProduct.Equals(searchword))
                   {
                       p = _products.ElementAt(i);
                       tblPriceTab1.Text = p.Price.ToString() + " " + _currency;
                       return;
                   }
               }
           }

       }


        private void writeRecordInHistoryItemsOutput(Item item,double productAm, StoreItemProduct It , string statusForHistoryOutput, string amountLastEntered,int numofRecipe) 
        {
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab1 Upis u istoriju recepata koriscenih za stavku kafica " + item.KindOfProduct);
               string id = "26";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "'" + item.NumOfCount + "'" + ";";

                conHistory.Open();
                com = new OleDbCommand(query, conHistory);
                dr = com.ExecuteReader();


                int oldamount = 0;
                int oldcostitem = 0;


                while (dr.Read())
                {
                    bool isNum = int.TryParse(dr["NumberOfSoldItem"].ToString(), out oldamount);
                    bool isNumN = int.TryParse(dr["WholeItemCost"].ToString(), out oldcostitem);

                } // end of while loop
                if (conHistory != null)
                {
                    conHistory.Close();
                }

               
                if (statusForHistoryOutput.Equals(Constants.NOMOREITEM) == false)
                {
                    id = "25";//Queries.xml ID

                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                     Query = (from xml2 in xdoc.Descendants("Query")
                             where xml2.Element("ID").Value == id
                             select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                     query = Query.Attribute(Constants.TEXT).Value;
                     _currDate = datepicker1.SelectedDate.ToString();
                     query = query + "(" + "'" + _numOfEverCreatedItem.ToString() + "'" + "," + "'" + item.KindOfProduct + "'" + "," + "'" + item.Price + "'" + "," + "'" + item.Amount + "'" + "," + "'" + item.CostItem + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + statusForHistoryOutput + "'" + "," + "'" + item.Shift + "'" + "," + "'" + It.CodeProduct + "'" + "," + "'" + It.KindOfProduct + "'" + "," + "'" + amountLastEntered + "'" + "," + "'" + It.Group + "'" + "," + "'" + productAm + "'" + "," + "'" + "0" + "'" + "," + "'" + item.NumOfUsedRecipes + "'" + "," + "'" + datepicker1.SelectedDate.Value.ToShortDateString() + "'" + ");";


                    conHistory.Open();
                    com = new OleDbCommand(query, conHistory);
                    com.ExecuteNonQuery();
                }
                else
                {
                    id = "25";//Queries.xml ID

                    /*int newAmount = item.Amount - oldamount;
                    int newCostItem = item.CostItem - oldcostitem;*/

                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    Query = (from xml2 in xdoc.Descendants("Query")
                             where xml2.Element("ID").Value == id
                             select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    query = Query.Attribute(Constants.TEXT).Value;
                    query = query + "(" + "'" + _numOfEverCreatedItem.ToString() + "'" + "," + "'" + item.KindOfProduct + "'" + "," + "'" + item.Price + "'" + "," + "'" + item.Amount + "'" + "," + "'" + item.CostItem + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + Constants.NOMOREITEM + "'" + "," + "'" + item.Shift + "'" + "," + "'" + It.CodeProduct + "'" + "," + "'" + It.KindOfProduct + "'" + "," + "'" + amountLastEntered + "'" + "," + "'" + It.Group + "'" + "," + "'" + productAm + "'" + "," + "'" + "0" + "'" + "," + "'" + item.NumOfUsedRecipes + "'" + "," + "'" + datepicker1.SelectedDate.Value.ToShortDateString() + "'" + ");";


                    conHistory.Open();
                    com = new OleDbCommand(query, conHistory);
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                if (conHistory != null)
                {
                    conHistory.Close();
                }
                if (dr != null)
                {
                    dr.Close();
                }
            }
        }
 
      

        private void writeItemInAllItemsSoldEver(Item item)
        {

            Logger.writeNode(Constants.INFORMATION, "Tab1 Upis u ikada prodate proizvode kafica Proizvod: " + item.KindOfProduct);
            try
            {

                string id = "27";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                query = query + "(" + "'" + _numOfEverCreatedItem.ToString() + "'" + "," + "'" + item.CodeProduct + "'" + "," + "'" + item.KindOfProduct + "'" + "," + "'" + item.Price + "'" + "," + "'" + item.Amount + "'" + "," + "'" + item.CostItem + "'" + "," + "'" + item.Shift + "'" + "," + "'" + _dateCreatedReport + "'" + "," + "'" + DateTime.Now + "'" + ");"; 

                con.Open();
                com = new OleDbCommand(query, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                savenumofitemsEVERCreated();
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
           //empty event
        }

        private void dataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                btnremoveItem.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            }
        }

        private void tfAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                btnaddItem.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            }
        }

        private void autoscrollToBottom(DataGrid dgrid) 
        {
            if (dgrid.Items.Count > 0)
            {
                var border = VisualTreeHelper.GetChild(dgrid, 0) as Decorator;
                if (border != null)
                {
                    var scroll = border.Child as ScrollViewer;
                    if (scroll != null) scroll.ScrollToEnd();
                }
            }
        }

        private void sortDateUpdate(Item item) 
        {
            int index = ProductsWithOrderNames.IndexOf(item.KindOfProduct);
            //MessageBox.Show("Broj indexa je : " + index);
            
           
            for (int i = (_items.Count-1); i > -1; i--)
            {
                if (i == (index - 2))
                {
                    _items.Add(item);
                    break;
                }


                // when you must move some elements
                _items.Insert(index - 1,item);
                break;
               
                
            }
            if (_items.Count == 0)
            {
                _items.Add(item);
            }
            
        }

       

       private void Click_btnaddItem(object sender, RoutedEventArgs e)
       {
           if (chbNotWorkingDay.IsChecked == true)
           {
               MessageBox.Show("Ovaj dan ste označili neradnim!!!");
               Logger.writeNode(Constants.MESSAGEBOX, "Ovaj dan ste označili neradnim!!!");
               return;
           }


           if (IsEnteredMoreBuyedStoreItems == false)
           {
               MessageBox.Show("Niste uneli unos u šank za selektovani datum.");
               return;
           }

           if (_items.Count != 0)
           {
               btncreateReport.IsEnabled = false;
           }

           if (numberOfEnteredProduct == _products.Count + 1) 
           {
               MessageBox.Show("Za selektovani datum uneli ste stavke u knjigu šanka! Pritisni dugme za kreiranje knjige šanka!");
               return;
           }

           string statusForHistoryOutput = String.Empty;
           bool isHaveAllStoreItems = false;
           bool isHave = false; // do you have store item in storehouse
           ObservableCollection<StoreItemProduct> storeItems = new ObservableCollection<StoreItemProduct>();
           ObservableCollection<double> realSpents = new ObservableCollection<double>();

           StorehouseItem storeItems_notEnough = new StorehouseItem();
          

           MainWindow window = (MainWindow)MainWindow.GetWindow(this);

          
           btncreateReport.IsEnabled = true;
          
           
           tblRemarkTab1.Text = String.Empty;
           if (tfAmount.Text.Equals(String.Empty)) { tblRemarkTab1.Text = Constants.REMARKAMOUNTNOTNUMERIC_NOTENTERED; return; }
           string codeProduct = String.Empty;
           string nameProduct = String.Empty;


           StoreItemProduct stIt;
           double realSpent = 0.0;
           // this three collections always are together for example coffee with milk(product), then productAms have values 0.05 and 0.1 which means in coffee with milk(product)has 5g of coffee(store item) and 10ml of milk(store item). Values of items is two object StoreItmeProduct {KindofProduct: Any kind of coffee ; KindoFProduct: Any kind of milk}
           ObservableCollection<double> productAms = new ObservableCollection<double>();// how many product (in kg/l) have in used store item
           ObservableCollection<StoreItemProduct> itemsStore = new ObservableCollection<StoreItemProduct>();//list of store items used in product
            ObservableCollection<bool> isRealSpent = new ObservableCollection<bool>();// if have store item in storehouse for example items[0] then isRealSpent[0]=true, but if haven't store item in storehouse for example items[1] then isRealSpent[1]= false
           // this three collections always are together
           ObservableCollection<string> statusesForHistoryOutput = new ObservableCollection<string>();
           

           int index = cmbNameProductTab1.SelectedIndex;
           nameProduct = cmbNameProductTab1.Items[index].ToString();
           _lastAmount =  tfAmount.Text;

           for (int i = 0; i < _products.Count; i++ )
           {
               if(_products.ElementAt(i).KindOfProduct.Equals(nameProduct))
               {
                   int amount,itemValue;
                   bool isNumeric = int.TryParse(tfAmount.Text,out amount);// number of sold products Items class ATTENTION!
                   bool isNumeric2 = int.TryParse(tblItemValueNumber.Text.Split().ElementAt(0),out itemValue);
                   if (amount < 0)
                   {
                       MessageBox.Show("Uneli ste negativan broj!");
                       return;
                   }


                   Product p = _products.ElementAt(i);

              
                   for (int j = 0; j < p.StoreItemProducts.Count; j++) 
                   {
                       double productAm = 0.0;
                       if (p.StoreItemProducts.ElementAt(j).isUsed == true)
                       {
                           stIt = p.StoreItemProducts.ElementAt(j);
                           storeItems.Add(stIt);
                           itemsStore.Add(stIt);// data for history items table in database
                           for(int k = 0; k < window.selectUpdateConnProdStore.Records.Count; k++)
                           {
                               if(window.selectUpdateConnProdStore.Records.ElementAt(k).ConnCodeProduct.Equals(p.CodeProduct)  && window.selectUpdateConnProdStore.Records.ElementAt(k).ConnStoreItemCode.Equals(stIt.CodeProduct))
                               {
                                  
                                   string amWitPoint = window.selectUpdateConnProdStore.Records.ElementAt(k).AmountProduct.Replace(',', '.');

                                    bool isN = Double.TryParse(amWitPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out productAm);
                                   productAms.Add(productAm);// data for history items table in database
                                   realSpent = amount * productAm;
                                   realSpents.Add(realSpent);
                                   statusesForHistoryOutput.Add(Constants.DENIED);
                                   break;
                               }                           
                           }
                          
                           int c;
                           for ( c = 0; c < window.storehouse.StorehouseItems.Count; c++)
                           {
                               if (window.storehouse.StorehouseItems.ElementAt(c).ItemCode.Equals(stIt.CodeProduct))
                               {
                                   realSpent = Math.Round(realSpent,5);
                                   if (window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount < (realSpent + window.storehouse.StorehouseItems.ElementAt(c).Threshold/* - 0.000001*/))
                                   {
                                       isHaveAllStoreItems = false;
                                       storeItems_notEnough = window.storehouse.StorehouseItems.ElementAt(c);
                                       MessageBox.Show("Nema dovoljno na stanju! Imate u šanku " + storeItems_notEnough.ItemName + " još " + storeItems_notEnough.ItemRealAmount + "  u (kg/l) !");
                                       string status = statusesForHistoryOutput.Last();
                                       status = Constants.DENIED;
                                       statusesForHistoryOutput[statusesForHistoryOutput.Count - 1] = status;
                                       break;                                   
                                   }
                                   else if (window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount == (realSpent + window.storehouse.StorehouseItems.ElementAt(c).Threshold) /*|| window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount - (realSpent + window.storehouse.StorehouseItems.ElementAt(c).Threshold) <= 0.000001*/)
                                   {


                                       if (window.storehouse.StorehouseItems.ElementAt(c).Threshold == 0)
                                       {

                                           isHaveAllStoreItems = true;
                                           MessageBox.Show("Upravo ste potrošili sve zalihe  " + stIt.KindOfProduct + " !");
                                           Logger.writeNode(Constants.MESSAGEBOX, "Upravo ste potrošili sve zalihe  " + stIt.KindOfProduct + " !");
                                           string status = statusesForHistoryOutput.Last();
                                           status = Constants.NOMOREITEM;
                                           statusesForHistoryOutput[statusesForHistoryOutput.Count - 1] = status;
                                       }
                                       else
                                       {
                                           isHaveAllStoreItems = false;
                                           MessageBox.Show("Imate u šanku još " + storeItems_notEnough.ItemName + "u (kg/l) ali morate dozvoliti njihovu upotrebu!");
                                           Logger.writeNode(Constants.MESSAGEBOX, "Imate u šanku još " + storeItems_notEnough.ItemName + "u (kg/l) ali morate dozvoliti njihovu upotrebu!");
                                           string status = statusesForHistoryOutput.Last();
                                           status = Constants.DENIED;
                                           statusesForHistoryOutput[statusesForHistoryOutput.Count - 1] = status;
                                           break;
                                       }
                                   }
                                   else
                                   {
                                       string status = statusesForHistoryOutput.Last();
                                       status = "Accepted!!!";
                                       statusesForHistoryOutput[statusesForHistoryOutput.Count - 1] = status;
                                       isHaveAllStoreItems = true;
                                   }
                               }//end of if is have item in storehouse
                           }//end of for search in storehouse
                           if (isHaveAllStoreItems == false && c == window.storehouse.StorehouseItems.Count)
                           {
                               string status = statusesForHistoryOutput.Last();
                               status = Constants.DENIED;
                               statusesForHistoryOutput[statusesForHistoryOutput.Count - 1] = status;
                               MessageBox.Show("Nemate stavku " + stIt.KindOfProduct + " u šanku");
                               Logger.writeNode(Constants.MESSAGEBOX, "Nemate stavku " + stIt.KindOfProduct + " u šanku");
                               break;
                           }
                           else if (isHaveAllStoreItems == false)
                           {
                               break;
                           }
                       }// end of if isUsed = true
                   }

                  

                   if (isHaveAllStoreItems)
                   {
                       // update storehouse
                       for (int k = 0; k < storeItems.Count; k++)
                       {
                           for (int c = 0; c < window.storehouse.StorehouseItems.Count; c++)
                           {
                               if (storeItems.ElementAt(k).CodeProduct.Equals(window.storehouse.StorehouseItems.ElementAt(c).ItemCode) == true)
                               {
                                   double currSpent =  Math.Round(realSpents.ElementAt(k), 5);
                                   //window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount = window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount - realSpents.ElementAt(k);
                                   window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount = window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount - currSpent;

                                   //refresh if store item selected in storehouse window
                                   if (storehouse.tf1.Text.Equals(window.storehouse.StorehouseItems.ElementAt(c).ItemCode) == true)
                                   {
                                       storehouse.tf4.Text = window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount.ToString();
                                       double newRealPrice = window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount / window.storehouse.StorehouseItems.ElementAt(c).ItemforOneAmount * window.storehouse.StorehouseItems.ElementAt(c).ItemforOnePrice;
                                       storehouse.tf5.Text = newRealPrice.ToString();
                                   }

                                   if (window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount == 0)
                                   {
                                       //refresh if store item selected in storehouse window
                                       if (storehouse.tf1.Text.Equals(window.storehouse.StorehouseItems.ElementAt(c).ItemCode) == true) 
                                       {
                                           storehouse.tf1.Text = String.Empty;
                                           storehouse.tf2.Text = String.Empty;
                                           storehouse.tf3.Text = String.Empty;
                                           storehouse.tf4.Text = String.Empty;
                                           storehouse.tf5.Text = String.Empty;
                                       }

                                       window.storehouse.StorehouseItems.RemoveAt(c);
                                       // delete item from database table storehouse
                                       try
                                       {

                                           string id = "19";//Queries.xml ID

                                           XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                                           XElement Query = (from xml2 in xdoc.Descendants("Query")
                                                             where xml2.Element("ID").Value == id
                                                             select xml2).FirstOrDefault();
                                           Console.WriteLine(Query.ToString());
                                           string query = Query.Attribute(Constants.TEXT).Value;
                                           query = query + "'" + storeItems.ElementAt(k).CodeProduct + "'" + ";";

                                           con.Open();
                                           com = new OleDbCommand(query, con);
                                           com.ExecuteNonQuery();
                                           break;
                                       }
                                       catch (Exception ex)
                                       {
                                           MessageBox.Show(ex.Message);
                                           Logger.writeNode(Constants.EXCEPTION, ex.Message);
                                           savenumofitemsEVERCreated();

                                       }
                                       finally
                                       {
                                           if (con != null)
                                           {
                                               con.Close();
                                           }
                                       }

                                   }
                                   else 
                                   {
                                       double newPriceOfStoreItem = 0.0;
                                       newPriceOfStoreItem = storeItems.ElementAt(k).Price * (window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount / storeItems.ElementAt(k).Amount);
                                       window.storehouse.StorehouseItems.ElementAt(c).ItemPrice = newPriceOfStoreItem;

                                       //update storehouseItem in database
                                       try
                                       {
                                           con.Open();
                                           string queryStorehouse = "UPDATE storehouse SET RealAmount = " + "'" + window.storehouse.StorehouseItems.ElementAt(c).ItemRealAmount.ToString() + "'" + " WHERE StoreItemCode =" + "'" + storeItems.ElementAt(k).CodeProduct + "'" + ";";
                                           com = new OleDbCommand(queryStorehouse, con);
                                           com.ExecuteNonQuery();
                                           queryStorehouse = "UPDATE storehouse SET RealPrice = " + "'" + newPriceOfStoreItem.ToString() + "'" + " WHERE StoreItemCode =" + "'" + storeItems.ElementAt(k).CodeProduct + "'" + ";";
                                           com = new OleDbCommand(queryStorehouse, con);
                                           com.ExecuteNonQuery();
                                           queryStorehouse = "UPDATE storehouse SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE StoreItemCode =" + "'" + storeItems.ElementAt(k).CodeProduct + "'" + ";";
                                           com = new OleDbCommand(queryStorehouse, con);
                                           com.ExecuteNonQuery();

                                           string query = "SELECT NumberOfUpdates FROM storehouse WHERE StoreItemCode = " + "'" + storeItems.ElementAt(k).CodeProduct + "'" + ";";
                                           com = new OleDbCommand(query, con);
                                           dr = com.ExecuteReader();
                                           int oldUpNum = 0;
                                           while (dr.Read())
                                           {
                                               bool isNum = int.TryParse(dr["NumberOfUpdates"].ToString(), out oldUpNum);
                                           }


                                           int upNum = oldUpNum + 1;
                                           queryStorehouse = "UPDATE storehouse SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE StoreItemCode =" + "'" + storeItems.ElementAt(k).CodeProduct + "'" + ";";
                                           com = new OleDbCommand(queryStorehouse, con);
                                           com.ExecuteNonQuery();
                                           break;
                                       }
                                       catch (Exception ex)
                                       {
                                           MessageBox.Show(ex.Message);
                                           Logger.writeNode(Constants.EXCEPTION, ex.Message);
                                           savenumofitemsEVERCreated();
                                       }
                                       finally
                                       {
                                           if (con != null)
                                           {
                                               con.Close();
                                           }
                                       }
                                       //update storehouseItem in database
 
                                   }
                               }
                           }

                       }


                       // update storehouse
                   }
                  
                     
                      
                
                       if (isNumeric && isNumeric2)
                       {

                           // Items class amount is not in kg or l this amount is integer ,number of sold products
                           Item item;
                           bool sameProduct = false;
                           for (int w = 0; w < _items.Count; w++)
                           {
                               if (_items.ElementAt(w).CodeProduct.Equals(p.CodeProduct))
                               {
                                   sameProduct = true;
                                   int ind = statusesForHistoryOutput.IndexOf(Constants.DENIED);
                                   if (ind != -1)
                                   {
                                       for (int pind = 0; pind < statusesForHistoryOutput.Count; pind++)
                                       {
                                           writeRecordInHistoryItemsOutput(_items.ElementAt(w), productAms.ElementAt(pind), itemsStore.ElementAt(pind), statusesForHistoryOutput.ElementAt(pind), "0", _items.ElementAt(w).NumOfUsedRecipes);
                                       }
                                       _total = 0;
                                       return;
                                   }

                                   int n;
                                   bool isNum = int.TryParse(tfAmount.Text, out n);
                                   //update amount
                                   _items.ElementAt(w).Amount = _items.ElementAt(w).Amount + n;
                                   Logger.writeNode(Constants.INFORMATION, "Tab1 Dodavanje postojeceg proizvoda kafica: " + _items.ElementAt(w).KindOfProduct + " Broj prodatih proizvoda(ne ukupna suma): " + tfAmount.Text);
                                   _total = _total + n * _items.ElementAt(w).Price;
                                   _items.ElementAt(w).NumOfUsedRecipes = _items.ElementAt(w).NumOfUsedRecipes + 1;
                                   //update costitem
                                   _items.ElementAt(w).CostItem = _items.ElementAt(w).Amount * _items.ElementAt(w).Price;
                                   int inds;
                                   for (int f = 0; f < storeItems.Count; f++) 
                                   {
                                       inds = _items.ElementAt(w).UsedStoreItem.IndexOf(storeItems.ElementAt(f));
                                       if (inds == -1)
                                       {
                                           _items.ElementAt(w).UsedStoreItem.Add(storeItems.ElementAt(f));
                                       }
                                   }
                                   dataGrid1.ItemsSource = _items;
                                   autoscrollToBottom(dataGrid1);
                                   writeItemInAllItemsSoldEver(_items.ElementAt(w));


                                  
                                   //write in history table
                                   for (int c = 0; c < productAms.Count; c++)
                                   {
                                       writeRecordInHistoryItemsOutput(_items.ElementAt(w), productAms.ElementAt(c), itemsStore.ElementAt(c), statusesForHistoryOutput.ElementAt(c), tfAmount.Text, _items.ElementAt(w).NumOfUsedRecipes);
                                   }

                                   //update database allItemsSoldEver
                                   try
                                   {
                                       con.Open();
                                       string query = "UPDATE allItemsSoldEver SET NumberOfSoldItemPieces = " + "'" + _items.ElementAt(w).Amount + "'" + " WHERE NumberOfItemCreated =" + "'" + _items.ElementAt(w).NumOfCount + "'" + ";";
                                       com = new OleDbCommand(query, con);
                                       com.ExecuteNonQuery();
                                       query = "UPDATE allItemsSoldEver SET WholeItemCost = " + "'" + _items.ElementAt(w).CostItem + "'" + " WHERE NumberOfItemCreated =" + "'" + _items.ElementAt(w).NumOfCount + "'" + ";";
                                       com = new OleDbCommand(query, con);
                                       com.ExecuteNonQuery();

                                   }
                                    
                                /*   if( statusesForHistoryOutput.ElementAt(c).Equals(Constants.DENIED))
                                   {
                                       return;
                                   }
                                   */
                                   catch (Exception ex)
                                   {
                                       MessageBox.Show(ex.Message);
                                       Logger.writeNode(Constants.EXCEPTION, ex.Message);
                                       savenumofitemsEVERCreated();
                                   }
                                   finally
                                   {
                                       if (con != null)
                                       {
                                           con.Close();
                                       }
                                   }

                               }// action if same products added
                           }// end of for loop
                           if (sameProduct == false)
                           {
                               numberOfEnteredProduct++;
                               //ordinalNumbers.Add(numberOfEnteredProduct.ToString());
                               this.cmbNameProductTab1.SelectedIndex = numberOfEnteredProduct + 1; 
                               if (numberOfEnteredProduct == _products.Count)
                               {
                                   MessageBox.Show("Uneli ste sve proizvode u knjigu šanka.");
                               }

                               _numOfEverCreatedItem++;
                               if (cmbShift.SelectedIndex == 0)
                               {

                                   item = new Item(p,p.CodeProduct, p.KindOfProduct, p.WayDisplayBookBar, p.Price, amount, itemValue, Shift.PrvaSmena.ToString(), _numOfEverCreatedItem);
                                   item.NumOfUsedRecipes = item.NumOfUsedRecipes + 1;
                                   int ind = statusesForHistoryOutput.IndexOf(Constants.DENIED);
                                   Logger.writeNode(Constants.INFORMATION, "Tab1 Dodavanje novog proizvoda kafica: " + item.KindOfProduct + " Broj prodatih proizvoda: " + item.Amount + "  u prvoj smeni");
                                  

                                   if (ind != -1)
                                   {
                                       item.Amount = 0;
                                       item.CostItem = 0;
                                       //_total = 0;
                                       //_items.Add(item);  
                                       sortDateUpdate(item);

                                       for (int pind = 0; pind < statusesForHistoryOutput.Count; pind++)
                                       {
                                           writeRecordInHistoryItemsOutput(item, productAms.ElementAt(pind), itemsStore.ElementAt(pind), statusesForHistoryOutput.ElementAt(pind), "0", item.NumOfUsedRecipes);
                                       }

                                    


                                      /* tblTotalValue.Text = _total.ToString() + " " + _currency;
                                       tblTotalValue.Foreground = System.Windows.Media.Brushes.Red;
                                       tblTotalValue.FontWeight = FontWeights.Bold;*/
                                       autoscrollToBottom(dataGrid1);
                                       return;
                                   }

                                   _total = _total + item.CostItem;
                                   //_items.Add(item);  
                                   sortDateUpdate(item);
                                   item.UsedStoreItem = storeItems;
                                   dataGrid1.ItemsSource = _items;
                                   autoscrollToBottom(dataGrid1);
                                   
                                   writeItemInAllItemsSoldEver(item);
                                   for (int c = 0; c < productAms.Count; c++)
                                   {

                                       writeRecordInHistoryItemsOutput(item, productAms.ElementAt(c), itemsStore.ElementAt(c), statusesForHistoryOutput.ElementAt(c), tfAmount.Text, item.NumOfUsedRecipes);
                                       
                                   }

                               }
                               else if (cmbShift.SelectedIndex == 1)
                               {
                                   item = new Item(p,p.CodeProduct, p.KindOfProduct, p.WayDisplayBookBar, p.Price, amount, itemValue, Shift.DrugaSmena.ToString(), _numOfEverCreatedItem);
                                   item.NumOfUsedRecipes = item.NumOfUsedRecipes + 1;
                                   Logger.writeNode(Constants.INFORMATION, "Tab1 Dodavanje novog proizvoda kafica: " + item.KindOfProduct + " Broj prodatih proizvoda: " + item.Amount + "  u drugoj smeni");
                                   _total = _total + item.CostItem;
                                   //_items.Add(item);  
                                   sortDateUpdate(item);
                                   item.UsedStoreItem = storeItems;
                                   dataGrid1.ItemsSource = _items;
                                   autoscrollToBottom(dataGrid1);
                                   writeItemInAllItemsSoldEver(item);
                                   for (int c = 0; c < productAms.Count; c++)
                                   {
                                       writeRecordInHistoryItemsOutput(item, productAms.ElementAt(c), itemsStore.ElementAt(c), statusesForHistoryOutput.ElementAt(c), tfAmount.Text, item.NumOfUsedRecipes);
                                     
                                   }
                               }
                               else if (cmbShift.SelectedIndex == 2)
                               {
                                   item = new Item(p,p.CodeProduct, p.KindOfProduct, p.WayDisplayBookBar, p.Price, amount, itemValue, Shift.TrećaSmena.ToString(), _numOfEverCreatedItem);
                                   item.NumOfUsedRecipes = item.NumOfUsedRecipes + 1;
                                   Logger.writeNode(Constants.INFORMATION, "Tab1 Dodavanje novog proizvoda kafica: " + item.KindOfProduct + " Broj prodatih proizvoda: " + item.Amount + "  u trecoj smeni");
                                   _total = _total + item.CostItem;
                                   //_items.Add(item);  
                                   sortDateUpdate(item);
                                   item.UsedStoreItem = storeItems;
                                   dataGrid1.ItemsSource = _items;
                                   autoscrollToBottom(dataGrid1);
                                   writeItemInAllItemsSoldEver(item);
                                   for (int c = 0; c < productAms.Count; c++)
                                   {
                                       writeRecordInHistoryItemsOutput(item, productAms.ElementAt(c), itemsStore.ElementAt(c), statusesForHistoryOutput.ElementAt(c), tfAmount.Text, item.NumOfUsedRecipes);
                                      
                                   }
                               }
                               else
                               {
                                   item = new Item(p,p.CodeProduct, p.KindOfProduct, p.WayDisplayBookBar, p.Price, amount, itemValue, Shift.ČetvrtaSmena.ToString(), _numOfEverCreatedItem);
                                   item.NumOfUsedRecipes = item.NumOfUsedRecipes + 1;
                                   Logger.writeNode(Constants.INFORMATION, "Tab1 Dodavanje novog proizvoda kafica: " + item.KindOfProduct + " Broj prodatih proizvoda: " + item.Amount + "  u cetvrtoj smeni");
                                   _total = _total + item.CostItem;
                                   //_items.Add(item);  
                                   sortDateUpdate(item);
                                   item.UsedStoreItem = storeItems;
                                   dataGrid1.ItemsSource = _items;
                                   autoscrollToBottom(dataGrid1);
                                   writeItemInAllItemsSoldEver(item);
                                   for (int c = 0; c < productAms.Count; c++)
                                   {
                                       writeRecordInHistoryItemsOutput(item, productAms.ElementAt(c), itemsStore.ElementAt(c), statusesForHistoryOutput.ElementAt(c), tfAmount.Text, item.NumOfUsedRecipes);
                                      
                                   }
                               }
                           }//add new item

                           tblTotalValue.Text = _total.ToString() + " " + _currency;
                           tblTotalValue.Foreground = System.Windows.Media.Brushes.Red;
                           tblTotalValue.FontWeight = FontWeights.Bold;


                       }
                       else if (isNumeric == false)
                       {
                           tblRemarkTab1.Text = Constants.REMARKAMOUNTNOTNUMERIC2;
                           tblItemValueNumber.Text = "0" + " " + _currency;
                           return;
                       }
                   
                   tfAmount.Text = String.Empty;
                  
                   tblItemValueNumber.Text = "0" + " " + _currency;
                   checkRemarkDownTab1();

                  
                      
                   return;
               }
           }
          
       }


       private void MouseEnter_btnaddItem(object sender, MouseEventArgs e)
       {
           int n;
           bool isNumeric = int.TryParse(tfAmount.Text ,out n);
           if (tfAmount.Text.Equals(String.Empty))
           {
               tblRemarkTab1.Text = Constants.REMARKAMOUNT;
           }
           else if (isNumeric == false)
           {
               tblRemarkTab1.Text = Constants.REMARKAMOUNTNOTNUMERIC;
           }
       }


       private void MouseLeave_btnaddItem(object sender, MouseEventArgs e)
       {
           int n;
           bool isNumeric = int.TryParse(_lastAmount,out n);
           if (isNumeric == false && _lastAmount.Equals(String.Empty) == false)
           {
               tblRemarkTab1.Text = Constants.REMARKAMOUNTNOTNUMERIC2;
           }
           else 
           {
               tblRemarkTab1.Text = String.Empty; 
           }
           
           tblTotalValue.Foreground = System.Windows.Media.Brushes.Black;
           tblTotalValue.FontWeight = FontWeights.Normal;
       }

       private void updateStoreHouseWithDeletedItem(Item itemForRemove) 
       {

           StoreItemProduct si = new StoreItemProduct();

           try
           {
               _total = _total - itemForRemove.Price * itemForRemove.Amount;
               tblTotalValue.Text = _total.ToString() + " " + _currency;
               tblTotalValue.Foreground = System.Windows.Media.Brushes.Red;
               tblTotalValue.FontWeight = FontWeights.Bold;


             

              

               for (int i = 0; i < itemForRemove.UsedStoreItem.Count; i++)
               {
                   string id = "29";//Queries.xml ID
                   XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                   XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                     where xml2.Element("ID").Value == id
                                     select xml2).FirstOrDefault();
                   Console.WriteLine(Query.ToString());
                   string query = Query.Attribute(Constants.TEXT).Value;
                   query = query + "'" + itemForRemove.NumOfCount + "'" + "AND StoreItemUsedCode = " + "'" + itemForRemove.UsedStoreItem.ElementAt(i).CodeProduct + "'" + ";";

                   conHistory.Open();
                   com = new OleDbCommand(query, conHistory);
                   drSum = com.ExecuteReader();
                   int usedPiecesForOneItem = 0;

                   while (drSum.Read())
                   {
                       int n;
                       bool isNumm2 = int.TryParse(drSum["PiecesUsedOfItem"].ToString(), out n);
                       usedPiecesForOneItem = usedPiecesForOneItem + n;
                   }


                   


                   id = "26";//Queries.xml ID
                   xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                   Query = (from xml2 in xdocStore.Descendants("Query")
                                     where xml2.Element("ID").Value == id
                                     select xml2).FirstOrDefault();
                   Console.WriteLine(Query.ToString());
                   query = Query.Attribute(Constants.TEXT).Value;
                   query = query + "'" + itemForRemove.NumOfCount + "'" + "AND StoreItemUsedCode = " + "'" + itemForRemove.UsedStoreItem.ElementAt(i).CodeProduct + "'" + ";";
                   com = new OleDbCommand(query, conHistory);
                   dr = com.ExecuteReader();
                   double amountForProduct = 0.0;

                   if (dr.Read())
                   {
                       string amountWithPoint = dr["AmountOfStoreItemUsed"].ToString().Replace(',', '.');
                       bool isN = Double.TryParse(amountWithPoint, NumberStyles.Any, CultureInfo.InvariantCulture, out amountForProduct);
                   }

                    

                       //update historyItemsOutput

                       query = "SELECT NumberOfUpdates FROM HistoryItemsOutput WHERE StoreItemUsedCode = " + "'" + si.CodeProduct + "'" + "AND NumberOfItem = " + "'" + itemForRemove.NumOfCount + "'" + ";";
                           com = new OleDbCommand(query, conHistory);
                           drStore = com.ExecuteReader();
                           int oldUpNum2 = 0;
                           while (drStore.Read())
                           {
                               bool isNumn = int.TryParse(dr["NumberOfUpdates"].ToString(), out oldUpNum2);
                           }


                           int upNum2 = oldUpNum2 + 1;
                           query = "UPDATE HistoryItemsOutput SET NumberOfUpdates = " + "'" + upNum2.ToString() + "'" + "WHERE StoreItemUsedCode =" + "'" + si.CodeProduct + "'" + "AND NumberOfItem = " + "'" + itemForRemove.NumOfCount + "'" + ";";
                           com = new OleDbCommand(query, conHistory);
                           com.ExecuteNonQuery();


                   

            /*   string numberOfSoldItem = String.Empty;

               string storeItemUsedCode = String.Empty;
               List<double> prodAmL = new List<double>();
               string prodAmStrWithPoint = String.Empty;
               List<int> numOfSoldL = new List<int>();
               string numOfSoldStrWithoutPoint = String.Empty;

               List<string> codesStore = new List<string>();
               List<List<int>> numberofSoldItems = new List<List<int>>();*/

                           bool existInStorehouse = false;

                       //update storehouse
                        StorehouseItem storeI = new StorehouseItem();
                        for (int w = 0; w < storehouse.StorehouseItems.Count; w++)
                        {
                            if (storehouse.StorehouseItems.ElementAt(w).ItemCode.Equals(itemForRemove.UsedStoreItem.ElementAt(i).CodeProduct) == true)
                            {
                                existInStorehouse = true;
                                storehouse.StorehouseItems.ElementAt(w).ItemRealAmount = storehouse.StorehouseItems.ElementAt(w).ItemRealAmount + (usedPiecesForOneItem * amountForProduct);//change real amount of store item
                                
                                storehouse.StorehouseItems.ElementAt(w).ItemPrice = storehouse.StorehouseItems.ElementAt(w).ItemRealAmount / storehouse.StorehouseItems.ElementAt(w).ItemforOneAmount * storehouse.StorehouseItems.ElementAt(w).ItemforOnePrice;
                                storehouse.cvStorehouseItems = CollectionViewSource.GetDefaultView(storehouse.StorehouseItems);
                                if (storehouse.cvStorehouseItems != null)
                                {
                                    storehouse.dgridStateOfStorehouse.ItemsSource = storehouse.cvStorehouseItems;
                                }
                                storeI = storehouse.StorehouseItems.ElementAt(w);

                                //refresh if store item selected in storehouse window
                                if (storehouse.tf1.Text.Equals(storehouse.StorehouseItems.ElementAt(w).ItemCode) == true)
                                {
                                    storehouse.tf4.Text = storehouse.StorehouseItems.ElementAt(w).ItemRealAmount.ToString();
                                    double newRealPrice = storehouse.StorehouseItems.ElementAt(w).ItemRealAmount / storehouse.StorehouseItems.ElementAt(w).ItemforOneAmount * storehouse.StorehouseItems.ElementAt(w).ItemforOnePrice;
                                    storehouse.tf5.Text = newRealPrice.ToString();
                                }
                            }


                        }
                        StorehouseItem storeItemMain = new StorehouseItem();
                        if (existInStorehouse == false) 
                        {

                            storeItemMain.ItemCode = itemForRemove.UsedStoreItem.ElementAt(i).CodeProduct;
                            storeItemMain.ItemName = itemForRemove.UsedStoreItem.ElementAt(i).KindOfProduct;
                            storeItemMain.ItemGroup = itemForRemove.UsedStoreItem.ElementAt(i).Group;
                            storeItemMain.ItemforOneAmount = itemForRemove.UsedStoreItem.ElementAt(i).Amount;
                            storeItemMain.ItemforOnePrice = itemForRemove.UsedStoreItem.ElementAt(i).Price;
                            storeItemMain.ItemRealAmount = usedPiecesForOneItem * amountForProduct;
                            storeItemMain.ItemPrice = storeItemMain.ItemRealAmount / storeItemMain.ItemforOneAmount * storeItemMain.ItemforOnePrice;
                            storeItemMain.Threshold = 0;
                            storehouse.StorehouseItems.Add(storeItemMain);
                            storehouse.cvStorehouseItems = CollectionViewSource.GetDefaultView(storehouse.StorehouseItems);
                            if (storehouse.cvStorehouseItems != null)
                            {
                                storehouse.dgridStateOfStorehouse.ItemsSource = storehouse.cvStorehouseItems;
                            }
      
                        }

                        Logger.writeNode(Constants.INFORMATION, "Tab1 Uklanjanje proizvoda kafica: " + itemForRemove.KindOfProduct + " Uklonjena stavka šanka: " + storeItemMain.ItemName + " Kolicina je : " + storeItemMain.ItemRealAmount);
                        if (existInStorehouse == true)
                        {

                           

                            //update in database


                            conConnProdStore.Open();
                            string queryStorehouse = "UPDATE storehouse SET RealAmount = " + "'" + storeI.ItemRealAmount + "'" + " WHERE StoreItemCode =" + "'" + storeI.ItemCode + "'" + ";";
                            com = new OleDbCommand(queryStorehouse, conConnProdStore);
                            com.ExecuteNonQuery();
                            queryStorehouse = "UPDATE storehouse SET RealPrice = " + "'" + storeI.ItemPrice + "'" + " WHERE StoreItemCode =" + "'" + storeI.ItemCode + "'" + ";";
                            com = new OleDbCommand(queryStorehouse, conConnProdStore);
                            com.ExecuteNonQuery();

                            queryStorehouse = "UPDATE storehouse SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE StoreItemCode =" + "'" + storeI.ItemCode + "'" + ";";
                            com = new OleDbCommand(queryStorehouse, conConnProdStore);
                            com.ExecuteNonQuery();

                            query = "SELECT NumberOfUpdates FROM storehouse WHERE StoreItemCode = " + "'" + storeI.ItemCode + "'" + ";";
                            com = new OleDbCommand(query, conConnProdStore);
                            drStore = com.ExecuteReader();
                            int oldUpNum = 0;
                            while (drStore.Read())
                            {
                                bool isNumn = int.TryParse(dr["NumberOfUpdates"].ToString(), out oldUpNum);
                            }


                            int upNum = oldUpNum + 1;
                            queryStorehouse = "UPDATE storehouse SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE StoreItemCode =" + "'" + storeI.ItemCode + "'" + ";";
                            com = new OleDbCommand(queryStorehouse, conConnProdStore);
                            com.ExecuteNonQuery();






                            if (conConnProdStore != null)
                            {
                                conConnProdStore.Close();
                            }
                        }
                        else // existInStorehouse = false
                        {
                            //insert in storehouse new store item
                            id = "14";//Queries.xml ID
                            conCancelItem.Open();
                            XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                            Query = (from xml2 in xdoc.Descendants("Query")
                                              where xml2.Element("ID").Value == id
                                              select xml2).FirstOrDefault();
                            Console.WriteLine(Query.ToString());
                            query = Query.Attribute(Constants.TEXT).Value;
                            query = query + "(" + "'" + storeItemMain.ItemCode + "'" + "," + "'" + storeItemMain.ItemRealAmount + "'" + "," + "'" + storeItemMain.ItemPrice + "'" + "," + "'" + Currency + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + "0" + "'" + "," + "'" + 0 + "'" + ");";


                            com = new OleDbCommand(query, conCancelItem);
                            com.ExecuteNonQuery();

                        }
                 

                 
                   if (conHistory != null)
                   {
                       conHistory.Close();
                   }
               }// end of for loop

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               Logger.writeNode(Constants.EXCEPTION, ex.Message);
               savenumofitemsEVERCreated();
           }
           finally
           {

               if (conConnProdStore != null)
               {
                   conConnProdStore.Close();
               }
               if (conHistory != null)
               {
                   conHistory.Close();
               }
               if (conCancelItem != null)
               {
                   conCancelItem.Close();
               }
               if (dr != null)
               {
                   dr.Close();
               }
               if (drStore != null)
               {
                   drStore.Close();
               }
               if (drSum != null)
               {
                   drSum.Close();
               }
               if (drInner != null)
               {
                   drInner.Close();
               }
           }

               
       }

       private void deleteItemInAllItemsSoldEver(Item itemForRemove) 
       {
           try
           {
               Logger.writeNode(Constants.INFORMATION, "Tab1 Uklanjanje proizvoda kafica: " + itemForRemove.KindOfProduct + " broj obrisanih komada proizvoda: " + itemForRemove.Amount);

               string id = "28";//Queries.xml ID

               XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
               XElement Query = (from xml2 in xdoc.Descendants("Query")
                                 where xml2.Element("ID").Value == id
                                 select xml2).FirstOrDefault();
               Console.WriteLine(Query.ToString());
               string query = Query.Attribute(Constants.TEXT).Value;
               query = query + "'" + itemForRemove.NumOfCount + "'" + ";";

               con.Open();
               com = new OleDbCommand(query, con);
               com.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               Logger.writeNode(Constants.EXCEPTION, ex.Message);
               savenumofitemsEVERCreated();

           }
           finally
           {
               if (con != null)
               {
                   con.Close();
               }
           }
       }



       private void tfWhyItemDeleted_MouseEnter(object sender, MouseEventArgs e)
       {
           if (tfWhyItemDeleted.Text.Equals(Constants.tfWhyItemDeleted_INITIALTEXT) == true)
           {
               tfWhyItemDeleted.Text = String.Empty;
           }
       }

       private void tfWhyItemDeleted_MouseLeave(object sender, MouseEventArgs e)
       {
           if (tfWhyItemDeleted.Text.Equals(String.Empty) == true)
           {
               tfWhyItemDeleted.Text = Constants.tfWhyItemDeleted_INITIALTEXT;
           }
       }


       private void writeItemInAllItemsDeletedEver(Item item)
       {


           try
           {
               Logger.writeNode(Constants.INFORMATION, "Upis u tabelu ikada obrisanih proizvoda kafica. Upisan je proizvod " + item.KindOfProduct);
               string id = "30";//Queries.xml ID

               XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
               XElement Query = (from xml2 in xdoc.Descendants("Query")
                                 where xml2.Element("ID").Value == id
                                 select xml2).FirstOrDefault();
               Console.WriteLine(Query.ToString());
               string query = Query.Attribute(Constants.TEXT).Value;
               query = query + "(" + "'" + _numOfEverCreatedItem.ToString() + "'" + "," + "'" + item.CodeProduct + "'" + "," + "'" + item.KindOfProduct + "'" + "," + "'" + item.Price + "'" + "," + "'" + item.Amount + "'" + "," + "'" + item.CostItem + "'" + "," + "'" + item.Shift + "'" + "," + "'" + _dateCreatedReport + "'" + "," + "'" + DateTime.Now + "'" + "," + "'" + tfWhyItemDeleted.Text + "'" + ");";

               con.Open();
               com = new OleDbCommand(query, con);
               com.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               Logger.writeNode(Constants.EXCEPTION, ex.Message);
               savenumofitemsEVERCreated();
           }
           finally
           {
               if (con != null)
               {
                   con.Close();
               }
           }
       }    

       private void Click_btnremoveItemTab1(object sender, RoutedEventArgs e)
       {
          

           if (tfWhyItemDeleted.Text.Equals(Constants.tfWhyItemDeleted_INITIALTEXT) == true)
           {
               MessageBox.Show("Morate uneti razlog uklanjanja stavke!!!", "NEPOSTOJANJE RAZLOGA UKLANJANJA");
               Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti razlog uklanjanja stavke!!!");
               return;
           }

           Item itemForRemove = new Item(); ;
           if (dataGrid1.SelectedIndex == -1)
           {
               MessageBox.Show("Morate selektovati barem jednu stavku da bi ste je uklonili.");
               Logger.writeNode(Constants.MESSAGEBOX, "Morate selektovati barem jednu stavku da bi ste je uklonili.");
               return;
           }


           if (dataGrid1.SelectedIndex >= 0)
           {
               int selectedItemsCount = dataGrid1.SelectedItems.Count;
               for (int i = 0; i < selectedItemsCount; i++)
               {
                   int ind = dataGrid1.SelectedIndex;
                   itemForRemove = _items.ElementAt(ind);
                   updateStoreHouseWithDeletedItem(itemForRemove);
                   deleteItemInAllItemsSoldEver(itemForRemove);//delete from allItemsSoldEver
                   writeItemInAllItemsDeletedEver(itemForRemove);
                   _items.RemoveAt(ind);
                   numberOfEnteredProduct--;
               };

               //ovo izvlacenje iz for petlje je pravilo gresku prilkom visestrukog brisanja
               dataGrid1.ItemsSource = _items;
               autoscrollToBottom(dataGrid1);
           }

          
          

           if (dataGrid1.Items.Count == 0)
           {
               btncreateReport.IsEnabled = false;
           }

           checkRemarkDownTab1();

           //update _itemsDeleted (deletion collection) 
           string dateCreated = _dateCreatedReport.ToString().Replace("0:00:00", "");
           ItemWithDate it = new ItemWithDate(itemForRemove, dateCreated);
           ItemWithDateDeletion itDel = new ItemWithDateDeletion(it, tfWhyItemDeleted.Text);
           Logger.writeNode(Constants.INFORMATION, "Tab1 Klik na dugme Ukloni stavku. Proizvod kafica koji se uklanja " + itemForRemove.KindOfProduct + " Broj komada koji se uklanja je " + itemForRemove.Amount);
           MainWindow window = (MainWindow)System.Windows.Window.GetWindow(this);

           for (DateTime x = window.createdReports.DateCreatedReportStartTab3; x <= window.createdReports.DateCreatedReportEndTab3; x = x.AddDays(1))
           {
               string dateCurrStr = x.ToString().Replace("0:00:00", "");
               if (dateCurrStr.Equals(it.Date.ToString()) == true)
               {
                   window.createdReports.ItemsDeleted.Add(itDel);

                   window.createdReports.dataGridReadDeletion.ItemsSource = window.createdReports.ItemsDeleted;

                   break;
               }
           }

           tfWhyItemDeleted.Text = String.Empty;
       }

       private void TextChanged_tfAmount(object sender, TextChangedEventArgs e)
       {
           tblRemarkTab1.Text = String.Empty;
           string[] valueItem = tblPriceTab1.Text.Split();
           int valueItemNum;
           bool isNumeric = int.TryParse(valueItem[0], out valueItemNum);
           string amount = tfAmount.Text;
           int amountNum;
           bool isNumericAmount = int.TryParse(amount, out amountNum);
           int multiplication = valueItemNum * amountNum;
           tblItemValueNumber.Text = multiplication.ToString() + " " + _currency;
       }

       private void MouseEnter_tfAmount(object sender, MouseEventArgs e)
       {
           if (cmbNameProductTab1.SelectedIndex == 0)
           {
               tblRemarkTab1.Text = Constants.REMARKPRODUCT;
           }
       }

       private void MouseLeave_tfAmount(object sender, MouseEventArgs e)
       {
           string locamount = tfAmount.Text;
           int n;
           bool isNumericloc = int.TryParse(locamount, out n);
           if (isNumericloc == false && locamount.Equals(String.Empty) == false)
           {
               tblRemarkTab1.Text = Constants.REMARKAMOUNTNOTNUMERIC;
           }
           else
           {
               string[] valueItem = tblPriceTab1.Text.Split();
               int valueItemNum;
               bool isNumeric = int.TryParse(valueItem[0], out valueItemNum);
               string amount = tfAmount.Text;
               int amountNum;
               bool isNumericAmount = int.TryParse(amount, out amountNum);
               int multiplication = valueItemNum * amountNum;
               tblItemValueNumber.Text = multiplication.ToString() + " " + _currency;
               tblRemarkTab1.Text = String.Empty;
           }
       }

       private string fileNameForDropBox = string.Empty;

       private string LoadPathOfCreatingReport()
       {
           
           string res = String.Empty;

           if (options.tblDir2.Text.Equals(Constants.DEFAULTOPTION) == true)
           {
               res = res + Constants.DEFAULTDIRECTORIUM + @"\";
           }
           else
           {
               res = res + options.tblDir2.Text + @"\";
           }


           if (options.tblFile2.Text.Equals(Constants.DEFAULTOPTION) == true)
           {
               DateTime date = DateTime.Parse(_currDate);
               res = res + Constants.DEFAULTREPORT + date.ToString("yyyyMMdd");
               fileNameForDropBox += Constants.DEFAULTREPORT + date.ToString("yyyyMMdd");
            }
           else 
           {
               res = res + options.tblFile2.Text;
               fileNameForDropBox += options.tblFile2.Text;
            }

           res = res + ".";

           if (options.tblExtension2.Text.Equals(Constants.DEFAULTOPTION) == true)
           {
               res = res + Constants.DEFAULTEXTENSIONOFCREATEDREPORT.ToLower();
               fileNameForDropBox += Constants.DEFAULTEXTENSIONOFCREATEDREPORT.ToLower();
            }
           else
           {
               res = res + options.tblExtension2.Text.ToLower();
               fileNameForDropBox += options.tblExtension2.Text.ToLower();
            }

           Logger.writeNode(Constants.INFORMATION, "Tab1 Ucitavanje putanje u kojoj ce se naci izvestaj prometa. Putanja kreiranog fajla je " + res);

           return res;
       }


       private bool checkSuppliesForYesterday(DateTime date, out ObservableCollection<StorehouseItemState> storeIts) 
       {
          
          

           try
           {
               storeIts = new ObservableCollection<StorehouseItemState>();
               string idCount = "55";//Queries.xml ID

               XDocument xdocCount = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
               XElement QueryCount = (from xml2 in xdocCount.Descendants("Query")
                                      where xml2.Element("ID").Value == idCount
                                      select xml2).FirstOrDefault();

               string query = QueryCount.Attribute(Constants.TEXT).Value;
               string storeMeasure = String.Empty;


               con.Open();
               com = new OleDbCommand(query, con);
               com.Parameters.Add("@Date", date.ToShortDateString());
               dr = com.ExecuteReader();
               string storeItemCode;
               string storeItemName;
               string storeItemGroup;
               string storeItemForOnePrice;
               string storeItemforOneAmount;
               string realAmount;
               string stateOfEndDateTime;
               DateTime StateOfEndDateTime;
              

               while (dr.Read())
               {
                   //StorehouseItemState storeI = new StorehouseItemState();
                   storeItemCode = dr["StoreItemCode"].ToString();
                   storeItemName = dr["StoreItemName"].ToString();
                   storeItemGroup = dr["StoreItemGroup"].ToString();
                   storeItemForOnePrice = dr["StoreItemForOnePrice"].ToString();
                   storeItemforOneAmount = dr["StoreItemforOneAmount"].ToString();
                   realAmount = dr["RealAmount"].ToString();
                   stateOfEndDateTime = dr["StateOfEndDateTime"].ToString();
                   StateOfEndDateTime = DateTime.Parse(stateOfEndDateTime);

                   int priceForOne;
                   bool isN = int.TryParse(storeItemForOnePrice, out priceForOne);
                   double amountForOne;
                   string storeItemforOneAmountWithPoint = storeItemforOneAmount.Replace(',','.');
                   bool isD = Double.TryParse(storeItemforOneAmountWithPoint, out amountForOne);
                   double realAmountDouble;
                   string realAmountWithPoint = realAmount.Replace(',','.');
                   bool isDD = Double.TryParse(realAmountWithPoint, out realAmountDouble);

                   StorehouseItem si = new StorehouseItem(storeItemCode, storeItemName, storeItemGroup, priceForOne, amountForOne, realAmountDouble);
                   StorehouseItemState storeItState = new StorehouseItemState(si, StateOfEndDateTime.ToShortDateString());
                   storeIts.Add(storeItState);
                 
               }

               if (storeIts.Count == 0)
               {
                   return false;
               }
               else 
               {
                   return true;
               }

              

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               Logger.writeNode(Constants.EXCEPTION, ex.Message);
               storeIts = new ObservableCollection<StorehouseItemState>();
               return false;
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



   

       private void notWorkingDay_btnremoveItemTab1()
       {
           // Create a thread
           Thread newWindowThread = new Thread(new ThreadStart(() =>
           {
               // Create and show the Window
               Timer tempWindow = new Timer();
               tempWindow.setDeletionText();
               tempWindow.Show();
               // Start the Dispatcher Processing
               System.Windows.Threading.Dispatcher.Run();
           }));
           // Set the apartment state
           newWindowThread.SetApartmentState(ApartmentState.STA);
           // Make the thread a background thread
           newWindowThread.IsBackground = true;
           // Start the thread
           newWindowThread.Start();


           tfWhyItemDeleted.Text = "neradan dan";

           if (tfWhyItemDeleted.Text.Equals(Constants.tfWhyItemDeleted_INITIALTEXT) == true)
           {
               MessageBox.Show("Morate uneti razlog uklanjanja stavke!!!", "NEPOSTOJANJE RAZLOGA UKLANJANJA");
               Logger.writeNode(Constants.MESSAGEBOX, "Morate uneti razlog uklanjanja stavke!!!");
               return;
           }

           Item itemForRemove = new Item(); ;
          /* if (dataGrid1.SelectedIndex == -1)
           {
               MessageBox.Show("Morate selektovati barem jednu stavku da bi ste je uklonili.");
               Logger.writeNode(Constants.MESSAGEBOX, "Morate selektovati barem jednu stavku da bi ste je uklonili.");
               return;
           }*/


           //if (dataGrid1.SelectedIndex >= 0)
           //{
           //int selectedItemsCount = dataGrid1.SelectedItems.Count;
           int ItemsCount = dataGrid1.Items.Count;
           for (int i = 0; i < ItemsCount/*dataGrid1.SelectedItems.Count*/; i++)
           {
               //int ind = dataGrid1.SelectedIndex;
               //itemForRemove = _items.ElementAt(i);
               itemForRemove = _items.ElementAt(0);
               updateStoreHouseWithDeletedItem(itemForRemove);
               deleteItemInAllItemsSoldEver(itemForRemove);//delete from allItemsSoldEver
               writeItemInAllItemsDeletedEver(itemForRemove);
               //_items.RemoveAt(i);
               _items.RemoveAt(0);
               numberOfEnteredProduct--;
           };
           // }
           //ovo izvlacenje iz for petlje je pravilo gresku prilkom visestrukog brisanja
           dataGrid1.ItemsSource = _items;
           autoscrollToBottom(dataGrid1);



           if (dataGrid1.Items.Count == 0)
           {
               btncreateReport.IsEnabled = false;
           }

           //checkRemarkDownTab1();

           //update _itemsDeleted (deletion collection) 
           string dateCreated = _dateCreatedReport.ToString().Replace("0:00:00", "");
           ItemWithDate it = new ItemWithDate(itemForRemove, dateCreated);
           ItemWithDateDeletion itDel = new ItemWithDateDeletion(it, tfWhyItemDeleted.Text);
           Logger.writeNode(Constants.INFORMATION, "Tab1 Klik na dugme Ukloni stavku. Proizvod kafica koji se uklanja " + itemForRemove.KindOfProduct + " Broj komada koji se uklanja je " + itemForRemove.Amount);
           MainWindow window = (MainWindow)System.Windows.Window.GetWindow(this);

           for (DateTime x = window.createdReports.DateCreatedReportStartTab3; x <= window.createdReports.DateCreatedReportEndTab3; x = x.AddDays(1))
           {
               string dateCurrStr = x.ToString().Replace("0:00:00", "");
               if (dateCurrStr.Equals(it.Date.ToString()) == true)
               {
                   window.createdReports.ItemsDeleted.Add(itDel);

                   window.createdReports.dataGridReadDeletion.ItemsSource = window.createdReports.ItemsDeleted;

                   break;
               }
           }

           tfWhyItemDeleted.Text = String.Empty;
           ordinalNumbers.Clear();//ocisti prvu kolonu 1 kolona
           numberOfEnteredProduct = 0;

           newWindowThread.Abort();
       }


         
            private void Click_btncreateReport(object sender, RoutedEventArgs e)
            {

           
                try
                {
                    string mailsenderPath = Properties.Settings.Default.MailSenderPath;
                    //List<string> mailSenderActive = File.ReadAllLines(@"D:\MailSenderKambodza\mailSenderActive.txt").ToList();
                    
                    List<string> mailSenderActive = File.ReadAllLines(mailsenderPath).ToList();
                    if (mailSenderActive[0].Equals("True"))
                    {
                        MessageBox.Show("Pošiljalac maila je aktivan!" + System.Environment.NewLine + " Probajte za dvadeset sekundi ponovo !");
                        return;
                    }

                    chbNotWorkingDay.IsEnabled = false;
                    int ordNum;
                    //making ordinal numbers
                    for (int i = 0; i < numberOfEnteredProduct; i++)
                    {
                        ordNum = i + 1;
                        ordinalNumbers.Add(ordNum.ToString());
                    }

                    tblisCreatingReportTime.Text = Constants.REMARKPROGRESSBAR;
                    tblisCreatingReportTime.FontWeight = FontWeights.Bold;
                    tblisCreatingReportTime.Foreground = System.Windows.Media.Brushes.Green;

                    //first check is enter todaybought storeitems for that day
                    if (IsEnteredMoreBuyedStoreItems == false)
                    {
                        MessageBox.Show("Morate prvo kliknuti na dugme za završen unos [Tab 3 Podtab1 Unos podataka u šank]!!! ");

                        //reset progress bar label
                        tblisCreatingReportTime.Text = Constants.REMARKPROGRESSBAREND;
                        tblisCreatingReportTime.FontWeight = FontWeights.Normal;
                        tblisCreatingReportTime.Foreground = System.Windows.Media.Brushes.Black;

                        return;
                    }


                    if (numberOfEnteredProduct < _products.Count && chbNotWorkingDay.IsChecked == false)
                    {
                        MessageBox.Show("Niste uneli sve stavke u knjigu šanka");
                        MessageBox.Show("U knjigu šanka uneli ste " + numberOfEnteredProduct + ". A trebate uneti " + _products.Count + " proizvoda u dnevni izveštaj knjige šanka");

                        //reset progress bar label
                        tblisCreatingReportTime.Text = Constants.REMARKPROGRESSBAREND;
                        tblisCreatingReportTime.FontWeight = FontWeights.Normal;
                        tblisCreatingReportTime.Foreground = System.Windows.Media.Brushes.Black;

                        return;
                    }

                    DateTime selectedDateForCreateBarBook = _dateCreatedReport;
                    DateTime forcheckLastCreateBarBook = selectedDateForCreateBarBook.AddDays(-1);
                    if (forcheckLastCreateBarBook.ToShortDateString().Equals(_dateOfLastCreatedBarBook.ToShortDateString()) == false)
                    {
                        MessageBox.Show("Ne možete za izabrani datum " + selectedDateForCreateBarBook.ToShortDateString() + " da kreirate knjigu šanka!!");
                        MessageBox.Show("Zadnji datum za koji ste kreirali knjigu šanka je :" + _dateOfLastCreatedBarBook.ToShortDateString());

                        //reset progress bar label
                        tblisCreatingReportTime.Text = Constants.REMARKPROGRESSBAREND;
                        tblisCreatingReportTime.FontWeight = FontWeights.Normal;
                        tblisCreatingReportTime.Foreground = System.Windows.Media.Brushes.Black;

                        return;
                    }

                    ObservableCollection<StorehouseItemState> storeItYesterdaySupplies = new ObservableCollection<StorehouseItemState>();

                    bool checkSupplies = checkSuppliesForYesterday(forcheckLastCreateBarBook, out storeItYesterdaySupplies);
                    if (checkSupplies == false && chbNotWorkingDay.IsChecked == false)
                    {
                        MessageBox.Show("Nemate jučerašnjih zaliha. Izveštaj neće biti kreiran!");

                        //reset progress bar label
                        tblisCreatingReportTime.Text = Constants.REMARKPROGRESSBAREND;
                        tblisCreatingReportTime.FontWeight = FontWeights.Normal;
                        tblisCreatingReportTime.Foreground = System.Windows.Media.Brushes.Black;


                        return;
                    }


                  
                    //get first column (yesterday supplies)
                    for (int i = 0; i < enterStoreItemsTab2.StoreItemProducts.Count; i++)
                    {
                        //MessageBox.Show(enterStoreItemsTab2.StoreItemProducts.ElementAt(i).KindOfProduct + "   " + enterStoreItemsTab2.StoreItemProducts.ElementAt(i).RealAmount);
                        for (int j = 0; j < storeItYesterdaySupplies.Count; j++)
                        {
                            if (enterStoreItemsTab2.StoreItemProducts.ElementAt(i).CodeProduct.Equals(storeItYesterdaySupplies.ElementAt(j).ItemCode) == true)
                            {
                                enterStoreItemsTab2.StoreItemProducts.ElementAt(i).RealAmount = storeItYesterdaySupplies.ElementAt(j).ItemRealAmount;
                                break;
                            }
                        }
                    }


                    MainWindow window = (MainWindow)System.Windows.Window.GetWindow(this);

                    if (chbNotWorkingDay.IsChecked == true)
                    {

                        //delete all entered items
                        notWorkingDay_btnremoveItemTab1();

                        storehouse.btnReturnOneDay.IsEnabled = true;

                        //return all daily bought enter
                        for (int i = 0; i < window.enterStoreItemsTab2.StoreItemProducts.Count; i++)
                        {
                            StoreItemProduct sip = new StoreItemProduct();
                            sip.CodeProduct = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).CodeProduct;
                            sip.Amount = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Amount;
                            sip.Group = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Group;
                            sip.isUsed = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).isUsed;
                            sip.KindOfProduct = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).KindOfProduct;
                            sip.Measure = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Measure;
                            sip.Price = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Price;
                            sip.RealAmount = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).RealAmount;
                            sip.Threshold = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Threshold;
                            storehouse.DailyStoreItem.Add(sip);
                        }

                        storehouse.dgridDailyEnterInStorehouse.ItemsSource = storehouse.DailyStoreItem;
                        storehouse.dgridDailyEnterInStorehouse.Foreground = System.Windows.Media.Brushes.Black;
                        storehouse.btnFinish.IsEnabled = true;



                        //save state of storehouse for that day
                        for (int i = 0; i < window.storehouse.StorehouseItems.Count; i++)
                        {
                            StorehouseItemState sItemState = new StorehouseItemState(window.storehouse.StorehouseItems.ElementAt(i), datepicker1.SelectedDate.Value.ToShortDateString());
                            insertRecordInstatesStoreOnEndDay(sItemState);
                        }

                        if (_total > 0)
                        {
                            _total = 0;
                            tblTotalValue.Text = _total.ToString() + " " + Currency;
                            if (_items.Count > 0)
                            {
                                _items.Clear();
                            }
                        }

                        MessageBox.Show("Izabrani datum " + selectedDateForCreateBarBook.ToShortDateString() + " ste uneli u sistem kao neradan! ");
                        chbNotWorkingDay.IsChecked = false;
                        // this can only
                        // DateTime currentDateTime = (DateTime)this.datepicker1.SelectedDate;
                        // _dateOfLastCreatedBarBook = currentDateTime;
                        DateTime currentDateTime = (DateTime)this.datepicker1.SelectedDate;
                        _currDate = currentDateTime.ToShortDateString();
                        _dateOfLastCreatedBarBook = DateTime.Parse(_currDate);
                        updateDateOfLastCreatedBarBook(_dateOfLastCreatedBarBook);


                        //then set date in first tab
                        _dateOfLastCreatedBarBook = (DateTime)this.datepicker1.SelectedDate;
                        DateTime oldDate = (DateTime)this.datepicker1.SelectedDate;
                        this.datepicker1.SelectedDate = oldDate.AddDays(1);
                        cmbNameProductTab1.SelectedIndex = 0;
                        cmbNameProductTab1.IsEnabled = false;

                        //reset progress bar label
                        tblisCreatingReportTime.Text = Constants.REMARKPROGRESSBAREND;
                        tblisCreatingReportTime.FontWeight = FontWeights.Normal;
                        tblisCreatingReportTime.Foreground = System.Windows.Media.Brushes.Black;

                        reportNotYetCreated = false;
                        return;


                    }

                    bool isCreated;


                    if (options.cmbAppSound.IsChecked == true)
                    {
                        player.Play();
                    }
                    // Create a thread
                    Thread newWindowThread = new Thread(new ThreadStart(() =>
                    {
                        // Create and show the Window
                        Timer tempWindow = new Timer();
                        tempWindow.Show();
                        // Start the Dispatcher Processing
                        System.Windows.Threading.Dispatcher.Run();
                    }));
                    // Set the apartment state
                    newWindowThread.SetApartmentState(ApartmentState.STA);
                    // Make the thread a background thread
                    newWindowThread.IsBackground = true;
                    // Start the thread
                    newWindowThread.Start();

                    System.Drawing.Color headerBackColor = System.Drawing.Color.LightGray;
                    System.Drawing.Color enterBackColor = System.Drawing.Color.Yellow;

                    string pathOfCreatingReport = String.Empty;
                    pathOfCreatingReport = LoadPathOfCreatingReport();

                    
                    ExcelFile report = new ExcelFile(pathOfCreatingReport);
                    if (options.rbtnPortrait.IsChecked == true)
                    {
                        isCreated = report.createFile(11, 15, 'P');
                        if (isCreated == false)
                        {
                            MessageBox.Show("Izveštaj neće biti kreiran!! Molimo vas da promenite putanju fajla koji niste želeli obrisati ili promenite u Opcijama putanju izveštaja koji želite da kreirate!!!");
                            Logger.writeNode(Constants.MESSAGEBOX, "Izveštaj neće biti kreiran!! Molimo vas da promenite putanju fajla koji niste želeli obrisati ili promenite u Opcijama putanju izveštaja koji želite da kreirate!!!");
                            newWindowThread.Abort();
                            tblisCreatingReportTime.Text = Constants.REMARKPROGRESSBAREND;
                            tblisCreatingReportTime.Foreground = System.Windows.Media.Brushes.Black;
                            report.closeFile();
                            return;
                        }
                    }
                    if (options.rbtnLandscape.IsChecked == true)
                    {
                        isCreated = report.createFile(11, 15, 'L');
                        if (isCreated == false)
                        {
                            MessageBox.Show("Izveštaj neće biti kreiran!! Molimo vas da promenite putanju fajla koji niste želeli obrisati ili promenite u Opcijama putanju izveštaja koji želite da kreirate!!!");
                            Logger.writeNode(Constants.MESSAGEBOX, "Izveštaj neće biti kreiran!! Molimo vas da promenite putanju fajla koji niste želeli obrisati ili promenite u Opcijama putanju izveštaja koji želite da kreirate!!!");
                            newWindowThread.Abort();
                            tblisCreatingReportTime.Text = Constants.REMARKPROGRESSBAREND;
                            tblisCreatingReportTime.Foreground = System.Windows.Media.Brushes.Black;
                            report.closeFile();
                            return;
                        }
                    }



                    //if (Directory.Exists(@"D:\MailSenderKambodza") == false)
                    //{
                    //    MessageBox.Show("Direktorijum MailSenderKambodza na D disku ne postoji! Morate na disku D da kreirate direktorijum sa nazivom MailSenderKambodza.");
                    //}
                    //else
                    //{


                    //    List<string> reportforMail = new List<string>();
                    //    reportforMail.Add(pathOfCreatingReport);
                    //    if (File.Exists(@"D:\MailSenderKambodza\mailings.txt") == false)
                    //    {
                    //        File.WriteAllLines(@"D:\MailSenderKambodza\mailings.txt", reportforMail.ToArray());
                    //    }
                    //    else
                    //    {
                    //        File.AppendAllLines(@"D:\MailSenderKambodza\mailings.txt", reportforMail.ToArray());
                    //    }
                    //}

                    btnaddItem.IsEnabled = false;
                    List<string> CodeProductArrayList = new List<string>();
                    List<string> KindOfProductArrayList = new List<string>();
                    List<string> PriceArrayList = new List<string>();
                    List<string> AmountArrayList = new List<string>();
                    List<string> CostItemArrayList = new List<string>();
                    List<string> KOMLITList = new List<string>();
                    List<string> prodajnecene = new List<string>();
                    List<string> jucerasnjezalihe = new List<string>();
                    List<string> danaskupljeno = new List<string>();
                    List<string> stanjenakraju = new List<string>();
                    List<string> column7 = new List<string>();


                    /*   if (_isCodeProductWrite)
                       {
                           CodeProductArrayList.Add(Constants.HEADER_CODEPRODUCT);
                       }
                       KindOfProductArrayList.Add(Constants.HEADER_KINDOFPRODUCT);
                       PriceArrayList.Add(Constants.HEADER_PRICE);
                       AmountArrayList.Add(Constants.HEADER_AMOUNT);
                       CostItemArrayList.Add(Constants.HEADER_COSTITEM);*/




                    for (int i = 0; i < _items.Count; i++)
                    {
                        // CodeProductArrayList.Add(_items.ElementAt(i).CodeProduct);
                        KindOfProductArrayList.Add(_items.ElementAt(i).KindOfProduct);//2 kolona
                        KOMLITList.Add(_items.ElementAt(i).Product.WayDisplayBookBar);//3 kolona
                        if (_items.ElementAt(i).Amount == 0)
                        {
                            AmountArrayList.Add(String.Empty);
                        }
                        else
                        {
                            AmountArrayList.Add(_items.ElementAt(i).Amount.ToString());// 8 kolona
                        }

                        if (_items.ElementAt(i).CostItem == 0)
                        {
                            CostItemArrayList.Add(String.Empty);// 9 kolona
                        }
                        else
                        {
                            CostItemArrayList.Add(_items.ElementAt(i).CostItem.ToString());// 9 kolona
                        }

                        for (int j = 0; j < KOMLITList.Count; j++) // 4 kolona
                        {
                            if (KOMLITList.ElementAt(i).Equals(Constants.LIT) == true)
                            {
                                _items.ElementAt(i).Product.Amount = getRecordFromProductsAmounts(_items.ElementAt(i).Product.CodeProduct);

                                double priceByLitar = _items.ElementAt(i).Price * (1 / _items.ElementAt(i).Product.Amount);
                                prodajnecene.Add(priceByLitar.ToString());
                                break;
                            }
                            else if ((KOMLITList.ElementAt(i).Equals(Constants.KOM) == true))
                            {
                                prodajnecene.Add(_items.ElementAt(i).Price.ToString());
                                break;

                            }
                        }

                        //yesterday supplies
                        bool isHave = false;
                        for (int j = 0; j < storeItYesterdaySupplies.Count; j++) //5 kolona
                        {
                            if (KOMLITList.ElementAt(i).Equals(Constants.LIT) == true)
                            {
                                if (storeItYesterdaySupplies.ElementAt(j).ItemCode.Equals(_items.ElementAt(i).Product.StoreItemProducts.ElementAt(0).CodeProduct))
                                {
                                    isHave = true;
                                    jucerasnjezalihe.Add(storeItYesterdaySupplies.ElementAt(j).ItemRealAmount.ToString());
                                    break;
                                }
                            }
                            else if ((KOMLITList.ElementAt(i).Equals(Constants.KOM) == true))
                            {
                                if (storeItYesterdaySupplies.ElementAt(j).ItemCode.Equals(_items.ElementAt(i).Product.StoreItemProducts.ElementAt(0).CodeProduct))
                                {
                                    isHave = true;
                                    _items.ElementAt(i).Product.Amount = getRecordFromProductsAmounts(_items.ElementAt(i).Product.CodeProduct);
                                    double yesterdayKOMs = storeItYesterdaySupplies.ElementAt(j).ItemRealAmount / _items.ElementAt(i).Product.Amount;
                                    jucerasnjezalihe.Add(yesterdayKOMs.ToString());
                                    break;
                                }

                            }
                        }
                        if (isHave == false)
                        {
                            double zero = 0.0;
                            jucerasnjezalihe.Add(zero.ToString());
                        }

                        bool isHave2 = false;
                        // today bought
                        for (int j = 0; j < storehouse.StoreItemBought.Count; j++) // 6 kolona
                        {
                            if (KOMLITList.ElementAt(i).Equals(Constants.LIT) == true)
                            {
                                if (storehouse.StoreItemBought.ElementAt(j).CodeProduct.Equals(_items.ElementAt(i).Product.StoreItemProducts.ElementAt(0).CodeProduct))
                                {
                                    isHave2 = true;
                                    if (storehouse.StoreItemBought.ElementAt(j).RealAmount.ToString().Equals("0"))
                                    {
                                        danaskupljeno.Add(String.Empty);
                                    }
                                    else
                                    {
                                        danaskupljeno.Add(storehouse.StoreItemBought.ElementAt(j).RealAmount.ToString());
                                    }
                                    break;
                                }
                            }
                            else if ((KOMLITList.ElementAt(i).Equals(Constants.KOM) == true))
                            {

                                if (storehouse.StoreItemBought.ElementAt(j).CodeProduct.Equals(_items.ElementAt(i).Product.StoreItemProducts.ElementAt(0).CodeProduct))
                                {
                                    isHave2 = true;
                                    _items.ElementAt(i).Product.Amount = getRecordFromProductsAmounts(_items.ElementAt(i).Product.CodeProduct);
                                    double todayboughtKOMs = storehouse.StoreItemBought.ElementAt(j).RealAmount / _items.ElementAt(i).Product.Amount;
                                    if (todayboughtKOMs == 0.0)
                                    {
                                        danaskupljeno.Add(String.Empty);
                                    }
                                    else
                                    {
                                        danaskupljeno.Add(todayboughtKOMs.ToString());
                                    }
                                    break;
                                }
                            }
                        }


                        if (isHave2 == false)
                        {

                            danaskupljeno.Add(String.Empty);
                        }




                        // 7 kolona sum of kolona 6 i kolona 5
                        double col5 = 0.0;
                        double col6 = 0.0;
                        string suppliesWithPoint = jucerasnjezalihe.Last().Replace(',', '.');
                        bool isNN = Double.TryParse(suppliesWithPoint, out col5);
                        string boughtTodayWithPoint = danaskupljeno.Last().Replace(',', '.');
                        bool isNNN = Double.TryParse(boughtTodayWithPoint, out col6);
                        double col7 = col5 + col6;
                        column7.Add(col7.ToString());


                        bool isHave3 = false;
                        // 11 kolona
                        //state of storehouse at the end of day
                        for (int j = 0; j < storehouse.StorehouseItems.Count; j++)
                        {
                            if (KOMLITList.ElementAt(i).Equals(Constants.LIT) == true)
                            {
                                if (storehouse.StorehouseItems.ElementAt(j).ItemCode.Equals(_items.ElementAt(i).Product.StoreItemProducts.ElementAt(0).CodeProduct))
                                {
                                    isHave3 = true;
                                    stanjenakraju.Add(storehouse.StorehouseItems.ElementAt(j).ItemRealAmount.ToString());
                                    break;
                                }
                            }
                            else if ((KOMLITList.ElementAt(i).Equals(Constants.KOM) == true))
                            {
                                if (storehouse.StorehouseItems.ElementAt(j).ItemCode.Equals(_items.ElementAt(i).Product.StoreItemProducts.ElementAt(0).CodeProduct))
                                {
                                    isHave3 = true;
                                    _items.ElementAt(i).Product.Amount = getRecordFromProductsAmounts(_items.ElementAt(i).Product.CodeProduct);
                                    double stateStoreEndDayKOMs = storehouse.StorehouseItems.ElementAt(j).ItemRealAmount / _items.ElementAt(i).Product.Amount;
                                    stanjenakraju.Add(stateStoreEndDayKOMs.ToString());
                                    break;
                                }

                            }
                        }

                        if (isHave3 == false)
                        {
                            double zero = 0.0;
                            stanjenakraju.Add(zero.ToString());
                        }




                    } // end of create bar book data

                    //reset today storeItemBought collection 
                    for (int ii = 0; ii < this.enterStoreItemsTab2.StoreItemProducts.Count; ii++)
                    {
                        this.storehouse.StoreItemBought.ElementAt(ii).RealAmount = 0.0;

                    }

                    //write header
                    string redBroj = "Red.\nBroj";
                    report.writeCell(11, excelNumbers("B"), redBroj, false, 12);
                    string nazivRobe = "NAZIV ROBE";
                    report.writeCell(11, excelNumbers("C"), nazivRobe, false, 12);
                    //write header vertical orientation 90 degree fontsize 10 set in optional parameter
                    string jedMere = "jedinica" + System.Environment.NewLine + "mere";
                    report.writeCell(11, excelNumbers("F"), jedMere, false);
                    string prodCena = "prodajna" + System.Environment.NewLine + "cena";
                    report.writeCell(11, excelNumbers("G"), prodCena, false);
                    string stanjeizPrethodnogDana = "stanje iz" + System.Environment.NewLine + "prethodnog" + System.Environment.NewLine + "dana";
                    report.writeCell(11, excelNumbers("H"), stanjeizPrethodnogDana, false);
                    string primljeno = "primljeno" + System.Environment.NewLine + "u toku" + System.Environment.NewLine + "dana";
                    report.writeCell(11, excelNumbers("I"), primljeno, false);
                    string ukupno = "ukupno\n(5+6)";
                    report.writeCell(11, excelNumbers("J"), ukupno, false);
                    string utroseno = "UTROŠENO";
                    report.writeCell(11, excelNumbers("K"), utroseno, false);
                    string kolicina = "količina";
                    report.writeCell(12, excelNumbers("K"), kolicina, false);
                    string vrednost = "vrednost\n(8x4)";
                    report.writeCell(12, excelNumbers("L"), vrednost, false);
                    string razlika = "razlika\n(7-8)";
                    report.writeCell(11, excelNumbers("M"), razlika, false);
                    string zalihe = "zalihe";
                    report.writeCell(11, excelNumbers("N"), zalihe, false);

                    //number of column header
                    string one = "1";
                    report.writeCell(15, excelNumbers("B"), one, false);
                    string two = "2";
                    report.writeCell(15, excelNumbers("C"), two, false);
                    string three = "3";
                    report.writeCell(15, excelNumbers("F"), three, false);
                    string four = "4";
                    report.writeCell(15, excelNumbers("G"), four, false);
                    string five = "5";
                    report.writeCell(15, excelNumbers("H"), five, false);
                    string six = "6";
                    report.writeCell(15, excelNumbers("I"), six, false);
                    string seven = "7";
                    report.writeCell(15, excelNumbers("J"), seven, false);
                    string eight = "8";
                    report.writeCell(15, excelNumbers("K"), eight, false);
                    string nine = "9";
                    report.writeCell(15, excelNumbers("L"), nine, false);
                    string ten = "10";
                    report.writeCell(15, excelNumbers("M"), ten, false);
                    string eleven = "11";
                    report.writeCell(15, excelNumbers("N"), eleven, false);


                    //date header
                    string date = "ZA DAN ";
                    if (_dateCreatedReport.Day.ToString().Equals("1") || _dateCreatedReport.Day.ToString().Equals("2") || _dateCreatedReport.Day.ToString().Equals("3") || _dateCreatedReport.Day.ToString().Equals("4") || _dateCreatedReport.Day.ToString().Equals("5") || _dateCreatedReport.Day.ToString().Equals("6") || _dateCreatedReport.Day.ToString().Equals("7") || _dateCreatedReport.Day.ToString().Equals("8") || _dateCreatedReport.Day.ToString().Equals("9"))
                    {
                        date = date + "0" + _dateCreatedReport.Day;
                    }
                    else
                    {
                        date = date + _dateCreatedReport.Day;
                    }

                    switch (_dateCreatedReport.Month.ToString())
                    {
                        case "1": date = date + ".JANUAR. "; break;
                        case "2": date = date + ".FEBRUAR. "; break;
                        case "3": date = date + ".MART. "; break;
                        case "4": date = date + ".APRIL. "; break;
                        case "5": date = date + ".MAJ. "; break;
                        case "6": date = date + ".JUN. "; break;
                        case "7": date = date + ".JUL. "; break;
                        case "8": date = date + ".AVGUST. "; break;
                        case "9": date = date + ".SEPTEMBAR. "; break;
                        case "10": date = date + ".OKTOBAR. "; break;
                        case "11": date = date + ".NOVEMBAR. "; break;
                        case "12": date = date + ".DECEMBAR. "; break;
                    }//end switch

                    date = date + _dateCreatedReport.Year + ".";
                    report.writeCell(10, excelNumbers("J"), date, false, 13);

                    //set header border
                    report.setBorderArea("B11", "N15", 3, 1);

                    //write header upper part 
                    string naziv = "naziv ugost.organizacije";
                    report.writeCell(4, excelNumbers("B"), naziv, false, 12);
                    string nazivPosJed = "naziv poslovne jedinice";
                    report.writeCell(6, excelNumbers("B"), nazivPosJed, false, 12);
                    string nazivObjekta = "naziv ugost. Objekta";
                    report.writeCell(8, excelNumbers("B"), nazivObjekta, false, 12);
                    string dnevniPromet = "DNEVNI OBRACUN";
                    report.writeCell(5, excelNumbers("H"), dnevniPromet, false, 18);
                    string promet = "PROMETA I ZALIHA ROBE U UGOSTITELJSTVU";
                    report.writeCell(6, excelNumbers("G"), promet, false, 12);

                    for (int i = 0; i < 5; i++) updateProgressBar(i);



                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.Add("Redni broj", typeof(string));
                    dt.Columns.Add("Naziv robe", typeof(string));
                    dt.Columns.Add("Naziv robe2", typeof(string));
                    dt.Columns.Add("Naziv robe3", typeof(string));
                    dt.Columns.Add("Jedinica mere", typeof(string));
                    dt.Columns.Add("Prodajna cena", typeof(string));
                    dt.Columns.Add("Stanje iz predhodnog dana", typeof(string));
                    dt.Columns.Add("Primljeno u toku dana", typeof(string));
                    dt.Columns.Add("Ukupno", typeof(string));
                    dt.Columns.Add("Kolicina", typeof(string));
                    dt.Columns.Add("Vrednost", typeof(string));
                    dt.Columns.Add("Razlika", typeof(string));
                    dt.Columns.Add("Zalihe", typeof(string));

                    int counter = 0;
                    foreach (var item in ordinalNumbers)
                    {
                        DataRow dtRow = dt.NewRow();

                        dtRow["Redni broj"] = ordinalNumbers[counter];
                        dtRow["Naziv robe"] = KindOfProductArrayList[counter];
                        dtRow["Naziv robe2"] = string.Empty;
                        dtRow["Naziv robe3"] = string.Empty;
                        dtRow["Jedinica mere"] = KOMLITList[counter];
                        dtRow["Prodajna cena"] = prodajnecene[counter];
                        dtRow["Stanje iz predhodnog dana"] = jucerasnjezalihe[counter];
                        dtRow["Primljeno u toku dana"] = danaskupljeno[counter];
                        dtRow["Ukupno"] = column7[counter];
                        dtRow["Kolicina"] = AmountArrayList[counter];
                        dtRow["Vrednost"] = CostItemArrayList[counter];
                        dtRow["Razlika"] = string.Empty;
                        dtRow["Zalihe"] = stanjenakraju[counter];

                        counter++;
                        dt.Rows.Add(dtRow);
                    }


                    int dailyReport_RowUnder = 14;
                    int dailyReport_ColumnRight = 1;

                    report.WriteDataTable(dt, dailyReport_RowUnder, dailyReport_ColumnRight);

                    report.setBorderArrayHorizontal(dailyReport_RowUnder + 1, dailyReport_ColumnRight + 1, dailyReport_RowUnder + 1, dailyReport_ColumnRight + dt.Columns.Count, 3);

                    // write bar book in excel file this is without header because items.Count - 1
                    //report.writeArrayVer("B", 16, "B", (16 + _items.Count - 1), ordinalNumbers.ToArray());

                    //reset ordinal numbers
                    ordinalNumbers.Clear();//1 kolona

                    //report.writeArrayVer("C", 16, "C", (16 + _items.Count - 1), KindOfProductArrayList.ToArray());
                    //report.writeArrayVer("F", 16, "F", (16 + _items.Count - 1), KOMLITList.ToArray());
                    for (int i = 5; i < 15; i++) updateProgressBar(i);
                    //report.writeArrayVer("G", 16, "G", (16 + _items.Count - 1), prodajnecene.ToArray());
                    //report.writeArrayVer("H", 16, "H", (16 + _items.Count - 1), jucerasnjezalihe.ToArray());
                    for (int i = 15; i < 30; i++) updateProgressBar(i);
                    //report.writeArrayVer("I", 16, "I", (16 + _items.Count - 1), danaskupljeno.ToArray());
                    //report.writeArrayVer("J", 16, "J", (16 + _items.Count - 1), column7.ToArray());
                    for (int i = 30; i < 45; i++) updateProgressBar(i);
                    //report.writeArrayVer("K", 16, "K", (16 + _items.Count - 1), AmountArrayList.ToArray());
                    //report.writeArrayVer("L", 16, "L", (16 + _items.Count - 1), CostItemArrayList.ToArray());

                    //report.writeArrayVer("N", 16, "N", (16 + _items.Count - 1), stanjenakraju.ToArray());
                    for (int i = 45; i < 60; i++) updateProgressBar(i);


                    //report.setBorderArrayVertical(16, 2, (16 + _items.Count - 1), 2, 3);//A
                    //report.setBorderArrayVertical(16, 3, (16 + _items.Count - 1), 3, 3);//B
                    for (int i = 75; i < 80; i++) updateProgressBar(i);
                    //report.setBorderArrayVertical(16, 4, (16 + _items.Count - 1), 4, 3);//C
                    //report.setBorderArrayVertical(16, 5, (16 + _items.Count - 1), 5, 3);//D
                    for (int i = 80; i < 85; i++) updateProgressBar(i);
                    //report.setBorderArrayVertical(16, 6, (16 + _items.Count - 1), 6, 3);//E
                    //report.setBorderArrayVertical(16, 7, (16 + _items.Count - 1), 7, 3);//F
                    for (int i = 85; i < 90; i++) updateProgressBar(i);
                    //report.setBorderArrayVertical(16, 8, (16 + _items.Count - 1), 8, 3);//G
                    //report.setBorderArrayVertical(16, 9, (16 + _items.Count - 1), 9, 3);//H
                    //report.setBorderArrayVertical(16, 10, (16 + _items.Count - 1), 10, 3);//I
                    for (int i = 90; i < 95; i++) updateProgressBar(i);
                    //report.setBorderArrayVertical(16, 11, (16 + _items.Count - 1), 11, 3);//J
                    //report.setBorderArrayVertical(16, 12, (16 + _items.Count - 1), 12, 3);//K
                    //report.setBorderArrayVertical(16, 13, (16 + _items.Count - 1), 13, 3);//L


                    //report.setBorderArrayVertical(16, 14, (16 + _items.Count - 1), 14, 3);//M
                    report.setBorderArrayHorizontal(dailyReport_RowUnder + dt.Rows.Count + 1, dailyReport_ColumnRight + 1, dailyReport_RowUnder + dt.Rows.Count + 1, dailyReport_ColumnRight + dt.Columns.Count,3);
                    report.setBorderArrayVertical(16, 14, (16 + _items.Count - 1), 14, 3);//N

                    //write header lower part 
                    string obracunObustavio = "obračun sastavio:";
                    report.writeCell((16 + _items.Count + 1), excelNumbers("C"), obracunObustavio, false, 12);
                    report.writeDoubleDownLine((16 + _items.Count + 2), excelNumbers("C"));
                    report.writeDoubleDownLine((16 + _items.Count + 2), excelNumbers("D"));
                    string potpisLica = "potpis ovlašćenog lica:";
                    report.writeCell((16 + _items.Count + 1), excelNumbers("G"), potpisLica, false, 12);
                    report.writeDoubleDownLine((16 + _items.Count + 2), excelNumbers("G"));
                    report.writeDoubleDownLine((16 + _items.Count + 2), excelNumbers("H"));
                    report.writeDoubleDownLine((16 + _items.Count + 2), excelNumbers("I"));
                    report.writeDoubleDownLine((16 + _items.Count + 2), excelNumbers("J"));
                    string svega = "SVEGA";
                    report.writeCell((16 + _items.Count), excelNumbers("M"), svega, false, 12);


                    //calculate svega
                    /*  double sum = 0;
                      for (int i = 0; i < stanjenakraju.Count; i++)
                      {
                          double curr;
                          string currWithPoint = stanjenakraju.ElementAt(i).Replace(',','.');
                          bool isN = Double.TryParse(currWithPoint, out curr);
                          sum = sum + curr;
                      }*/

                    report.writeCell((16 + _items.Count), excelNumbers("N"), _total.ToString(), false, 12);
                    int finalrow = 16 + _items.Count;
                    report.setBorderCell("N" + finalrow.ToString(), 3);

                    int rowBeforeData = 15;
                    for (int i = 1; i <= danaskupljeno.Count; i++)
                    {
                        int index = rowBeforeData + i;
                        double number = 0;
                        bool isN = double.TryParse(danaskupljeno[i-1], out number);
                        if (isN && number > 0)
                        {
                            report.setBackgroundArea("I" + index, "I" + index, enterBackColor);
                        }
                    }
                   
                    for (int i = 95; i <= 100; i++) updateProgressBar(i);


                    /*      if (_isCodeProductWrite)
                          {
                              report.setBackgroundArea("B4", "F4", headerBackColor);
                       

                          }
                          else
                          {
                              report.setBackgroundArea("C4", "F4", headerBackColor);
                          }

                          if (_isCodeProductWrite)
                          {
                              for (int i = 0; i < 5; i++) updateProgressBar(i);
                              report.writeArrayVer("B", 4, "B", (4 + _items.Count), CodeProductArrayList.ToArray());

                              for (int i = 5; i < 15; i++) updateProgressBar(i);


                              report.writeArrayVer("C", 4, "C", (4 + _items.Count), KindOfProductArrayList.ToArray());

                              for (int i = 15; i < 30; i++) updateProgressBar(i);

                              report.writeArrayVer("D", 4, "D", (4 + _items.Count), PriceArrayList.ToArray());

                              for (int i = 30; i < 45; i++) updateProgressBar(i);
                              report.writeArrayVer("E", 4, "E", (4 + _items.Count), AmountArrayList.ToArray());

                              for (int i = 45; i < 60; i++) updateProgressBar(i);
                              report.writeArrayVer("F", 4, "F", (4 + _items.Count), CostItemArrayList.ToArray());

                              for (int i = 60; i < 75; i++) updateProgressBar(i);
                              report.setBorderArrayVertical(4, 2, (4 + _items.Count), 2, 3);

                              for (int i = 75; i < 80; i++) updateProgressBar(i);
                              report.setBorderArrayVertical(4, 3, (4 + _items.Count), 3, 3);

                              for (int i = 80; i < 85; i++) updateProgressBar(i);
                              report.setBorderArrayVertical(4, 4, (4 + _items.Count), 4, 3);

                              for (int i = 85; i < 90; i++) updateProgressBar(i);
                              report.setBorderArrayVertical(4, 5, (4 + _items.Count), 5, 3);

                              for (int i = 90; i < 95; i++) updateProgressBar(i);
                              report.setBorderArrayVertical(4, 6, (4 + _items.Count), 6, 3);

                              for (int i = 95; i <= 100; i++) updateProgressBar(i);
                          }
                          else
                          {

                              report.writeArrayVer("C", 4, "C", (4 + _items.Count), KindOfProductArrayList.ToArray());

                              for (int i = 1; i < 18; i++) updateProgressBar(i);

                              report.writeArrayVer("D", 4, "D", (4 + _items.Count), PriceArrayList.ToArray());

                              for (int i = 18; i < 36; i++) updateProgressBar(i);
                              report.writeArrayVer("E", 4, "E", (4 + _items.Count), AmountArrayList.ToArray());

                              for (int i = 36; i < 54; i++) updateProgressBar(i);
                              report.writeArrayVer("F", 4, "F", (4 + _items.Count), CostItemArrayList.ToArray());

                              for (int i = 54; i < 72; i++) updateProgressBar(i);
                              report.setBorderArrayVertical(4, 3, (4 + _items.Count), 3, 3);

                              for (int i = 72; i < 79; i++) updateProgressBar(i);
                              report.setBorderArrayVertical(4, 4, (4 + _items.Count), 4, 3);

                              for (int i = 79; i < 86; i++) updateProgressBar(i);
                              report.setBorderArrayVertical(4, 5, (4 + _items.Count), 5, 3);

                              for (int i = 86; i < 93; i++) updateProgressBar(i);
                              report.setBorderArrayVertical(4, 6, (4 + _items.Count), 6, 3);

                              for (int i = 93; i <= 100; i++) updateProgressBar(i);
 
                          }
                          string company = String.Empty;
                          string author = String.Empty;

                          if (options.tblCompany2.Text.Equals(Constants.DEFAULTOPTION) == true)
                          {
                              company = options.tblInitialCompany.Text;
                          }
                          else 
                          {
                              company = options.tblInitialAuthor.Text;
                          }

                          if (options.tblAuthor2.Text.Equals(Constants.DEFAULTOPTION) == true)
                          {
                              author = options.tblInitialAuthor.Text;
                          }
                          else
                          {
                              author = options.tblAuthor2.Text;
                          }

                         if (_isCodeProductWrite)
                          {
                              report.writeCell(3, excelNumbers("B"), company, false);
                              report.writeCell((4 + _items.Count + 3), excelNumbers("B"), "Author : " + author, true);
                          }
                          else 
                          {
                              report.writeCell(3, excelNumbers("C"), company, false);
                              report.writeCell((4 + _items.Count + 3), excelNumbers("C"), "Author : " + author, true);
                          }
                          report.writeCell(3, excelNumbers("F"), _currDate, false);
                          report.writeCell((4 + _items.Count + 3), excelNumbers("D"), "TOTAL : ", false);
                          report.writeCell((4 + _items.Count + 3), excelNumbers("F"), _total.ToString() + " " + _currency, false);*/


                    //return all daily bought enter
                    for (int i = 0; i < window.enterStoreItemsTab2.StoreItemProducts.Count; i++)
                    {
                        StoreItemProduct sip = new StoreItemProduct();
                        sip.CodeProduct = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).CodeProduct;
                        sip.Amount = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Amount;
                        sip.Group = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Group;
                        sip.isUsed = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).isUsed;
                        sip.KindOfProduct = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).KindOfProduct;
                        sip.Measure = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Measure;
                        sip.Price = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Price;
                        sip.RealAmount = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).RealAmount;
                        sip.Threshold = window.enterStoreItemsTab2.StoreItemProducts.ElementAt(i).Threshold;
                        storehouse.DailyStoreItem.Add(sip);
                    }

                    storehouse.dgridDailyEnterInStorehouse.ItemsSource = storehouse.DailyStoreItem;
                    storehouse.dgridDailyEnterInStorehouse.Foreground = System.Windows.Media.Brushes.Black;
                    storehouse.btnFinish.IsEnabled = true;


                    //save state of storehouse for that day
                    for (int i = 0; i < window.storehouse.StorehouseItems.Count; i++)
                    {
                        StorehouseItemState sItemState = new StorehouseItemState(window.storehouse.StorehouseItems.ElementAt(i), datepicker1.SelectedDate.Value.ToShortDateString());
                        insertRecordInstatesStoreOnEndDay(sItemState);
                    }


                    newWindowThread.Abort();
                    tblRemarkTab1.Text = Constants.MUSTCLOSE;
                    tblisCreatingReportTime.Text = Constants.REMARKPROGRESSBAREND;
                    tblisCreatingReportTime.FontWeight = FontWeights.Normal;
                    tblisCreatingReportTime.Foreground = System.Windows.Media.Brushes.Black;

                    if (options.cmbAppSound.IsChecked == true)
                    {
                        player.Stop();
                    }
                    _currDate = _dateOfLastCreatedBarBook.ToShortDateString();
                    MessageBox.Show(Constants.NOTIFICATION_REPORTCREATEDBEGIN + datepicker1.Text + Constants.NOTIFICATION_REPORTCREATEDEND, "IZVEŠTAJ JE KREIRAN");
                    Logger.writeNode(Constants.MESSAGEBOX, Constants.NOTIFICATION_REPORTCREATEDBEGIN + datepicker1.Text + Constants.NOTIFICATION_REPORTCREATEDEND);
                    cmbNameProductTab1.IsEnabled = false;
                    tblRemarkTab1.Text = Constants.ENTERDATE_REPORT;
                    btnaddItem.IsEnabled = true;

                    _dateOfLastCreatedBarBook = DateTime.Parse(datepicker1.Text);
                    updateDateOfLastCreatedBarBook(_dateOfLastCreatedBarBook);

                    //set enter date in storehouse enter tab3 podtab1
                    storehouse.datepicker1.SelectedDate = _dateOfLastCreatedBarBook.AddDays(1);

                    //set enter date for first tab datepicker
                    datepicker1.SelectedDate = storehouse.datepicker1.SelectedDate;
                    _items.Clear();
                    dataGrid1.ItemsSource = _items;
                    autoscrollToBottom(dataGrid1);
                    btncreateReport.IsEnabled = false;
                    _total = 0;
                    tblTotalValue.Text = _total.ToString() + " " + _currency;
                    _currDate = String.Empty;

                    checkRemarkDownTab1();
                    //if (options.cmbAppOpen.IsChecked == true)
                    //{
                    //    report.openFile();
                    //}
                    //else
                    //{
                    //    report.closeFile();
                    //}
                    pbar.Value = 0;
                    // reset num of entered products
                    numberOfEnteredProduct = 0;

                    cmbNameProductTab1.SelectedIndex = 0;
                    IsEnteredMoreBuyedStoreItems = false;
                    this.storehouse.isBarBookCreated = true;
                    this.storehouse.btnEnter.IsEnabled = true;
                    Logger.writeNode(Constants.INFORMATION, "Tab1 Zavrsavanje kreiranje prometa u excel file");




                    //this.storehouse.DailyStoreItem.RemoveAt(0);
                    this.storehouse.dgridDailyEnterInStorehouse.ItemsSource = this.storehouse.DailyStoreItem;

                    //reset comboboxs for enter data
                    this.storehouse.cmbSGroup.SelectedIndex = 0;
                    this.storehouse.cmbSItem.SelectedIndex = 0;

                    //enable return button
                    storehouse.btnReturnOneDay.IsEnabled = true;


                ////send to dropbox
                //if (string.IsNullOrEmpty(Properties.Settings.Default.AccessToken))
                //{
                //    this.GetAccessToken();
                //}
                //else
                //{
                //    GetFiles();
                //}

                Ping ping = new Ping();
                PingReply pingReply = ping.Send("8.8.8.8");

                if (pingReply.Status == IPStatus.Success)
                {
                    //Machine is alive

                    //send to gmail
                    report.closeFile();
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
                    message.Attachments.Add(new Attachment(pathOfCreatingReport));
                    client.Send(message);
                }
                else
                {
                    MessageBox.Show("Niste konektovani na INTERNET  !!!!");
                }




                if (options.cmbAppOpen.IsChecked == true)
                {
                    report.openFile();
                }
                else
                {
                    report.closeFile();
                }


                reportNotYetCreated = false;


                              }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("Putanja do fajla za muzikom vam je neispravna! Zvuk neće biti reprodukovan!", "MUZIČKI FAJL ERROR");
                    Logger.writeNode(Constants.MESSAGEBOX, "Putanja do fajla za muzikom vam je neispravna! Zvuk neće biti reprodukovan!");
                    savenumofitemsEVERCreated();
                    reportNotYetCreated = false;
                }
            
             
            }

        private void GetFiles()
        {

        }

        private void GetAccessToken()
        {
            dropboxForm = new DropboxForm();
            var login = new DropboxLogin("ns3tt30p6hefti7", "6709rpzp9hj5484");
            login.Owner = dropboxForm;
            login.ShowDialog();
            if (login.IsSuccessfully)
            {
                MessageBox.Show("Login is successful!!");

                Properties.Settings.Default.AccessToken = login.AccessToken.Value;
                Properties.Settings.Default.Save();

                
                OAuthUtility.PutAsync
                    (
                        "https://api-content.dropbox.com/1/files_put/auto/",
                        new HttpParameterCollection
                        {
                            { "access_token", Properties.Settings.Default.AccessToken},
                            { "path",fileNameForDropBox},
                            { "overwite","true"},
                            { "autorename","true" }
                        },
                        callback: Upload_Result
                    );


            }
            else
            {
                MessageBox.Show("Neuspesno logovanje na Dropbox!!!");
            }

        }

        private void Upload_Result(RequestResult result)
        {
            if (dropboxForm.InvokeRequired)
            {
                dropboxForm.Invoke(new Action<RequestResult>(Upload_Result),result);
                return;
            }
            if (result.StatusCode == 200)
            {
                MessageBox.Show("200");
            }
            else if (result.StatusCode == 400)
            {
                MessageBox.Show("400");
            }
            else if (result.StatusCode == 400)
            {
                MessageBox.Show("400");
            }
            else if (result.StatusCode == 401)
            {
                MessageBox.Show("401");
            }
            else if (result.StatusCode == 403)
            {
                MessageBox.Show("403");
            }
            else if (result.StatusCode == 404)
            {
                MessageBox.Show("404");
            }
            else if (result.StatusCode == 405)
            {
                MessageBox.Show("405");
            }
            else if (result.StatusCode == 429)
            {
                MessageBox.Show("429");
            }
            else if (result.StatusCode == 503)
            {
                MessageBox.Show("503");
            }
            else if (result.StatusCode == 507)
            {
                MessageBox.Show("507");
            }
            else
            {
                if (result["error"].HasValue)
                {
                    MessageBox.Show(result["error"].ToString());
                }
                else
                {
                    MessageBox.Show(result.ToString());
                }
            }


        }


            private double getRecordFromProductsAmounts(string ProductCode)
            {
                try
                {
                    double am = 0.0;
                    string query = "SELECT * FROM productsAmounts WHERE PrCode = @PrCode;";

                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.Parameters.Add(" @PrCode", ProductCode);
                    dr = com.ExecuteReader();
                    string amount;


                    while (dr.Read())
                    {
                        //StorehouseItemState storeI = new StorehouseItemState();
                        amount = dr["PrAmount"].ToString();
                        string amountWithPoint = amount.Replace(',', '.');
                       
                        bool isN = Double.TryParse(amountWithPoint, out am);
                        
                    }

                    return am;



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                    return 0.0;

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

            private void updateDateOfLastCreatedBarBook(DateTime dateOfLastCreatedBarBook) 
            {
                try
                {
                    conOptions.Open();
                    string query = "UPDATE savedOptions SET DateOfLastCreatedBarBook ='" + dateOfLastCreatedBarBook.ToShortDateString() + "' WHERE Options='options';";
                    comOptions = new OleDbCommand(query, conOptions);
                    comOptions.ExecuteNonQuery();

                    string queryStorehouse = "UPDATE savedOptions SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='options' ;";
                    com = new OleDbCommand(queryStorehouse, conOptions);
                    com.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                    MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                    window.savenumofitemsEVERCreated();
                }
                finally
                {
                    if (conOptions != null)
                    {
                        conOptions.Close();
                    }
                    if (dr != null)
                    {
                        dr.Close();
                    }
                }
            }

            private void updateProgressBar(double value)
            {
                this.Dispatcher.Invoke(DispatcherPriority.Background, new System.Action(delegate()
                {
                    this.pbar.Value = value; // Do all the ui thread updates here
                }));
            }

            private void chbNotWorkingDay_Checked(object sender, RoutedEventArgs e)
            {
                if (datepicker1.Text.Equals(String.Empty) == false)
                {
                    btncreateReport.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Morate uneti datum koji je neradan dan!");
                    chbNotWorkingDay.IsChecked = false;
                }
            }

            private void chbNotWorkingDay_Unchecked(object sender, RoutedEventArgs e)
            {
                if (_items.Count == 0)
                {
                    btncreateReport.IsEnabled = false;
                }
            }

            private void insertRecordInstatesStoreOnEndDay(StorehouseItemState s)
            {
                try
                {

                    string newMeasure = String.Empty;
                    

                    string id = "54";//Queries.xml ID

                    XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                    XElement Query = (from xml2 in xdoc.Descendants("Query")
                                      where xml2.Element("ID").Value == id
                                      select xml2).FirstOrDefault();
                    Console.WriteLine(Query.ToString());
                    string query = Query.Attribute(Constants.TEXT).Value;
                    DateTime stateOfEndDateTime = DateTime.Parse(s.StateOfEndDateTime);
                   

                    con.Open();
                    com = new OleDbCommand(query, con);
                    com.Parameters.Add("@StoreItemCode", s.ItemCode);
                    com.Parameters.Add("@StoreItemName", s.ItemName);
                    com.Parameters.Add("@StoreItemGroup", s.ItemGroup);
                    com.Parameters.Add("@StoreItemForOnePrice", s.ItemforOnePrice.ToString());
                    com.Parameters.Add("@StoreItemforOneAmount", s.ItemforOneAmount.ToString());
                    com.Parameters.Add("@RealAmount", s.ItemRealAmount.ToString());
                    com.Parameters.Add("@RealPrice", s.ItemPrice.ToString());
                    com.Parameters.Add("@Valuta", this.Currency);
                    com.Parameters.Add("@CreatedDateTime", DateTime.Now.ToString());
                    com.Parameters.Add("@LastDateTimeUpdated", DateTime.Now.ToString());
                    com.Parameters.Add("@StateOfEndDateTime", stateOfEndDateTime);
                    com.ExecuteNonQuery();
                   

                   


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.writeNode(Constants.EXCEPTION, ex.Message);
                    MainWindow win = (MainWindow)System.Windows.Window.GetWindow(this);
                    win.savenumofitemsEVERCreated();

                }
                finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
 
            }

            private void SelectedDateChanged_datapicker1(object sender, SelectionChangedEventArgs e)
            {
                try
                {

                    if (dataGrid1.Items.Count > 0)
                    {
                        btncreateReport.IsEnabled = true;
                    }
                    // ... Get DatePicker reference.
                    var picker = sender as DatePicker;

                    // ... Get nullable DateTime from SelectedDate.
                    DateTime? date = picker.SelectedDate;
                    if (date == null)
                    {
                        // ... A null object.
                        _currDate =String.Empty;
                    }
                    else
                    {
                        // ... No need to display the time.
                        _currDate = date.Value.ToShortDateString();
                        _dateCreatedReport = date.Value;
                        Logger.writeNode(Constants.INFORMATION, "Tab1 Izbor datuma kreiranja izvestaja prometa. Izabrani datum je: " + _currDate);
                        tblRemarkTab1.Text = String.Empty;
                        cmbNameProductTab1.IsEnabled = true;
                        checkRemarkDownTab1();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("You did not enter date in tab1!!!");
                    Logger.writeNode(Constants.MESSAGEBOX, "You did not enter date in tab1!!!");
                    savenumofitemsEVERCreated();
                }

            }

            private void checkRemarkDownTab1()
            {
                if (dataGrid1.Items.Count == 0 && _currDate.Equals(String.Empty))
                {
                    tblRemarkTabDown1.Text = Constants.REMARKDOWNTAB1_1;
                }
                else if (dataGrid1.Items.Count == 0 && _currDate.Equals(String.Empty) == false)
                {
                    tblRemarkTabDown1.Text = Constants.REMARKDOWNTAB1_2;
                }
                else if (dataGrid1.Items.Count != 0 && _currDate.Equals(String.Empty))
                {
                    tblRemarkTabDown1.Text = Constants.REMARKDOWNTAB1_3;
                }
                else
                {
                    tblRemarkTabDown1.Text = string.Empty;
                }
            }


            private void ValueChanged_pbar(object sender, RoutedPropertyChangedEventArgs<double> e)
            {
                ExtensionMethods.Refresh(this.pbar);
            }

       #endregion



       private void SizeChanged_MainWindow(object sender, SizeChangedEventArgs e)
       {
           options.Height = this.ActualHeight;
           options.Width = this.ActualWidth;
           options.tabControl1.Height = this.ActualHeight;
           options.tabControl1.Width = this.ActualWidth;

           /* griddataGrid.Height = this.ActualHeight;
            griddataGrid.Width = this.ActualWidth;
            Logger.writeNode(Constants.INFORMATION, "Tab1 Promena velicine MainWindowa na novu visinu " + griddataGrid.Height + " i novu sirinu " + griddataGrid.Width);*/

       }


       public void savenumofitemsEVERCreated() 
       {
           //update numofitemsEVERCreated in database
           try
           {
               con.Open();
               string queryStorehouse = "UPDATE numofitemsEVER SET NumberOfItemCreated = " + "'" + _numOfEverCreatedItem.ToString() + "'" + ";";
               com = new OleDbCommand(queryStorehouse, con);
               com.ExecuteNonQuery();

               Logger.writeNode(Constants.INFORMATION, "Tab1 Upis broja ikada prodatih stavki prometa. Ukupno je do sada prodato stavki prometa " + _numOfEverCreatedItem.ToString());

               queryStorehouse = "UPDATE numofitemsEVER SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + ";";
               com = new OleDbCommand(queryStorehouse, con);
               com.ExecuteNonQuery();
             
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               Logger.writeNode(Constants.EXCEPTION, ex.Message);
               savenumofitemsEVERCreated();
           }
           finally
           {
               if (con != null)
               {
                   con.Close();
               }
           }
           //update numofitemsEVERCreated in database
       }

       private void saveNumOfLogNodes() 
       {
           //update LogNodeNumber in database
           try
           {
               conLoggerNumber.Open();
               string queryStorehouse = "UPDATE logNumberNodes SET LogNumber = " + "'" + Logger.LogNodeNumber + "'" + ";";
               com = new OleDbCommand(queryStorehouse, conLoggerNumber);
               com.ExecuteNonQuery();

               queryStorehouse = "UPDATE logNumberNodes SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + ";";
               com = new OleDbCommand(queryStorehouse, conLoggerNumber);
               com.ExecuteNonQuery();

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               Logger.writeNode(Constants.EXCEPTION, ex.Message);
               savenumofitemsEVERCreated();
           }
           finally
           {
               if (conLoggerNumber != null)
               {
                   conLoggerNumber.Close();
               }
           }
           //update LogNodeNumber in database
       }

       static void CreateMdb(string fileNameWithPath)
       {
           ADOX.Catalog cat = new ADOX.Catalog();
           string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Engine Type=5";
           cat.Create(String.Format(connstr, fileNameWithPath));
           cat = null;
       }


       private void archiveAndCreateNewLogFile() 
       {
           try
           {
               conLogger.Close();
               string idCount = "50";//Queries.xml ID

               XDocument xdocCount = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
               XElement QueryCount = (from xml2 in xdocCount.Descendants("Query")
                                      where xml2.Element("ID").Value == idCount
                                      select xml2).FirstOrDefault();

               string queryCount = QueryCount.Attribute(Constants.TEXT).Value;
               string numArchiveLogger = String.Empty;
               int numArchiveLoggerNumber = 0;

               conLoggerNumber.Open();
               com = new OleDbCommand(queryCount, conLoggerNumber);
               dr = com.ExecuteReader();
               if (dr.Read())
               {
                   numArchiveLogger = dr["loggerArchiveNumber"].ToString();
                   bool isN = int.TryParse(numArchiveLogger, out numArchiveLoggerNumber);
               }
               conLoggerNumber.Close();
               numArchiveLoggerNumber++;
               string from = System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_LOGGER;
               string to = System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_LOGGER_ARCHIVE + numArchiveLoggerNumber.ToString() + ".mdb";
               File.Move(from, to);

               //create new logger file
               string fileNameWithPath = System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_LOGGER;
               CreateMdb(fileNameWithPath);

               string queryCreate;
               conLogger.Open();
               queryCreate = "CREATE TABLE LoggerTable (ID counter primary key,NodeNumber varchar(255),Status varchar(255),Node LONGTEXT,DateTimeWrite DateTime);";
               com = new OleDbCommand(queryCreate, conLogger);
               com.ExecuteNonQuery();

               //update numArchiveLoggerNumber in database
               conLoggerNumber.Open();
               string queryArchive = "UPDATE loggerArchiveNumber SET loggerArchiveNumber = " + "'" + numArchiveLoggerNumber + "'" + ";";
               com = new OleDbCommand(queryArchive, conLoggerNumber);
               com.ExecuteNonQuery();

               
               this.Close();

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               Logger.writeNode(Constants.EXCEPTION, ex.Message);
           }
           finally
           {
               if (conLogger != null)
               {
                   conLogger.Close();
               }
               if (conLoggerNumber != null)
               {
                   conLoggerNumber.Close();
               }
               System.Environment.Exit(0);
           }
       }

       private void Closed_Window(object sender, EventArgs e)
       {
          

           try
           {
               savenumofitemsEVERCreated();
               saveNumOfLogNodes();
               string idCount = "49";//Queries.xml ID

               XDocument xdocCount = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
               XElement QueryCount = (from xml2 in xdocCount.Descendants("Query")
                                      where xml2.Element("ID").Value == idCount
                                      select xml2).FirstOrDefault();

               string queryCount = QueryCount.Attribute(Constants.TEXT).Value;

               conLogger.Open();
               com = new OleDbCommand(queryCount, conLogger);
               Int32 count = (Int32)com.ExecuteScalar();
               if (count >= 600000)
               {
                   Logger.LogNodeNumber = 0;
                   archiveAndCreateNewLogFile();
               }
               this.Close();
              
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               Logger.writeNode(Constants.EXCEPTION, ex.Message);
           }
           finally 
           {
               if (conLogger != null)
               {
                   conLogger.Close();
               }
               System.Environment.Exit(0);
           }

           
       }


     

      

     
    

      
     
      
      



    }// end of class
}
