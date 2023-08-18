#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProducerApp/ProducerApp.csproj", "ProducerApp/"]
COPY ["DBLayer/DBLayer.csproj", "DBLayer/"]
RUN dotnet restore "ProducerApp/ProducerApp.csproj"
COPY . .
WORKDIR "/src/ProducerApp"
RUN dotnet build "ProducerApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProducerApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProducerApp.dll"]