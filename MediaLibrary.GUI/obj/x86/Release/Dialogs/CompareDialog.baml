   M S B A M L   `   `   `  ���� P  LDesktopCore.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH8clr-namespace:DesktopCore;assembly=DesktopCore.ResourcesDesktopCore  O KMediaLibrary.Core, Version=1.0.3.1105, Culture=neutral, PublicKeyToken=nullP:clr-namespace:MediaLibrary.Core;assembly=MediaLibrary.CoreMediaLibrary.Core  MediaLibrary.GUI$   MediaLibrary.GUI.CompareDialog   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Fc8clr-namespace:DesktopCore;assembly=DesktopCore.Resources     Kcore:clr-namespace:MediaLibrary.Core;assembly=MediaLibrary.Core       � Icon$9  3/MediaLibrary.GUI;component/Images/MediaLibrary.png�� � SizeToContent$ WidthAndHeight=� � ShowInTaskbar .   � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Title    DesktopCore.Resource  *CompareLibraries+ � 	Resources    compareTypesProvider S� 
MethodName S� 
ObjectType#  MediaLibrary.Core.CompareType	 S� MethodParameters
 M� TypeName��%D   &	        S� $ 	GetValues��"  	 M� $
 core:CompareType��.����  
 border#��[ ._�� $��5q� � ColumnDefinitions �� $
��Auto ��� $
��Auto � � RowDefinitions �� �� �� �� $
��Auto �.+��� $��5q�$��0��$��0����  *LibraryName+�� $��5q�$��0��$��1����  *LibraryName+�� -    ��Name$ lblError��$��5q�
���  ��$y�Hidden=�$��0��$��2����  *SelectLibraryError+�� -    ��Name$ 	coxSource�� �� DisplayMemberPath$
 Name��$��5q�$	��200��$��1��$��0������ *	Databases+ �� SelectedItem �� *Source+�� $
 Name��$��5q�$��1��$��1������ *	Databases+ �� *Target+�� $��5q�$��0��$��2��$��2������  �� Source# [   �� *CompareType+�� $��5q� �� Orientation$ 
Horizontal=�$��Right=�$��1��$��3��.+��� -    ��Name$ 
btnCompare��$	��5,3q���  *	Compare+