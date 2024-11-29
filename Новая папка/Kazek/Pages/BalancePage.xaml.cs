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
    /// Логика взаимодействия для BalancePage.xaml
    /// </summary>
    public partial class BalancePage : Page
    {
        public BalancePage(ProfilePage profilePage)
        {
            InitializeComponent();

        }

        private void Depos(object sender, RoutedEventArgs e)
        {
            if (Amount.Text == null)
            {
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                var TransTemp = new Transact_history { Prof_id = Us.LoggedUser.Prof_id, Trans_ID = int.Parse(Amount.Text), Trans_date = DateTime.Now };


                {
                    MessageBox.Show($"Счет пополнен на {Amount.Text}");
                    Us.LoggedUser.balance += int.Parse(Amount.Text);
                    Us.db.Transact_history.Add(TransTemp);
                    Us.db.SaveChanges();


                    Amount.Clear();

                    NavigationService.Navigate(new MainPage());
                }
            }
        }
    
    }
}
