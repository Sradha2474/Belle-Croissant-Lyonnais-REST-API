# 🥐 Session1Api — Belle Croissant Lyonnais REST API

> A fully-featured ASP.NET Core Web API built on **.NET 10** for managing products, orders, and customer accounts for the *Belle Croissant Lyonnais* bakery system — secured with HTTP Basic Authentication and backed by SQL Server via Entity Framework Core.

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [API Endpoints](#-api-endpoints)
- [Authentication](#-authentication)
- [Database Setup](#-database-setup)
- [Getting Started](#-getting-started)
- [Swagger UI](#-swagger-ui)

---

## 🌟 Overview

Session1Api is a RESTful backend service providing complete CRUD operations across three core domains:

| Domain | Description |
|---|---|
| **Accounts** | Customer account management |
| **Products** | Bakery product catalogue with pricing |
| **Orders** | Order lifecycle management with status transitions |

---

## 🛠 Tech Stack

| Technology | Version |
|---|---|
| ASP.NET Core | .NET 10 |
| Entity Framework Core | Latest |
| SQL Server | LocalDB / Full SQL Server |
| Swashbuckle (Swagger) | Latest |
| Authentication | HTTP Basic Auth (custom handler) |

---

## 📁 Project Structure

```
Session1Api/
├── Controllers/
│   ├── AccountsController.cs   # Customer account CRUD
│   ├── OrdersController.cs     # Order management & status transitions
│   └── ProductsController.cs   # Product catalogue CRUD (auth-protected)
├── Models/
│   ├── Account.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── Product.cs
│   ├── Store.cs
│   └── AppDBContext.cs         # EF Core DB context
├── BasicAuth.cs                # Custom Basic Authentication handler
├── Program.cs                  # App entry point & service registration
└── appsettings.json
```

---

## 📡 API Endpoints

### 👤 Accounts — `/api/Accounts`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/Accounts` | Get all accounts |
| `GET` | `/api/Accounts/{id}` | Get account by ID |
| `POST` | `/api/Accounts` | Create new account |
| `PUT` | `/api/Accounts/{id}` | Update account |
| `DELETE` | `/api/Accounts/{id}` | Delete account |

---

### 📦 Products — `/api/Products` *(🔒 Requires Auth)*

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/Products` | Get all products |
| `GET` | `/api/Products/{id}` | Get product by ID |
| `POST` | `/api/Products` | Create new product |
| `PUT` | `/api/Products/{id}` | Update product |
| `DELETE` | `/api/Products/{id}` | Delete product (cascades OrderItems) |

---

### 🧾 Orders — `/api/Orders`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/Orders` | Get latest 100 orders (with customer name) |
| `GET` | `/api/Orders/{id}` | Get order details with line items |
| `POST` | `/api/Orders` | Create new order |
| `PUT` | `/api/Orders/{id}` | Update order |
| `DELETE` | `/api/Orders/{id}` | Delete order |
| `PUT` | `/api/Orders/{id}/complete` | Transition order to **Completed** (status 2 → 3) |
| `PUT` | `/api/Orders/{id}/cancel` | Cancel an order (status → 4) |

#### Order Status Codes

| Code | Status |
|---|---|
| `1` | Pending |
| `2` | Processing |
| `3` | Completed |
| `4` | Cancelled |

---

## 🔐 Authentication

Product endpoints are protected with **HTTP Basic Authentication**.

Encode your credentials in Base64 and pass them in the `Authorization` header:

```
Authorization: Basic <base64(username:password)>
```

**Default credentials:**

| Username | Password |
|---|---|
| `staff` | `WSA2025` |

**Example using curl:**
```bash
curl -H "Authorization: Basic c3RhZmY6V1NBMjAyNQ==" https://localhost:7000/api/Products
```

> ⚠️ **Note:** For production use, replace the hardcoded credentials with a secure credential store or proper identity provider.

---

## 🗄 Database Setup

This project uses **SQL Server** with EF Core (Database-First).

The connection string is configured in `Program.cs` and `AppDBContext.cs`:

```
Data Source=localhost;
Initial Catalog=BelleCroissantLyonnais;
Integrated Security=True;
TrustServerCertificate=True
```

Update this in `appsettings.json` before running:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=BelleCroissantLyonnais;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

### Database Schema

```
Account ──< Order ──< OrderItem >── Product
Store   ──< Order
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or remote)
- Git

### Clone & Run

```bash
# 1. Clone the repository
git clone https://github.com/YOUR_USERNAME/Session1Api.git
cd Session1Api

# 2. Restore dependencies
dotnet restore

# 3. Update the connection string in appsettings.json

# 4. Run the application
dotnet run --project Session1Api
```

The API will be available at:
- `https://localhost:7000`
- `http://localhost:5000`

---

## 📖 Swagger UI

Interactive API documentation is available at:

```
https://localhost:7000/swagger
```

All endpoints are listed and testable directly from the browser. Use the **Authorize** button to enter Basic Auth credentials for protected routes.

---

## 📝 License

This project was developed as part of an academic assessment. All rights reserved.