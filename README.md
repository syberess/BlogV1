# 📘 BlogV1 – ASP.NET Core MVC Blog Uygulaması

BlogV1, **ASP.NET Core MVC** ile geliştirilmiş, SEO uyumlu bir blog platformudur.  
Projede **Entity Framework Core**, **Identity**, **Admin Paneli**, **Yorum Sistemi**, **Arama Fonksiyonu** ve **Responsive Tasarım** bulunmaktadır.  

Bu proje, hem yazılım geliştirme pratiğini hem de gerçek bir blog sisteminde olması gereken temel özellikleri sergilemek için hazırlanmıştır.  

---

## 🚀 Özellikler

- ✅ Blog CRUD işlemleri (ekleme, güncelleme, silme, listeleme)  
- ✅ Yorum sistemi (yorum ekleme, yorum sayısı takibi)  
- ✅ SEO uyumlu meta etiketleri (dinamik Title, Description, Keywords)  
- ✅ Gösterge Paneli (LINQ ile istatistikler)  
- ✅ Kullanıcı kayıt/giriş sistemi (Identity ile)  
- ✅ Kategori ve etiket desteği  
- ✅ Arama fonksiyonu (başlık, açıklama, etiket üzerinden)  
- ✅ Responsive tasarım (Mobil, Tablet, Desktop uyumlu)  
- ✅ Admin panelinden kullanıcı, blog ve yorum yönetimi  

---

## 🖥️ Ekran Görüntüleri

### 🔹 Anasayfa
![Anasayfa](images/screenshots/Blog Sayfasi.png)

### 🔹 Blog Detay Sayfası
![Blog Detay](images/screenshots/Detay Sayfasi.png)

### 🔹 Yorumlar
![Yorumlar](images/screenshots/Blog Duzenleme Sayfasi.png)

### 🔹 Gösterge Paneli (Admin)
![Dashboard](images/screenshots/Admin Sayfasi.png)

### 🔹 Destek Sayfası
![Destek](images/screenshots/Destek Sayfasi.png)

### 🔹 Hakkımızda
![Hakkımızda](images/screenshots/Hakkimda Sayfasi.png)

### 🔹 İletişim
![İletişim](images/screenshots/Iletisim Sayfasi.png)

### 🔹 Kullanıcı Kayıt / Login
![Kullanıcı Kayıt](images/screenshots/Kullanici Kayit Sayfasi.png)
![Login](images/screenshots/Login Sayfasi.png)

### 🔹 Responsive Görünümler
![Masaüstü](images/screenshots/Masaustu Gorunum.png)
![Tablet](images/screenshots/Tablet Gorunum.png)
![Mobil](images/screenshots/Mobile Gorunum.png)


## 🧩 Örnek Kod Parçaları

### 1. Controller – Blog Detay ve SEO
```csharp
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
2. Model – Blog.cs
public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime PublishDate { get; set; }
    public string Tags { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public int ViewCount { get; set; }
    public int Status { get; set; }
}
3. View – Blog Detay Sayfası (Details.cshtml)
<h1>@Model.Name</h1>
<p>@Model.Description</p>
<p>Etiketler: <a href="#">@Model.Tags</a></p>

<h2>Yorumlar</h2>
@foreach(var item in ViewBag.Comments) {
    <div class="comment">
        <p><strong>@item.Name</strong> (@item.PublishDate):</p>
        <p>@item.Message</p>
    </div>
}

<h2>Yorum Yapın</h2>
<form action="/Blogs/CreateComment" method="post">
    <input type="hidden" name="BlogId" value="@Model.Id"/>
    <input type="text" name="Name" placeholder="İsim" />
    <input type="email" name="Email" placeholder="E-posta" />
    <textarea name="Message" placeholder="Yorumunuzu yazın..."></textarea>
    <button type="submit">Gönder</button>
</form>
4. Admin Dashboard (LINQ ile İstatistikler)
LINQ sorguları kullanılarak blog sistemi için özet istatistikler çıkarılmaktadır:

Toplam blog sayısı

Toplam görüntülenme

En çok görüntülenen blog

En son yayınlanan blog

En çok yorum alan blog

Bugün yapılan yorum sayısı

var model = new DashboardViewModel
{
    TotalBlogCount = _context.Blogs.Count(),
    TotalViewCount = _context.Blogs.Sum(b => b.ViewCount),
    MostViewedBlog = _context.Blogs.OrderByDescending(b => b.ViewCount).FirstOrDefault(),
    LatestBlog = _context.Blogs.OrderByDescending(b => b.PublishDate).FirstOrDefault(),
    MostCommentedBlog = _context.Blogs.OrderByDescending(b => b.CommentCount).FirstOrDefault(),
    TodayCommentCount = _context.Comments.Count(c => c.PublishDate.Date == DateTime.UtcNow.Date)
};

⚙️ Kurulum
Repoyu klonla:
git clone https://github.com/syberess/BlogV1

Proje dizinine gir:
cd BlogV1

Veritabanını güncelle:
dotnet ef database update

Projeyi çalıştır:
Proje varsayılan olarak https://localhost:7214 adresinde çalışacaktır.

📌 Kullanılan Teknolojiler
ASP.NET Core MVC

Entity Framework Core

Identity (Authentication & Authorization)

Bootstrap 5

PostgreSQL

LINQ

👩‍💻 Geliştirici
Esma Polat

🌐 LinkedIn https://www.linkedin.com/in/esma-polat-17a367234/

💻 GitHub https://github.com/syberess
