﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["API/API.csproj", "API/"]
COPY ["API.Infrastructure/API.Infrastructure.csproj", "API.Infrastructure/"]
COPY ["API.Domain/API.Domain.csproj", "API.Domain/"]
COPY ["API.Application/API.Application.csproj", "API.Application/"]
RUN dotnet restore
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
