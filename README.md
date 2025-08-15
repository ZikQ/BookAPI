# BookAPI
Just a pet project)


# 📖 BookAPI

**BookAPI** — это RESTful API для работы с книгами и отзывами.  
Поддерживает регистрацию, аутентификацию через JWT, роли пользователей, а также CRUD-операции с пагинацией, сортировкой и фильтрацией.

---

## 🚀 Возможности

- **Регистрация и авторизация** (JWT)
- **Роли пользователей** (User, Admin, Librarian)
- **CRUD для книг**
- **CRUD для отзывов**
- **Валидация входных данных**
- **Пагинация и сортировка**
- **Авторизация на уровне ролей и владельца ресурса**
- **Swagger-документация**

---

## 🛠 Технологии

- **.NET 8** + **ASP.NET Core Web API**
- **Entity Framework Core**
- **JWT Authentication**
- **Swagger / OpenAPI**
- **PostgreSQL Server**
---

## 📂 Структура проекта

BookAPI/
│
├── Configurations/ # Конфигураторы для моделей
├── Controllers/ # Контроллеры API
├── Data/ # Миграции и контекст
├── DTOs/ # Сущности взаимодействия
├── Extensions/ # Расширения
├── Helpers/ # Утилиты
├── Models/ # Сущности базы данных
├── Repositories/ # Доступ к данным
├── Services/ # Бизнес-логика
├── Config/ # Настройки приложения
└── Program.cs # Точка входа

---

## ⚙️ Установка и запуск

1. Клонировать репозиторий:
git clone https://github.com/username/BookAPI.git
cd BookAPI

2. Установить зависимости:
dotnet restore

3. Настроить подключение к БД в appsettings.json:
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=books;Username=postgres;Password=admin"
  }

4. Применить миграции:
dotnet ef database update

5. Запустить проект:
dotnet run
