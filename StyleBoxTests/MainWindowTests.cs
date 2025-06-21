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

            mainWindow.Close();
            Assert.AreEqual(expected_cloth.cloth_article, actual_cloth.cloth_article);
        }

        [TestMethod()]
        public void Search_TypeTest()
        {
            List<Cloth> expected_list = new List<Cloth>
            {
                new Cloth
                {
                    cloth_article = "00002",
                    cloth_name = "Jacket",
                    cloth_price = 1500.0,
                    cloth_type = "M",
                    cloth_number = 100
                },
                new Cloth
                {
                    cloth_article = "00001",
                    cloth_name = "Jeans",
                    cloth_price = 300.0,
                    cloth_type = "M",
                    cloth_number = 210
                }
            };


            MainWindow mainWindow = new MainWindow();
            object sender = mainWindow;
            RoutedEventArgs e = null;

            SearchType searchTypeWindow = new SearchType(mainWindow);

            searchTypeWindow.SearchTypeBox.Text = "M";

            searchTypeWindow.Button_Click(sender, e);
            List<Cloth> actual_list = mainWindow.clothList;

            mainWindow.Close();
            for (int i = 0; i < actual_list.Count; i++)
            {
                Assert.AreEqual(expected_list[i].cloth_article, actual_list[i].cloth_article);
                Assert.AreEqual(expected_list[i].cloth_name, actual_list[i].cloth_name);
                Assert.AreEqual(expected_list[i].cloth_price, actual_list[i].cloth_price);
                Assert.AreEqual(expected_list[i].cloth_type, actual_list[i].cloth_type);
                Assert.AreEqual(expected_list[i].cloth_number, actual_list[i].cloth_number);
            }
        }

        [TestMethod()]
        public void Add_ClickTest()
        {
            Cloth expected_cloth = new Cloth
            {
                cloth_article = "00004",
                cloth_name = "T-Shirt",
                cloth_price = 200.0,
                cloth_type = "F",
                cloth_number = 200
            };

            MainWindow mainWindow = new MainWindow();
            object sender = mainWindow;
            RoutedEventArgs e = null;

            AddForm addForm = new AddForm(mainWindow);
            addForm.NameTextBox.Text = expected_cloth.cloth_name;
            addForm.ArticleTextBox.Text = expected_cloth.cloth_article;
            addForm.PriceTextBox.Text = expected_cloth.cloth_price.ToString();
            addForm.TypeTextBox.Text = expected_cloth.cloth_type;
            addForm.NumberTextBox.Text = expected_cloth.cloth_number.ToString();


            addForm.Add_Button_Click(sender, e);

            Assert.IsTrue(mainWindow.clothList.Count > 0);

            Cloth actual_cloth = mainWindow.clothList.Last();

            mainWindow.Close();

            Assert.AreEqual(expected_cloth.cloth_article, actual_cloth.cloth_article);
            Assert.AreEqual(expected_cloth.cloth_name, actual_cloth.cloth_name);
            Assert.AreEqual(expected_cloth.cloth_price, actual_cloth.cloth_price);
            Assert.AreEqual(expected_cloth.cloth_type, actual_cloth.cloth_type);
            Assert.AreEqual(expected_cloth.cloth_number, actual_cloth.cloth_number);


        }
    }
}