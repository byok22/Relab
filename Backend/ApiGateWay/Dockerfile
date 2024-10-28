# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copiar el archivo csproj y restaurar las dependencias
COPY ApiGateWay/ApiGateWay.csproj ApiGateWay/
WORKDIR /source/ApiGateWay
RUN dotnet restore

# Copiar el resto del código y compilar
COPY ApiGateWay/. .
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Configurar el puerto de escucha y ejecutar la aplicación
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "ApiGateWay.dll"]
