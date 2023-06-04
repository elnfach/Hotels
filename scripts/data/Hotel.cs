using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.scripts.data
{
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
}
