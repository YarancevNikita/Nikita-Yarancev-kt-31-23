# Nikita-Yarancev-kt-31-23

Учебный проект по дисциплине «Проектный практикум».

**Студент:** Яранцев Никита
**Группа:** КТ-31-23

## Описание

Веб-API на ASP.NET Core (.NET 9) + EF Core (PostgreSQL), логирование NLog, Swagger UI.
Через Swagger можно выполнить все операции с БД: просмотр, фильтрацию, добавление, изменение и удаление.

## Структура

- `Program.cs` — точка входа и конфигурация приложения
- `Models/` — сущности: специальность, группа, студент, дисциплина, оценка, зачет
- `Database/` — `StudentDbContext` и конфигурации таблиц
- `Migrations/` — миграции БД
- `Filters/` — классы-фильтры для методов получения списков
- `Requests/` — данные для добавления/изменения записей
- `Responses/` — результаты расчета среднего балла
- `Interfaces/` — интерфейсы сервисов и их реализации
- `ServiceExtensions/ServiceExtensions.cs` — регистрация сервисов в DI
- `Middlewares/ExceptionHandlerMiddleware.cs` — ответы 404/400 при ошибках
- `Controllers/` — контроллеры API

## Удаление

Группы, студенты и дисциплины удаляются **логически** (ставится признак `IsDeleted`).
При удалении группы удаляются и все её студенты. Удаленные записи можно восстановить (`PUT .../{id}/restore`).

## Методы API

У каждого контроллера: `GET /` — все записи, `GET /{id}` — запись по id, `POST /` — добавить,
`PUT /{id}` — изменить, `DELETE /{id}` — удалить.

| Контроллер     | Дополнительные методы |
|----------------|-----------------------|
| `/Specialties` | `POST /filter` — по коду специальности |
| `/Groups`      | `POST /filter` — по специальности, году набора, статусу удаления; `PUT /{id}/restore` |
| `/Students`    | `POST /filter` — по группе, ФИО, статусу удаления; `PUT /{id}/restore` |
| `/Disciplines` | `POST /filter` — по направлению (`Humanitarian` / `Technical`), статусу удаления; `PUT /{id}/restore` |
| `/Grades`      | `POST /student` — оценки студента; `POST /average/group-discipline` — средний балл по предмету в группе; `POST /average/year` — средний балл за год |
| `/Credits`     | `POST /student` — зачеты студента |

## Запуск

```bash
dotnet ef database update
dotnet run
```

Приложение стартует по адресу `http://localhost:5234`, Swagger — `http://localhost:5234/swagger`.
