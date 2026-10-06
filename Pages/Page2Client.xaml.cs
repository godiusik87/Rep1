using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace DetMir.Pages
{
    public partial class Page2Client : Page
    {
        public Page2Client(object DataContext)
        {
            InitializeComponent();

            // 1. Приветствие
            clientTX.Text = "Вы вошли как клиент " + (string)DataContext;

            // 2. Грузим связанные таблицы
            ConnectOdb.ConObj.Manufactures.Load();
            ConnectOdb.ConObj.Providers.Load();
            ConnectOdb.ConObj.Products.Load();

            // 3. Заполняем список и таблицу
            ListProduct.ItemsSource = ConnectOdb.ConObj.Products.Local.ToList();
            gridList.ItemsSource = ConnectOdb.ConObj.Products.Local.ToList();

            // 4. Показываем количество
            RowCount();
        }

        // Счётчик выведенных записей
        public void RowCount()
        {
            rowCount.Text = $"Выведено {gridList.Items.Count} записей из {ConnectOdb.ConObj.Products.Local.Count}";
        }

        // ПОИСК по наименованию
        private void PoiskTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            gridList.ItemsSource = null;

            var query = ConnectOdb.ConObj.Products.Local
                .Where(x => x.Name.ToLower().Contains(PoiskTB.Text.ToLower()));

            gridList.ItemsSource = query.ToList();
            ListProduct.ItemsSource = query.ToList();

            RowCount();
        }

        // СОРТИРОВКА по стоимости
        private void sortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (sortCB.SelectedIndex)
            {
                case 0:  // возрастание
                    gridList.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .OrderBy(x => x.Stoimost).ToList();
                    ListProduct.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .OrderBy(x => x.Stoimost).ToList();
                    break;
                case 1:  // убывание
                    gridList.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .OrderByDescending(x => x.Stoimost).ToList();
                    ListProduct.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .OrderByDescending(x => x.Stoimost).ToList();
                    break;
            }
            RowCount();
        }

        // ФИЛЬТРАЦИЯ по скидке
        private void fitCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (fitCB.SelectedIndex)
            {
                case 0:  // Все диапазоны
                    gridList.ItemsSource = ConnectOdb.ConObj.Products.Local.ToList();
                    ListProduct.ItemsSource = ConnectOdb.ConObj.Products.Local.ToList();
                    break;
                case 1:  // 0–10
                    gridList.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .Where(x => x.Discount <= 10).ToList();
                    ListProduct.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .Where(x => x.Discount <= 10).ToList();
                    break;
                case 2:  // 11–14
                    gridList.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .Where(x => x.Discount >= 11 && x.Discount <= 14).ToList();
                    ListProduct.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .Where(x => x.Discount >= 11 && x.Discount <= 14).ToList();
                    break;
                case 3:  // 15+
                    gridList.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .Where(x => x.Discount >= 15).ToList();
                    ListProduct.ItemsSource = ConnectOdb.ConObj.Products.Local
                        .Where(x => x.Discount >= 15).ToList();
                    break;
            }
            RowCount();
        }
    }
}