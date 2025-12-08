# Imagen base para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Puerto que se va a usar la app dentro del contenedor
EXPOSE 10000

# 👇 ESTA LÍNEA AYUDA A QUE KESTREL ESCUCHE EN ESE PUERTO
ENV ASPNETCORE_URLS=http://0.0.0.0:10000

# Imagen para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["AlmacenVencimientos.csproj", "./"]
RUN dotnet restore "AlmacenVencimientos.csproj"

COPY . .
RUN dotnet publish "AlmacenVencimientos.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AlmacenVencimientos.dll"]