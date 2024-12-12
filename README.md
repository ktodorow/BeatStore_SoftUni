
# BeatStore

BeatStore is a modern web application designed to serve as a platform for selling, managing, and discovering beats for music production. 

---

## **Key Features**
- **Admin Management**:
  - Manage user roles and permissions.
  - Oversee beats uploaded by users.
- **User Features**:
  - Upload beats with details like price, audio file, and cover art.
  - Browse, play, and purchase beats.
  - Add beats to cart and complete transactions.

---

## **Project Structure**
- `BeatStore_SoftUni`:
  - Core ASP.NET project containing configuration and startup logic.
- `BeatStore_SoftUni.Data`:
  - Entity Framework Core database context and migrations.
  - Models and seed data for initial setup.
- `BeatStore_SoftUni.Services.Data`:
  - Services for handling business logic (e.g., BeatService, PurchaseService).
- `BeatStore_SoftUni.ViewModels`:
  - View models for data transfer between the front-end and back-end.
- `BeatStore_SoftUni.Common`:
  - Shared utilities and constants.

---

## **Technology Stack**
- **Back-end**: ASP.NET Core 8.0
- **Front-end**: Razor Pages, JavaScript, SweetAlert2, Bootstrap
- **Database**: SQL Server
- **Containerization**: Docker and Docker Compose

---

## **Getting Started**
Follow the [DEPLOY.md](DEPLOY.md) file for detailed deployment instructions.
