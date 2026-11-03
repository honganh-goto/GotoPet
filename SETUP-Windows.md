# 🐾 GoToPet — Hướng Dẫn Cài Đặt Trên Windows (Không Dùng Docker)

> **Chủ dự án:** [honganh-goto](https://github.com/honganh-goto)  
> **Nền tảng:** Windows 10/11, .NET 10 SDK, Microsoft SQL Server 2022  
> **Kho mã nguồn:** [https://github.com/honganh-goto/gotopet](https://github.com/honganh-goto/gotopet)

Tài liệu này hướng dẫn bạn cài đặt và chạy dự án **GoToPet** trên môi trường **Windows** sử dụng **SQL Server cài đặt trực tiếp trên máy (Native)** — thích hợp nếu bạn không muốn hoặc không thể sử dụng Docker Desktop.

> ⏱️ **Lần đầu tiếp cận?** Chỉ cần thực hiện lần lượt từ **Bước 1 $\rightarrow$ Bước 5**, bạn sẽ hoàn tất chỉ trong vài phút.

---

## ✅ Chuẩn Bị Công Cụ (Cài đặt một lần)

Hãy đảm bảo máy tính của bạn đã cài đặt các phần mềm sau:

| Công cụ | Mục đích | Liên kết tải về |
| :--- | :--- | :--- |
| **Visual Studio 2022** (17.14+) hoặc **2026** | Môi trường lập trình (IDE) để mở và chạy mã nguồn | [visualstudio.microsoft.com](https://visualstudio.microsoft.com/) |
| **.NET 10 SDK** | Bộ phát triển ứng dụng nền tảng `net10.0` | [dotnet.microsoft.com/download/dotnet/10.0](https://dotnet.microsoft.com/download/dotnet/10.0) |
| **SQL Server 2022** (Express hoặc Developer) | Hệ quản trị cơ sở dữ liệu (miễn phí) | [microsoft.com/sql-server-downloads](https://www.microsoft.com/sql-server/sql-server-downloads) |
| **SSMS** hoặc **Azure Data Studio** | Công cụ giao diện đồ họa để thực thi script SQL | [Tải SSMS tại Microsoft Learn](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) |
| **Git** hoặc **GitHub Desktop** | Công cụ tải và đồng bộ mã nguồn | [git-scm.com](https://git-scm.com/) |

> 💡 *Kiểm tra .NET đã sẵn sàng:* Mở Terminal/PowerShell và gõ `dotnet --version` $\rightarrow$ kết quả hiển thị dạng `10.x.x`.

---

## 1️⃣ Tải Mã Nguồn Về Máy

### Cách 1: Thao tác qua Visual Studio
1. Mở Visual Studio $\rightarrow$ chọn **Clone a repository**.
2. Nhập URL kho mã nguồn: `https://github.com/honganh-goto/gotopet.git`
3. Chọn thư mục lưu trữ trên máy $\rightarrow$ nhấn **Clone**.

### Cách 2: Sử dụng dòng lệnh (PowerShell / Command Prompt)
```powershell
git clone https://github.com/honganh-goto/gotopet.git
cd gotopet
```

> 📌 *Nếu đã có sẵn mã nguồn:* Mở Visual Studio $\rightarrow$ chọn **File $\rightarrow$ Open $\rightarrow$ Project/Solution** và chọn tệp [GoToPet.sln](file:///d:/GotoPet/GoToPet.sln).

---

## 2️⃣ Cài Đặt & Khởi Chạy Dịch Vụ SQL Server

1. Chạy trình cài đặt **SQL Server 2022** $\rightarrow$ chọn kiểu cài đặt **Basic**.
2. Sau khi cài đặt xong, hãy chú ý **Instance Name** (Tên phiên bản) hiển thị trên màn hình:
   - Bản **SQL Server Express:** thường có tên là `SQLEXPRESS` (đường dẫn máy chủ dạng: `localhost\SQLEXPRESS`).
   - Bản **SQL Server Developer:** thường là phiên bản mặc định không có hậu tố (đường dẫn máy chủ là: `localhost`).
3. Dịch vụ SQL Server sẽ tự động chạy ngầm dưới dạng một Windows Service.

> 🔍 *Kiểm tra trạng thái:* Nhấn tổ hợp `Win + R`, gõ `services.msc`, tìm dịch vụ **SQL Server (SQLEXPRESS)** hoặc **SQL Server (MSSQLSERVER)** và đảm bảo trạng thái là **Running**.

---

## 3️⃣ Khởi Tạo Cơ Sở Dữ Liệu Bằng SSMS

1. Khởi động công cụ **SQL Server Management Studio (SSMS)** và kết nối:
   - **Server name:** `localhost\SQLEXPRESS` *(đối với bản Express)* hoặc `localhost` *(đối với bản Developer)*.
   - **Authentication:** Chọn **Windows Authentication** *(đăng nhập tự động bằng tài khoản Windows của bạn)*.
   - Nhấn **Connect**.
2. Trên thanh menu SSMS, chọn **File $\rightarrow$ Open $\rightarrow$ File...** $\rightarrow$ điều hướng mở tệp [database/01_schema.sql](file:///d:/GotoPet/database/01_schema.sql) $\rightarrow$ nhấn nút **Execute ▶️** *(hoặc phím `F5`)*.
   *(Lệnh này tạo database `GoToPet`, các bảng, trigger và stored procedure)*.
3. Tiếp tục mở tệp [database/02_seed.sql](file:///d:/GotoPet/database/02_seed.sql) $\rightarrow$ nhấn nút **Execute ▶️** *(hoặc `F5`)*.
   *(Lệnh này nạp 10 thú cưng mẫu và nhật ký khám thú y ban đầu)*.

> [!WARNING]
> **Không bỏ qua bước Execute:** Bạn bắt buộc phải nhấn nút **Execute** chạy file `01_schema.sql` trước, sau đó mới chạy file `02_seed.sql`.

---

## 4️⃣ Cấu Hình Chuỗi Kết Nối Trong `appsettings.json`

Mở tệp [GoToPet.Web/appsettings.json](file:///d:/GotoPet/GoToPet.Web/appsettings.json) và cập nhật chuỗi kết nối `GoToPet` tương ứng với phiên bản SQL Server bạn đang dùng:

### Dành cho SQL Server Express:
```json
"ConnectionStrings": {
  "GoToPet": "Server=localhost\\SQLEXPRESS;Database=GoToPet;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Dành cho SQL Server Developer / Default Instance:
```json
"ConnectionStrings": {
  "GoToPet": "Server=localhost;Database=GoToPet;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> 💡 **Một số lưu ý quan trọng:**
> - `Trusted_Connection=True;` cho phép đăng nhập trực tiếp qua quyền Windows của bạn mà **không cần ghi mật khẩu**.
> - Trong tệp JSON, ký tự dấu gạch chéo ngược phải được nhân đôi: `\\` (ví dụ: `localhost\\SQLEXPRESS`).
> - Giá trị của `Server=` phải trùng khớp chính xác với tên máy chủ bạn đã dùng để kết nối trong SSMS ở **Bước 3**.

---

## 5️⃣ Khởi Chạy Ứng Dụng

### Lựa chọn 1: Chạy trực tiếp từ Visual Studio
1. Nhìn lên thanh công cụ trên cùng, chọn dự án khởi động là **GoToPet.Web**.
2. Nhấn phím `F5` *(Debug)* hoặc `Ctrl + F5` *(Start Without Debugging)*.
3. Trình duyệt mặc định sẽ tự động mở trang web.

### Lựa chọn 2: Chạy từ dòng lệnh Terminal / PowerShell
Tại thư mục gốc của dự án:
```powershell
cd GoToPet.Web
dotnet run
```

Ứng dụng sẽ khởi chạy tại địa chỉ: **[http://localhost:5190](http://localhost:5190)** 🎉

---

## 🔑 Các Cổng Truy Cập & Tài Khoản Đăng Nhập

| Đường dẫn (URL) | Mục đích sử dụng |
| :--- | :--- |
| **[http://localhost:5190](http://localhost:5190)** | Trang chủ cộng đồng — Khám phá thú cưng |
| **[http://localhost:5190/Account/Register](http://localhost:5190/Account/Register)** | Đăng ký tài khoản người nhận nuôi mới |
| **[http://localhost:5190/Account/Login](http://localhost:5190/Account/Login)** | Đăng nhập tài khoản người nhận nuôi |
| **[http://localhost:5190/Admin/Login](http://localhost:5190/Admin/Login)** | Cổng đăng nhập dành riêng cho Quản trị viên (Admin) |

### 👤 Tài khoản Admin Quản Trị Mặc Định
* **Email:** `admin@gotopet.com` *(hoặc `admin@furever.com`)*
* **Mật khẩu (Password):** `admin123`

---

## 🛠 Xử Lý Sự Cố Thường Gặp (Troubleshooting)

<details>
<summary><strong>1. Lỗi SqlException: "Cannot open database / Login failed" hoặc không kết nối được</strong></summary>

- Kiểm tra xem dịch vụ SQL Server có đang ở trạng thái **Running** hay không (`services.msc`).
- Kiểm tra lại chuỗi `Server=` trong `appsettings.json` đã đúng với instance của bạn chưa (`localhost\SQLEXPRESS` hay `localhost`).
- Đảm bảo bạn đã thêm `TrustServerCertificate=True;`.
- Đảm bảo bạn đã hoàn thành **Bước 3** để database `GoToPet` thực sự tồn tại.
</details>

<details>
<summary><strong>2. Lỗi: "Invalid object name 'dbo.Pet'..." hoặc cơ sở dữ liệu trống</strong></summary>

- Bạn chưa chạy script tạo bảng. Hãy mở lại SSMS và thực thi tệp `database/01_schema.sql`, tiếp theo là `database/02_seed.sql`.
</details>

<details>
<summary><strong>3. Lỗi: "Failed to bind to address ... address already in use (Cổng 5190)"</strong></summary>

- Tiến trình ứng dụng `GoToPet.Web` lần trước vẫn đang chạy ngầm. Hãy mở Task Manager và kết thúc tác vụ `GoToPet.Web`, hoặc mở PowerShell chạy lệnh:
  ```powershell
  Stop-Process -Name GoToPet.Web -Force
  ```
</details>

<details>
<summary><strong>4. Lỗi biên dịch liên quan đến phiên bản .NET 10</strong></summary>

- Kiểm tra phiên bản bằng `dotnet --version`. Nếu chưa có bản 10.x, hãy tải và cài đặt **.NET 10 SDK**, sau đó khởi động lại Visual Studio hoặc Terminal.
</details>

---

## 🧱 Công Nghệ Cốt Lõi

- **Nền tảng:** ASP.NET Core MVC (.NET 10)
- **Truy xuất dữ liệu (ORM):** Entity Framework Core 10 (SQL Server Provider)
- **Hệ cơ sở dữ liệu:** Microsoft SQL Server 2022
- **Cơ chế xác thực:** Cookie Authentication kèm mã hóa mật khẩu an toàn BCrypt
- **Giao diện:** Razor Views kết hợp hệ thống CSS tùy biến tối ưu trải nghiệm người dùng

---

## 👤 Thông Tin Dự Án

- **Chủ dự án:** [honganh-goto](https://github.com/honganh-goto)
- **Kho lưu trữ:** [https://github.com/honganh-goto/gotopet](https://github.com/honganh-goto/gotopet)
