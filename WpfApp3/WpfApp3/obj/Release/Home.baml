   M S B A M L   `   `   `  ���� E  AstemSchool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp3WpfApp3      WpfApp3.Home   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  !localclr-namespace:WpfApp3  -     N� Title$
  Home��.��� -   $��homeGrid������  �� ImageSource$ /background.jpg�� � RowDefinitions �� $��* � � ColumnDefinitions �� $��* �.+�� .+��� -   $��SelectPackage��$��90��$
��Left=�$	��Top=�$��
17,10,0,13q�.@��� $��Chọn khối�� �� 
IsSelected . �� -   $��SelectSubject��$	��120��$
��Left=�$	��Top=�$��117,10,0,13q�.@��� $��Chọn môn�� . �� -   $��	txtSearch��$
��Left=� �� TextAlignment$ Justify=� �� TextWrapping$
 Wrap=�$	��Top=�$	��230��$��24��$��247,10,0,13q��� -   $��	btnSearch��$��Tìm kiếm��$
��Left=�$��490,10,0,13q�$	��Top=�$��24��$	��100�����	#FF4679E7
�������$
��Bold"��� -   $��
GifLoading��"  WpfAnimatedGif.ImageBehavior   AnimatedSource$ Spin.gif��$
��Left=�$��25��$��600,10,0,13q�$	��Top=�$��25��$y�	Collapsed=��� $��Right=�$��25��$��0,10,310,00q�$	��Top=�$��25��$��/3237472.png���� -   $��NameUser��$	��Top=�$��25�� �� TextAlignment$ Justify=�$��19(�$��Right=�$��0,10,230,00q�
��������� -	   $��
btnHistory��$��Lịch sử tải��$��Right=�$��
0,10,20,00q�$	��Top=�$��24��$	��100�����	#FF4679E7
�������$
��Bold"��� $��0��$��0��$��	0,43,0,13q���. .���� -
   $��
listFilePP������ .R�� 	 � Orientation$	 
Horizontal=����� .R�   WpfApp3.Item  $��10q�
   MyData
 �� 