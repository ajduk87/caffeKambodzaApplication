   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  S OcaffeKambodzaApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:caffeKambodzaApplicationcaffeKambodzaApplication )   #caffeKambodzaApplication.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6sys&clr-namespace:System;assembly=mscorlib     8local&clr-namespace:caffeKambodzaApplication     -   
���  ��
�������  � Title$  caffe KAMBODŽA��$	��700��$
��1450��.���  � 	Resources  
  Color1 
 Color2 
 Color3 
 Color4 
 Color5 
 Color6 
 Brush1 �� Color 
 Brush2  	Gradient1 �� 
StartPoint �� EndPoint � Color � Offset 	 	Gradient2 
 	Gradient3  	Gradient4��%�  &	        &	      &	      &	 ,     &	 ?     &	 N     &	 b     &	 n     &	 z     &		 p    &	
 $    &	     ��	Black����	White����Gray����	LightGray����	Green����
LightGreen���� # �   �� # �  �� $	 0,0&�$	 1,0&�.��� # �   $	 0.1Q�� # �  $	 0.2Q�� # �  $	 0.3Q�� # �   $	 0.4Q�� # �  $	 0.5Q�� # �  $	 0.6Q�� # �   $	 0.7Q�� # �  $	 0.8Q�� # �  $	 0.9Q�� # �   $ 1Q��� $	 0,0&�$	 1,0&�.��� # �  $	 0.2Q�� # �  $	 0.3Q�� # �  $	 0.5Q�� # �  $	 0.6Q�� # �  $	 0.8Q�� # �  $	 0.9Q�� # �  $ 1Q��� $	 0,0&�$	 1,0&�.��� # �  $	 0.1Q�� # �  $	 0.2Q�� # �  $	 0.3Q�� # �  $	 0.4Q�� # �  $	 0.5Q�� # �  $	 0.6Q�� # �  $	 0.7Q�� # �  $	 0.8Q�� # �  $	 0.9Q�� # �  $ 1Q��� $	 0,0&�$	 1,1&�.��� $
 Gray��$	 0.0Q�� $ White��$	 0.1Q�� $ 	LightGray��$	 0.2Q�� $
 Gray��$	 0.3Q�� $ White��$	 0.4Q�� $ 	LightGray��$	 0.5Q�� $
 Gray��$	 0.6Q�� $ White��$	 0.7Q�� $ 	LightGray��$	 0.8Q�� $
 Gray��$	 0.9Q�� $ White��$ 1Q�.+��� -   $��
