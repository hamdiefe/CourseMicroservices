FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/*.csproj Shared/Course.Shared/
COPY Services/PhotoStock/Course.Services.PhotoStock/*.csproj Services/PhotoStock/Course.Services.PhotoStock/
RUN dotnet restore Services/PhotoStock/Course.Services.PhotoStock/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/Course.Services.PhotoStock/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Services.PhotoStock.dll" ]