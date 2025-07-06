
# Login & Dashboard Mini Project

This is a simple full-stack application built as a hiring task to demonstrate core skills in Angular (frontend) and .NET Core Web API (backend), including JWT authentication, protected routing, a styled dashboard, and Redis-based distributed rate limiting.

## Features

- **Login Page**: Authenticates with hardcoded credentials (`admin` / `admin`) and returns a JWT token.
- **Dashboard**: Protected route that displays a bar chart based on hardcoded data.
- **JWT Authentication**: Login returns a token stored in localStorage, used to access protected endpoints.
- **Rate Limiting**: Redis-based distributed rate limiting is applied on the login endpoint to prevent brute-force attempts.
- **Modern UI**: Styled login and dashboard views using Bootstrap and CSS.

## Tech Stack

- **Frontend**: Angular 18, NgCharts (wrapper around Chart.js), Bootstrap
- **Backend**: ASP.NET Core 8 Web API
- **Rate Limiting**: AspNetCoreRateLimit + Redis
- **Token Handling**: JWT via `Microsoft.IdentityModel.Tokens`
- **Development Tools**: Docker (for Redis), Postman/cURL for API testing

## Setup Instructions

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [Docker](https://www.docker.com/products/docker-desktop)

### 1. Clone the Project

```bash
git clone https://github.com/GameDevLight/Fantacode.git
cd login-dashboard
```

### 2. Run Redis with Docker

Create a `docker-compose.yml` file:

```yaml
services:
  redis:
    image: redis:latest
    ports:
      - "6379:6379"
```

Then run:

```bash
docker compose up -d
```

### 3. Backend Setup (.NET)

```bash
cd LoginDashboardAPI
dotnet restore
dotnet run
```

The API will run at `http://localhost:5270`

### 4. Frontend Setup (Angular)

```bash
cd login-dashboard
npm install
ng serve
```

Runs the app at `http://localhost:4200`

## Testing the App

- **Login**: Navigate to `http://localhost:4200` and log in with:
  - Username: `admin`
  - Password: `admin`

- **Dashboard**: After login, the user is redirected to a dashboard showing a bar chart of ticket statuses.

- **Rate Limiting**: To test, try sending 6+ login requests quickly using curl:

```bash
for i in {1..10}; do
  curl -X POST http://localhost:5270/api/auth/login \
    -H "Content-Type: application/json" \
    -d "{\"username\":\"admin\", \"password\":\"admin\"}"
  echo "---"
done
```

Only 5 will succeed per minute.

## Project Structure

```
LoginDashboardAPI/
├── Controllers/
│   ├── AuthController.cs
│   └── DashboardController.cs
├── Models/
│   └── LoginModel.cs
├── Program.cs
└── RedisRateLimitCounterStore.cs

login-dashboard/
├── src/app/
│   ├── login/
│   ├── dashboard/
│   ├── auth.guard.ts
│   ├── auth.service.ts
│   ├── auth.interceptor.ts
│   └── app.module.ts
```

## Why These Technologies?

- **Angular**: Clean architecture, routing, guards, and good chart support.
- **.NET Core API**: Powerful backend framework, JWT support, easy DI.
- **Redis**: Simple, fast, scalable option for distributed rate limiting.
- **AspNetCoreRateLimit**: Well-supported package for configurable IP and endpoint throttling.

## Notes

- The UI has been styled to give a clean, modern look with simple responsiveness.
- Authentication tokens are securely stored and attached via an HTTP interceptor.
- The backend is easily extensible for database integration or user roles.
