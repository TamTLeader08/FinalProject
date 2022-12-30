using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace MP3_Final.UserControls
{
    /// <summary>
    /// Interaction logic for EditPlaylistButton.xaml
    /// </summary>
    public partial class EditPlaylistButton : UserControl
    {
        public EditPlaylistButton()
        {
            InitializeComponent();
        }

        public string Function
        {
            get { return (string)GetValue(FunctionProperty); }
            set { SetValue(FunctionProperty, value); }
        }

        public static readonly DependencyProperty FunctionProperty = DependencyProperty.Register("Function", typeof(string), typeof(EditPlaylistButton));


        public MaterialDesignThemes.Wpf.PackIconKind Icon
        {
            get { return (MaterialDesignThemes.Wpf.PackIconKind)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(MaterialDesignThemes.Wpf.PackIconKind), typeof(EditPlaylistButton));

        public static readonly RoutedEvent PopupClickEvent = EventManager.RegisterRoutedEvent(
        "PopupClick", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(EditPlaylistButton));

        public event RoutedEventHandler PopupClick
        {
            add { AddHandler(PopupClickEvent, value); }
            remove { AddHandler(PopupClickEvent, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PopupClickEvent));
        }
    }
}
