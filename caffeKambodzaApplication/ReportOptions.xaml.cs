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
using System.IO;
using System.Data.OleDb;
using System.Collections.ObjectModel;



namespace caffeKambodzaApplication
{
    /// <summary>
    /// Interaction logic for ReportOptions.xaml
    /// </summary>
    public partial class ReportOptions : System.Windows.Controls.UserControl
    {

        private int _oldschedule, _newschedule;
        private bool _okDatabasePath;
        private OleDbConnection conOptions = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0.;Data Source = " + System.Environment.CurrentDirectory + Constants.DATABASECONNECTION_APP);
        private OleDbCommand comOptions;
        private OleDbDataReader drOptions;
        private OleDbCommand com;
        private OleDbDataReader dr;

        private ObservableCollection<ProductWithOrderNumber> productsWithOrder = new ObservableCollection<ProductWithOrderNumber>();

        private void disableTab1()
        {
            btnChooseDirPath.IsEnabled = false;
            tfDir.IsEnabled = false;
            btnSaveDir.IsEnabled = false;

            tfFile.IsEnabled = false;
            btnSaveFile.IsEnabled = false;

            cmbExtension.IsEnabled = false;
            btnSaveExtension.IsEnabled = false;

            btnChooseDatabasePath.IsEnabled = false;
            tfDatabasePath.IsEnabled = false;
            btnSaveDatabasePath.IsEnabled = false;
        }


        private void disableTab2()
        {
            tfCompany.IsEnabled = false;
            btnSaveCompany.IsEnabled = false;
            tfAuthor.IsEnabled = false;
            btnSaveAuthor.IsEnabled = false;
        }

        public ReportOptions()
        {
            InitializeComponent();
            string[] excelExt = new string [2];
            excelExt[0] = ExcelExtensions.xls.ToString().ToUpper();
            excelExt[1] = ExcelExtensions.xlsx.ToString().ToUpper();
            cmbExtension.ItemsSource = excelExt;
            cmbExtension.SelectedIndex = 0;
            disableTab1();

            //return buttons

            if(tblDir2.Text.Equals(Constants.DEFAULTOPTION))
            {
                btnSaveDir2.IsEnabled = false;
            }
            if(tblFile2.Text.Equals(Constants.DEFAULTOPTION))
            {
                btnSaveFile2.IsEnabled = false;
            }
            if(tblExtension2.Text.Equals(Constants.DEFAULTOPTION))
            {
                btnSaveExtension2.IsEnabled = false;
            }
            if(tblDatabasePath2.Text.Equals(Constants.DEFAULTOPTION))
            {
                btnSaveDatabasePath2.IsEnabled = false;
            }

            disableTab2();

            btnSavePathStateOfStorehouse.IsEnabled = false;
            btnChooseDirPathStore.IsEnabled = false;
            tfStateOfStorehouse.IsEnabled = false;


            tfScheduleNew.IsEnabled = false;

            


        }

        #region PathsOptions

        private void btnChooseDirPath_Click(object sender, RoutedEventArgs e)
        {

            string dirPath = String.Empty;
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            // Show open file dialog box 
            DialogResult result = folderDlg.ShowDialog();

            // Process open file dialog box results 
            if (result == DialogResult.OK)
            {  
             dirPath = folderDlg.SelectedPath;
            }

            tfDir.Text = dirPath;
        }

       

        private void btnChooseDatabasePath_Click(object sender, RoutedEventArgs e)
        {
            string databasePath = String.Empty;
            string ext = String.Empty;
            OpenFileDialog openDlg = new OpenFileDialog();

            // Show open file dialog box 
            DialogResult result = openDlg.ShowDialog();

            // Process open file dialog box results 
            if (result == DialogResult.OK)
            {
                databasePath = openDlg.FileName;
                ext = System.IO.Path.GetExtension(openDlg.FileName);
                _okDatabasePath = checkDatabaseExtension(ext);
                if (_okDatabasePath == false)
                {
                    databasePath = String.Empty;
                    System.Windows.Forms.MessageBox.Show("Izabrani fajl " + databasePath + " nije baza podataka! Molimo vas učitajte fajl sa ispravnom ekstenzijom!", "POKUŠAJ UČITAVANJA NEISPRAVNOG FORMATA BAZE PODATAKA");
                    return;
                }
            }

            tfDatabasePath.Text = databasePath;
        }

        private bool checkDatabaseExtension(string ext) 
        {
            string check = ext.Substring(1,ext.Length-1);
            bool res;


            DatabaseExtensions i;
            string currExt = DatabaseExtensions.abcddb.ToString();

            for (i = DatabaseExtensions.abcddb; i <= DatabaseExtensions.prc; i++)
            {
                 currExt = i.ToString();
                 if (currExt.Equals(check) == true) return true;
            }

            return false;
        }

