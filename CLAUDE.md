# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Tech Stack

- **Backend**: C# ASP.NET Core (.NET 9, targeting .NET 10 when available)
- **Frontend**: Vue 3

## Project Structure

```
/
├── backend/
│   ├── TodoApp.sln
│   ├── TodoApi/            # ASP.NET Core Web API
│   │   ├── Controllers/    # TodoController
│   │   ├── Models/         # TodoItem
│   │   └── Services/       # TodoService (in-memory, singleton)
│   └── TodoApi.Tests/      # xUnit tests for TodoService
└── frontend/               # Vue 3 app (not yet scaffolded)
```

## Common Commands

### Backend (run from `backend/`)

```bash
dotnet build
dotnet run --project TodoApi
dotnet test
dotnet test --filter "DisplayName~MethodName"
```

### Frontend (run from `frontend/`)

```bash
npm install
npm run dev
npm run build
npm run test
npm run lint
```

## Architecture Notes

- `TodoService` is registered as a singleton and holds an in-memory list with 3 stub items seeded at startup.
- Tests directly instantiate `TodoService` — no mocking needed since there are no external dependencies.
- Swagger UI is available at `/swagger` in development.
