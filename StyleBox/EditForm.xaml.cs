using MySql.Data.MySqlClient;
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
    public partial class EditForm : Window
    {
        private Cloth Selected_Cloth;
        private MainWindow mainWindow;
        public EditForm(Cloth cloth, MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            Selected_Cloth = cloth;
            NameTextBox.Text = Selected_Cloth.cloth_name;
            ArticleTextBox.Text = Selected_Cloth.cloth_article;
            PriceTextBox.Text = Selected_Cloth.cloth_price.ToString();
            TypeTextBox.Text = Selected_Cloth.cloth_type.ToString();
            NumberTextBox.Text = Selected_Cloth.cloth_number.ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (mainWindow != null)
            {
                mainWindow.Show();
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e) 
        { 

            Selected_Cloth.cloth_name = NameTextBox.Text;
            Selected_Cloth.cloth_article = ArticleTextBox.Text;
            Selected_Cloth.cloth_price = Convert.ToDouble(PriceTextBox.Text);
            Selected_Cloth.cloth_type = TypeTextBox.Text;
            Selected_Cloth.cloth_number = Convert.ToInt32(NumberTextBox.Text);

            bool edited = DB_Communication.DB_Update_Item(Selected_Cloth);
           
            if (edited)
            {
                MessageBox.Show("Успішно оновлено!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                mainWindow.Show();
                mainWindow.Db_download(null, null);
                Close();
            }
            else
            {
                MessageBox.Show("Помилка при доданні", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
