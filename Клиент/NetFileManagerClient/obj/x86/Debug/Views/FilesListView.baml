   M S B A M L   `   `   `  ����O  KNetFileManagerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:NetFileManagerClientNetFileManagerClient  5      .    (NetFileManagerClient.Views.FilesListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      CNetFileManagerClient"clr-namespace:NetFileManagerClient     5        � 	Resources  5	       �� 
TargetType �� Property �� Value��%w   '��       �� 5
   
   " ��6   .�5      ��  � ��$ 2q�6'   �� 5       � ��$
 10,5q�6(   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ]� $��0��6   ��.  .+�5      �� $
��Left=�6   $��2q�5       �� Padding$ 3q�6#   $��Center=�5      $"��IP-адрес сервера��5      �� 5      $
��Left=�6   $��2q�5      $��3q�6!   $��150px��5      ��5       �� *
ServerIp+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ 	LostFocus=��� 5!      $��Вы вошли как:��6   $��28��6,   $	��Top=�68   �� 5"      -   5         ��Name$ 	LabelUser��5"      $��28��6(   $	��Top=�64   ��5#      �� *	Auth.User+63   5%      ]� 5&   
   $��1��6   ��.  .+�5'      �� $
��Left=�6   $��Открыть��5(      	 � ToolTip$3	 -Загрузить и открыть файл��5)      {�5*      �� *IsServerReady+$ OneWay=���5+      �� *OpenCommand+$ OneWay=��� 5,      $
��Left=�6   $��Открыть папку��5-      $J	 DОткрыть папку с загруженными файлами��5.      ��5/      �� *OpenFolderCommand+$ OneWay=��� 50      $��Right=�6   $#��Обновить список��51      {�52      �� *IsServerReady+$ OneWay=���53      �� *RefreshCommand+$ OneWay=��� 54      $��Добавить файл��6   $��29��6-   $��0,2,0,0q�69   $	��Top=�6J   $	��109��6b   
 J� RenderTransformOrigin$
 0.493,0.533&�6n   {�55      �� *IsServerReady+$ OneWay=���56      �� *RefreshCommand+$ OneWay=�z�57      V� 58      .~�59      �� �� 5:       �� AngleY$ 0.304Q�6(   �� 5;      T� 5<       T� Y$ -0.006Q�6-   5=      5?      5@      &   System.Windows.Controls.DataGrid  5A   
   -   5          Name$ dGrid��5B      $��2��5A         SelectionMode$ Single=�5C      $��2q�5D         CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     AutoGenerateColumns .  ��5G      �� *Files+$ OneWay=� �� SelectedItem 5H      �� *SelectedFile+$ TwoWay=�   Columns 5I      0  *System.Windows.Controls.DataGridTextColumn  5J      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ Название��6/      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6S   �� *.+$ OneWay=�5L      �� 5M   
   $��3��6   $��2q�5N      $ 3q�6   $��Right=�5O      ��5P      �� *Items.Count+ �� ElementName$ dGrid��$ OneWay=� �� StringFormat$ Файлов: {0}��5Q      5R      