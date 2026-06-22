namespace Task_6.Models
{
    public class ArticleStat
    {
        public int Id { get; set; }

        public int Article_Id { get; set; }

        public int Likes_Count { get; set; }
        public int Shares_Count { get; set; }
        public int Bookmarks_Count { get; set; }
        public int Comments_Count { get; set; }
    }
}