FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/*.csproj Shared/Course.Shared/

COPY Services/Order/Course.Services.Order.Application/*.csproj Services/Order/Course.Services.Order.Application/

COPY Services/Order/Course.Services.Order.Domain/*.csproj Services/Order/Course.Services.Order.Domain/

COPY Services/Order/Course.Services.Order.Domain.Core/*.csproj Services/Order/Course.Services.Order.Domain.Core/

COPY Services/Order/Course.Services.Order.Infrastructure/*.csproj Services/Order/Course.Services.Order.Infrastructure/


COPY Services/Order/Course.Services.Order.API/*.csproj Services/Order/Course.Services.Order.API/
RUN dotnet restore Services/Order/Course.Services.Order.API/*.csproj
COPY . .
RUN dotnet publish Services/Order/Course.Services.Order.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Services.Order.API.dll" ]