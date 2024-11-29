using Kazek.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private Random random;
        private readonly string[] symbols = { "A", "B", "C", "7", "X", "Y", "Z" };
        private DispatcherTimer spinTimer;
        private int spinCount;
        string username;
        public GamePage(string USERNAME)
        {
            InitializeComponent();
            random = new Random();
            spinTimer = new DispatcherTimer();
            spinTimer.Interval = TimeSpan.FromMilliseconds(100);
            spinTimer.Tick += SpinTimer_Tick;
            this.username = USERNAME;
        }

        private void SpinBF(object sender, RoutedEventArgs e)
        {
            spinCount = 10; // Количество обновлений барабана
            spinTimer.Start();
            SpinB.IsEnabled = false;
            Result.Text = null;
        }
        private void SpinTimer_Tick(object sender, EventArgs e)
        {
            if (spinCount > 0)
            {
                reel1.Text = symbols[random.Next(symbols.Length)];
                if (reel1.Text == "A")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\1.png");
                    slot1.Source = new BitmapImage(imageUri);
                }
                else if (reel1.Text == "B")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\2.png");
                    slot1.Source = new BitmapImage(imageUri);
                }
                else if (reel1.Text == "C")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\3.png");
                    slot1.Source = new BitmapImage(imageUri);
                }
                else if (reel1.Text == "7")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\4.png");
                    slot1.Source = new BitmapImage(imageUri);
                }
                else if (reel1.Text == "X")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\5.png");
                    slot1.Source = new BitmapImage(imageUri);
                }
                else if (reel1.Text == "Y")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\6.png");
                    slot1.Source = new BitmapImage(imageUri);
                }
                else if (reel1.Text == "Z")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\7.png");
                    slot1.Source = new BitmapImage(imageUri);
                }
                reel2.Text = symbols[random.Next(symbols.Length)];
                if (reel2.Text == "A")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\1.png");
                    slot2.Source = new BitmapImage(imageUri);
                }
                else if (reel2.Text == "B")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\2.png");
                    slot2.Source = new BitmapImage(imageUri);
                }
                else if (reel2.Text == "C")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\3.png");
                    slot2.Source = new BitmapImage(imageUri);
                }
                else if (reel2.Text == "7")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\4.png");
                    slot2.Source = new BitmapImage(imageUri);
                }
                else if (reel2.Text == "X")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\5.png");
                    slot2.Source = new BitmapImage(imageUri);
                }
                else if (reel2.Text == "Y")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\6.png");
                    slot2.Source = new BitmapImage(imageUri);
                }
                else if (reel2.Text == "Z")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\7.png");
                    slot2.Source = new BitmapImage(imageUri);
                }
                reel3.Text = symbols[random.Next(symbols.Length)];
                if (reel3.Text == "A")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\1.png");
                    slot3.Source = new BitmapImage(imageUri);
                }
                else if (reel3.Text == "B")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\2.png");
                    slot3.Source = new BitmapImage(imageUri);
                }
                else if (reel3.Text == "C")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\3.png");
                    slot3.Source = new BitmapImage(imageUri);
                }
                else if (reel3.Text == "7")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\4.png");
                    slot3.Source = new BitmapImage(imageUri);
                }
                else if (reel3.Text == "X")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\5.png");
                    slot3.Source = new BitmapImage(imageUri);
                }
                else if (reel3.Text == "Y")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\6.png");
                    slot3.Source = new BitmapImage(imageUri);
                }
                else if (reel3.Text == "Z")
                {
                    var imageUri = new Uri("C:\\Users\\lopno\\source\\repos\\Kazek\\Kazek\\Image\\7.png");
                    slot3.Source = new BitmapImage(imageUri);
                }
                spinCount--;
            }
            else
            {
                spinTimer.Stop();
                if (decimal.Parse(UserBet.Text) != 0 || decimal.Parse(UserBet.Text) < Us.LoggedUser.balance)
                {
                    CheckWin();
                    SpinB.IsEnabled = true;
                } else
                {
                    MessageBox.Show("Не хватает денег на счету или ставка равна нулю");
                }
            }
        }
        private void CheckWin()
        {
            
            if (reel1.Text == reel2.Text && reel2.Text == reel3.Text)
            {
                    Us.LoggedUser.balance += int.Parse(UserBet.Text);
                    var GameTemp = new Sesion { Sesion_id = 1,  Sesion_date = DateTime.Now, Prof_id = Us.LoggedUser.Prof_id };
                    Us.db.Sesion.Add(GameTemp);
                    Us.db.SaveChanges();
                    MessageBox.Show($"Поздравляем! Вы выиграли {UserBet.Text}!");
            }
            else
            {
                Us.LoggedUser.balance -= int.Parse(UserBet.Text);
                var GameTemp = new Sesion { Sesion_id = 1,  Sesion_date = DateTime.Now, Prof_id = Us.LoggedUser.Prof_id };
                Us.db.Sesion.Add(GameTemp);
                Us.db.SaveChanges();
                UserBalance.Text = Us.LoggedUser.balance.ToString();
                Result.Text = "Попробуйте еще раз!";
            }
        }
        private void UserBalance_Loaded(object sender, RoutedEventArgs e)
        {
            var tempUser = Us.db.Pofile.FirstOrDefault(user => user.Login == username);
            UserBalance.Text = Convert.ToString(tempUser.balance);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HistoryPage());
        }
    }
}
