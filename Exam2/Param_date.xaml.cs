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
    /// Логика взаимодействия для Param_date.xaml
    /// </summary>
    public partial class Param_date : Window
    {
        public Param_date()
        {
            InitializeComponent();
        }

        public DateTime MyDate;

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_date.Text.Length == 0 )
            {
                MessageBox.Show("Выберите дату!");
                return;
            }

            MyDate = txt_date.SelectedDate.Value;
            this.Close();
        }
    }
}
