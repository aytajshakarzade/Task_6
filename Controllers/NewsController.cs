using Microsoft.AspNetCore.Mvc;
using Task_6.Data;

namespace Task_6.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("featured")]
        public IActionResult GetFeatured()
        {
            try
            {
                var featured =
                    (from a in _context.Articles
                     join c in _context.Categories
                         on a.Category_Id equals c.Id
                     join au in _context.Authors
                         on a.Author_Id equals au.Id
                     join s in _context.ArticleStats
                         on a.Id equals s.Article_Id
                     where a.Is_Featured
                     orderby a.Created_At descending
                     select new
                     {
                         id = a.Id,
                         title = a.Title,
                         short_description = a.Short_Description,
                         image_url = a.Image_Url,
                         created_at = a.Created_At,
                         read_time_minutes = a.Read_Time_Minutes,
                         is_trending = a.Is_Trending,

                         category = new
                         {
                             id = c.Id,
                             name = c.Name,
                             slug = c.Slug,
                             badge_color = c.Badge_Color
                         },

                         author = new
                         {
                             id = au.Id,
                             name = au.Name
                         },

                         stats = new
                         {
                             likes_count = s.Likes_Count,
                             shares_count = s.Shares_Count,
                             bookmarks_count = s.Bookmarks_Count
                         }
                     })
                    .FirstOrDefault();

                return Ok(new
                {
                    success = true,
                    message = "OK",
                    data = featured
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }

        [HttpGet("breaking")]
        public IActionResult GetBreaking()
        {
            try
            {
                var breaking = _context.Articles
                    .Where(x => x.Is_Breaking)
                    .OrderByDescending(x => x.Created_At)
                    .Select(x => new
                    {
                        id = x.Id,
                        title = x.Title
                    })
                    .ToList();

                return Ok(new
                {
                    success = true,
                    message = "OK",
                    data = breaking
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }
        [HttpGet("articles")]
        public IActionResult GetArticles(
    string tab = "latest",
    int page = 1,
    int limit = 4)
        {
            try
            {
                var query =
                    from a in _context.Articles
                    join c in _context.Categories
                        on a.Category_Id equals c.Id
                    join au in _context.Authors
                        on a.Author_Id equals au.Id
                    join s in _context.ArticleStats
                        on a.Id equals s.Article_Id
                    select new
                    {
                        Article = a,
                        Category = c,
                        Author = au,
                        Stats = s
                    };

                if (tab.ToLower() == "think")
                {
                    query = query.Where(x =>
                        x.Category.Slug.ToLower() == "think");
                }
                else if (tab.ToLower() == "health")
                {
                    query = query.Where(x =>
                        x.Category.Slug.ToLower() == "health");
                }

                var totalItems = query.Count();

                var articles = query
                    .OrderByDescending(x => x.Article.Created_At)
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .Select(x => new
                    {
                        id = x.Article.Id,
                        title = x.Article.Title,
                        short_description =
                            x.Article.Short_Description,
                        image_url = x.Article.Image_Url,
                        created_at = x.Article.Created_At,
                        read_time_minutes =
                            x.Article.Read_Time_Minutes,

                        category = new
                        {
                            id = x.Category.Id,
                            name = x.Category.Name,
                            slug = x.Category.Slug,
                            badge_color =
                                x.Category.Badge_Color
                        },

                        author = new
                        {
                            id = x.Author.Id,
                            name = x.Author.Name
                        },

                        stats = new
                        {
                            likes_count =
                                x.Stats.Likes_Count,
                            shares_count =
                                x.Stats.Shares_Count,
                            bookmarks_count =
                                x.Stats.Bookmarks_Count
                        }
                    })
                    .ToList();

                return Ok(new
                {
                    success = true,
                    message = "OK",
                    data = articles,
                    pagination = new
                    {
                        current_page = page,
                        total_pages =
                            (int)Math.Ceiling(
                                (double)totalItems / limit),
                        total_items = totalItems,
                        limit = limit
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }

        [HttpGet("live")]
        public IActionResult GetLive()
        {
            try
            {
                var live =
                    (from a in _context.Articles
                     join c in _context.Categories
                        on a.Category_Id equals c.Id
                     where a.Is_Live
                     orderby a.Created_At descending
                     select new
                     {
                         id = a.Id,
                         title = a.Title,
                         image_url = a.Image_Url,

                         category = new
                         {
                             name = c.Name
                         }
                     })
                    .FirstOrDefault();

                return Ok(new
                {
                    success = true,
                    message = "OK",
                    data = live
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }
        [HttpGet("editors-picks")]
        public IActionResult GetEditorsPicks()
        {
            try
            {
                var picks =
                    (from a in _context.Articles
                     join c in _context.Categories
                        on a.Category_Id equals c.Id
                     join s in _context.ArticleStats
                        on a.Id equals s.Article_Id
                     where a.Is_Editors_Pick
                     orderby a.Created_At descending
                     select new
                     {
                         id = a.Id,
                         title = a.Title,
                         image_url = a.Image_Url,

                         category = new
                         {
                             id = c.Id,
                             name = c.Name,
                             badge_color = c.Badge_Color
                         },

                         stats = new
                         {
                             likes_count = s.Likes_Count,
                             shares_count = s.Shares_Count,
                             bookmarks_count = s.Bookmarks_Count
                         }
                     })
                    .ToList();

                return Ok(new
                {
                    success = true,
                    message = "OK",
                    data = picks
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var article =
                    (from a in _context.Articles
                     join c in _context.Categories
                        on a.Category_Id equals c.Id
                     join au in _context.Authors
                        on a.Author_Id equals au.Id
                     join s in _context.ArticleStats
                        on a.Id equals s.Article_Id
                     where a.Id == id
                     select new
                     {
                         id = a.Id,
                         title = a.Title,
                         short_description = a.Short_Description,
                         content = a.Content,
                         image_url = a.Image_Url,
                         created_at = a.Created_At,
                         read_time_minutes = a.Read_Time_Minutes,

                         category = new
                         {
                             id = c.Id,
                             name = c.Name,
                             slug = c.Slug,
                             badge_color = c.Badge_Color
                         },

                         author = new
                         {
                             id = au.Id,
                             name = au.Name
                         },

                         stats = new
                         {
                             likes_count = s.Likes_Count,
                             shares_count = s.Shares_Count,
                             bookmarks_count = s.Bookmarks_Count,
                             comments_count = s.Comments_Count
                         }
                     })
                    .FirstOrDefault();

                if (article == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Article not found",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "OK",
                    data = article
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message,
                    data = (object)null
                });dotnet ru
            }
        }
    }
}