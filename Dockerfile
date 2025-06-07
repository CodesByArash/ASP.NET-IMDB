# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# کپی فایل csproj و restore کردن
COPY api/api.csproj ./api/
RUN dotnet restore ./api/api.csproj

# کپی بقیه فایل‌ها
COPY api/. ./api/

# بیلد پروژه و پابلیش خروجی
RUN dotnet publish ./api/api.csproj -c Release -o /app/publish

# ساخت ایمیج نهایی برای اجرا
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]
