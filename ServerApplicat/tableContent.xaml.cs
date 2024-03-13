using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace ServerApplicat
{
    /// <summary>
    /// Логика взаимодействия для tableContent.xaml
    /// </summary>
    public partial class tableContent : Window
    {
        Button selectedButton;
        public tableContent(Db connection, Button selected)
        {
            InitializeComponent();
            selectedButton = selected;
            MySqlCommand command = connection.GetConnection().CreateCommand();
            command.CommandText = $"SELECT * from {selectedButton.Content}";
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            contentOfTable.ItemsSource = new DataView(ds.Tables[0]);
        }
    }
}
