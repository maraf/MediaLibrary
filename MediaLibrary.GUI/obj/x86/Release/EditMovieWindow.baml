   M S B A M L   `   `   `  ���� P  LDesktopCore.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH8clr-namespace:DesktopCore;assembly=DesktopCore.ResourcesDesktopCore   MediaLibrary.GUI&    MediaLibrary.GUI.EditMovieWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Fc8clr-namespace:DesktopCore;assembly=DesktopCore.Resources     -     � Icon$9  3/MediaLibrary.GUI;component/Images/MediaLibrary.png�� � SizeToContent$ WidthAndHeight=� � ShowInTaskbar .   � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Title �� *Name+ �� StringFormat    DesktopCore.Resource  *DetailTitle+.���  � RowDefinitions �� �� $
��Auto �.+���  �� Orientation$ 
Horizontal=�$��0��.+���  
  border#��[  ._��  �� �� �� �� �� �� �� 	 � ColumnDefinitions	 �� $	��120 ��� .+�]� $��10q�$��0��$��0��.+�d� -   
 d�Name$
 btnToggleStarred��  toggleStarButton#��[ $��0,5,0,0q�$
��Left=� � ToolTip   *ToggleStarred+ d� 	IsChecked �� *	Starred+�� $��0��$��0����  *Name+�� $��10q�$��0��$��1����  *OriginalName+�� $��10q�$��0��$��2����  *	Storage+�� $��10q�$��0��$��3����  *Actors+�� $��10q�$��0��$��4����  *Year+�� $��10q�$��0��$��5����  *	Country+�� $��10q�$��0��$��6����  *
Category+]� $��10q�$��1��$��0��.+��� -    ��Name$ btnBrowseWeb��  imageButton#��[ $��5,0,0,0q�$��Right=�   *GoWeb+{��� *OnlineIdentifier+  
nullToBool �� 	Converter# [ .���� $��16��$4��./MediaLibrary.GUI;component/Images/WorldGo.png�� �� BitmapScalingMode$ NearestNeighbor=��� -    ��Name$ tbxName������ *Name+ �� UpdateSourceTrigger$ PropertyChanged=��� $��10q�$��1��$��1������ *OriginalName+$ PropertyChanged=�� $��10q�$��1��$��2��	 �� $	��120 ��� $	��100 ��� $	��120 �.+��� $��0��$��0������ *	Storage+$ PropertyChanged=��� $��Right=�$��0,0,5,0q�$��1����  *OnlineIdentifier+�� $��2������ *OnlineIdentifier+$ PropertyChanged=��� $��10q�$��1��$��3������ *Actors+  stringToArray# [ $ PropertyChanged=�� $��10q�$��1��$��4��	 �� $	��120 ��� $	��100 ��� $	��120 �.+��� $��0������ *Year+$ PropertyChanged=��� $��Right=�$��0,0,5,0q�$��1����  *
Language+�� $��2������ *
Language+$ PropertyChanged=�� $��10q�$��1��$��5��	 �� $	��120 ��� $	��100 ��� $	��120 �.+��� -    ��Name$ 
cbxCountry�� �� 
IsEditable . $��0�� �� Text �� *	Country+$ PropertyChanged=��� $��Right=�$��0,0,5,0q�$��1����  *Added+��  �� 
IsReadOnly . ��.  $��2������ *Added+  dateTimeFormat# [ �� -   $ cbxCategory��$��10q� . $��1��$��6�� �� *
Category+$ PropertyChanged=��� #��[  $	��300��$��10,0,0,0q�._�� $
��5,10q� �� $��20 ��� �� $��25 �.+��� $	��5,0q�$��0����  *RelatedMovies+�� -    ��Name$ 
lvwRelated��$��1������ *	Related+���� .R��� $ 
Horizontal=�.+��� ���� *Name+�� ���� *Year+ �� StringFormat$ ({0})��y��� *Year+  nullToVisibility# [ � $��2��$	��5,2q�.+��� $ 
Horizontal=�.+��� -   $ btnRelatedAdd����  *
AddMovie+�� $ 
Horizontal=�$��Right=�.+��� -	   $ btnRelatedRemove����  *RemoveMovie+9� $	��0,5q�$��1����  *Description+.���� -
   $ tbxDescription��$	��250�� �� AcceptsReturn .  �� TextWrapping$
 Wrap=�$	��780��$��Stretch=����� *Description+$ PropertyChanged=