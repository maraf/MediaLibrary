   M S B A M L   `   `   `  ���� F  BDesktopCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH3clr-namespace:DesktopCore.Data;assembly=DesktopCoreDesktopCore.Data   MediaLibrary.GUI   MediaLibrary.GUI.App   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Elocal3clr-namespace:DesktopCore.Data;assembly=DesktopCore       �� 	Resources     boolToVisibility8   2DesktopCore.Data.InversedBoolToVisibilityConverter  inversedBoolToVisibility4   .DesktopCore.Data.SelectedItemToStringConverter  selectedItemToString0   *DesktopCore.Data.NullToVisibilityConverter  nullToVisibility*   $DesktopCore.Data.NullToBoolConverter  
nullToBool-   'DesktopCore.Data.StringToArrayConverter  stringToArray)   #DesktopCore.Data.GrayScaleConverter  	grayScale/   )DesktopCore.Data.BoolToGrayScaleConverter  boolToGrayScale.   (DesktopCore.Data.DateTimeFormatConverter  dateTimeFormat 
	 border �� 
TargetType �� Property �� Value 
 link �� Padding �� Triggers P� Property P� Value 	 label  plainButton  imageButton �� BasedOn  toggleStarButton	 �� RelativeSource
 �� Path �� 	Converter��%�  &	        &	      &	 
     &	      &	      &	      &	      &	 #     &	 (     &		 -     '9� �     '��     &	
 �    &	 �    &	 �    &	     7  &	 �    7  7  7   ��                 �� " ��.���  � ��
 �������  � ��$ 1q���  � ��
 �������  �@��CornerRadius$ 3���� " 9�.���  � ��
 �������  � ��$ 5q���  � ��$ 5q���  � ��$ 1q���  � ��
 ������� " ��.���  � ��
 �������  � ��$ 5,5,5,5q���  � ��$ 2q���  � ��$ 1q���  � ��
 ������� " ��.���  � ��
 ��  ���  �@�Cursor$
 Hand����  � �� �� .R��� $��Center=�$��Center=�$��0,0,0,1q�#��z��# z��#��z��._��� #��z��#��z�� P�  �@J�IsMouseOver . . ���  � ��
 �  ���� " ��.���  � ��$ 11(���  � ��$
 Bold"��� .���  � �� �� .R��� #��z����  �@�Cursor$
 Hand���� 8   .���  � ��$ 0,4,5,4q���  �@��BitmapScalingMode$ NearestNeighbor=� P�  � {� .  . ���  �@J�Opacity$ .5Q��� " d�8   .���  � ��$ 0q���  � �� �� .R��� .+��� $��16��$5��//MediaLibrary.GUI;component/Images/StarGray.png��y��� 	 �� *TemplatedParent+$
 	IsChecked�8  �� $��16��$1��+/MediaLibrary.GUI;component/Images/Star.png��y��� 	 �� *TemplatedParent+$
 	IsChecked�8  