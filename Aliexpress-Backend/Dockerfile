FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем всё содержимое проекта
COPY . .

# Переходим в директорию с основным проектом
WORKDIR /src/Aliexpress-Backend

# Выполняем восстановление зависимостей и сборку
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Aliexpress-Backend.dll"]