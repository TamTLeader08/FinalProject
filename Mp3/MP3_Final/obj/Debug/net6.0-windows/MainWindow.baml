   M S B A M L   `   `   `  ????D  @MP3_Final, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MP3_Final	MP3_Final  5   	   ?$clr-namespace:MP3_Final.UserControlsMP3_Final.UserControls  5   	       MP3_Final.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MP3_Final     5   	   3uc$clr-namespace:MP3_Final.UserControls     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	     ? Icon$  /IconApp/Spotify.ico??5
   	    ? WindowStyle$
 None=?5   	   
??????  ? AllowsTransparency$
 True??5   	    ? Title$ Spotify Lite??5   	    ? WindowStartupLocation$ CenterScreen=?6    ? WindowState$ 	Maximized=?6D   $
??1080??6\   $	??720??6l   $	??720??6|   $
??1080??6?   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      CaptionHeight$ 0Q?6      ResizeBorderThickness$ 5q?6)   	 ? 	Resources	 5         	BoolToVis??%   &	        ?? 5   
   6'   5      .??5      ?? -   5        
 ??Name$
 
mainBorder??5       ?? CornerRadius$ 10??6>   $??1q?5       
 color1#???  5      ._?5   
   ? -   5         ?Name$ Music_Player??5       ? ColumnDefinitions 5      ?? 5      $	??225 ?6#   61   ?? 5      $??* ?6#   6/    ? RowDefinitions 5       ?? 5!      $??* ?6    6-   ?? 5"      $??90 ?6    6.   .+?5+      ?? -   5        $
 
menuBorder??5+      $??0??6)   $ 10 0 0 0??69   $
??auto??6Q   ??5,      C? 5-       C? ResourceKey$ 	greenBgr1??6&   ._?5/      ?  50      ?? 51      $
??1.5* ?6(   68   ?? 52      $??* ?6(   65   ?? 53      $??* ?6(   65   ?? 54      $??* ?6(   65   ?? 55      $??* ?6(   65   ?? 56      $??* ?6(   65   ?? 57      $??* ?6(   65   ?? 58      $??* ?6(   65   ?? 59      $??5 ?6(   65   ?? 5:      $
??3.5* ?6(   68   .+?5<      ??  ?? Orientation$ 
Horizontal=?6!   .+?5=      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Spotify=?62   $??10 0 0 0q?6A   $??50??6S   $??50??6^   $??Center=?6j    
 color2#??[ 6?   ?? 5>      $??Music Player??6$   $??10 0 0 0q?68   $??Center=?6J   $??25(?6e   #??[ 6s   5?      ?? 5@      -   5         ??Name$ Playlist??5@        
menuButton#??[ 6/   $??1??6S   .??5A      ?? $ 
Horizontal=?6%   .+?5B        $ HomeVariant=?66     menuButtonIcon#??[ 6I   ?? 5C      $
??Home??6(     menuButtonTitle#??[ 64   5D      5E      ?? 5F      -   5        $ Favorite??5F      #??[ 6/   $??2??6S   .??5G      ?? $ 
Horizontal=?6%   .+?5H        $&  CardsPlayingHeartMultipleOutline=?66   #??[ 6^   ?? 5I      $??Favorite??6(   #??[ 68   5J      5K      ?? 5M      -   5        #??[ 5M      $??3??6Y   .??5N      ?? $ 
Horizontal=?6%   .+?5O        $ 
FileUpload=?66   #??[ 6H   ?? 5P      $??	Load File??6(   #??[ 69   5Q      5R      ?? 5T      -   5        #??[ 5T      $??4??6[   .??5U      ?? $ 
Horizontal=?6%   .+?5V        $ FolderUpload=?66   #??[ 6J   ?? 5W      $??Load Folder??6(   #??[ 6;   5X      5Y      ?? 5[      #??[ 6   $??5??6A   5]      d? 5_      -   5         d?Name$ MoreOptionToggleBtn??5_   #   $??5??6@   .??5`      ?? $ 
Horizontal=?6%   .+?5a        $ ViewGridPlusOutline=?66   #??[ 6Q   ?? 5b      $??More Option??6(   #??[ 6;     5c      $
 Play=?66   $??Center=?6B   #??[ 6]   $??20 0 0 0q?6?   $??25??6?   $??25??6?   5d      ??5f      ?? 5g       ?? 
