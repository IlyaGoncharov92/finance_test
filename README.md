# finance_test

Для запуска:
- Запущенный Postgres
- Отредактировать `ConnectionString` в `Auth.API.appsettings.json` and `Finance.API.appsettings.json`
- Тесты запускаются с `Testcontainers` (запустить `docker`)
- Запустить одновременно три проекта: `Gateway.API` `Auth.API` `Finance.API`. Обращаться только к `Gateway.API` 

## Проекты

- Gateway.API - port: 5100
- Auth.API - port: 5101
- Finance.API - port: 5102

## Endpoints

### Register

**POST** `/api/auth/register`

#### Request body:
```json
{
  "email": "user@example.com",
  "name": "user",
  "password": "string"
}
```

### Login

**POST** `/api/auth/login`

#### Request body:
```json
{
  "email": "user@example.com",
  "password": "string"
}
```

### Add Favorite Currencies

**POST** `/api/finance/favorites`

#### Request body:
```json
{
  "CurrencyIds": [
    "05c4dbc6-cffb-47f2-a50f-dae5f5e8677b",
    "0da147f7-3884-4660-b2b6-24a8be0d6ceb",
    "0947d215-63f5-43ca-a450-a43620277a69"
  ]
}
```

### Get Favorite Currencies

**GET** `/api/finance/favorites`

