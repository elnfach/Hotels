using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataStorage m_data = null;

        List<string> m_titles = new List<string>() { "Отель", "Расстояние",
                                                     "Средняя оценка", "Количество свободных комнат",
                                                     "Цена" };
        List<string> m_titles_sorted_by_price_and_rating = new List<string>()
        {
            "Отель", "Оценка",
            "Расстояние", "Цена"
        };
        List<string> m_tab_headers = new List<string>()
        {
            "Отели", "Отсортированные отели", "Сгруппированные отели"
        };
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            button1.Visible     = false;
            label1.Visible      = false;
            label2.Visible      = false;
            label3.Visible      = false;
            textBox1.Visible    = false;
            textBox2.Visible    = false;
            label1.Text         = "Мин. оценка: ";
            label2.Text         = "Макс. цена: ";
            label3.Text         = "";
            button1.Text        = "Поиск";
            button2.Text        = "Открыть файл";
            label3.ForeColor    = Color.Red;

            tabControl1.SelectedIndexChanged += new EventHandler( TabControl1_SelectedIndexChanged );
            for (var it = 0; it < m_tab_headers.Count; it++)
            {
                tabControl1.TabPages[it].Text = m_tab_headers[it];
            }
        }
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "sorted")
            {
                button1.Visible     = true;
                button2.Visible     = false;
                label1.Visible      = true;
                label2.Visible      = true;
                label3.Visible      = true;
                textBox1.Visible    = true;
                textBox2.Visible    = true;
            }
            else
            {
                button1.Visible     = false;
                button2.Visible     = true;
                label1.Visible      = false;
                label2.Visible      = false;
                label3.Visible      = false;
                textBox1.Visible    = false;
                textBox2.Visible    = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

/// <summary>
/// Вывод данных в dataGridView1 и в dataGridView3, прочитанные из файла
/// </summary>
/// <param name="path">Путь до файла формата .txt</param>
        private void ShowData(String path)
        {
            try
            {
                m_data = DataStorage.DataCreator(path);
                dataGridView1.DataSource = m_data.GetRawData();
                dataGridView1.ReadOnly = true;

                m_data.GetAllHotels();
                dataGridView3.DataSource = m_data.GetAllHotelsByPrice();
                dataGridView3.ReadOnly = true;

                for (var it = 0; it < m_titles_sorted_by_price_and_rating.Count; it++)
                {
                    dataGridView3.Columns[it].HeaderText = m_titles_sorted_by_price_and_rating[it];
                }
            }
            catch
            {
                MessageBox.Show("При запуске данных что-то сломалось");
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (rangeCheck())
                {
                    m_data.BuildNearestList( Convert.ToDouble( textBox1.Text ), Convert.ToInt32( textBox2.Text ) );
                    dataGridView2.DataSource = m_data.GetNearestHotelByRatedAndPrice();
                    dataGridView2.ReadOnly = true;
                    for (var it = 0; it < m_titles_sorted_by_price_and_rating.Count; it++)
                    {
                        dataGridView2.Columns[it].HeaderText = m_titles_sorted_by_price_and_rating[it];
                    }
                    label3.Text = "";
                }
                else
                {
                    label3.Text = "Введите оценку от 1 до 5!";
                }
            }
            catch
            {
                label3.Text = "Введены неправильные данные.";
            }
        }

        bool rangeCheck()
        {
            try
            {
                double a  = Convert.ToDouble(textBox1.Text);
                if (a > 0 && a <= 5)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false; 
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Отели_по_цене_и_по_дистранции_Click(object sender, EventArgs e)
        {

        }

/// <summary>
/// Функция кнопки "Открыть файл"
/// </summary>
/// <param name="sender">Объект отправитель</param>
/// <param name="e">Параметр, содержащий данные о событии</param>
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.ExecutablePath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ShowData( openFileDialog1.FileName );
                for (var it = 0; it < m_titles.Count; it++)
                {
                    dataGridView1.Columns[it].HeaderText = m_titles[it];
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
    interface IData
    {
        List<RawDataItem> GetRawData();
        List<AllHotelsByRating> GetAllHotelsByPrice();
        List<NearestHotelByRatedAndPrice> GetNearestHotelByRatedAndPrice();

    }

    class NearestHotelByRatedAndPrice
    {
        public String hotelName
        {
            get; set;
        }
        public double rating
        {
            get; set;
        }
        public double distance
        {
            get; set;
        }
        public int price
        {
            get; set;
        }
    }

    class AllHotelsByRating
    {
        public String hotelName
        {
            get; set;
        }
        public double rating
        {
            get; set;
        }
        public double distance
        {
            get; set;
        }
        public int price
        {
            get; set;
        }
    }

/// <summary>
/// Структура данных, хранящая имя, дистанцию, оценку и цену отелей
/// </summary>
    public struct Hotel
    {
        public String name;
        public double distance;
        public double rating;
        public int price;
    }

/// <summary>
/// Класс с публичными статическими функциями, хранящий отели в словаре
/// </summary>
    class Utils
    {
        const int m_count = 25;
        private static Dictionary<String, Hotel> m_dict_nearest_hotels = new Dictionary<String, Hotel>(m_count); 

        public static void addStorageItem(String id, Hotel myType)
        {
            if (m_dict_nearest_hotels.ContainsKey(id) == false)
            {
                m_dict_nearest_hotels.Add(id, myType);
            }
        }
        public static String GetNameItemById(String id)
        {
            if (m_dict_nearest_hotels.ContainsKey(id))
            {
                return m_dict_nearest_hotels[id].name;
            }
            else
            {
                return "Unknown";
            }
        }
        public static double GetDistanceItemById(String id)
        {
            if (m_dict_nearest_hotels.ContainsKey(id))
            {
                return m_dict_nearest_hotels[id].distance;
            }
            else
            {
                return 0;
            }
        }
        public static double GetRatingItemById(String id)
        {
            if (m_dict_nearest_hotels.ContainsKey(id))
            {
                return m_dict_nearest_hotels[id].rating;
            }
            else
            {
                return 0;
            }
        }
        public static int GetPriceItemById(String id)
        {
            if (m_dict_nearest_hotels.ContainsKey(id))
            {
                return m_dict_nearest_hotels[id].price;
            }
            else
            {
                return 0;
            }
        }
    }

/// <summary>
/// Класс RawDataItem, используемый как контейнер для данных иза файла
/// </summary>
    class RawDataItem
    {
        public String name
        {
            get; set;
        }
        public double distance
        {
            get; set;
        }
        public double averageRating
        {
            get; set;
        }
        public uint countFreeRooms
        {
            get; set;
        }
        public int price
        {
            get; set;
        }
    }

/// <summary>
/// Класс, реализующий интерфейс и обрабатывающий данные из файла
/// </summary>
    class DataStorage : IData
    {
        public bool IsReady
        {
            get
            {
                if (m_rawData == null)
                    return false;
                else
                    return true;
            }
        }

/// <summary>
/// Вывод данных сгруппированных отелей по оценке
/// </summary>
        public void GetAllHotels()
        {
            m_all_hotels = new List<AllHotelsByRating>();
            List<AllHotelsByRating> _buf = new List<AllHotelsByRating>();
            foreach (var item in m_rawData)
            {
                _buf.Add(new AllHotelsByRating()
                {
                    hotelName   = Utils.GetNameItemById(item.name),
                    rating      = Utils.GetRatingItemById( item.name ),
                    distance    = Utils.GetDistanceItemById(item.name),
                    price       = Utils.GetPriceItemById(item.name)
                });
            }
            var sorted = from buf in _buf orderby buf.rating select buf;
            foreach (var item in sorted)
            {
                m_all_hotels.Add(new AllHotelsByRating()
                {
                    hotelName   = Utils.GetNameItemById(item.hotelName),
                    rating      = Utils.GetRatingItemById( item.hotelName ),
                    distance    = Utils.GetDistanceItemById(item.hotelName),
                    price       = Utils.GetPriceItemById(item.hotelName)
                });
            }
        }

/// <summary>
/// Вывод данных отсортированных отелей по оценке и цене
/// </summary>
        public void BuildNearestList(double rating, int price)
        {
            m_nearestHotels = new List<NearestHotelByRatedAndPrice>();
            foreach (var item in m_rawData)
            {
                if (item.averageRating >= rating && item.price <= price)
                {
                    m_nearestHotels.Add(new NearestHotelByRatedAndPrice()
                    {
                        hotelName   = Utils.GetNameItemById(item.name),
                        distance    = Utils.GetDistanceItemById(item.name),
                        rating      = Utils.GetRatingItemById(item.name),
                        price       = Utils.GetPriceItemById(item.name)
                    });
                }
            }
        }

/// <summary>
/// Производит чтение данных из файла и сохраняет в m_rawData
/// </summary>
/// <param name="datapath">Путь до файла формата .txt</param>
/// /// <returns>
/// <para>Возвращает true, если чтение и сохранение произошли успешно</para>
/// <para>Возвращает false, если сохранить данные не удалось</para>
/// </returns>
        private bool InitData(String datapath)
        {
            m_rawData = new List<RawDataItem>();

            try
            {
                StreamReader sr = new StreamReader(datapath, Encoding.UTF8);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split( m_devider );
                    var item = new RawDataItem()
                    {
                        name            = items[0].Trim(),
                        distance        = Convert.ToDouble(items[1]),
                        averageRating   = Convert.ToDouble( items[2]),
                        countFreeRooms  = Convert.ToUInt32(items[3]),
                        price           = Convert.ToInt32(items[4])
                    };
                    Hotel type = new Hotel() 
                    { 
                        name        = item.name, 
                        distance    = item.distance, 
                        rating      = item.averageRating, 
                        price       = item.price 
                    };
                    Utils.addStorageItem(item.name, type);
                    m_rawData.Add(item);
                }
                sr.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static DataStorage DataCreator(String path)
        {
            DataStorage d = new DataStorage();
            if (d.InitData(path))
                return d;
            else
                return null;
        }

        public List<RawDataItem> GetRawData()
        {
            if (this.IsReady)
                return m_rawData;
            else
                return null;
        }

        public List<NearestHotelByRatedAndPrice> GetNearestHotelByRatedAndPrice()
        {
            if (this.IsReady)
                return m_nearestHotels;
            else
                return null;
        }

        public List<AllHotelsByRating> GetAllHotelsByPrice()
        {
            if (this.IsReady)
                return m_all_hotels;
            else
                return null;
        }

        private List<RawDataItem> m_rawData;
        private List<AllHotelsByRating> m_all_hotels;
        private List<NearestHotelByRatedAndPrice> m_nearestHotels;
        private readonly char m_devider = '%';
        private DataStorage()
        {
        }
    }
}
