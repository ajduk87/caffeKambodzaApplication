      #region newremoveItems_SecondTab 

        private void Click_btnNewItem(object sender, RoutedEventArgs e)
        {
            if (_entered) { tbkRemark.Text = Constants.REMARKENTERED; return; }
            _lastProduct = tfNameProduct.Text;

            string codeProduct = String.Empty;
            string nameProduct = String.Empty;
            string measureProduct = String.Empty;
            string kindOfProduct = String.Empty;
            int price = -1;
            codeProduct = tfCodeProduct.Text;
            nameProduct = tfNameProduct.Text;

            int n;
            bool isNumeric = int.TryParse(tfPrice.Text,out n);
            if (isNumeric)
            {
                price = Convert.ToInt32(tfPrice.Text);
            }

            if (tfNameProduct.Text == String.Empty && tfPrice.Text == String.Empty)
            {
                tbkRemark.Text = Constants.REMARKPRODUCTANDPRICE;
                return;
            }
            else if (tfNameProduct.Text == String.Empty)
            {
                tbkRemark.Text = Constants.REMARKPRODUCT;
                tfPrice.Foreground = System.Windows.Media.Brushes.Red;
                tfCodeProduct.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }
            else if (tfPrice.Text == String.Empty)
            {
                tbkRemark.Text = Constants.REMARKPRICE;
                tfNameProduct.Foreground = System.Windows.Media.Brushes.Red;
                tfCodeProduct.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }
         /*   for (int i = 0; i < _products.Count; i++)
            {
                if (_products.ElementAt(i).KindOfProduct.Equals(nameProduct))
                {
                    tfCodeProduct.Foreground = System.Windows.Media.Brushes.Red;
                    tfNameProduct.Foreground = System.Windows.Media.Brushes.Red;
                    tfPrice.Foreground = System.Windows.Media.Brushes.Red;
                    tbkRemark.Text = Constants.NAMEPRODUCTEXISTS;
                    return;
                }
            }*/


            tbkRemark.Text = String.Empty;

            tfCodeProduct.Foreground = System.Windows.Media.Brushes.DarkOliveGreen;
            tfNameProduct.Foreground = System.Windows.Media.Brushes.DarkOliveGreen;
            tfPrice.Foreground = System.Windows.Media.Brushes.DarkOliveGreen;
            _entered = true;

            if (codeProduct.Equals(String.Empty) == true)
            {
                Product product = new Product(nameProduct, price);

                insertProductInDatabase(product);
                _products.Add(product);
                Products.Add(product.ComboBoxForm());
            }
            else 
            {
                Product product = new Product(codeProduct, nameProduct, price);
                for (int i = 0; i < _products.Count; i++)
                {
                    if (product.CodeProduct.Equals(_products.ElementAt(i).CodeProduct))
                    {
                        tbkRemark.Text = Constants.USEDCODEPRODUCT + "   " + Constants.USEDNAMEPRODUCT + "  " + _products.ElementAt(i).NameProduct + "  " + Constants.USEDPRICE + "  " + _products.ElementAt(i).Price;
                        tfCodeProduct.Foreground = System.Windows.Media.Brushes.Red;
                        tfNameProduct.Foreground = System.Windows.Media.Brushes.Red;
                        tfPrice.Foreground = System.Windows.Media.Brushes.Red;
                        return;
                    }
                }

                insertProductInDatabase(product);
                _products.Add(product);
                Products.Add(product.ComboBoxForm());
                Codes.Add(product.Code());
            }
           
        }

        private void Click_btnDeleteTextbox(object sender, RoutedEventArgs e)
        {
            _entered = false;
            tfCodeProduct.Text = String.Empty;
            tfNameProduct.Text = String.Empty;
            tfPrice.Text = String.Empty;
            tfCodeProduct.Foreground = System.Windows.Media.Brushes.Black;
            tfNameProduct.Foreground = System.Windows.Media.Brushes.Black;
            tfPrice.Foreground = System.Windows.Media.Brushes.Black;
        }


       private void Click_btnRemoveItem(object sender, RoutedEventArgs e)
       {
           if (cmbNameProductr.SelectedIndex == 0 && cmbNameProductr2.SelectedIndex == 0)
           {
               tbkRemark.Text = Constants.REMOVEITEMREMARK;
           }
           else 
           {
               tbkRemark.Text = String.Empty;
               if (cmbNameProductr.SelectedIndex != 0)
               {
                   int index = cmbNameProductr.SelectedIndex;
                   Product product = _products.ElementAt(index - 1);
                  
                   Products.RemoveAt(index);
                   _products.RemoveAt(index - 1);
                   removeProductFromDatabase(product);
                   cmbNameProductr.ItemsSource = Products;
                   cmbNameProductr.SelectedIndex = 0;
                   

                   if(product.CodeProduct.Equals(Constants.CODENOTENTERED) == false)
                   {
                       for (int i = 0; i < Codes.Count; i++ )
                       {
                           if (product.CodeProduct.Equals(Codes.ElementAt(i)))
                           {
                               Codes.RemoveAt(i);
                               break;
                           }
                       }
                       cmbNameProductr2.ItemsSource = Codes;
                       cmbNameProductr2.SelectedIndex = 0;
                   }
               }
               if (cmbNameProductr2.SelectedIndex != 0)
               {
                   int index = cmbNameProductr2.SelectedIndex;
                   string searchkey = cmbNameProductr2.Items[index].ToString();
                   int prodindex;
                   Product product;
                   for (int i = 0; i < _products.Count; i++)
                   {
                       if (_products.ElementAt(i).CodeProduct.Equals(searchkey))
                       {
                           product = _products.ElementAt(i);
                           removeProductFromDatabase(product);
                           prodindex = i;
                           Products.RemoveAt(prodindex+1);
                           cmbNameProductr.ItemsSource = Products;
                           if (cmbNameProductr.SelectedIndex == prodindex)
                           {
                           cmbNameProductr.SelectedIndex = 0;
                           }
                           _products.RemoveAt(prodindex);
                           break;
                       }
                   }
                   Codes.RemoveAt(index);
                  
                  
                   cmbNameProductr2.ItemsSource = Codes;
                   cmbNameProductr2.SelectedIndex = 0;
               }

           }
       }

       private void SelectionChanged_cmbNameProductr(object sender, SelectionChangedEventArgs e)
       {
           if (cmbNameProductr.SelectedIndex != 0)
           {
               cmbNameProductr2.IsEnabled = false;
           }
           else 
           {
               cmbNameProductr2.IsEnabled = true;
           }

       }

       private void SelectionChanged_cmbNameProductr2(object sender, SelectionChangedEventArgs e)
       {
           if (cmbNameProductr2.SelectedIndex != 0)
           {
               cmbNameProductr.IsEnabled = false;
           }
           else
           {
               cmbNameProductr.IsEnabled = true;
           }
       }

       private void MouseEnter_tfNameProduct(object sender, MouseEventArgs e)
       {
           if (tfNameProduct.Text.Equals(_lastProduct) && tfNameProduct.Text.Equals(String.Empty) == false) { return; }

           string codeProduct = String.Empty;
           tfNameProduct.IsReadOnly = false;
          
           codeProduct = tfCodeProduct.Text;


           for (int i = 0; i < _products.Count; i++)
           {
               if (codeProduct.Equals(_products.ElementAt(i).CodeProduct))
               {
                   tbkRemark.Text = Constants.USEDCODEPRODUCT + "   " + Constants.USEDNAMEPRODUCT + "  " + _products.ElementAt(i).NameProduct + "  " + Constants.USEDPRICE + "  " + _products.ElementAt(i).Price;
                   tfCodeProduct.Foreground = System.Windows.Media.Brushes.Red;
                   tfNameProduct.Foreground = System.Windows.Media.Brushes.Red;
                   tfPrice.Foreground = System.Windows.Media.Brushes.Red;
                   tfNameProduct.IsReadOnly = true;
                   return;
               }
           }

       }

       private void MouseLeave_tfNameProduct(object sender, MouseEventArgs e)
       {
           tbkRemark.Text = String.Empty;
       }


       private void MouseEnter_tfPrice(object sender, MouseEventArgs e)
       {
           if (tfNameProduct.Text.Equals(_lastProduct) && tfNameProduct.Text.Equals(String.Empty) == false) { return; }

           string codeProduct = String.Empty;
           tfPrice.IsReadOnly = false;

           codeProduct = tfCodeProduct.Text;


           for (int i = 0; i < _products.Count; i++)
           {
               if (codeProduct.Equals(_products.ElementAt(i).CodeProduct))
               {
                   tbkRemark.Text = Constants.USEDCODEPRODUCT + "   " + Constants.USEDNAMEPRODUCT + "  " + _products.ElementAt(i).NameProduct + "  " + Constants.USEDPRICE + "  " + _products.ElementAt(i).Price;
                   tfCodeProduct.Foreground = System.Windows.Media.Brushes.Red;
                   tfNameProduct.Foreground = System.Windows.Media.Brushes.Red;
                   tfPrice.Foreground = System.Windows.Media.Brushes.Red;
                   tfPrice.IsReadOnly = true;
                   return;
               }
           }
       }

       private void MouseLeave_tfPrice(object sender, MouseEventArgs e)
       {
           string price = tfPrice.Text;
           int n;
           bool isNumeric = int.TryParse(price,out n);
           if (isNumeric == false && price.Equals(String.Empty) == false)
           {
               tbkRemark.Text = Constants.REMARKAMOUNTNOTNUMERIC3TAB1;
           }
           else
           {
               tbkRemark.Text = String.Empty;
           }
       }

       private void TextChanged_tfPrice(object sender, TextChangedEventArgs e)
       {
           tbkRemark.Text = String.Empty;
       }


       private void TextChanged_tfCodeProduct(object sender, TextChangedEventArgs e)
       {
           tfCodeProduct.Foreground = System.Windows.Media.Brushes.Black;
           tfNameProduct.Foreground = System.Windows.Media.Brushes.Black;
           tfPrice.Foreground = System.Windows.Media.Brushes.Black;
       }

        #endregion