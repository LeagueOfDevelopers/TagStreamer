using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication11
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
                Window.Show();
            }
            else { MessageBox.Show("Не удалось войти в систему"); }

        }
    }
}
