   M S B A M L   `   `   `  ����F  BDesktopCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH3clr-namespace:DesktopCore.Data;assembly=DesktopCoreDesktopCore.Data  5       MediaLibrary.GUI   MediaLibrary.GUI.App   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Elocal3clr-namespace:DesktopCore.Data;assembly=DesktopCore     5        �� 	Resources  5         boolToVisibility8   2DesktopCore.Data.InversedBoolToVisibilityConverter  inversedBoolToVisibility4   .DesktopCore.Data.SelectedItemToStringConverter  selectedItemToString0   *DesktopCore.Data.NullToVisibilityConverter  nullToVisibility*   $DesktopCore.Data.NullToBoolConverter  
nullToBool-   'DesktopCore.Data.StringToArrayConverter  stringToArray)   #DesktopCore.Data.GrayScaleConverter  	grayScale/   )DesktopCore.Data.BoolToGrayScaleConverter  boolToGrayScale.   (DesktopCore.Data.DateTimeFormatConverter  dateTimeFormat 
	 border �� 
TargetType �� Property �� Value 
 link �� Padding �� Triggers P� Property P� Value 	 label  plainButton  imageButton �� BasedOn  toggleStarButton	 �� RelativeSource
 �� Path �� 	Converter��%x	  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &	 �     &		 �     '9� n    '�� G    &	
 &    &	 �    &	 T    &	 �    7  &	 �    7  7  7   �� 5   
   6'     5   
   62     5	   
   6.     5
   
   6*     5   
   6$     5   
   6'     5   
   6#     5   
   6)     5   
   6(   �� 5   
   " ��6   .�5      ��  � ��
 �����6,   �� 5       � ��$ 1q�60   �� 5       � ��
 �����6+   �� 5       �@��CornerRadius$ 3��6-   5      �� 5   
   " 9�6   .�5      ��  � ��
 �����6+   �� 5       � ��$ 5q�6'   �� 5       � ��$ 5q�6(   �� 5       � ��$ 1q�60   �� 5       � ��
 �����6,   5      �� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5        � ��$ 5,5,5,5q�6'   �� 5!       � ��$ 2q�6(   �� 5"       � ��$ 1q�60   �� 5#       � ��
 �����6,   5$      �� 5%   
   " ��6   .�5&      ��  � ��
 ��  �6+   �� 5'       �@�Cursor$
 Hand��6'   �� 5(       � �� 5)      �� 5*      .R�5+      �� $��Center=�6!   $��Center=�6<   $��0,0,0,1q�6Y   #��z��6s   # z��6�   #��z��6�   ._�5,      �� #��z��6(   #��z��6P   5-      5.      50       51      P� 52       �@J�IsMouseOver . . �53      ��  � ��
 �  ��63   54      56      �� 57   
   " ��6   .�58      ��  � ��$ 11(�6)   �� 59       � ��$
 Bold"�6+   5:      �� 5;   
   .�5<      ��  � �� 5=      �� 5>      .R�5?      �� #��z��6+   5@      5B      �� 5C       �@�Cursor$
 Hand��6.   5D      �� 5E   
   8   .�5F      ��  � ��$ 0,4,5,4q�6.   �� 5G       �@��BitmapScalingMode$ NearestNeighbor=�6@    5H      P� 5I       � {� .  . �5J      ��  �@J�Opacity$ .5Q�67   5K      5M      �� 5N   
   " d�6)   8   .�5O      ��  � ��$ 0q�6'   �� 5P       � �� 5Q      �� 5R      .R�5S      �� .+�5T      �� $��16��6$   $5��//MediaLibrary.GUI;component/Images/StarGray.png��6/   y�6h   �� 	 �� *TemplatedParent+$
 	IsChecked�8  �� 5U      $��16��6$   $1��+/MediaLibrary.GUI;component/Images/Star.png��6/   y�6d   �� 	 �� *TemplatedParent+$
 	IsChecked�8  5V      5W      5Y      5Z      5\      5]      