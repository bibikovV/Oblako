   M S B A M L   `   `   `  ����  NetFileManagerServer.    (NetFileManagerServer.Views.FilesListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� 
TargetType �� Property �� Value��%w   '��       �� 5	   
   " ��6   .�5
      ��  � ��$ 2q�6'   �� 5       � ��$
 10,5q�6(   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ]� $��0��6   ��.  .+�5      �� $
��Left=�6   $��Добавить��5      ��5      �� *
AddCommand+ �� Mode$ OneWay=��� 5      $
��Left=�6   $��Удалить��5      ��5      �� *RemoveCommand+$ OneWay=�5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dGrid��5      $��1��5         SelectionMode$ Single=�5       $��2q�5!         CanUserAddRows .  	   CanUserDeleteRows	 .  
   CanUserResizeColumns
 .     AutoGenerateColumns .  ��5$      �� *Files+$ OneWay=� �� SelectedItem 5%      �� *SelectedFile+$ TwoWay=�   Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6I   �� *Name+$ OneWay=�  5(      $ Путь��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 63    .  6O   �� *Path+$ OneWay=�5*      �� 5+   
   $��2��6   $��2q�5,       �� Padding$ 3q�6   $��Right=�5-      ��5.      ��  �� ElementName$ dGrid�� �� Path$ Items.Count�$ OneWay=� �� StringFormat$ Файлов: {0}��5/      50      