using Hotels.scripts;
using Hotels.scripts.data;
using Hotels.scripts.rawData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotels
{
    public partial class Hotels : Form
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
        public Hotels()
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
            double a = Convert.ToDouble( textBox1.Text );
            if (a > 0 && a <= 5)
            {
                return true;
            }
            return false;
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
}
