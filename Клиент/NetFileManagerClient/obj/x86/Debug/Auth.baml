   M S B A M L   `   `   `  ����O  KNetFileManagerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:NetFileManagerClientNetFileManagerClient  5   	       NetFileManagerClient.Auth   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:NetFileManagerClient     5   	   -   5          �Name$  
AuthWindow��5   	    � Title$
 Auth��5   	   $��184.043��6   $��282.894��6'   .��5	      �  � ColumnDefinitions 5
   
   �� 5      .+�5   
    � -   5          �Name$ frame��5        � NavigationUIVisibility$ Hidden=�6   $��0,0,10,0q�6?    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5   
   -   5        $
��Auth��5      $
��Left=�6    $��107,123,0,0q�6;   $	��Top=�6P   $��75��6h   $��20��6s   6   �� 5   
   -   5         ��Name$ TextBoxLogin��5      $
��Left=�6(   $��23��6C   $��	85,51,0,0q�6O    �� TextWrapping$
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��120��6�   �� 5   
   -   5        $ TextBoxPassword��5      $
��Left=�6+   $��23��6F   $��	85,79,0,0q�6R   $	��Top=�6e   $	��120��6}   �� 5   
   $��Login��6   $
��Left=�6    $��	41,51,0,0q�6;   $	��Top=�6N   $��26��6f   $��39��6r   �� 5   
   $��Password��6   $
��Left=�6#   $��	20,79,0,0q�6>   $	��Top=�6Q   $��26��6i   $��60��6u   5      5      