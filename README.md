# Серверная часть системы учета СВТ и расходников

![.NET](https://img.shields.io/badge/.NET-6.0-blue)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17.2-blue)
![Swagger](https://img.shields.io/badge/Swagger-3.0-green)

Серверная часть приложения для учета средств вычислительной техники (СВТ) и расходников, реализованная на .NET 6.0.  
Предоставляет REST API для интеграции с веб- и мобильными клиентами.

## 🚀 Особенности

- Учет оборудования (принтеры, компьютеры и т.д.) и расходников (картриджи, лампы).
- Связь «многие ко многим» между оборудованием и совместимыми расходниками.
- Авторизация через JWT с ролевой моделью.
- Генерация отчетов по оборудованию.
- Логирование запросов и ошибок через Serilog.
- Документирование API через Swagger.

## 🛠 Технологии

- **Backend**: C#, .NET 6.0, ASP.NET Core WebAPI
- **База данных**: PostgreSQL, Entity Framework Core
- **Аутентификация**: JWT, ASP.NET Core Identity
- **Инструменты**: Swagger, Serilog, AutoMapper
- **Архитектура**: Domain-Driven Design (DDD), слоистая архитектура

## 📦 Установка и запуск

1. **Установите зависимости**:
   - [.NET 6.0 SDK](https://dotnet.microsoft.com/download)
   - [PostgreSQL 17+](https://www.postgresql.org/download/)

2. **Настройка базы данных**:
   - Создайте БД `svt_inventory` в PostgreSQL.
   - Обновите строку подключения в `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "InventoryConnection": "Host=ваш_хост;Port=5432;Database=svt_inventory;Username=ваш_логин;Password=ваш_пароль"
     }
     ```

3. **Примените миграции**:
   ```bash
   dotnet ef database update
