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
    /// Логика взаимодействия для Form_buy.xaml
    /// </summary>
    public partial class Form_buy : Window
    {
        public Form_buy()
        {
            InitializeComponent();
        }
        public static class Buy_ticket
        {
            public static decimal Value { get; set; }
            public static DateTime date_of_bought { get; set; }
            public static int client_id { get; set; }
            public static int event_name_id { get; set; }

        }
        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_client.Text.Length == 0)
            {
                MessageBox.Show("Укажите ID клиента!");
                return;
            }
            if (txt_event.Text.Length == 0)
            {
                MessageBox.Show("Укажите ID события!");
                return;
            }
            if (txt_sum.Text.Length == 0)
            {
                MessageBox.Show("Укажите стоимость!");
                return;
            }

            Buy_ticket.Value = Convert.ToDecimal(txt_sum.Text);
            Buy_ticket.date_of_bought = DateTime.Now;
            Buy_ticket.client_id = Convert.ToInt32(txt_client.Text);
            Buy_ticket.event_name_id = Convert.ToInt32(txt_event.Text);

            this.Close();
        }
    }
}
