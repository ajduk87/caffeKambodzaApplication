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
using System.Data;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Data.OleDb;
using System.ComponentModel;


namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for SelectUpdateConnProdStore.xaml
    /// </summary>
    public partial class SelectUpdateConnProdStore : UserControl
    {


        private string oldProductAmount = String.Empty;
        private string oldStoreItemAmount = String.Empty;
        private string oldPrice = String.Empty;
        

        private bool filteredDgridUsedRecipes = false;

         private OleDbConnection conConnProdStore = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
         private OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
         private OleDbConnection conHelp = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
         private OleDbCommand com;
         private OleDbDataReader drConn;
         private OleDbDataReader dr;
         private OleDbDataReader dr2;
         private ObservableCollection<ConnectionRecord> records = new ObservableCollection<ConnectionRecord>();
         public ObservableCollection<ConnectionRecord> Records
         {
             get 
             {
                 return records;
             }
             set
             {
                 records = value;
             }
             
         }

         public ICollectionView cvRecords;

         private int indexSelected;

        public SelectUpdateConnProdStore()
        {
            InitializeComponent();
            records = getData();
            
            cvRecords = CollectionViewSource.GetDefaultView(records);
            if (cvRecords != null)
            {
                dgridCurrProductStoreItemConn.ItemsSource = cvRecords;
            }

            tf1.IsReadOnly = true;
            tf2.IsReadOnly = true;
            tf3.IsReadOnly = true;
            tf4.IsReadOnly = true;
            tf5.IsReadOnly = true;

            cmbFilterColumnRecipes.SelectedIndex = 0;
        }

      
        private ObservableCollection<ConnectionRecord> getData()
        {
            ObservableCollection<ConnectionRecord> res = new ObservableCollection<ConnectionRecord>();

            try
            {

                string id = "13";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;


                conConnProdStore.Open();
                com = new OleDbCommand(query, conConnProdStore);
                drConn = com.ExecuteReader();
                string yesNo = String.Empty;

                while (drConn.Read())
                {
                    ConnectionRecord cr = new ConnectionRecord(drConn["ConnCodeProduct"].ToString(), drConn["ConnStoreItemCode"].ToString(), drConn["ConnKindOfProduct"].ToString(), drConn["ConnStoreItemName"].ToString(), drConn["GroupStoreItem"].ToString(), drConn["AmountProduct"].ToString(), drConn["AmountStoreItem"].ToString(), drConn["Price"].ToString());
                    yesNo = drConn["isUsed"].ToString();
                    if (yesNo.Equals(Constants.YES))
                    {
                        cr.IsUsed = true;
                    }
                    else
                    {
                        cr.IsUsed = false;
                    }
                    res.Add(cr);
                }

                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.savenumofitemsEVERCreated();
                return new ObservableCollection<ConnectionRecord>();

            }
            finally 
            {
                conConnProdStore.Close();
                drConn.Close();
            }
        }


        public void updateConnection() 
        {
            records = getData();
            cvRecords = CollectionViewSource.GetDefaultView(records);
            if (cvRecords != null)
            {
                dgridCurrProductStoreItemConn.ItemsSource = cvRecords;
            }
            tf1.IsReadOnly = true;
            tf2.IsReadOnly = true;
            tf3.IsReadOnly = true;
            tf4.IsReadOnly = true;
            tf5.IsReadOnly = true;
        }
        private void dgridCurrProductStoreItemConn_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           
            

            indexSelected = dgridCurrProductStoreItemConn.SelectedIndex;

            if (indexSelected > -1)
            {

                records.ElementAt(indexSelected);
                tf1.Text = records.ElementAt(indexSelected).ConnCodeProduct;
                tf2.Text = records.ElementAt(indexSelected).ConnStoreItemCode;
                tf3.Text = records.ElementAt(indexSelected).ConnKindOfProduct;
                tf4.Text = records.ElementAt(indexSelected).ConnStoreItemName;
                tf5.Text = records.ElementAt(indexSelected).GroupStoreItem;
                tf6.Text = records.ElementAt(indexSelected).AmountProduct;
                tf7.Text = records.ElementAt(indexSelected).AmountStoreItem;
                tf8.Text = records.ElementAt(indexSelected).Price;
                Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab3 Selektovani recept. Sifra proizvoda kafica :" + tf1.Text + ". Vrsta proizvoda kafica :" + tf3.Text + ". Kolicinski udeo proizvoda(kg/l) :" + tf6.Text);
                Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab3 Selektovani recept. Sifra magacinske stavke :" + tf2.Text + ". Naziv magacinske stavke :" + tf4.Text + ". Grupa magacinske stavke :" + tf5.Text + ". Kolicina jedne magacinske stavke(kg/l) :" + tf7.Text + ". Jedinicna cena magacinske stavke(din) :" + tf8.Text);

                oldProductAmount = tf6.Text;
                oldStoreItemAmount = tf7.Text;
                oldPrice = tf8.Text;


                string names = dgridCurrProductStoreItemConn.Items[dgridCurrProductStoreItemConn.SelectedIndex].ToString();
                string[] combotexts = names.Split('&');
            }
          
           
        }


        private void insertRecordInHistoryChangeRecipes(string codeProduct, string codeStoreItem, string kindOfProduct, string storeItemName, string storeItemGroup, string type, string oldProductAmount, string newProductAmount, string oldStoreItemAmount, string newStoreItemAmount) 
        {
            try
            {
                MainWindow win = (MainWindow)MainWindow.GetWindow(this);

               
                string id = "43";//Queries.xml ID

                XDocument xdoc = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdoc.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;


                conHelp.Open();
                com = new OleDbCommand(query, conHelp);
                com.Parameters.AddWithValue("@ProductCode", codeProduct);
                com.Parameters.AddWithValue("@StoreItemCode", codeStoreItem);
                com.Parameters.AddWithValue("@KindOfProduct", kindOfProduct);
                com.Parameters.AddWithValue("@StoreItemName", storeItemName);
                com.Parameters.AddWithValue("@StoreItemGroup", storeItemGroup);
                com.Parameters.AddWithValue("@Type", type);
                com.Parameters.AddWithValue("@OLDProductAmount", oldProductAmount);
                com.Parameters.AddWithValue("@NEWProductAmount", newProductAmount);
                com.Parameters.AddWithValue("@OLDStoreItemAmount", oldStoreItemAmount);
                com.Parameters.AddWithValue("@NEWStoreItemAmount", newStoreItemAmount);
                com.Parameters.AddWithValue("@DateChangeEntered", DateTime.Now.ToString());
                

                com.ExecuteNonQuery();



                //refresh hRecipes collection

                for (DateTime x = win.history.DateCreatedReportStart; x <= win.history.DateCreatedReportEnd; x = x.AddDays(1))
                    {
                        string dateCurrStr = x.ToString().Replace("0:00:00", "");
                        dateCurrStr = dateCurrStr.Substring(0,dateCurrStr.Length-1);
						
						
                        HistoryChangeRecipes hRecipe = new HistoryChangeRecipes(codeProduct, codeStoreItem, kindOfProduct, storeItemName, storeItemGroup, oldProductAmount, newProductAmount, oldStoreItemAmount, newStoreItemAmount, DateTime.Now);
                        hRecipe.setType(type);
                        DateTime sHistRCorrDate = DateTime.Parse(hRecipe.DateChanged);
                        
                        
                        if (dateCurrStr.Equals(sHistRCorrDate.ToShortDateString()) == true)
                        {

                
                            win.history.hRecipes.Add(hRecipe);

                            win.history.cvhRecipes = CollectionViewSource.GetDefaultView(win.history.hRecipes);
                            if (win.history.cvhRecipes != null)
                            {
                                win.history.dataGridReadHistoryRecipes.ItemsSource = win.history.cvhRecipes;
                            }


                        break;
                        }
                    }


                oldStoreItemAmount = newStoreItemAmount;
                oldProductAmount = newProductAmount;
                
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


        private void insertRecordInHistoryChangePrices(string code, string name, string type,string oldprice, string newprice) 
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string  type = String.Empty;

                if (oldProductAmount.Equals(tf6.Text) == false && oldStoreItemAmount.Equals(tf7.Text) == false)
                {
                    type = Constants.PRODUCTSTOREITEM;
                }
                else if (oldProductAmount.Equals(tf6.Text) == false)
                {
                    type = Constants.PRODUCT;
                }
                else if (oldStoreItemAmount.Equals(tf7.Text) == false)
                {
                    type = Constants.STOREITEM;
                }

                if (type.Equals(String.Empty) == false && (oldProductAmount.Equals(tf6.Text) == false || oldStoreItemAmount.Equals(tf7.Text) == false))
                {
                    //insert record in HistoryChangeRecipes
                    insertRecordInHistoryChangeRecipes(tf1.Text, tf2.Text, tf3.Text, tf4.Text, tf5.Text, type, oldProductAmount, tf6.Text, oldStoreItemAmount, tf7.Text);
                }

                if (oldPrice.Equals(tf8.Text) == false) 
                {
                     //insert record in HistoryChangePrices
                    insertRecordInHistoryChangePrices(tf2.Text, tf4.Text, Constants.STOREITEM, oldPrice, tf8.Text);
                }


                Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab3 Menjanje recepata i/ili cena stavki šanka. Sifra proizvoda kafica: " + tf1.Text + ". Vrsta proizvoda kafica :" + tf3.Text + ".Stari kolicinski udeo proizvoda(kg/l) :" + oldProductAmount + ". Novi kolicinski udeo proizvoda(kg/l) :" + tf6.Text);
                Logger.writeNode(Constants.INFORMATION, "Tab2 PodTab3 Menjanje recepata i/ili cena stavki šanka. Sifra stavke šanka: " + tf2.Text + ". Naziv stavke šanka :" + tf4.Text + ". Stara jedinicna kolicinska stavke u magacinu(kg/l) :" + oldStoreItemAmount + ". Nova jedinicna kolicinska stavke u magacinu(kg/l) :" + tf7.Text + ". Stara cena magacinske stavke(din) :" + oldPrice + ". Nova cena magacinske stavke(din) :" + tf8.Text);


                oldProductAmount = tf6.Text;
                oldStoreItemAmount = tf7.Text;
                oldPrice = tf8.Text;

                string query = "UPDATE connectionTableProductsStore SET AmountProduct = " + "'" + tf6.Text + "'" +  "," + "AmountStoreItem =" + "'" + tf7.Text + "'" + "," + "Price =" + "'" + tf8.Text + "'" + " WHERE ConnCodeProduct =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnCodeProduct + "'" + " AND " + "ConnStoreItemCode =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnStoreItemCode + "'" + ";";
                
                
                string queryStoreIrem = "UPDATE storeItems SET StoreItemPrice = " + "'" + tf8.Text + "'" + "," + "Amount = " + "'" + tf7.Text + "'" + " WHERE StoreItemCode = " + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnStoreItemCode + "'" + ";";

                conConnProdStore.Open();
                com = new OleDbCommand(query, conConnProdStore);
                com.ExecuteNonQuery();
                com = new OleDbCommand(queryStoreIrem, conConnProdStore);
                com.ExecuteNonQuery();


                // update table productsAmount
                query = "UPDATE productsAmounts SET PrAmount = " + "'" + tf6.Text + "'" + " WHERE PrCode = " + "'" + tf1.Text + "'" + ";";
                com = new OleDbCommand(query, conConnProdStore);
                com.ExecuteNonQuery();

                conConnProdStore.Close();

              




                /*****          date update ***********/
                con.Open();
                string queryStorehouse = "UPDATE storeItems SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE StoreItemCode =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnStoreItemCode + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();
                queryStorehouse = "UPDATE connectionTableProductsStore SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + " WHERE ConnCodeProduct =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnCodeProduct + "'" + " AND " + "ConnStoreItemCode =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnStoreItemCode + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();

                query = "SELECT NumberOfUpdates FROM storeItems WHERE StoreItemCode = " + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnStoreItemCode + "'" + ";";
                com = new OleDbCommand(query, con);
                dr = com.ExecuteReader();
                query = "SELECT NumberOfUpdates FROM connectionTableProductsStore " +  "WHERE ConnCodeProduct =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnCodeProduct + "'" + " AND " + "ConnStoreItemCode =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnStoreItemCode + "'" + ";";
                com = new OleDbCommand(query, con);
                dr2 = com.ExecuteReader();
                int oldUpNum2 = 0;
                while (dr2.Read())
                {
                    bool isNum = int.TryParse(dr2["NumberOfUpdates"].ToString(), out oldUpNum2);
                }
                int upNum2 = oldUpNum2 + 1;
                queryStorehouse = "UPDATE connectionTableProductsStore SET NumberOfUpdates = " + "'" + upNum2.ToString() + "'" + "WHERE ConnCodeProduct =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnCodeProduct + "'" + " AND " + "ConnStoreItemCode =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnStoreItemCode + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();

                int oldUpNum = 0;
                while (dr.Read())
                {
                    bool isNum = int.TryParse(dr["NumberOfUpdates"].ToString(), out oldUpNum);
                }


                int upNum = oldUpNum + 1;
                queryStorehouse = "UPDATE storeItems SET NumberOfUpdates = " + "'" + upNum.ToString() + "'" + "WHERE StoreItemCode =" + "'" + records.ElementAt(dgridCurrProductStoreItemConn.SelectedIndex).ConnStoreItemCode + "'" + ";";
                com = new OleDbCommand(queryStorehouse, con);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window2 = (MainWindow)MainWindow.GetWindow(this);
                window2.savenumofitemsEVERCreated();

            }
            finally
            {
                if (conConnProdStore != null)
                {
                    conConnProdStore.Close();
                }
                if (con != null)
                {
                    con.Close();
                }
            }

            records.ElementAt(indexSelected).AmountProduct = tf6.Text;
            records.ElementAt(indexSelected).AmountStoreItem = tf7.Text;
            records.ElementAt(indexSelected).Price = tf8.Text;

            cvRecords = CollectionViewSource.GetDefaultView(records);
            if (cvRecords != null)
            {
                dgridCurrProductStoreItemConn.ItemsSource = cvRecords;
            }

            MainWindow window = (MainWindow)MainWindow.GetWindow(this);
            ConnectionRecord informUsedRec = new ConnectionRecord();

            if (records.ElementAt(indexSelected).IsUsed == true) 
            {
                informUsedRec  = records.ElementAt(indexSelected);
           

                for (int i = 0; i < window.storehouse.UsedRecords.Count; i++)
                {
                    if (window.storehouse.UsedRecords.ElementAt(i).ConnCodeProduct.Equals(informUsedRec.ConnCodeProduct) == true && window.storehouse.UsedRecords.ElementAt(i).ConnStoreItemCode.Equals(informUsedRec.ConnStoreItemCode) == true)
                    {
                        window.storehouse.UsedRecords.ElementAt(i).AmountProduct = informUsedRec.AmountProduct;
                        window.storehouse.UsedRecords.ElementAt(i).AmountStoreItem = informUsedRec.AmountStoreItem;
                        window.storehouse.UsedRecords.ElementAt(i).Price = informUsedRec.Price; //price for store item NOT FOR PRODUCTS
                    }
                }
            }

            
        }


        #region partforFiltering_Recipes


        private void filterDataRecipes()
        {

            tblFilterStatusRecipes.Text = Constants.FILTERON;
            tblFilterStatusRecipes.Background = Brushes.Orange;

            this.cvRecords.Filter = item =>
            {
                var vitem = item as ConnectionRecord;
                if (vitem == null) return false;
                string searchText = tfFilterRecipes.Text.ToUpper();

                if (cmbFilterColumnRecipes.SelectedIndex == 1)
                {

                    string codeOfProduct = vitem.ConnCodeProduct.ToUpper();
                    if (codeOfProduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnRecipes.SelectedIndex == 2)
                {
                    string storeCode = vitem.ConnStoreItemCode.ToUpper();
                    if (storeCode.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnRecipes.SelectedIndex == 3)
                {

                    string kindOfproduct = vitem.ConnKindOfProduct.ToUpper();
                    if (kindOfproduct.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnRecipes.SelectedIndex == 4)
                {

                    string storeName = vitem.ConnStoreItemName.ToString().ToUpper();
                    if (storeName.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnRecipes.SelectedIndex == 5)
                {
                    string group = vitem.GroupStoreItem.ToUpper();
                    if (group.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnRecipes.SelectedIndex == 6)
                {

                    string productAmount = vitem.AmountProduct.ToUpper();
                    if (productAmount.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else if (cmbFilterColumnRecipes.SelectedIndex == 7)
                {

                    string storeAmount = vitem.AmountStoreItem.ToString().ToUpper();
                    if (storeAmount.Contains(searchText) == true) { return true; }
                    else { return false; }
                }
                else
                {

                    string price = vitem.Price.ToString().ToUpper();
                    if (price.Contains(searchText) == true) { return true; }
                    else { return false; }
                }

            };

        }

        private void unFilteredDataRecipes()
        {
            tblFilterStatusRecipes.Text = String.Empty;
            MainWindow win = (MainWindow)Window.GetWindow(this);
            if (win.options.chkbMask.IsChecked == true)
            {
                Object obj1 = this.Resources["Gradient4"];
                tblFilterStatusRecipes.Background = (Brush)obj1;
            }
            else
            {
                tblFilterStatusRecipes.Background = Brushes.White;
            }

            this.cvRecords.Filter = item =>
            {

                var vitem = item as ConnectionRecord;
                if (vitem == null) return false;
                else return true;

            };
        }



        private void tfFilterRecipes_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbFilterColumnRecipes.SelectedIndex == 0)
            {
                tfFilterRecipes.IsReadOnly = true;

                tblFilterStatusRecipes.Text = Constants.FILTER_COLUMN;
                tblFilterStatusRecipes.Foreground = Brushes.White;
                tblFilterStatusRecipes.Background = Brushes.Red;
            }
            else
            {
                tfFilterRecipes.IsReadOnly = false;
            }
        }

        private void tfFilterRecipes_MouseLeave(object sender, MouseEventArgs e)
        {
            if (filteredDgridUsedRecipes == false)
            {
                tblFilterStatusRecipes.Text = String.Empty;
                MainWindow win = (MainWindow)Window.GetWindow(this);
                if (win.options.chkbMask.IsChecked == true)
                {
                    Object obj1 = this.Resources["Gradient4"];
                    tblFilterStatusRecipes.Background = (Brush)obj1;
                }
                else
                {
                    tblFilterStatusRecipes.Background = Brushes.White;
                }
            }
        }

        private void tfFilterRecipes_KeyDown(object sender, KeyEventArgs e)
        {
            if (filteredDgridUsedRecipes)
            {
                if (e.Key == Key.Enter)
                {
                    unFilteredDataRecipes();
                    filteredDgridUsedRecipes = false;
                }
            }
            else
            {
                if (e.Key == Key.Enter)
                {
                    Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab3 Rec se filtrira. Rec koja se filtrira :" + tfFilterRecipes.Text);
                    filterDataRecipes();
                    filteredDgridUsedRecipes = true;
                }
            }
        }

        private void btnAddFilterRecipes_MouseEnter(object sender, MouseEventArgs e)
        {
            btnAddFilterRecipes.Background = Brushes.Orange;
        }

        private void btnAddFilterRecipes_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAddFilterRecipes.Background = Brushes.Black;
        }

        private void btnAddFilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            Logger.writeNode(Constants.INFORMATION, "Tab3 PodTab3 Rec se filtrira. Rec koja se filtrira :" + tfFilterRecipes.Text);
            filterDataRecipes();
            filteredDgridUsedRecipes = true;
        }

        private void btnRemoveFilterRecipes_MouseEnter(object sender, MouseEventArgs e)
        {
            btnRemoveFilterRecipes.Background = Brushes.Orange;
        }

        private void btnRemoveFilterRecipes_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRemoveFilterRecipes.Background = Brushes.Black;
        }

        private void btnRemoveFilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            unFilteredDataRecipes();
            filteredDgridUsedRecipes = false;
        }







        #endregion

    }//end of class
}
