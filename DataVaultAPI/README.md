# DataVaultAPI

Thi is an enterprise-grade, high-performance RESTful backend API built with **C# (.NET)** and **Dapper**, designed for secure relational data management and structured record tracking using Microsoft SQL Server.

---

## 🚀 Key Features

* **Micro-ORM Architecture (Dapper):** Leverages raw, optimized SQL queries mapped directly to strongly-typed C# models, ensuring peak database performance and complete developer control over query execution.
* **Repository Pattern:** Implements a clean separation of concerns (`IVaultRepository` / `VaultRepository`), making the data access layer modular, testable, and maintainable.
* **Security First:** Utilizes strict parameterized SQL queries to completely eliminate vulnerabilities to SQL injection attacks.
* **Full CRUD Support:** Complete Create, Read, Update, and Delete operations exposed through modern .NET endpoints.
* **Interactive Documentation:** Built-in Swagger/OpenAPI support for rapid endpoint testing and inspection.

---

## 🛠️ The Tech Stack

* **Language:** C#
* **Framework:** .NET 8 / .NET 10 Web API
* **Data Access:** Dapper, Microsoft.Data.SqlClient
* **Database:** Microsoft SQL Server
* **Version Control:** Git & GitHub

---

## 📂 Project Architecture

```text
DataVaultAPI/
│
├── Models/
│   └── VaultRecord.cs        # Domain entity representing core data structure
│
├── Repositories/
│   ├── IVaultRepository.cs   # Data contract interface
│   └── VaultRepository.cs    # Dapper implementation with raw SQL execution
│
├── Properties/
│   └── launchSettings.json   # Local server configuration
│
├── appsettings.json          # Application configuration & connection strings
├── database.sql              # Relational database schema script
├── Program.cs                # Entry point, dependency injection & route mapping
└── DataVaultAPI.csproj       # Project dependencies and package references