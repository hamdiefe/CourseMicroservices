FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80

COPY  Course.Gateway/*.csproj  Course.Gateway/
RUN dotnet restore Course.Gateway/*.csproj
COPY . .
RUN dotnet publish Course.Gateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Gateway.dll" ]