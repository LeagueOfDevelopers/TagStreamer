using System.Windows;

namespace TagStreamDesktop
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            var Window = new MainWindow();
            this.Close();
            Window.Show();
        }

        private void bEnter_Click(object sender, RoutedEventArgs e)
        {
            string key = Connection.Authorization(login.Text, password.Password);
            if (key != null)
            {
                AdminInterface Window = new AdminInterface(key);
				Close();
                Window.Show();
            }
            else { MessageBox.Show("Не удалось войти в систему"); }

        }
    }
}
