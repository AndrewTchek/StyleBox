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
    public partial class MainWindow : Window
    {
        private bool autorization = true;
        private Cloth selected_row;
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
            if (autorization){
                DataGridRow row = (DataGridRow)sender;
                Cloth selectedCloth = (Cloth)row.Item;

                EditForm editForm = new EditForm(selectedCloth, this);
                editForm.Show();
                Hide();
               }
            else
            {
                MessageBox.Show("Недостатньо прав");
            }
        }

        private void Info_Click(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            Cloth selected_row = (Cloth)row.Item;
 
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!autorization)
            {
                MessageBox.Show("Недостатньо прав");
                return;
            }

            if (ClothListDG.SelectedItem is Cloth selectedCloth)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Ви впевнені, що хочете видалити: {selectedCloth.cloth_name}?",
                    "Підтвердження видалення",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool deleted = DB_Communication.Delete_Cloth(selectedCloth.cloth_article);
                    if (deleted)
                    {
                        MessageBox.Show("Успішно видалено!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                        Db_download(null, null); // Оновлення списку
                    }
                    else
                    {
                        MessageBox.Show("Помилка при видаленні", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Оберіть елемент для видалення", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
    
}
