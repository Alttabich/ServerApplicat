using System;
using MySql.Data.MySqlClient;
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

namespace ServerApplicat
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void auth_Click(object sender, RoutedEventArgs e)
        {
            bool access = true;

            string ip, port, un, up;
            ip = _ip.Text;
            port = _port.Text;
            un = _login.Text;
            up = _password.Password;
            Db connection = new Db($"server={ip};port={port};username={un};password={up}");
            try
            {
                connection.OpenConnection();
            }
            catch (Exception)
            {
                Console.WriteLine("Неверные данные");
                access = false;
            }
            if (access == true)
            {
                MainWindow mainWindow = new MainWindow(connection);
                mainWindow.Show();
                Close();
            }
        }
    }
}