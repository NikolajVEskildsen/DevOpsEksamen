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
- The Vue frontend is served as static files from the ASP.NET Core app (`wwwroot/`), so both run in a single Docker container on port 8080.

## PR Review (Claude API)

The Claude API is used to automatically review pull requests. When reviewing:

- **Review**: C# (`.cs`) and Vue (`.vue`) files for functionality — correctness, logic, bugs, and behaviour.
- **Skip**: `.yml`/`.yaml` files, and common data/config files (`.json`, `.xml`, `.csproj`, `.sln`).
- **Do not** flag linting or formatting issues — these are already enforced by the CI pipeline.
