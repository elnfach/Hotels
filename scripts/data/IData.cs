using Hotels.scripts.rawData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotels.scripts.data
{
    internal interface IData
    {
        List<RawDataItem> GetRawData();
        List<AllHotelsByRating> GetAllHotelsByPrice();
        List<NearestHotelByRatedAndPrice> GetNearestHotelByRatedAndPrice();
    }
}
