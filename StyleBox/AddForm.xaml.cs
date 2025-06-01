using Mysqlx.Crud;
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

namespace StyleBox
{
    /// <summary>
    /// Логика взаимодействия для AddForm.xaml
    /// </summary>
    public partial class AddForm : Window
    {
        private MainWindow mainWindow;

        public AddForm(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (mainWindow != null)
            {
                mainWindow.Show();
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            Cloth new_item = new Cloth();
            try
            {
                new_item.cloth_name = NameTextBox.Text;
                new_item.cloth_article = ArticleTextBox.Text;
                new_item.cloth_price = Convert.ToDouble(PriceTextBox.Text);
                new_item.cloth_type = TypeTextBox.Text;
                new_item.cloth_number = Convert.ToInt32(NumberTextBox.Text);
            }
            catch(Exception) {
                MessageBox.Show("Помилка конвертації даних.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(new_item.cloth_article.Length != 5)
            {
                MessageBox.Show("Артикул повинен містити 5 символів.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(new_item.cloth_type != "M" && new_item.cloth_type != "F" && new_item.cloth_type != "K")
            {
                MessageBox.Show("Тип одягу повинен бути 'M'(для чловіків), 'F'(для жінок) або 'K'(для дітей).", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            bool added = DB_Communication.DB_Add_Item(new_item);
            if (added)
            {
                MessageBox.Show("Успішно додано!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                mainWindow.Show();// Оновлення списку
                mainWindow.Db_download(null, null);
                Close(); 
            }
            
            
        }
    }
}
