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
    /// Логика взаимодействия для View_pic.xaml
    /// </summary>
    public partial class View_pic : Window
    {
        public View_pic()
        {
            InitializeComponent();
        }
        public int num_event; 
        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(number_event.Text) == -1)
            {
                MessageBox.Show("Укажите id события!");
                return;
            }

            num_event = Convert.ToInt32(number_event.Text);
            this.Close();
        }
    }
}
