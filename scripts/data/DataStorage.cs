using Hotels.scripts.rawData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.scripts.data
{
    /// <summary>
    /// Класс, реализующий интерфейс и обрабатывающий данные из файла
    /// </summary>
    internal class DataStorage : IData
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
                _buf.Add( new AllHotelsByRating()
                {
                    hotelName = Utils.GetNameItemById( item.name ),
                    rating = Utils.GetRatingItemById( item.name ),
                    distance = Utils.GetDistanceItemById( item.name ),
                    price = Utils.GetPriceItemById( item.name )
                } );
            }
            var sorted = from buf in _buf orderby buf.rating select buf;
            foreach (var item in sorted)
            {
                m_all_hotels.Add( new AllHotelsByRating()
                {
                    hotelName = Utils.GetNameItemById( item.hotelName ),
                    rating = Utils.GetRatingItemById( item.hotelName ),
                    distance = Utils.GetDistanceItemById( item.hotelName ),
                    price = Utils.GetPriceItemById( item.hotelName )
                } );
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
                    m_nearestHotels.Add( new NearestHotelByRatedAndPrice()
                    {
                        hotelName = Utils.GetNameItemById( item.name ),
                        distance = Utils.GetDistanceItemById( item.name ),
                        rating = Utils.GetRatingItemById( item.name ),
                        price = Utils.GetPriceItemById( item.name )
                    } );
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
                StreamReader sr = new StreamReader( datapath, Encoding.UTF8 );
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split( m_devider );
                    var item = new RawDataItem()
                    {
                        name = items[0].Trim(),
                        distance = Convert.ToDouble( items[1] ),
                        averageRating = Convert.ToDouble( items[2] ),
                        countFreeRooms = Convert.ToUInt32( items[3] ),
                        price = Convert.ToInt32( items[4] )
                    };
                    Hotel type = new Hotel()
                    {
                        name = item.name,
                        distance = item.distance,
                        rating = item.averageRating,
                        price = item.price
                    };
                    Utils.addStorageItem( item.name, type );
                    m_rawData.Add( item );
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
            if (d.InitData( path ))
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
