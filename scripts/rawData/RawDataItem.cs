using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.scripts.rawData
{
    /// <summary>
    /// Класс RawDataItem, используемый как контейнер для данных из файла
    /// </summary>
    internal class RawDataItem
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
}
