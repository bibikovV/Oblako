   M S B A M L   `   `   `  ����   NetFileManagerClient.    (NetFileManagerClient.Views.FilesListView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources   �� 
TargetType �� Property �� Value��%D   '��       �� " ��.���  � ��$ 2q���  � ��$
 10,5q�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+�]� $��0����.  .+��� $
��Left=�$��2q� �� Padding$ 3q�$��Center=�$"��IP-адрес сервера���� $
��Left=�$��2q�$��3q�$��150px������ *
ServerIp+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ 	LostFocus=�]� $��1����.  .+��� $
��Left=�$��Открыть�� � ToolTip$3 -Загрузить и открыть файл��{��� *IsServerReady+$ OneWay=����� *OpenCommand+$ OneWay=��� $
��Left=�$��Открыть папку��$J DОткрыть папку с загруженными файлами������ *OpenFolderCommand+$ OneWay=��� $��Right=�$#��Обновить список��{��� *IsServerReady+$ OneWay=����� *RefreshCommand+$ OneWay=�&   System.Windows.Controls.DataGrid  -   	  Name$	 dGrid��$��2��
   SelectionMode$
 Single=�$��2q�   CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     AutoGenerateColumns .  ���� *Files+$ OneWay=� �� SelectedItem �� *SelectedFile+$ TwoWay=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    Header$ Название��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *.+$ OneWay=��� $��3��$��2q�$ 3q�$��Right=�����  �� ElementName$ dGrid�� �� Path$ Items.Count�$ OneWay=� �� StringFormat$ Файлов: {0}��