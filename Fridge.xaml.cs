using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для Fridge.xaml
    /// </summary>
    public partial class Fridge : Window
    {
        int count = 0;
        int columns = 0;
        int row = 0;

        public Fridge(string imageUrl, string description)
        {
            InitializeComponent();

            Image myImage = new Image();
            myImage.Source = new BitmapImage(new Uri(imageUrl));
            // Остальной код, связанный с отображением изображения

            TextBlock myTextBlock = new TextBlock();
            myTextBlock.Text = description;
            // Остальной код, связанный с отображением описания
        }

       

        //  private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Строка подключения к базе данных
        //    string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=household_appliances;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //    // SQL-запрос для получения значения определенного столбца и строки
        //    string query = "SELECT opis FROM Catalogs";

        //    // Создаем подключение к базе данных
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        // Создаем команду для выполнения запроса
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            try
        //            {
        //                // Открываем подключение
        //                connection.Open();

        //                // Выполняем запрос и получаем результат
        //                string fieldValue = command.ExecuteScalar()?.ToString();

        //                // Выводим значение столбца и строки в текстовое поле
        //                outputTextBox.Text = fieldValue;
        //            }
        //            catch (Exception ex)
        //            {
        //                // Обрабатываем ошибку подключения или выполнения запроса
        //                MessageBox.Show(ex.Message);
        //            }
        //        }
        //    }
        //}

        private void back_Click(object sender, RoutedEventArgs e)
        {
           
                this.Hide();
                Catalog back = new Catalog();
                back.Show();
            
        }
    }
}
