using Kazek.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage(LoginPage loginPage)
        {
            InitializeComponent();
        }

        public bool IsLoginUnique(string log)
        {
          
                var exists = Us.db.Pofile.FirstOrDefault(u => u.Login == log);
                if (exists == null)
                    return true;
                return false;
            
            
        }

        private void RegCl(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
           
            string pass = Password.Password;
            
            if (login == null  || pass == null || pass == "")
            {
                MessageBox.Show("Заполните все данные!");
            }

            else if (IsLoginUnique(login))
            {
                var regTemp = new Pofile { Login = login,  Password = pass, balance = 0};
                Us.db.Pofile.Add(regTemp);
                Us.db.SaveChanges();
                Login.Clear();
            
                Password.Clear();
   
                NavigationService.GoBack();
            } else
            {
                MessageBox.Show("Логин уже занят, попробуйте другой!");
            }
        }
    }
}
