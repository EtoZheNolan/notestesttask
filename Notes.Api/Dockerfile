﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Notes.Api/Notes.Api.csproj", "Notes.Api/"]
RUN dotnet restore "Notes.Api/Notes.Api.csproj"
COPY . .
WORKDIR "/src/Notes.Api"
RUN dotnet build "Notes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notes.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notes.Api.dll"]
