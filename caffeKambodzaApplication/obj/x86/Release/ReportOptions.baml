   M S B A M L   `   `   `  ����   caffeKambodzaApplication,    &caffeKambodzaApplication.ReportOptions   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources   �� MergedDictionaries �� Source��  �� $1 +Resources/OptionsButtonStyleDictionary.xaml?�%    .��� -   $��gridOptionsRoot��   
  Color1 
 Color2 
 Color3 
 Color4 
 Color5 
 Color6  	Gradient3 �� 
StartPoint �� EndPoint � Color � Offset  	Gradient4 �� 
TargetType �� Property	 �� Value��%�  &	        &	      &	      &	 ,     &	 ?     &	 N     &	 b     &	 X    '�� �    '�� �    '	�     '�� $    '�� G    ��	Black����	White����Gray����	LightGray����	Green����
LightGreen���� $	 0,0&�$	 1,0&�.��� # �  $	 0.1Q�� # �  $	 0.2Q�� # �  $	 0.3Q�� # �  $	 0.4Q�� # �  $	 0.5Q�� # �  $	 0.6Q�� # �  $	 0.7Q�� # �  $	 0.8Q�� # �  $	 0.9Q�� # �  $ 1Q��� $	 0,0&�$	 1,1&�.��� $
 Gray��$	 0.0Q�� $ White��$	 0.1Q�� $ 	LightGray��$	 0.2Q�� $
 Gray��$	 0.3Q�� $ White��$	 0.4Q�� $ 	LightGray��$	 0.5Q�� $
 Gray��$	 0.6Q�� $ White��$	 0.7Q�� $ 	LightGray��$	 0.8Q�� $
 Gray��$	 0.9Q�� $ White��$ 1Q��� " ��.���  � ��$	 18(��� " ��.���  � ��$	 18(���  � ��$
	 Bold"��� " 	�.���  � ��$	 18(��� " ��.���  � ��$	 18(��� " ��.���  � ��$	 18(�.+��� -   $��tabControl1��$	��700��$
��Left=�$	��Top=�$
��1400��.@��� -   $��tabIPutanje��$��Putanje��.��� -   $��gridRoot��#��[ .+�� 
 � ShowGridLines
 .  � RowDefinitions �� $��* ��� $��* � � ColumnDefinitions �� $��* ��� $��* �.+�� -   $��gridCmb��$��0��$��0�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��15 �$��15���� $��* �.+��� -   $	��cmb��$4��.Želim uneti direktorijum kreiranja izveštaja��$��Center=�$
��Left=�$��1��$��2���� -   $
��cmb2��$#��Želim uneti naziv izveštaja��$��Center=�$
��Left=�$��1��$��4���� -   $
��cmb3��$(��"Želim uneti ekstenziju izveštaja��$��Center=�$
��Left=�$��1��$��6���� -	   $
��cmb4��{�.  $/��)Želim uneti putanju backup baze podataka��$��Center=�$
��Left=�$��1��$��8��� -
   $��gridInitialOptions��$��1��$��0�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��15 �$��15���� $��* �.+��� -   $��tblCaption1��$8��2Podrazumevani direktorijum kreiranja izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��1���� -   $��tblInitialDir��$��D:\BarBooks��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��2���� -   $��tblCaption2��$'��!Podrazumevani naziv izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��3���� -   $��tblInitialFile��$��Izvestaj_dd/mm/gggg��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��4���� -   $��tblCaption3��$,��&Podrazumevana ekstenzija izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��5���� -   $��tblInitialExtension��$
��XLSX��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��6���� -   $��tblCaption4��$3��-Podrazumevana putanja backup baze podataka : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��7���� -   $��tblInitialDatabasePath��$)��#D:\caffeKambodzaPodaci\BazaPodataka��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��8��� -   $��gridTf��$��0��$��1�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��15 �$��15���� $��6* ��� $��* �.+�� -   $��gridChooseDir��$��1��$��1�� �� $
��Auto ��� $��* �.+��� -   $��tblCaption5��$4��.Unesite direktorijum kreiranja izveštaja :   ��$��
ExtraBlack"�$��Center=�$
��Left=�$��0���� -   $��btnChooseDirPath��$	��...��$��20��$��20��$
��Left=�$��Bottom=�$��1��$��0,0,0,4q��� -   $��tfDir��$
��Auto��$
��Auto��$��
ExtraBlack"�
���   �$��Center=�$��Stretch=�$��2��$��1���� -   $��
btnSaveDir��$��Zapamti��$��Center=�$��Center=�$��2��$��2���� -   $��tblCaption6��$!��Unesite naziv izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��3���� -   $��tfFile��$
��Auto��
���   �$��Center=�$��Stretch=�$��4��$��1���� -   $��btnSaveFile��$��Zapamti��$��Center=�$��Center=�$��4��$��2���� -   $��tblCaption7��$(��"Izaberite ekstenziju izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��5���� -   $��cmbExtension��$
��Auto��$��Center=�$��Stretch=� � Cursor$
 Hand��$��6��$��1���� -   $��btnSaveExtension��$��Zapamti��$��Center=�$��Center=�$��6��$��2��� -   $��gridChooseDatabasePath��$��1��$��7�� �� $
��Auto ��� $��* �.+��� -    $��tblCaption8��$/��)Unesite putanju backup baze podataka :   ��$��
ExtraBlack"�$��Center=�$
��Left=�$��0���� -!   $��btnChooseDatabasePath��$	��...��$��20��$��20��$
��Left=�$��Center=�$��1���� -"   $��tfDatabasePath��$
��Auto��$��
ExtraBlack"�
���   �$��Center=�$��Stretch=�$��8��$��1���� -#   $��btnSaveDatabasePath��$��Zapamti��$��Center=�$��Center=�$��8��$��2��� -$   $��gridSetupOptions��$��1��$��1�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��15 �$��15���� $��6* ��� $��* �.+��� -%   $��tblCaption9��$*��$Direktorijum kreiranja izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��1���� -&   $��tblDir2��$'��!Koristi se podrazumevana vrednost��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��2���� -'   $��btnSaveDir2��$��Vrati��$��Center=�$��Center=�$��2��$��2�� � ToolTip _� .���� .+��� !Vrati na podrazumevanu vrednost�� -(   $��tblCaption10��$��Naziv izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��3���� -)   $��tblFile2��$'��!Koristi se podrazumevana vrednost��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��4���� -*   $��btnSaveFile2��$��Vrati��$��Center=�$��Center=�$��4��$��2�� _� .���� .+��� !Vrati na podrazumevanu vrednost�� -+   $��tblCaption11��$��Ekstenzija izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��5���� -,   $��tblExtension2��$'��!Koristi se podrazumevana vrednost��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��6���� --   $��btnSaveExtension2��$��Vrati��$��Center=�$��Center=�$��6��$��2�� _� .���� .+��� !Vrati na podrazumevanu vrednost�� -.   $��tblCaption12��$%��Putanja backup baze podataka : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��7���� -/   $��tblDatabasePath2��$'��!Koristi se podrazumevana vrednost��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��8���� -0   $��btnSaveDatabasePath2��$��Vrati��$��Center=�$��Center=�$��8��$��2�� _� .���� .+��� !Vrati na podrazumevanu vrednost�� -1   $��tabIApp��$��Opcije Aplikacije��.��� -2   $��gridRootApp��#��[ .+�� 
 .  �� $��* ��� $��* � �� $��* ��� $��* �.+�� -3   $��
gridCmbApp��$��0��$��0�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��15 �$��15���� $��* ��� $��* �.+��� -4   $��tblPageOrientation��$+��%Page Orientation kreiranog izveštaja��$��Center=�$
��Left=�$��2��$��1��	� -5   $��rbtnPortrait�� 	� 	GroupName$ rbtngrouppageOrientation��$��Portrait��$��Center=�$
��Left=�$��3��$��1��	� -6   $��rbtnLandscape��$ rbtngrouppageOrientation��$��	Landscape��$��Center=�$
��Left=�$��4��$��1���� -7   $��chbtnWriteCode��$0��*Uključi upis šifre proizvoda u izveštaj��$
��Left=�$��Center=�$��6��$��1���� -8   $��chkbMask��$��Uključi masku��$
��Left=�$��Center=�$��8��$��1���� -9   $��cmbAppSound��$��Uključi zvuk aplikacije��$��Center=�$
��Left=�$��2��$��2���� -:   $��
cmbAppOpen��$/��)Otvori fajl kada se završi sa kreiranjem��$��Center=�$
��Left=�$��2��$��4���� -;   $��cmbAppCompany��$��Želim uneti ime firme��$��Center=�$
��Left=�$��2��$��6���� -<   $��cmbAppAuthor��$$��Želim uneti autora izveštaja��$��Center=�$
��Left=�$��2��$��8���� -=   $��cmbAppStateStorehouse��$��17(�$,��&Želim promeniti putanju stanja šanka��$��Center=�$
��Left=�$��2��$��10��� ->   $#��gridInitialApplicationOptions��$��1��$��0�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��15 �$��15���� $��* �.+��� -?   $��tblCaptionShowCode��$-��'Opcija prikazivanja šifre proizvoda : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��2���� -@   $��tblSound��$��Zvuk je isključen��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��3���� -A   $ ��tblCaptionOpenAfterCreated��$C��=Opcija otvaranja fajla neposredno posle njegovog kreiranja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��4���� -B   $��tblOpenWhenCreated��$;��5Fajl se ne otvara neposredno posle njegovog kreiranja��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��5���� -C   $��tblCaption1App��$"��Podrazumevani naziv firme : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��6���� -D   $��tblInitialCompany��$��caffe KAMBODŽA��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��7���� -E   $��tblCaption2App��$'��!Podrazumevani autor izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��8���� -F   $��tblInitialAuthor��$��Saša Milošević��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��9��� -G   $��	gridTfApp��$��0��$��1�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��15 �$��15���� $��6* ��� $��* �.+�� -H   $��gridChangeOptionsApp��$��1��$��1�� �� $
��Auto ��� $��* �.+��� -I   $��tblCaption5App��$��Unesite naziv firme :   ��$��
ExtraBlack"�$��Center=�$
��Left=�$��0���� -J   $��	tfCompany��$
��Auto��$
��Auto��$��
ExtraBlack"�
���   �$��Center=�$��Stretch=�$��2��$��1���� -K   $��btnSaveCompany��$��Zapamti��$��Center=�$��Center=�$��2��$��2���� -L   $��tblCaption6App��$(��"Unesite naziv autora izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��5���� -M   $��tfAuthor��$
��Auto��
���   �$��Center=�$��Stretch=�$��6��$��1���� -N   $��btnSaveAuthor��$��Zapamti��$��Center=�$��Center=�$��6��$��2��� -O   $��gridChooseDirStorehouse��$��1��$��8�� �� $
��Auto ��� $��* �.+��� -P   $��tblCaption7App��$2��,Promenite putanju kreiranja stanja šanka : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��0���� -Q   $��btnChooseDirPathStore��$	��...��$��20��$��20��$
��Left=�$��Center=�$��1���� -R   $��tfStateOfStorehouse��$
��Auto��
���   �$��Center=�$��Stretch=�$��9��$��1���� -S   $"��btnSavePathStateOfStorehouse��$��Zapamti��$��Center=�$��Center=�$��9��$��2��� -T   $��gridSetupOptionsApp��$��1��$��1�� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � �� $��15 �$��15���� $��6* ��� $��* �.+��� -U   $��tblCaption9App��$��Naziv firme : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��1���� -V   $��tblCompany2��$'��!Koristi se podrazumevana vrednost��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��2���� -W   $��btnreturnCompany��$��Vrati��$��Center=�$��Center=�$��2��$��2�� _� .���� .+��� !Vrati na podrazumevanu vrednost�� -X   $��tblCaption10App��$ ��Naziv autora izveštaja : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��5���� -Y   $��
tblAuthor2��$'��!Koristi se podrazumevana vrednost��$��
ExtraBlack"�
�������$��Center=�$
��Left=�$��1��$��6���� -Z   $��tblCaption12App��$(��"Putanja kreiranja stanja šanka : ��$��
ExtraBlack"�$��Center=�$
��Left=�$��1��$��8���� -[   $��tblPathStateStore2��$��
ExtraBlack"�
���/kU�$��Center=�$
��Left=�$��1��$��9���� -\   $��btnReturnAuthor��$��Vrati��$��Center=�$��Center=�$��6��$��2�� _� .���� .+��� !Vrati na podrazumevanu vrednost�� -]   $��tabISchedule��$=��7Raspored prikazivanja proizvoda kafića u knjizi šanka��.��� -^   $��gridScheduleRoot��#��[  �� $	��150 ��� $��* ��� $	��100 � �� $��20 ��� $��* ��� $��20 �.+�� -_   $��	gridField��$��1�� �� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 ��� $��* ��� $��20 � �� $��15 ��� $��25 ��� $��10 ��� $��25 ��� $��10 ��� $��25 ��� $��10 ��� $��25 �.+��� $��Vrsta proizvoda :��$
��Left=�$��Center=�$��3��$��1���� -`   $��tfProductKind��$��16(� �� 
IsReadOnly . $��Stretch=�$��Center=�$��3��$��3���� $7��1Redni broj prikaza u knjizi šanka (stari/novi) :��$
��Left=�$��Center=�$��5��$��1���� -a   $��tfScheduleOld��$��16(� . $��Stretch=�$��Center=�$��5��$��3���� -b   $��tfScheduleNew��$��16(�$��Stretch=�$��Center=�$��5��$��5���� -c   $��btnChangeSchedule��$��   promeni redosled   ��$��5��$��7��� -d   $��gridDataGridSchedule��$��1��$��1�� �� $��20 ��� $��* ��� $��20 �.+�&   System.Windows.Controls.DataGrid  -e   $��dataGridSchedule��$��Stretch=�$��Stretch=�$
��Auto��   AutoGenerateColumns .     CanUserReorderColumns .     CanUserAddRows .     RowBackground
 �����   AlternatingRowBackground# [ $��Stretch=�$��Stretch=�$��1������ *productsWithOrder+   ColumnHeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��	 �� .R��� $
��Wrap=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Šifra proizvoda��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *CodeProduct+  $ Vrsta proizvoda��$ *  .  �� *KindOfProduct+  $( "Redni broj prikaza u knjizi šanka��$ *  .  �� *OrderNumber+