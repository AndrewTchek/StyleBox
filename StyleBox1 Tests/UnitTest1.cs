using Microsoft.VisualStudio.TestTools.UnitTesting;
using StyleBox;
using System.Windows;

namespace StyleBox1_Tests;

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        // Arrange

        Cloth expected_cloth = new Cloth
        {
            cloth_name = "Jeans",
            cloth_article = "00001",
            cloth_price = 300.00,
            cloth_type = "M",
            cloth_number = 210
        };

        // Act
        MainWindow target = new MainWindow();
        target.Search_Article();



    }
}
