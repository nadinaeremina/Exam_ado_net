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
using System.Windows.Shapes;

namespace Exam2
{
    /// <summary>
    /// Логика взаимодействия для Add_client.xaml
    /// </summary>
    public partial class Add_client : Window
    {
        public Add_client()
        {
            InitializeComponent();
        }

        public static class Client
        {
            public static string First_name { get; set; }
            public static string Last_name { get; set; }
            public static string Middle_name { get; set; }
            public static DateTime Birthday { get; set; }
            public static string Email { get; set; }

        }
        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_date.SelectedDate.Value == null)
            {
                MessageBox.Show("Укажите дату рождения!");
                return;
            }
            if (txt_email.Text.Length == 0)
            {
                MessageBox.Show("Укажите email!");
                return;
            }
            if (txt_first_name.Text.Length == 0)
            {
                MessageBox.Show("Укажите имя!");
                return;
            }
            if (txt_last_name.Text.Length == 0)
            {
                MessageBox.Show("Укажите фамилию!");
                return;
            }
            if (txt_middle_name.Text.Length == 0)
            {
                MessageBox.Show("Укажите отчество!");
                return;
            }

            Client.Birthday = txt_date.SelectedDate.Value.Date;
            Client.Email = txt_email.Text;
            Client.First_name = txt_first_name.Text;
            Client.Last_name = txt_last_name.Text;
            Client.Middle_name = txt_middle_name.Text;

            this.Close();
        }
    }
}
