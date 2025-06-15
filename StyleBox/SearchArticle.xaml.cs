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
    public partial class SearchArticle : Window
    {
        private MainWindow mainWindow;
        public SearchArticle(MainWindow main)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string inpText = SearchArticleBox.Text;
            if (inpText.Length != 5)
            {
                MessageBox.Show("Артикул повинен містити 5 символів", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            mainWindow.clothList = DB_Communication.DB_Get_Data(inpText);
            mainWindow.ClothListDG.ItemsSource = mainWindow.clothList;
            this.Close();
            mainWindow.Show();
        }
    }
}
