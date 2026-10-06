using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DetMir.Pages
{
    public partial class PageEditNew : Page
    {
        // Товар, переданный из PageProduct
        private Product _product;

        string mainImg;
        List<string> imgList = new List<string>();

        public PageEditNew(Product product)
        {
            InitializeComponent();

            _product = product;

            // === 1. Заполняем ComboBox'ы ===

            comboBox2.SelectedValuePath = "ID";
            comboBox2.DisplayMemberPath = "Manufacture1";
            comboBox2.ItemsSource = ConnectOdb.ConObj.Manufactures.ToList();

            comboBox3.SelectedValuePath = "ID";
            comboBox3.DisplayMemberPath = "Provider1";
            comboBox3.ItemsSource = ConnectOdb.ConObj.Providers.ToList();

            comboBox4.SelectedValuePath = "Number_category";
            comboBox4.DisplayMemberPath = "Name_category";
            comboBox4.ItemsSource = ConnectOdb.ConObj.ProductCategories.ToList();

            // === 2. Заполняем текстовые поля данными товара ===

            articleTX.Text = product.Article;
            titleTX.Text = product.Name;
            StoiMTX.Text = product.Stoimost?.ToString();
            MaxSkdTX.Text = product.Max_discount?.ToString();
            scidTX.Text = product.Discount?.ToString();
            colSkladTX.Text = product.Quantity_in_stock?.ToString();
            descTX.Text = product.Description;

            // Единица измерения — выбираем в ComboBox нужный пункт
            foreach (ComboBoxItem item in typeEdIzmCB.Items)
            {
                if ((string)item.Content == product.Unit)
                {
                    typeEdIzmCB.SelectedItem = item;
                    Lbl.Content = item.Content.ToString();
                    break;
                }
            }

            // === 3. Устанавливаем выбранного производителя / поставщика / категорию ===

            if (product.Manufacture2 != null)
                comboBox2.SelectedValue = product.Manufacture2;

            if (product.Provide2 != null)
                comboBox3.SelectedValue = product.Provide2;

            if (product.Number_category != null)
                comboBox4.SelectedValue = product.Number_category;

            // === 4. Фото ===

            mainImgName.Text = System.IO.Path.GetFileName(product.Photo ?? "");
            StringImage.Text = product.Photo;   // путь уже с "../Resources/"
        }

        // Кнопка «Обзор» — выбрать новую картинку
        private void AddIMG(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image | *.png; *.jpg";
            if (fd.ShowDialog() == true)
            {
                mainImg = fd.SafeFileName;
                mainImgName.Text = mainImg;
                StringImage.Text = "../Resources/" + mainImg;
            }
        }

        // Смена единицы измерения
        private void EdIzmCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo?.SelectedItem is ComboBoxItem item)
                Lbl.Content = item.Content.ToString();
        }

        // Кнопка «Сохранить» — обновляем запись в БД
        private void saveAddBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Перезаписываем поля товара новыми значениями
                _product.Article = articleTX.Text;
                _product.Name = titleTX.Text;
                _product.Stoimost = ParseFloat(StoiMTX.Text);
                _product.Max_discount = ParseFloat(MaxSkdTX.Text);
                _product.Discount = ParseFloat(scidTX.Text);
                _product.Quantity_in_stock = ParseInt(colSkladTX.Text);
                _product.Description = descTX.Text;
                _product.Photo = StringImage.Text;
                _product.Unit = Convert.ToString(Lbl.Content);

                _product.Manufacture2 = (comboBox2.SelectedItem as Manufacture)?.ID;
                _product.Provide2 = (comboBox3.SelectedItem as Provider)?.ID;
                _product.Number_category = (comboBox4.SelectedItem as ProductCategory)?.Number_category;

                // Сохраняем изменения
                ConnectOdb.ConObj.SaveChanges();

                MessageBox.Show("Данные успешно изменены!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                FrameObj.MainFrame.Navigate(new PageProduct());
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