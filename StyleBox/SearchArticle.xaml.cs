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
    /// Логика взаимодействия для SearchArticle.xaml
    /// </summary>
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
    }
}
