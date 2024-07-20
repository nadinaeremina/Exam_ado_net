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
    /// Логика взаимодействия для Param_category.xaml
    /// </summary>
    public partial class Param_category : Window
    {
        public Param_category()
        {
            InitializeComponent();
        }

        public string MyString;

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (combo_choice_category.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите категорию!");
                return;
            }

            if(combo_choice_category.SelectedIndex == 0)
                MyString = "Спектакль";
            if (combo_choice_category.SelectedIndex == 1)
                MyString = "Концерт";
            if (combo_choice_category.SelectedIndex == 2)
                MyString = "Выставка";
            if (combo_choice_category.SelectedIndex == 3)
                MyString = "Цирк";
            if (combo_choice_category.SelectedIndex == 4)
                MyString = "Спорт";
            if (combo_choice_category.SelectedIndex == 5)
                MyString = "Семинары и тренинги";
            if (combo_choice_category.SelectedIndex == 6)
                MyString = "Кино";
            if (combo_choice_category.SelectedIndex == 7)
                MyString = "Юмор";
            if (combo_choice_category.SelectedIndex == 8)
                MyString = "Вечеринки";
            if (combo_choice_category.SelectedIndex == 9)
                MyString = "Детям";
            if (combo_choice_category.SelectedIndex == 10)
                MyString = "Другое";

            this.Close();
        }
    }
}
