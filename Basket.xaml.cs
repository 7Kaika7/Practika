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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Praktika
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        int count = 0; 
        int columns = 0;
        int row = 0;

        public Basket()
        {
            InitializeComponent();

            var contex = new AppDbContext();
            var q = contex.Baskets.Count(); var l = contex.Baskets.Where(x => x.ID > 0).ToList();
            int ss = l.Sum(x => Convert.ToInt32(x.count));
            var w = contex.Baskets.Where(x => x.ID > 0).ToList();
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
                string a = w[count].image.ToString();
                image.Source = new BitmapImage(new Uri($"{a}", UriKind.RelativeOrAbsolute));

                TextBlock textBlock = new TextBlock();
                textBlock.Text = w[count].naim;
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock kol = new TextBlock();
                kol.Text = w[count].naim;
                kol.TextWrapping = TextWrapping.Wrap;
                kol.TextAlignment = TextAlignment.Center;
                kol.VerticalAlignment = VerticalAlignment.Center;
                kol.HorizontalAlignment = HorizontalAlignment.Center;



                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = w[count].price.ToString()  + " руб.";
                textBlock1.Width = 150;
                textBlock1.Height = 35;
                textBlock1.FontSize = 20;

                //button.Click += Button_Click;


                //  textBlock.Style = transparentButtonStyle;
                //  textBlock.Click += Button_Click;
                chislo.Text = ss.ToString();
                Grid.SetColumn(image, columns);
                Grid.SetRow(image, row);
                Grid.SetColumn(textBlock, columns + 1);
                Grid.SetRow(textBlock, row);
                Grid.SetColumn(textBlock1, columns + 2);
                Grid.SetRow(textBlock1, row);
                myGrid.Children.Add(image);
                myGrid.Children.Add(textBlock);
                myGrid.Children.Add(textBlock1);
                row++;
                count++;
            }


          
        }




        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Button button = sender as Button; var context = new AppDbContext();
        //    string par = button.CommandParameter as string; var q = context.Catalogs.Where(x => x.image == par).ToList();
        //    var r = context.Baskets.Where(x => x.image == par).ToList(); if (r.Count > 0)
        //    {
        //        if (q[0].id == r[0].ID)
        //        {
        //            string price = (Convert.ToInt32(r[0].count) + 1).ToString();
        //            var h = context.Baskets.Where(x => x.ID == r[0].ID).AsEnumerable().Select(x => { x.count = price; return x; }); foreach (var x in h)
        //            {
        //                context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //            }
        //            context.SaveChanges();
        //        }
        //    }
        //    else
        //    {
        //        var tovar = new Bask { ID = q[0].id, image = q[0].image, naim = q[0].naim, count = "1", price = q[0].price }; 
        //        context.Baskets.Add(tovar);
        //    }
        //    context.SaveChanges(); 
        //    var l = context.Baskets.Where(x => x.ID> 0).ToList();
        //    int ss = l.Sum(x => Convert.ToInt32(x.count)); 
        //    chislo.Text = ss.ToString();
        //}

        private void back_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            Catalog back = new Catalog();
            back.Show();

        }

        // Cложения цен
        private void GetTotalPriceButton_Click(object sender, RoutedEventArgs e)
        {
            decimal totalPrice = GetTotalPrice();
            totalPriceTextBox.Text = totalPrice.ToString("Сумма заказа составит " + "0.##" + " руб."); // Выводим сумму с двумя знаками после запятой
        }

        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0m;

            using (AppDbContext context = new AppDbContext())
            {
                totalPrice = (decimal)context.Baskets.Sum(p => p.price);
            }

            return totalPrice;
        }

        private void ClearTableButton_Click(object sender, RoutedEventArgs e)
        {
            ResetTable();
            ShowSuccessDialog();
            totalPriceTextBox.Text = "";
        }

        private void ResetTable()
        {
            using (AppDbContext context = new AppDbContext())
            {
                context.Baskets.RemoveRange(context.Baskets);
                context.SaveChanges();
            }
        }

        private void ShowSuccessDialog()
        {
            MessageBox.Show("Покупка успешно завершена!");
        }
    }
}






        

      

       



