using BlogV1.Context;
using BlogV1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogV1.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogDbContext _context;

        public BlogsController(BlogDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var blogs = _context.Blogs.Where(x => x.Status == 1).ToList();

            ViewData["Title"] = "Blog Yazıları - MyBlog";
            ViewData["Description"] = "Kullanıcıların paylaştığı güncel blog yazıları. Teknoloji, yazılım ve güncel konular hakkında makaleler.";
            ViewData["Keywords"] = "blog, yazı, makale, teknoloji, yazılım";

            return View(blogs);
        }


        public IActionResult Details(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog == null) return NotFound();

            blog.ViewCount++;
            _context.SaveChanges();

            var comments = _context.Comments.Where(x => x.BlogId == id).ToList();
            ViewBag.Comments = comments;

            // SEO için dinamik değerler
            ViewData["Title"] = blog.Name + " - MyBlog";
            ViewData["Description"] = !string.IsNullOrEmpty(blog.Description)
                ? blog.Description
                : (blog.Name + " hakkında detaylı blog yazısı.");
            ViewData["Keywords"] = blog.Tags;

            return View(blog);
        }

        [HttpPost]
        public IActionResult CreateComment(Comment model)
        {
            model.PublishDate = DateTimeOffset.UtcNow;
            _context.Comments.Add(model);
            var blog = _context.Blogs.Where(x=> x.Id == model.BlogId).FirstOrDefault();
            blog.CommentCount += 1;
            _context.SaveChanges();
            return RedirectToAction("Details",new { id = model.BlogId });
        }

        public IActionResult About()
        {
            ViewData["Title"] = "Hakkında - MyBlog";
            ViewData["Description"] = "MyBlog hakkında bilgi alın. Projenin amacı ve geliştirici Esma Polat hakkında detaylar.";
            return View();
        }


        //bunu unuttum
        public IActionResult Contact()
        {
            ViewData["Title"] = "İletişim - MyBlog";
            ViewData["Description"] = "Bize ulaşmak için iletişim formunu doldurun. Sorularınız ve görüşleriniz bizim için önemli.";
            return View();
        }


        [HttpPost]
        public IActionResult CreateContact(Contact model)
        {
            model.PublishDate = DateTimeOffset.UtcNow;
            _context.Contacts.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Support()
        {
            ViewData["Title"] = "Destek - MyBlog";
            ViewData["Description"] = "MyBlog kullanıcıları için destek ve yardım sayfası.";
            return View();
        }

        public IActionResult Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return RedirectToAction("Index");
            }

            var results = _context.Blogs
                .Where(b => b.Status == 1 &&
                            (b.Name.Contains(q) || b.Description.Contains(q) || b.Tags.Contains(q)))
                .OrderByDescending(b => b.PublishDate)
                .ToList();

            ViewData["Title"] = $"Arama Sonuçları - {q}";
            ViewData["Description"] = $"{q} ile ilgili blog yazıları listeleniyor.";
            return View("Index", results); // Index.cshtml'i kullanıyoruz
        }




    }
}
