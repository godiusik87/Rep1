using System;
using ClassLibrary1;
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

namespace DetMir.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAvtoriz.xaml
    /// </summary>
    public partial class PageAvtoriz : Page
    {
        public PageAvtoriz()
        {
            InitializeComponent();
            Class1 param = new Class1();
            param.Method();
        }
        public static System.Windows.MessageBoxResult Show(string messageBoxText, string caption, System.Windows.MessageBoxButton button, System.Windows.MessageBoxImage icon)
        {
            return new MessageBoxResult();
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new Page1Guest());
        }

        private void BtnPerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = ConnectOdb.ConObj.Users
                    .FirstOrDefault(x => x.Login == tbLog.Text && x.Password == PbPass.Password);

                DataContext = userObj.FIO;

                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя не существует",
                                    "Ошибка авторизации",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                switch (userObj.role1)
                {
                    case 1:
                        MessageBox.Show("Добро пожаловать. Вы авторизовались как администратор " + userObj.FIO,
                                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        FrameObj.MainFrame.Navigate(new Page3Admin(DataContext));
                        break;

                    case 2:
                        MessageBox.Show("Добро пожаловать. Вы авторизовались как менеджер " + userObj.FIO,
                                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        FrameObj.MainFrame.Navigate(new Page4Manadzer());
                        break;

                    case 3:
                        MessageBox.Show("Добро пожаловать. Вы авторизовались как пользователь " + userObj.FIO,
                                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        FrameObj.MainFrame.Navigate(new Page2Client(DataContext));
                        break;

                    default:
                        MessageBox.Show("Данные не обнаружены",
                                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка: " + Ex.Message,
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.Navigate(new PageRegistr());
        }
    }
}
