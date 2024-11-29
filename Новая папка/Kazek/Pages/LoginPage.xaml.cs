using Kazek.DB;
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

namespace Kazek.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void EnterCl(object sender, RoutedEventArgs e)
        {
            var login = Login.Text;
            var pass = Password.Password;

            var user = Us.db.Pofile.FirstOrDefault(i => i.Password == pass && i.Login == login);

            if (user != null)
            {
                Us.LoggedUser = user;
                NavigationService.Navigate(new MainPage());

            }
            else
            {
                MessageBox.Show("Проверьте правильность введеных данных");
            }
        }

        private void Register_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegPage(this));
        }
    }
}
