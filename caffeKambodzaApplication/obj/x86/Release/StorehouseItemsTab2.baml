   M S B A M L   `   `   `  ����   caffeKambodzaApplication2    ,caffeKambodzaApplication.StorehouseItemsTab2   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources   �� MergedDictionaries �� Source��  �� $1 +Resources/OptionsButtonStyleDictionary.xaml?�%    .��� -   $��gridRoot�� � ShowGridLines .      	Gradient4 �� 
StartPoint �� EndPoint � Color � Offset �� 
TargetType	 �� Property
 �� Value  
HeaderFont��%{  &	        '�� K    &	 n    '�� �    '	� �    '�� �    '��     �� $	 0,0&�$	 1,1&�.��� $
 Gray��$	 0.0Q�� $ White��$	 0.1Q�� $ 	LightGray��$	 0.2Q�� $
 Gray��$	 0.3Q�� $ White��$	 0.4Q�� $ 	LightGray��$	 0.5Q�� $
 Gray��$	 0.6Q�� $ White��$	 0.7Q�� $ 	LightGray��$	 0.8Q�� $
 Gray��$	 0.9Q�� $ White��$ 1Q��� " ��.��� 	 � ��$
 18(��� " ��.��� 	 � ��$
 14(��� " ��.��� 	 � ��$
 18(��� 	 � ��$

 Bold"��� " 	�.��� 	 � ��$
 18(��� " ��.��� 	 � ��$
 18(��� " ��.��� 	 � ��$
 18(� � ColumnDefinitions �� $��* ��� $��* ��� $��* �.+�� -   $��gridCurrProduct��$��0�� � RowDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $
��1.2* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+�� $��9�� �� $
��Auto ��� $��* �.+��� $	��   ��$��0���� -   $��tblRemarkAddStoreItem��$��20(�$
��Bold"�$
��Wrap=�$��Center=�$
��Left=�$��1���� -   $��tblEarlierEnteredProduct��$2��,   Informacije o izabranom proizvodu kafića��$��Center=�$
��Bold"�$��1���� -   $��tblEarlierEnteredProduct2��$"��   Izabrani proizvod kafića��$��Center=�$
��Bold"�$��2��� $��3�� �� $
��Auto ��� $��* �.+��� -   $��blanko2��$	��   ��$��0���� -   $��cmbChooseEarlierProduct��$��Center=�$��Stretch=�$��2������  �� Path$ Products�� -   $��gridProductCodeDown��$��5�� �� $	��170 ��� $��* �.+��� -	   $#��tblJustEnteredProductCodeDown��$��   Šifra proizvoda je : ��$��Center=�$��0���� -
   $.��(tblJustEnteredProductCodeInformationDown��$
��Auto��$��Center=�$
��Left=�$��1��� -   $��gridKindOfProductDown��$��6�� �� $	��170 ��� $��* �.+��� -   $%��tblJustEnteredKindOfProductDown��$��   Vrsta proizvoda je : ��$��Center=�$��0���� -   $0��*tblJustEnteredKindOfProductInformationDown��$
��Auto��$��Center=�$
��Left=�$��1��� -   $��gridProductPriceDown��$��7�� �� $	��250 ��� $��* �.+��� -   $$��tblJustEnteredProductPriceDown��$(��"   Jedinična cena proizvoda je : ��$��Center=�$��0���� -   $/��)tblJustEnteredProductPriceInformationDown��$
��Auto��$��Center=�$
��Left=�$��1���� -   $��tlbCaption15��$��Center=�$
��Left=�$%��   Izabrana grupa stavki šanka��$
��Bold"�$��10��� $��11�� �� $
��Auto ��� $��* �.+��� $	��   ��$��0���� -   $��cmbChooseStoreItemGroup��$��30��$��Center=�$��Stretch=�$��2������ *
StoreItems+�� -   $��tlbCaption11��$��Center=�$
��Left=�$��   Izabrana stavka šanka��$
��Bold"�$��12��� $��13�� �� $
��Auto ��� $��* �.+��� -   $��blanko3��$	��   ��$��0���� -   $��cmbChooseStoreItem2��$��30��$��Center=�$��Stretch=�$��2������ *
StoreItems+� $��15�� �� $
��Auto ��� $��* ��� $��10 �$��10��.+��� $	��   ��$��0���� $@��:Unesi koliko količinski izabrane stavke ima u proizvodu: ��$��1��� $��16�� �� $
��Auto ��� $��* ��� $��20 �$��20���� $	��100 �$	��100��.+��� $	��   ��$��0���� -   $��tfProductAmount��$��16(�$��25��$��Normal"�$��1���� -   $��cmbMeasureSI��$
��Left=�$��25��$��15(�$��3��.@��� .��ml�� .��l�� .��g�� .��kg� $��18�� �� $
��Auto ��� $��* �.+��� $	��   ��$��0���� -   $��btnAddStoreItemDown��$��  dodaj stavku šanka  ��$��Center=�$
��Left=�$��1��� $��19�� �� $
��Auto ��� $��* �.+��� $	��   ��$��0���� -   $��tblRemarkAddStoreItem2��$��20(�$
��Bold"�$
��Wrap=�$��Center=�$
��Left=�$��1��� -   $��gridEnterNewStoreItem��$��1�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $
��1.1* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $��tblNewStoreItemCode��$*��$Unesite šifru nove stavke šanka : ��$
��Left=�$��Center=�$��18(�$
��Bold"�$��1���� -   $��tfNewStoreItemCode��$��Stretch=�$��Center=�$��12(�$��2���� -   $��tblNewStoreItem��$)��#Unesite naziv nove stavke šanka : ��$
��Left=�$��Center=�$��18(�$
��Bold"�$��3���� -   $��tfNewStoreItem��$��Stretch=�$��Center=�$��12(�$��4���� -   $��tblNewStoreItemMeasure��$3��-Unesite jediničnu meru nove stavke šanka : ��$
��Left=�$��Center=�$��18(�$
��Bold"�$��5���� -    $��cmbStoreItemMeasure��$��30��$��18(�$��6���� -!   $��tblNewStoreItemGroup��$7��1Unesite grupu kojoj nova stavka šanka pripada : ��$
��Left=�$��Center=�$��18(�$
��Bold"�$��7���� -"   $��tfNewStoreItemGroup��$��Stretch=�$��Center=�$��12(�$��8���� -#   $��tblNewStoreItemPrice��$3��-Unesite jediničnu cenu nove stavke šanka : ��$
��Left=�$��Center=�$
��Bold"�$��18(�$��9��� $��10�� �� $��* ��� $��35 �.+��� -$   $��tfNewStoreItemPrice��$��Stretch=�$��Center=�$��12(��� -%   $��tblStorecurrency��$	��din��$��Center=�$
��Bold"�$��1���� $8��2Unesi kolika je količina izabrane stavke šanka: ��$
��Bold"�$��18(�$��25��$��11��� $��12�� �� $��* ��� $��20 �$��20���� $	��100 �$	��100��.+��� -&   $��tfStoreAmount��$��16(�$��25��$��Normal"�$��0���� -'   $��cmbMeasureSI2��$
��Left=�$��25��$��15(�$��2��.@��� .��ml�� .��l�� .��g�� .��kg�� -(   $��btnNewStoreItem��$"��  Unesi novu stavku šanka  ��$��25��$��Center=�$
��Left=�$��13���� -)   $��btnDeletetfMeasure��$(��"   Obriši tesktualna polja       ��$��Center=�$
��Left=�$��25��$��15���� -*   $��	tblRemark��$
��Left=�$��Center=�$��18(�$
��Bold"�$
��Wrap=�$��14���� -+   $��tblRemoveStoreItemGroup��$"��Izabrana grupa stavke šanka��$��18(�$
��Bold"�$��16���� -,   $��cmbRemoveStoreItemGroup��$��30��$��18(�$��17���� --   $��tblRemoveStoreItem��$��Izabrana stavka šanka��$��18(�$
��Bold"�$��18���� -.   $��cmbRemoveStoreItem��$��35��$��16(�$��19������ *
StoreItems+�� -/   $��btnRemoveStoreItem��$'��!  Ukloni izabranu stavku šanka  ��$��Center=�$
��Left=�$��25��$��20���� -0   $��tblStoreItemCode��$#��Izabrana šifra stavke šanka��$��18(�$
��Bold"�$��22���� -1   $��cmbStoreItemCode��$��Center=�$��30��$��18(�$��23������ *StoreItemCodes+� -2   $��gridProductCodePart2��$��24�� �� $	��210 ��� $��* �.+��� -3   $$��tblJustEnteredProductCodePart2��$��Šifra stavke šanka je :��$��18(�$��Center=�$��0���� -4   $/��)tblJustEnteredProductCodeInformationPart2��$
��Auto��$��Center=�$
��Left=�$��1��� -5   $��gridProductPricePart2��$��25�� �� $	��290 ��� $��* �.+��� -6   $%��tblJustEnteredProductPricePart2��$(��"Jedinična cena stavke šanka je :��$��18(�$��Center=�$��0���� -7   $0��*tblJustEnteredProductPriceInformationPart2��$
��Auto��$��Center=�$
��Left=�$��1��� -8   $��gridProductsStoreItemConn��$��2�� �� $��2* ��� $
��0.1* ��� $��* �.+�� -9   $"��gridCurrProductStoreItemConn��$��0�� �� $��60 ��� $��* ��� $��25 �.+��� -:   $��tblcurr��$6��0Stavke šanka za proizvod koji se trenutno unosi��$��Bottom=�$
��Bold"�$��0��&   System.Windows.Controls.DataGrid  -;   $#��dgridCurrProductStoreItemConn��   AutoGenerateColumns .     CanUserAddRows .  $��1������ *_currStoreItemProducts+   ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .��� 	 � ��$
 12(��� 	 � ��
 �� .R��� $
��Wrap=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Stavke šanka��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 5*    FontSize$ 12Q�   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $ KindOfProduct��� -<   $&�� btnRemoveStoreItemForCurrProduct��$��Right=�$��Center=�$,��&   ukloni selektovanu stavku šanka   ��$��2��� -=   $��gridStoreItemsMeasures��$��2�� �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 �.+��� ->   $��tlbCaption19��$��Center=�$
��Left=�$��18(�$6��0   Unesite naziv jedinične mere stavke šanka :��$
��Bold"�$��1��� $��2�� �� $
��Auto ��� $��* ��� $��20 �.+��� $	��   ��$��0���� -?   $��tfNewStoreItemMeasure��$��16(�$��25��$��Normal"�$��1��� $��3�� �� $
��Auto ��� $��* ��� $��20 �.+��� $	��   ��$��0���� -@   $!��btnEnterNewStoreItemMeasure��$
��Left=�$��Center=�$4��.   unesi novu jediničnu meru stavke šanka   ��$��1���� -A   $��tlbCaption20��$��Center=�$
��Left=�$��18(�$(��"   Jedinična mera stavke šanka :��$
��Bold"�$��5��� $��6�� �� $
��Auto ��� $��* ��� $��20 �.+��� $	��   ��$��0���� -B   $��cmbStoreItemMeasureRemove��$��Stretch=�$��Center=�$��1��� $��8�� �� $
��Auto ��� $��* ��� $��20 �.+��� $	��   ��$��0���� -C   $"��btnRemoveNewStoreItemMeasure��$
��Left=�$��Center=�$9��3   ukloni izabranu jediničnu meru stavke šanka   ��$��1��