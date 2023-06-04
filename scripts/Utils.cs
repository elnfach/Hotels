using Hotels.scripts.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.scripts
{
    /// <summary>
    /// Класс с публичными статическими функциями, хранящий отели в словаре
    /// </summary>
    internal class Utils
    {
        const int m_count = 25;
        private static Dictionary<String, Hotel> m_dict_nearest_hotels = new Dictionary<String, Hotel>( m_count );

        public static void addStorageItem(String id, Hotel myType)
        {
            if (m_dict_nearest_hotels.ContainsKey( id ) == false)
            {
                m_dict_nearest_hotels.Add( id, myType );
            }
        }
        public static String GetNameItemById(String id)
        {
            if (m_dict_nearest_hotels.ContainsKey( id ))
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
            if (m_dict_nearest_hotels.ContainsKey( id ))
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
            if (m_dict_nearest_hotels.ContainsKey( id ))
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
            if (m_dict_nearest_hotels.ContainsKey( id ))
            {
                return m_dict_nearest_hotels[id].price;
            }
            else
            {
                return 0;
            }
        }
    }
}
