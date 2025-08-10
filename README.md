📚 BookStoreSolution — ASP.NET Core Web API + MVC Kitap Satış Uygulaması
BookStoreSolution, çevrim içi kitap satışı senaryosuna dayanan, ASP.NET Core tabanlı bir web uygulamasıdır. Proje iki ana bileşenden oluşur:

🧩 Proje Bileşenleri
1️⃣ BookStore.API — Web API Katmanı
Kitap, kategori, kullanıcı, sipariş ve favori verilerini yöneten RESTful servisler içerir.
- ✅ Entity Framework Core (Code First) ile SQL Server veri yönetimi
- ✅ Swagger UI ile API test imkânı
- ✅ DTO kullanımı ile veri alışverişinin sadeleştirilmesi
- ✅ CORS yapılandırması ile güvenli istemci erişimi
2️⃣ BookStore.MVC — Kullanıcı Arayüzü
Kitapları inceleme, sepete ekleme, favorilere alma ve satın alma işlemlerinin yapıldığı MVC tabanlı web arayüzüdür.
- ✅ ASP.NET Core MVC ile kullanıcı dostu arayüz
- ✅ Admin Paneli (Area) ile kategori/kitap CRUD ve görsel yükleme
- ✅ ASP.NET Core Identity ile kullanıcı yönetimi ve rol tabanlı erişim (Admin/User)
- ✅ Bootstrap ile responsive ve modern tasarım
- ✅ Session tabanlı sepet yönetimi, ViewBag / ViewData / TempData kullanımı
---
🎯 Projenin Amaçları ve İşlevleri
- 📖 Kitap listelerini görüntüleme ve detay sayfalarına erişim
- 🛒 Sepete ekleme ve favorilere alma
- 🗂 Kategoriye göre filtreleme
- 🛠 Admin paneli ile kitap/kategori CRUD işlemleri
- 🖼 Kitap görselleri yükleme desteği
- 🔗 REST API ile dış uygulamalara veri sunumu
- 🗃 SQL Server üzerinde EF Core ile veri yönetimi
---
✅ Gerekli Araçlar
- 💻 Visual Studio 2022 (ASP.NET workload yüklü)
- 🗄 SQL Server / SQL Express
- ⚙️ .NET 7/8 SDK (proje uyumlu)
- 📦 NuGet paketleri (otomatik indirilir
---
🔧 Kurulum Adımları
📌 Not: API ve MVC projelerini aynı anda çalıştırın (Multiple startup projects).

- Visual Studio’da çözümü açın
- API ve MVC projelerini başlangıç olarak ayarlayın
- Veritabanını EF Core ile oluşturun (Update-Database)
- Uygulamayı çalıştırın ve test edin

