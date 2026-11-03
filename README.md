# 🐾 GoToPet — Hệ Thống Quản Lý Trạm Cứu Hộ & Nhận Nuôi Thú Cưng

> **Chủ dự án:** [honganh-goto](https://github.com/honganh-goto)  
> **Nền tảng:** ASP.NET Core MVC (.NET 10) & Microsoft SQL Server 2022  
> **Kho mã nguồn:** [https://github.com/honganh-goto/gotopet](https://github.com/honganh-goto/gotopet)

---

## 📖 Giới thiệu Dự án

**GoToPet** là ứng dụng web full-stack phục vụ công tác quản lý trạm cứu hộ và kết nối nhận nuôi thú cưng. Dự án cung cấp giải pháp toàn diện cho cả cộng đồng yêu động vật lẫn đội ngũ nhân viên cứu hộ:

- **Dành cho cộng đồng & người nhận nuôi (Adopters):**
  - Khám phá danh sách thú cưng cần tìm mái ấm (chó, mèo, thỏ, chim,...).
  - Tra cứu chi tiết tiểu sử, tình trạng sức khỏe, lịch sử khám thú y và tiêm phòng.
  - Lưu danh sách thú cưng yêu thích và nộp hồ sơ đăng ký nhận nuôi trực tuyến.
- **Dành cho nhân viên trạm cứu hộ (Staff / Admins):**
  - Bảng điều khiển (Admin Dashboard) theo dõi chỉ số tổng quan.
  - Quản trị toàn bộ danh mục thú cưng, hồ sơ người nhận nuôi và quy trình duyệt hồ sơ.
  - Quản lý nhật ký khám thú y (Veterinary Visits) và lịch tiêm chủng (Vaccinations).
  - Báo cáo thống kê tình hình nhận nuôi và tình trạng sức khỏe đàn thú cưng.

---

## 🛠 Yêu cầu Hệ thống (Prerequisites)

Trước khi bắt đầu, máy tính của bạn cần cài đặt sẵn:

| Công cụ | Phiên bản khuyến nghị | Mục đích |
| :--- | :--- | :--- |
| **.NET SDK** | `.NET 10.0` trở lên | Biên dịch và chạy ứng dụng ASP.NET Core |
| **Docker Desktop** | Bản mới nhất | Khởi tạo nhanh SQL Server 2022 độc lập |
| *Hoặc:* **SQL Server** | `SQL Server 2022` (Developer/Express) | Cơ sở dữ liệu nội bộ (nếu không dùng Docker) |
| **Git** | Bản mới nhất | Quản lý mã nguồn |

> 💡 *Kiểm tra phiên bản .NET trên máy bằng lệnh:* `dotnet --version` *(kết quả hiển thị dạng `10.x.x`)*.

---

## 🚀 Hướng Dẫn Cài Đặt & Khởi Chạy

### Cách 1: Tải mã nguồn qua Git CLI

```bash
# 1. Clone kho mã nguồn về máy
git clone https://github.com/honganh-goto/gotopet.git

# 2. Di chuyển vào thư mục dự án
cd gotopet
```

### Cách 2: Tải qua GitHub Desktop

1. Mở **GitHub Desktop** $\rightarrow$ chọn **File** $\rightarrow$ **Clone Repository...**
2. Chuyển sang tab **URL** và nhập: `https://github.com/honganh-goto/gotopet.git`
3. Chọn thư mục lưu trên máy và nhấn **Clone**.

---

### Bước 1: Khởi động SQL Server bằng Docker

Chạy một container SQL Server 2022 mới với mật khẩu chuẩn đã đồng bộ với cấu hình dự án:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=GoToPet2026Db" \
  -p 1433:1433 --name gotopet-sql \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

*Đợi khoảng 5 – 10 giây để dịch vụ SQL Server khởi động hoàn tất.* Kiểm tra trạng thái bằng:
```bash
docker ps
```

> 📌 *Nếu container `gotopet-sql` đã được tạo từ trước, bạn chỉ cần khởi động lại:* `docker start gotopet-sql`.

---

### Bước 2: Khởi tạo Cơ sở dữ liệu & Nạp Dữ liệu Mẫu

Copy các file script SQL vào container và thực thi:

```bash
# 1. Sao chép các tệp script vào container
docker cp database/01_schema.sql gotopet-sql:/tmp/01_schema.sql
docker cp database/02_seed.sql   gotopet-sql:/tmp/02_seed.sql

# 2. Tạo cơ sở dữ liệu GoToPet, bảng, triggers và stored procedures
docker exec gotopet-sql /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P "GoToPet2026Db" -C -i /tmp/01_schema.sql

# 3. Nạp dữ liệu mẫu ban đầu (thú cưng, bệnh án mẫu)
docker exec gotopet-sql /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P "GoToPet2026Db" -C -i /tmp/02_seed.sql
```

> 💡 *Bạn cũng có thể mở trực tiếp 2 file script trên trong **SSMS** hoặc **Azure Data Studio** và chạy kết nối tới `localhost,1433`.*

---

### Bước 3: Kiểm tra Chuỗi Kết Nối

Mở tệp [GoToPet.Web/appsettings.json](file:///d:/GotoPet/GoToPet.Web/appsettings.json) và xác nhận thông số kết nối:

```json
"ConnectionStrings": {
  "GoToPet": "Server=localhost,1433;Database=GoToPet;User Id=sa;Password=GoToPet2026Db;TrustServerCertificate=True;"
}
```

---

### Bước 4: Chạy Ứng Dụng

Di chuyển vào thư mục web và khởi chạy:

```bash
cd GoToPet.Web
dotnet run
```

Ứng dụng sẽ hoạt động tại: **[http://localhost:5190](http://localhost:5190)**

---

## 📂 Cấu Trúc Thư Mục Dự Án

```
gotopet/
├── GoToPet.Web/                 # Ứng dụng chính ASP.NET Core MVC
│   ├── Areas/Admin/             # Phân hệ Quản trị (Dashboard, CRUD, Báo cáo)
│   │   ├── Controllers/         # Các controller nghiệp vụ của Admin
│   │   └── Views/               # Giao diện dành riêng cho Admin
│   ├── Controllers/             # Controllers dành cho cộng đồng & người nhận nuôi
│   ├── Data/                    # GoToPetContext (Entity Framework Core)
│   ├── Models/                  # Data Entities và ViewModels
│   ├── Views/                   # Razor Views công khai & Shared Layouts
│   ├── wwwroot/                 # Tài nguyên tĩnh (CSS, JS, Fonts, Uploads ảnh thú cưng)
│   ├── appsettings.json         # Cấu hình hệ thống & chuỗi kết nối Database
│   └── GoToPet.Web.csproj       # Tệp định nghĩa dự án .NET 10
├── database/
│   ├── 01_schema.sql            # Script tạo Database, 7 bảng, 5 triggers, 3 stored procedures
│   └── 02_seed.sql              # Dữ liệu mẫu ban đầu
├── SETUP-Windows.md             # Hướng dẫn thiết lập chi tiết trên môi trường Windows
├── README.md                    # Tài liệu hướng dẫn tổng quan dự án
└── GoToPet.sln                  # Visual Studio Solution File
```

---

## 🌐 Các Cổng Truy Cập & Tài Khoản Mặc Định

| Đường dẫn (URL) | Mô tả vai trò |
| :--- | :--- |
| **[http://localhost:5190](http://localhost:5190)** | Trang chủ — Danh sách thú cưng và thông tin trạm |
| **[http://localhost:5190/Account/Login](http://localhost:5190/Account/Login)** | Đăng nhập dành cho Người nhận nuôi (Member / Adopter) |
| **[http://localhost:5190/Account/Register](http://localhost:5190/Account/Register)** | Đăng ký tài khoản người nhận nuôi mới |
| **[http://localhost:5190/Admin/Login](http://localhost:5190/Admin/Login)** | Cổng đăng nhập dành riêng cho Quản trị viên (Staff / Admin) |

### 🔑 Thông tin Đăng nhập Admin Mặc định
* **Email:** `admin@gotopet.com` *(hoặc `admin@furever.com`)*
* **Mật khẩu (Password):** `admin123`

> [!NOTE]
> Hệ thống phân quyền sử dụng Cookie Authentication riêng biệt. Người dùng bình thường đăng ký tại `/Account/Register`; nhân viên quản lý đăng nhập tại `/Admin/Login`.

---

## ⚙️ Công Nghệ Sử Dụng (Tech Stack)

- **Backend Framework:** ASP.NET Core MVC (.NET 10)
- **Cơ sở dữ liệu:** Microsoft SQL Server 2022
- **ORM:** Entity Framework Core 10 (SQL Server Provider)
- **Bảo mật & Xác thực:**
  - Cookie Authentication (thời hạn 7 ngày, sliding expiration)
  - Mã hóa mật khẩu chuẩn BCrypt (`BCrypt.Net-Next`)
- **Frontend:** ASP.NET Core Razor Views, Semantic HTML5, CSS Custom Design-System, Bootstrap Icons / Glyphs

---

## 🗄 Kiến Trúc Cơ Sở Dữ Liệu

Toàn bộ tính toàn vẹn dữ liệu được đảm bảo nghiêm ngặt ngay tại tầng cơ sở dữ liệu thông qua ràng buộc (Constraints), Stored Procedures và Triggers:

- **7 Bảng dữ liệu chính:** `Pet`, `Adopter`, `Adoption`, `Veterinary_Visit`, `Vaccination`, `Favorite`, `Admin`.
- **5 Triggers tự động hóa:**
  - Quản lý luồng trạng thái thú cưng: `Available` $\rightarrow$ `Reserved` $\rightarrow$ `Adopted`.
  - Kiểm tra điều kiện trước khi nhận nuôi và tự động hủy thú cưng khỏi danh sách yêu thích khi đã có chủ mới.
- **3 Stored Procedures:**
  - Tra cứu danh sách thú cưng đang sẵn sàng nhận nuôi.
  - Thống kê tỷ lệ nhận nuôi theo tháng.
  - Cảnh báo danh sách các mũi tiêm phòng quá hạn cần xử lý.

---

## ❓ Xử Lý Sự Cố Thường Gặp (Troubleshooting)

### 1. Lỗi: *Cannot open database "GoToPet"... Login failed for user 'sa'*
- Nguyên nhân: Docker container chưa chạy hoặc chưa thực thi script nạp database.
- Khắc phục: Kiểm tra `docker ps`, nếu chưa nạp database hãy thực hiện lại **Bước 2**.

### 2. Lỗi: *Port already in use (5190 hoặc 1433)*
- Cổng `1433`: Kiểm tra xem có container cũ hoặc SQL Server cục bộ nào đang chiếm cổng không.
- Cổng `5190`: Tiến trình `GoToPet.Web` lần trước chưa tắt. Trên PowerShell, chạy lệnh sau để dừng:
  ```powershell
  Stop-Process -Name GoToPet.Web -Force
  ```

### 3. Lỗi: *`dotnet` command not found*
- Tải và cài đặt **.NET 10 SDK** từ trang chủ Microsoft, sau đó khởi động lại Terminal.

---

## 👤 Chủ Dự Án (Project Owner)

- **Tác giả:** **honganh-goto**
- **GitHub:** [@honganh-goto](https://github.com/honganh-goto)
- Mọi đóng góp (Pull Requests) và phản hồi (Issues) đều được hoan nghênh!
