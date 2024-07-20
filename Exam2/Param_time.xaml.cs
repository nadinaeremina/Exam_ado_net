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
    /// Логика взаимодействия для Param_time.xaml
    /// </summary>
    public partial class Param_time : Window
    {
        public Param_time()
        {
            InitializeComponent();
        }

        public DateTime MyTime;

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txt_hour.Text) >24 || Convert.ToInt32(txt_hour.Text) < 0)
            {
                MessageBox.Show("Введите часы корректно!");
                return;
            }
            if (Convert.ToInt32(txt_min.Text) > 60 || Convert.ToInt32(txt_min.Text) < 0)
            {
                MessageBox.Show("Введите минуты корректно!");
                return;
            }


            MyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(txt_hour.Text), Convert.ToInt32(txt_min.Text), 00);

            this.Close();
        }
    }
}