tabcontrol��$
��Auto��$
��Auto��$��Stretch=�$��Stretch=�.@��� -   $��	tab_first��$��Kreiranje knjige šanka��.��� -   $��gridTab1��#��[ .+��  � RowDefinitions �� $��25 ��� $��60 ��� $��35 ��� $��35 ��� $��* ��� $��35 ��� $��25 ��� $��25 ��� $��25 ��� $��25 �.+�� -   $��dataItemLabels��$��1��$��2�� � ColumnDefinitions �� $
��1.4* ��� $��5* ��� $��10 �$��10���� $	��150 �$	��150���� $��10 �$��10���� $��* ��� $��10 �$��10���� $
��1.4* ��� $��10 �$��10���� $��3* ��� $��* ��� $
��1.5* ��� $
��0.5* �.+��� -   $��tblNameProduct��$��Naziv proizvoda:  ��$
��Auto��$��Stretch=�$��Bottom=�$��
ExtraBlack"�$��15(�$��1���� -   $��tblPrice��$#��Jedinična cena proizvoda :  ��$
��Auto��$��13(�$��Stretch=�$��Bottom=�$��
ExtraBlack"�$��3���� -   $��	tblAmount��$ ��Količina (broj komada):  ��$
��Wrap=�$��15(�$
��Left=�$��Center=�$��
ExtraBlack"�$��5���� -	   $��tblItemValue��$��Vrednost stavke:  ��$��15(�$��Stretch=�$��Bottom=�$��
ExtraBlack"�$��7���� -
   $��tblShift��$��	Smena :  ��$
��Auto��$��Stretch=�$��Bottom=�$��
ExtraBlack"�$��15(�$��11��� -   $��dataItem��$��2��$��2�� �� $
��1.4* ��� $��5* ��� $��10 ��� $	��150 �$	��150���� $��10 ��� $��* ��� $��10 ��� $
��1.4* ��� $��10 ��� $��3* ��� $��* ��� $
��1.5* ��� $
��0.5* �.+��� -   $��cmbNameProductTab1��	 � Cursor$
	 Hand��$��35��$��35��$��20(�$
��Bold"�$��1������ 
 �� Path$
 Products��� -   $��tblPriceTab1��$�� ��$	��150��$��35��$��35�� �� 
IsReadOnly . $��20(�$
��Bold"�
�������$
��Left=�$��Center=�$��3���� -   $��tfAmount��$��Stretch=�$��Times New Roman)�
���   �
�������$��35��$��35��$��20(�$
��Bold"�$��5���� -   $��tblItemValueNumber��$�� ��$
��Auto��$��35�� . 
�������$��20(�$
��Bold"�$��Stretch=�$��Center=�$��7���� -   $��
btnaddItem��$��dodaj stavku��$��Center=�$��25��$��25��$��9���� -   $��cmbShift��$
��Left=�$��11��.@��� .��1�� .��2�� .��3�� .��4� -   $��
griddTotal��$��3��$��2�� �� $��20 �$��20���� $	��40* ��� $��5* ��� $��5* ��� $��* �.+��� -   $��tblRemarkTab1��$�� ��$
��Left=�$��Center=�$��20(�$
��Bold"�#��[ $��1���� -   $��tblTotal��$��Total : ��$��
ExtraBlack"�$��Right=�$��Center=�$��20(�$��2���� -   $��tblTotalValue��$��
ExtraBlack"�$
��Left=�$��Center=�$��20(�$��3��� -   $��griddataGrid��$��4�� �� $��* ��� $
��100* ��� $��3* �.+�&   System.Windows.Controls.DataGrid  -   $��	dataGrid1��$��Stretch=�   CanUserDeleteRows .  $��Stretch=�$
��Auto��   AutoGenerateColumns .     CanUserReorderColumns .     CanUserAddRows .     RowBackground
 �����   AlternatingRowBackground# [
 $��Stretch=�$��Stretch=�$��1������ *_items+   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value �� .R��� $
��Wrap=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Šifra proizvoda��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *CodeProduct+  $ Vrsta proizvoda��$ *  .  �� *KindOfProduct+  $ Jedinična cena��$ *  .  �� *Price+  $ 	Količina��$ *  .  �� *Amount+  $ Vrednost stavke��$ *  .  �� *
CostItem+� -   $��gridProgressBar��$��5��$��2�� �� $��20 �$��20���� $��* ��� $��* ��� $��* ��� $��70 �.+��� -   $��tblRemarkTabDown1��$�� ��$
��Left=�$��Center=�$��16(�$
��Bold"�#��[ $��1���� -   $��tfWhyItemDeleted��$��Center=�$&�� Unesite razlog uklanjanja stavke��
��� E��$��3��� -   $��gridRemoveItem��$��6��$��2�� �� $
��0.5* ��� $
��1.6* ��� $
��0.2* ��� $
��2.5* ��� $
��0.2* ��� $��2* ��� $
��0.1* ��� $��* ��� $
��0.2* ��� $��* ��� $
��0.2* ��� $
��1.4* ��� $��2* ��� $
��0.2* ��� $��2* ��� $��70 �$��70��.+��� -   $��tblDate��$��Unesite datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��1��(  "System.Windows.Controls.DatePicker  -   $��datepicker1��
 �� Now   SelectedDate# Z $��15(�$��3�� :  4System.Windows.Controls.Primitives.DatePickerTextBox��%6   '        �� "  .���  �@��
IsReadOnly . �� -   $��btncreateReport��$��kreiraj izveštaj��$��Center=�$��Stretch=�{�.  $��5���� -   $��tblisCreatingReportTime��$&�� Kreiranje izveštaja nije u toku��$��14(�$��Stretch=�$��Center=�$��3��$��7��� -    $
��pbar��$��15�� � Minimum$ 0Q� � Maximum$	 100Q� � Value$  0Q�$��11��$��2���� -!   $��btnremoveItem��$��ukloni stavku��$��Center=�$��Stretch=�$��14��� -"   $��gridNotWorkingDay��$��8��$��2�� �� $
��0.5* ��� $
��1.6* ��� $
��0.2* ��� $
��2.5* ��� $
��0.2* ��� $��2* ��� $
��0.1* ��� $��* ��� $
��0.2* ��� $��* ��� $
��0.2* ��� $
��1.4* ��� $��2* ��� $
��0.2* ��� $��2* ��� $��70 �$��70��.+��� -#   $��chbNotWorkingDay��$
��Left=�$��Center=�$��20(�$��neradan dan��$��3���� -$   $��
tab_second��$��Unesi/Ukloni novu stavku��.���� -%   $��innerTabControl��.@��� -&   $��productCafe��$(��"Unesi/Ukloni novi proizvod kafića��.��� -'   $��gridTab2��#��[ .+��  �� $��25 ��� $��40 ��� $��50 ��� $��50 ��� $��25 ��� $��25 ��� $��25 ��� $��40 ��� $��40 ��� $��25 ��� $��50 ��� $��25 ��� $��25 ��� $��40 ��� $��40 ��� $��40 �.+�� -(   $��captionUnesi��$��1��.+��� -)   $��lblcaptionUnesi��$��Unesi novi proizvod��$��Center=�$��Center=�$
��Bold"�$��20(�� -*   $��newitem��$��2�� �� $��* ��� $��2* ��� $��* ��� $��3* ��� $��20 ��� $
��1.5* ��� $��20 ��� $��* ��� $��20 �.+��� -+   $��lblCodeProduct��$��Šifra proizvoda :   ��$��Right=�$��Center=�$��
ExtraBlack"�$��0���� -,   $��tfCodeProduct��$��25��$��14(�$��1���� --   $��lblNameProduct��$��Naziv proizvoda :   ��$��Right=�$��Center=�$��
ExtraBlack"�$��2���� -.   $��tfNameProduct��$��14(�$��25��$��3���� -/   $��lblWayOfDisplayBookBar��$��Način prikaza u knjizi :��$��Right=�$��Center=�$��
ExtraBlack"�$��5���� -0   $��cmbWayDisplayBookBar��$��20(�$
	 Hand��$��30��$��7��.@��� .��KOM�� .��LIT�� .��KG� -1   $��newItem2��$��3�� �� $��* ��� $
��1.4* ��� $
��0.5* ��� $��* ��� $��3* ��� $��2* �.+��� -2   $��lblPrice��$��Jedinična cena :��$��Right=�$��Center=�$��
ExtraBlack"�$��0���� -3   $��tfPrice��$��14(�$��25��$��1���� -4   $��tbkCurrency��$	��din��$��Center=�$
��Left=�$��
ExtraBlack"�$��2���� -5   $��
lblMeasure��$��Jedinična mera :��$��Right=�$��Center=�$��
ExtraBlack"�$��3���� -6   $��
cmbMeasure��$��20(�$
	 Hand��$��30��$��4��� -7   $��dugmenewitem��$��4�� �� $��* ��� $��3* ��� $
��0.1* ��� $��* ��� $��0.05* �.+��� -8   $��
btnNewItem��$��Unesi novi proizvod��$��Center=�$��3��� -9   $��dugmedeletetextbox��$��5�� �� $��* ��� $��3* ��� $
��0.1* ��� $��* ��� $��0.05* �.+��� -:   $��btnDeleteTextbox��$$��Obriši polja unetog proizvoda��$��Center=�$��3��� -;   $��captionUkloni��$��7��.+��� -<   $��lblcaptionUkloni��$��Ukloni izabrani proizvod��$��Center=�$��Center=�$
��Bold"�$��20(�� -=   $��
removeitem��$��8�� �� $��* ��� $��3* ��� $
��0.1* ��� $��* ��� $��0.05* �.+��� ->   $��lblNameProductr��$!��Vrsta izabranog proizvoda :��$��Right=�$��Center=�$��
ExtraBlack"�$��0���� -?   $��cmbNameProductr��$��20(�$��30��$
	 Hand��$��1������ $
 Products��� -@   $��btnRemoveItem��$��Ukloni izabrani proizvod��$��25��$��3��� $��9�� �� $��* ��� $
��0.9* ��� $��* ��� $
��0.2* ��� $��* �.+�� $��0�� �� $��* ��� $��* �.+��� $#��    Do kad je cena važila : ��$��Center=�$��Right=�$��0��  -A   $��datepickerChangePrice��$��12(�$��3�� ��%6   '        �� "  .���  �@��
IsReadOnly . �� $)��#Nova cena izabranog proizvoda je : ��$��Center=�$
��Left=�$��1���� -B   $��
tfNewPrice��$��2���� $
��din ��$��90��$��Center=�$
��Left=�$��3���� -C   $��btnChangePrice��$��25��$��Center=�$
��Left=�$&�� Promeni cenu izabranog proizvoda��$	��275��$��4��� -D   $��removeitem2��$��10�� �� $��* ��� $��3* ��� $
��0.1* ��� $��* ��� $��0.05* �.+��� -E   $��lblCodeProductr��$"��Šifra izabranog proizvoda :��$��Right=�$��Center=�$��
ExtraBlack"�$��0���� -F   $��cmbNameProductr2��$��30��$
	 Hand��$��20(�$��1��� -G   $��remark��$��6��.+��� -H   $��	tbkRemark��$
��Left=�$��Center=�$
��Bold"�� -I   $��gridNewMeasure��$��13�� �� $��* ��� $��3* ��� $
��0.1* ��� $��* ��� $��0.05* �.+��� -J   $��tblNewMeasure��$&�� Unesite naziv jedinične mere : ��$��Right=�$��Center=�$��20(�$
��Bold"�$��0���� -K   $��tfNewMeasure��$��Stretch=�$��Center=�$��20(�$��1���� -L   $��btnNewMeasure��$ ��Unesi novu jediničnu meru��$��25��$��3��� -M   $��gridDeletetfMeasure��$��14�� �� $��* ��� $
��2.7* ��� $
��0.1* ��� $
��0.2* ��� $
��0.1* ��� $��* ��� $��0.05* �.+��� -N   $��btnDeletetfMeasure��$��Obriši polje za meru��$��25��$��5��� -O   $��gridRemoveMeasure��$��15�� �� $��* ��� $��3* ��� $
��0.1* ��� $��* ��� $��0.05* �.+��� -P   $��tblRemoveMeasure��$�� Jedinična mera : ��$��Right=�$��Center=�$��20(�$
��Bold"�$��0���� -Q   $��cmbRemoveMeasure��$��Stretch=�$��Center=�$��30��$��20(�$��1���� -R   $��btnRemoveMeasure��$%��Ukloni izabranu jediničnu meru��$��25��$��3���� -S   $��productStore��$'��!Unesi/Ukloni novu stavku magacina��.��� -T   $��gridStoreProducts��#��[ .+�2	  ,caffeKambodzaApplication.StorehouseItemsTab2	  -U     	 Name$  enterStoreItemsTab2���� -V   $��connProductsStore��$9��3Pregled/Ažuriranje odnosa proizvod-stavka magacina��.��� -W   $��gridStoreProductsUpdate��.+�8
  2caffeKambodzaApplication.SelectUpdateConnProdStore
  -X   ! 
 Name$! selectUpdateConnProdStore���� -Y   $��	tab_third��$��Šank��.��� .+�)  #caffeKambodzaApplication.Storehouse  -Z   "  Name$" 
storehouse���� -[   $��
tab_fourth��$#��Rad sa kreiranim izveštajima��.��� .+�8  2caffeKambodzaApplication.WorkingWithCreatedReports  -\   #  Name$# createdReports���� -]   $��	tab_fifth��$��Pregled rada šanka��.��� .+�5  /caffeKambodzaApplication.OverviewStorehouseWork  -^   $  Name$$ overviewStorehouse���� -_   $��	tab_sixth��$'��!Istorija menjanja recepata i cena��.��� .+�&   caffeKambodzaApplication.History  -`   %  Name$% history���� $��Opcije��.��� .+�,  &caffeKambodzaApplication.ReportOptions  -a   &  Name$& options���� $��Slanje na mail��.��� .+�)  #caffeKambodzaApplication.MailSender  -b   '  Name$' 
mailsender��