#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OfficesManager.API/OfficesManager.API/OfficesManager.API.csproj", "OfficesManager.API/OfficesManager.API/"]
COPY ["OfficesManager.API/OfficesManager.Database/OfficesManager.Database.csproj", "OfficesManager.API/OfficesManager.Database/"]
COPY ["OfficesManager.API/OfficesManager.Domain/OfficesManager.Domain.csproj", "OfficesManager.API/OfficesManager.Domain/"]
COPY ["OfficesManager.API/OfficesManager.DTO/OfficesManager.DTO.csproj", "OfficesManager.API/OfficesManager.DTO/"]
COPY ["OfficesManager.API/OfficesManager.Contracts/OfficesManager.Contracts.csproj", "OfficesManager.API/OfficesManager.Contracts/"]
RUN dotnet restore "OfficesManager.API/OfficesManager.API/OfficesManager.API.csproj"
COPY . .
WORKDIR "/src/OfficesManager.API/OfficesManager.API"
RUN dotnet build "OfficesManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OfficesManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OfficesManager.API.dll"]