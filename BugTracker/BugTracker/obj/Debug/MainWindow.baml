   M S B A M L   `   `   `  ����E  ABugTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BugTracker
BugTracker  5   	       BugTracker.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:BugTracker     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $��50 �6   6*   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $	��25* �6   6-   �� 5      $	��25* �6   6-   �� 5      $	��25* �6   6-   �� 5      $	��25* �6   6-   .+�5   
   �� $��3��6    �� Orientation$ 
Horizontal=�6)   $y�Visible=�6B   .+�5      �� -   5        $
��HOME��5      6=   �� 5      -   5        $��	USER PAGE��5      6A   �� 5      -   5        $��Page 3��5      6;   �� 5      -   5        $��	Page Test��5      6@   �� 5      -   5        $��Page 5��5      6@   �� 5      -   5        $��PROFILE��5      6@   5      ]� 5   
   $��4��6   $��1��6$   $y�Visible=�68   .+�5       �� -   5        $��WelcomeBanner��5       $��Welcome user��6)   $��Center=�6@   6]   �� 5!      -	   5        $��LOG OUT��5!      6A   5"       � 5#   
   -
   5          �Name$ 
MainCanvas��5#        � NavigationUIVisibility$ Hidden=�6$   $��1��6D   $��2��6Q   $��4��6b   6x   &   System.Windows.Controls.DataGrid  5$   
   -   5        $��myDataGrid2��5$      $��3��6&   $��3��63   $y�	Collapsed=�6G   6`   5%      5&      