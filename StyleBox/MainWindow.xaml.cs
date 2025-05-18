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
            DB_Communication.DB_Connect();

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

       

        public void Db_download(object sender, RoutedEventArgs e)
        {
            
            List<Cloth> clothList = DB_Communication.DB_Get_Data();
            ClothListDG.ItemsSource = clothList;

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            Cloth selectedCloth = (Cloth)row.Item;

            EditForm editForm = new EditForm(selectedCloth, this);
            editForm.Show();
            Hide();
        }
    }
}
