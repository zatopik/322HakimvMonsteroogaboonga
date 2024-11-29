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
    /// Логика взаимодействия для Vivid.xaml
    /// </summary>
    public partial class Vivid : Page
    {
        public Vivid()
        {
            InitializeComponent();
        }

        private void Depos(object sender, RoutedEventArgs e)
        {
            var TransTemp = new Transact_history { Prof_id = Us.LoggedUser.Prof_id, Trans_ID = -(int.Parse(Amount.Text)), Trans_date = DateTime.Now };
           
          
            {
                MessageBox.Show($"Выведено {Amount.Text}₽");
                Us.LoggedUser.balance -= int.Parse(Amount.Text);
                Us.db.Transact_history.Add(TransTemp);
                Us.db.SaveChanges();
               
                Amount.Clear();
                
                NavigationService.Navigate(new MainPage());
            }
        }
    }
}
