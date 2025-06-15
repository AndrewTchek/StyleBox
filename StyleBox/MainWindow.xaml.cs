using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Word = Microsoft.Office.Interop.Word;

namespace StyleBox
{
    public partial class MainWindow : System.Windows.Window
    {
        private bool autorization = true;
        public List<Cloth> clothList;
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

        public void Db_download(object sender, RoutedEventArgs e)
        {

            clothList = DB_Communication.DB_Get_Data();
            ClothListDG.ItemsSource = clothList;

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (autorization)
            {
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
                        Db_download(null, null);
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (autorization)
            {

                AddForm addForm = new AddForm(this);
                addForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Недостатньо прав");
            }
        }

        public void Search_Article(object sender, RoutedEventArgs e)
        {

            SearchArticle articleForm = new SearchArticle(this);
            articleForm.Show();
            Hide();
        }

        private void Search_Type(object sender, RoutedEventArgs e)
        {

            SearchType typeForm = new SearchType(this);
            typeForm.Show();
            Hide();
        }

        private void Word_Click(object sender, RoutedEventArgs e)
        {
            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Add();

            Word.Paragraph para = doc.Content.Paragraphs.Add();
            para.Range.Text = "Список одягу:";
            para.Range.InsertParagraphAfter();
            if (clothList == null || clothList.Count == 0 )
            {
                MessageBox.Show("Немає даних для експорту.", "Експорт", MessageBoxButton.OK, MessageBoxImage.Warning);
                doc.Close(false);
                wordApp.Quit();
                return;
            }
            Word.Table table = doc.Tables.Add(doc.Bookmarks.get_Item("\\endofdoc").Range, clothList.Count + 1, 5);
            table.Borders.Enable = 1;

            table.Cell(1, 1).Range.Text = "Назва";
            table.Cell(1, 2).Range.Text = "Артикул";
            table.Cell(1, 3).Range.Text = "Ціна";
            table.Cell(1, 4).Range.Text = "Тип";
            table.Cell(1, 5).Range.Text = "Кількість";

            for (int i = 0; i < clothList.Count; i++)
            {
                var item = clothList[i];
                table.Cell(i + 2, 1).Range.Text = item.cloth_name;
                table.Cell(i + 2, 2).Range.Text = item.cloth_article;
                table.Cell(i + 2, 3).Range.Text = item.cloth_price.ToString("F2");
                table.Cell(i + 2, 4).Range.Text = item.cloth_type;
                table.Cell(i + 2, 5).Range.Text = item.cloth_number.ToString();
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word Documents (*.docx)|*.docx";
            saveFileDialog.DefaultExt = ".docx";
            saveFileDialog.FileName = "ExportedData.docx";

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;
                    doc.SaveAs2(filePath);
                    doc.Close();
                    wordApp.Quit();

                    MessageBox.Show("Дані успішно збережені в Word!", "Експорт", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при експорті даних в Word: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                doc.Close(false);
                wordApp.Quit();
            }
        }
    }

}