TargetType" d?6.   .R?5h   "   ?? #??z??6)   ._?5i   &   ?? $??Stretch=?67   $??Center=?6U   5j   #   5k      ??5m      ?? 5n       ?? 
TargetType" d?6$   .?5o   "   ??  ?? Property ? ?? ?? Value
 ???? 6?   ?? 5p   "    ? ??
 ?????6?   ?? 5q   "    ? ??$ 25(?6=   ?? 5r   "    ? ??$
 Bold"?6?   ?? 5s   "    ?@?FocusVisualStyle 6E   Z? ?? 5t   "    ?@?Tag$ 	Collapsed??68    ?? Triggers 5u   "   P? 5v   &    P? Property ?@J?IsMouseOver P? Value$
 True??6E   . ?5w   *   ??  ? ?? ?#1aab7a6G   ?? 5x   *    ? ??
 ?????6G   ?? 5y   *    ?@?Tag$ Visible??6@   5z   '   5|      5~      ? 5?      $	??120??6   $	??180??6)    ? AllowsTransparency$
 True??6t    ? 	StaysOpen$ False??6?   $??Right=?6?    ? VerticalOffset$ 10??6?    ? HorizontalOffset$ 0??6  ??66   ?? *	IsChecked+ ?? ElementName$ MoreOptionToggleBtn??  ? PlacementTarget  6?   ?? $ MoreOptionToggleBtn??.??5?      ?? -	   5        $
 BorderPopupMoreOption??5?   !   ??5?      C? 5?   "   $ 	greenBgr1??62   ._?5?      ?? .+?5?   "   ?? -
   5        $ darkmodeBtn??5?   )     PopupButton#??[ 6>   	 5?   &    	 Light  optionButtonIcon 	 Dark??%j  &	       7  7  &		 ?     7  7  ?? 5?   *   $ 
Horizontal=?6D   .+?5?   .     $??10 0 0 0q?6F   $ 
ShieldMoon=?6Y   8??  6k   ?? 5?   .   $??	Dark Mode??68   8?? 6I   5?   +   ?? 5?   *   $ 
Horizontal=?6C   .+?5?   .     $??10 0 0 0q?6F   $ ShieldMoonOutline=?6Y   8??  6r   ?? 5?   .   $??
Light Mode??68   8?? 6J   5?   +   5?   '   .??5?   &   C? $ Light??66   5?   #   ?? 5?   "   -   5        #??[ 5?   @   .??5?   &   ?? $ 
Horizontal=?61   .+?5?   *     $??10 0 0 0q?6B   $ DeleteEmpty=?6T   #??[ 6g   ?? 5?   *   $??Delete Playlist??64   #??[ 6K   5?   '   5?   #   ?? 5?   "   #??[ 6)   .??5?   &   ?? $ 
Horizontal=?61   .+?5?   *     $??10 0 0 0q?6B   $ InformationOutline=?6T   #??[ 6n   ?? 5?   *   $??About us??64   #??[ 6D   5?   '   5?   #   5?      5?      5?      ?? 5?      -   5        #??[ 5?   6   $??6??6[   .??5?      ?? $ 
Horizontal=?6%   .+?5?        $ PlaylistMusic=?66   #??[ 6K   ?? 5?      $??Your Playlists??6(   #??[ 6>   5?      5?      ?? 5?      -   5        #??[ 5?   1   $??7??6V   .??5?      ?? $ 
Horizontal=?6%   .+?5?        $ PlusBox=?66   #??[ 6E   ?? 5?      $??Create Playlist??6(   #??[ 6?   5?      5?      ?? 5?      $??8??6    
???????$??1??6<   $??2??6G   ?? 5?      $??9??6#   $	??Top=?60   $
??Auto=?6I   $??Disabled=?6l   .??5?      ?? -   5        $??listMenu??5?   %   5?      5?      5?      5?      ?? 5?      -   5        $
 CenterBorder??5?      $ 0 10 0 0??6,   $??1??6D   $
??auto??6T   ??5?      C? 5?      $ 	greenBgr1??6&   ._?5?      ?  5?      ?? 5?      $??70 ?6(   ?? 5?      $??* ?6(   .+?5?      ? $??0??6    5?      ?? 5?      $??* ?6/   ?? 5?      $	??120 ?6/   .+?5?      (  "MaterialDesignThemes.Wpf.ColorZone  -   5        !  Name$! 	searchBar??5?   3    
 foreGrSearchBar#??[
 5?      $??8,4,8,4q?5?      " K? ZIndex$" 1??5?      .  (MaterialDesignThemes.Wpf.ElevationAssist#   	Elevation$	# Dp2=?5?      $   CornerRadius$$ 18??5?      $??30 0 0 0q?5?      %   Mode$% Standard=?5?      $	??600??6-   $??40??69   $
??Left=?6E   $??Center=?6`   .??5?      ?  5?   "   ?? 5?   &   $
??Auto ?67   ?? 5?   &   $??* ?67   .+?5?   "   ??   MaterialDesignToolButton#??[ 5?   !   .??5?   &     $ Search=?5?   %   & J? Opacity$	& 0.9Q?5?   %   5?   #   ?? 5?   "   -   5        ' ??Name$' searchTB??5?   *   $??1??5?   !   $	??200??5?   !   $??14(?5?   !   $??8,0,0,0q?5?   !   $??Center=?5?   !   )  #MaterialDesignThemes.Wpf.HintAssist(   Hint$( Search your song ...??5?   !   .  (MaterialDesignThemes.Wpf.TextFieldAssist)   DecorationVisibility$) Hidden=?5   !   $??0q?5  !   5     5     ?? 5     $??	10 0 0 25q?6%   $ 
Horizontal=?68   $??1??6Q   .+?5     ?? -   5        $ minimizeBtn??5  &     controlButton#??[ 6<   .??5  "     $ WindowMinimize=?6:     controlButtonIcon#??[ 6P   5	     ?? 5
     -   5        $ maximizeBtn??5
  %   #??[ 6;   .??5  "     $ 
DockWindow=?6:   #??[ 6L   5     ?? 5     -   5        $ closeBtn??5  %   #??[ 68   .??5  "     $ WindowClose=?6:   #??[ 6M   5     5     5     ? 5     -   5        $ centerUI??5     $??1??6-    5     ?? 5     $??* ?6/   ?? 5     $??3* ?6/   .+?5     ? $??0??6    5     ?? 5   "   $	??300 ?60   ?? 5!  "   $??* ?60   .+?5$     A? $	??200??6&   $	??200??62   $??0 0 0 0q?6?   $??Center=?6P   $??Center=?6k   * J? RenderTransformOrigin$* 0.5,0.5&?6?   z?5%  "   ?? 5&  &   -   5        MyAnimatedTransform??+ ?? Angle$+ 0Q?5&  S   ??5(  "   ?? 5)  &   -   5        img??, ?? ImageSource$, /Images/m2.png??5)  >   5+     ?? 5.     -   5        - ??Name$- infotextblock??5.  }   $??10 0 0 0q?6(   $??Center=?6:   $	??Top=?6W   $??1??6o   $??THÔNG TIN BÀI HÁT??6?   $
??Wrap=?6?   
???????$??16(?6?   $
??Bold"?6?   5/     ?? 52     $??1??6'   $
??Auto=?67   $??Disabled=?53  E   .??54     ?? -   5        $??songMenu??54  )   $??0 0 10 0q?69   56     57     58     59     5:     ?? 5>     -   5        $
 PlayerBorder??5>     $??1??6+   $ 	0 0 10 10??68   $??2??6R   $
??auto??6f   ??5?     C? 5@     $ 	greenBgr2??6&   ._?5B     ?  5C     ?? 5D     $	??100 ?6+   ?? 5E     $	??270 ?6+   ?? 5F     $??50 ?6+   ?? 5G     $??* ?6+   ?? 5H     $??50 ?6+   ?? 5I     $	??160 ?6+   .+?5L     ?? . ?? RadiusX$. 5??6    / ?? RadiusY$/ 5??6,   $??75??68   $??75??6D   $??Center=?6O   $??5 0 0 0q?6j   ??5M     ?? 5N     -   5        staticImage??$, /Images/m2.png??5N  >   5P     ? 5Q     $??1??6!   0 ? Orientation$0 Vertical=?61   $??Center=?6H   .+?5S     ?? -   5        $- titleTxtBlock??5S  $   $??Có không giữ??6;   $
??Wrap=?6O   
???????$??16(?6v   $
??Bold"?6?   ?? 5T     -   5        $- singerTxtBlock??5T  $   $??Singer Singer Singer??6<   ???#9ae5c3$??14(?6m   $??SemiBold"?6{   5U     ?? 5W     -   5        $ heartbtn??5W  \   $??-5 0 0 0q?6   $??Center=?6/   $??2??6L     playerButton#??[ 6n   .??5X       $ HeartOutline=?62     playerButtonIcon#??[ 6F   5Y     ? 5[     $??3??6    5\     ?? 5]     $??2* ?6,   6:   ?? 5^     $
??1.5* ?6,   6<   .+?5a     ?? $??Center=?6%   $ 
Horizontal=?6B   .+?5b     ?? -   5        $ 
shufflebtn??5b  %   #??[ 69   .??5c  "     $ ShuffleVariant=?6:   #??[ 6P   5d     ?? 5f     -    5        $ previousbtn??5f  %   #??[ 6:   .??5g  "     $ SkipPrevious=?6:   #??[ 6N   5h     ?? 5j     -!   5        $ pausebtn??5j  %   #??[ 67   	 5k  "     Play 	 Pause  spinellipse1 ?? 
TargetName2 ?? TargetProperty3 W? From	4 W? To5 g? Duration6 g? RepeatBehavior  stopellipse7 g? DecelerationRatio??%?  &	       7  &	 (     7  &	 Q     &	       5l  &   $
 Play=?6K   8??  6W     5m  &   $ Pause=?6L   8??  6Y   ?? 5n  &   .??5o  *   W? $1 MyAnimatedTransform??5p  1   $2 (RotateTransform.Angle)?5q  1   3 0.05r  1   4 3606<   $5 0:0:7D?6E   $6 Forever??6V   5s  '   ?? 5t  &   .??5u  *   W? $1 MyAnimatedTransform??5v  1   $2 (RotateTransform.Angle)?5w  1   $	7 0.5Q?5x  1   4 3606I   5y  '   5z  #   .??5{  "   C? $
 Play??62   5|     ?? 5~     -"   5        $ nextbtn??5~  %   #??[ 66   .??5  "     $ SkipNext=?6:   #??[ 6J   5?     ?? 5?     -#   5        $ 	replaybtn??5?  %   #??[ 68   .??5?  "     $ Replay=?6:   #??[ 6H   5?     5?     ? 5?     $??1??6    5?     ?? 5?  "   $??80 ?63   ?? 5?  "   $??* ?63   ?? 5?  "   $??80 ?63   .+?5?     ?? -$   5        $- tbStart??5?  F   $??Center=?6(   $
??0:00??6W   ???#ffffff$??16(?6y   $??SemiBold"?6?   ?? 5?     -%   5        $??slider_seek??5?  7   $??1??6%   $
??auto??6J   ???#ffffff6l   ?? 5?     -&   5        $- tbEnd??5?  V   $??2??6(   $??Center=?68   $
??0:00??6f   ???#ffffff$??16(?6?   $??SemiBold"?6?   5?     5?     ?? 5?     -'   5        $??4??5?  2   $??-5 0 0 0q?6B   #??[ 6T   .??5?       $ StickerTextOutline=?62   #??[ 6L   5?     ?? 5?     $??5??6!   $ 
Horizontal=?61   .+?5?     ?? -(   5        $??-2 0 0 0q?5?  !   #??[ 63   .??5?       $ 
VolumeHigh=?66   #??[ 6H   5?     ?? 5?     -)   5        8 ??Name$8 	sldVolume??5?  !   $??Center=?64   ???#ffffff$	??110??6d   $??20??6p   9 ? LargeChange$	9 0.1Q?6?   : ? Maximum$: 1Q?6?   ; ? SmallChange$
; 0.01Q?6?   < ? Value$	< 0.5Q?6?   5?     5?     5?     5?     5?     5?     