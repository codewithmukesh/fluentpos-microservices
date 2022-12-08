FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /source

COPY . .

RUN dotnet restore ./Gateway.csproj
RUN dotnet build -c Release --no-restore ./Gateway.csproj

WORKDIR /source

RUN dotnet publish -c Release --no-build -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=build  /app ./

ENTRYPOINT ["dotnet", "FluentPOS.Gateway.dll"]