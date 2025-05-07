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
using static StyleBox.GlobalClass;


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

                //List<GlobalClass.Cloth> clothlist = new List<GlobalClass.Cloth>();
                GlobalClass.Cloth[] clothList = new GlobalClass.Cloth[10];

                int i = 0;
                while (reader.Read())
                {
                    //string name = reader["cloth_name"];
                    //MessageBox.Show("typeof name" + reader["cloth_name"] + )

                    //ClothDataGrid.Items.Add(reader["cloth_article"] + " " + reader["cloth_name"]);
                    //clothlist.Add(new Cloth(reader["cloth_articel"], reader["cloth_name", "M", Convert.ToDouble(reader["cloth_price"]), (int)reader["cloth_number"]);
                    clothList[i].cloth_article = (string)reader["cloth_article"];
                    clothList[i].cloth_name = (string)reader["cloth_name"];
                    clothList[i].cloth_type = (string)reader["cloth_type"];
                    clothList[i].cloth_price = Convert.ToDouble(reader["cloth_price"]);
                    clothList[i].cloth_number = Convert.ToInt32(reader["cloth_number"]);
                    i++;

                    
                }

                ClothListDG.ItemsSource = clothList;
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
