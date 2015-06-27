using System.Windows;

namespace WpfApplication11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            var Window = new Authorization();
            Close();
            Window.Show();
        }

        private void ButtonUser_Click(object sender, RoutedEventArgs e)
        {
            var Window = new UserInterface();
			Close();
            Window.Show();
        }
    }
}
