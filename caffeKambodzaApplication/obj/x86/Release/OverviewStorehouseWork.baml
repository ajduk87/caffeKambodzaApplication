   M S B A M L   `   `   `  ���� S  OcaffeKambodzaApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:caffeKambodzaApplicationcaffeKambodzaApplication  5    /caffeKambodzaApplication.OverviewStorehouseWork   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:caffeKambodzaApplication       � 	Resources  .   (caffeKambodzaApplication.StringConverter   converterYESNO 
 Color1 
 Color2 
 Color3 
 Color4 
 Color5 
 Color6  	Gradient3 �� 
StartPoint �� EndPoint � Color � Offset  	Gradient4��%  &	        &	      &	      &	 #     &	 1     &	 D     &	 S     &	 g     &	 ]      ��	Black����	White����Gray����	LightGray����	Green����
LightGreen���� $	 0,0&�$	 1,0&�.��� # �  $	 0.1Q�� # �  $	 0.2Q�� # �  $	 0.3Q�� # �  $	 0.4Q�� # �  $	 0.5Q�� # �  $	 0.6Q�� # �  $	 0.7Q�� # �  $	 0.8Q�� # �  $	 0.9Q�� # �  $ 1Q��� $	 0,0&�$	 1,1&�.��� $
 Gray��$	 0.0Q�� $ White��$	 0.1Q�� $ 	LightGray��$	 0.2Q�� $
 Gray��$	 0.3Q�� $ White��$	 0.4Q�� $ 	LightGray��$	 0.5Q�� $
 Gray��$	 0.6Q�� $ White��$	 0.7Q�� $ 	LightGray��$	 0.8Q�� $
 Gray��$	 0.9Q�� $ White��$ 1Q�.��� .+��� -   $��
tabcontrol��$
��Auto��$
��Auto��$��Stretch=�$��Stretch=�.@��� -   $��tabIStateOfStorehouse��$,��&Učitavanje ikada unetih šanka stavki��.��� -   $��gridHistoryTab1��#��[  � RowDefinitions �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��* ��� $��25 ��� $��25 ��� $��25 �.+�� -   $��gridLoadReport��$��2�� � ColumnDefinitions �� $��20 ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��20 �.+��� $��Početni datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��1��(  "System.Windows.Controls.DatePicker  -   $��datepickerStartTab1��$��15(�$��2��  :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property	 �� Value��%6   '        �� "  .���  �@��
IsReadOnly	 . �� $��Krajnji datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��3��  -   $��datepickerEndTab1��$��15(�$��4��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� -   $��btnloadReport��$��Učitaj izveštaj��$��Center=�$��Stretch=�$��6��� -   $��gridFilterTab1��$��4�� �� $��20 ��� $	��290 ��� $	��280 ��� $��10 �$��10���� $��2* ��� $
��0.1* ��� $��30���� $��10 �$��10���� $��30���� $��20 �$��20��.+�c� -	   $��leftToolBarTab1��$��1��$��8��$��36��$��Stretch=�$��Stretch=�$
��Auto��
��������� $4��.   Izaberite kolonu koju želite filtrirati : ��$
��Left=�$��Center=�$��1���� -
   $��cmbFilterColumnTab1��$��2��$��Stretch=�$��Center=�$
��Auto��
 � Cursor$

 Hand��.@��� .��!Izaberite kolonu za filtriranje�� .��Šifra stavke šanka�� .��Naziv stavke šanka�� .��" Količina stavke u šanku (kg/l)�� .��Vrednost stavke šanka (din)�� -   $��tfFilterTab1��$��Center=�$��Stretch=�$��4��� -   $��gridFilterStatusTab1��$��0��$��5�� �� $��20 ��� $��* ��� $��20 �.+��� -   $��tblFilterStatusTab1��#��[ $��1��$��Center=�$��Stretch=�� -   $��gridDataGrud��$��6�� �� $��15 ��� $��* ��� $��15 �.+�&   System.Windows.Controls.DataGrid  -   $��dataGridReadStore��$��Stretch=�$��Stretch=�$
��Auto��   AutoGenerateColumns .     CanUserReorderColumns .     CanUserAddRows .     RowBackground
 �����   AlternatingRowBackground# [ $��Stretch=�$��Stretch=�$��1������ *	sRecord+   ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��$	 12(���  � ��	 �� .R��� $
��Wrap=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Šifra stavke šanka��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    
IsReadOnly . 1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding �� *StoreItemCode+  $ Naziv stavke šanka��$ *  .  �� *StoreItemName+  $' !Količina stavke u šanku (kg/l) ��$ *  .  �� *
RealAmount+  $" Vrednost stavke šanka (din)��$ *  .  �� *	RealPrice+  $ Datum korisnikovog unosa��$ *  .  �� *UserCanControlDateTime+�� -   $��tabIStateOfStorehouseTab2��$/��)Učitavanje ikada obrisanih stavki šanka��.��� -   $��gridHistoryTab2��#��[  �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��* ��� $��25 ��� $��25 ��� $��25 �.+�� -   $��gridLoadReportTab2��$��2�� �� $��20 ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��20 �.+��� $��Početni datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��1��  -   $��datepickerStartTab2��$��15(�$��2��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� $��Krajnji datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��3��  -   $��datepickerEndTab2��$��15(�$��4��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� -   $��btnloadReportTab2��$��Učitaj izveštaj��$��Center=�$��Stretch=�$��6��� -   $��gridFilterTab2��$��4�� �� $��20 ��� $	��290 ��� $	��280 ��� $��10 �$��10���� $��2* ��� $
��0.1* ��� $��30���� $��10 �$��10���� $��30���� $��20 �$��20��.+�c� -   $��leftToolBarTab2��$��1��$��8��$��36��$��Stretch=�$��Stretch=�$
��Auto��
��������� $4��.   Izaberite kolonu koju želite filtrirati : ��$
��Left=�$��Center=�$��1���� -   $��cmbFilterColumnTab2��$��2��$��Stretch=�$��Center=�$
��Auto��$

 Hand��.@��� .��!Izaberite kolonu za filtriranje�� .��Šifra stavke šanka�� .��Naziv stavke šanka�� .��" Količina stavke u šanku (kg/l)�� .��Vrednost stavke šanka (din)�� -   $��tfFilterTab2��$��Center=�$��Stretch=�$��4��� -   $��gridFilterStatusTab2��$��0��$��5�� �� $��20 ��� $��* ��� $��20 �.+��� -   $��tblFilterStatusTab2��$��1��$��Center=�$��Stretch=�� -   $��gridDataGridTab2��$��6�� �� $��15 ��� $��* ��� $��15 �.+�  -   $��dataGridReadStoreTab2��$��Stretch=�$��Stretch=�$
��Auto�� .   .   .  
 �����# [ $��Stretch=�$��Stretch=�$��1������ *
sRecordDel+ �� "  .���  � ��$	 12(���  � ��	 �� .R��� $
��Wrap=�����    $ Šifra stavke šanka��$ *  .  �� *StoreItemCode+  $ Naziv stavke šanka��$ *  .  �� *StoreItemName+  $' !Količina stavke u šanku (kg/l) ��$ *  .  �� *
RealAmount+  $" Vrednost stavke šanka (din)��$ *  .  �� *	RealPrice+  $$ Datum za koji se briše stavka��$ *  .  �� *CreatedDateTimeInApp+  $ Razlog brisanja��$ *  .  �� *DeleteReason+�� -   $��tabIStateOfStorehouseTab3��$1��+Učitavanje ikada korigovanih stavki šanka��.��� -   $��gridHistoryTab3��#��[  �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��* ��� $��25 ��� $��25 ��� $��25 �.+�� -    $��gridLoadReportTab3��$��2�� �� $��20 ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��20 �.+��� $��Početni datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��1��  -!   $��datepickerStartTab3��$��15(�$��2��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� $��Krajnji datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��3��  -"   $��datepickerEndTab3��$��15(�$��4��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� -#   $��btnloadReportTab3��$��Učitaj izveštaj��$��Center=�$��Stretch=�$��6��� -$   $��gridFilterTab3��$��4�� �� $��20 ��� $	��290 ��� $	��280 ��� $��10 �$��10���� $��2* ��� $
��0.1* ��� $��30���� $��10 �$��10���� $��30���� $��20 �$��20��.+�c� -%   $��leftToolBarTab3��$��1��$��8��$��36��$��Stretch=�$��Stretch=�$
��Auto��
��������� $4��.   Izaberite kolonu koju želite filtrirati : ��$
��Left=�$��Center=�$��1���� -&   $��cmbFilterColumnTab3��$��2��$��Stretch=�$��Center=�$
��Auto��$

 Hand��.@��� .��!Izaberite kolonu za filtriranje�� .��Šifra stavke šanka�� .��Naziv stavke šanka�� .��(&Stara količina stavke u šanku (kg/l)�� .��'%Nova količina stavke u šanku (kg/l)�� .��Razlika u količini�� .��$"Stara vrednost stavke šanka (din)�� .��#!Nova vrednost stavke šanak (din)�� .��Razlika vrednosti�� .��Razlog korekcije�� -'   $��tfFilterTab3��$��Center=�$��Stretch=�$��4��� -(   $��gridFilterStatusTab3��$��0��$��5�� �� $��20 ��� $��* ��� $��20 �.+��� -)   $��tblFilterStatusTab3��$��1��$��Center=�$��Stretch=�� -*   $��gridDataGridTab3��$��6�� �� $��15 ��� $��* ��� $��15 �.+�  -+   $��dataGridReadStoreTab3��$��Stretch=�$��Stretch=�$
��Auto�� .   .   .  
 �����# [ $��Stretch=�$��Stretch=�$��1������ *
sRecordCor+ �� "  .���  � ��$	 12(���  � ��	 �� .R��� $
��Wrap=�����    $ Šifra stavke šanka��$ *  .  �� *StoreItemCode+  $ Naziv stavke šanka��$ *  .  �� *StoreItemName+  $- 'Stara količina stavke u šanku (kg/l) ��$ *  .  �� *	OldAmount+  $, &Nova količina stavke u šanku (kg/l) ��$ *  .  �� *NewRealAmount+  $ Razlika u količini��$ *  .  �� *DifferenceRealAmount+  $( "Stara vrednost stavke šanka (din)��$ *  .  �� *OldRealPrice+  $' !Nova vrednost stavke šanka (din)��$ *  .  �� *NewRealPrice+  $ Razlika vrednosti��$ *  .  �� *DiffRealPrice+  $' !Datum na koji se korekcija odnosi��$ *  .  �� *CorrectionUserDateTime+  $ Razlog korekcije��$ *  .  �� *CorrectionReason+