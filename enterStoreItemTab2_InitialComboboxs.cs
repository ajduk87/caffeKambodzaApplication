  private void initialComboboxs()
        {
            try
            {
                string id = "7";//Queries.xml ID
                XDocument xdocStore = XDocument.Load(System.Environment.CurrentDirectory + Constants.QUERIESPATH);
                XElement Query = (from xml2 in xdocStore.Descendants("Query")
                                  where xml2.Element("ID").Value == id
                                  select xml2).FirstOrDefault();
                Console.WriteLine(Query.ToString());
                string query = Query.Attribute(Constants.TEXT).Value;
                
                conStore.Open();
                com = new OleDbCommand(query, conStore);
                dr = com.ExecuteReader();
                string codeProduct = String.Empty;
                string kindOfProduct = String.Empty;
                int price = -1;
                string storeGroup = String.Empty;
                string isUsed = String.Empty;
                bool isUsedBool;
                string amount = String.Empty;
                double amountDouble;

                StoreItems.Add(Constants.CHOOSEPRODUCT_STORE);
                StoreItemCodes.Add(Constants.CHOOSECODE_STORE);
                while (dr.Read())
                {
                    codeProduct = dr["StoreItemCode"].ToString();
                    kindOfProduct = dr["StoreItemName"].ToString();
                    int n;
                    bool isNumeric = int.TryParse(dr["StoreItemPrice"].ToString(), out n);
                    if (isNumeric) { price = Convert.ToInt32(dr["StoreItemPrice"].ToString()); }
                    storeGroup = dr["StoreItemGroup"].ToString();
                    isUsed = dr["isUsed"].ToString();
                    if (isUsed.Equals(Constants.YES)) isUsedBool = true;
                    else isUsedBool = false;
                    amount = dr["Amount"].ToString();
                    bool isNum = Double.TryParse(dr["Amount"].ToString(), out amountDouble);

                    StoreItemProduct storeProduct = new StoreItemProduct(codeProduct, kindOfProduct, price, storeGroup, isUsedBool, amountDouble);
                    StoreItemProducts.Add(storeProduct);
                    StoreItems.Add(storeProduct.ComboBoxForm());
                    StoreItemCodes.Add(storeProduct.Code());

                    if (numGroup == 0)
                    {
                        GroupsItemsInStore.Add(storeProduct.Group);
                        numGroup++;
                    }
                    else
                    {
                        int g;
                        for ( g = 0; g < GroupsItemsInStore.Count; g++) 
                        {
                            if (GroupsItemsInStore.ElementAt(g).Equals(storeProduct.Group) == true) 
                            {
                                break;
                            }
                        }
                        if (g == GroupsItemsInStore.Count) 
                        {
                            GroupsItemsInStore.Add(storeProduct.Group);
                            numGroup++;
                        }


                    }

                   // StoreItemByGroup.ElementAt(0).Add(storeProduct);

                }

                
                cmbChooseStoreItem2.ItemsSource = StoreItems;
                cmbRemoveStoreItem.ItemsSource = StoreItems;


                cmbChooseStoreItem2.SelectedIndex = 0;
                cmbRemoveStoreItem.SelectedIndex = 0;

                cmbStoreItemCode.ItemsSource = StoreItemCodes;
                cmbStoreItemCode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conStore.Close();
                dr.Close();
            }


        }