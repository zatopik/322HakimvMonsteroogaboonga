using Kazek.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Threading;


namespace Kazek.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        byte[] Avatar;
        public EditPage(ProfilePage profilePage)
        {
            InitializeComponent();
            DataContext = Us.LoggedUser;
            var context = new Us();
        }

        private void RegisterClick(object sender, RoutedEventArgs e) 
        {
            using (var context = new ESHKEREEntities())
            {
                int userId = Us.LoggedUser.Prof_id; 
                // Находим пользователя по идентификатору
                //var user = context.User.FirstOrDefault(u => u.id_user == );
                var user = context.Pofile.FirstOrDefault(u => u.Prof_id == userId);

                if (user != null)
                {
                    var login = LoginTB.Text;
                    var pass = PassTB.Text;
                    // Изменяем необходимые поля
                    user.Login = login;
                    user.Password = pass;

                    // Сохраняем изменения в базе данных
                    context.SaveChanges();
                }
                MessageBoxResult result = MessageBox.Show("Данные успешно изменены!", "Успех", MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new LoginPage());

                }
            //var user = new User();
            //user.username = LoginTB.Text;
            //user.password = PassTB.Text;
            //if (Avatar != Us.LoggedUser.Avatar)
            //{
            //    user.Avatar = Avatar;
            //}
            //Us.db.SaveChanges();

            //MessageBoxResult result = MessageBox.Show("Данные успешно изменены!", "Успех", MessageBoxButton.OK);
            //if (result == MessageBoxResult.OK)
            //{
            //    NavigationService.Navigate(new LoginPage());
            }
        }   
    }
}