        private void btnSaveDir_Click(object sender, RoutedEventArgs e)
        {
            
            tblDir2.Text = tfDir.Text;
            tblDir2.Foreground = Brushes.DarkOliveGreen;
            btnSaveDir2.IsEnabled = true;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE zapamcen      direktorijum kreiranja izveštaja. Direktorijum je :" + tfDir.Text);
            tfDir.Text = string.Empty;

            try
            {
                conOptions.Open();
                string query = "UPDATE savedOptions SET Directorium ='" + tblDir2.Text + "' WHERE Options='options';";
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

        private void btnSaveDir2_Click(object sender, RoutedEventArgs e)
        {
            if (chkbMask.IsChecked == true)
            {
                tblDir2.Foreground = Brushes.Azure;
            }
            else 
            {
                tblDir2.Foreground = Brushes.Blue;
            }
            
            tblDir2.Text = Constants.DEFAULTOPTION;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE vracen      direktorijum kreiranja izveštaja    na podrazumevanu vrednost.");
            btnSaveDir2.IsEnabled = false;
            try
            {
                conOptions.Open();
                string query = "UPDATE savedOptions SET Directorium ='" + tblInitialDir.Text + "' WHERE Options='options';";
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

        private void btnSaveFile_btnSaveFile(object sender, RoutedEventArgs e)
        {
            tblFile2.Text = tfFile.Text;
            tblFile2.Foreground = Brushes.DarkOliveGreen;
            btnSaveFile2.IsEnabled = true;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE zapamcen      naziv izveštaja. Naziv izveštaja je :" + tfFile.Text);
            tfFile.Text = string.Empty;

            try
            {
                conOptions.Open();
                string query = "UPDATE savedOptions SET NameCreatedReport ='" + tblFile2.Text + "' WHERE Options='options';";
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

        private void btnSaveFile2_Click(object sender, RoutedEventArgs e)
        {

            if (chkbMask.IsChecked == true)
            {
                tblFile2.Foreground = Brushes.Azure;
            }
            else
            {
                tblFile2.Foreground = Brushes.Blue;
            }


            tblFile2.Text = Constants.DEFAULTOPTION;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE vracen      naziv izveštaja    na podrazumevanu vrednost.");
            btnSaveFile2.IsEnabled = false;

            try
            {
                conOptions.Open();
                string query = "UPDATE savedOptions SET NameCreatedReport ='" + tblInitialFile.Text + "' WHERE Options='options';";
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

        private void btnSaveExtension_Click(object sender, RoutedEventArgs e)
        {
            tblExtension2.Text = cmbExtension.SelectedItem.ToString();
            tblExtension2.Foreground = Brushes.DarkOliveGreen;
            btnSaveExtension2.IsEnabled = true;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE zapamcena      ekstenzija izveštaja. Ekstenzija izveštaja je :" + tblExtension2.Text);
            cmbExtension.SelectedIndex = 0;

            try
            {
                conOptions.Open();
                string query = "UPDATE savedOptions SET ExtensionOfCreatedReport ='" + tblExtension2.Text + "' WHERE Options='options';";
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

        private void btnSaveExtension2_Click(object sender, RoutedEventArgs e)
        {
            if (chkbMask.IsChecked == true)
            {
                tblExtension2.Foreground = Brushes.Azure;
            }
            else
            {
                tblExtension2.Foreground = Brushes.Blue;
            }


            tblExtension2.Text = Constants.DEFAULTOPTION;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE vracena      ekstenzija izveštaja    na podrazumevanu vrednost." );
            btnSaveExtension2.IsEnabled = false;

            try
            {
                conOptions.Open();
                string query = "UPDATE savedOptions SET ExtensionOfCreatedReport ='" + tblInitialExtension.Text + "' WHERE Options='options';";
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

        private void btnSaveDatabasePath_Click(object sender, RoutedEventArgs e)
        {
            tblDatabasePath2.Text = tfDatabasePath.Text;
            tblDatabasePath2.Foreground = Brushes.DarkOliveGreen;
            btnSaveDatabasePath2.IsEnabled = true;
            tfDatabasePath.Text = String.Empty;


            try
            {
                conOptions.Open();
                string query = "UPDATE savedOptions SET DatabasePath ='" + tblDatabasePath2.Text + "' WHERE Options='options';";
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


        private void btnSaveDatabasePath2_Click(object sender, RoutedEventArgs e)
        {
            if (chkbMask.IsChecked == true)
            {
                tblDatabasePath2.Foreground = Brushes.Azure;
            }
            else
            {
                tblDatabasePath2.Foreground = Brushes.Blue;
            }

            tblDatabasePath2.Text = Constants.DEFAULTOPTION;
            btnSaveDatabasePath2.IsEnabled = false;

            try
            {
                conOptions.Open();
                string query = "UPDATE savedOptions SET DatabasePath ='" + tblInitialDatabasePath.Text + "' WHERE Options='options';";
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

        

        private void cmb_Checked(object sender, RoutedEventArgs e)
        {
            btnChooseDirPath.IsEnabled = true;
            tfDir.IsEnabled = true;
            //btnSaveDir.IsEnabled = true;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE čekirana opcija     Želim uneti direktorijum kreiranja izveštaja");
               
        }

        private void cmb_Unchecked(object sender, RoutedEventArgs e)
        {
            btnChooseDirPath.IsEnabled = false;
            tfDir.IsEnabled = false;
            btnSaveDir.IsEnabled = false;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE odčekirana opcija     Želim uneti direktorijum kreiranja izveštaja");
        }

        private void cmb2_Checked(object sender, RoutedEventArgs e)
        {
            tfFile.IsEnabled = true;
            //btnSaveFile.IsEnabled = true;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE čekirana opcija     Želim uneti naziv izveštaja");
        }

        private void cmb2_Unchecked(object sender, RoutedEventArgs e)
        {
            tfFile.IsEnabled = false;
            btnSaveFile.IsEnabled = false;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE odčekirana opcija     Želim uneti naziv izveštaja");
        }

        private void cm3_Checked(object sender, RoutedEventArgs e)
        {
            cmbExtension.IsEnabled = true;
            btnSaveExtension.IsEnabled = true;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE čekirana opcija     Želim uneti ekstenziju izveštaja");
        }

        private void cm3_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbExtension.IsEnabled = false;
            btnSaveExtension.IsEnabled = false;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE PUTANJE odčekirana opcija     Želim uneti ekstenziju izveštaja");
        }

        private void cm4_Checked(object sender, RoutedEventArgs e)
        {
            btnChooseDatabasePath.IsEnabled = true;
            tfDatabasePath.IsEnabled = true;
            //btnSaveDatabasePath.IsEnabled = true;
        }

        private void cm4_Unchecked(object sender, RoutedEventArgs e)
        {
            btnChooseDatabasePath.IsEnabled = false;
            tfDatabasePath.IsEnabled = false;
            btnSaveDatabasePath.IsEnabled = false;
        }

        private void tfDir_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tfDir.Text.Equals(String.Empty) == false)  btnSaveDir.IsEnabled = true;
            else btnSaveDir.IsEnabled = false;
        }

        private void tfFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfFile.Text.Equals(String.Empty) == false) btnSaveFile.IsEnabled = true;
            else btnSaveFile.IsEnabled = false;
        }

        private void tfDatabasePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfDatabasePath.Text.Equals(String.Empty) == false) btnSaveDatabasePath.IsEnabled = true;
            else btnSaveDatabasePath.IsEnabled = false;
        }

        #endregion




        #region ApplicationOptions


        private void cmbAppSound_Checked(object sender, RoutedEventArgs e)
        {
            tblSound.Text = Constants.SOUNDON;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE čekirana opcija     Uključi zvuk aplikacije");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET CodeProductCheck ='" + Constants.YES + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void cmbAppSound_Unchecked(object sender, RoutedEventArgs e)
        {
            tblSound.Text = Constants.SOUNDOFF;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE odčekirana opcija     Uključi zvuk aplikacije");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET CodeProductCheck ='" + Constants.NO + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void cmbAppOpen_Checked(object sender, RoutedEventArgs e)
        {
            tblOpenWhenCreated.Text = Constants.OPENFILE;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE čekirana opcija     Otvori fajl kada se završi sa kreiranjem");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET OpenAfterCreating ='" + Constants.YES + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void cmbAppOpen_Unchecked(object sender, RoutedEventArgs e)
        {
            tblOpenWhenCreated.Text = Constants.NOTOPENFILE;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE odčekirana opcija     Otvori fajl kada se završi sa kreiranjem");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET OpenAfterCreating ='" + Constants.NO + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void cmbAppCompany_Checked(object sender, RoutedEventArgs e)
        {
            tfCompany.IsEnabled = true;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE čekirana opcija     Želim uneti ime firme");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsNameOfCompanyChecked ='" + Constants.YES + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void cmbAppCompany_Unchecked(object sender, RoutedEventArgs e)
        {
            tfCompany.IsEnabled = false;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE odčekirana opcija     Želim uneti ime firme");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsNameOfCompanyChecked ='" + Constants.NO + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void cmbAppAuthor_Checked(object sender, RoutedEventArgs e)
        {
            tfAuthor.IsEnabled = true;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE čekirana opcija     Želim uneti autora izveštaja");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsAuthorChecked ='" + Constants.YES + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void cmbAppAuthor_Unchecked(object sender, RoutedEventArgs e)
        {
            tfAuthor.IsEnabled = false;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE odčekirana opcija     Želim uneti autora izveštaja");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsAuthorChecked ='" + Constants.NO + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void cmbAppStateStorehouse_Checked(object sender, RoutedEventArgs e)
        {
            btnChooseDirPathStore.IsEnabled = true;
            tfStateOfStorehouse.IsEnabled = true;

            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE čekirana opcija     Želim promeniti putanju stanja magacina");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsPathStorehouseChecked ='" + Constants.YES + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void cmbAppStateStorehouse_Unchecked(object sender, RoutedEventArgs e)
        {
            btnChooseDirPathStore.IsEnabled = false;
            tfStateOfStorehouse.IsEnabled = false;

            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE odčekirana opcija     Želim promeniti putanju stanja magacina");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsPathStorehouseChecked ='" + Constants.NO + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void btnSaveCompany_Click(object sender, RoutedEventArgs e)
        {
            tblCompany2.Text = tfCompany.Text;
            tblCompany2.Foreground = Brushes.DarkOliveGreen;
            btnreturnCompany.IsEnabled = true;
            Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE zapamcen      naziv firme. Naziv firme je :" + tblCompany2.Text);
            tfCompany.Text = string.Empty;

            try
            {
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET NameOfCompany ='" + tblCompany2.Text + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void btnSaveAuthor_Click(object sender, RoutedEventArgs e)
        {
            tblAuthor2.Text = tfAuthor.Text;
            tblAuthor2.Foreground = Brushes.DarkOliveGreen;
            btnReturnAuthor.IsEnabled = true;
            tfAuthor.Text = string.Empty;

            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE zapamcen      naziv autora izvestaja. Naziv autora izvestaja je :" + tblAuthor2.Text);
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET Author ='" + tblAuthor2.Text + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void btnreturnCompany_Click(object sender, RoutedEventArgs e)
        {
            if (chkbMask.IsChecked == true)
            {
                tblCompany2.Foreground = Brushes.Azure;
            }
            else
            {
                tblCompany2.Foreground = Brushes.Blue;
            }
            
            tblCompany2.Text = Constants.DEFAULTOPTION;
            btnreturnCompany.IsEnabled = false;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE vracen      Naziv firme    na podrazumevanu vrednost.");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET NameOfCompany ='" + tblInitialCompany.Text + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void btnReturnAuthor_Click(object sender, RoutedEventArgs e)
        {

            if (chkbMask.IsChecked == true)
            {
                tblAuthor2.Foreground = Brushes.Azure;
            }
            else
            {
                tblAuthor2.Foreground = Brushes.Blue;
            }

            tblAuthor2.Text = Constants.DEFAULTOPTION;
            btnReturnAuthor.IsEnabled = false;
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE vracen      Naziv autora izveštaja    na podrazumevanu vrednost.");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET Author ='" + tblInitialAuthor.Text + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void tfCompany_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfCompany.Text.Equals(String.Empty) == false) btnSaveCompany.IsEnabled = true;
            else btnSaveCompany.IsEnabled = false;
        }


        private void tfAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tfAuthor.Text.Equals(String.Empty) == false) btnSaveAuthor.IsEnabled = true;
            else btnSaveAuthor.IsEnabled = false;
        }


        private void rbtnLandscape_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE čekirana opcija     Landscape");
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE odčekirana opcija     Portrait");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsLandscape ='" + Constants.YES + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void rbtnPortrait_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE čekirana opcija     Portrait");
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE odčekirana opcija     Landscape");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsLandscape ='" + Constants.NO + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        #endregion

        #region isCodeProductWrite


        private void chbtnWriteCode_Checked(object sender, RoutedEventArgs e)
        {

            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE čekirana opcija     Uključi upis šifre proizvoda u izveštaj");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsCodeProductWrite ='" + Constants.YES + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                MainWindow window = (MainWindow)MainWindow.GetWindow(this);
                window.isCodeProductWrite = true;

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        private void chbtnWriteCode_Unchecked(object sender, RoutedEventArgs e)
        {

            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE odčekirana opcija     Uključi upis šifre proizvoda u izveštaj");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsCodeProductWrite ='" + Constants.NO + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();


                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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



        #endregion


        #region MaskChecked

        private void chkbMask_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE čekirana opcija     Uključi masku");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsMaskChecked ='" + Constants.YES + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
                com = new OleDbCommand(queryStorehouse, conOptions);
                com.ExecuteNonQuery();

               
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window2 = (MainWindow)MainWindow.GetWindow(this);
                window2.savenumofitemsEVERCreated();
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

            MainWindow window = (MainWindow)MainWindow.GetWindow(this);
            window.gridTab1.Background = (Brush)FindResource("Gradient4");
            window.dataGrid1.AlternatingRowBackground = (Brush)FindResource("Gradient3");
            window.tfAmount.Background = System.Windows.Media.Brushes.Black;
            window.tfAmount.Foreground = System.Windows.Media.Brushes.White;
            window.gridTab2.Background = (Brush)FindResource("Gradient4");

            this.gridRoot.Background = (Brush)FindResource("Gradient4");
            this.gridRootApp.Background = (Brush)FindResource("Gradient4");

            //set azure color for path options
            tblInitialDir.Foreground = System.Windows.Media.Brushes.Azure;
            tblInitialFile.Foreground = System.Windows.Media.Brushes.Azure;
            tblInitialExtension.Foreground = System.Windows.Media.Brushes.Azure;
            tblInitialDatabasePath.Foreground = System.Windows.Media.Brushes.Azure;

            if (tblDir2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblDir2.Foreground = System.Windows.Media.Brushes.Azure;
            }
            if (tblFile2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblFile2.Foreground = System.Windows.Media.Brushes.Azure;
            }
            if (tblExtension2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblExtension2.Foreground = System.Windows.Media.Brushes.Azure;
            }
            if (tblDatabasePath2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblDatabasePath2.Foreground = System.Windows.Media.Brushes.Azure;
            }

            //set azure color for application options
            tblSound.Foreground = Brushes.Azure;
            tblOpenWhenCreated.Foreground = Brushes.Azure;
            tblInitialCompany.Foreground = Brushes.Azure;
            tblInitialAuthor.Foreground = Brushes.Azure;

            if (tblCompany2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblCompany2.Foreground = System.Windows.Media.Brushes.Azure;
            }

            if (tblAuthor2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblAuthor2.Foreground = System.Windows.Media.Brushes.Azure;
            }


            //schedule report options
            Object obj1 = this.gridOptionsRoot.Resources["Gradient4"];
            Object obj3 = this.gridOptionsRoot.Resources["Gradient3"];
            window.options.gridScheduleRoot.Background = (Brush)obj1;
            window.options.dataGridSchedule.AlternatingRowBackground = (Brush)obj3;

            //storehouse tab5
            window.storehouse.gridTab5.Background = (Brush)obj1;
            window.storehouse.tblFilterStatusTab5.Background = (Brush)obj1;
            window.storehouse.dataGridReadStateStorehouse.AlternatingRowBackground = (Brush)obj3;
            //history tab [tab1]
            window.history.gridHistoryTab1.Background = (Brush)obj1;
            window.history.tblFilterStatusTab1.Background = (Brush)obj1;
            window.history.dataGridReadHistoryRecipes.AlternatingRowBackground = (Brush)obj3;
            //history tab [tab2]
            window.history.gridHistoryTab2.Background = (Brush)obj1;
            window.history.tblFilterStatusTab2.Background = (Brush)obj1;
            window.history.dataGridReadHistoryPrices.AlternatingRowBackground = (Brush)obj3;
            //overviewStorehouse tab [tab1]
            window.overviewStorehouse.gridHistoryTab1.Background = (Brush)obj1;
            window.overviewStorehouse.tblFilterStatusTab1.Background = (Brush)obj1;
            window.overviewStorehouse.dataGridReadStore.AlternatingRowBackground = (Brush)obj3;
            //overviewStorehouse tab [tab2]
            window.overviewStorehouse.gridHistoryTab2.Background = (Brush)obj1;
            window.overviewStorehouse.tblFilterStatusTab2.Background = (Brush)obj1;
            window.overviewStorehouse.dataGridReadStoreTab2.AlternatingRowBackground = (Brush)obj3;
            //overviewStorehouse tab [tab3]
            window.overviewStorehouse.gridHistoryTab3.Background = (Brush)obj1;
            window.overviewStorehouse.tblFilterStatusTab3.Background = (Brush)obj1;
            window.overviewStorehouse.dataGridReadStoreTab3.AlternatingRowBackground = (Brush)obj3;
            //createdReports [tab1]
            window.createdReports.gridHistoryTab1.Background = (Brush)obj1;
            window.createdReports.dataGridRead.AlternatingRowBackground = (Brush)obj3;
            //createdReports [tab2]
            window.createdReports.gridHistoryTab2.Background = (Brush)obj1;
            window.createdReports.dataGridReadByProduct.AlternatingRowBackground = (Brush)obj3;
            //createdReports [tab3]
            window.createdReports.gridHistoryTab3.Background = (Brush)obj1;
            window.createdReports.dataGridReadDeletion.AlternatingRowBackground = (Brush)obj3;
            //createdReports [tab4]
            window.createdReports.gridHistoryTab4.Background = (Brush)obj1;
            window.createdReports.dataGridReadCorrection.AlternatingRowBackground = (Brush)obj3;
            //storehouse [tab1]
            window.storehouse.gridTab1.Background = (Brush)obj1;
            //storehouse [tab2]
            window.storehouse.gridTab2.Background = (Brush)obj1;
            //storehouse [tab3]
            window.storehouse.gridTab3.Background = (Brush)obj1;
            //storehouse [tab4]
            window.storehouse.gridTab4.Background = (Brush)obj1;
            //selectUpdateConnProdStore
            window.selectUpdateConnProdStore.gridAllFilterData.Background = (Brush)obj1;
            window.selectUpdateConnProdStore.gridtfsPart.Background = (Brush)obj1;
            window.selectUpdateConnProdStore.tblFilterStatusRecipes.Background = (Brush)obj1;
            //enterStoreItemsTab2
            window.enterStoreItemsTab2.gridRoot.Background = (Brush)obj1;
        }

        private void chkbMask_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE odčekirana opcija     Uključi masku");
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET IsMaskChecked ='" + Constants.NO + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();


                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
                com = new OleDbCommand(queryStorehouse, conOptions);
                com.ExecuteNonQuery();

                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                MainWindow window2 = (MainWindow)MainWindow.GetWindow(this);
                window2.savenumofitemsEVERCreated();
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

            MainWindow window = (MainWindow)MainWindow.GetWindow(this);
            window.gridTab1.Background = System.Windows.Media.Brushes.White;
            window.dataGrid1.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            window.tfAmount.Background = System.Windows.Media.Brushes.LightGreen;
            window.tfAmount.Foreground = System.Windows.Media.Brushes.Black;
            window.gridTab2.Background = System.Windows.Media.Brushes.White;

            this.gridRoot.Background = System.Windows.Media.Brushes.White;
            this.gridRootApp.Background = System.Windows.Media.Brushes.White;
            
            //set blue color for path options
            tblInitialDir.Foreground = System.Windows.Media.Brushes.Blue;
            tblInitialFile.Foreground = System.Windows.Media.Brushes.Blue;
            tblInitialExtension.Foreground = System.Windows.Media.Brushes.Blue;
            tblInitialDatabasePath.Foreground = System.Windows.Media.Brushes.Blue;

            if (tblDir2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblDir2.Foreground = System.Windows.Media.Brushes.Blue;
            }
            if (tblFile2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblFile2.Foreground = System.Windows.Media.Brushes.Blue;
            }
            if (tblExtension2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblExtension2.Foreground = System.Windows.Media.Brushes.Blue;
            }
            if (tblDatabasePath2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblDatabasePath2.Foreground = System.Windows.Media.Brushes.Blue;
            }

            //set blue color for application options
            tblSound.Foreground = Brushes.Blue;
            tblOpenWhenCreated.Foreground = Brushes.Blue;
            tblInitialCompany.Foreground = Brushes.Blue;
            tblInitialAuthor.Foreground = Brushes.Blue;

            if (tblCompany2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblCompany2.Foreground = System.Windows.Media.Brushes.Blue;
            }

            if (tblAuthor2.Text.Equals(Constants.DEFAULTOPTION))
            {
                tblAuthor2.Foreground = System.Windows.Media.Brushes.Blue;
            }


            //schedule report options
            window.options.gridScheduleRoot.Background = System.Windows.Media.Brushes.White;
            window.options.dataGridSchedule.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;

            //storehouse tab5
            window.storehouse.gridTab5.Background = System.Windows.Media.Brushes.White;
            window.storehouse.tblFilterStatusTab5.Background = System.Windows.Media.Brushes.White;
            window.storehouse.dataGridReadStateStorehouse.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //history tab [tab1]
            window.history.gridHistoryTab1.Background = System.Windows.Media.Brushes.White;
            window.history.tblFilterStatusTab1.Background = System.Windows.Media.Brushes.White;
            window.history.dataGridReadHistoryRecipes.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //history tab [tab2]
            window.history.gridHistoryTab2.Background = System.Windows.Media.Brushes.White;
            window.history.tblFilterStatusTab2.Background = System.Windows.Media.Brushes.White;
            window.history.dataGridReadHistoryPrices.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //overviewStorehouse tab [tab1]
            window.overviewStorehouse.gridHistoryTab1.Background = System.Windows.Media.Brushes.White;
            window.overviewStorehouse.tblFilterStatusTab1.Background = System.Windows.Media.Brushes.White;
            window.overviewStorehouse.dataGridReadStore.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //overviewStorehouse tab [tab2]
            window.overviewStorehouse.gridHistoryTab2.Background = System.Windows.Media.Brushes.White;
            window.overviewStorehouse.tblFilterStatusTab2.Background = System.Windows.Media.Brushes.White;
            window.overviewStorehouse.dataGridReadStoreTab2.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //overviewStorehouse tab [tab3]
            window.overviewStorehouse.gridHistoryTab3.Background = System.Windows.Media.Brushes.White;
            window.overviewStorehouse.tblFilterStatusTab3.Background = System.Windows.Media.Brushes.White;
            window.overviewStorehouse.dataGridReadStoreTab3.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //createdReports [tab1]
            window.createdReports.gridHistoryTab1.Background = System.Windows.Media.Brushes.White;
            window.createdReports.dataGridRead.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //createdReports [tab2]
            window.createdReports.gridHistoryTab2.Background = System.Windows.Media.Brushes.White;
            window.createdReports.dataGridReadByProduct.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //createdReports [tab3]
            window.createdReports.gridHistoryTab3.Background = System.Windows.Media.Brushes.White;
            window.createdReports.dataGridReadDeletion.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //createdReports [tab4]
            window.createdReports.gridHistoryTab4.Background = System.Windows.Media.Brushes.White;
            window.createdReports.dataGridReadCorrection.AlternatingRowBackground = System.Windows.Media.Brushes.LightGray;
            //storehouse [tab1]
            window.storehouse.gridTab1.Background = System.Windows.Media.Brushes.White;
            //storehouse [tab2]
            window.storehouse.gridTab2.Background = System.Windows.Media.Brushes.White;
            //storehouse [tab3]
            window.storehouse.gridTab3.Background = System.Windows.Media.Brushes.White;
            //storehouse [tab4]
            window.storehouse.gridTab4.Background = System.Windows.Media.Brushes.White;
            //selectUpdateConnProdStore
            window.selectUpdateConnProdStore.gridAllFilterData.Background = System.Windows.Media.Brushes.White;
            window.selectUpdateConnProdStore.gridtfsPart.Background = System.Windows.Media.Brushes.White;
            window.selectUpdateConnProdStore.tblFilterStatusRecipes.Background = System.Windows.Media.Brushes.White;
            //enterStoreItemsTab2
            window.enterStoreItemsTab2.gridRoot.Background = System.Windows.Media.Brushes.White;


        }

        private void tfStateOfStorehouse_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (tfStateOfStorehouse.Text.Equals(String.Empty) == false) btnSavePathStateOfStorehouse.IsEnabled = true;
            else btnSavePathStateOfStorehouse.IsEnabled = false;
        }

        private void btnChooseDirPathStore_Click(object sender, RoutedEventArgs e)
        {

            string dirPathState = String.Empty;
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            // Show open file dialog box 
            DialogResult result = folderDlg.ShowDialog();

            // Process open file dialog box results 
            if (result == DialogResult.OK)
            {
                dirPathState = folderDlg.SelectedPath;
            }

            tfStateOfStorehouse.Text = dirPathState;
        }


        private void btnSavePathStateOfStorehouse_Click(object sender, RoutedEventArgs e)
        {
            tblPathStateStore2.Text = tfStateOfStorehouse.Text;

            tfStateOfStorehouse.Text = string.Empty;

            try
            {
                Logger.writeNode(Constants.INFORMATION, "Tab OPCIJE APLIKACIJE zapamcen     putanja kreiranja stanja magacina. Putanja kreiranja stanja magacina je :" + tblPathStateStore2.Text);
                conOptions.Open();
                string query = "UPDATE optionsOfApplication SET PathStateStorehouse ='" + tblPathStateStore2.Text + "' WHERE Options='application';";
                comOptions = new OleDbCommand(query, conOptions);
                comOptions.ExecuteNonQuery();

                string queryStorehouse = "UPDATE optionsOfApplication SET LastDateTimeUpdated = " + "'" + DateTime.Now + "'" + "WHERE Options='application' ;";
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

        #endregion



        #region Tab3_Schedule

        private void dataGridSchedule_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridSchedule.SelectedItem != null)
            {
                MainWindow window = (MainWindow)Window.GetWindow(this);

                int selectedIndex = dataGridSchedule.SelectedIndex;
                //string selectedItem = dataGridSchedule.SelectedCells.ToString();
                string selectedItem = dataGridSchedule.SelectedValue.ToString();

                //tfProductCode.Text = arr[0];
                //tfProductKind.Text = arr[1];
                //tfScheduleOld.Text = arr[2];
                tfProductKind.Text = selectedItem;
                tfScheduleOld.Text = window.ProductsWithOrder[selectedIndex].OrderNumber.ToString();
                tfScheduleNew.IsEnabled = true;
                Logger.writeNode(Constants.INFORMATION, "Tab7 PodTab3 Selektovanje datagrida za promenu prikaza u knjizi sanka. ");



                bool isN = int.TryParse(tfScheduleOld.Text, out _oldschedule);
              

            }
        }


        private void btnChangeSchedule_Click(object sender, RoutedEventArgs e)
        {
           
            MainWindow window = (MainWindow)Window.GetWindow(this);

            bool isN = int.TryParse(tfScheduleNew.Text, out _newschedule);

            if (isN == true)
            {
                if (_newschedule < 1 || _newschedule > window.ProductsWithOrder.Count)
                {
                    System.Windows.MessageBox.Show(" Novi uneti redni broj mora biti u opsegu od 1 do " + window.ProductsWithOrder.Count);
                    return;
                }
            }


            ProductWithOrderNumber temp = new ProductWithOrderNumber() ;

            temp.CodeProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).CodeProduct;//kafa
            temp.Amount = window.ProductsWithOrder.ElementAt(_oldschedule - 1).Amount;
            temp.KindOfProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).KindOfProduct;
            temp.MeasureProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).MeasureProduct;
            temp.NameProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).NameProduct;
            temp.Price = window.ProductsWithOrder.ElementAt(_oldschedule - 1).Price;
            temp.StoreItemProducts = window.ProductsWithOrder.ElementAt(_oldschedule - 1).StoreItemProducts;
            temp.WayDisplayBookBar = window.ProductsWithOrder.ElementAt(_oldschedule - 1).WayDisplayBookBar;
            temp.OrderNumber = _newschedule;

           
            window.ProductsWithOrder.RemoveAt(_oldschedule - 1);
            for (int i = 0; i < window.ProductsWithOrder.Count; i++)
            {
                if(i >= _oldschedule - 1)
                {
                    window.ProductsWithOrder[i].OrderNumber = window.ProductsWithOrder[i].OrderNumber - 1; 
                }
            }
            window.ProductsWithOrder.Insert(_newschedule - 1, temp);
            for (int i = 0; i < window.ProductsWithOrder.Count; i++)
            {
                if (i >= _newschedule)
                {
                    window.ProductsWithOrder[i].OrderNumber = window.ProductsWithOrder[i].OrderNumber + 1;
                }
            }

            productsWithOrder = window.ProductsWithOrder;
            dataGridSchedule.ItemsSource = productsWithOrder;
            dataGridSchedule.Items.Refresh();

            try
            {
                conOptions.Open();

                // update vinjak [selected item]
                string querySwap = "DELETE FROM productsWithOrderNumber;";
                com = new OleDbCommand(querySwap, conOptions);
                com.ExecuteNonQuery();

                for (int i = 0; i < window.ProductsWithOrder.Count; i++)
                {
                    string queryInsert = "INSERT INTO productsWithOrderNumber (CodeProduct, KindOfProduct, NameProduct, MeasureProduct, Price, Valuta, WayDisplayBookBar, NumberOrder) VALUES (" + "'" + window.ProductsWithOrder[i].CodeProduct + "','" + window.ProductsWithOrder[i].KindOfProduct + "','" + window.ProductsWithOrder[i].NameProduct + "','" + window.ProductsWithOrder[i].MeasureProduct + "','" + window.ProductsWithOrder[i].Price + "','" + "din" + "','" + window.ProductsWithOrder[i].WayDisplayBookBar + "','" + window.ProductsWithOrder[i].OrderNumber + "'" + ");";
                    com = new OleDbCommand(queryInsert, conOptions);
                    com.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
            }
            finally
            {
                if (conOptions != null)
                {
                    conOptions.Close();
                }

            }

            //ProductWithOrderNumber temp = new ProductWithOrderNumber() ;// here is removed product
            //ProductWithOrderNumber selectedProductAfterChanged = new ProductWithOrderNumber();


          
            //temp.CodeProduct = window.ProductsWithOrder.ElementAt(_newschedule - 1).CodeProduct;//kafa
            //temp.Amount = window.ProductsWithOrder.ElementAt(_newschedule - 1).Amount;
            //temp.KindOfProduct = window.ProductsWithOrder.ElementAt(_newschedule - 1).KindOfProduct;
            //temp.MeasureProduct = window.ProductsWithOrder.ElementAt(_newschedule - 1).MeasureProduct;
            //temp.NameProduct = window.ProductsWithOrder.ElementAt(_newschedule - 1).NameProduct;
            //temp.Price = window.ProductsWithOrder.ElementAt(_newschedule - 1).Price;
            //temp.StoreItemProducts = window.ProductsWithOrder.ElementAt(_newschedule - 1).StoreItemProducts;
            //temp.WayDisplayBookBar = window.ProductsWithOrder.ElementAt(_newschedule - 1).WayDisplayBookBar;
            //temp.OrderNumber = _oldschedule;

            //// set selected product at the new place
            //window.ProductsWithOrder.ElementAt(_newschedule - 1).CodeProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).CodeProduct;//vinjak
            //window.ProductsWithOrder.ElementAt(_newschedule - 1).Amount = window.ProductsWithOrder.ElementAt(_oldschedule - 1).Amount;
            //window.ProductsWithOrder.ElementAt(_newschedule - 1).KindOfProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).KindOfProduct;
            //window.ProductsWithOrder.ElementAt(_newschedule - 1).MeasureProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).MeasureProduct;
            //window.ProductsWithOrder.ElementAt(_newschedule - 1).NameProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).NameProduct;
            //window.ProductsWithOrder.ElementAt(_newschedule - 1).Price = window.ProductsWithOrder.ElementAt(_oldschedule - 1).Price;
            //window.ProductsWithOrder.ElementAt(_newschedule - 1).StoreItemProducts = window.ProductsWithOrder.ElementAt(_oldschedule - 1).StoreItemProducts;
            //window.ProductsWithOrder.ElementAt(_newschedule - 1).WayDisplayBookBar = window.ProductsWithOrder.ElementAt(_oldschedule - 1).WayDisplayBookBar;
            //window.ProductsWithOrder.ElementAt(_newschedule - 1).OrderNumber = _newschedule;

            ////save selected changed product
            //selectedProductAfterChanged.CodeProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).CodeProduct;//vinjak
            //selectedProductAfterChanged.Amount = window.ProductsWithOrder.ElementAt(_oldschedule - 1).Amount;
            //selectedProductAfterChanged.KindOfProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).KindOfProduct;
            //selectedProductAfterChanged.MeasureProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).MeasureProduct;
            //selectedProductAfterChanged.NameProduct = window.ProductsWithOrder.ElementAt(_oldschedule - 1).NameProduct;
            //selectedProductAfterChanged.Price = window.ProductsWithOrder.ElementAt(_oldschedule - 1).Price;
            //selectedProductAfterChanged.StoreItemProducts = window.ProductsWithOrder.ElementAt(_oldschedule - 1).StoreItemProducts;
            //selectedProductAfterChanged.WayDisplayBookBar = window.ProductsWithOrder.ElementAt(_oldschedule - 1).WayDisplayBookBar;
            //selectedProductAfterChanged.OrderNumber = _newschedule;


            //// set removed product with new place to the old place of selected product
            //window.ProductsWithOrder.ElementAt(_oldschedule - 1).CodeProduct = temp.CodeProduct;//kafa
            //window.ProductsWithOrder.ElementAt(_oldschedule - 1).Amount = temp.Amount;
            //window.ProductsWithOrder.ElementAt(_oldschedule - 1).KindOfProduct = temp.KindOfProduct;
            //window.ProductsWithOrder.ElementAt(_oldschedule - 1).MeasureProduct = temp.MeasureProduct;
            //window.ProductsWithOrder.ElementAt(_oldschedule - 1).NameProduct = temp.NameProduct;
            //window.ProductsWithOrder.ElementAt(_oldschedule - 1).Price = temp.Price;
            //window.ProductsWithOrder.ElementAt(_oldschedule - 1).StoreItemProducts = temp.StoreItemProducts;
            //window.ProductsWithOrder.ElementAt(_oldschedule - 1).WayDisplayBookBar = temp.WayDisplayBookBar;
            //window.ProductsWithOrder.ElementAt(_oldschedule - 1).OrderNumber = _oldschedule;

           
            //dataGridSchedule.ItemsSource = window.ProductsWithOrder;
           
            
            //// ProductsWithNames change schedule
            //window.ProductsWithOrderNames.Clear();
            //window.ProductsWithOrderNames.Add(Constants.CHOOSEPRODUCT);
            //for (int i = 0; i < window.ProductsWithOrder.Count; i++)
            //{
            //    window.ProductsWithOrderNames.Add(window.ProductsWithOrder.ElementAt(i).KindOfProduct);
            //}

            //window.cmbNameProductTab1.ItemsSource = window.ProductsWithOrderNames;
            //window.cmbNameProductTab1.SelectedIndex = 0;

            ////update textboxes
            ////tfProductCode.Text = temp.CodeProduct;
            //tfProductKind.Text = temp.KindOfProduct;
            //tfScheduleOld.Text = temp.OrderNumber.ToString();
            //tfScheduleNew.Text = String.Empty;

            //// update new schedule in database [table productsWithOrderNumber]
            
            //try
            //{
            //    conOptions.Open();

            //    // update vinjak [selected item]
            //    string querySwap = "UPDATE productsWithOrderNumber SET CodeProduct  = " + "'" + selectedProductAfterChanged.CodeProduct + "'" + ", KindOfProduct = " + "'" + selectedProductAfterChanged.KindOfProduct + "'" + ", NameProduct = " + "'" + selectedProductAfterChanged.NameProduct + "'" + ", MeasureProduct = " + "'" + selectedProductAfterChanged.MeasureProduct + "'" + ", Price = " + "'" + selectedProductAfterChanged.Price.ToString() + "'" + ", Valuta = " + "'" + window.Currency + "'" + ", LastDateTimeUpdated = " + "'" + DateTime.Now.ToString() + "'" + ", WayDisplayBookBar = " + "'" + selectedProductAfterChanged.WayDisplayBookBar + "'" + "  WHERE NumberOrder = " + "'" + selectedProductAfterChanged.OrderNumber + "'" + ";";
            //    com = new OleDbCommand(querySwap, conOptions);
            //    com.ExecuteNonQuery();

            //    // update kafa [removed item]
            //    querySwap = "UPDATE productsWithOrderNumber SET CodeProduct  = " + "'" + temp.CodeProduct + "'" + ", KindOfProduct = " + "'" + temp.KindOfProduct + "'" + ", NameProduct = " + "'" + temp.NameProduct + "'" + ", MeasureProduct = " + "'" + temp.MeasureProduct + "'" + ", Price = " + "'" + temp.Price.ToString() + "'" + ", Valuta = " + "'" + window.Currency + "'" + ", LastDateTimeUpdated = " + "'" + DateTime.Now.ToString() + "'" + ", WayDisplayBookBar = " + "'" + temp.WayDisplayBookBar + "'" + "  WHERE NumberOrder = " + "'" + temp.OrderNumber.ToString() + "'" + ";";
            //    com = new OleDbCommand(querySwap, conOptions);
            //    com.ExecuteNonQuery();


               

            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.Message);
            //    Logger.writeNode(Constants.EXCEPTION, ex.Message);
            //}
            //finally
            //{
            //    if (conOptions != null)
            //    {
            //        conOptions.Close();
            //    }
                
            //}

                       
        }


        #endregion

       




















    }
}
