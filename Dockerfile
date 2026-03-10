# Stage 1: Build Vue frontend
FROM node:22-alpine AS frontend-build
WORKDIR /app/frontend
COPY frontend/package*.json ./
RUN npm ci
COPY frontend/ ./
RUN npm run build

# Stage 2: Build .NET backend
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS backend-build
WORKDIR /app/backend
COPY backend/TodoApp.sln ./
COPY backend/TodoApi/TodoApi.csproj TodoApi/
COPY backend/TodoApi.Tests/TodoApi.Tests.csproj TodoApi.Tests/
RUN dotnet restore
COPY backend/ ./
RUN dotnet publish TodoApi/TodoApi.csproj -c Release -o /publish

# Stage 3: Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=backend-build /publish ./
COPY --from=frontend-build /app/frontend/dist ./wwwroot
EXPOSE 8080
ENTRYPOINT ["dotnet", "TodoApi.dll"]
