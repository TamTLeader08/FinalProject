   M S B A M L   `   `   `  ????D  @MP3_Final, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MP3_Final.UserControlsMP3_Final.UserControls  5      %    MP3_Final.UserControls.SongName   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      0local$clr-namespace:MP3_Final.UserControls  5      -   5        $??songName??5
      .??5      ?? -   5          ??Name$  
BdSongName??5   	   $??0 0 0 3q?6    ?? CornerRadius$ 5??6.    ?? Padding$ 7q?6?   ??5   
   ?? 5       ?? 
TargetType" ??6    ?? Triggers 5      P? 5       P? Property ?@J?IsMouseOver P? Value$
 True??65   . ?5      ??  ?? Property ? ?? ?? Value ?#1aab7a67   5      ?? 5      	 ?? Binding	 6"   ?? 
 ?? ElementName$
 songName?? ?? Path$ IsActive? ?? Value True6Z   .a?5      ??  ? ?? ?#1aab7a67   5      5      ._?5   
   ?  ? ColumnDefinitions 5      ?? 5      $??40 ?6#   ?? 5       $??3* ?6#   ?? 5!      $??2* ?6#   ?? 5"      $??40 ?6#   ?? 5#      $??40 ?6#   .+?5&      ??  
  color2#??[  6K   $??14(?6p   $??SemiBold"?6~   $??Center=?6?   ??6   ?? $
 songName??$ Number?6?   ?? 5'      $??1??6J   #??[  6Z   $??14(?6   $??SemiBold"?6?   $??Center=?6?   ??6   ?? $
 songName??$ Title?6?   ?? 5(      $??2??6K   #??[  6[   $??14(?6?   $??SemiBold"?6?   $??Center=?6?   ??6   ?? $
 songName??$ Singer?6?   ?? 5)      $??3??6I   #??[  6Y   $??14(?6~   $??SemiBold"?6?   $??Center=?6?   ??6   ?? $
 songName??$
 Time?6?   d? 5+      -   5        $??	editTgBtn??5+      $??4??6,   $??Center=?6=   .??5,      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ DotsVertical=?6*   $??25??6>   $??25??6I   ???#f1f1f16U   ??5-      ?? 5.       ?? 
TargetType" d?6&   .R?5/      ?? #??z??6!   $ 50??6K   ._?50      ?? $??Center=?6/   $??Center=?6L   51      52      ??54      ?? 55      " d?6   .?56      ??  ? ??
 ???? 67   ?? 57       ? ?? ?#ffffff67   ?? 58       ? ??$ 40??62   ?? 59       ? ??$ 40??63    5:      P? 5;       ?@J?IsMouseOver$
 True??6=   . ?5<   "   ??  ? ?? ?#1fae696?   ?? 5=   "    ? ?? ?#f0f0f06?   5>      5@      5B      ? 5D      -   5         ?Name$ playlistPopup??5D       ? AllowsTransparency$
 True??6_    ? 	StaysOpen$ False??6y   $
??Left=?6?    ? VerticalOffset$ 25??6?    ? HorizontalOffset$ 10??6?   ??6+   ?? *	IsChecked+$
 	editTgBtn?? ? PlacementTarget 6?   ?? $
 	editTgBtn??.??5E      ?? 
???????$ 5??6,   $	 0 4q?6=   ._?5F      ?? .+?5G      /   )MP3_Final.UserControls.EditPlaylistButton  -   5          Name$ AddAlbumBtn??5G   3   /   )MP3_Final.UserControls.EditPlaylistButton   
PopupClick$ AddAlbumBtn_PopupClick??6q      Icon$ PlaylistPlus=?6H      Function$ 	Add album??6\   6q     5H      -   5        $ 
DelSongBtn??5H   3   $ DelSongBtn_PopupClick??6g   $ Delete=?6G   $ Delete??6U   6g   5I      5J      5K      5M      5N      5P      