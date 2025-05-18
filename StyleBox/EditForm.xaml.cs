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
    /// Логика взаимодействия для EditForm.xaml
    /// </summary>
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

            // и т.д. для других полей
        }



        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Selected_Cloth.cloth_name = NameTextBox.Text;
            mainWindow.Show();
            Close();
        }
    }
}
