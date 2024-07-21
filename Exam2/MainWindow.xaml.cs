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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Data.Linq;
using System.Runtime.CompilerServices;
using System.Data.Common;

namespace Exam2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // глобальные переменные
        DateTime myDate;
        bool flagg = false;
        int My_num_event;
        string fileName = "";
        int number_cat = -1;
        string myString = "";
        bool flag = false;
        string cs = "";
        SqlConnection conn = null;
        SqlCommand cmd = new SqlCommand();
        DataSet ds_s = new DataSet();
        DataTable dt_events = new DataTable();
        SqlDataAdapter events_adapter = new SqlDataAdapter();
        SqlCommandBuilder cmd_events = new SqlCommandBuilder();
        Param_date form_param = new Param_date();
        Param_category form_param_2 = new Param_category();
        Param_city form_param_3 = new Param_city();
        Param_time form_param_4 = new Param_time();
        View_pic new_wind = new View_pic();
        Add_client new_client = new Add_client();
        Form_buy buy_tick = new Form_buy();

        public MainWindow()
        {
            InitializeComponent();

            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["Events"].ConnectionString;
            conn.ConnectionString = cs;

            string connectionstring = ConfigurationManager.ConnectionStrings["Events"].ConnectionString;

            // работаем в присоединенном режиме - считываем таблицы
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                conn.Open();

                fill_data_set("dbo.move_to_archive", "table_dbo.move_to_archive");

                fill_data_set("dbo.actual_events_full_sold", "table_actual_events_full_sold");
                fill_data_set("dbo.actual_events_top3", "table_actual_events_top3");
                fill_data_set("dbo.popular_events_cat_top_3", "table_popular_events_cat_top_3");
                fill_data_set("dbo.active_client", "table_active_client");
                fill_data_set("dbo.not_popular_cat", "table_not_popular_cat");
                fill_data_set("dbo.top_3_for_5_days", "table_top_3_for_5_days");
                fill_data_set("dbo.today_cities", "table_today_cities");

                fill_data_set("dbo.show_cities", "table_show_cities");
                fill_data_set("dbo.show_countries", "table_show_countries");
                fill_data_set("dbo.show_categories_events", "table_show_categories_events");
                fill_data_set("dbo.show_title_events", "table_show_title_events");
                fill_data_set("dbo.show_locations", "table_show_locations");
                fill_data_set("dbo.show_clients", "table_show_clients");
                fill_data_set("dbo.show_events_contents", "table_show_events_contents");
                fill_data_set("dbo.show_buy_tickets", "table_show_buy_tickets");
                fill_data_set("dbo.show_archive_events", "table_dbo.move_to_archive");
            }
        }

        // создаем команду, передавая в нее хранимку, создаем адаптер, коммандбилдер, из адаптеру в датасет передаем информацию в соданнную таблицу
        private void fill_data_set(string prosedure_name, string table_name, string str_param = "", object param = null)
        {
            cmd = new SqlCommand(prosedure_name, conn);
            events_adapter = new SqlDataAdapter();
            fill_adapter(cmd, str_param, param);
            cmd_events = new SqlCommandBuilder(events_adapter);
            events_adapter.Fill(ds_s, table_name);
        }

        // заполняем адаптер характеристиками и передаем параметр, если таковой есть
        private void fill_adapter(SqlCommand command, string str_param = "", object param = null)
        {
            events_adapter.InsertCommand = command;
            events_adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
            events_adapter.SelectCommand = events_adapter.InsertCommand;

            if (str_param != "")
                command.Parameters.AddWithValue(str_param, param);
        }

        // заполняем дата грид информацией из определенной таблицы
        private void fill_data_grid(string table_name)
        {
            dt_events = ds_s.Tables[table_name];
            Data_grid.DataContext = null;
            Data_grid.DataContext = ds_s.Tables[table_name];

            if (Data_grid.Items.Count == 1)
                System.Windows.MessageBox.Show("Таблица пустая или были введены некорректные данные!");
        }

        // заполняем датагрид в соответствии с выбранным айтемом из комбобокса
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hiding_buttons();

            if (combo_box.SelectedItem == actual_events_100percent)
                fill_data_grid("table_actual_events_full_sold");
            else if (combo_box.SelectedItem == actual_events_top3)
                fill_data_grid("table_actual_events_top3");
            else if (combo_box.SelectedItem == top3_events_category)
                fill_data_grid("table_popular_events_cat_top_3");
            else if (combo_box.SelectedItem == active_client)
                fill_data_grid("table_active_client");
            else if (combo_box.SelectedItem == notpopular_category)
                fill_data_grid("table_not_popular_cat");
            else if (combo_box.SelectedItem == top3_popular_5days)
                fill_data_grid("table_top_3_for_5_days");
            else if (combo_box.SelectedItem == cities_ontoday)
                fill_data_grid("table_today_cities");
            else
            {
                // при выборе других айтемов необходимо создание дополнительной формы для ввода параметров 
                btn_show.Visibility = Visibility.Visible;

                if (combo_box.SelectedIndex == 0)
                {
                    dt_events.Clear();
                    form_param = new Param_date();
                    form_param.ShowDialog();
                }
                else if (combo_box.SelectedIndex == 1)
                {
                    dt_events.Clear();
                    form_param_2 = new Param_category();
                    form_param_2.ShowDialog();
                }
                else if (combo_box.SelectedIndex == 5)
                {
                    dt_events.Clear();
                    form_param_3 = new Param_city();
                    form_param_3.ShowDialog();
                }
                else if (combo_box.SelectedIndex == 9)
                {
                    dt_events.Clear();
                    form_param_4 = new Param_time();
                    form_param_4.ShowDialog();
                }

                flag = !flag;
            }
        }

        // кнопка для заполнения датагрида после ввода параметров
        private void btn_show_Click(object sender, RoutedEventArgs e)
        {
            Data_grid.DataContext = null;

            if (combo_box.SelectedItem == actual_events_ondate)
            {
                myDate = form_param.MyDate;
                fill_data_set("dbo.actual_events_specific_date", "table_actual_events_specific_date", "@data", myDate);
                fill_data_grid("table_actual_events_specific_date");
            }
            else if (combo_box.SelectedItem == actual_events_oncategory)
            {
                myString = form_param_2.MyString;
                fill_data_set("dbo.actual_events_specific_category", "table_events_specific_category", "@category", myString);
                fill_data_grid("table_events_specific_category");
            }
            else if (combo_box.SelectedItem == popular_event_oncity)
            {
                myString = form_param_3.MyString;
                fill_data_set("dbo.popular_event_from_city", "table_popular_event_from_city", "@city", myString);
                fill_data_grid("table_popular_event_from_city");
            }
            else if (combo_box.SelectedItem == events_ontoday)
            {
                myDate = form_param_4.MyTime;
                fill_data_set("dbo.events_specific_time", "table_events_specific_time", "@datetime", myDate);
                fill_data_grid("table_events_specific_time");
            }
        }

        //////////////////////////////////// правая панель для вывода всех таблиц нашей БД
        private void btn_countries_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            fill_data_grid("table_show_countries");
        }

        private void btn_cities_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            fill_data_grid("table_show_cities");
        }

        private void btn_cat_events_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            fill_data_grid("table_show_categories_events");
        }

        private void btn_title_events_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            fill_data_grid("table_show_title_events");
        }

        private void btn_locations_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            fill_data_grid("table_show_locations");
        }

        private void btn_info_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            fill_data_grid("table_show_events_contents");
            btn_pic_event.Visibility = Visibility.Visible;
            add_event.Visibility = Visibility.Visible;
        }

        private void btn_clients_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            fill_data_grid("table_show_clients");
            add_client.Visibility = Visibility.Visible;
        }

        private void btn_archive_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            fill_data_grid("table_dbo.move_to_archive");
        }

        private void btn_buy_tickets_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            fill_data_grid("table_show_buy_tickets");
            buy_ticket.Visibility = Visibility.Visible;
        }

        private void btn_picturess_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();
            combo_choice_category.Visibility = Visibility.Visible;
            text_choice_category.Visibility = Visibility.Visible;
        }

        // сокрытие ненужных кнопок
        private void hiding_buttons()
        {
            btn_1_pic.Visibility = Visibility.Hidden;
            combo_choice_category.Visibility = Visibility.Hidden;
            text_choice_category.Visibility = Visibility.Hidden;
            add_client.Visibility = Visibility.Hidden;
            btn_pic_event.Visibility = Visibility.Hidden;
            btn_show.Visibility = Visibility.Hidden;
            my_image.Source = null;
            add_event.Visibility = Visibility.Hidden;
            btn_update_client.Visibility = Visibility.Hidden;
            buy_ticket.Visibility = Visibility.Hidden;
            btn_update_tickets.Visibility = Visibility.Hidden;
        }

        // работа с опенфайл - выбор картинки для события
        private void choice_category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hiding_buttons();

            var ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Графические файлы |*.bmp; *.png; *.gif; *.jpg";
            ofd.FileName = "";
            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                fileName = ofd.FileName;
                LoadPicture();
            }

            btn_1_pic.Visibility = Visibility.Visible;
        }

        // загрузка картинки в событие - заполнение таблицы - 'Pictures'
        private void LoadPicture()
        {
            try
            {
                byte[] bytes = CreateCopy();

                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into dbo.Pictures(Title, Image_," +
                    " event_Category_Id) values (@title, @picture, @event_category_id);", conn);

                int index = -1;

                if (combo_choice_category.SelectedItem == null) return;

                index = combo_choice_category.SelectedIndex;
                index++;

                if (index == -1) return;

                cmd.Parameters.Add("@title", SqlDbType.NVarChar, 255).Value = fileName;
                cmd.Parameters.Add("@picture", SqlDbType.Image, bytes.Length).Value = bytes;
                cmd.Parameters.Add("@event_category_id", SqlDbType.Int, 255).Value = index;

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("update_pictures", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@picture", bytes);
                cmd.Parameters.AddWithValue("@category", index);

                cmd.ExecuteNonQuery();

                number_cat = index;

                conn.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private byte[] CreateCopy()
        {
            // это часть для того, чтобы картинки меньше веcили
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);

            int maxWidth = 300, maxHeight = 300;

            double rationX = (double)maxWidth / img.Width;
            double rationY = (double)maxHeight / img.Height;

            double ratio = Math.Min(rationX, rationY);

            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);

            // обязательная часть
            System.Drawing.Image im = new Bitmap(newWidth, newHeight); //наша картинка по новым размерам
            Graphics g = Graphics.FromImage(im);
            g.DrawImage(img, 0, 0, newWidth, newHeight); // прорисовываем нашу картинку
            MemoryStream ms = new MemoryStream();
            im.Save(ms, ImageFormat.Jpeg);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(ms);
            byte[] buf = br.ReadBytes((int)ms.Length);

            return buf;
        }

        private void btn_1_pic_Click(object sender, RoutedEventArgs e)
        {
            hiding_buttons();

            try
            {
                if (number_cat == -1)
                {
                    System.Windows.MessageBox.Show("Укажите id события!");
                    return;
                }

                int index = -1;
                index = number_cat;

                if (index == -1)
                {
                    System.Windows.MessageBox.Show("Укажите id картинки в правильном формате");
                    return;
                }

                events_adapter = new SqlDataAdapter("select Image_ from dbo.Pictures where Event_category_id=@id", conn);
                cmd_events = new SqlCommandBuilder(events_adapter);
                events_adapter.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = index;
                DataSet ds_s_2 = new DataSet();
                events_adapter.Fill(ds_s_2, "table_Picture");
                byte[] bytes = (byte[])ds_s_2.Tables[0].Rows[0]["Image_"];

                MemoryStream stream = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage returnImage = new BitmapImage();
                returnImage.BeginInit();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                returnImage.StreamSource = ms;
                returnImage.EndInit();

                my_image.Source = returnImage;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        // отображение картинки выбранного пользователем события
        private void btn_pic_event_Click_1(object sender, RoutedEventArgs e)
        {
            if (!flagg)
            {
                View_pic new_wind = new View_pic();
                new_wind.ShowDialog();
                My_num_event = new_wind.num_event;
                flagg = true;
            }
            else
            {
                try
                {
                    int index = -1;
                    index = My_num_event;

                    conn.Open();
                    events_adapter.Update(ds_s, "table_show_events_contents");
                    Data_grid.DataContext = ds_s.Tables["table_show_events_contents"];
                    conn.Close();

                    events_adapter = new SqlDataAdapter("select Image_ from dbo.Events_contents where Id=@id", conn);
                    cmd_events = new SqlCommandBuilder(events_adapter);
                    events_adapter.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = index;
                    DataSet ds_s_2 = new DataSet();
                    events_adapter.Fill(ds_s_2, "table_Picture2");

                    byte[] bytes = (byte[])ds_s_2.Tables[0].Rows[0]["Image_"];

                    MemoryStream stream = new MemoryStream(bytes);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                    BitmapImage returnImage = new BitmapImage();
                    returnImage.BeginInit();

                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);
                    returnImage.StreamSource = ms;
                    returnImage.EndInit();

                    my_image.Source = returnImage;

                    flagg = false;
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Отсутствует событие или картинка!");
                    my_image.Source = null;
                    flagg = false;
                }
            }
        }

        // добавить клиента
        private void add_client_Click(object sender, RoutedEventArgs e)
        {
            btn_update_client.Visibility = Visibility.Visible;
            Add_client new_client = new Add_client();
            new_client.ShowDialog();
        }

        // добавить событие
        private void add_event_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                events_adapter.Update(ds_s, "table_show_events_contents");
            }
        }

        // покупка билета
        private void buy_ticket_Click(object sender, RoutedEventArgs e)
        {
            btn_update_tickets.Visibility = Visibility.Visible;
            Form_buy new_tick = new Form_buy();
            new_tick.ShowDialog();
        }

        // подключиться к БД и обновить данные по клиентам
        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            // чтобы работать с таблицами в БД как с источником Ling - нам нужно создать класс 'DataContext'
            using (DataContext db = new DataContext(conn))
            {
                Table<Client> clients = db.GetTable<Client>();

                Client client_new = new Client
                {
                    First_name = Add_client.Client.First_name,
                    Last_name = Add_client.Client.Last_name,
                    Middle_name = Add_client.Client.Middle_name,
                    Birthday = Add_client.Client.Birthday,
                    Email = Add_client.Client.Email
                };

                clients.InsertOnSubmit(client_new);
                db.SubmitChanges();

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    fill_data_set("dbo.show_clients", "table_show_clients");
                    fill_data_grid("table_show_clients");
                }
            }
        }

        // подключиться к БД и проверить информацию по билетам
        private void btn_update_tickets_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext(conn))
            {
                Table<Buy_ticket> new_buy_ticket = db.GetTable<Buy_ticket>();

                Buy_ticket new_buy = new Buy_ticket()
                {
                    Value = Form_buy.Buy_ticket.Value,
                    date_of_bought = Form_buy.Buy_ticket.date_of_bought,
                    client_Id = Form_buy.Buy_ticket.client_id,
                    event_name_ID = Form_buy.Buy_ticket.event_name_id
                };

                int max_tick = 0, buy_tick_int = 0, min_age = 0, client_age = 0, month = -1;
                DateTime birth, today = DateTime.Now;

                conn.Open(); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                // находим событие и получаем информацию о нем (купленные билеты+всего билетов)
                cmd = new SqlCommand("dbo.search_event", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", new_buy.event_name_ID); // нужно быть уверенным, что второй пар-р нужного типа

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    max_tick = (int)dr[0];
                    buy_tick_int = (int)dr[1];
                    min_age = (int)dr[2];
                }

                dr.Close();

                // находим клиента и получаем информацию о нем (возраст)
                cmd = new SqlCommand("dbo.search_client", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", new_buy.client_Id); // нужно быть уверенным, что второй пар-р нужного типа

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    birth = (DateTime)dr[0];
                    month = today.Month - birth.Month;
                    client_age = today.Year - birth.Year;

                    if (month < 0)
                        client_age--;
                    if (month == 0)
                    {
                        if (today.Day < birth.Day)
                            client_age--;
                    }
                }

                dr.Close();

                if (max_tick > buy_tick_int && client_age >= min_age)
                {
                    new_buy_ticket.InsertOnSubmit(new_buy);
                    db.SubmitChanges();

                    fill_data_set("dbo.show_buy_tickets", "table_show_buy_tickets");
                    fill_data_grid("table_show_buy_tickets");

                    System.Windows.MessageBox.Show("Покупка совершена!");
                }
                else
                {
                    if (max_tick <= buy_tick_int)
                        System.Windows.MessageBox.Show("К сожалению, билетов не осталось!");
                    else
                        System.Windows.MessageBox.Show("К сожалению, возраст не подходит!");
                }

                conn.Close(); // почему-то при работе с этим событием  возникала ошибка - подключение не было закрыто в месте, которое помечено '!!!'
            }
        }
    }
}

