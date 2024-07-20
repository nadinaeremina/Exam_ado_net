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
    /// Логика взаимодействия для Param_city.xaml
    /// </summary>
    public partial class Param_city : Window
    {
        public Param_city()
        {
            InitializeComponent();
        }

        public string MyString;

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (combo_choice_city.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите город!");
                return;
            }

            if (combo_choice_city.SelectedIndex == 0)
                MyString = "Москва";
            if (combo_choice_city.SelectedIndex == 1)
                MyString = "Санкт-Петербург";
            if (combo_choice_city.SelectedIndex == 2)
                MyString = "Астана";
            if (combo_choice_city.SelectedIndex == 3)
                MyString = "Алматы";

            this.Close();
        }
    }
}
