using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            var log = Login.Text;
            var pass = Password.Text;
            var context = new AppDbContext();
            var LogEnter = context.Users.SingleOrDefault(x => x.Login == log && x.Password == pass);

            if (log != "Введите логин" && pass != "")
            {
                if (LogEnter != null)
                {
                    this.Hide();
                    Catalog catalog = new Catalog();
                    catalog.Show();
                    var log1 = context.Users.SingleOrDefault(x => (x.Login == log && x.Password == pass));
                }
                else
                {
                    int count = 0;
                    count++;
                    Login.BorderBrush = new SolidColorBrush(Colors.Red);
                    Password.BorderBrush = new SolidColorBrush(Colors.Red);
                    oshibka.Text = "Неправильно введен логин или пароль";
                    oshibka.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void registration_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.Show();
        }
    }
}