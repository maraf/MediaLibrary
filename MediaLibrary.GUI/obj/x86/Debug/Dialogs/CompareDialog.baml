   M S B A M L   `   `   `  ����P  LDesktopCore.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH8clr-namespace:DesktopCore;assembly=DesktopCore.ResourcesDesktopCore  5   	   O KMediaLibrary.Core, Version=1.0.3.1105, Culture=neutral, PublicKeyToken=nullP:clr-namespace:MediaLibrary.Core;assembly=MediaLibrary.CoreMediaLibrary.Core 5   	    MediaLibrary.GUI$   MediaLibrary.GUI.CompareDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Fc8clr-namespace:DesktopCore;assembly=DesktopCore.Resources     5   	   Kcore:clr-namespace:MediaLibrary.Core;assembly=MediaLibrary.Core     5   	     � Icon$9  3/MediaLibrary.GUI;component/Images/MediaLibrary.png��5   /    � SizeToContent$ WidthAndHeight=�6j    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�65    � Title 5   	      DesktopCore.Resource  *CompareLibraries+ � 	Resources 5         compareTypesProvider S� 
MethodName S� 
ObjectType#  MediaLibrary.Core.CompareType	 S� MethodParameters
 M� TypeName��%w   &	        S� 5	   
   $ 	GetValues��6:   "  6Q   	 5
      M� 5      $
 core:CompareType��6   5      5      .��5      ��  
 border#��[ 6   ._�5   
   � $��5q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#    � RowDefinitions 5      �� 5      �� 5      �� 5      �� 5      $
��Auto �6    .+�5      �� $��5q�67   $��0��6B   $��0��6R   ��6     *LibraryName+6R   �� 5      $��5q�67   $��0��6B   $��1��6R   ��6     *LibraryName+6R   �� 5      -   5         ��Name$ lblError��5      $��5q�6P   
���  ��$y�Hidden=�6l   $��0��6�   $��2��6�   ��6&     *SelectLibraryError+6�   �� 5       -   5         ��Name$ 	coxSource��5        �� DisplayMemberPath$
 Name��6L   $��5q�6�   $	��200��6�   $��1��6�   $��0��6�   ��6*   �� *	Databases+ �� SelectedItem 6e   �� *Source+6�   �� 5!      $
 Name��69   $��5q�6r   $��1��6}   $��1��6�   ��6   �� *	Databases+ 6R   �� *Target+6�   �� 5"      $��5q�6�   $��0��6�   $��2��6�   $��2��6�   ��6   ��  �� Source# [   6\   �� *CompareType+6�   �� 5$      $��5q�6    �� Orientation$ 
Horizontal=�6$   $��Right=�6=   $��1��6Y   $��3��6i   .+�5%      �� -   5         ��Name$ 
btnCompare��5%      $	��5,3q�6e   ��6-     *	Compare+6e   5&      5'      5(      5)      