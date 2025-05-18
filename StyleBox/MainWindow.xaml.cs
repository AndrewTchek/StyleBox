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
using System.Windows.Navigation;
using System.Windows.Shapes;


using MySql.Data.MySqlClient;

namespace StyleBox
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ClothListDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            AutorizationForm autorizationForm = new AutorizationForm();
            autorizationForm.Show();
            Hide();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
            Hide();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditForm editForm = new EditForm();
            editForm.Show();
            Hide();
        }

        private void Db_download(object sender, RoutedEventArgs e)
        {
            try
            {
                string connstring = "server=localhost;uid=root;pwd=135135135;database=stylebox_db";
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connstring;
                con.Open();
                string sql = "select * from stocks";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<Cloth> clothList = new List<Cloth>();

                int i = 0;
                while (reader.Read())
                {

                    clothList.Add(new Cloth
                    {
                        cloth_article = reader["cloth_article"].ToString(),
                        cloth_name = reader["cloth_name"].ToString(),
                        cloth_type = reader["cloth_type"].ToString(),
                        cloth_price = Convert.ToDouble(reader["cloth_price"]),
                        cloth_number = Convert.ToInt32(reader["cloth_number"])
                    });
                }
                
                ClothListDG.ItemsSource = clothList;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Помилка: Підключення не відбулось");
                MessageBox.Show(ex.ToString());

            }

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            Cloth selectedCloth = (Cloth)row.Item;

            EditForm editForm = new EditForm(selectedCloth);
            editForm.Show();
            Hide();
        }
    }
}
