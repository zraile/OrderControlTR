FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/OrderControlTR.API/OrderControlTR.API.csproj", "src/OrderControlTR.API/"]
COPY ["src/OrderControlTR.Application/OrderControlTR.Application.csproj", "src/OrderControlTR.Application/"]
COPY ["src/OrderControlTR.Infrastructure/OrderControlTR.Infrastructure.csproj", "src/OrderControlTR.Infrastructure/"]
COPY ["src/OrderControlTR.Domain/OrderControlTR.Domain.csproj", "src/OrderControlTR.Domain/"]
RUN dotnet restore "src/OrderControlTR.API/OrderControlTR.API.csproj"
COPY . .
WORKDIR "/src/src/OrderControlTR.API"
RUN dotnet build "OrderControlTR.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderControlTR.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderControlTR.API.dll"]
