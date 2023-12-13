using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.HotelRoom
{
    public class HotelRoomDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public HotelRoomType Type { get; set; }
        public int BaseCost { get; set; } //right now this is only here for visibility purposes in swagger. But its business logic handled in the repository, so I don't know if we can delete it?
    }
}
