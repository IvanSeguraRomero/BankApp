FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#crea un directorio/carpeta es como un mkdir
WORKDIR /app

COPY bankapp.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bankapp.dll"]

#creado por el PROFESOR
# FROM mcr.microsoft.com/dotnet/sdk:6.0
# WORKDIR /src
# COPY . .
# RUN dotnet publish "bankapp.csproj" -c Release -o /app
# ENTRYPOINT ["dotnet", "bankapp.dll"]