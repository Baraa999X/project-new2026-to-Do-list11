# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# نسخ ملف المشروع أولاً لاستعادة الحزم
COPY *.csproj ./
RUN dotnet restore

# نسخ باقي الملفات وبناء المشروع
COPY . .
RUN dotnet publish -c Release -o /app

# مرحلة التشغيل
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# التأكد من استخدام المنفذ 10000
ENV ASPNETCORE_URLS=http://+:10000

# تشغيل التطبيق
ENTRYPOINT ["dotnet", "project2026ToDoList.dll"]