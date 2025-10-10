# TodoX

A modern RESTful API for managing to-do lists, built with **ASP.NET Core** and **Entity Framework Core**, following **Domain-Driven Design (DDD)** principles.

---

## 🚀 Features

- ✅ User registration and authentication (JWT)
- 🧾 Full CRUD for Todo Lists
- 🗂️ Full CRUD for Todo Items
- 🔒 Protected routes with JWT authentication
- 🧱 Clean architecture with domain separation
- 🐳 Docker & docker-compose support

---

## 🧩 Project Structure

```
TodoX/
├── docker-compose.yml         # Docker environment configuration
├── migrate.sh                 # Script to run release a migration
├── ROADMAP                    # Planned features and improvements
├── src/
│   ├── TodoX.API/             # API layer (Controllers, Program.cs)
│   ├── TodoX.Application/     # Application layer (DTOs, Services, Interfaces)
│   ├── TodoX.Domain/          # Domain layer (Entities, ValueObjects, Interfaces)
│   └── TodoX.Infrastructure/  # Infrastructure (Repositories, EF Core, JWT, Fluent API)
└── TodoX.sln                  # Solution file
```

---

## 🛠️ Technologies

- **.NET 9.0**
- **Entity Framework Core**
- **Fluent API**
- **ASP.NET Core Web API**
- **JWT (JSON Web Token)**
- **PostgreSQL**
- **Docker & Docker Compose**
- **Domain-Driven Design (DDD)**

---

## ⚙️ Setup and Run

### 🐳 Docker

```bash
git clone https://github.com/Miguel0101/TodoX.git
cd TodoX
docker compose up -d
```

Once started, the API will be available at:  
👉 **http://localhost:6789**

## 🔑 JWT Authentication

Authentication is handled using **Bearer Tokens (JWT)**.  
After logging in, include your token in the `Authorization` header:

```
Authorization: Bearer YOUR_TOKEN_HERE
```

---

## 📡 Main Endpoints

### 👤 User

| Method | Route                | Description               | Auth |
|--------|----------------------|---------------------------|------|
| POST   | `/api/user/register` | Register a new user       | ❌   |
| POST   | `/api/user/login`    | Login and get JWT token   | ❌   |
| GET    | `/api/user`       | Get logged user info      | ✅   |

### 🧾 Todo Lists

| Method | Route             | Description             | Auth |
|--------|------------------|-------------------------|------|
| GET    | `/api/todolists` | Get all todo lists      | ✅   |
| GET    | `/api/todolists/{id}` | Get a todo list by id      | ✅   |
| POST   | `/api/todolists` | Create a new todo list  | ✅   |
| PUT    | `/api/todolists/{id}` | Update a list        | ✅   |
| DELETE | `/api/todolists/{id}` | Delete a list        | ✅   |

### 🗂️ Todo Items

| Method | Route                                              | Description             | Auth |
|--------|----------------------------------------------------|-------------------------|------|
| GET    | `/api/todolists/{listId}/items`                   | Get all items in a list | ✅   |
| GET    | `/api/todolists/{listId}/items/{id}`               | Get a item in a list by id | ✅   |
| POST   | `/api/todolists/{listId}/items`                   | Create a new item       | ✅   |
| PUT  | `/api/todolists/{listId}/items/{id}` | Update a item   | ✅   |
| PATCH  | `/api/todolists/{listId}/items/{id}/complete` | Mark item as complete   | ✅   |
| DELETE | `/api/todolists/{listId}/items/{id}`          | Delete item             | ✅   |

---

## 🧪 Quick Test with `curl`

```bash
curl -X POST http://localhost:6789/api/user/login -H "Content-Type: application/json" -d '{ "email": "admin@example.com", "password": "123456" }'
```
#### JSON Result
```bash
{
  "errorCode": 1,
  "message": "User not found."
}
```

---

## 📜 License

Distributed under the **MIT License**.  
See [`LICENSE`](./LICENSE) for details.

---

## 👨‍💻 Author

**Miguel Magno**  
Back-end developer passionate about clean architecture and DDD.  
📧 [miguelmagnofn123@gmail.com](mailto:miguelmagnofn123@gmail.com)  
💼 [GitHub - Miguel0101](https://github.com/Miguel0101)

---

> _“Software architecture is not a luxury; it determines the longevity of your system.”_  
> **— Robert C. Martin**
