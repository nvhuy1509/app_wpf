   M S B A M L   `   `   `  ����E  AstemSchool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp3WpfApp3  5          WpfApp3.Home   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5      !localclr-namespace:WpfApp3  5      -   5          N� Title$
  Home��5
      .��5      � -   5        $��homeGrid��5      ��5   
   �� 5       �� ImageSource$ /background.jpg��6    � RowDefinitions 5   
   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   .+�5   
   � .+�5      �� -   5        $��SelectPackage��5   =   $��90��6   $
��Left=�6"   $	��Top=�6R   $��
17,10,0,13q�6j   .@�5      �� $��Chọn khối��6    �� 
IsSelected . 6G   5      �� 5      -   5        $��SelectSubject��5   >   $	��120��6   $
��Left=�6#   $	��Top=�6S   $��117,10,0,13q�6k   .@�5      �� $��Chọn môn��6    . 6F   5      �� 5      -   5        $��	txtSearch��5   2   $
��Left=�6    �� TextAlignment$ Justify=�6C    �� TextWrapping$
 Wrap=�6[   $	��Top=�6p   $	��230��6�   $��24��6�   $��247,10,0,13q�6�   �� 5       -   5        $��	btnSearch��5    (   $��Tìm kiếm��6   $
��Left=�69   $��490,10,0,13q�6T   $	��Top=�6i   $��24��6�   $	��100��6�   ���	#FF4679E7
�������$
��Bold"�6�   6�   �� 5!      -   5        $��
GifLoading��5!   �   "  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Spin.gif��6   $
��Left=�6A   $��25��6\   $��600,10,0,13q�6h   $	��Top=�6}   $��25��6�   $y�	Collapsed=�6�   �� 5"      $��Right=�6   $��25��60   $��0,10,310,00q�6<   $	��Top=�6Q   $��25��6i   $��/3237472.png��6t   �� 5#      -   5        $��NameUser��5#   c   $	��Top=�6   $��25��61    �� TextAlignment$ Justify=�6=   $��19(�6U   $��Right=�6s   $��0,10,230,00q�6�   
�������6�   �� 5$      -	   5        $��
btnHistory��5$   +   $��Lịch sử tải��6   $��Right=�6=   $��
0,10,20,00q�6Y   $	��Top=�6m   $��24��6�   $	��100��6�   ���	#FF4679E7
�������$
��Bold"�6�   6�   5%      �� 5&   
   $��0��6   $��0��6$   $��	0,43,0,13q�65   ��. .��5'      �� -
   5        $��
listFilePP��5'      ��5(      �� 5)      .R�5*      � 	 � Orientation$	 
Horizontal=�6$   5+      ��5-      �� 5.      .R�5/         WpfApp3.Item  $��10q�68   
   MyData
 6%   �� 68   50      52      53      56      57      