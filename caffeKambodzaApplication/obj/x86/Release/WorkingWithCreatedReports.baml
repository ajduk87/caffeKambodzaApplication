   M S B A M L   `   `   `  ����   caffeKambodzaApplication8    2caffeKambodzaApplication.WorkingWithCreatedReports   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources   
  Color1 
 Color2 
 Color3 
 Color4 
 Color5 
 Color6  	Gradient3 �� 
StartPoint �� EndPoint � Color � Offset  	Gradient4��%�  &	        &	      &	      &	 ,     &	 ?     &	 N     &	 b     &	 X    ��	Black����	White����Gray����	LightGray����	Green����
LightGreen���� $	 0,0&�$	 1,0&�.��� # �  $	 0.1Q�� # �  $	 0.2Q�� # �  $	 0.3Q�� # �  $	 0.4Q�� # �  $	 0.5Q�� # �  $	 0.6Q�� # �  $	 0.7Q�� # �  $	 0.8Q�� # �  $	 0.9Q�� # �  $ 1Q��� $	 0,0&�$	 1,1&�.��� $
 Gray��$	 0.0Q�� $ White��$	 0.1Q�� $ 	LightGray��$	 0.2Q�� $
 Gray��$	 0.3Q�� $ White��$	 0.4Q�� $ 	LightGray��$	 0.5Q�� $
 Gray��$	 0.6Q�� $ White��$	 0.7Q�� $ 	LightGray��$	 0.8Q�� $
 Gray��$	 0.9Q�� $ White��$ 1Q�.��� .+��� -   $��
tabcontrol��$
��Auto��$
��Auto��$��Stretch=�$��Stretch=�.@��� -   $��tabIStateOfStorehouse��$&�� Učitavanje kreiranih izveštaja��.��� -   $��gridHistoryTab1��#��[  � RowDefinitions �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��* ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 �.+�� -   $��gridLoadReport��$��2�� � ColumnDefinitions �� $��20 ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��20 �.+��� $��Početni datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��1��(  "System.Windows.Controls.DatePicker  -   $��datepickerStartTab1��$��15(�$��2��  :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property	 �� Value��%6   '        �� "  .���  �@��
IsReadOnly	 . �� $��Krajnji datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��3��  -   $��datepickerEndTab1��$��15(�$��4��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� -   $��btnloadReport��$��Učitaj izveštaj��$��Center=�$��Stretch=�$��6��� -   $��gridDataGrud��$��4�� �� $��15 ��� $��* ��� $��15 �.+�&   System.Windows.Controls.DataGrid  -	   $��dataGridRead��$��Stretch=�$��Stretch=�$
��Auto��
   AutoGenerateColumns
 .     CanUserReorderColumns .     CanUserAddRows .     RowBackground
 �����   AlternatingRowBackground# [ $��Stretch=�$��Stretch=�$��1������ *
_itemsLoad+   ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��	 �� .R��� $
��Wrap=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Šifra proizvoda��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *CodeProduct+  $ Vrsta proizvoda��$ *  .  �� *KindOfProduct+  $ Jedinična cena��$ *  .  �� *Price+  $ 	Količina��$ *  .  �� *Amount+  $ Vrednost stavke��$ *  .  �� *
CostItem+  $ Datum kreiranja��$ *  .  �� *Date+� -
   $��gridFirstRowTfs��$��6�� �� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 �.+��� -   $	��tf1�� �� 
IsReadOnly . 
���   �$��Stretch=�$��Center=�$��1���� -   $	��tf2�� . 
���   �$��Stretch=�$��Center=�$��3���� -   $	��tf3�� . 
���   �$��Stretch=�$��Center=�$��5��� -   $��gridSecondRowTfs��$��7�� �� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 �.+��� -   $	��tf4��
���   �$��Stretch=�$��Center=�$��1���� -   $	��tf5�� . 
���   �$��Stretch=�$��Center=�$��3���� -   $	��tf6�� . 
���   �$��Stretch=�$��Center=�$��5��� -   $��gridDeletionRowTfs��$��9�� �� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 �.+��� -   $��tfDeletionOutput��$"��Unesi razlog brisanja stavke��
��� E��
���   �$��Stretch=�$��Center=�$��1��$��5���� -   $��btnDeletion��$ ��Obriši selektovanu stavku��$��Center=�$��Stretch=�$��7��� -   $��gridCorrectionRowTfs��$��10�� �� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 �.+��� -   $��tfCorrectionOutput��$#��Unesi razlog korekcije stavke��
��� E��
���   �$��Stretch=�$��Center=�$��1��$��5���� -   $��btnCorrection��$%��Uradi korekciju izabrane stavke��$��Center=�$��Stretch=�$��7���� -   $%��tabIStateOfStorehouseByProducts��$5��/Učitavanje kreiranih izveštaja po proizvodima��.��� -   $��gridHistoryTab2��#��[  �� $��25 ��� $��25 ��� $��35 ��� $��25 ��� $��* ��� $��25 �.+�� -   $��gridLoadReportbyProduct��$��2�� �� $��20 ��� $��* ��� $��* ��� $��* ��� $��* ��� $
��1.5* ��� $��2* ��� $��* ��� $��* ��� $��20 �.+��� $��Početni datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��1��  -   $��datepickerStartTab2��$��15(�$��Center=�$��2��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� $��Krajnji datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��3��  -   $��datepickerEndTab2��$��15(�$��Center=�$��4��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� $��Izaberite proizvod : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��5���� -   $��cmbProductsTab2�� � Cursor$
 Hand��$��35��$��20(�$
��Bold"�$��6������  �� Path$ Products��� -   $��btnloadReportByProduct��$��Učitaj izveštaj��$��Center=�$��Stretch=�$��8��� -   $��gridDataGrudTab2��$��4�� �� $��15 ��� $��* ��� $��15 �.+�  -    $��dataGridReadByProduct��$��Stretch=�$��Stretch=�$
��Auto��
 .   .   .  
 �����# [ $��Stretch=�$��Stretch=�$��1������ *__itemsLoad2+ �� "  .���  � ��	 �� .R��� $
��Wrap=�����    $ Šifra proizvoda��$ *  .  �� *CodeProduct+  $ Vrsta proizvoda��$ *  .  �� *KindOfProduct+  $ Jedinična cena��$ *  .  �� *Price+  $ 	Količina��$ *  .  �� *Amount+  $ Vrednost stavke��$ *  .  �� *
CostItem+  $ Datum kreiranja��$ *  .  �� *Date+�� -!   $#��tabIStateOfStorehouseDeletion��$&�� Pregled obrisanih stavki prometa��.��� -"   $��gridHistoryTab3��#��[  �� $��25 ��� $��25 ��� $��35 ��� $��25 ��� $��* ��� $��25 �.+�� -#   $��gridLoadReportDeletion��$��2�� �� $��20 ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��20 �.+��� $��Početni datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��1��  -$   $��datepickerStartTab3��$��15(�$��Center=�$��2��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� $��Krajnji datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��3��  -%   $��datepickerEndTab3��$��15(�$��Center=�$��4��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� -&   $��btnloadReportDeletion��$��Učitaj izveštaj��$��Center=�$��Stretch=�$��6��� -'   $��gridDataGrudTab3��$��4�� �� $��15 ��� $��* ��� $��15 �.+�  -(   $��dataGridReadDeletion��$��Stretch=�$��Stretch=�$
��Auto��
 .   .   .  
 �����# [ $��Stretch=�$��Stretch=�$��1������ *cvItemsDeleted+ �� "  .���  � ��	 �� .R��� $
��Wrap=�����    $ Šifra proizvoda��$ *  .  �� *CodeProduct+  $ Vrsta proizvoda��$ *  .  �� *KindOfProduct+  $ Jedinična cena��$ *  .  �� *Price+  $ 	Količina��$ *  .  �� *Amount+  $ Vrednost stavke��$ *  .  �� *
CostItem+  $ Datum kreiranja��$ *  .  �� *Date+  $ Razlog uklanjanja��$ *  .  �� *DeleteReason+�� -)   $%��tabIStateOfStorehouseCorrection��$(��"Pregled korigovanih stavki prometa��.��� -*   $��gridHistoryTab4��#��[  �� $��25 ��� $��25 ��� $��35 ��� $��25 ��� $��* ��� $��25 �.+�� -+   $��gridLoadReportCorrection��$��2�� �� $��20 ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��20 �.+��� $��Početni datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��1��  -,   $��datepickerStartTab4��$��15(�$��Center=�$��2��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� $��Krajnji datum : ��$
��Auto��$��Right=�$��Center=�$��
ExtraBlack"�$��3��  --   $��datepickerEndTab4��$��15(�$��Center=�$��4��  ��%6   '        �� "  .���  �@��
IsReadOnly	 . �� -.   $��btnloadReportCorrection��$��Učitaj izveštaj��$��Center=�$��Stretch=�$��6��� -/   $��gridDataGrudTab4��$��4�� �� $��15 ��� $��* ��� $��15 �.+�  -0   $��dataGridReadCorrection��$��Stretch=�$��Stretch=�$
��Auto��
 .   .   .  
 �����# [ $��Stretch=�$��Stretch=�$��1������ *cvItemsCorrected+ �� "  .���  � ��$	 12(���  � ��	 �� .R��� $
��Wrap=�����    $ Šifra proizvoda��$ *  .  �� *CodeProduct+  $ Vrsta proizvoda��$ *  .  �� *KindOfProduct+  $ Jedinična cena��$ *  .  �� *Price+  $ Stara količina��$ *  .  �� *	OldAmount+  $ Nova količina��$ *  .  �� *Amount+  $ Razlika količina��$ *  .  �� *
DiffAmount+  $ Stara vrednost stavke��$ *  .  �� *OldCostItem+  $ Nova vrednost stavke��$ *  .  �� *
CostItem+  $ Razlika vrednosti��$ *  .  �� *DiffCostItem+  $ Datum kreiranja��$ *  .  �� *Date+  $ Razlog korigovanja��$ *  .  �� *CorrectionReason+