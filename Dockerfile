# --- Etapa 1: build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todo (necesario porque hay múltiples proyectos referenciados entre sí)
COPY . .

# Publicamos directo desde el csproj del proyecto de arranque.
# dotnet publish restaura automáticamente todos los proyectos referenciados,
# así que no hace falta pasar por el .slnx (formato no soportado por el SDK 8.0).
RUN dotnet publish PortfolioV1/PortfolioV1.csproj -c Release -o /app/publish

# --- Etapa 2: runtime (imagen liviana, sin el SDK completo) ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production

# Render inyecta el puerto real en la variable de entorno PORT recién al arrancar el
# contenedor, por eso usamos shell form: así $PORT se resuelve en runtime, no en build.
ENTRYPOINT dotnet PortfolioV1.dll --urls http://+:$PORT
