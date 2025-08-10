📌 Proje Tanımı

BookStoreSolution, kitap satışı yapan bir çevrim içi mağaza senaryosunu temel alan ASP.NET Core tabanlı bir web uygulamasıdır.
Proje iki ana bileşenden oluşur:
	1.	BookStore.API → Kitap, kategori, kullanıcı, sipariş ve favoriler gibi verilerin tutulduğu ve RESTful servisler aracılığıyla dış dünyaya sunulduğu Web API projesi.
	•	Entity Framework Core (Code First) ile SQL Server üzerinde veri tabanı yönetimi
	•	Swagger UI ile API test imkânı
	•	DTO kullanımı ile API veri alışverişinin kolaylaştırılması
	•	CORS yapılandırması ile MVC istemcisinin API’ye güvenli erişimi
	2.	BookStore.MVC → Kullanıcıların kitapları inceleyebileceği, sepete ekleyebileceği, favorilere alabileceği ve satın alma sürecini tamamlayabileceği MVC tabanlı web arayüzü.
	•	ASP.NET Core MVC ile geliştirilmiş kullanıcı dostu arayüz
	•	Admin Paneli (Area) ile yönetimsel işlemler: kategori ve kitap CRUD, görsel yükleme
	•	ASP.NET Core Identity ile kullanıcı yönetimi ve rol tabanlı erişim (Admin/User)
	•	Bootstrap ile responsive ve modern tasarım
	•	Session tabanlı sepet yönetimi, TempData/ViewBag/ViewData kullanımı

⸻

🎯 Projenin Amaçları ve İşlevleri
	•	Kullanıcılar kitap listelerine göz atabilir, detay sayfalarını görüntüleyebilir.
	•	Kitapları sepete ekleyebilir veya favorilere alabilir.
	•	Kategorilere göre filtreleme yapabilir.
	•	Admin paneli üzerinden kitap ve kategori ekleme, güncelleme, silme işlemleri yapılabilir.
	•	Kitaplar için görsel yükleme desteği vardır.
	•	REST API sayesinde başka uygulamalar da bu verileri kullanabilir.
	•	Tüm veri yönetimi Entity Framework Core aracılığıyla SQL Server üzerinde yapılır.# 📚 BookStoreSolution (API + MVC)

ASP.NET Core **Web API** + **MVC** mimarisiyle kitap satış vitrini.  
Ödev gereksinimleri:
- Code-First: **Kullanıcı**, **Kategori**, **Kitap**, **Sipariş**, **Favori**
- **REST API** (CRUD + Swagger)
- **MVC** istemci (Bootstrap, responsive, Cards)
- **Sepet**, **Favoriler** (Session + ViewBag + TempData)
- **Detay sayfaları** (ViewBag / ViewData / TempData kullanımı)
- **Admin Panel** (Role-based / Identity)
- Görsel **upload**, Admin’de **CRUD**
- CORS, DTO, temizlik ve hata yönetimi

---

## 🛠 Kullanılan Teknolojiler

- .NET 7/8, ASP.NET Core Web API & MVC
- Entity Framework Core (Code First, SQL Server)
- ASP.NET Core Identity (Cookie Auth, Roles)
- Bootstrap 5, Razor Views
- Swagger / Swashbuckle
- HttpClient + DTO + JSON (Newtonsoft.Json)
- Session (Cart/Fav UI), ViewBag/ViewData/TempData
- CORS

---

## ✅ Gerekli Araçlar

- **Visual Studio 2022** (ASP.NET workload)
- **SQL Server** / SQL Express
- .NET 7/8 SDK (projeye uyumlu)
- NuGet paketleri (VS indirir)

---

## 🔧 Kurulum (Sıfırdan Çalıştırma)

> **Not:** API ve MVC’yi **aynı anda** çalıştırın (Multiple startup projects).



