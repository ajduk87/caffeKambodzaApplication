   M S B A M L   `   `   `  ����   caffeKambodzaApplication8    2caffeKambodzaApplication.SelectUpdateConnProdStore   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources     AddFilterImage  RemoveFilterImage 
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
 Gray��$	 0.9Q�� $ White��$ 1Q�.���  � RowDefinitions �� $��8 ��� $��55 ��� $	��10* ��� $��2* �.+�� -   $��gridAllFilterData��$��1��$��0��#��[	  �� $��* ��� $��* �.+�� -   $��gridFilterRecipes��$��0��$��0�� � ColumnDefinitions �� $	��290 ��� $	��255 ��� $��10 �$��10���� $��2* ��� $
��0.1* ��� $��30���� $��10 �$��10���� $��30���� $��20 �$��20��.+�c� -   $��leftToolBarRecipes��$��0��$��10��$��36��$��Stretch=�$��Stretch=�$
��Auto��
��������� $4��.   Izaberite kolonu koju želite filtrirati : ��$
��Left=�$��Center=�$��0���� -   $��cmbFilterColumnRecipes��$��1��$��Stretch=�$��Center=�$
��Auto�� � Cursor$
 Hand��.@��� .��!Izaberite kolonu za filtriranje�� .��Šifra proizvoda�� .��Šifra stavke šanka�� .��Vrsta proizvoda�� .��Naziv stavke šanka�� .��Grupa stavke šanka�� .��" Količinski udeo proizvoda(kg/l)�� .��Količina stavke šanka(kg/l)�� .��Cena stavke šanka(din)�� -   $��tfFilterRecipes��$��Center=�$��Stretch=�$��3���� -   $��btnAddFilterRecipes��
���   �$��25��$��Center=�$��Center=�$��5������ #��[  �� -   $��btnRemoveFilterRecipes��
���   �$��25��$��Center=�$��Center=�$��7������ #��[ � -   $��gridFilterStatusRecipes��$��0��$��1��.+��� -	   $��tblFilterStatusRecipes��$��Center=�$��Stretch=�&   System.Windows.Controls.DataGrid  -
   $#��dgridCurrProductStoreItemConn��   AutoGenerateColumns .  	   CanUserAddRows	 .  
   SelectionMode$
 Single=�$��2������ *	records+   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value$ 12(���  � �� �� .R��� $
��Wrap=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Šifra proizvoda��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    FontSize$ 12Q�   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ ConnCodeProduct�  $ Šifra stavke šanka��$ * $ 12Q� .  �� $ ConnStoreItemCode�  $ Vrsta proizvoda��   
Foreground
 ��  �$ * $ 12Q� .  �� $ ConnKindOfProduct�  $ Naziv stavke šanka��
 � � �$ * $ 12Q� .  �� $ ConnStoreItemName�  $ Grupa stavke šanka��$ * $ 12Q� .  �� $ GroupStoreItem�  $' !Količinski udeo proizvoda(kg/l) ��
 ��  �$ * $ 12Q� .  �� $ AmountProduct� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�  $# Količina stavke šanka(kg/l)��
 � � �$ * $ 12Q� .  �� $ AmountStoreItem�  $ Cena stavke šanka(din)��   
FontWeight$
 Bold"�$ * $ 12Q� .  �� $ Price�� -   $��gridtfsPart��$��3��#��[	  �� $��* ��� $��* ��� $��* ��� $��* � �� $��* ��� $��* ��� $��* �.+��� -   $	��tf1��$	��300��$��Center=�$��Stretch=�$��0��$��0���� -   $	��tf2��$	��300��$��Center=�$��Stretch=�$��1��$��0���� -   $	��tf3��$	��300��$��Center=�$��Stretch=�$��2��$��0���� -   $	��tf4��$	��300��$��Center=�$��Stretch=�$��3��$��0���� -   $	��tf5��$	��300��$��Center=�$��Stretch=�$��0��$��1���� -   $	��tf6��$	��300��$��Center=�$��Stretch=�$��1��$��1���� -   $	��tf7��$	��300��$��Center=�$��Stretch=�$��2��$��1���� -   $	��tf8��$	��300��$��Center=�$��Stretch=�$��3��$��1���� -   $��	btnUpdate��$�� Zapamti promene ��$��Center=�$��Center=�$��3��$��2��