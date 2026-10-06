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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace DetMir.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageRegistr.xaml
    /// </summary>
    public partial class PageRegistr : Page
    {
        public PageRegistr()
        {
            InitializeComponent();

            ConnectOdb.ConObj.Roles.Load();
            comboBox1.ItemsSource = ConnectOdb.ConObj.Roles.Local;
        }

        private void BtnRg_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectOdb.ConObj.Users.Count(x => x.Login == tbLog1.Text)>0)
            {
                MessageBox.Show("Такой пользователь уже существует", "ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                User userObj = new User()
                {
                    FIO = tbFio.Text,
                    role1 = (comboBox1.SelectedItem as Role).ID,
                    Login = tbLog1.Text,
                    Password = tbPas1.Text,
                };
                ConnectOdb.ConObj.Users.Add(userObj);
                ConnectOdb.ConObj.SaveChanges();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
        }
    }
}
