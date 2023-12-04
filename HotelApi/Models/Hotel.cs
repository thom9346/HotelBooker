namespace HotelApi.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Rating { get; set; }
        public bool IsAvailable { get; set; }

    }
}
