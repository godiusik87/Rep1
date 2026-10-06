using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DetMir.Pages
{
    public partial class PageAddProduct : Page
    {
        string mainImg;
        List<string> imgList = new List<string>();

        public PageAddProduct()
        {
            InitializeComponent();

            // Производитель
            comboBox2.SelectedValuePath = "ID";
            comboBox2.DisplayMemberPath = "Manufacture1";
            comboBox2.ItemsSource = ConnectOdb.ConObj.Manufactures.ToList();

            // Поставщик
            comboBox3.SelectedValuePath = "ID";
            comboBox3.DisplayMemberPath = "Provider1";
            comboBox3.ItemsSource = ConnectOdb.ConObj.Providers.ToList();

            // Категория товара
            comboBox4.SelectedValuePath = "Number_category";
            comboBox4.DisplayMemberPath = "Name_category";
            comboBox4.ItemsSource = ConnectOdb.ConObj.ProductCategories.ToList();
        }

        // Загрузка картинки
        private void AddIMG(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image | *.png; *.jpg";
            if (fd.ShowDialog() == true)
            {
                mainImgName.Text = fd.SafeFileName;
                mainImg = fd.SafeFileName;
                StringImage.Text = "../Resources/" + mainImg;
            }
        }

        // Выбор единицы измерения
        private void EdIzmCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo?.SelectedItem is ComboBoxItem item)
                Lbl.Content = item.Content.ToString();
        }

        // Сохранение товара
        private void saveAddBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product()
                {
                    Article = articleTX.Text,
                    Name = titleTX.Text,
                    Stoimost = ParseFloat(StoiMTX.Text),
                    Max_discount = ParseFloat(MaxSkdTX.Text),
                    Discount = ParseFloat(scidTX.Text),
                    Quantity_in_stock = ParseInt(colSkladTX.Text),
                    Description = descTX.Text,
                    Photo = StringImage.Text,
                    Unit = Convert.ToString(Lbl.Content),

                    Manufacture2 = (comboBox2.SelectedItem as Manufacture)?.ID,
                    Provide2 = (comboBox3.SelectedItem as Provider)?.ID,
                    Number_category = (comboBox4.SelectedItem as ProductCategory)?.Number_category,
                };

                ConnectOdb.ConObj.Products.Add(product);
                ConnectOdb.ConObj.SaveChanges();

                MessageBox.Show("Данные успешно добавлены!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                FrameObj.MainFrame.Navigate(new PageAddProduct());
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
        }

        private float? ParseFloat(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            if (float.TryParse(s.Replace('.', ','), out float r)) return r;
            return null;
        }

        private int? ParseInt(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            if (int.TryParse(s, out int r)) return r;
            return null;
        }
    }
}