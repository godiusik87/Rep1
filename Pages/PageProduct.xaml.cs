using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace DetMir.Pages
{
    public partial class PageProduct : Page
    {
        // Кэш всех товаров — загружаем ОДИН РАЗ при открытии страницы
        private List<Product> _allProducts;

        public PageProduct()
        {
            InitializeComponent();

            // Один раз грузим связанные таблицы и товары
            ConnectOdb.ConObj.Manufactures.Load();
            ConnectOdb.ConObj.Providers.Load();

            _allProducts = ConnectOdb.ConObj.Products.ToList();

            // Заполняем ListBox и DataGrid
            ListProduct.ItemsSource = _allProducts;
            gridList.ItemsSource = _allProducts;
        }

        // Поиск — срабатывает при каждом изменении текста
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_allProducts == null) return;

            string query = (TxtSearch.Text ?? "").Trim().ToLower();

            ListProduct.ItemsSource = string.IsNullOrWhiteSpace(query)
                ? _allProducts
                : _allProducts
                    .Where(x => x.Name != null && x.Name.ToLower().Contains(query))
                    .ToList();
        }

        // Кнопка «Редактировать» — передаём товар в PageEditNew
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button?.DataContext as Product;
            if (product == null) return;

            FrameObj.MainFrame.Navigate(new PageEditNew(product));
        }

        // Сортировка по возрастанию
        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            if (_allProducts == null) return;
            gridList.ItemsSource = _allProducts.OrderBy(x => x.Name).ToList();
        }

        // Сортировка по убыванию
        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            if (_allProducts == null) return;
            gridList.ItemsSource = _allProducts.OrderByDescending(x => x.Name).ToList();
        }

        private void DelBT_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (ListProduct.SelectedItems != null)
                {
                    var s = (Product)ListProduct.SelectedItem;

                    var pla = ConnectOdb.ConObj.Placements.Where(w => w.Article1 == s.Article).FirstOrDefault();
                    if (pla != null) 
                    {
                        ConnectOdb.ConObj.Placements.Remove(pla);
                    }
                    var pla1 = ConnectOdb.ConObj.OrderCompositions.Where(w => w.Article2 == s.Article).FirstOrDefault();
                    if (pla1 != null) 
                    {
                        ConnectOdb.ConObj.OrderCompositions.Remove(pla1);
                    }
                    ConnectOdb.ConObj.Products.Remove(s);
                    ConnectOdb.ConObj.SaveChanges();
                }
            }
        }
    }
}