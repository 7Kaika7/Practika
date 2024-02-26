using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Button = System.Windows.Controls.Button;

namespace Praktika
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        int count = 0;
        int columns = 0;
        int row = 0;
    

        public Catalog()
        {

            InitializeComponent();

                var context = new AppDbContext();
                var q = context.Catalogs.Count();
                var l = context.Baskets.Where(x => x.ID > 0).ToList();
                int ss = l.Sum(x => Convert.ToInt32(x.count));
                var W = context.Catalogs.Where(x => x.id > 0).ToList();
                while (count < q)
                {
                    if (columns == 4)
                    {
                        columns = 0;
                        row += 1;
                  
                    }

                    // Создание стиля
                    Style transparentButtonStyle = new Style(typeof(Button));
                    transparentButtonStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.Transparent));
                    transparentButtonStyle.Setters.Add(new Setter(BorderBrushProperty, Brushes.Transparent));
                    transparentButtonStyle.Setters.Add(new Setter(ForegroundProperty, Brushes.White));


                  Image image = new Image();
                  string imageUrl = W[count].image.ToString(); // Путь к изображению
                  BitmapImage bitmap = new BitmapImage(new Uri(imageUrl, UriKind.RelativeOrAbsolute));
                  image.Source = bitmap;
                  image.VerticalAlignment = VerticalAlignment.Top;
                  image.Width = 125;
                  image.Height = 135;
                  //image.MouseLeftButtonUp += MyImage_MouseLeftButtonUp;

                TextBlock textBlock = new TextBlock();
                  textBlock.Text =  W[count].naim + "\n" + W[count].price + " руб." + "\n" ;
                  textBlock.FontSize = 20;
                  textBlock.TextAlignment = TextAlignment.Center;
                  textBlock.VerticalAlignment = VerticalAlignment.Bottom;

                  Button button = new Button();
                  button.Content = "В корзину";
                  button.Width = 150;
                  button.Height = 35;
                  button.FontSize = 20;
                  button.VerticalAlignment = VerticalAlignment.Bottom;
                  
                  button.CommandParameter = imageUrl;
                  button.Click += But_Click;
                  button.Style = transparentButtonStyle;

               
                Button button1 = new Button();
                button1.Width = 30;
                button1.Content = "*  ";
                button1.Height = 30;
                button1.FontSize = 20;
                button1.CommandParameter = imageUrl;
                button1.VerticalAlignment = VerticalAlignment.Top;
                button1.Style = transparentButtonStyle;
                button1.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                button1.Click += btn_Click;
 
                Button Info = new Button();
                  Info.Template = (ControlTemplate)Resources["кнопка"];
                  Info.CommandParameter = imageUrl;
                  Info.Click += Info_Click;
                  chislo.Text = ss.ToString();

                // Добавление элементов на Gridw
                  Grid.SetRow(image, row );
                  Grid.SetRow(textBlock, row );
                  Grid.SetRow(button, row );
                  Grid.SetRow(Info, row );
                  Grid.SetRow(button1, row );


                  Grid.SetColumn(image, columns);
                  Grid.SetColumn(textBlock, columns);
                  Grid.SetColumn(button, columns);
                  Grid.SetColumn(button1, columns);
                  Grid.SetRowSpan(Info, 4);

                // Добавление элементов на Grid
                  myGrid.Children.Add(image);
                  myGrid.Children.Add(textBlock);
                  myGrid.Children.Add(button);
                  myGrid.Children.Add(Info);
                  myGrid.Children.Add(button1);

               

                  columns++;
                  count++;



                }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string imageUrl = btn.CommandParameter as string;

                var context = new AppDbContext();
                var product = context.Catalogs.FirstOrDefault(x => x.image == imageUrl);

                if (product != null)
                {
                    System.Windows.MessageBox.Show($"{product.opis}");
                }
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void basket_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Basket basket = new Basket();
            basket.Show();

        }

        private void But_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(chislo.Text, out int currentValue))
            {

                currentValue++;
                chislo.Text = currentValue.ToString();
            }

            Button button = sender as Button; var context = new AppDbContext();
            string par = button.CommandParameter as string; var q = context.Catalogs.Where(x => x.image == par).ToList();
            var r = context.Baskets.Where(x => x.image == par).ToList(); if (r.Count > 0)
            {
                if (q[0].id == r[0].ID)
                {
                    string cost = (Convert.ToInt32(r[0].count) + 1).ToString();
                    var h = context.Baskets.Where(x => x.ID == r[0].ID).AsEnumerable().Select(x => { x.count = cost; return x; }); foreach (var x in h)
                    {
                        context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
            else
            {
                var tov = new Bask { image = q[0].image, naim = q[0].naim, count = "1", price = q[0].price };
                context.Baskets.Add(tov);
            }
            context.SaveChanges();
            var l = context.Baskets.Where(x => x.ID > 0).ToList(); int ss = l.Sum(x => Convert.ToInt32(x.count));


        }

        public string GetDescriptionFromDatabase(string imageUrl)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=household_appliances;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string description = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT opis FROM Catalogs WHERE image = @image";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@image", imageUrl);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            description = reader.GetString(0);
                        }
                    }
                }
            }

            return description;
        }

                //Image clickedImage = (Image)sender;

                //// Получите путь к изображениюS
                //string imageUrl = clickedImage.Source.ToString();

                //// Получите описание из базы данных по пути к изображению
                //string description = GetDescriptionFromDatabase.(imageUrl);

                //// Создаем новое окно и передаем изображение и описание
                //Fridge imageWindow = new Fridge(imageUrl, description);
                //imageWindow.Show();


    }
}