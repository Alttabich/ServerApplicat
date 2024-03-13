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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerApplicat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Db connection;

        public MainWindow(Db con)
        {
            connection = con;
            InitializeComponent();
            databases.Items.Clear();

            MySqlCommand command = connection.GetConnection().CreateCommand();
            command.CommandText = "show databases;";
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            int counter = 0;
            while (reader.Read())
            {
                Button _temp = new Button();
                string row = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    //Console.WriteLine(counter);

                    row += reader.GetValue(i);
                    _temp.Content = row;
                    _temp.Background = counter % 2 == 0 ? Brushes.Blue : Brushes.Purple;
                    _temp.Click += DB_Click;
                    databases.Items.Add(_temp);
                    counter++;
                }
                Console.WriteLine(row);
            }
            reader.Close();
        }

        public void DB_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            tables.Items.Clear();
            Button selected_db = (Button)e.Source;
            //Console.WriteLine(selected_db.Content);
            MySqlCommand command = connection.GetConnection().CreateCommand();
            command.CommandText = $"USE {selected_db.Content}; SHOW TABLES";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Button table = new Button();
                table.Content = reader.GetValue(0);
                table.Background = Brushes.Ivory;
                table.Click += TABLE_Click;
                tables.Items.Add(table);
                counter++;
            }
            reader.Close();
            tables.Items.Insert(0, "Количество таблиц в базе данных: " + counter);
        }
        private void TABLE_Click(object sender, RoutedEventArgs e)
        {
            Button selected_table = (Button)e.Source;
            tableContent tableContent = new tableContent(connection, selected_table)
            {
                Owner = this
            };
            tableContent.Show();
        }
    }
}
