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
    /// Логика взаимодействия для KartPage.xaml
    /// </summary>
    public partial class KartPage : Page
    {
        public KartPage()
        {
            InitializeComponent();
            listv.ItemsSource = Us.db.Transact_history.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
