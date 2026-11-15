# FixIt — Backend REST API

Centralized **ASP.NET Core Web API** for the **FixIt** Field Service & Infrastructure Management System. It serves as the single source of truth, handling business logic, JWT authentication, file uploads, and database operations for both the Blazor web portal and the Android mobile client.

### Related Repositories
* **Web Portal (Blazor):** [FixIt-Web]()
* **Mobile Client (Android - Java):** [FixIt-Android]()

---

## Architecture & Key Features

* **Layered Architecture:** Clean separation of concerns across `Controllers -> Services -> Repositories -> DbContext`.
* **Normalized SQL Server Database:** Designed using a Database-First approach (`Scaffold-DbContext`), featuring:
  * Separated credential storage (`UserLocalAuth` & OAuth-ready design).
  * Flexible user-to-ticket assignment tracking (`IsRepairer`) allowing both official technicians and regular users to resolve open issues.
  * Shared attachment architecture (`FileUpload` + relational join tables) to eliminate sparse `NULL` columns.
  * Dynamic priority weighting and manageable system dictionaries (`Categories`, `Priorities`, `Statuses`, `Roles`).
* **Security:** Password hashing with cryptographic salting and **JWT Bearer** role-based authorization (`Reporter`, `Technician`, `Dispatcher`).
* **Shared Library (`FixIt.Shared`):** Contains DTOs, validation attributes, and enums shared with the Blazor frontend.
* **DevOps Ready:** Containerized via **Docker** with automated build and test execution configured for **Jenkins**.

---

## Tech Stack
* **Framework:** .NET 10 / ASP.NET Core Web API
* **ORM & Database:** Entity Framework Core, Microsoft SQL Server
* **Authentication:** JWT (JSON Web Tokens)
* **Containers & CI/CD:** Docker, Docker Compose, Jenkins

---



### Prerequisites
* [.NET 10]
* [Microsoft SQL Server] & SSMS (or Docker)


