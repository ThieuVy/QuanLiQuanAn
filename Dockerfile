# Sử dụng image .NET SDK chính thức để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Sao chép các tệp dự án và khôi phục các gói phụ thuộc
COPY *.csproj ./
RUN dotnet restore

# Sao chép các tệp còn lại và xây dựng ứng dụng
COPY . ./
RUN dotnet publish -c Release -o out

# Sử dụng image runtime .NET chính thức để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Đặt entry point
ENTRYPOINT ["dotnet", "QuanLiQuanAn.dll"]
