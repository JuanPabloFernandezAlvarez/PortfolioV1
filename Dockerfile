# --- Etapa 1: build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todo (necesario porque hay múltiples proyectos referenciados entre sí)
COPY . .

# Restauramos usando el archivo de solución
RUN dotnet restore PortfolioV1.slnx

# Publicamos el proyecto de arranque (el que tiene Program.cs / el .csproj ejecutable)
RUN dotnet publish PortfolioV1/PortfolioV1.csproj -c Release -o /app/publish --no-restore

# --- Etapa 2: runtime (imagen liviana, sin el SDK completo) ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production

# Render inyecta el puerto real en la variable de entorno PORT recién al arrancar el
# contenedor, por eso usamos shell form: así $PORT se resuelve en runtime, no en build.
ENTRYPOINT dotnet PortfolioV1.dll --urls http://+:$PORT
