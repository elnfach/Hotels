using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.scripts.data
{
    internal class NearestHotelByRatedAndPrice
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
}
