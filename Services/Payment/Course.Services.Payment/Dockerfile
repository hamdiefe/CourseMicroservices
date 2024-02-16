FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/*.csproj Shared/Course.Shared/
COPY Services/Payment/Course.Services.Payment/*.csproj Services/Payment/Course.Services.Payment/
RUN dotnet restore Services/Payment/Course.Services.Payment/*.csproj
COPY . .
RUN dotnet publish Services/Payment/Course.Services.Payment/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Course.Services.Payment.dll" ]