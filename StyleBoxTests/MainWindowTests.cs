using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StyleBox.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void Search_ArticleTest()
        {
            Cloth expected_cloth = new Cloth
            {
                cloth_article = "00001",
                cloth_name = "Jeans",
                cloth_price = 300.0,
                cloth_type = "M",
                cloth_number = 210
            };

            MainWindow mainWindow = new MainWindow();
            object sender = mainWindow;
            RoutedEventArgs e = null;

            SearchArticle searchArtWindow = new SearchArticle(mainWindow);
            searchArtWindow.SearchArticleBox.Text = "00001";

            searchArtWindow.Button_Click(sender, e);

            Cloth actual_cloth = mainWindow.clothList[0];


            Assert.AreEqual(expected_cloth.cloth_article, actual_cloth.cloth_article);
        }
    }
}