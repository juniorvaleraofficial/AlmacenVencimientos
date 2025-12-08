# Imagen base para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Puerto que se va a usar la app dentro del contenedor
EXPOSE 10000

# Imagen para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# ⬇️ CAMBIA ESTO por el nombre real de tu .csproj
COPY ["AlmacenVencimientos.csproj", "./"]

RUN dotnet restore "AlmacenVencimientos.csproj"

# Copiamos todo el código
COPY . .

# Publicamos en modo Release
RUN dotnet publish "AlmacenVencimientos.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final con solo lo necesario para correr
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# ⬇️ CAMBIA ESTO por el nombre real de tu .dll
ENTRYPOINT ["dotnet", "AlmacenVencimientos.dll"]
