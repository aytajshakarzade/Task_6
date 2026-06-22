namespace Task_6.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Short_Description { get; set; }
        public string Content { get; set; }
        public string Image_Url { get; set; }

        public int Category_Id { get; set; }
        public int Author_Id { get; set; }

        public int Read_Time_Minutes { get; set; }

        public bool Is_Breaking { get; set; }
        public bool Is_Editors_Pick { get; set; }
        public bool Is_Featured { get; set; }
        public bool Is_Live { get; set; }
        public bool Is_Trending { get; set; }

        public DateTime Created_At { get; set; }
    }
}