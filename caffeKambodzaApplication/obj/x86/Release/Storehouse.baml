   M S B A M L   `   `   `  ����   caffeKambodzaApplication)    #caffeKambodzaApplication.Storehouse   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources     AddFilterImage  RemoveFilterImage 
 Color1 
 Color2 
 Color3 
 Color4 
 Color5 
 Color6  	Gradient3 �� 
StartPoint �� EndPoint � Color � Offset 	 	Gradient4��%Q  &	        &	 &     &	 J     &	 Y     &	 h     &	 v     &	 �     &	 �     &	 �     &		 �    �� Resources\AdvFilterImage.ico����Resources\DeleteFilter.ico����	Black����	White����Gray����	LightGray����	Green����
LightGreen���� $	 0,0&�$	 1,0&�.��� # �  $	 0.1Q�� # �  $	 0.2Q�� # �  $	 0.3Q�� # �  $	 0.4Q�� # �  $	 0.5Q�� # �  $	 0.6Q�� # �  $	 0.7Q�� # �  $	 0.8Q�� # �  $	 0.9Q�� # �  $ 1Q��� $	 0,0&�$	 1,1&�.��� $
 Gray��$	 0.0Q�� $ White��$	 0.1Q�� $ 	LightGray��$	 0.2Q�� $
 Gray��$	 0.3Q�� $ White��$	 0.4Q�� $ 	LightGray��$	 0.5Q�� $
 Gray��$	 0.6Q�� $ White��$	 0.7Q�� $ 	LightGray��$	 0.8Q�� $
 Gray��$	 0.9Q�� $ White��$ 1Q�.��� .+��� -   $��tabcontrolStore��.@��� -   $��tabIEnterInStore��$��Unos podataka u šank��.��� -   $��gridTab1��#��[	  � RowDefinitions �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��* � � ColumnDefinitions �� $��* ��� $��* �.+�� $��9�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� $-��'Izaberite stavku šanka koju unosite : ��$��1��� $��1�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� $%��Izaberite datum unosa stavki : ��$��1��� $��2�� �� $
��Auto ��� $	��300 ��� $��* �.+��� $��           ��$��0��(  "System.Windows.Controls.DatePicker  -   $��datepicker1��$	��300��$��15(�$��Center=�$
��Left=�$��1��  :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property	 �� Value��%6   '        �� "  .���  �@��
IsReadOnly	 . �� -   $��
dateRemark��$/��)  Morate uneti datum unosa stavki šanka ��$��2��� $��7�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� $3��-Izaberite grupu stavki šanka koju unosite : ��$��1��� $��8�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� -   $��	cmbSGroup��$	��500��$��Center=�$
��Left=�$��1��� $��10�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� -   $��cmbSItem��$	��500��$��Center=�$
��Left=�$��1��� $��12�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� $3��-Unesite broj komada izabrane stavke šanka : ��$��1��� $��13�� �� $��50 ��� $	��500 ��� $��20 �$��20���� $	��100 �$	��100��.+��� -   $��tfStoreRealAmount��$��16(�$��25��$��Normal"�$��1��� $��15�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� -	   $��btnEnter��$��  Unesi stavku u šank  ��$
��Auto��$��Center=�$
��Left=�$��1��� $��17�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� -
   $��	btnFinish��$(��"  Završi današnji unos u šank  ��$
��Auto��$��Center=�$
��Left=�$��1��� $��19�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� $+��%Vraćanje sistema jedan dan unazad : ��$��1��� $��21�� �� $
��Auto ��� $��* �.+��� $��           ��$��0���� -   $��btnReturnOneDay��$!��  Vrati sistem dan unazad  ��$
��Auto��$��Center=�$
��Left=�$��1��� $��2��$��1�� �� $��20 ��� $��* �.+��� $��           ��$��0���� $H��B Spisak unetih stavki šanka za izabrani datum, koje nisu unete : ��$��1��� $��4��$��1��$��15�� �� $��20 ��� $��* ��� $��20 �.+��� $��           ��$��0��&   System.Windows.Controls.DataGrid  -   $!��dgridDailyEnterInStorehouse��
   AutoGenerateColumns
 .     CanUserAddRows .     SelectionMode$ Single=�$��2��$��1������ *DailyStoreItem+   ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��$	 12(���  � ��	 �� .R��� $
��Wrap=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Naziv stavke šanka��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 4*    FontSize$ 12Q�   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *KindOfProduct+�� -   $��tabIStateStore��$��Trenutno u upotrebi��.��� -   $��gridTab2��#��[	  �� $��* ��� $��2* �.+�� -   $��gridLeftSide��$��0�� �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��3* ��� $��25 ��� $��3* �.+��� $$��   Izabrani proizvod kafića :��$��1��� $��2�� �� $��15 �$��15���� $��* �.+��� -   $��cmbItemStore��$��Stretch=�$��25��$��15(�$��1��� -   $��gridDataGrid��$��4�� �� $��15 ��� $��* �.+�  -   $��dgrid��
 .   .  $ Single=�$��1������ *_currStoreItemProducts+ �� "  .���  � ��$	 14(�   $ Stavke šanka��$ 5* $ 12Q� .  ��  �� Path$ KindOfProduct��� $@��:   Stavke šanka koje su u upotrebi za izabrani proizvod :��$��Center=�$��5��� -   $��gridDataGridinUse��$��6�� �� $��15 ��� $��* �.+�  -   $��
dgridinUse��
 .   .  $ Single=�$��1������ *_currStoreItemProductsInUse+ �� "  .���  � ��$	 14(�   $ Stavke šanka��$ 5* $ 12Q� .  �� $ KindOfProduct�� -   $��	rightSide��$��1�� �� $��20 ��� $��* ��� $��20 � �� $��30 ��� $��25 ��� $��* �.+�� -   $��
gridFilter��$��1��$��0�� �� $	��290 ��� $	��255 ��� $��10 �$��10���� $��2* ��� $
��0.1* ��� $��30���� $��10 �$��10���� $��30���� $��20 �$��20��.+�c� -   $��leftToolBar��$��0��$��10��$��36��$��Stretch=�$��Stretch=�$
��Auto��
��������� $4��.   Izaberite kolonu koju želite filtrirati : ��$
��Left=�$��Center=�$��0���� -   $��cmbFilterColumn��$��1��$��Stretch=�$��Center=�$
��Auto�� � Cursor$
 Hand��.@��� .��!Izaberite kolonu za filtriranje�� .��Vrsta proizvoda�� .��Naziv stavke šanka�� .��Grupa stavke šanka�� .��" Količinski udeo proizvoda(kg/l)�� .��Količina stavke šanka(kg/l)�� .��Cena stavke šanka(din)�� -   $��tfFilter��$��Center=�$��Stretch=�$��3���� -   $��btnAddFilter��
���   �$��25��$��Center=�$��Center=�$��5������ #��[  �� -   $��btnRemoveFilter��
���   �$��25��$��Center=�$��Center=�$��7������ #��[ � -   $��gridFilterStatus��$��1��$��1��.+��� -   $��tblFilterStatus��$��Center=�$��Stretch=�  -   $��	dgridUsed��
 .   .  $ Single=�$��2��$��1������ *usedRecords+ �� "  .���  � ��$	 12(���  � ��	 �� .R��� $
��Wrap=�����    $ Vrsta proizvoda��   
Foreground
 ��  �$ * $ 12Q� .  �� $ ConnKindOfProduct�  $ Naziv stavke šanka��
 � � �$ * $ 12Q� .  �� $ ConnStoreItemName�  $ Grupa stavke šanka��$ * $ 12Q� .  �� $ GroupStoreItem�  $' !Količinski udeo proizvoda(kg/l) ��
 ��  �$ * $ 12Q� .  �� $ AmountProduct� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�  $# Količina stavke šanka(kg/l)��
 � � �$ * $ 12Q� .  �� $ AmountStoreItem�  $ Cena stavke šanka(din)��   
FontWeight$
 Bold"�$ * $ 12Q� .  �� $ Price��� -   $��tabIThreshold��$��Pragovi stavki šanka��.��� -    $��gridTab3��#��[	  �� $��* ��� $��2* �.+�� -!   $��gridTab3LeftSide��$��0�� �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $	��105 ��� $��25 ��� $��25 ��� $��25 �.+�� $��1�� �� $��15 ��� $��* �.+��� $	��   ���� $3��-Izaberite grupu stavki šanka koju unosite : ��$��1��� $��2�� �� $��15 ��� $��* �.+��� $	��   ���� -"   $��cmbThresholdGroups��$��1��� $��4�� �� $��15 ��� $��* �.+��� $	��   ���� $-��'Izaberite stavku šanka koju unosite : ��$��1��� $��5�� �� $��15 ��� $��* �.+��� $	��   ���� -#   $��cmbThresholdItems��$��1��� $��7�� �� $��15 ��� $��* �.+��� $	��   ���� $9��3Unesite prag izabrane stavke šanka [broj](kg/l) : ��$��1��� $��8�� �� $��15 ��� $��* �.+��� -$   $��tfThresholdForItem��$��16(�$��25��$��Normal"�$��1��� $��10�� �� $��15 ��� $��* �.+��� $	��   ��$��0���� -%   $��btnEnterThreshold��$,��&  Postavi prag izabrane stavke šanka ��$
��Auto��$��Center=�$
��Left=�$��1��� $��12�� �� $��15 ��� $��* �.+��� $	��   ���� $'��!Prag izabrane stavke šanka je : ��$��1��� $��13�� �� $��15 ��� $��* �.+��� -&   $��tfThresholdForItemRead��$��16(�$��25�� �� 
IsReadOnly . $��Normal"�$��1��� -'   $��gridTab3RightSide��$��1�� �� $��20 ��� $��* ��� $��20 � �� $��30 ��� $��25 ��� $��* �.+�� -(   $��gridFilterTab3��$��1��$��0�� �� $	��290 ��� $	��255 ��� $��10 �$��10���� $��2* ��� $
��0.1* ��� $��30���� $��10 �$��10���� $��30���� $��20 �$��20��.+�c� -)   $��leftToolBarTab3��$��0��$��10��$��36��$��Stretch=�$��Stretch=�$
��Auto��
��������� $4��.   Izaberite kolonu koju želite filtrirati : ��$
��Left=�$��Center=�$��0���� -*   $��cmbFilterColumnTab3��$��1��$��Stretch=�$��Center=�$
��Auto��$
 Hand��.@��� .��!Izaberite kolonu za filtriranje�� .��Šifra stavke�� .��Naziv stavke�� .��Grupa stavke�� .��Jed cena stavke(din)�� .��Jed kol stavke(kg/l)�� .��
Prag(kg/l)�� -+   $��tfFilterTab3��$��Center=�$��Stretch=�$��3���� -,   $��btnAddFilterTab3��
���   �$��25��$��Center=�$��Center=�$��5������ #��[  �� --   $��btnRemoveFilterTab3��
���   �$��25��$��Center=�$��Center=�$��7������ #��[ � -.   $��gridFilterStatusTab3��$��1��$��1��.+��� -/   $��tblFilterStatusTab3��$��Center=�$��Stretch=�  -0   $��dgridThresholds��
 .   .  $ Single=�$��2��$��1������ *ItemsThreshold+ �� "  .���  � ��$	 12(���  � ��	 �� .R��� $
��Wrap=�����    $ Šifra stavke��$ * $ 12Q� .  �� $ CodeProduct�  $ Naziv stavke��$ * $ 12Q� .  �� $ KindOfProduct�  $ Grupa stavke��$ * $ 12Q� .  �� $ Group�  $ Jed cena stavke(din) ��$ * $ 12Q� .  �� $ Price�$ TwoWay=�$ PropertyChanged=�  $ Jed kol stavke(kg/l) ��$ * $ 12Q� .  �� $ Amount�$ TwoWay=�$ PropertyChanged=�  $ 
Prag(kg/l)��$
 Bold"�
 � � �$ * $ 12Q� .  �� $ 	Threshold�$ TwoWay=�$ PropertyChanged=��� -1   $��tabIStateOfStorehouse��$��Stanje šanka��.��� -2   $��gridTab4��#��[	  �� $��30 ��� $��25 ��� $��8* ��� $��2* � �� $	��20* ��� $
��538* ��� $
��812* ��� $	��20* �.+�� -3   $��gridAllFilterData��$��0��$��2��$��1��$��2�� �� $��* ��� $��* �.+�� -4   $��gridFilterTab4��$��0��$��0�� �� $	��290 ��� $	��255 ��� $��10 �$��10���� $��2* ��� $
��0.1* ��� $��30���� $��10 �$��10���� $��30���� $��20 �$��20��.+�c� -5   $��leftToolBarTab4��$��0��$��10��$��36��$��Stretch=�$��Stretch=�$
��Auto��
��������� $4��.   Izaberite kolonu koju želite filtrirati : ��$
��Left=�$��Center=�$��0���� -6   $��cmbFilterColumnTab4��$��1��$��Stretch=�$��Center=�$
��Auto��$
 Hand��.@��� .��!Izaberite kolonu za filtriranje�� .��Šifra stavke šanka�� .��Naziv stavke šanka�� .��Grupa stavke šanka�� .��" Količina stavke u šanku (kg/l)�� .��Vrednost stavke šanka (din)�� -7   $��tfFilterTab4��$��Center=�$��Stretch=�$��3��� -8   $��gridFilterStatusTab4��$��0��$��1��.+��� -9   $��tblFilterStatusTab4��$��Center=�$��Stretch=�  -:   $��dgridStateOfStorehouse��
 .   .  $ Single=�$��2��$��1��$��2������ *StorehouseItems+ �� "  .���  � ��$	 12(���  � ��	 �� .R��� $
��Wrap=�����    $ Šifra stavke šanka��$ * $ 12Q� .  �� $ ItemCode�  $ Naziv stavke šanka��$ * $ 12Q� .  �� $ ItemName�  $ Grupa stavke šanka��$ * $ 12Q� .  �� $ 	ItemGroup�  $' !Količina stavke u šanku (kg/l) ��$ * $ 12Q� .  �� $ ItemRealAmount�$ TwoWay=�$ PropertyChanged=�  $" Vrednost stavke šanka (din)��$ * $ 12Q� .  �� $ 	ItemPrice�$ TwoWay=�$ PropertyChanged=�� -;   $��gridControls��$��3��$��1��$��2�� �� $��* ��� $��* ��� $��* ��� $��10 ��� $��* � �� $��* ��� $��* ��� $��* �.+��� -<   $%��btnCreateStateOfStorehouseExcel��$��Kreiraj stanje šanka��$��Stretch=�$��Center=�$��0��$��4���� -=   $%��tfCorrectionDelReasonStorehouse��$)��#Unesite razlog korekcije/uklanjanja��
��� E��$��Center=�$��Stretch=�$��2��$��0��$��2���� ->   $$��btnStateOfStorehouseCorrection��$%��Uradi korekciju izabrane stavke��$��Stretch=�$��Center=�$��1��$��4���� -?   $ ��btnStateOfStorehouseDelete��$��Ukloni izabranu stavku��$��Center=�$��2��$��4���� -@   $	��tf1��
�������
���   �$��Stretch=�$��Center=�$��0��$��0���� -A   $	��tf2��
�������
���   �$��Stretch=�$��Center=�$��0��$��1���� -B   $	��tf3��
�������
���   �$��Stretch=�$��Center=�$��0��$��2���� -C   $	��tf4��
�������
���   �$��Stretch=�$��Center=�$��1��$��0���� -D   $	��tf5��
�������
���   �$��Stretch=�$��Center=�$��1��$��1���� $)��# Datum unosa korekcije/uklanjanja :��$
��Left=�$��Center=�$��1��$��2��  -E   $��datepickerCorrDelStore��$��15(�$��Stretch=�$��Center=�$��2��$��2��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� -F   $(��"tabIDailyOverviewOfStateStorehouse��$%��Pregled stanja šanka po danima��.��� -G   $��gridTab5��#��[	  �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��* ��� $��25 ��� $��25 ��� $��25 �.+�� -H   $��gridLoadReport��$��2�� �� $��20 ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��20 �.+��� $��Početni datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��1��  -I   $��datepickerStartTab5��$��15(�$��2��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� $��Krajnji datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��3��  -J   $��datepickerEndTab5��$��15(�$��4��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� -K   $��btnloadReportStateStore��$��Ucitaj izveštaj��$��Center=�$��Stretch=�$��6��� -L   $��gridFilterTab5��$��4�� �� $��20 ��� $	��290 ��� $	��280 ��� $��10 �$��10���� $��2* ��� $
��0.1* ��� $��30���� $��10 �$��10���� $��30���� $��20 �$��20��.+�c� -M   $��leftToolBarTab5��$��1��$��8��$��36��$��Stretch=�$��Stretch=�$
��Auto��
��������� $4��.   Izaberite kolonu koju želite filtrirati : ��$
��Left=�$��Center=�$��1���� -N   $��cmbFilterColumnTab5��$��2��$��Stretch=�$��Center=�$
��Auto��$
 Hand��.@��� .��!Izaberite kolonu za filtriranje�� .��Šifra stavke šanka�� .��Naziv stavke šanka�� .��Grupa stavke šanka�� -O   $��tfFilterTab5��$��Center=�$��Stretch=�$��4��� -P   $��gridFilterStatusTab5��$��0��$��5�� �� $��20 ��� $��* ��� $��20 �.+��� -Q   $��tblFilterStatusTab5��#��[	 $��1��$��Center=�$��Stretch=�� -R   $��gridDataGrud��$��6�� �� $��15 ��� $��* ��� $��15 �.+�  -S   $!��dataGridReadStateStorehouse��$��Stretch=�$��Stretch=�$
��Auto��
 .     CanUserReorderColumns .   .     RowBackground
 �����   AlternatingRowBackground# [ $��Stretch=�$��Stretch=�$��1������ *sRecordState+ �� "  .���  � ��$	 12(���  � ��	 �� .R��� $
��Wrap=�����    $ Šifra stavke šanka��$ *  .  �� *StoreItemCode+  $ Naziv stavke šanka��$ *  .  �� *StoreItemName+  $ Grupa stavke šanka��$ *  .  �� *StoreItemGroup+  $' !Količina stavke u šanku (kg/l) ��$ *  .  �� *
RealAmount+  $) #Datum stanja šanka (na kraju dana)��$ *  .  �� *StateOfEndDateTime+