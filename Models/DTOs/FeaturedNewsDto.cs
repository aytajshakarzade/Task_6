namespace Task_6.Models.DTOs
{
    public class FeaturedNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Short_Description { get; set; }
        public string Image_Url { get; set; }
        public DateTime Created_At { get; set; }
        public int Read_Time_Minutes { get; set; }
        public bool Is_Trending { get; set; }

        public object Category { get; set; }
        public object Author { get; set; }
        public object Stats { get; set; }
    }
}